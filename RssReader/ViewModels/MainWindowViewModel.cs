using RssReader.Infrastructure;
using RssReader.Models;
using RssReader.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using FeedURL = System.String;

namespace RssReader.ViewModels
{
    public class MainWindowViewModel: ViewModelBase
    {
        Story currentStory;
        FeedURL currentFeed;
       

        ObservableCollection<Story> stories;
        ObservableCollection<FeedURL> feeds;
        RelayCommand changeFeedCommand;
        RelayCommand saveFeedCommand;
        RelayCommand delFeedCommand;

        public Story CurrentStory
        {
            get
            {
                if (currentStory == null)
                {
                    currentStory = new Story();
                }
                return currentStory;
            }
            set
            {
                currentStory = value;
                OnPropertyChanged("CurrentStory");
            }
        }


        public FeedURL CurrentFeed
        {
            get
            {
                if (currentFeed == null)
                {
                    currentFeed = "";
                }
                return currentFeed;
            }
            set
            {
                currentFeed = value;
                OnPropertyChanged("CurrentFeed");
            }
        }


        public ObservableCollection<Story> Stories
        {
            get
            {
                if (stories == null)
                {
                    stories = new ObservableCollection<Story>(StoryRepository.AllStories(currentFeed));
                }
                return stories;
            }
            set
            {
                stories = value;
                OnPropertyChanged("Stories");
            }
        }


        public ObservableCollection<FeedURL> Feeds
        {
            get
            {
                if (feeds == null)
                {
                    feeds = new ObservableCollection<FeedURL>(FeedRepository.AllFeeds());
                }
                return feeds;
            }
            set
            {
                feeds = value;
                OnPropertyChanged("Feeds");
            }
        }


        public ICommand ChangeFeed
        {
            get
            {
                if (changeFeedCommand == null)
                {
                    changeFeedCommand = new RelayCommand(ExecuteChangeFeedCommand);
                }
                return changeFeedCommand;
            }
        }


        public ICommand SaveFeed
        {
            get
            {
                if (saveFeedCommand == null)
                {
                    saveFeedCommand = new RelayCommand(ExecuteSaveFeedCommand);
                }
                return saveFeedCommand;
            }
        }

        public ICommand DelFeed
        {
            get
            {
                if (delFeedCommand == null)
                {
                    delFeedCommand = new RelayCommand(ExecuteDelFeedCommand);
                }
                return delFeedCommand;
            }
        }

        public void ExecuteChangeFeedCommand(object parameter)
        {
            Stories = new ObservableCollection<Story>(StoryRepository.AllStories(currentFeed)); 
        }

        public void ExecuteSaveFeedCommand(object parameter)
        {
            FeedRepository.AddFeed(currentFeed);
            Apdate();
        }

        public void ExecuteDelFeedCommand(object parameter)
        {
            FeedRepository.DelFeed(currentFeed);
            Apdate();
        }


        private void Apdate()
        {
            Feeds = new ObservableCollection<FeedURL>(FeedRepository.AllFeeds());
            currentFeed = feeds.FirstOrDefault();
            Stories = new ObservableCollection<Story>(StoryRepository.AllStories(currentFeed));           
        }


    }
}
