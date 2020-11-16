using BachHoaXanh.Data.Models;
using BachHoaXanh.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BachHoaXanh.Web.Api
{
    public class ProductController : ApiController
    {
        private readonly IProductData db;
            public ProductController(IProductData db)
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
