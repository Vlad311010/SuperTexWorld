using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPProject.Models.Home
{
    public enum OrderState {
        IN_CART = 0,
        ORDERED = 1
    }

    public enum UserRoles
    {
        Admin = 0,
        User = 1
    }

}