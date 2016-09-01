using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaterialiseCloud.Sdk.Operations
{
    public class RepairOperationApiClient : OperationApiClient
    {
        public RepairOperationApiClient(string host, TokenProvider tokenProvider) 
            : base(host, tokenProvider)
        { }

        public async Task<string> RepairAsync(string inputId, string callbackUrl = null)
        {
            var url = "web-api/operation/repair";
            var request = new OperationRequest
            {
                InputId = inputId,
                CallbackUrl = callbackUrl
            };

            var result = await PostOperationAsync(url, request);
            return result;
        }

        public async Task<string> GetRepairResultAsync(string operationId)
        {
            var url = $"web-api/operation/{operationId}/repair/result";
            var result = await GetOperationResultAsync<GenericResult>(url);

            return result.ResultId;
       }
    }

    
}