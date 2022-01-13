using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ASPProject.Models.Home;

namespace ASPProject.Authentication
{
    public class CustomRole : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string userEmail)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return null;
            }

            var userRoles = new string[] { };

            using (ShopEntities context = new ShopEntities())
            {
                var selectedUser = (from us in context.Users.Include("Roles")
                                    where string.Compare(us.Email, userEmail, StringComparison.OrdinalIgnoreCase) == 0
                                    select us).FirstOrDefault();
                
                if (selectedUser != null)
                {
                    userRoles = new[] { selectedUser.Roles.Select(r => r.RoleName).ToString(), selectedUser.Role };
                }

                return userRoles.ToArray();
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            ShopEntities db = new ShopEntities();
            
            User user = (from e in db.Users
                        where e.Email == username
                        select e).FirstOrDefault();
            
            if (user == null)
                return false;

            string userRole = user.Role;
            return roleName == userRole;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}