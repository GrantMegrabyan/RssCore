using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Grant.RssCore
{
    public class RssParser
    {
        public async Task<RssFeed> ParseAsync(IRssSource source)
        {
            var xml = await source.GetAsync();

            return Parse(xml);
        }

        public RssFeed Parse(string rssXml)
        {
            if (rssXml == null)
                throw new ArgumentNullException(nameof(rssXml));

            if (string.IsNullOrEmpty(rssXml))
                throw new ArgumentException(nameof(rssXml));

            var rssFeed = new RssFeed();
            var items = new List<RssFeedItem>();
            var xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(rssXml);

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