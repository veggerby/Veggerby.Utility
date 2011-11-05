using System.Web.Mvc;

namespace Veggerby.Utility.Mvc
{
    public class Return404Attribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.StatusCode = 404;
        }
    }
}