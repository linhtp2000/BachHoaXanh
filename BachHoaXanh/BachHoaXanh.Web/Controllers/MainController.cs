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

    public class MainController : Controller
    {
        // GET: Main
        BachHoaXanhDbContext bhx = new BachHoaXanhDbContext();
        public ActionResult Index(int? page, string tieuchuanloc)
        {
            //tieuchuanloc = Request.Form["tieuchuanloc"];
            //ViewBag.PriceSortParm = tieuchuanloc;

            ViewBag.PriceSortParm = String.IsNullOrEmpty(tieuchuanloc) ? "giagiam" : "";
            //if (String.IsNullOrEmpty(tieuchuanloc))
            //{
            //    ViewBag.NameSortParm = "giagiam";
            //}
            //else
            //{
            //    ViewBag.NameSortParm = "";
            //}

            //chọn tất cả sản phẩm
            var model = (from sp in bhx.Products
                         select sp);//.OrderByDescending(sp=>sp.Price);

            // if (model.FirstOrDefault() == null) return Content("Sản phẩm sẽ được thêm vào sớm, xin lỗi quý khách vì sự bất tiện này.");
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            //tieuchuanloc = "giagiam" ;
            //tieuchuanloc = Request.Form["tieuchuanloc"];
            //ViewBag.PriceSortParm = tieuchuanloc;
            switch (tieuchuanloc)
            {
                case "giagiam":
                    ViewBag.tieuchuan = "giá giảm dần";
                    model = model.OrderByDescending(sp => sp.Price);
                    break;
                default:
                    model = model.OrderBy(s => s.Price);
                    ViewBag.tieuchuan = "giá tăng dần";
                    break;
            }
            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 4;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.
            return View(model.ToPagedList(pageNumber, pageSize));
        }

    }
}