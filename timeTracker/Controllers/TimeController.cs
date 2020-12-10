using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using timeTracker.Data;
using timeTracker.Models;

namespace timeTracker.Controllers
{
    [Authorize]
    public class TimeController : Controller
    {
        // private ApplicationDbContext context { get; set; }
        private IEventUnitOfWork _uow;

        public TimeController(IEventUnitOfWork uow)
        {
            _uow = uow;
        }

        [Route("/index")]
        public IActionResult timeIndex()
        {
            ViewBag.Month = DateTime.Now.ToString("MMMM");
            ViewBag.Year = DateTime.Now.Year;
            ViewBag.Today = DateTime.Now;
            List<Event> events = _uow.Events.List().ToList(); //.OrderBy(x => x.start).ToList();

            return View(events);
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
            // var ev = context.Events.Find(id);
            var ev = _uow.Events.Get(id);
            return View(ev);
        }

        [HttpPost]
        public IActionResult EditEvent(Event ev)
        {
            if (ModelState.IsValid)
            {
                if (ev.EventId == 0)
                {
                    // context.Events.Add(ev);
                    _uow.Events.Insert(ev);
                }
                else
                {
                    // context.Events.Update(ev);
                    _uow.Events.Update(ev);
                }
                // context.SaveChanges();
                _uow.Save();
                return RedirectToAction("timeIndex");
            }
            else
            {
                return View(ev);
            }
        }

        public IActionResult Delete(int id)
        {
            // var ev = context.Events.Find(id);
            var ev = _uow.Events.Get(id);
            //context.Events.Remove(ev);
            _uow.Events.Delete(ev);
            // context.SaveChanges();
            _uow.Events.Save();
            return RedirectToAction("timeIndex");
        }
    }
}
