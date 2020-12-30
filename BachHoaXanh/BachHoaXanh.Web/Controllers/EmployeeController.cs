using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BachHoaXanh.Data.Services;
using BachHoaXanh.Data.Models;
using PagedList;

namespace BachHoaXanh.Web.Controllers
{
    [Authorize(Roles = "QuanTri, QLNhanVien")]
    public class EmployeeController : Controller
    {
        public BachHoaXanhDbContext bhx = new BachHoaXanhDbContext();
        // GET: Admin
        private readonly IEmployeesData db;
        public EmployeeController(IEmployeesData db)
        {
            this.db = db;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListEmployee(int? page)
        {

            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.

            var nv = (from nhanvien in bhx.Employees
                      where nhanvien.ManagerId != null //CHƯA HIỆN ĐƯỢC NHÂN VIÊN TƯƠNG ỨNG VỚI NG QUẢN LÝ
                      select nhanvien).OrderBy(x => x.Id);

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 4;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.
            return View(nv.ToPagedList(pageNumber, pageSize));



            // return View(nv);
        }
        [HttpGet]
        public ActionResult Details(string id)
        {
            var model = db.Get(id);
            if (model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        [HttpGet]   // use to edit, create 
        public ActionResult Create()
        {
            User user = new User();
            ViewBag.UserId = new SelectList(bhx.Users, "Id", "Name", user.Id);
            return View();
        }

        [HttpPost]  //use to post, write 
        [ValidateAntiForgeryToken] //thuoc tinh ngan chan mot yeu cau gia mao
        public ActionResult Create(Employee employee)
        {
            if (String.IsNullOrEmpty(employee.Id))
            {
                ModelState.AddModelError(nameof(employee.Id), "Employee's ID is required");      //thong bao loi khi Id co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(employee.Name))
            {
                ModelState.AddModelError(nameof(employee.Name), "The name is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(employee.ManagerId))
            {
                ModelState.AddModelError(nameof(employee.ManagerId), "ManagerId is required");      //thong bao loi khi ManagerId co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(employee.Salary.ToString()))
            {
                ModelState.AddModelError(nameof(employee.Salary), "Salary is required");      //thong bao loi khi Salary co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(employee.WorkAts.ToString()))
            {
                ModelState.AddModelError(nameof(employee.WorkAts), "Branch's ID is required");      //thong bao loi khi BranchId co gia tri null/ rong
            }

            if (ModelState.IsValid)
            {
                db.Add(employee);
                return RedirectToAction("Details", new { id = employee.Id });
            }

            return View();
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            var model = db.Get(id);
            if (model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(string id)
        {
            db.Delete(id);
            return RedirectToAction("ListEmployee");
        }


        [HttpGet]
        public ActionResult Edit(string id)
        {
            var model = db.Get(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            User user = new User();
            ViewBag.UserId = new SelectList(bhx.Users, "Id", "Name", user.Id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Update(employee);
                TempData["Message"] = "You have saved the employee!";
                return RedirectToAction("Details", new { id = employee.Id });
            }
            return View(employee);
        }
    }
}