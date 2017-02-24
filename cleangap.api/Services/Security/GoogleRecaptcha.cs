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

        IApiCommand api = null;

        private bool _success = false;
        private IList<string> _errorCodes = new List<string>();
        private DateTime _challengeTS = DateTime.Now;
        private string _hostname = string.Empty;
        
        private string _response = string.Empty;
        private string ActionService
        {
            get
            {
                string _secret = ConfigurationManager.AppSettings["GoogleRecaptchaSecret"].ToString();
                return string.Format("siteverify?secret={0}&response={1}", _secret, _response);
            }
        }


        public bool Success { get { return _success; } }
        public DateTime ChallengeTS { get { return _challengeTS; } }
        public string HostName { get { return _hostname; } }
        public IList<string> ErrorCodes { get { return _errorCodes; } }

        private void MapAttribResponse(JObject json)
        {
            var x = json.SelectToken("error-codes");
            //var _errorCodes = JsonConvert.DeserializeObject<List<string>>(x.ToString());

            _success = (bool)json.SelectToken("success");
            if (json["error-codes"] == null)
            {
                _challengeTS = (DateTime)json.SelectToken("challenge_ts");
                _hostname = json.SelectToken("hostname").ToString();
            }
            else
            {
                string errors = json["error-codes"].ToString();
                _errorCodes = JsonConvert.DeserializeObject<List<string>>(errors);
            }
        }

        public GoogleRecaptcha(string Response)
        {
            _response = Response;

            api = new ApiCommand(RequestUri);

        }

        public async Task<bool> Validate()
        {
            string response = await api.ExecuteGet(ActionService);

            if (api.IsSucess)
            {
                var json = JObject.Parse(response);
                MapAttribResponse(json);
            } else
            {
                _errorCodes.Add(response);
            }

            return api.IsSucess;
        }

    }
}