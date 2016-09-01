using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialiseCloud.Sdk.Operations
{
    public class ShrinkwrapRepairOperationApiClient : OperationApiClient
    {
        public ShrinkwrapRepairOperationApiClient(string host, TokenProvider tokenProvider)
            : base(host, tokenProvider)
        { }

        public async Task<string> RepairAsync(string inputId, ShrinkwrapRepairAccuracies accuracy, string callbackUrl = null)
        {
            var url = "web-api/operation/shrinkwrap-repair";
            var request = new ShrinkwrapRepairOperationRequest
            {
                InputId = inputId,
                Accuracy = accuracy.ToString(),
                CallbackUrl = callbackUrl
            };

            var result = await PostOperationAsync(url, request);
            return result;
        }

        public async Task<string> GetRepairResultAsync(string operationId)
        {
            var url = $"web-api/operation/{operationId}/shrinkwrap-repair/result";
            var result = await GetOperationResultAsync<GenericResult>(url);

            return result.ResultId;
        }
    }

    public class ShrinkwrapRepairOperationRequest : OperationRequest
    {
        public string Accuracy { get; set; }
    }
}
