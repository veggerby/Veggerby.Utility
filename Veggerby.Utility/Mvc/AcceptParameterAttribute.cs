using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Veggerby.Utility.Mvc
{
    public enum ParameterSource
    {
        Form,
        RouteData
    }

    public class AcceptParameterAttribute : ActionMethodSelectorAttribute
    {
        public AcceptParameterAttribute()
        {
            this.Source = ParameterSource.Form;            
        }

        public string Name { get; set; }
        public string Value { get; set; }
        public string[] Values { get; set; }
        public ParameterSource Source { get; set; }

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            if (this.Source == ParameterSource.Form)
            {
                var req = controllerContext.RequestContext.HttpContext.Request;
                return
                    string.Equals(req.Form[this.Name], this.Value) ||
                    ((this.Values != null) && this.Values.Contains(req.Form[this.Name]));
            }

            if (this.Source == ParameterSource.RouteData)
            {
                return string.Equals(controllerContext.RequestContext.RouteData.Values[this.Name], this.Value) ||
                       ((this.Values != null) &&
                        this.Values.Contains(controllerContext.RequestContext.RouteData.Values[this.Name]));
            }

            return false;
        }
    }
}