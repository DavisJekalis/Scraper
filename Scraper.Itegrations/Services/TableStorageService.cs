using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Scraper.Integrations.Configurations;
using Scraper.Integrations.Models;

namespace Scraper.Integrations.Services
{
    public class TableStorageService : ITableStorageService
    {
        private readonly TableClient _tableClient;

        public TableStorageService(ITableStorageServiceConfiguration configuration)
        {
            _tableClient = new TableClient(configuration.ConnectionString, configuration.tableName);
            _tableClient.CreateIfNotExists();
        }

        public async Task<string> CreateLogEntry(bool successful)
        {
            await _tableClient.AddEntityAsync(CreateLogEntry(successful, out var id));
           
            return id;
        }

        public IEnumerable<LogEntry> FindLogEntries(DateTime from, DateTime to)
        {
            var logEntries = _tableClient.Query<LogEntry>(e => 
                e.Timestamp >= from && e.Timestamp <= to);
            
            return logEntries.ToList();
        }

        private LogEntry CreateLogEntry(bool successful, out string id)
        {
            id = Guid.NewGuid().ToString().Replace("-", "");

            var entry = new LogEntry()
            {
                PartitionKey = id,
                RowKey = id,
                Succeeded = successful
            };

            return entry;
        }
    }
}
