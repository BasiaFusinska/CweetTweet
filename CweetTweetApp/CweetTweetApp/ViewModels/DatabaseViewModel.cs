using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using TwitterDataBase;
using TwitterModel;

namespace CweetTweetApp.ViewModels
{
    public class DatabaseViewModel : Screen, ILoadTweets
    {
        private readonly DataContextProvider _provider;
        private readonly INavigationService _navigationService;
        public IList<TweetViewItem> Items { get; set; }

        public DatabaseViewModel(DataContextProvider provider, INavigationService navigationService)
        {
            _provider = provider;
            _navigationService = navigationService;
            Items = new ObservableCollection<TweetViewItem>();
        }

        public string UserName { get; set; }

        public void LoadTweets()
        {
            Predicate<TweetViewItem> predicate = _ => true;
            if (!string.IsNullOrEmpty(UserName))
                predicate = t => t.Tweet.UserName == UserName;

            var items = GetAllTweets().Where(t => predicate(t));
            Items.Clear();

            foreach (var tweetViewItem in items)
            {
                Items.Add(tweetViewItem);
            }
        }

        private IEnumerable<TweetViewItem> GetAllTweets()
        {
            using (var context = _provider.InitializeDataContext())
            {
                var tweets = from t in context.GetTable<Tweet>()
                             orderby t.CreatedAt descending 
                             select new TweetViewItem(t, this, false);
                return tweets.ToArray();
            }
        }

        public void Delete(object id)
        {
            if (MessageBox.Show("Delete? Sure?", "", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                var item = Items.FirstOrDefault(i => i.Tweet.Id == (long)id);
                Items.Remove(item);
            }
        }

        public void OnTap(object item)
        {
            var tweetViewItem = item as TweetViewItem;
            //navigate

            new UriBuilder<TweetDetailsViewModel>().AttachTo(_navigationService)
                .WithParam(model => model.UserName, tweetViewItem.Tweet.UserName)
                .WithParam(model => model.Text, tweetViewItem.Tweet.Text)
                .WithParam(model => model.CreatedAtText, tweetViewItem.CreatedAtText)
                .Navigate();
        }
    }
}
