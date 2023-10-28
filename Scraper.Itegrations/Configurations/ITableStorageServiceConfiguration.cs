namespace Scraper.Integrations.Configurations
{
    public interface ITableStorageServiceConfiguration
    {
        string ConnectionString { get; }

        string tableName { get; set; }
    }
}
