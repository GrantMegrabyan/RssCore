using System;
using System.Collections.Generic;
using System.Linq;
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
            var items = new List<RssFeedItem>();
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

                    case "item":
                        var item = ParseItem(childNode);
                        items.Add(item);
                        break;
                }
            }

            if (items.Any())
            {
                rssFeed.Items = items.ToArray();
            }

            return rssFeed;
        }

        private RssFeedItem ParseItem(XmlNode node)
        {
            var item = new RssFeedItem();

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name.ToLower())
                {
                    case "title":
                        item.Title = childNode.InnerText;
                        break;

                    case "description":
                        item.Description = childNode.InnerText;
                        break;

                    case "link":
                        item.Link = new Uri(childNode.InnerText);
                        break;

                    case "pubdate":
                        item.PubDate = DateTimeOffset.Parse(childNode.InnerText);
                        break;
                }
            }

            return item;
        }
    }
}