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
    [Authorize(Roles = "QuanTri, QLKhachHang, QLDonHang, XemKH")]
    public class CustomerController : Controller
    {
        // GET: Customer
        BachHoaXanhDbContext bhx = new BachHoaXanhDbContext();
        private readonly ICustomerData db;
        public CustomerController(ICustomerData db)
        {
            this.db = db;
        }
        [Authorize(Roles = "QuanTri, QLKhachHang, QLDonHang")]
        public ActionResult ListCustomer(int? page)
        {

            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
            var kh = db.GetAll().OrderBy(x => x.Id);
            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 4;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.
            return View(kh.ToPagedList(pageNumber, pageSize));
        }
        [Authorize(Roles = "QuanTri, QLKhachHang, QLDonHang")]
        [HttpGet]
        public ActionResult Details(string id)
        {
            var model = db.Get(id);
            if (model == null)
            {
                return View("Error");
            }
            return View(model);
        }
        [Authorize(Roles = "QuanTri, QLKhachHang, QLDonHang")]
        public ActionResult Top20Customers()
        {
            var model = (from bill in bhx.Bills
                         join kh in bhx.Customers on bill.CustomerId equals kh.Id
                         group bill by new { kh.Id, kh.Name } into khach
                         select new { id = khach.Key.Id, Name = khach.Key.Name, AmountSum = khach.Sum(bill => bill.Total) }).OrderByDescending(x => x.AmountSum);
            List<Data.ModelView.Top20Customers> top20 = new List<Top20Customers>();
            int i = 1;
            foreach (var kh in model)
            {


                Top20Customers khachhang = new Top20Customers();
                khachhang.Id = kh.id.ToString();
                khachhang.Name = kh.Name;
                khachhang.Total = kh.AmountSum;              //tổng sản phẩm đã mua
                khachhang.No = i; // số thứ tự từ 1->20
                top20.Add(khachhang);
                if (i == 20) break;
                i++;
            }
            return View(top20);
        }
    }
}