using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaterialiseCloud.Sdk.Operations
{
    public class ExportOperationApiClient : OperationApiClient
    {
        public ExportOperationApiClient(string host, TokenProvider tokenProvider) 
            : base(host, tokenProvider)
        { }

        public async Task<string> ExportAsync(string inputId, ExportFormats exportToFormat, string callbackUrl = null)
        {
            var url = "web-api/operation/export";

            var request = new ExportOperationRequest
            {
                InputId = inputId,
                ExportToFormat = exportToFormat.ToString(),
                CallbackUrl = callbackUrl
            };

            var result = await PostOperationAsync(url, request);
            return result;
        }

        public async Task<string> GetExportResultAsync(string operationId)
        {
            var url = $"web-api/operation/{operationId}/export/result";

            var result = await GetOperationResultAsync<FileResult>(url);

            return result.FileId;
        }
    }

    public class ExportOperationRequest : OperationRequest
    {
        public string ExportToFormat { get; set; }
    }

   
}