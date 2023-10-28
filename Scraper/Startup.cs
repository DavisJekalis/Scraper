using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Scraper.Integrations;
using Scraper.Integrations.Configurations;
using Scraper.Integrations.Services;


[assembly: FunctionsStartup(typeof(Scraper.Startup))]
namespace Scraper
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
           builder.Services.AddRefitClient<IPublicApiClient>().ConfigureHttpClient(client =>
                client.BaseAddress = new Uri("https://api.publicapis.org"));

            builder.Services.AddSingleton<IBlobStorageServiceConfiguration, StorageConfiguration>();
            builder.Services.AddSingleton<ITableStorageServiceConfiguration, StorageConfiguration>();
            builder.Services.AddTransient<IBlobStorageService, BlobStorageService>();
            builder.Services.AddTransient<ITableStorageService, TableStorageService>();

        }
    }
}
