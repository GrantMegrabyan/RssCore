using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Grant.RssCore
{
    public interface IRssSource
    {
        Task<string> GetAsync();
    }

    public abstract class RssSource
    {
        public static IRssSource FromUrl(Uri url)
        {
            return new UrlRssSource(url);
        }
    }

    public class TextRssSource : IRssSource
    {
        private readonly string _xml;
        public TextRssSource(string xml)
        {
            _xml = xml;
        }

        public Task<string> GetAsync()
        {
            return Task.FromResult(_xml);
        }
    }

    public class UrlRssSource : IRssSource
    {
        private readonly Uri _url;

        public UrlRssSource(Uri url)
        {
            _url = url;
        }

        public async Task<string> GetAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_url);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}