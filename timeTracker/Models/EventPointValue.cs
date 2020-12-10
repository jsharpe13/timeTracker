using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace timeTracker.Models
{
    public class EventPointValue
    {
        [Key]
        //public string pointValueId { get; set; }
        //public string Name { get; set; }
        public int Start { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}