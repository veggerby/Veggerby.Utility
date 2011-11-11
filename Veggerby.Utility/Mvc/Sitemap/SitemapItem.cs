using System;

namespace Veggerby.Utility.Mvc.Sitemap
{
    public class SitemapItem : ISitemapItem
    {
        public SitemapItem()
        {            
        }

        public SitemapItem(string url)
        {
            Url = url;
        }

        public string Url { get; set; }

        public DateTime? LastModified { get; set; }

        public ChangeFrequency? ChangeFrequency { get; set; }

        public float? Priority { get; set; }
    }
}