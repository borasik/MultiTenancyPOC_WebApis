using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GenericRepositorySolution.Entities;

namespace GenericRepositorySolution
{
    class Program
    {
        static IList<IEntity> list = null;

        static void Main(string[] args)
        {
            // create BrillianKid repository
            var _repo = new BrilliantKidRepository();

            // add enntity BrillianKid
            var _enBrillianKid = new BrilliantKid();
            _repo.Add(_enBrillianKid);
            
            // read all entities
            var allEntities = _repo.GetAll();
            foreach (var en in allEntities)
            {
                //Console.WriteLine($"Entity:{en.GetType().Name},Id:{en.Id},Name:{en.Name}");
                PropertyInfo[] allProps = en.GetType().GetProperties();
                var strOut = new StringBuilder();
                foreach (var prop in allProps)
                    strOut.Append($"{prop.Name}:{prop.GetValue(en,null)} | ");
                Console.WriteLine(strOut.ToString());
            }
            Console.ReadLine();
        }

        public static bool AddEntity()
        {
            list = new List<IEntity>();

            for (var i = 0; i < 4; i++)
            {
                Console.Write("Entity ID:");
                var entityId = int.Parse(Console.ReadLine());

                Console.Write("Entity Name:");
                var entityName = Console.ReadLine();

                Console.Write("Entity Type (1-Dumb; 2-Smart):");
                var entityType = Console.ReadLine();
                
                switch (entityType)
                {
                    case "1":
                        StupidKid _stupidKid = new StupidKid();
                        _stupidKid.Id = entityId;
                        _stupidKid.Name = entityName;
                        _stupidKid.LikeCheerios = "Noooo";
                        list.Add(_stupidKid);
                        break;
                    case "2":
                        IEntity _brillianKid = new BrilliantKid();
                        _brillianKid.Id = entityId;
                        _brillianKid.Name = entityName;
                        list.Add(_brillianKid);
                        break;
                }

            }

            return true;
        }

        public static IList<IEntity> GetAllEntities()
        {
            return list;
        }
    }
}
