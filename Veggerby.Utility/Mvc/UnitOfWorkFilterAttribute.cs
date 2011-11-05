using System.Web.Mvc;
using System;

namespace Veggerby.Utility.Mvc
{
    /// <summary>
    /// Global action filter to ensure UoW is commited upon successfull action invokation 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class UnitOfWorkFilterAttribute : FilterAttribute, IActionFilter
    {
        //[Inject]
        public IUnitOfWork UnitOfWork { get; set; }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.UnitOfWork.Commit();
        }
    }
}