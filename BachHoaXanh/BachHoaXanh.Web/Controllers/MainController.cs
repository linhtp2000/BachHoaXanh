using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BachHoaXanh.Web.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Milks()
        {
            return View();
        }
        public ActionResult Snacks()
        {
            return View();
        }
        public ActionResult Noodles()
        {
            return View();
        }
        public ActionResult Spices()
        {
            return View();
        }
        //public ActionResult Spices()
        //{
        //    return View();
        //}
    }
}