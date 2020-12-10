using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using timeTracker.Data;

namespace timeTracker.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext context { get; set; }
        private DbSet<T> dbset { get; set; }

        public Repository(ApplicationDbContext ctx)
        {
            context = ctx;
            dbset = ctx.Set<T>();
            // dbset = context.Set<T>();
        }

        public virtual IEnumerable<T> List(EventQueryOptions<T> options)
        {
            IQueryable<T> query = dbset;
            foreach (string include in options.getIncludes())
            {
                query = query.Include(include);
            }
            if (options.HasWhere)
                query = query.Where(options.Where);
            if (options.HasOrderBy)
            {
                query = query.OrderBy(options.OrderBy);
            }
            return query.ToList();
        }

        public virtual IEnumerable<T> List()
        {
            IQueryable<T> query = dbset;
            return query.ToList();
        }

        public virtual T Get(int idNumber)
        {
            return dbset.Find(idNumber);
        }

        public virtual T Get(EventQueryOptions<T> options)
        {
            IQueryable<T> query = dbset;
            foreach (string include in options.getIncludes())
            {
                query = query.Include(include);
            }
            if (options.HasWhere)
                query = query.Where(options.Where);
            return query.FirstOrDefault();
        }

        public virtual void Insert(T entity) => dbset.Add(entity);
        public virtual void Update(T entity) => dbset.Update(entity);
        public virtual void Delete(T entity) => dbset.Remove(entity);
        public virtual void Save() => context.SaveChanges();
    }

}