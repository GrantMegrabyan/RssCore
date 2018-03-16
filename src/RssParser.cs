using System;

namespace Grant.RssCore
{
    public class RssParser
    {
        public RssFeed Parse(string rssFeedString)
        {
            if (rssFeedString == null)
            {
                throw new ArgumentNullException(nameof(rssFeedString));
            }

            return new RssFeed();
        }
    }
}