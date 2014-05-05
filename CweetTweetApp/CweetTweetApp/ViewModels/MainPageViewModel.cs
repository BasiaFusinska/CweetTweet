using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using TwitterDataBase;
using TwitterModel;

namespace CweetTweetApp.ViewModels
{
    public class MainPageViewModel : Conductor<object>.Collection.OneActive
    {
        private readonly Func<TwitterFeedViewModel> _twitterViewFactory;
        private readonly Func<DatabaseViewModel> _databaseViewFactory;
        private readonly DataContextProvider _provider;

        public MainPageViewModel(Func<TwitterFeedViewModel> twitterViewFactory,
                                 Func<DatabaseViewModel> databaseViewFactory,
                                 DataContextProvider provider)
        {
            _twitterViewFactory = twitterViewFactory;
            _databaseViewFactory = databaseViewFactory;
            _provider = provider;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            CreatePages();
            ActivateItem(Items.First());

            _provider.CreateDataBase();
        }

        private void CreatePages()
        {
            Items.AddRange(new object[]
                {
                    CreateTwitterFeedView(),
                    CreateDatabaseView()
                });
        }

        private TwitterFeedViewModel CreateTwitterFeedView()
        {
            var twitterView = _twitterViewFactory();
            twitterView.DisplayName = "twitter";
            return twitterView;
        }

        private DatabaseViewModel CreateDatabaseView()
        {
            var databaseView = _databaseViewFactory();
            databaseView.DisplayName = "database";
            return databaseView;
        }

        public void LoadTweets()
        {
            var item = ActiveItem as ILoadTweets;
            item.LoadTweets();
        }

        public void SaveSelected()
        {
            var firstView = ActiveItem as TwitterFeedViewModel;
            if (firstView == null)
                return;

            var items = firstView.Items.Where(x => x.Selected);

            using (var context = _provider.InitializeDataContext())
            {
                var tweetsTable = context.GetTable<Tweet>();
                var tweetsIds = tweetsTable.Select(t => t.Id);
                var itemsToBeSaved = items.Where(t => !tweetsIds.Contains(t.Tweet.Id));

                foreach (var tweetViewItem in itemsToBeSaved)
                {
                    tweetsTable.InsertOnSubmit(tweetViewItem.Tweet);
                }

                context.SubmitChanges();
            }
        }
    }
}
