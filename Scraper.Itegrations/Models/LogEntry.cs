﻿using System;
using Azure;
using Azure.Data.Tables;

namespace Scraper.Integrations.Models
{
    public class LogEntry : ITableEntity
    {
        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }

        public bool Succeeded { get; set; }
    }
}
