using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TINTOMTAT.Data;
using TINTOMTAT.Models.Auth;

namespace TINTOMTAT.Controllers
{
    public class AuthController : Controller
    {
        TinTomTatDbContext _connect = new TinTomTatDbContext();

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (Session["TaiKhoan"] != null)
            {
                return RedirectToAction("Index", "HomeAdmin");
            }

            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //var f_password = model.password; //GetMD5(password); bỏ qua md5 hardpark
                var user = _connect.Users.Where(s => s.TaiKhoan.Equals(model.TaiKhoan) && s.MatKhau.Equals(model.MatKhau)).ToList();
                if (user.Count() > 0)
                {
                    //add session
                    Session["TaiKhoan"] = user.FirstOrDefault().TaiKhoan;
                    Session["idUser"] = user.FirstOrDefault().Id;
                    if (String.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Index", "HomeAdmin");
                    }

                    return Redirect(returnUrl);
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("index", "HomeAdmin");
        }

        public ActionResult ChuaDangNhap()
        {
            return View();
        }
    }
}