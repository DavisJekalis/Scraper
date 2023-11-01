namespace Scraper.Integrations.Configurations
{
    public interface IBlobStorageServiceConfiguration
    {
         public string ConnectionString { get; }

         public string ContainerName { get; }
    }
}
