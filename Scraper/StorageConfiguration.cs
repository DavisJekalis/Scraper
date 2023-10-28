using System;
using Scraper.Integrations;
using Scraper.Integrations.Configurations;

namespace Scraper
{
    public class StorageConfiguration : IBlobStorageServiceConfiguration, ITableStorageServiceConfiguration
    {
        public StorageConfiguration()
        {
            ConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            tableName = Environment.GetEnvironmentVariable("tableName");
            ContainerName = Environment.GetEnvironmentVariable("containerName");
        }

        public string ConnectionString { get; }
        public string tableName { get; set; }
        public string ContainerName { get; }
    }
}
