namespace GenericRepositorySolution.Entities
{
    class StupidKid : IEntity
    {
        private int _id = 1;
        private string _name = "Kiddo Kiddie";
        private string _likeCheerio = "Yes";

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string LikeCheerios
        {
            get { return _likeCheerio; }
            set { _likeCheerio = value; }
        }
    }
}
