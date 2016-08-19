using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositorySolution.Entities
{
    class StupidKidRepository : GenericRepository<ServiceContext<IEntity>,IEntity>
    {
        public StupidKidRepository()
        {
            var _wsProxy = new WebServiceProxy(@"http://localhost/dummio/someservice.svc");
            Context = new ServiceContext<IEntity>(_wsProxy);
        }
    }
}
