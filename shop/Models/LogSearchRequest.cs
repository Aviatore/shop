using System;
using System.Diagnostics.CodeAnalysis;

namespace shop.Models
{
    public class LogSearchRequest
    {
        [AllowNull]
        public int OrderId { get; set; }
        [AllowNull]
        public DateTime TimestampFrom { get; set; }
        [AllowNull]
        public DateTime TimestampTo { get; set; }
    }
}