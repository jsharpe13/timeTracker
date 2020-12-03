using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using timeTracker.Data;
using timeTracker.Models;

namespace timeTracker.Controllers
{
    public class TimeController : Controller
    {
        private ApplicationDbContext context { get; set; }
        
        public TimeController(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        [Route("/index")]
        public IActionResult timeIndex(int month = 13,  int currentYear = -99)
        {
            ViewBag.Month = DateTime.Now.ToString("MMMM");
            ViewBag.Year = DateTime.Now.Year;
            ViewBag.Today = DateTime.Now;
            List<Event> events = context.Events.OrderBy(x => x.start).ToList();
            return View(events);
        }

        [HttpPost]
        public IActionResult filter(int months, int years)
        {
            int selectedMonth = months;
            int selectedYears = years;
            ViewBag.Month = selectedMonth;
            ViewBag.Years = selectedYears;
            return View();
        }

        [Route("/add")]
        [HttpGet]
        public IActionResult AddEvent()
        {
            return View("EditEvent", new Event());
        }

        [HttpGet]
        public IActionResult EditEvent(int id)
        {
            var ev = context.Events.Find(id);
            return View(ev);
        }

        [HttpPost]
        public IActionResult EditEvent(Event ev)
        {
            if(ModelState.IsValid)
            {
                if(ev.EventId == 0)
                {
                    context.Events.Add(ev);
                }
                else
                {
                    context.Events.Update(ev);
                }
                context.SaveChanges();
                return RedirectToAction("timeIndex");
            }
            else
            {
                return View(ev);
            }
        }

        public IActionResult Delete(int id)
        {
            var ev = context.Events.Find(id);
            context.Events.Remove(ev);
            context.SaveChanges();
            return RedirectToAction("timeIndex");
        }
    }
}
