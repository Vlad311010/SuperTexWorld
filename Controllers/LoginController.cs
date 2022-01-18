using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ASPProject.Models.Home;
using ASPProject.Authentication;
using Newtonsoft.Json;
using System;

namespace ASPProject.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private ShopEntities context = new ShopEntities();


        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string ReturnUrl = "")
        {
            /*if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }*/
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(User userModel, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {                
                if (Membership.ValidateUser(userModel.Email, userModel.Password))
                {
                    var user = (CustomMembershipUser)Membership.GetUser(userModel.Email, false);
                    //userModel.Role = user.user.r;
                    if (user != null)
                    {
                        userModel.Role = user.user.Role;
                        string userData = JsonConvert.SerializeObject(userModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                            (
                              1, user.user.Email, DateTime.Now, DateTime.Now.AddMinutes(300), false, userData
                            );

                        string enTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
                        Response.Cookies.Add(faCookie);
                    }

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Items");
                    }
                }
            }
            ViewBag.Error = "Something Wrong : Username or Password invalid ^_^ ";
            return View();
        }

        /*
            [HttpPost]
            public ActionResult Registration(RegistrationView registrationView)
            {
                bool statusRegistration = false;
                string messageRegistration = string.Empty;

                if (ModelState.IsValid)
                {
                    // Email Verification
                    string userName = Membership.GetUserNameByEmail(registrationView.Email);
                    if (!string.IsNullOrEmpty(userName))
                    {
                        ModelState.AddModelError("Warning Email", "Sorry: Email already Exists");
                        return View(registrationView);
                    }

                    //Save User Data
                    using (AuthenticationDB dbContext = new AuthenticationDB())
                    {
                        var user = new User()
                        {
                            Username = registrationView.Username,
                            FirstName = registrationView.FirstName,
                            LastName = registrationView.LastName,
                            Email = registrationView.Email,
                            Password = registrationView.Password,
                            ActivationCode = Guid.NewGuid(),
                        };

                        dbContext.Users.Add(user);
                        dbContext.SaveChanges();
                    }

                    //Verification Email
                    VerificationEmail(registrationView.Email, registrationView.ActivationCode.ToString());
                    messageRegistration = "Your account has been created successfully. ^_^";
                    statusRegistration = true;
                }
                else
                {
                    messageRegistration = "Something Wrong!";
                }
                ViewBag.Message = messageRegistration;
                ViewBag.Status = statusRegistration;

                return View(registrationView);
            }

            
            public ActionResult LogOut()
            {
                HttpCookie cookie = new HttpCookie("Cookie1", "");
                cookie.Expires = DateTime.Now.AddYears(-1);
                Response.Cookies.Add(cookie);

                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account", null);
            }
        */
    }
}
