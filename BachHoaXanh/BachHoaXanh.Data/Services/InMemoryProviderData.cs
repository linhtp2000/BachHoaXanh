using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class InMemoryProviderData: IProvidersData
    {
        List<Provider> providers;
        public InMemoryProviderData()
        {
            providers = new List<Provider>()
            {
                new Provider{Id="NCC01", Name="Vinamilk", PhoneNumber="01234567",Address="Quan 9" }
            };
        }
        public void Add(Provider provider)
        {
            providers.Add(provider);
        }
        public void Update(Provider provider)
        {
            var existing = Get(provider.Id);
            if (existing != null)
            {
                existing.Id = provider.Id;
                existing.Name = provider.Name;
                existing.PhoneNumber = provider.PhoneNumber;
                existing.Address = provider.Address;
            }

        }
        public Provider Get(string id)
        {
            return providers.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Provider> GetAll()
        {
            return providers.OrderBy(r => r.Name);
        }

        public void Delete(string id)
        {
            var provider = Get(id);
            if (provider != null)
            {
                providers.Remove(provider);
            }

        }
    }
}
