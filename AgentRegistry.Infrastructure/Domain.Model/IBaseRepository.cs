using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AgentRegistry.Infrastructure.Domain.Model
{
    public interface IBaseRepository<T> where T : IEntity
    {
        IEnumerable<T> GetAll();

        int Count();

        T FindById(int id);

        T FindByPredicate(Expression<Func<T, bool>> predicate);

        bool Exists(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Commit();
    }
}
