using System.Linq;

namespace GenericRepositorySolution
{
    public class WebServiceProxy
    {
        
        public WebServiceProxy(string uri) { }
        
        public IQueryable Entity<T>(IQueryable src)
        {
            return null;
        }

        public bool Post(IEntity entity)
        {
            // TODO: serialize entity into URI string to RESTful service

            // TODO: for Testing purposes only, remove after that!
            return Program.AddEntity();
        }

        public IQueryable<IEntity> Get<T>(IQueryable src)
        {
            // TODO: serialize entity into URI string to RESTful service

            // TODO: for Testing purposes only, remove after that!
            return Program.GetAllEntities().AsQueryable();
        }
    }
  
}
