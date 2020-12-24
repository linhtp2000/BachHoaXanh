using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BachHoaXanh.Data.Services;


namespace BachHoaXanh.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public BachHoaXanhDbContext bhx = new BachHoaXanhDbContext();
        private readonly ILoginData db;

        [HttpGet]
        public ActionResult SignIn()
        {

            return View();
        }
        [HttpPost]
        public ActionResult SignIn(FormCollection f)//; Data.Models.Login account)
        {

            //if (String.IsNullOrEmpty(account.Email))
            //{
            //    ModelState.AddModelError(nameof(account.Email), "Email is required");      //yeu cau nhap email
            //}
            //if (String.IsNullOrEmpty(account.PassWord))
            //{
            //    ModelState.AddModelError(nameof(account.PassWord), "Password is required");      //yeu cau nhap email
            //}

            // FormCollection f = new FormCollection();
            string sodienthoai = f["SDT"];
            string matkhau = f["password"];
            Data.Models.Customer us = bhx.Customers.SingleOrDefault(n => n.PhoneNumber == sodienthoai && n.Password == matkhau);
            if (us != null)
            {
                Session["Name"] = us.Name;
                Session["Email"] = us.Email;
                return Content("/Main/Index");
            }
            else
            {
                var us1 = (from u in bhx.Employees
                           where u.Password == matkhau && u.PhoneNumber == sodienthoai
                           select u).SingleOrDefault();// FirstOrDefault();
                if (us1 != null)
                    return Content("/Products/Drinks");
            }
            return Content("false");

            //   return View();

        }


        [HttpGet]
        public ActionResult LoginSuccessfully()
        {

            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index", "Main");
        }


    }
}