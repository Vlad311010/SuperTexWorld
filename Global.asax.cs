using ASPProject.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using ASPProject.Models.Home;

namespace ASPProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies["Cookie1"];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                var serializeModel = JsonConvert.DeserializeObject<User>(authTicket.UserData);
                CustomPrincipal principal = new CustomPrincipal(authTicket.Name);
                User user = new User();
                user.Username = serializeModel.Username;
                user.Email = serializeModel.Email;
                user.Roles = serializeModel.Roles;
                user.Password = serializeModel.Password;
                user.Orders = serializeModel.Orders;
                user.Role = serializeModel.Role;
                principal.user = user;
                HttpContext.Current.User = principal;
            }
        }
    }
}
