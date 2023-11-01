using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scraper.Integrations.Models;

namespace Scraper.Integrations.Services;

public interface ITableStorageService
{
    Task<string> CreateLogEntry(bool successful);
    IEnumerable<LogEntry> FindLogEntries(DateTime from, DateTime to);
}