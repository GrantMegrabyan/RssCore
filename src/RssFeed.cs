using System;

namespace Grant.RssCore
{
    public class RssFeed
    {
        public string Title { get; set; }

        public Uri Link { get; set; }

        public string Description { get; set; }

        public DateTimeOffset LastBuildDate { get; set; }

        public RssFeedItem[] Items { get; set; }
    }
}