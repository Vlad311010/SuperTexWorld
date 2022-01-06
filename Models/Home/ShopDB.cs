using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ASPProject.Models.Home
{
    public class ShopDB : DbContext
    {
        public ShopDB()
        { }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ItemModel> Items { get; set; }
    }
}