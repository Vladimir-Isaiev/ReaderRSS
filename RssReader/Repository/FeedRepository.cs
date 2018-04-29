using RssReader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.Repository
{
    public static class FeedRepository
    {
        /// <summary>
        /// DataLayer for RSS channels
        /// </summary>
        private static List<string> feedAddresses;
        private static DataContractJsonSerializer jsonFormatter;

        static FeedRepository()
        {
           jsonFormatter = new DataContractJsonSerializer(typeof(List<string>));

            using (FileStream fileStream = new FileStream("feed.json", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                try
                {
                    feedAddresses = (List<string>)jsonFormatter.ReadObject(fileStream);
                }
                catch
                {
                    feedAddresses = new List<string>
                    {
                        "https://www.cnet.com/rss/all/",
                        "https://stackoverflow.com/feeds/"
                    };

                    jsonFormatter.WriteObject(fileStream, feedAddresses);
                }
            }
        }

        public static List<string> AllFeeds()
        {
            return feedAddresses;
        }

        public static void AddFeed(string feedAddress)
        {
            if (!feedAddresses.Contains(feedAddress) && feedAddress!=string.Empty)
            {
                feedAddresses.Add(feedAddress);
                SaveFeeds();
            }
        }

        public static void DelFeed(string feedAddress)
        {
            if (feedAddresses.Contains(feedAddress))
            {
                feedAddresses.Remove(feedAddress);
                SaveFeeds();
            }
        }

        private static void SaveFeeds()
        {
            using (FileStream fileStream = new FileStream("feed.json", FileMode.Create, FileAccess.Write))
            {
                jsonFormatter.WriteObject(fileStream, feedAddresses);
            }
        }
    }
}
