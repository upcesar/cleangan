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
        private void SendConfirmationMail(CustomerModel data)
        {
            MailingService m = new MailingService();

            StringBuilder sbBody = new StringBuilder();

            sbBody.AppendFormat("<h3>Hello, {0}</h3>", data.name.ToUpper());
            sbBody.AppendLine();
            sbBody.AppendLine("<p>Welcome to CleanGap. We are glad you are registered.</p>");
            sbBody.AppendLine();
            sbBody.AppendLine("<p>Welcome to CleanGap. We are glad you are registered.</p>");
            sbBody.AppendLine("<p>Regards.</p>");
            sbBody.AppendLine("<p>Clean Gap Team</p>");

            m.SendMail(data.email, sbBody.ToString(), "Clean Gap Welcome E-Mail");
        }

        private string EncodeMD5(string plainText)
        {
            Cryptography crypto = new Cryptography();
            string passwordmd5 = crypto.EncodeMD5(plainText);
            return passwordmd5;
        }

        private string GenerateToken(string email)
        {
            Cryptography crypto = new Cryptography();
            string hashEmail = string.Format("{0}|{1}", email, DateTime.Now.ToString());

            return crypto.EncodeMD5(hashEmail);

        }

        private customers UpdateToken(CleanGapDataContext db, customers c)
        {
            //c.token_expire = DateTime.Now.AddHours(3);
            c.hash_link = GenerateToken(c.email);

            db.Entry(c).State = EntityState.Modified;

            db.SaveChanges();

            return c;
        }
        private void SendTokenRecoveryMail(customers pCust)
        {

            var request = HttpContext.Current.Request;

            var baseUrl = string.Format("{0}://{1}/account/password-reset/{2}", request.Url.Scheme, request.Url.Authority, pCust.hash_link);

            MailingService m = new MailingService();

            StringBuilder sbBody = new StringBuilder();

            sbBody.AppendFormat("<h3>Hello, {0}</h3>", pCust.name.ToUpper());
            sbBody.AppendLine();
            sbBody.AppendFormat("<p>Here is the link for password reset: <a href=\"{0}\">{0}</a></p>", baseUrl);
            sbBody.AppendLine();
            sbBody.AppendLine("<p>The link shown above expires in 3 horas, from this e-mail date</p>");
            sbBody.AppendLine("<p>Regards.</p>");
            sbBody.AppendLine("<p>Clean Gap Team</p>");

            m.SendMail(pCust.email, sbBody.ToString(), "Password reset instructions");
        }
        private bool UpdateNewPassword(ResetPasswordModel data, CleanGapDataContext db, customers pCust)
        {
            db.Entry(pCust).State = EntityState.Modified;
            pCust.password = EncodeMD5(data.NewPassword);
            pCust.hash_link = null;
            pCust.token_expire = null;

            return db.SaveChanges() > 0;

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
                    cust.hash_link = null;
                    customers editCust = UpdateToken(db, cust);

                    if (editCust.hash_link != null)
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
                customers cust = db.customers.FirstOrDefault(c => c.hash_link == token && c.token_expire >= DateTime.Now);

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
                customers cust = db.customers.FirstOrDefault(c => c.hash_link == data.Token && c.token_expire >= DateTime.Now);

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
        #endregion

    }
}