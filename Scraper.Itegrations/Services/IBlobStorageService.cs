using System.Threading.Tasks;
using Scraper.Integrations.Models;

namespace Scraper.Integrations.Services;

public interface IBlobStorageService
{
    Task UploadJsonFile(PublicApiResponse fileContent, string fileName);
    Task<string> DownloadJsonFileContent(string fileName);
}