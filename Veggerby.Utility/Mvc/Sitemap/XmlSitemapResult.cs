using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Veggerby.Utility.Mvc.Sitemap
{
    public class XmlSitemapResult : ActionResult
    {
        public const int MaxItemsPerSitemap = 50000;

        private readonly IEnumerable<ISitemapItem> _Items;

        public XmlSitemapResult(IEnumerable<ISitemapItem> items)
        {
            this._Items = items;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var encoding = context.HttpContext.Response.ContentEncoding.WebName;
            var sitemap = new XDocument(new XDeclaration("1.0", encoding, "yes"),
                new XElement(Utility.xmlns + "urlset",
                    new XAttribute(XNamespace.Xmlns + "xsi", Utility.xsi),
                    new XAttribute(Utility.xsi + "schemaLocation", Utility.UrlSetSchemaLocationUrl),
                    this._Items.Select(this.CreateItemElement)
                )
            );

            context.HttpContext.Response.ContentType = "application/rss+xml";
            context.HttpContext.Response.Flush();
            context.HttpContext.Response.Write(sitemap.Declaration + sitemap.ToString());
        }

        private XElement CreateItemElement(ISitemapItem item)
        {
            var itemElement = new XElement(Utility.xmlns + "url", new XElement(Utility.xmlns + "loc", item.Url.ToLower()));

            if (item.LastModified.HasValue)
            {
                itemElement.Add(new XElement(Utility.xmlns + "lastmod", Utility.CreateXmlDate(item.LastModified)));
            }

            if (item.ChangeFrequency.HasValue)
            {
                itemElement.Add(new XElement(Utility.xmlns + "changefreq", item.ChangeFrequency.Value.ToString().ToLower()));
            }

            if (item.Priority.HasValue)
            {
                itemElement.Add(new XElement(Utility.xmlns + "priority", item.Priority.Value.ToString(CultureInfo.InvariantCulture)));
            }

            return itemElement;
        }
    }
}