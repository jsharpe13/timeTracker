using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace timeTracker.Models
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> List(EventQueryOptions<T> options);
        IEnumerable<T> List();
        T Get(int idNumber);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
