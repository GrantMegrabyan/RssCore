namespace Grant.RssCore.Tests
{
    public class RssXmlBuilder
    {
        string _title = string.Empty;
        string _link = string.Empty;
        string _description = string.Empty;
        string _lastBuildDate = string.Empty;

        public RssXmlBuilder WithTitle(string title)
        {
            _title = $"<title>{title}</title>";
            return this;
        }

        public RssXmlBuilder WithLink(string link)
        {
            _link = $"<link>{link}</link>";
            return this;
        }

        public RssXmlBuilder WithDescription(string description)
        {
            _description = $"<description>{description}</description>";
            return this;
        }

        public RssXmlBuilder WithLastBuildDate(string lastBuildDate)
        {
            _lastBuildDate = $"<lastBuildDate>{lastBuildDate}</lastBuildDate>";
            return this;
        }

        public string Build()
        {
            return $@"<?xml version='1.0' encoding='UTF-8'?>
<rss xmlns:dc=""http://purl.org/dc/elements/1.1/"" xmlns:atom=""http://www.w3.org/2005/Atom"" version=""2.0"">
  <channel>
    {_title}
    {_link}
    {_description}
    {_lastBuildDate}
  </channel>
</rss>
";
        }
    }
}