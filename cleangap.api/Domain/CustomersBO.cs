using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cleangap.api.DAL;
using cleangap.api.Models.Domain;
using cleangap.api.Models.Tables;
using cleangap.api.Services.Security;
using cleangap.api.Services.Mailing;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Configuration;

namespace cleangap.api.Domain
{
    public interface ICustomersBO
    {
        bool Register(CustomerModel data);
        //bool SendTokenEmail(CustomerModel data);
        //CustomerModel FindByToken(string token);
        //bool ResetPassword(ResetPasswordModel data);
        Task<LoginResponseModel> Authenticate(string UserName, string Password);
    }

    public class CustomersBO : ICustomersBO
    {
        private Cryptography crypto = new Cryptography();

        #region Private Methods
        private string EncodeMD5(string plainText)
        {
            Cryptography crypto = new Cryptography();
            string passwordmd5 = crypto.EncodeMD5(plainText);
            return passwordmd5;
        }
        private string GenerateToken(string email)
        {
            Cryptography crypto = new Cryptography();

            string md5Email = crypto.EncodeMD5(email);
            string md5Date = crypto.EncodeMD5(DateTime.Now.ToString());

            string hashEmail = string.Format("{0}{1}", md5Email, md5Date);

            return hashEmail;

        }
        private customers UpdateToken(CleanGapDataContext db, customers c)
        {
            c.token_forgot_pass = GenerateToken(c.email);
            c.token_expire = DateTime.Now.AddHours(3);

            db.Entry(c).State = EntityState.Modified;

            db.SaveChanges();

            return c;
        }
        private bool UpdateNewPassword(ResetPasswordModel data, CleanGapDataContext db, customers pCust)
        {
            db.Entry(pCust).State = EntityState.Modified;
            pCust.password = EncodeMD5(data.NewPassword);
            pCust.token_forgot_pass = null;
            pCust.token_expire = null;

            return db.SaveChanges() > 0;

        }
        private bool SetConfirmedUser(CleanGapDataContext db, customers pCust)
        {
            if (pCust.confirmation_date == null)
            {
                db.Entry(pCust).State = EntityState.Modified;
                pCust.confirmation_date = DateTime.Now;
                pCust.token_signin = null;

                return db.SaveChanges() > 0;
            }
            return false;
        }
        private void SendConfirmationMail(CustomerModel data)
        {
            MailingService m = new MailingService();

            StringBuilder sbBody = new StringBuilder();

            sbBody.AppendFormat("Dear, {0}", data.name.ToUpper());
            sbBody.AppendLine();
            sbBody.AppendLine();
            sbBody.AppendLine("Welcome to RepSpark and thank you for registering on the RepSpark website.");
            sbBody.AppendLine();
            sbBody.AppendLine("RepSpark is the world's leading B2B selling system built for Total Order Life Cycle Management.");
            sbBody.AppendLine("TOLCM is a seamless end-to-end sales system that effectively enhances and manages every step in an organization’s order life cycle.");
            sbBody.AppendLine();
            sbBody.AppendLine("If you need assistance, please contact us at helpdesk@repspark.com");
            sbBody.AppendLine();
            sbBody.AppendLine("Thank you for your interest in RepSpark. We are glad you have taken the first step in becoming engaged with us.");
            sbBody.AppendLine();
            sbBody.AppendLine("Best wishes,");
            sbBody.AppendLine("RepSpark Customer Care");

            m.SendMail(data.email, sbBody.ToString(), "Clean Gap Welcome E-Mail");
        }
        private void SendTokenRecoveryMail(customers pCust)
        {

            var request = HttpContext.Current.Request;

            var BaseUrl = ConfigurationManager.AppSettings["WebUrl"].ToString();
            var UrlPasswordReset = string.Format("{0}/password-reset?q={1}", BaseUrl, pCust.token_forgot_pass);

            MailingService m = new MailingService();

            StringBuilder sbBody = new StringBuilder();

            sbBody.AppendFormat("Hello, {0}", pCust.name.ToUpper());
            sbBody.AppendLine();
            sbBody.AppendLine();
            sbBody.AppendLine("You have received this e-mail because you forgot your password and you wish to create a new one.");
            sbBody.AppendLine();
            sbBody.AppendFormat("Hence, the password reset URL is {0}. Just click on the link or select it for copying and pasting in your favorite browser's address bar.", UrlPasswordReset);
            sbBody.AppendLine();
            sbBody.AppendLine();
            sbBody.AppendLine("The link shown above expires in 3 hour from this e-mail's date.");
            sbBody.AppendLine();
            sbBody.AppendLine("If you need assistance, please contact us at helpdesk@repspark.com");
            sbBody.AppendLine();
            sbBody.AppendLine("Best wishes,");
            sbBody.AppendLine("RepSpark Customer Care");

            m.SendMail(pCust.email, sbBody.ToString(), "Password reset instructions");
        }
        private void SendClosureMail(customers pCust)
        {
            MailingService m = new MailingService();

            StringBuilder sbBody = new StringBuilder();

            sbBody.AppendFormat("Hi, {0}", pCust.name.ToUpper());
            sbBody.AppendLine();
            sbBody.AppendLine();
            sbBody.AppendLine("Welcome to RepSpark! Thanks so much for joining us. You’re on your way to super-productivity and beyond!");
            sbBody.AppendLine();
            sbBody.AppendLine("RepSpark is the industry’s only B2B selling system built for Total Order Life Cycle Management.");
            sbBody.AppendLine();
            sbBody.Append("Have any questions? Just login to Service Desk at https://repspark.tpondemand.com/helpdesk and create a ticket ");
            sbBody.AppendLine("and we will get back to you as soon as possible! We’re always here to help");
            sbBody.AppendLine();
            sbBody.AppendLine("Cheerfully yours,");
            sbBody.AppendLine("RepSpark Team");

            m.SendMail(pCust.email, sbBody.ToString(), "RepSpark Welcome Message");
        }
        #endregion
        #region Public Methods
        public bool Register(CustomerModel data)
        {
            int saved = 0;
            bool success = false;

            using (var db = new CleanGapDataContext())
            {
                bool RecordExists = db.customers.Where(c => c.email.ToLower() == data.email.ToLower()).Any();

                if (!RecordExists)
                {
                    customers dbCustomer = new customers()
                    {
                        name = data.name,
                        email = data.email,
                        password = crypto.EncodeMD5(data.password),
                        token_signin = GenerateToken(data.email),
                        token_expire = DateTime.Now.AddDays(1),
                        creation_date = DateTime.Now,
                    };

                    db.customers.Add(dbCustomer);
                    saved = db.SaveChanges();

                    // Send E-Mail once registered
                    if (saved > 0)
                    {
                        SendConfirmationMail(data);
                        success = true;
                    }
                }
            }

            return success;
        }
        public bool SendTokenEmail(CustomerModel data)
        {
            bool success = false;

            using (var db = new CleanGapDataContext())
            {
                customers cust = db.customers.FirstOrDefault(c => c.email.ToLower() == data.email.ToLower());

                if (cust != null && cust.id > 0)
                {
                    cust.token_forgot_pass = null;
                    customers editCust = UpdateToken(db, cust);

                    if (editCust.token_forgot_pass != null)
                    {
                        success = true;
                        SendTokenRecoveryMail(editCust);
                    }

                }
            }

            return success;
        }
        public CustomerModel FindByToken(string token)
        {
            CustomerModel register = null;

            using (var db = new CleanGapDataContext())
            {
                customers cust = db.customers.FirstOrDefault(c => c.token_forgot_pass == token && c.token_expire >= DateTime.Now);

                if (cust != null && cust.id > 0)
                {
                    register = new CustomerModel()
                    {
                        name = cust.name,
                        email = cust.email
                    };
                }
            }

            return register;
        }
        public bool ResetPassword(ResetPasswordModel data)
        {
            bool success = false;
            using (var db = new CleanGapDataContext())
            {
                customers cust = db.customers.FirstOrDefault(c => c.token_forgot_pass == data.Token && c.token_expire >= DateTime.Now);

                if (cust != null)
                {
                    success = UpdateNewPassword(data, db, cust);
                }
            }

            return success;

        }
        public async Task<LoginResponseModel> Authenticate(string userName, string password)
        {
            LoginResponseModel response = null;
            LoginModel data = new LoginModel()
            {
                email = userName,
                password = password,
            };

            using (var db = new CleanGapDataContext())
            {
                string passwordMD5 = EncodeMD5(data.password);
                customers cust = await db.customers.FirstOrDefaultAsync(c => c.email == data.email && c.password == passwordMD5);

                if (cust != null)
                {
                    //Set confirmed date if the user is not yet confirmed
                    SetConfirmedUser(db, cust);

                    response = new LoginResponseModel()
                    {
                        ID = cust.id,
                        email = cust.email,
                        name = cust.name
                    };
                }
            }

            return response;
        }
        public void SendWelcomeEMail(int CustomerId)
        {
            using (var db = new CleanGapDataContext())
            {
                customers cust = db.customers.Find(CustomerId);

                if (cust != null && cust.id > 0)
                {
                    SendClosureMail(cust);
                }
            }
        }
        #endregion

    }
}