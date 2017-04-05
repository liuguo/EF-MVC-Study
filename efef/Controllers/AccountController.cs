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
        public static int Total = 10; 
        public static int TotalPage = 10;
        public static int pageSize = 5;


        //public JsonResult SearchString(string searchString)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    var useraa = from des in db.SysUsers where (des.Email.Contains(searchString)) select des;
        //    return Json(useraa, JsonRequestBehavior.AllowGet);
        //    //var useraa = from des in db.SysUsers where (des.Email.Contains(searchString)) select des ;
        //    //return Json(useraa, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult SearchString(string searchString, int? id, string sortOrder)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ViewBag.PageIndex = id;
            ViewBag.Total = Total;
            ViewBag.TotalPage = TotalPage;
            int pageIndex = 1;
            if (id.HasValue)
            {
                pageIndex = id.Value;
            }
            if (string.IsNullOrEmpty(searchString))
            {
                ViewBag.NameSortParm = string.IsNullOrWhiteSpace(sortOrder) ? "name_desc" : "";
                var userdata = ViewBag.NameSortParm == "" ? db.SysUsers.OrderBy(u => u.UserName).Skip((pageIndex - 1) * pageSize).Take(pageSize) : db.SysUsers.OrderByDescending(u => u.UserName).Skip((pageIndex - 1) * pageSize).Take(pageSize);

                return Json(
                    new { pageIndex = pageIndex, NameSortParm = string.IsNullOrWhiteSpace(sortOrder) ? "name_desc" : "", data = userdata }
                    , JsonRequestBehavior.AllowGet);
            }
            var useraa = from des in db.SysUsers where (des.Email.Contains(searchString)) select des;
            var user = ViewBag.NameSortParm == "" ? useraa.OrderBy(u => u.UserName).Skip((pageIndex - 1) * pageSize).Take(pageSize) : useraa.OrderByDescending(u => u.UserName).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return Json(new { pageIndex= pageIndex, NameSortParm = string.IsNullOrWhiteSpace(sortOrder) ? "name_desc" : "", data = user }, JsonRequestBehavior.AllowGet);
            //db.Configuration.ProxyCreationEnabled = false;
            //ViewBag.PageIndex = id;
            //ViewBag.Total = Total;
            //ViewBag.TotalPage = TotalPage;
            //ViewBag.NameSortParm = string.IsNullOrWhiteSpace(sortOrder) ? "name_desc" : "";

            //var useraa = from des in db.SysUsers where (des.Email.Contains(searchString)) select des;
            //var user = ViewBag.NameSortParm == "" ? useraa.OrderBy(u => u.UserName).Skip((id - 1) * pageSize).Take(pageSize) : useraa.OrderByDescending(u => u.UserName).Skip((id - 1) * pageSize).Take(pageSize);
            //return Json(user, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Sort (string searchString, string sortOrder, int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ViewBag.PageIndex = id;
            ViewBag.Total = Total;
            ViewBag.TotalPage = TotalPage;
            if (string.IsNullOrEmpty(searchString))
            {
                ViewBag.NameSortParm = string.IsNullOrWhiteSpace(sortOrder) ? "name_desc" : "";
                var userdata = ViewBag.NameSortParm == "" ? db.SysUsers.OrderBy(u => u.UserName).Skip((id - 1) * pageSize).Take(pageSize) : db.SysUsers.OrderByDescending(u => u.UserName).Skip((id - 1) * pageSize).Take(pageSize);

                return Json(
                    new { NameSortParm = string.IsNullOrWhiteSpace(sortOrder) ? "name_desc" : "", data = userdata }
                    , JsonRequestBehavior.AllowGet);
            }
            var useraa = from des in db.SysUsers where (des.Email.Contains(searchString)) select des;
            var user = ViewBag.NameSortParm == "" ? useraa.OrderBy(u => u.UserName).Skip((id - 1) * pageSize).Take(pageSize) : useraa.OrderByDescending(u => u.UserName).Skip((id - 1) * pageSize).Take(pageSize);
            return Json(new { NameSortParm= string.IsNullOrWhiteSpace(sortOrder) ? "name_desc" : "",data = user }, JsonRequestBehavior.AllowGet);

        }
        // GET: Account
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.PageIndex = 1;
            ViewBag.Total = Total;
            ViewBag.TotalPage = TotalPage;
 
            int i = 1;
            var user = db.SysUsers.OrderBy(u => u.UserName).Skip((i - 1) * pageSize).Take(pageSize);
            return View(user);
        }

        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public ActionResult fenye(int i)
        {
            if (i > TotalPage || i < 1)
            {
                i = 1;
            }
            ViewBag.PageIndex = i;
            ViewBag.Total = Total;
            ViewBag.TotalPage = TotalPage;
  
            var user = db.SysUsers.OrderBy(u => u.UserName).Skip((i - 1) * pageSize).Take(pageSize);
            return View("Index", user);
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