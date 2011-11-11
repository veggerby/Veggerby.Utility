using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Veggerby.Utility.Mvc.Sitemap
{
    public class XmlSitemapIndexResult : ActionResult
    {
        private readonly IEnumerable<ISitemap> _Sitemaps;

        public XmlSitemapIndexResult(IEnumerable<ISitemap> sitemaps)
        {
            this._Sitemaps = sitemaps;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var encoding = context.HttpContext.Response.ContentEncoding.WebName;
            var sitemap = new XDocument(new XDeclaration("1.0", encoding, "yes"),
                 new XElement(Utility.xmlns + "sitemapindex",
                    new XAttribute(XNamespace.Xmlns + "xsi", Utility.xsi),
                    new XAttribute(Utility.xsi + "schemaLocation", Utility.UrlSetSchemaLocationUrl),
                    this._Sitemaps.Select(this.CreateItemElement))
                 );

            context.HttpContext.Response.ContentType = "application/rss+xml";
            context.HttpContext.Response.Flush();
            context.HttpContext.Response.Write(sitemap.Declaration + sitemap.ToString());
        }

        private XElement CreateItemElement(ISitemap sitemap)
        {
            var element = new XElement(Utility.xmlns + "sitemap",
                new XElement(Utility.xmlns + "loc", sitemap.Url));

            if (sitemap.LastModified.HasValue)
            {
                element.Add(new XElement(Utility.xmlns + "lastmod", Utility.CreateXmlDate(sitemap.LastModified)));
            }

            return element;
        }
    }
}