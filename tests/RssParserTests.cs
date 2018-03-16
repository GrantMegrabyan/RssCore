using System;
using Xunit;

namespace Grant.RssCore.Tests
{
    public class RssParserTests
    {
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

    }
}
