using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class InMemoryCategoryData: ICategoryData
    {
        List<Category> catalogues;
        public InMemoryCategoryData()
        {
            catalogues = new List<Category>()
            {
                new Category{Id="DM01", Name="Tieu dung" }                
            };
        }
        public void Add(Category catalogue)
        {
            catalogues.Add(catalogue);
        }
        public void Update(Category catalogue)
        {
            var existing = Get(catalogue.Id);
            if (existing != null)
            {
                existing.Id = catalogue.Id;
                existing.Name = catalogue.Name;
            }
        }

        public Category Get(string id)
        {
            return catalogues.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Category> GetAll()
        {
            return catalogues.OrderBy(r => r.Name);
        }

        public void Delete(string id)
        {
            var catalogue = Get(id);
            if (catalogue != null)
            {
                catalogues.Remove(catalogue);
            }

        }
    }
}
