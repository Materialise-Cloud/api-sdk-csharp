using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using MaterialiseCloud.Sdk.Operations;

namespace MaterialiseCloud.Sdk.Tests
{
    [TestClass]
    public class ExampleTests
    {
        private string _apiId = "";//your api id
        private string _apiSecret = ""; //your api secret
        private string _userEmail = ""; //your user email
        private string _userPassword = ""; //your user password
        private string _host = "api-cloudtoolkit-sandbox.materialise.net"; // for sandbox environment

        private string _uploadFilePath = "meerkat_bad.3DS";
        private string _downloadFilePath = "meerkat_result.stl";

        private string _initialFileId;
        private string _resultId;
        private string _operationId;
        private string _exportedFileId;

        private TokenProvider _tokenProvider;

        [TestInitialize]
        public void Setup()
        {
            _tokenProvider = new TokenProvider(new AccessApiClient(_host, _apiId, _apiSecret), _userEmail, _userPassword);
        }


        [TestMethod]
        public async Task UsageExample()
        {
            #region upload
            Console.WriteLine($"Uploading file {_uploadFilePath}");
            var fileClient = new OperationFileApiClient(_host, _tokenProvider);
            _initialFileId = await fileClient.UploadFileAsync(_uploadFilePath);
            Console.WriteLine($"Upload done, fileId is {_initialFileId}");
            #endregion

            #region Import
            Console.WriteLine($"Importing file");
            var importClient = new ImportOperationApiClient(_host, _tokenProvider);
            _operationId = await importClient.ImportAsync(_initialFileId, MeasurementUnits.Mm);
            
            await importClient.WaitForOperationToFinish(_operationId);

            _resultId = await importClient.GetImportResultAsync(_operationId);
            Console.WriteLine($"Import done, resultId is {_resultId}");

            #endregion
            bool isRepairNeeded=false;
            //#region Analyze
            //Console.WriteLine($"Analyzing an input {_resultId}");
            //var analyzeClient = new AnalyzeOperationApiClient(_host, _tokenProvider);
            //_operationId = await analyzeClient.AnalyzeAsync(_resultId);

            //await analyzeClient.WaitForOperationToFinish(_operationId);
            //var result = await analyzeClient.GetAnalyzeResultAsync(_operationId);

            //isRepairNeeded = result.BadEdges > 0 || result.BadContours > 0 || result.VolumeMm3 <= 0;

            //var answer = isRepairNeeded ? "" : "not ";
            //Console.WriteLine($"Analysis done, repair is {answer}needed");
            //#endregion

            if (isRepairNeeded)
            {
                //#region  Repair
                //Console.WriteLine($"Repairing an input {_resultId}");
                //var repairClient = new RepairOperationApiClient(_host, _tokenProvider);
                //_operationId = await repairClient.RepairAsync(_resultId);

                //await repairClient.WaitForOperationToFinish(_operationId);

                //_resultId = await repairClient.GetRepairResultAsync(_operationId);
                //Console.WriteLine($"Repair done, resultId is {_resultId}");

                //#endregion

                //#region  ShrinkWrap Repair
                //Console.WriteLine($"Repairing(ShrinkWrap) an input {_resultId}");
                //var shrinkWrapRepairClient = new ShrinkwrapRepairOperationApiClient(_host, _tokenProvider);
                //_operationId = await shrinkWrapRepairClient.RepairAsync(_resultId, ShrinkwrapRepairAccuracies.Rough);

                //await shrinkWrapRepairClient.WaitForOperationToFinish(_operationId);

                //_resultId = await shrinkWrapRepairClient.GetRepairResultAsync(_operationId);
                //Console.WriteLine($"Repair done, resultId is {_resultId}");

                //#endregion


                //#region  Geometrical Repair
                //Console.WriteLine($"Repairing(Geometrical) an input {_resultId}");
                //var geometricalRepairClient = new GeometricalRepairOperationApiClient(_host, _tokenProvider);
                //_operationId = await geometricalRepairClient.RepairAsync(_resultId);

                //await geometricalRepairClient.WaitForOperationToFinish(_operationId);

                //_resultId = await geometricalRepairClient.GetRepairResultAsync(_operationId);
                //Console.WriteLine($"Repair done, resultId is {_resultId}");

                //#endregion


            }

            //#region ConvertToStl
            //Console.WriteLine($"Converting to Stl result {_resultId}");
            //var converToStlClient = new ConvertToStlOperationApiClient(_host, _tokenProvider);
            //_operationId = await converToStlClient.ConvertAsync(_resultId);

            //await converToStlClient.WaitForOperationToFinish(_operationId);

            //var convertedFileId = await converToStlClient.GetConversionResultAsync(_operationId);
            //Console.WriteLine($"Convertion done, fileId is {convertedFileId}");

            //#endregion


            //#region Scale
            //Console.WriteLine($"Scale input {_resultId}");
            //var scaleClient = new ScaleOperationApiClient(_host, _tokenProvider);
            //_operationId = await scaleClient.ScaleAsync(_resultId, Axes.X, 5M);

            //await scaleClient.WaitForOperationToFinish(_operationId);

            //_resultId = await scaleClient.GetScalingResultAsync(_operationId);
            //Console.WriteLine($"Scale done, resultId is {_resultId}");

            //#endregion


            //#region Thumbnail generation
            //Console.WriteLine($"Thumbnail generation for input {_resultId}");
            //var thumbnailClient = new GenerateThumbnailOperationApiClient(_host, _tokenProvider);
            //_operationId = await thumbnailClient.GenerateThumbnailAsync(_resultId, 300, 300, 145, 145, 145);

            //await thumbnailClient.WaitForOperationToFinish(_operationId);

            //var thumbnailFileId = await thumbnailClient.GetGenerateThumbnailOperationResultAsync(_operationId);

            //fileClient = new OperationFileApiClient(_host, _tokenProvider);
            //await fileClient.DownloadFileAsync(thumbnailFileId, "model_preview.jpg");

            //Console.WriteLine($"Thumbnail generation done, fileId is {thumbnailFileId}");

            //#endregion


            //#region ReduceTriangles
            //Console.WriteLine($"Reduce triangles for input {_resultId}");
            //var reducer = new ReduceTrianglesOperationApiClient(_host, _tokenProvider);
            //_operationId = await reducer.ReduceTrianglesAsync(_resultId, 2, 15, 1);

            //await reducer.WaitForOperationToFinish(_operationId);

            //_resultId = await reducer.GetReduceTrianglesOperationResultAsync(_operationId);
            //Console.WriteLine($"Triangles reduction done, resultId is {_resultId}");

            //#endregion

            //#region Hollowing
            //Console.WriteLine($"Hollowing input {_resultId}");
            //var hollower = new HollowingOperationApiClient(_host, _tokenProvider);
            //_operationId = await hollower.HollowAsync(_resultId, 1);

            //await hollower.WaitForOperationToFinish(_operationId);

            //_resultId = await hollower.GetHollowingOperationResultAsync(_operationId);
            //Console.WriteLine($"Hollowing done, resultId is {_resultId}");

            //#endregion

            //#region Wallthickness Analysis
            //Console.WriteLine($"Running Wallthickness Analysis for input {_resultId}");
            //var analyzer2 = new WallThicknessAnalysisOperationApiClient(_host, _tokenProvider);
            //_operationId = await analyzer2.AnalyzeAsync(_resultId, minimalWallThicknessMm:1, accuracyWallThickness:2);

            //await analyzer2.WaitForOperationToFinish(_operationId);

            //var analysisResult = await analyzer2.GetHollowingOperationResultAsync(_operationId);
            //Console.WriteLine($"Analysis done, fileId {analysisResult.FileId}");
            //Console.WriteLine($"Has thin walls {analysisResult.HasThinWalls}");

            //var analysisFilePath = "wallThicknessAnalysisFile.stl";
            
            //#region dwonload
            //Console.WriteLine($"Downloading analysis file to {analysisFilePath}");
            //fileClient = new OperationFileApiClient(_host, _tokenProvider);
            //await fileClient.DownloadFileAsync(analysisResult.FileId, analysisFilePath);
            //Console.WriteLine("Download done");
            //#endregion

            //#endregion


            #region Export
            Console.WriteLine($"Exporting a result {_resultId}");
            var exportClient = new ExportOperationApiClient(_host, _tokenProvider);
            _operationId = await exportClient.ExportAsync(_resultId, ExportFormats.Stl);

            await exportClient.WaitForOperationToFinish(_operationId);

            _exportedFileId = await exportClient.GetExportResultAsync(_operationId);
            Console.WriteLine($"Export done, fileId is {_exportedFileId}");

            #endregion

            #region Download
            Console.WriteLine($"Downloading file to {_downloadFilePath}");
            fileClient = new OperationFileApiClient(_host, _tokenProvider);
            await fileClient.DownloadFileAsync(_exportedFileId, _downloadFilePath);
            Console.WriteLine("Download done");

            #endregion
        }
    }
}
