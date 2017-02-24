using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using cleangap.api.Services.HttpClient;
using System.Threading.Tasks;

namespace cleangap.api.Services.Security
{
    public class GoogleRecaptcha
    {
        private const string RequestUri = "https://www.google.com/recaptcha/api/";

        public static bool Validate(string Response)
        {
            string Secret = ConfigurationManager.AppSettings["GoogleRecaptchaSecret"].ToString();
            string ActionService = string.Format("siteverify?secret={0}&response={1}", Secret, Response);

            IApiCommand api = new ApiCommand(RequestUri);

            Task<string> response = api.ExecuteGet(ActionService);

            response.Wait();

            if (api.IsSucess)
            {
                var json = JObject.Parse(response.Result);
                return (bool)json.SelectToken("success");
            }

            return false;
        }

    }
}