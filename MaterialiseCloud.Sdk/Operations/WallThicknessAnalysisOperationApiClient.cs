using System;
using System.Threading.Tasks;

namespace MaterialiseCloud.Sdk.Operations
{
    public class WallThicknessAnalysisOperationApiClient: OperationApiClient
    {
        public WallThicknessAnalysisOperationApiClient(string host, TokenProvider tokenProvider)
            : base(host, tokenProvider)
        { }

        public async Task<string> AnalyzeAsync(string inputId, double minimalWallThicknessMm, int accuracyWallThickness, string callbackUrl = null)
        {
            var url = "web-api/operation/wall-thickness-analysis";
            var request = new WallThicknessAnalysisOperationRequest
            {
                InputId = inputId,
                MinimalWallThicknessMm = minimalWallThicknessMm,
                AccuracyWallThickness = accuracyWallThickness,
                CallbackUrl = callbackUrl
            };
            var result = await PostOperationAsync(url, request);
            return result;
        }

        public async Task<WallThicknessAnalysisResult> GetHollowingOperationResultAsync(string operationId)
        {
            var url = $"web-api/operation/{operationId}/wall-thickness-analysis/result";
            var result = await GetOperationResultAsync<WallThicknessAnalysisResult>(url);
            return result;
        }
    }

    public class WallThicknessAnalysisOperationRequest : OperationRequest
    {
        public int AccuracyWallThickness { get; internal set; }
        public double MinimalWallThicknessMm { get; internal set; }
    }
}