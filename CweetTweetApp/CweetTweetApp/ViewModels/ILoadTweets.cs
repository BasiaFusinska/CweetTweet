namespace CweetTweetApp.ViewModels
{
    public interface ILoadTweets
    {
        void LoadTweets();

        void Delete(object id);
    }
}
