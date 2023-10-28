using System.Threading.Tasks;
using FluentAssertions;
using Refit;
using Scraper.Itegrations;

namespace Scraper.Integrations.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var service = RestService.For<IPublicApiClient>("https://api.publicapis.org");
            var result = await service.GetRandomData();
            result.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}