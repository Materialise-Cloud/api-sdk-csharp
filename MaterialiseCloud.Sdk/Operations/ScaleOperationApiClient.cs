using System;
using System.Threading.Tasks;

namespace MaterialiseCloud.Sdk.Operations
{
    public class ScaleOperationApiClient: OperationApiClient
    {
        public ScaleOperationApiClient(string host, TokenProvider tokenProvider) 
            : base(host, tokenProvider)
        { }

        public async Task<string> ScaleAsync(string inputId, Axes axis, decimal scaleToMm, string callbackUrl = null)
        {
            var url = "web-api/operation/scale";
            var request = new ScaleOperationRequest
            {
                InputId = inputId,
                Axis = axis.ToString(),
                ScaleToSizeMm = scaleToMm,
                CallbackUrl = callbackUrl
            };

            var result = await PostOperationAsync(url, request);
            return result;
        }

        public async Task<string> GetScalingResultAsync(string operationId)
        {
            var url = $"web-api/operation/{operationId}/scale/result";
            var result = await GetOperationResultAsync<GenericResult>(url);

            return result.ResultId;
        }
    }

    public class ScaleOperationRequest : OperationRequest
    {
        public string Axis { get; set; }
        public decimal ScaleToSizeMm { get; set; }
    }
}