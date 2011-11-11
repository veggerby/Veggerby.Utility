using System;

namespace Veggerby.Utility.Mvc.Sitemap
{
    public interface ISitemap
    {
        string Url { get; set; }
        DateTime? LastModified { get; set; }
    }
}