using System;
using System.Xml.Linq;

namespace Veggerby.Utility.Mvc.Sitemap
{
    public enum ChangeFrequency
    {
        Always,
        Hourly,
        Daily,
        Weekly,
        Monthly,
        Yearly,
        Never
    }
    
    public class Utility
    {
        public const string UrlSetSchemaLocationUrl = "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd";

        public static readonly XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        public static readonly XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";

        public static string CreateXmlDate(DateTime? date)
        {
            // YYYY-MM-DDThh:mm:ssTZD
            if (date.HasValue)
            {
                if (date.Value.Kind == DateTimeKind.Local)
                {
                    date = date.Value.ToUniversalTime();
                }

                if (date.Value.TimeOfDay != TimeSpan.MinValue)
                {
                    return date.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'");
                }                
                
                return date.Value.ToString("yyyy'-'MM'-'dd");
            }

            return string.Empty;
        }
    }
}