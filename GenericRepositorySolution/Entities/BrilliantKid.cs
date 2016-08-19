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
