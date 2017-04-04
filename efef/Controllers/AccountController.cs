using efef.DAL;
using efef.Models;
using efef.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace efef.Controllers
{
    public class AccountController : Controller
    {
        private AccountContext db = new AccountContext();
        // GET: Account
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(db.SysUsers);
        }

        /// <summary>
        /// login
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            //ViewBag.LoginState = "before login";
            return View("IndexPage");
        }

        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string email = fc["inputEmail3"];
            string userPassword = fc["inputPassword3"];
            var user = db.SysUsers.Where(b => b.Email == email && b.Password == userPassword);
            if (user.Count() > 0)
            {
                ViewBag.LoginState = "email:" + email + "         password:" + userPassword + "     after login";
            }
            else
            {
                ViewBag.LoginState = "not exit this user";
            }

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            SysUser sysUser = db.SysUsers.Find(id);
            return View(sysUser);
        }


        public ActionResult Create()

        {
            return View();

        }

        [HttpPost]

        public ActionResult Create(SysUser sysUser)

        {

            //db.SysUsers.Add(sysUser);
            //db.SaveChanges();

            SysUserRepository.Singleton.Add(sysUser);
            return RedirectToAction("Index");

        }



        //修改用户

        public ActionResult Edit(int id)

        {

            SysUser sysUser = db.SysUsers.Find(id);

            return View(sysUser);

        }

        [HttpPost]

        public ActionResult Edit(SysUser sysUser)

        {

            db.Entry(sysUser).State = EntityState.Modified;

            db.SaveChanges();

            return RedirectToAction("Index");

        }



        //删除用户

        public ActionResult Delete(int id)

        {

            SysUser sysUser = db.SysUsers.Find(id);

            return View(sysUser);

        }

        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)

        {

            SysUser sysUser = db.SysUsers.Find(id);

            db.SysUsers.Remove(sysUser);

            db.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}