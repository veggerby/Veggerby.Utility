using System;

namespace Veggerby.Utility.Mvc.Sitemap
{
    public interface ISitemapItem
    {
        string Url { get; }
        DateTime? LastModified { get; }
        ChangeFrequency? ChangeFrequency { get; }
        float? Priority { get; }
    }
}