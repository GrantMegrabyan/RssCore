namespace Grant.RssCore.Tests
{
    class RssItemXmlBuilder
    {
        string _title = string.Empty;
        string _link = string.Empty;
        string _description = string.Empty;
        string _pubDate = string.Empty;

        public RssItemXmlBuilder WithTitle(string title)
        {
            _title = $"<title>{title}</title>";
            return this;
        }

        public RssItemXmlBuilder WithLink(string link)
        {
            _link = $"<link>{link}</link>";
            return this;
        }

        public RssItemXmlBuilder WithDescription(string description)
        {
            _description = $"<description>{description}</description>";
            return this;
        }

        public RssItemXmlBuilder WithPubDate(string pubDate)
        {
            _pubDate = $"<pubDate>{pubDate}</pubDate>";
            return this;
        }

        public string Build()
        {
            return $@"
<item>
    {_title}
    {_link}
    {_description}
    {_pubDate}
</item>
";
        }
    }
}