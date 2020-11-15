using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class InMemoryAreaData: IAreaData
    {
        List<Area> areas;
        public InMemoryAreaData()
        {
            areas = new List<Area>()
            {
                new Area{Id="KV01", Name="Mien Nam" }
            };
        }
        public void Add(Area area)
        {
            areas.Add(area);           
        }
        public void Update(Area area)
        {
            var existing = Get(area.Id);
            if (existing != null)
            {
                existing.Name = area.Name;                
            }

        }

        public Area Get(string id)
        {
            return areas.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Area> GetAll()
        {
            return areas.OrderBy(r => r.Name);
        }

        public void Delete(string id)
        {
            var restaurant = Get(id);
            if (restaurant != null)
            {
                areas.Remove(restaurant);
            }

        }
    }
}
