using System;
using AutoFixture;
using Xunit;
using Xunit.Abstractions;

namespace Grant.RssCore.Tests
{
    public class RssParserTests
    {
        private readonly ITestOutputHelper _output;
        public RssParserTests(ITestOutputHelper output)
        {
            this._output = output;
        }

        [Fact]
        public void Parse_WithNullInput_ThrowsArgumentNullException()
        {
            var parser = new RssParser();

            Action act = () => parser.Parse(null);

            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void Parse_WithEmptyInput_ThrowsArgumentException()
        {
            var parser = new RssParser();

            Action act = () => parser.Parse(string.Empty);

            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void Parse_WithChannelData_CorrectObjectReturns()
        {
            var fx = new Fixture();

            var title = fx.Create<string>();
            var link = fx.Create<Uri>();
            var description = fx.Create<string>();
            var lastBuildDateString = "Fri, 16 Mar 2018 20:20:20 +0000";
            var lastBuildDate = new DateTimeOffset(2018, 3, 16, 20, 20, 20, TimeSpan.FromHours(0));

            var rssXml = new RssXmlBuilder()
                .WithTitle(title)
                .WithLink(link.ToString())
                .WithDescription(description)
                .WithLastBuildDate(lastBuildDateString)
                .Build();

            var parser = new RssParser();
            var rssFeed = parser.Parse(rssXml);

            Assert.Equal(title, rssFeed.Title);
            Assert.Equal(link, rssFeed.Link);
            Assert.Equal(description, rssFeed.Description);
            Assert.Equal(lastBuildDate, rssFeed.LastBuildDate);
        }

        [Fact]
        public void Parse_WithItem_RssFeedItemWithCorrectDataReturns()
        {
            //Given
            var fx = new Fixture();

            var title = fx.Create<string>();
            var link = fx.Create<Uri>();
            var description = fx.Create<string>();
            var pubDateString = "Sun, 18 Mar 2018 16:48:17 +0000";
            var pubDate = new DateTimeOffset(2018, 3, 18, 16, 48, 17, TimeSpan.FromHours(0));

            var item = new RssItemXmlBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithLink(link.ToString())
                .WithPubDate(pubDateString)
                .Build();

            var rssXml = new RssXmlBuilder()
                .WithItems(new [] { item })
                .Build();

            //When
            var parser = new RssParser();
            var rssFeed = parser.Parse(rssXml);

            //Then
            Assert.Equal(1, rssFeed.Items?.Length);
            Assert.Equal(title, rssFeed.Items?[0].Title);
            Assert.Equal(description, rssFeed.Items?[0].Description);
            Assert.Equal(pubDate, rssFeed.Items?[0].PubDate);
            Assert.Equal(link, rssFeed.Items?[0].Link);
        }

    }
}