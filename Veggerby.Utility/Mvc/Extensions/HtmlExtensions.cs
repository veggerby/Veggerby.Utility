using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Veggerby.Utility.Mvc.Extensions
{
    /// <summary>
    /// HtmlHelper extension methods
    /// </summary>
    public static class HtmlExtensions
    {
        public static MvcHtmlString Resource(this HtmlHelper html, string key, string @default = null)
        {
            var view = html.ViewContext.View as RazorView;
            if (view == null)
            {
                return MvcHtmlString.Create(@default);
            }

            return MvcHtmlString.Create(html.ViewContext.HttpContext.GetLocalResourceObject(view.ViewPath, key) as string ?? @default);
        }

        public static MvcHtmlString Script(this HtmlHelper html, string local, string remote = null)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);
            var script = url.CDN(local, remote);
            if (!string.IsNullOrEmpty(script))
            {
                var tag = new TagBuilder("script");
                tag.Attributes.Add("src", script);
                tag.Attributes.Add("type", "text/javascript");
                return MvcHtmlString.Create(tag.ToString());
            }

            return MvcHtmlString.Empty;
        }

        public static bool HasFile(this HttpPostedFileBase file)
        {
            return (file != null && file.ContentLength > 0);
        }

        public static MvcHtmlString ToShorter(this string value, int maxLength)
        {
            if (value.Length > maxLength)
            {
                var ixc = value.Substring(0, maxLength).LastIndexOf(' ');
                var substr = ixc > 10 ? value.Substring(0, ixc) : value.Substring(0, maxLength);
                var tag = new TagBuilder("abbr");
                tag.Attributes["title"] = value;
                tag.InnerHtml = string.Format("{0}&hellip;", substr);
                return MvcHtmlString.Create(tag.ToString());
            }

            return MvcHtmlString.Create(value);
        }

        public static MvcHtmlString NavigationLink(this HtmlHelper html, string title, string actionName, string controllerName = null, object routeValues = null, object htmlAttributes = null, bool moreLinks = true)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);
            var tag = new TagBuilder("a");
            tag.Attributes.Add("href", url.Action(actionName, controllerName, routeValues));
            var rvd = new RouteValueDictionary(htmlAttributes);
            tag.MergeAttributes(rvd);

            tag.InnerHtml = title;

            if (html.ViewContext.IsFor(null, controllerName))
            {
                tag.AddCssClass("selected");
            }

            return MvcHtmlString.Create(tag + (moreLinks ? " <span>|</span>" : string.Empty));
        }
    }
}