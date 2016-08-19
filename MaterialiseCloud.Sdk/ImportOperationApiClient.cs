using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaterialiseCloud.Sdk
{
    public class ImportOperationApiClient : OperationApiClient
    {
        public ImportOperationApiClient(string host, TokenProvider tokenProvider) : base(host, tokenProvider)
        {
        }

        public async Task<string> ImportAsync(string fileId, MeasurementUnits measurementUnits, string callbackUrl = null)
        {
            var requestData = new Dictionary<string, string>
            {
                {"fileId", fileId},
                {"measurementUnits", measurementUnits.ToString()},
                {"callbackUrl", callbackUrl}
            };

            var url = "web-api/operation/import";

            var result = await PostOperationAsync(url, requestData);
            return result;
        }

        public async Task<string> GetImportResultAsync(string operationId)
        {
            var url = $"web-api/operation/{operationId}/import/result";

            var result = await GetOperationResultAsync<ImportResult>(url);

            return result.ResultId;
        }
    }

    public class ImportResult
    {
        public string ResultId { get; set; }
    }
}