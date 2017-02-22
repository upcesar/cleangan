using System;
using System.Configuration;

namespace cleangap.api.Services.Mailing
{
    public class MailingSetup : IMailingSetup
    {
        public string Host
        {
            get
            {
                return ConfigurationManager.AppSettings["SMTP_Host"].ToString();
            }
        }

        public int Port
        {
            get
            {
                return Convert.ToInt16(ConfigurationManager.AppSettings["SMTP_Port"]);
            }
        }

        public bool DefaultCredencials
        {
            get
            {
                return false;
            }
        }

        public string DisplayName
        {
            get
            {
                return ConfigurationManager.AppSettings["SMTP_FromName"].ToString();

                //return ConfigurationManager

            }
        }

        public string UserName
        {
            get
            {
                return ConfigurationManager.AppSettings["SMTP_Email"].ToString();
            }
        }

        public string Password
        {
            get
            {
                return ConfigurationManager.AppSettings["SMTP_Password"].ToString();
            }
        }
    }
}