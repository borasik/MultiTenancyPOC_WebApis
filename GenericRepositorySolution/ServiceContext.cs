using System.Linq;

namespace GenericRepositorySolution
{
    public class ServiceContext<T> : IServiceContext<T>
    {
        private readonly WebServiceProxy _proxy;

        public ServiceContext()
        {
            
        }

        public ServiceContext(WebServiceProxy wsProxy)
        {
            _proxy = wsProxy;
        }

        public IQueryable<T> Set<T>(IQueryable src = null)
        {
            var entityQuery = _proxy.Get<T>(src);

            //var entityQuery = (DataServiceQuery<T>)
                

            // Execute query.
            return entityQuery as IQueryable<T>;
        }
        
        public void Add(T entity)
        {
            _proxy.Post((IEntity)entity);
        }

        public void Remove(T entity)
        {

        }

        public void SaveChanges()
        {

        }
    }
}
