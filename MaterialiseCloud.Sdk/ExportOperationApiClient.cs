using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaterialiseCloud.Sdk
{
    public class ExportOperationApiClient : OperationApiClient
    {
        public ExportOperationApiClient(string host, TokenProvider tokenProvider) : base(host, tokenProvider)
        {
        }

        public async Task<string> ExportAsync(string inputId, ExportFormats exportToFormat, string callbackUrl = null)
        {
            var requestData = new Dictionary<string, string>
            {
                {"inputId", inputId},
                {"exportToFormat", exportToFormat.ToString()},
                {"callbackUrl", callbackUrl}
            };

            var url = "web-api/operation/export";

            var result = await PostOperationAsync(url, requestData);
            return result;
        }

        public async Task<string> GetExportResultAsync(string operationId)
        {
            var url = $"web-api/operation/{operationId}/export/result";

            var result = await GetOperationResultAsync<ExportResult>(url);

            return result.FileId;
        }
    }

    public class ExportResult
    {
        public string FileId { get; set; }
    }
}