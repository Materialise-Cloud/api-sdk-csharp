using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaterialiseCloud.Sdk.Operations
{
    public class GeometricalRepairOperationApiClient : OperationApiClient
    {
        public GeometricalRepairOperationApiClient(string host, TokenProvider tokenProvider) 
            : base(host, tokenProvider)
        { }

        public async Task<string> RepairAsync(string inputId, string callbackUrl = null)
        {
            var url = "web-api/operation/geometrical-repair";
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
            var url = $"web-api/operation/{operationId}/geometrical-repair/result";
            var result = await GetOperationResultAsync<GenericResult>(url);

            return result.ResultId;
       }
    }

    
}