using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TwitterApi.OAuth;
using TwitterModel;

namespace TwitterApi
{
    public class TwitterApi
    {
        private const string TwitterUriString = "https://api.twitter.com";
        private const string DefaultScreenNameParameter = "screen_name";
        private const string DefaultCountParameter = "count";
        private const string DefaultUserTimelineResource = "1.1/statuses/user_timeline.json";

        private readonly IRestClient _restClient = new RestClient(TwitterUriString)
            {
                Authenticator = TwitterAuthenticator.GetTwitterAuthenticator()
            };

        public Task<Tweet[]> GetTweetsForUser(string userScreenName, int numberOfTweets)
        {
            var request = CreateRequestForUserTweets(userScreenName, numberOfTweets);
            var taskComplete = new TaskCompletionSource<Tweet[]>();

            _restClient.ExecuteAsync(request, 
                r => taskComplete.SetResult(JsonConvert.DeserializeObject<Tweet[]>(r.Content)));

            return taskComplete.Task;
        }

        public Task<string> GetTweets(string userScreenName, int numberOfTweets)
        {
            var request = CreateRequestForUserTweets(userScreenName, numberOfTweets);
            var taskComplete = new TaskCompletionSource<string>();

            _restClient.ExecuteAsync(request,
                r => taskComplete.SetResult(r.Content));

            return taskComplete.Task;
        }

        private static RestRequest CreateRequestForUserTweets(string userScreenName, int numberOfTweets)
        {
            var request = new RestRequest(DefaultUserTimelineResource);
            var formattedName = userScreenName.Trim(new[] { '@' });
            request.AddParameter(DefaultScreenNameParameter, formattedName);
            request.AddParameter(DefaultCountParameter, numberOfTweets);
            return request;
        }
    }
}
