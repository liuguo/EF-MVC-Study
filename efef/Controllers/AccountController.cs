using efef.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace efef.Controllers
{
    public class AccountController : Controller
    {
        private AccountContext db = new AccountContext();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.LoginState = "before login";
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string email = fc["inputEmail3"];
            string userPassword = fc["inputPassword3"];
            var user = db.SysUsers.Where(b=>b.Email== email&& b.Password== userPassword);
            if (user.Count() > 0)
            {
                ViewBag.LoginState = "email:" + email + "         password:" + userPassword + "     after login";
            }
            else
            {
                ViewBag.LoginState ="not exit this user";
            }
           
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}