namespace GenericRepositorySolution.Entities
{
    class BrilliantKidRepository : GenericRepository<ServiceContext<IEntity>,IEntity>
    {
        public BrilliantKidRepository()
        {
            var _wsProxy = new WebServiceProxy(@"http://localhost/smartio/someservice.svc");
            Context = new ServiceContext<IEntity>(_wsProxy);
        }
    }
}
