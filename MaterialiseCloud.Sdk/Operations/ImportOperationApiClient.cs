using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaterialiseCloud.Sdk.Operations
{
    public class ImportOperationApiClient : OperationApiClient
    {
        public ImportOperationApiClient(string host, TokenProvider tokenProvider) 
            : base(host, tokenProvider)
        { }

        public async Task<string> ImportAsync(string fileId, MeasurementUnits measurementUnits, string callbackUrl = null)
        {
            var url = "web-api/operation/import";
            var requestData = new ImportRequest
            {
                FileId = fileId,
                MeasurementUnits = measurementUnits.ToString(),
                CallbackUrl = callbackUrl
            };

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

    public class ImportRequest : OperationRequestBase
    {
        public string FileId { get; set; }
        public string MeasurementUnits { get; set; }
    }


    public class ImportResult
    {
        public string ResultId { get; set; }
    }
}