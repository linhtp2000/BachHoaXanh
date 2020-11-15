using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class InMemoryProductData: IProductData
    {
        List<Product> products;
        public InMemoryProductData()
        {
            products = new List<Product>()
            {
                new Product{Id="SP01", Name="Sua", Price=10000,Discount=0,Amount=100,Details="",BranchId="CN01N",ProviderId="NCC01",ClassifyId="NH01",Image="Hinh01" }
            };
        }
        public void Add(Product product)
        {
            products.Add(product);
        }
        public void Update(Product product)
        {
            var existing = Get(product.Id);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Price = product.Price;
                existing.Discount = product.Discount;
                existing.Amount = product.Amount;
                existing.Details = product.Details;
                existing.BranchId = product.BranchId;
                existing.ProviderId = product.ProviderId;
                existing.ClassifyId = product.ClassifyId;
                existing.Image = product.Image;
            }
        }

        public Product Get(string id)
        {
            return products.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return products.OrderBy(r => r.Name);
        }

        public void Delete(string id)
        {
            var product = Get(id);
            if (product != null)
            {
                products.Remove(product);
            }

        }
    }
}
