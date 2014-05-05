using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Caliburn.Micro;

namespace CweetTweetApp.ViewModels
{
    public class TwitterFeedViewModel : Screen, ILoadTweets
    {
        private readonly TwitterApi.TwitterApi _twitterApi;
        public IList<TweetViewItem> Items { get; set; }

        public TwitterFeedViewModel(TwitterApi.TwitterApi twitterApi)
        {
            _twitterApi = twitterApi;
            Items = new ObservableCollection<TweetViewItem>();
        }

        public string UserName { get; set; }

        public async void LoadTweets()
        {
            if (string.IsNullOrEmpty(UserName))
                return;

            var items = await _twitterApi.GetTweetsForUser(UserName, 10);

            if (items == null)
            {
                MessageBox.Show("No internet :(", "", MessageBoxButton.OK);
                return;
            }

            Items.Clear();
            foreach (var tweet in items)
            {
                Items.Add(new TweetViewItem(tweet, this));
            }
        }

        public void Delete(object id)
        {
        }

        public void OnTap(object item)
        {
        }
    }
}
