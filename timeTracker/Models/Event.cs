using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace timeTracker.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Please enter a Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a start time")]
        public DateTime start { get; set; }

        [Required(ErrorMessage = "Please enter an end time")]
        public DateTime end { get; set; }

        //public bool isToday => start.Date.Equals(DateTime.Today.Date);
        public bool isToday => TimeZoneInfo.ConvertTime(start.Date, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")) == TimeZoneInfo.ConvertTime(DateTime.Now.Date, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));

        public bool isPast => TimeZoneInfo.ConvertTime(start.Date, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")) < TimeZoneInfo.ConvertTime(DateTime.Now.Date, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));

    }
}
