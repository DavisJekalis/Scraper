using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Scraper.Integrations;
using Scraper.Integrations.Services;

namespace Scraper
{
    public class ListLogs
    {
        private readonly ILogger<ListLogs> _logger;
        private readonly ITableStorageService _tableStorageService;

        public ListLogs(ILogger<ListLogs> log, ITableStorageService tableStorageService)
        {
            _logger = log;
            _tableStorageService = tableStorageService;
        }

        [FunctionName("ListLogs")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiParameter(name: "from", In = ParameterLocation.Query, Required = true, Type = typeof(DateTime), Description = "The **from** parameter")]
        [OpenApiParameter(name: "to", In = ParameterLocation.Query, Required = true, Type = typeof(DateTime), Description = "The **toto** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var from = DateTime.Parse(req.Query["from"]);
            var to = DateTime.Parse(req.Query["to"]);

            var logs = _tableStorageService.FindLogEntries(from, to);

            return new OkObjectResult(logs);
        }
    }
}

