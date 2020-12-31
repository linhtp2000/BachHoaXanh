using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BachHoaXanh.Data.Models;
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
        //public ActionResult SignIn(FormCollection f)//; Data.Models.Login account)
        //{

        //    //if (String.IsNullOrEmpty(account.Email))
        //    //{
        //    //    ModelState.AddModelError(nameof(account.Email), "Email is required");      //yeu cau nhap email
        //    //}
        //    //if (String.IsNullOrEmpty(account.PassWord))
        //    //{
        //    //    ModelState.AddModelError(nameof(account.PassWord), "Password is required");      //yeu cau nhap email
        //    //}

        //    // FormCollection f = new FormCollection();
        //    string sodienthoai = f["SDT"];
        //    string matkhau = f["password"];
        //    Data.Models.Customer us = bhx.Customers.SingleOrDefault(n => n.PhoneNumber == sodienthoai && n.Password == matkhau);
        //    if (us != null)
        //    {
        //        Session["Name"] = us.Name;
        //        Session["Email"] = us.Email;
        //        Session["ID"] = us.Id;
        //        //  Session["Cart"] = us.Id;
        //        return Content("/Products/Index");
        //    }
        //    else
        //    {
        //        var us1 = (from u in bhx.Employees
        //                   where u.Password == matkhau && u.PhoneNumber == sodienthoai
        //                   select u).SingleOrDefault();// FirstOrDefault();
        //        if (us1 != null)
        //            return Content("/Products/Drinks");
        //    }
        //    return Content("false");

        //    //   return View();
        //}

        public ActionResult SignIn(FormCollection f)//; Data.Models.Login account)
        {
            string taikhoan = f["SDT"];
            string matkhau = f["password"];

            var us = bhx.Customers.SingleOrDefault(n => n.PhoneNumber == taikhoan && n.Password == matkhau);
            if (us != null)
            {
                Session["Name"] = us.Name;
                Session["ID"] = us.Id;
                var lstQuyen = bhx.User_Authorizes.Where(n => n.UserId == us.UserId);
                string Quyen = "";
                foreach (var item in lstQuyen)
                {
                    Quyen += item.AuthorizeId + ",";
                }
                Quyen = Quyen.Substring(0, Quyen.Length - 1);
                PhanQuyen(us.PhoneNumber.ToString(), Quyen);
                return Content("/Main/Index");
            }
            else
            {

                var us2 = bhx.Employees.SingleOrDefault(n => n.PhoneNumber == taikhoan && n.Password == matkhau);
                if (us2 != null)
                {
                    Session["ID"] = us2.Id;
                    Session["Name"] = us2.Name;
                    var lstQuyen = bhx.User_Authorizes.Where(n => n.UserId == us2.UserId);
                    string Quyen = "";
                    foreach (var item in lstQuyen)
                    {
                        Quyen += item.AuthorizeId + ",";
                    }
                    Quyen = Quyen.Substring(0, Quyen.Length - 1);
                    PhanQuyen(us2.PhoneNumber.ToString(), Quyen);
                    return Content("/Admin/Index");
                }

            }
            return Content("false");

            //   return View();
        }

        [HttpGet]
        public ActionResult LoginSuccessfully()
        {

            return View();
        }
        //public ActionResult Logout()
        //{
        //    Session.Clear();//remove session
        //    return RedirectToAction("Index", "Main");
        //}


        //Khong du quyen truy cap
        public ActionResult LoiPhanQuyen()
        {
            return PartialView("~/Views/Shared/Error.cshtml");
        }
        public ActionResult LogOut()
        {
            Session.Clear();//remove session
            //Session["ID"] = null;
            //FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Main");
        }

        public void PhanQuyen(string TaiKhoan, string Quyen)
        {
            FormsAuthentication.Initialize();

            var ticket = new FormsAuthenticationTicket(1,
                                                TaiKhoan,   //user
                                                DateTime.Now,//begin
                                                DateTime.Now.AddHours(24),//timeout
                                                false,  //rememner
                                                Quyen, //permissons
                                                FormsAuthentication.FormsCookiePath);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));  //ma hoa cookie

            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

            Response.Cookies.Add(cookie);

        }
        public string RandomString(int size)
        {
            Random rand = new Random();
            StringBuilder sb = new StringBuilder();
            char c;
            for (int i = 0; i < size; i++)
            {
                c = Convert.ToChar(Convert.ToInt32(rand.Next(97, 122)));
                sb.Append(c);//thêm kí tự vào string
            }
            return sb.ToString();
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(string Email)
        {
            Email = Request.Form["email"];


            string newpass = RandomString(10); //null;
            var model = (from kh in bhx.Customers
                         where kh.Email == Email
                         select kh).FirstOrDefault();
            if (model != null)
            {

                var senderEmail = new MailAddress("yukihana1808@gmail.com", "Bach Hoa Xanh Grocery");
                var receiverEmail = new MailAddress(Email);
                var password = "***********";  //***********  SẼ ĐIỀN KHI BÁO CÁO
                var subject = "New password for Bach Hoa Xanh";
                var body = "Chào " + model.Name + ", có phải bạn đã quên mật khẩu không?" +
                    "Nếu có, thì đây là mật khẩu mới của bạn:  " + newpass + ". Mật khẩu này chỉ là tạm thời. Bạn có thể đặt lại mật khẩu mới theo ý bạn ở phần chỉnh sửa thông tin trong website";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
                model.Password = newpass;
                bhx.SaveChanges();
                ViewBag.message = "Hãy kiểm tra hòm thư của bạn";
                smtp.Dispose();

            }
            else
            {
                //ViewBag.message = "Bạn hãy kiểm tra lại địa chỉ email!";
                return Content("Địa chỉ email không hợp lệ");

            }
            return View(model);
        }

    }
}