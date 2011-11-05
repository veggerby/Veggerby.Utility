using System;
using System.Web.Routing;
using System.Web.Mvc;

namespace Veggerby.Utility.Mvc.Extensions
{
    public static class RouteExtensions
    {
        public static bool IsFor(this ViewContext view, string actionName, string controllerName = null)
        {
            return
                ((actionName == null) || string.Equals(view.RouteData.Values["action"] as string, actionName, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(controllerName) || (string.Equals(view.RouteData.Values["controller"] as string, controllerName, StringComparison.OrdinalIgnoreCase)));
        }    
        
        public static RouteValueDictionary Merge(this RouteValueDictionary source, RouteValueDictionary routeValueDictionary = null, object routeValues = null)
        {
            var result = routeValues != null ? new RouteValueDictionary(routeValues) : new RouteValueDictionary();
            if (source != null)
            {
                foreach (var key in source.Keys)
                {
                    if (!result.ContainsKey(key))
                    {
                        result[key] = source[key];
                    }
                }
            }

            if (routeValueDictionary != null)
            {
                foreach (var key in routeValueDictionary.Keys)
                {
                    result[key] = routeValueDictionary[key];
                }
            }

            return result;
        }

        public static Route MapRouteLowercase(this RouteCollection routes, string name, string url, object defaults)
        {
            return routes.MapRouteLowercase(name, url, defaults, null);
        }

        public static Route MapRouteLowercase(this RouteCollection routes, string name, string url, object defaults, object constraints)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }

            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            var route = new LowercaseRoute(url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints)
            };

            if (string.IsNullOrEmpty(name))
            {
                routes.Add(route);
            }
            else
            {
                routes.Add(name, route);
            }

            return route;
        }
    }
}
