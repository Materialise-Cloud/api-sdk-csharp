using System;
using System.Threading.Tasks;

namespace MaterialiseCloud.Sdk.Operations
{
    public class ReduceTrianglesOperationApiClient : OperationApiClient
    {
        public ReduceTrianglesOperationApiClient(string host, TokenProvider tokenProvider)
            :base (host, tokenProvider)
        {  }

        public async Task<string> ReduceTrianglesAsync(string inputId, double accuracyMm, int maxAngle, int numberOfIterations, string callbackUrl = null)
        {
            var url = "web-api/operation/reduce-triangles";
            var request = new ReduceTrianglesOperationRequest
            {
                InputId = inputId,
                AccuracyMm = accuracyMm,
                MaxAngle = maxAngle,
                NumberOfIterations = numberOfIterations,
                CallbackUrl = callbackUrl
            };

            var result = await PostOperationAsync(url, request);
            return result;
        }

        public async Task<string> GetReduceTrianglesOperationResultAsync(string operationId)
        {
            var url = $"web-api/operation/{operationId}/reduce-triangles/result";
            var result = await GetOperationResultAsync<GenericResult>(url);

            return result.ResultId;
        }
    }

    public class ReduceTrianglesOperationRequest : OperationRequest
    {
        public double AccuracyMm { get; set; }
        public int MaxAngle { get; set; }
        public int NumberOfIterations { get; set; }
        
    }
}