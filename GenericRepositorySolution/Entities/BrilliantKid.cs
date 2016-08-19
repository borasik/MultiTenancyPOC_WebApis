using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositorySolution
{
    class BrilliantKid : IEntity
    {
        private int _id = 1;
        private string _name = "Kiddo Kiddie";

        public int Id {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
