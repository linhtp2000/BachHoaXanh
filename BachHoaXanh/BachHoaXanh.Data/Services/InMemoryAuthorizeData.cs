using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class InMemoryAuthorizeData: IAuthorizeData
    {
        List<Authorize> authorizes;
        public InMemoryAuthorizeData()
        {
            authorizes = new List<Authorize>()
            {
                new Authorize{Id="1", Name="User" }
            };
        }
        public void Add(Authorize authorize)
        {
           authorizes.Add(authorize);
        }
        public void Update(Authorize authorize)
        {
            var existing = Get(authorize.Id);
            if (existing != null)
            {
                existing.Name = authorize.Name;
            }

        }

        public Authorize Get(string id)
        {
            return authorizes.FirstOrDefault(r => r.Id == id);
        }

        //public IEnumerable<Area> GetAll()
        //{
        //    return areas.OrderBy(r => r.Name);
        //}

        public void Delete(string id)
        {
            var authorize = Get(id);
            if (authorize != null)
            {
                authorizes.Remove(authorize);
            }

        }
    }
}
