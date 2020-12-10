using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace timeTracker.Models
{
	public interface IEventUnitOfWork
	{
		public IRepository<Event> Events { get; }
		//public IRepository<EventPointValue> EventPointValues { get; }
		//public IRepository<EventStatus> EventStatuses { get; }

		public void Save();
	}
}
