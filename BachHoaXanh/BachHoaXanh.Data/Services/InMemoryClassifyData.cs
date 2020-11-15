using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class InMemoryClassifyData: IClassifyData
    {
        List<Classify> classifies;
        public InMemoryClassifyData()
        {
            classifies = new List<Classify>()
            {
                new Classify{Id="NH01", Name="Sua",CategoryId="DM01"},
            };
        }
        public void Add(Classify classify)
        {
           classifies.Add(classify);;
        }
        public void Update(Classify classify)
        {
            var existing = Get(classify.Id);
            if (existing != null)
            {
                existing.Name = classify.Name;
                existing.CategoryId = classify.CategoryId;
            }

        }

        public Classify Get(string id)
        {
            return classifies.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Classify> GetAll()
        {
            return classifies.OrderBy(r => r.Name);
        }

        public void Delete(string id)
        {
            var classify = Get(id);
            if (classify != null)
            {
                classifies.Remove(classify);
            }

        }
    }
}
