using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace shop.Models
{
    public class LogDataContainer
    {
        public IEnumerable<Log> Logs;
        [AllowNull]
        public string DateTimeFrom;
        [AllowNull]
        public string DateTimeTo;
    }
}