using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using ASPProject.Models.Home;

namespace ASPProject.Authentication
{
    public class CustomPrincipal : IPrincipal
    {

        public User user;
        
        public IIdentity Identity
        {
            get; private set;
        }

        public bool IsInRole(string role)
        {
            if (user.Role == role)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CustomPrincipal(string username)
        {
            Identity = new GenericIdentity(username);
        }
    }
}