using BachHoaXanh.Data.Models;
using BachHoaXanh.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BachHoaXanh.Web.Api
{
    public class ProcductsController : Controller
    {
        // GET: Procducts
        private readonly IProductData db;
        public ProcductsController(IProductData db)
        {
            this.db = db;
        }
        public IEnumerable<Product> Get()
        {
            var model = db.GetAll();
            return model;
        }
    }
}