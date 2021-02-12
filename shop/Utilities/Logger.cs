using System;
using shop.Models;

namespace shop.Utilities
{
    public class Logger
    {
        private readonly shopContext _ctx;
        
        public Logger(shopContext ctx)
        {
            _ctx = ctx;
        }

        public void Add(int orderId, string msg)
        {
            _ctx.Logs.Add(new Log()
            {
                OrderId = orderId,
                Msg = msg,
                Timestamp = DateTime.Now
            });
        }
    }
}