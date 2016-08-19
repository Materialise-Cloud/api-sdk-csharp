using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaterialiseCloud.Sdk
{
    public class RepairOperationApiClient : OperationApiClient
    {
        public RepairOperationApiClient(string host, TokenProvider tokenProvider) : base(host, tokenProvider)
        {
        }

        public async Task<string> RepairAsync(string inputId, string callbackUrl = null)
        {
            var requestData = new Dictionary<string, string>
            {
                {"inputId", inputId},
                {"callbackUrl", callbackUrl}
            };

            var url = "web-api/operation/repair";

            var result = await PostOperationAsync(url, requestData);
            return result;
        }

        public async Task<string> GetRepairResultAsync(string operationId)
        {
            var url = $"web-api/operation/{operationId}/repair/result";

            var result = await GetOperationResultAsync<RepairResult>(url);

            return result.ResultId;
       }
    }

    public class RepairResult
    {
        public string ResultId { get; set; }
    }
}