using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC5Course.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        // GET: Member
        
        
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel Login)
        {
            if (CheckLogin(Login.Email, Login.Password))
            {
                FormsAuthentication.RedirectFromLoginPage(Login.Email, false);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("Password", "您輸入的帳號或密碼錯誤");
            return View();
        }

        private bool CheckLogin(string email, string password)
        {
            return email == "e4366.tw@gmail.com" && password == "1234";
        }
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }
    }
}