using System.Security.Principal;
using System.Web;

namespace Veggerby.Utility.Mvc.Security
{
    public class HttpContextCurrentUserService : ICurrentUserService
    {
        public IPrincipal CurrentUser
        {
            get { return HttpContext.Current.User; }
        }
    }
}
