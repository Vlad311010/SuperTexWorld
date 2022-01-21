using System;
using System.IO;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using ASPProject.Models.Home;
using System.Data;
using System.Linq;


[assembly: OwinStartup(typeof(ASPProject.Start.Startup))]

namespace ASPProject.Start
{
    public class Startup
    {
        static ShopEntities db = new ShopEntities();
        public void Configuration(IAppBuilder app)
        {
            app.Use((context, next) =>
            {
                TextWriter output = context.Get<TextWriter>("host.TraceOutput");
                return next().ContinueWith(result =>
                {
                    output.WriteLine("Scheme {0} : Method {1} : Path {2} : MS {3}",
                    context.Request.Scheme, context.Request.Method, context.Request.Path, getTime());
                });
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync(getTime() + " My First OWIN App");
            });
        }

        string getTime()
        {
            return DateTime.Now.Millisecond.ToString();
        }

        public static void CreateFirstAdminUser()
        {

            var admin = (from e in db.Users
                         where e.Username == "admin"
                         select e).FirstOrDefault();

            if (admin == null)
            {
                User user = new User();
                user.Username = "admin";
                user.Email = "admin@stw.com";
                string hashPassword = BCrypt.Net.BCrypt.HashPassword("admin");
                user.Password = hashPassword;
                user.Role = "Admin";
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
    }
}
