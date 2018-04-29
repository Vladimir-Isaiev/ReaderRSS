using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.Models
{
    /// <summary>
    /// Item of Rss channel
    /// </summary>
    public class Story
    {
        public string Title { get; set; }
        public Uri Uri { get; set; }
        public string Description { get; set; }

        public Story()
        { }

        public Story(string title, Uri uri, string description)
        {
            this.Title = title;
            this.Uri = uri;
            this.Description = description;
        }
    }
}
