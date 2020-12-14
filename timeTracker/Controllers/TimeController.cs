using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using timeTracker.Data;
using timeTracker.Models;


namespace timeTracker.Controllers
{
    [Authorize]
    [EnableCors("mypolicy")]
    public class TimeController : Controller
    {
        // private ApplicationDbContext context { get; set; }
        private IEventUnitOfWork _uow;
        private readonly ILogger _logger;

        public TimeController(IEventUnitOfWork uow, ILogger<TimeController> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        [Route("/index")]
        public IActionResult timeIndex(int month=13, int year=0)
        {
            try {
                    ViewBag.Month = DateTime.Now.ToString("MMMM");
                ViewBag.MonthNumber = DateTime.Now.Month;
                ViewBag.Year = DateTime.Now.Year;


                var events = _uow.Events.List().OrderBy(x => x.start).ToList(); //.OrderBy(x => x.start).ToList();

                if (year != 9999)
                {
                    if (month != 13)
                    {
                        //query the list for the selected month
                        events = events.Where(t => t.start.Month == month).ToList();

                        DateTime selectedMonth = new DateTime(2010, month, 1);
                        string monthToUse = selectedMonth.ToString("MMMM");
                        ViewBag.Month = monthToUse;
                        ViewBag.MonthNumber = month;

                    }
                    else
                    {
                        //query the list for the current month
                        int currentMonth = DateTime.Now.Month;
                        events = events.Where(t => t.start.Month == currentMonth).ToList();
                    }

                    if (year != 0)
                    {
                        events = events.Where(t => t.start.Year == year).ToList();
                        ViewBag.Year = year;
                    }
                    else
                    {
                        int currentYear = DateTime.Now.Year;
                        events = events.Where(t => t.start.Year == currentYear).ToList();
                    }

                }
                else
                {
                    ViewBag.Month = "All";
                    ViewBag.Year = "Events";
                    ViewBag.MonthNumber = month;
                }

                return View(events);
            }
            catch (Exception e)
            {
                string Message = $"Error loading information occured at {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message);
                return View();
            }
        }

        [HttpPost]
        public IActionResult filter(int months, int years)
        {
            string Message = $"filters accessed on {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);
            return RedirectToAction("timeIndex", new { month = months, year = years });
        }

        [Route("/add")]
        [HttpGet]
        public IActionResult AddEvent()
        {
            string Message = $"Add event screen accessed on {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);
            return View("EditEvent", new Event());
        }

        [HttpGet]
        public IActionResult EditEvent(int id)
        {
            string Message = $"Edit event screen accessed on {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);
            // var ev = context.Events.Find(id);
            var ev = _uow.Events.Get(id);
            return View(ev);
        }

        [HttpPost]
        public IActionResult EditEvent(Event ev)
        {
            if (ModelState.IsValid)
            {
                int Selectedmonth = ev.start.Month;
                int Selectedyear = ev.start.Year;
                string Message;
                if (ev.EventId == 0)
                {
                    // context.Events.Add(ev);
                    _uow.Events.Insert(ev);
                    Message = $"Event added on {DateTime.UtcNow.ToLongTimeString()}";
                }
                else
                {
                    // context.Events.Update(ev);
                    _uow.Events.Update(ev);
                    Message = $"Event updated on {DateTime.UtcNow.ToLongTimeString()}";
                }
                // context.SaveChanges();
                _uow.Save();
                _logger.LogInformation(Message);
                return RedirectToAction("timeIndex", new { month = Selectedmonth, year = Selectedyear });
            }
            else
            {
                string Message = $"Error Editing information occured at {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message);
                return View(ev);
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                // var ev = context.Events.Find(id);
                var ev = _uow.Events.Get(id);
                //context.Events.Remove(ev);
                int Selectedmonth = ev.start.Month;
                int Selectedyear = ev.start.Year;
                _uow.Events.Delete(ev);
                // context.SaveChanges();
                _uow.Events.Save();
                string Message = $"Event deleted on {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message);
                return RedirectToAction("timeIndex", new { month = Selectedmonth, year = Selectedyear });
            }
            catch(Exception e)
            {
                string Message = $"Error deleting information occured at {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message);
                return RedirectToAction("timeIndex");
            }
        }
    }
}
