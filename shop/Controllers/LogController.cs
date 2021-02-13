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
            //IEnumerable<Log> logs = _ctx.Logs.Skip(_logsPerPage * page).Take(_logsPerPage);
            //IEnumerable<Log> logs = JsonConvert.DeserializeObject<IEnumerable<Log>>()
            
            /*IEnumerable<Log> logs = null;
            
            if (TempData["logs"] != null)
            {
                logs = JsonConvert.DeserializeObject<IEnumerable<Log>>(TempData["logs"].ToString());
            }

            logs ??= new List<Log>();
            
            return View(logs);*/
            
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

            /*LogDataContainer ldc = new LogDataContainer()
            {
                Logs = logs,
                DateTimeFrom = lsr.TimestampFrom.ToString() != "1/1/0001 12:00:00 AM" ? lsr.TimestampFrom : null,
                DateTimeTo = lsr.TimestampTo.ToString() != "1/1/0001 12:00:00 AM" ? lsr.TimestampTo : null
            };*/
            
            ldc.Logs = logs;

            return View(ldc);
        }

        public IActionResult Search(LogSearchRequest lsr)
        {
            /*IQueryable<Log> logQ = _ctx.Logs;
            Console.WriteLine($"DEBUG3: {lsr.TimestampFrom.ToString()}");
            
            
            if (lsr.OrderId > 0)
            {
                logQ = logQ.Where(l => l.OrderId == lsr.OrderId);
            }

            if (lsr.TimestampFrom.ToString() != "1/1/0001 12:00:00 AM" &&
                lsr.TimestampTo.ToString() != "1/1/0001 12:00:00 AM")
            {
                logQ = logQ.Where(l => l.Timestamp >= lsr.TimestampFrom && l.Timestamp <= lsr.TimestampTo);
            }*/
            
            /*else
            {
                logQ = _ctx.Logs.Where(l => l.Timestamp >= lsr.TimestampFrom && l.Timestamp <= lsr.TimestampTo);
            }*/

            //IEnumerable<Log> logs = logQ.ToList();
            
            //TempData["logs"] = JsonConvert.SerializeObject(logs);
            TempData["logs"] = JsonConvert.SerializeObject(lsr);
            
            return RedirectToAction("Index", "Log");
        }
    }
}