using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MaterialiseCloud.Sdk
{
    public class ApiClientBase
    {
        private string _host;

        public ApiClientBase(string host)
        {
            _host = host;
        }

        protected async void CheckResponseIsOk(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                await ThrowApiException(response);
            }
        }

        protected async Task ThrowApiException(HttpResponseMessage response)
        {
            var errorResponse = await response.Content.ReadAsAsync<ErrorResponse>();

            if (!errorResponse.Errors.Any())
            {
                errorResponse.Errors = new[] { new Error(-1, "An unknown error has occured.") };
            }

            throw new ApiClientException(response.StatusCode, errorResponse);
        }

        protected HttpClient CreateHttpClient(string token)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_host);
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }
    }
}