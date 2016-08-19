using System.Linq;

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
