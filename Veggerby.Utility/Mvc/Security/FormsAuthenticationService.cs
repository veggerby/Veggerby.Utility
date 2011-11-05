using System;
using System.Web;
using System.Web.Security;
using Veggerby.Utility.Properties;

namespace Veggerby.Utility.Mvc.Security
{
    public class FormsAuthenticationService : IAuthenticationService
    {
        public FormsAuthenticationService(ICurrentUserService currentUserService)
        {
            this._CurrentUserService = currentUserService;
        }

        private readonly ICurrentUserService _CurrentUserService;
        public ICurrentUserService CurrentUserService
        {
            get { return this._CurrentUserService; }
        }

        public void Login(UserState user)
        {
            var expireDate = DateTime.Now.AddDays(Settings.Default.FormsAuthenticationCookieLifetimeDays);
            var ticket = new FormsAuthenticationTicket(
                1,
                user.UserId,
                DateTime.Now,
                expireDate,
                true,
                user.ToString());

            var ticketString = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketString) { Expires = expireDate };
            HttpContext.Current.Response.Cookies.Add(cookie);

            this.CurrentUser = user;
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

        private UserState _CurrentUser;
        public UserState CurrentUser
        {
            get
            {
                if (this._CurrentUser == null)
                {
                    // not testable
                    var user = this.CurrentUserService.CurrentUser;
                    UserState state;
                    var ticket = (user != null) && (user.Identity is FormsIdentity) ? ((FormsIdentity)user.Identity).Ticket : null;
                    if ((ticket != null) && UserState.TryParse(ticket.UserData, out state))
                    {
                        this.CurrentUser = state;
                    }
                }

                return this._CurrentUser;
            }

            private set { this._CurrentUser = value; }
        }
    }
}
