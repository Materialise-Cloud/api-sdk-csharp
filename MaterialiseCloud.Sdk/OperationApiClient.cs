using System.Collections.Generic;
using System.Net.Http;
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

        protected async Task<string> PostOperationAsync(string url, Dictionary<string, string> parameters)
        {
            using (var client = CreateHttpClient(await _tokenProvider.GetAccessTokenAsync()))
            {
                var response = await client.PostAsJsonAsync(url, parameters);

                CheckResponseIsOk(response);

                var result = await response.Content.ReadAsAsync<OperationResponse>();

                return result.OperationId;
            }
        }

        protected async Task<TResult> GetOperationResultAsync<TResult>(string url)
        {
            using (var client = CreateHttpClient(await _tokenProvider.GetAccessTokenAsync()))
            {
                var response = await client.GetAsync(url);

                CheckResponseIsOk(response);

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
            using (var client = CreateHttpClient(await _tokenProvider.GetAccessTokenAsync()))
            {
                var url = $"/web-api/operation/{operationId}/status";
                var response = await client.GetAsync(url);

                CheckResponseIsOk(response);

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