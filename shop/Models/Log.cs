using System;
using System.Collections.Generic;

#nullable disable

namespace shop.Models
{
    public partial class Log
    {
        public int LogId { get; set; }
        public int OrderId { get; set; }
        public string Msg { get; set; }

        public virtual Order Order { get; set; }
    }
}
