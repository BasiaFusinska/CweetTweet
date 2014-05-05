using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TwitterModel
{
    [Table]
    public class Tweet
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        [JsonIgnore]
        public int LocalId { get; set; }

        [Column]
        public long Id { get; set; }

        [Column]
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAtString
        {
            set { CreatedAt = TwitterDateTimeParser.Parse(value); }
        }

        [Column]
        public string Text { get; set; }

        [Column]
        [JsonIgnore]
        public string UserName { get; set; }

        public User User
        {
            set { UserName = value.Name; }
        }
    }
}
