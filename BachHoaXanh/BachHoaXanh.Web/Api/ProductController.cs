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
        [HttpPut]
        public bool UpdateProduct(string id, string name, decimal price, int discount, int amount)
        {
            try
            {
                BachHoaXanhDbContext bhx = new BachHoaXanhDbContext();
                Product sp = bhx.Products.FirstOrDefault(x => x.Id == id);
                if (sp == null) return false;
                sp.Id = id;
                sp.Name = name;
                sp.Price = price;
                sp.Discount = discount;
                sp.Amount = amount;
                return true;
            }
            catch { return false; }
        }
    }
}
