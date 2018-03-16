using System;
using System.Xml;
using System.Xml.Linq;

namespace Grant.RssCore
{
    public class RssParser
    {
        public RssFeed Parse(string rssFeedString)
        {
            if (rssFeedString == null)
                throw new ArgumentNullException(nameof(rssFeedString));

            if (string.IsNullOrEmpty(rssFeedString))
                throw new ArgumentException(nameof(rssFeedString));

            var rssFeed = new RssFeed();
            var xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(rssFeedString);

            var channelNode = xmlDoc.DocumentElement.SelectSingleNode("channel");

            foreach (XmlNode childNode in channelNode.ChildNodes)
            {
                switch (childNode.Name.ToLower())
                {
                    case "title":
                        rssFeed.Title = childNode.InnerText;
                        break;

                    case "description":
                        rssFeed.Description = childNode.InnerText;
                        break;

                    case "link":
                        rssFeed.Link = new Uri(childNode.InnerText);
                        break;

                    case "lastbuilddate":
                        rssFeed.LastBuildDate = DateTimeOffset.Parse(childNode.InnerText);
                        break;
                }
            }

            return rssFeed;
        }
    }
}