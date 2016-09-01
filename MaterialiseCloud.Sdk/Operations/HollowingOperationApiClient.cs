using System;
using System.Threading.Tasks;

namespace MaterialiseCloud.Sdk.Operations
{
    public class HollowingOperationApiClient : OperationApiClient
    {
        public HollowingOperationApiClient(string host, TokenProvider tokenProvider)
            :base(host, tokenProvider)
        { }

        public async Task<string> HollowAsync(string inputId, double wallThicknessMm, string callbackUrl=null)
        {
            var url = "web-api/operation/hollowing";
            var request = new HolowingOperationRequest
            {
                InputId = inputId,
                WallThicknessMm = wallThicknessMm,
                CallbackUrl = callbackUrl
            };

            var result = await PostOperationAsync(url, request);
            return result;
        }

        public async Task<string> GetHollowingOperationResultAsync(string operationId)
        {
            var url = $"web-api/operation/{operationId}/hollowing/result";
            var result = await GetOperationResultAsync<GenericResult>(url);

            return result.ResultId;
        }
    }

    public class HolowingOperationRequest : OperationRequest
    {
        public double WallThicknessMm { get; set; }
    }
}