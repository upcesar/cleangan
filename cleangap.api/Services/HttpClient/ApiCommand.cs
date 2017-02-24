using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace cleangap.api.Services.HttpClient
{


    public class ApiCommand : IApiCommand
    {

        private System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
        private HttpStatusCode _HttpCode = HttpStatusCode.NotFound;
        private bool _isSuccess = false;

        public HttpStatusCode HttpCode { get { return _HttpCode; } }
        public bool IsSucess { get { return _isSuccess; } }

        public ApiCommand(string pURI)
        {
            InitializeHttp(pURI);
        }

        public ApiCommand(string pURI, string ApiKey)
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
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            _isSuccess = false;

            try
            {
                response = await client.PostAsJsonAsync(action, param);
                response.EnsureSuccessStatusCode();
                // Handle success
                responseData = await response.Content.ReadAsStringAsync();

            }
            catch (HttpRequestException httpEx)
            {
                // Handle failure
                responseData = httpEx.Message;
            }
            finally
            {
                SetStatusHttpResponse(response);
                response.Dispose();
            }

            return responseData;
        }

        public async Task<string> ExecuteGet(string action)
        {
            string responseData = string.Empty;
            HttpResponseMessage response = null;
            _isSuccess = false;
            try
            {
                response = await client.GetAsync(action);
                response.EnsureSuccessStatusCode();
                // Handle success
                responseData = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException httpEx)
            {
                // Handle failure
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                responseData = httpEx.Message;
            }
            finally
            {
                SetStatusHttpResponse(response);
                response.Dispose();
            }

            return responseData;

        }
    }
}
