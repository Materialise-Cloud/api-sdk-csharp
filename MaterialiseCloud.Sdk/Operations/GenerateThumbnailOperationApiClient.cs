using System;
using System.Threading.Tasks;

namespace MaterialiseCloud.Sdk.Operations
{
    public class GenerateThumbnailOperationApiClient : OperationApiClient
    {
        public GenerateThumbnailOperationApiClient(string host, TokenProvider tokenProvider)
         : base(host, tokenProvider)
        { }

        public async Task<string> GenerateThumbnailAsync(string inputId, int widthPx, int heightPx, int cameraAngleX, int cameraAngleY, int cameraAngleZ, string callbackUrl = null)
        {
            var url = "web-api/operation/thumbnail";
            var request = new ThumbnailGenerationReguest
            {
                InputId = inputId,
                Width = widthPx,
                Height = heightPx,
                CameraAngleX = cameraAngleX,
                CameraAngleY = cameraAngleY,
                CameraAngleZ = cameraAngleZ,
                CallbackUrl = callbackUrl
            };

            var result = await PostOperationAsync(url, request);
            return result;
        }

        public async Task<string> GetGenerateThumbnailOperationResultAsync(string operationId)
        {
            var url = $"web-api/operation/{operationId}/thumbnail/result";
            var result = await GetOperationResultAsync<FileResult>(url);

            return result.FileId;
        }
    }

    public class ThumbnailGenerationReguest : OperationRequest
    {
        public int CameraAngleX { get; set; }
        public int CameraAngleY { get; set; }
        public int CameraAngleZ { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}