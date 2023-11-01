using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Newtonsoft.Json;
using Scraper.Integrations.Configurations;
using Scraper.Integrations.Models;

namespace Scraper.Integrations.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobContainerClient _containerClient;

        public BlobStorageService(IBlobStorageServiceConfiguration configuration)
        {
            _containerClient = new BlobContainerClient(configuration.ConnectionString, configuration.ContainerName);
            _containerClient.CreateIfNotExists();
        }

        public async Task UploadJsonFile(PublicApiResponse fileContent, string fileName)
        {
            var contentBytes = await PrepareFileContentForUpload(fileContent);
            var blobClient = _containerClient.GetBlobClient($"{fileName}.json");
            await blobClient.UploadAsync(new MemoryStream(contentBytes));
        }

        public async Task<string> DownloadJsonFileContent(string fileName)
        {
            var blobClient = _containerClient.GetBlobClient($"{fileName}.json");
            var content = await blobClient.DownloadContentAsync();

            return content.Value.Content.ToString();
        }

        private async Task<byte[]> PrepareFileContentForUpload(PublicApiResponse fileContent)
        {
            var blobContent = JsonConvert.SerializeObject(fileContent);
            var byteArray = Encoding.UTF8.GetBytes(blobContent);
            return byteArray;
        }
    }
}
