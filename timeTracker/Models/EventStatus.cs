using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace timeTracker.Models
{
    public class EventStatus
    {
        [Key]
        public string StatusId { get; set; }
        public string Name { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}