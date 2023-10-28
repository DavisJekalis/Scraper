using System.Collections.Generic;

namespace Scraper.Integrations.Models
{
    public class PublicApiResponse
    {
        public int Count { get; set; }

        public List<Entry> Entries { get; set; }
    }
}
