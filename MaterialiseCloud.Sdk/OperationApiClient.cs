using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace MaterialiseCloud.Sdk
{
    public class OperationApiClient : ApiClientBase
    {
        private TokenProvider _tokenProvider;

        public OperationApiClient(string host, TokenProvider tokenProvider) : base(host)
        {
            _tokenProvider = tokenProvider;
        }

        protected async Task<string> PostOperationAsync<TRequest>(string url, TRequest request)
        {
            using (var client = CreateHttpClient(await _tokenProvider.GetAccessTokenAsync()))
            {
                var s = JObject.FromObject(request).ToString();

                var formatter = new JsonMediaTypeFormatter();
                formatter.SerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                };
                client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
                var response = await client.PostAsync(url, request, formatter);
                ThrowIfNotSuccessful(response);

                var result = await response.Content.ReadAsAsync<OperationResponse>();
                return result.OperationId;
            }
        }

        protected async Task<TResult> GetOperationResultAsync<TResult>(string url)
        {
            using (var client = CreateHttpClient(await _tokenProvider.GetAccessTokenAsync()))
            {
                var response = await client.GetAsync(url);
                ThrowIfNotSuccessful(response);

                var result = await response.Content.ReadAsAsync<TResult>();
                return result;
            }
        }

        public async Task<bool> WaitForOperationToFinish(string operationId, int pollingIntervalMilliseconds = 3000)
        {
            var isCompleted = false;
            var result = new OperationStatusResponse();

            while (!isCompleted)
            {
                await Task.Delay(pollingIntervalMilliseconds);

                result = await GetOperationStatusAsync(operationId);
                isCompleted = result.IsCompleted;
            }

            return result.IsSuccessful;
        }

        private async Task<OperationStatusResponse> GetOperationStatusAsync(string operationId)
        {
            var url = $"/web-api/operation/{operationId}/status";
            using (var client = CreateHttpClient(await _tokenProvider.GetAccessTokenAsync()))
            {
                var response = await client.GetAsync(url);
                ThrowIfNotSuccessful(response);

                var result = await response.Content.ReadAsAsync<OperationStatusResponse>();
                return result;
            }
        }
    }

    class OperationResponse
    {
        public string OperationId { get; set; }
    }

    class OperationStatusResponse
    {
        public bool IsCompleted { get; set; }

        public bool IsSuccessful { get; set; }
    }
}