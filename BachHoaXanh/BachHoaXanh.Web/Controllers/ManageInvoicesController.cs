using BachHoaXanh.Data.Models;
using BachHoaXanh.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BachHoaXanh.Web.Controllers
{
    public class ManageInvoicesController : Controller
    {
        BachHoaXanhDbContext bhx = new BachHoaXanhDbContext();
        // GET: NhapHang
        [HttpGet]
        public ActionResult NhapHang()
        {
            ViewBag.NCC = bhx.Providers;
            ViewBag.SP = bhx.Products;
            return View();
        }
        [HttpPost]
        public ActionResult NhapHang(IEnumerable<DetailsOfInvoice> Model)
        {
            return View();
        }
    }
}