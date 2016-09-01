using System.Threading.Tasks;

namespace MaterialiseCloud.Sdk.Operations
{
    public class ConvertToStlOperationApiClient : OperationApiClient
    {
        private string _host;
        private TokenProvider _tokenProvider;

        public ConvertToStlOperationApiClient(string host, TokenProvider tokenProvider)
            : base(host, tokenProvider)
        { }

        public async Task<string> ConvertAsync(string inputId, string callbackUrl = null)
        {
            var url = "web-api/operation/stl-conversion";

            var request = new OperationRequest
            {
                InputId = inputId,
                CallbackUrl = callbackUrl
            };

            var result = await PostOperationAsync(url, request);
            return result;
        }

        public async Task<string> GetConversionResultAsync(string operationId)
        {
            var url = $"web-api/operation/{operationId}/stl-conversion/result";

            var result = await GetOperationResultAsync<FileResult>(url);

            return result.FileId;
        }
    }
}