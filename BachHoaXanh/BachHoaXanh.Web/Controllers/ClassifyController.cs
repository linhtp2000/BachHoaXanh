using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BachHoaXanh.Data.Services;
using BachHoaXanh.Data.Models;
namespace BachHoaXanh.Web.Controllers
{
    public class ClassifyController : Controller
    {
        private BachHoaXanhDbContext bhx = new BachHoaXanhDbContext();
        //  private readonly IClassifyData db;
        //public ClassifyController(IClassifyData db)
        //{
        //    this.db = db;
        //}
        // GET: Classify

       // [HttpGet]
        //public ActionResult Category()
        //{
        //    var model = from category in bhx.Categories
        //                orderby category.Name
        //                select category;/*).OrderBy(s => s.Name);*/
        //    //var model1 = from dm in bhx.Categories
        //    //            join nh in bhx.Classifys
        //    //            on dm.Id equals nh.CategoryId
        //    //            select new { Cate = dm.Name, Classify = nh.Name };
        //    return PartialView("_Category", model);
        //}
        //public ActionResult Classify(string CategoryID)
        //{
        //    var model = from classify in bhx.Classifys
        //                where classify.CategoryId == CategoryID
        //                select classify;

        //    ViewBag.classify = model;
        //    if (model != null)
        //        return PartialView("Classify", model);
        //    else
        //        return PartialView("Classify", model);
        //}
    }
}