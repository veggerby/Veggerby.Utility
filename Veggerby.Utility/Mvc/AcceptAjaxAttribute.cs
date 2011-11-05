using System.Reflection;
using System.Web.Mvc;

namespace Veggerby.Utility.Mvc
{
    public class AcceptAjaxAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.RequestContext.HttpContext.Request.IsAjaxRequest();
        }
    }
}