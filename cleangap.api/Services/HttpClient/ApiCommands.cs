using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace cleangap.api.Services.HttpClient
{


    public class ApiCommands : IApiCommand
    {

        private System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
        private HttpStatusCode _HttpCode = HttpStatusCode.OK;
        private bool _isSuccess = false;

        public HttpStatusCode HttpCode { get { return _HttpCode; } }
        public bool IsSucess { get { return _isSuccess; } }
        
        public ApiCommands(string pURI)
        {
            InitializeHttp(pURI);
        }

        public ApiCommands(string pURI, string ApiKey)
        {
            InitializeHttp(pURI);
            // Api Key in Base64 format. IE: "dXBjZXNhcnx0ZXN0MTIzNA=="
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", ApiKey);

        }

        private void InitializeHttp(string URI)
        {
            // New code:
            client.BaseAddress = new Uri(URI);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //

        }
        private void SetStatusHttpResponse(HttpResponseMessage response)
        {
            _isSuccess = response.IsSuccessStatusCode;
            _HttpCode = response.StatusCode;
        }

        public async Task<string> ExecutePost(string action, object param)
        {
            string responseData = string.Empty;

            using (var response = await client.PostAsJsonAsync(action, param))
            {
                _isSuccess = false;
                try
                {
                    response.EnsureSuccessStatusCode();
                    // Handle success
                    responseData = await response.Content.ReadAsStringAsync();

                }
                catch (HttpRequestException httpEx)
                {
                    // Handle failure
                    responseData = httpEx.Message;
                }
                SetStatusHttpResponse(response);

            }

            return responseData;
        }

        public async Task<string> ExecuteGet(string action)
        {
            string responseData = string.Empty;

            using (var response = await client.GetAsync(action))
            {
                _isSuccess = false;
                try
                {
                    response.EnsureSuccessStatusCode();
                    // Handle success
                    responseData = await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException httpEx)
                {
                    // Handle failure
                    responseData = httpEx.Message;
                }

                SetStatusHttpResponse(response);

            }

            return responseData;

            throw new NotImplementedException();
        }
    }
}
