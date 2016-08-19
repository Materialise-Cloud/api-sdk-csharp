using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MaterialiseCloud.Sdk
{
    public class OperationFileApiClient : ApiClientBase
    {
        private TokenProvider _tokenProvider;

        public OperationFileApiClient(string host, TokenProvider tokenProvider) : base(host)
        {
            _tokenProvider = tokenProvider;
        }

        public async Task<string> UploadFileAsync(string filePath)
        {
            using (var client = CreateHttpClient(await _tokenProvider.GetAccessTokenAsync()))
            {
                using (var content = new MultipartFormDataContent())
                {
                    var file = File.ReadAllBytes(filePath);
                    var fileName = Path.GetFileName(filePath);

                    var fileContent = new ByteArrayContent(file);
                    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = fileName
                    };

                    content.Add(fileContent);

                    var url = "web-api/operation/file";
                    var response = await client.PostAsync(url, content);

                    CheckResponseIsOk(response);

                    var result = await response.Content.ReadAsAsync<FileUploadResult>();

                    return result.FileId;
                }
            }
        }

        public async Task DownloadFileAsync(string fileId, string filePath)
        {
            using (var client = CreateHttpClient(await _tokenProvider.GetAccessTokenAsync()))
            {
                var url = $"web-api/operation/file/{fileId}";

                var response = await client.GetAsync(url);

                CheckResponseIsOk(response);

                var fileBytes = await response.Content.ReadAsByteArrayAsync();

                File.WriteAllBytes(filePath, fileBytes);
            }
        }
    }

    class FileUploadResult
    {
        public string FileId { get; set; }
    }
}