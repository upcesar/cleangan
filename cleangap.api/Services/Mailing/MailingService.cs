using System;
using System.Net.Mail;

namespace cleangap.api.Services.Mailing
{
    public class MailingService
    {
        private SmtpClient _client;
        private MailingSetup _mailSetup = new MailingSetup();

        public MailingService()
        {
            SetInitialparameters();
            SetupForGmailApp();
        }

        public MailingService(MailingSetup customSetup)
        {
            _mailSetup = customSetup;
            SetInitialparameters();
            SetupForGmailApp();
        }

        private void SetInitialparameters()
        {
            _client = new SmtpClient()
            {
                Port = _mailSetup.Port,
                Host = _mailSetup.Host,
                Timeout = 20000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };
        }

        private void SetupForGmailApp()
        {
            //NECESSARIO PARA UTILIZAÇÂO NO GMAIL APPS
            if (_mailSetup.Host.Contains("gmail"))
            {
                _client.Credentials = new System.Net.NetworkCredential(_mailSetup.UserName, _mailSetup.Password);
                _client.EnableSsl = true;
            }
        }

        public void SendMail(string emailto, string body, string subject)
        {
            MailMessage mail = new MailMessage();
                        
            mail.From = new MailAddress(_mailSetup.UserName, _mailSetup.DisplayName);
            mail.To.Add(emailto);
            mail.Subject = subject; //
            mail.Body = body; //"Utilize este token em sua operação: " + token
            mail.IsBodyHtml = true;

            try
            {
                _client.Send(mail);
            }
            catch (Exception po)
            {
                throw new Exception(po.Message);
            }
        }        
    }
}
