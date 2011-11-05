namespace Veggerby.Utility.Mvc.Security
{
    public interface IAuthenticationService
    {
        void Login(UserState user);
        void Logout();

        UserState CurrentUser { get; }
    }
}
