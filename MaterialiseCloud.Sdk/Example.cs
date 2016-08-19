using System;
using System.Threading.Tasks;

namespace MaterialiseCloud.Sdk
{
    public class Example
    {
        private string _apiId = ""; //your api id
        private string _apiSecret = ""; //your api secret
        private string _userEmail = ""; //your user email
        private string _userPassword = ""; //your user password
        private string _host = ""; // for production environment

        private string _uploadFilePath = "meerkat_bad.3DS";
        private string _downloadFilePath = "meerkat_result.stl";

        private string _initialFileId;
        private string _resultId;
        private string _operationId;
        private string _exportedFileId;

        readonly TokenProvider _tokenProvider;

        public Example()
        {
            _tokenProvider = new TokenProvider(new AccessApiClient(_host, _apiId, _apiSecret), _userEmail, _userPassword);
        }

        public async Task Upload_Import_Analyze_Repair_Export_Download()
        {
            //Upload
            Console.WriteLine($"Uploading a file {_uploadFilePath}");

            var fileClient = new OperationFileApiClient(_host, _tokenProvider);

            _initialFileId = await fileClient.UploadFileAsync(_uploadFilePath);

            Console.WriteLine($"Upload done, fileId is {_initialFileId}");

            //Import
            Console.WriteLine($"Importing a file {_initialFileId}");

            var importClient = new ImportOperationApiClient(_host, _tokenProvider);

            _operationId = await importClient.ImportAsync(_initialFileId, MeasurementUnits.Mm);

            Console.WriteLine($"Import is in proccess, _operationId is {_operationId}");

            await importClient.WaitForOperationToFinish(_operationId);

            _resultId = await importClient.GetImportResultAsync(_operationId);

            Console.WriteLine($"Import done, resultId is {_resultId}");

            //Analyze
            Console.WriteLine($"Analyzing an input {_resultId}");

            var analyzeClient = new AnalyzeOperationApiClient(_host, _tokenProvider);

            _operationId = await analyzeClient.AnalyzeAsync(_resultId);

            Console.WriteLine($"Analyze is in proccess, _operationId is {_operationId}");

            await importClient.WaitForOperationToFinish(_operationId);

            var result = await analyzeClient.GetAnalyzeResultAsync(_operationId);

            var isRepaitNeeded = result.BadEdges > 0 || result.BadContours > 0 || result.VolumeMm3 <= 0;

            var answer = isRepaitNeeded ? "" : "not ";
            Console.WriteLine($"Analyze done, repair is {answer}needed");

            //Repair
            if (isRepaitNeeded)
            {
                Console.WriteLine($"Repairing an input {_resultId}");

                var repairClient = new RepairOperationApiClient(_host, _tokenProvider);

                _operationId = await repairClient.RepairAsync(_resultId);

                Console.WriteLine($"Repair is in proccess, _operationId is {_operationId}");

                await importClient.WaitForOperationToFinish(_operationId);

                _resultId = await repairClient.GetRepairResultAsync(_operationId);

                Console.WriteLine($"Repair done, resultId is {_resultId}");
            }

            //Export
            Console.WriteLine($"Exporting a result {_resultId}");

            var exportClient = new ExportOperationApiClient(_host, _tokenProvider);

            _operationId = await exportClient.ExportAsync(_resultId, ExportFormats.Stl);

            Console.WriteLine($"Export is in proccess, _operationId is {_operationId}");

            await importClient.WaitForOperationToFinish(_operationId);

            _exportedFileId = await exportClient.GetExportResultAsync(_operationId);

            Console.WriteLine($"Export done, fileId is {_exportedFileId}");

            //Download
            Console.WriteLine($"Downloading a file to {_downloadFilePath}");

            fileClient = new OperationFileApiClient(_host, _tokenProvider);

            await fileClient.DownloadFileAsync(_exportedFileId, _downloadFilePath);

            Console.WriteLine("Download done");
        }
    }
}