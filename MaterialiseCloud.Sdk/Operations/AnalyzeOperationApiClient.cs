using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaterialiseCloud.Sdk.Operations
{
    public class AnalyzeOperationApiClient : OperationApiClient
    {
        public AnalyzeOperationApiClient(string host, TokenProvider tokenProvider) 
            : base(host, tokenProvider)
        { }

        public async Task<string> AnalyzeAsync(string inputId, string callbackUrl = null)
        {
            var url = "web-api/operation/analyze";

            var request = new OperationRequest
            {
                InputId = inputId,
                CallbackUrl = callbackUrl
            };

            var result = await PostOperationAsync(url, request);
            return result;
        }

        public async Task<AnalyzeResult> GetAnalyzeResultAsync(string operationId)
        {
            var url = $"web-api/operation/{operationId}/analyze/result";
            var result = await GetOperationResultAsync<AnalyzeResult>(url);

            return result;
        }
    }

    public class AnalyzeResult
    {
        public long Vertices { get; set; }

        public long Triangles { get; set; }

        public long Contours { get; set; }

        public long Shells { get; set; }

        public double VolumeMm3 { get; set; }

        public double SurfaceAreaMm2 { get; set; }

        public double DimensionXMm { get; set; }

        public double DimensionYMm { get; set; }

        public double DimensionZMm { get; set; }

        public long BadEdges { get; set; }

        public long BadContours { get; set; }

        public SolidityParams SolidityParams { get; set; }

        public QualityParams QualityParams { get; set; }
    }

    public class SolidityParams
    {
        public long InvertedNormals { get; set; }

        public long BadEdges { get; set; }

        public long BadContours { get; set; }

        public long NearBadEdges { get; set; }

        public long PlanarHoles { get; set; }
    }

    public class QualityParams
    {
        public long NoiseShells { get; set; }

       public long OverlappingTriangles { get; set; }

        public long IntersectingTriangles { get; set; }

        public bool IsFacetingScoreOk { get; set; }
    }
}