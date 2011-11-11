using System;

namespace Veggerby.Utility.Mvc.Sitemap
{
    public class Sitemap : ISitemap
    {
        public Sitemap()
        {            
        }

        public Sitemap(string url)
        {
            this.Url = url;
        }

        public string Url { get; set; }

        public DateTime? LastModified { get; set; }
    }
}
