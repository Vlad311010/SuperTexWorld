using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ASPProject.Models.Home;

namespace ASPProject.Authentication
{
    public class CustomMembershipUser : MembershipUser
    {
        public User user;
        public CustomMembershipUser(User user) : base("CustomAuthentication", user.Username, user.Id, user.Email, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            this.user = user;
            /*this.user.Id = user.Id;
            this.user.Username= user.Username;
            this.user.Email = user.Email;
            this.user.Roles = user.Roles;
            this.user.Password = user.Password;
            this.user.Orders = user.Orders;
            this.user.Role = user.Role;*/
        }
    }
}