using AgentRegistry.Infrastructure.Common;
using AgentRegistry.Infrastructure.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LampsCost.DataAccess.Context
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
    {
        protected IDataContext dataContext;

        public BaseRepository(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public virtual IEnumerable<T> GetAll()
            => dataContext.Set<T>().AsEnumerable();

        public virtual int Count()
            => dataContext.Set<T>().Count();

        public virtual void Add(T entity) => dataContext.Set<T>().Add(entity);

        public virtual T FindById(int id) => dataContext.Set<T>().FirstOrDefault(x => x.Id == id);

        public T FindByPredicate(Expression<Func<T, bool>> predicate)
            => dataContext.Set<T>().FirstOrDefault(predicate);

        public bool Exists(Expression<Func<T, bool>> predicate)
            => dataContext.Set<T>().Any(predicate);

        public virtual void Update(T entity)
        {
            EntityEntry dbEntityEntry = dataContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = dataContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void Commit()
        {
            dataContext.SaveChanges();
        }
    }
}
