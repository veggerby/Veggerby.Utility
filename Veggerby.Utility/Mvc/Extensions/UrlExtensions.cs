using System;
using System.Web;
using System.Web.Mvc;
using Veggerby.Utility.Extensions;
using Veggerby.Utility.Properties;

namespace Veggerby.Utility.Mvc.Extensions
{
    /// <summary>
    /// HtmlHelper extension methods
    /// </summary>
    public static class UrlExtensions
    {
        public static string CDN(this UrlHelper url, string local, string remote)
        {
            if (url.RequestContext.HttpContext.Request.IsLocal)
            {
                return url.Content(local);
            }

            return remote;
        }

        public static Uri GetBaseUrl(this UrlHelper url)
        {
            var contextUri = new Uri(url.RequestContext.HttpContext.Request.Url, url.RequestContext.HttpContext.Request.RawUrl);
            var realmUri = new UriBuilder(contextUri) { Path = url.RequestContext.HttpContext.Request.ApplicationPath, Query = null, Fragment = null };
            return realmUri.Uri;
        }

        public static string RouteAbsolute(this UrlHelper url, string routeName, object routeValues)
        {
            return new Uri(GetBaseUrl(url), url.RouteUrl(routeName, routeValues)).AbsoluteUri.Replace("http://0.0.0.0", "http://localhost");
        }

        public static string ActionAbsolute(this UrlHelper url, string actionName, string controllerName, object routeValues)
        {
            return new Uri(GetBaseUrl(url), url.Action(actionName, controllerName, routeValues)).AbsoluteUri.Replace("http://0.0.0.0", "http://localhost");
        }

        public static string ActionAbsolute(this UrlHelper url, string actionName, string controllerName)
        {
            return new Uri(GetBaseUrl(url), url.Action(actionName, controllerName)).AbsoluteUri.Replace("http://0.0.0.0", "http://localhost");
        }

        public static string Absolute(this UrlHelper url, string contentUrl)
        {
            return new Uri(GetBaseUrl(url), url.Content(contentUrl)).AbsoluteUri.Replace("http://0.0.0.0", "http://localhost");
        }

        public static string GravatarUrl(this UrlHelper url, string email, int size)
        {
            string imageUrl = Settings.Default.DefaultGravatar;
            if (imageUrl.StartsWith("~/"))
            {
                imageUrl = url.Absolute(imageUrl);
            }

            if (string.IsNullOrEmpty(email))
            {
                email = Guid.NewGuid().ToString(); // random image
            }

            string md5 = email.ToLowerInvariant().MD5();

            return string.Format(
                "http://www.gravatar.com/avatar/{0}.jpg?d={1}&s={2}&r=g",
                md5.ToLowerInvariant(),
                url.Encode(imageUrl),
                size);
        }

        public static string GravatarUrl(this UrlHelper url, string email)
        {
            return url.GravatarUrl(email, 32);
        }

        public static string ToAbsolutePath(string virtualPath)
        {
            return VirtualPathUtility.ToAbsolute(virtualPath, HttpContext.Current.Request.ApplicationPath);
        }

    }
}