using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebTu.BLL;
using WebTu.Models;

namespace WebTu.Controllers
{
    public class AuthenticationController : Controller
    {
        //Get: Authentication
        public ActionResult Login()
        {
            return View(new UserDetails());
        }

        [HttpPost]
        public ActionResult DoLogin(UserDetails u)
        {
            if (ModelState.IsValid)
            {
                EmployeeBusinessLayer bal = new EmployeeBusinessLayer();

                var us = bal.GetUserValidity(u);

                bool IsAdmin = false;
                if(us == UserStatus.AuthenticatedAdmin)
                {
                    IsAdmin = true;
                }
                else if(us == UserStatus.AuthenticatedUser)
                {
                    IsAdmin = false;
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                    return View("Login");
                }

                FormsAuthentication.SetAuthCookie(u.UserName, false);
                Session["IsAdmin"] = IsAdmin;
                return RedirectToAction("Index", "Employee");

                //if (bal.IsValidUser(u))
                //{
                //    FormsAuthentication.SetAuthCookie(u.UserName, false);
                //    return RedirectToAction("Index", "Employee");
                //}
                //else
                //{
                //    ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                //    return View("Login");
                //}
            }
            else
            {
                return View("Login");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

	}
}