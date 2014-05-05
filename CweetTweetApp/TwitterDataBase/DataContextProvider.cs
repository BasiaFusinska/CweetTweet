using System.Data.Linq;

namespace TwitterDataBase
{
    public class DataContextProvider
    {
        private const string DbFileName = "base.sdf";

        public string BaseConnectionString
        {
            get
            {
                return "Data Source=isostore:/" + DbFileName + ";" + " Max Database Size = 512";
            }
        }

        public DataContext InitializeDataContext()
        {
            return new TwitterDataContext(BaseConnectionString);
        }

        public void CreateDataBase()
        {
            using (var db = new TwitterDataContext(BaseConnectionString))
            {
                if (db.DatabaseExists() == false)
                {
                    // Create the database.
                    db.CreateDatabase();
                }
            }
        }
    }
}
