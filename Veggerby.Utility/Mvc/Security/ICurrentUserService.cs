using System.Security.Principal;

namespace Veggerby.Utility.Mvc.Security
{
    public interface ICurrentUserService
    {
        IPrincipal CurrentUser { get; }
    }
}
