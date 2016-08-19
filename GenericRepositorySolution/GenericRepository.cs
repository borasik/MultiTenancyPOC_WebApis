using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GenericRepositorySolution
{
    public abstract class GenericRepository<TC, T> :
        IGenericRepository<T> where T : IEntity where TC : IServiceContext<T>
    {
        public TC Context { get; set; }

        public virtual IEnumerable<T> GetAll()
        {

            return Context.Set<T>().AsEnumerable();
       
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return Context?.Set<T>().Where(predicate);
        }

        public T FindById(int entityId)
        {
            return Context.Set<T>().FirstOrDefault(x => x.Id == entityId);
        }

        public virtual void Add(T entity)
        {
            Context.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            Context.Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Edit(int entityId)
        {
            //_entities.Entry(entity).State = System.Data.EntityState.Modified;
            throw new NotImplementedException();
        }

        public virtual void Save()
        {
            Context.SaveChanges();
        }
    }
}
