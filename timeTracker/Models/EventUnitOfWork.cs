using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using timeTracker.Data;

namespace timeTracker.Models
{
    public class EventUnitOfWork : IEventUnitOfWork
    {
        private ApplicationDbContext context { get; set; }
        public EventUnitOfWork(ApplicationDbContext ctx) => context = ctx;

        private IRepository<Event> EventData;
        public IRepository<Event> Events
        {
            get
            {
                if (EventData == null)
                    EventData = new Repository<Event>(context);
                return EventData;
            }
        }

        //private IRepository<EventPointValue> EventPointValueData;
        //public IRepository<EventPointValue> EventPointValues
        //{
        //    get
        //    {
        //        if (EventPointValueData == null)
        //            EventPointValueData = new Repository<EventPointValue>(context);
        //        return EventPointValueData;
        //    }
        //}

        //private IRepository<EventStatus> EventStatusData;
        //private Mock<ApplicationDbContext> mockdatabase;

        //public IRepository<EventStatus> EventStatuses
        //{
        //    get
        //    {
        //        if (EventStatusData == null)
        //            EventStatusData = new Repository<EventStatus>(context);
        //        return EventStatusData;
        //    }
        //}

        public void Save() => context.SaveChanges();
    }
}