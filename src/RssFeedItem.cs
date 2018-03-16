using System;

namespace Grant.RssCore
{
    public class RssFeedItem
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset PubDate { get; set; }

        public Uri Link { get; set; }
    }
}