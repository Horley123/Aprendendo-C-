using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Logging
{
    public class CustomLoggerProviderConfig
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;

        public int EventId { get; set; } = 0;
    }
}