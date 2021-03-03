using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using shop.Models;

namespace shop.Controllers
{
    public class LogController : Controller
    {
        private readonly shopContext _ctx;
        private int _logsPerPage = 3;

        public LogController(shopContext ctx)
        {
            _ctx = ctx;
        }
        
        public IActionResult Index()
        {
            LogSearchRequest lsr = null;
            IEnumerable<Log> logs;
            LogDataContainer ldc = new LogDataContainer();
            
            if (TempData["logs"] != null)
            {
                lsr = JsonConvert.DeserializeObject<LogSearchRequest>(TempData["logs"].ToString());
                
                IQueryable<Log> logQ = _ctx.Logs;
            
                if (lsr.OrderId > 0)
                {
                    logQ = logQ.Where(l => l.OrderId == lsr.OrderId);
                }

                if (lsr.TimestampFrom.ToString() != "1/1/0001 12:00:00 AM" &&
                    lsr.TimestampTo.ToString() != "1/1/0001 12:00:00 AM")
                {
                    logQ = logQ.Where(l => l.Timestamp.Date >= lsr.TimestampFrom.Date && l.Timestamp.Date <= lsr.TimestampTo.Date);
                    ldc.DateTimeFrom = lsr.TimestampFrom.ToString("yyyy-0M-0d");
                    ldc.DateTimeTo = lsr.TimestampTo.ToString("yyyy-0M-0d");
                }
                
                logs = logQ.ToList();
            }
            else
            {
                logs = new List<Log>();
            }
            
            ldc.Logs = logs;

            return View(ldc);
        }

        public IActionResult Search(LogSearchRequest lsr)
        {
            TempData["logs"] = JsonConvert.SerializeObject(lsr);
            
            return RedirectToAction("Index", "Log");
        }
    }
}