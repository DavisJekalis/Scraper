using System.Threading.Tasks;
using Refit;
using Scraper.Integrations.Models;

namespace Scraper.Integrations.Services
{
    public interface IPublicApiClient
    {
        [Get("/random")]
        Task<ApiResponse<PublicApiResponse>> GetRandomData(string? auth = null);
    }
}
