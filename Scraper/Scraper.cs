using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Scraper.Integrations;
using Scraper.Integrations.Services;

namespace Scraper
{
    public class Scraper
    {
        private readonly IPublicApiClient _service;
        private readonly ITableStorageService _tableStorageService;
        private readonly IBlobStorageService _blobStorageService;

        public Scraper(IPublicApiClient service, ITableStorageService tableStorageService, IBlobStorageService blobStorageService)
        {
            _service = service;
            _tableStorageService = tableStorageService;
            _blobStorageService = blobStorageService;
        }

        [FunctionName("Scraper")]
        public async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            var data = await _service.GetRandomData(null);
            var fileName = await _tableStorageService.CreateLogEntry(data.IsSuccessStatusCode);
            await _blobStorageService.UploadJsonFile(data.Content, fileName);
        }
    }
}
