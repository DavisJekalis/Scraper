using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Scraper.Integrations.Services;

namespace Scraper
{
    public class BlobFunction
    {
        private readonly ILogger<BlobFunction> _logger;
        private readonly IBlobStorageService _blobStorageService;

        public BlobFunction(ILogger<BlobFunction> log, IBlobStorageService blobStorageService)
        {
            _logger = log;
            _blobStorageService = blobStorageService;
        }

        [FunctionName("BlobFunction")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **id** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string blobId = req.Query["id"];
            var content = await _blobStorageService.DownloadJsonFileContent(blobId);

            return new OkObjectResult(content);
        }
    }
}

