using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BachHoaXanh.Data.Models;
using BachHoaXanh.Data.Services;

namespace BachHoaXanh.Web.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public BachHoaXanhDbContext bhx = new BachHoaXanhDbContext();
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(FormCollection model)//(Register model)
        {
            //FormCollection dk = new FormCollection();
            /* BachHoaXanh.Data.Models.Login tk = new BachHoaXanh.Data.Models.Login();*/ //Request.Form;   
                                                                                         //string password = model.MatKhau;
                                                                                         //string youremail = model.Email;
                                                                                         //string phone = model.SDT;
                                                                                         //string name = model.HoTen;
                                                                                         //string retypepassword = model.NhapLaiMatKhau;
                                                                                         //m.SDT = model.SDT;
                                                                                         //m.Email = model.Email;
                                                                                         //m.MatKhau = model.MatKhau;
            string password = model["password1"];
            string youremail = model["email"];
            string phone = model["SDT1"];
            string name = model["name"];
            string retypepassword = model["retypepassword"];
            Customer kh = new Customer();
            kh.Name = name;
            kh.PhoneNumber = phone;
            kh.Password = password;
            kh.Email = youremail;
            var u = (from id in bhx.Customers
                     select id.Id).ToList().Last();
            int IdKH = Convert.ToInt32(u) + 1;
            kh.Id = IdKH.ToString();
            //    tk. = phone;
            //   tk.Email = youremail;
            //   tk.MatKhau = password;
            if (ModelState.IsValid)
            {
                bhx.Configuration.ValidateOnSaveEnabled = false;
                // bhx.Logins.Add(m);

                bhx.Customers.Add(kh);
                bhx.SaveChanges();
                //return View("DangNhap");
                return RedirectToAction("SignIn", "Login");
            }
            else return View(model);
            // return View();  */
        }
        public ActionResult SignUpForEmp()  //Khúc này chưa test
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUpForEmp(FormCollection model)//Khúc này chưa test
        {
            string password = model["password1"];
            string youremail = model["email"];
            string phone = model["SDT1"];
            string name = model["name"];
            string retypepassword = model["retypepassword"];
            Employee nv = new Employee();
            nv.Name = name;
            nv.PhoneNumber = phone;
            nv.Password = password;
            nv.Email = youremail;
            var u = (from id in bhx.Employees
                     select id.Id).ToList().Last();
            int IdNV = Convert.ToInt32(u) + 1;
            nv.Id = IdNV.ToString();

            if (ModelState.IsValid)
            {
                bhx.Configuration.ValidateOnSaveEnabled = false;
                // bhx.Logins.Add(m);

                bhx.Employees.Add(nv);
                bhx.SaveChanges();
                //return View("DangNhap");
                return RedirectToAction("SignIn", "Login");
            }
            else return View(model);
            // return View();  */
        }
    }
}