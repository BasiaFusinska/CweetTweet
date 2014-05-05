using System.Data.Linq;
using TwitterModel;

namespace TwitterDataBase
{
    public class TwitterDataContext : DataContext
    {
        public TwitterDataContext(string connectionString)
            : base(connectionString)
        { }

        public Table<Tweet> Tweets;

    }

}
