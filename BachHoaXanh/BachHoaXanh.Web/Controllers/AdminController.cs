using BachHoaXanh.Data.Models;
using BachHoaXanh.Data.ModelView;
using BachHoaXanh.Data.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace BachHoaXanh.Web.Controllers
{
    //CHỖ CHO NGƯỜI QUẢN TRỊ VÀ QUẢN LÝ
    [Authorize(Roles = "QuanTri, QLDonHang, QLThongKe, QLNhanVien, QLSanPham")]
    public class AdminController : Controller
    {
        BachHoaXanhDbContext bhx = new BachHoaXanhDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListProduct()
        {
            var model = from sp in bhx.Products
                        select sp;
            return View(model);
        }

        [HttpGet]
        public ActionResult ProductDetails(string id)
        {
            //var model = db.Get(id);
            var model = (from sp in bhx.Products
                         where sp.Id == id
                         select sp).FirstOrDefault();
            if (model == null)
            {
                return View("Error");
            }
            return View(model);
        }
    }
}