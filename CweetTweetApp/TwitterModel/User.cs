using Newtonsoft.Json;

namespace TwitterModel
{
    public class User
    {
        [JsonProperty("screen_name")]
        public string Name { get; set; }
    }
}
