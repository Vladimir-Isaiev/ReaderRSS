using RssReader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RssReader.Repository
{
    /// <summary>
    /// Get RSS items from cnannel
    /// </summary>
    public static class StoryRepository
    {
        private static List<Story> stories;

        static StoryRepository()
        {
            stories = new List<Story>();
        }

        public static List<Story> AllStories(string rssUri)
        {
            XmlReader reader;
            SyndicationFeed feed;

            try
            {
                reader = XmlReader.Create(rssUri);
                feed = SyndicationFeed.Load(reader);
                reader.Close();

                if (feed != null)
                {
                    stories.Clear();

                    foreach (SyndicationItem item in feed.Items)
                    {
                        Story story;

                        if (item.Content != null) // Atom
                        {
                            story = new Story(item.Title.Text, 
                                item.Links.First().Uri, 
                                ((TextSyndicationContent)item.Content).Text);
                        }
                        else //  RSS
                        {
                            story = new Story(item.Title.Text, item.Links.First().Uri, item.Summary.Text);
                        }

                        stories.Add(story);
                    }
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Invalid feed"+ rssUri);
            }

            return stories;
        }
    }
}
