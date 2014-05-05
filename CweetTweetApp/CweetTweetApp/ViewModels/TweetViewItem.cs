using System;
using System.Windows;
using System.Windows.Input;
using TwitterModel;

namespace CweetTweetApp.ViewModels
{
    public class TweetViewItem
    {
        private const string DateFormat = "yyyy-MM-dd hh:mm";
        public Tweet Tweet { get; private set; }

        public ICommand DeleteCommand { get; protected set; }

        private ILoadTweets _parent;

        public TweetViewItem(Tweet tweet, ILoadTweets parent, bool shouldBeSelected = true)
        {
            _parent = parent;
            Tweet = tweet;
            CreatedAtText = string.Format("Created at: {0}", tweet.CreatedAt.ToString(DateFormat));

            SelectionVisibility = shouldBeSelected ? Visibility.Visible : Visibility.Collapsed;
            DeletionVisibility = shouldBeSelected ? Visibility.Collapsed : Visibility.Visible;

            DeleteCommand = new ActionCommand(_parent.Delete);
        }

        public string CreatedAtText { get; private set; }

        public bool Selected { get; set; }
        public Visibility SelectionVisibility { get; private set; }
        public Visibility DeletionVisibility { get; private set; }
    }

    public class ActionCommand : ICommand
    {
        private readonly Action<object> _action;

        public ActionCommand(Action<object> action)
        {
            _action = action;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
