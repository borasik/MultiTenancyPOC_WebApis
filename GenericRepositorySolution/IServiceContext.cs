using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositorySolution
{
    public interface IServiceContext<T>
    {
        IQueryable<T> Set<T>(IQueryable src = null);

        void Add(T entity);

        void Remove(T entity);

        void SaveChanges();
    }
}
