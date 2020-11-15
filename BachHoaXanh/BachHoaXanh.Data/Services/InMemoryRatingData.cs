using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class InMemoryRatingDataL:IRatingData
    {
        List<Rating> ratings;
        public InMemoryRatingDataL()
        {
            ratings = new List<Rating>()
            {
                new Rating{ProductId="SP01",CustomerId="KH01", Rate=5, Comments="Tot"}
            };
        }
        public void Add(Rating rating)
        {
            ratings.Add(rating);
        }
        public void Update(Rating rating)
        {
            var existing = Get(rating.ProductId);
            if (existing != null)
            {
                existing.Comments = rating.Comments;
                existing.Rate = rating.Rate;            
            }

        }
        public Rating Get(string id)
        {
            return ratings.FirstOrDefault(r => r.ProductId== id);
        }

        public IEnumerable<Rating> GetAll()
        {
            return ratings.OrderBy(r => r.Rate);
        }

        public void Delete(string id)
        {
            var rating= Get(id);
            if (rating != null)
            {
                ratings.Remove(rating);
            }

        }
    }
}
