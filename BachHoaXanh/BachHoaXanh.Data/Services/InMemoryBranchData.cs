using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class InMemoryBranchData:IBranchData
    {
        List<Branch> branches;
        public InMemoryBranchData()
        {
            branches = new List<Branch>()
            {
                new Branch{Id="CN01N", Name="Chi nhanh Quan 9", AreaId="KV01",ManagerId="NV01",Address="Quan 9" }
            };
        }
        public void Add(Branch branch)
        {
            branches.Add(branch);
            branch.Id = branches.Max(r => r.Id) + 1;
        }
        public void Update(Branch branch)
        {
            var existing = Get(branch.Id);
            if (existing != null)
            {
                existing.Name = branch.Name;
                existing.AreaId = branch.AreaId;
                existing.ManagerId = branch.AreaId;
                existing.Address = branch.Address;
            }

        }

        public Branch Get(string id)
        {
            return branches.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Branch> GetAll()
        {
            return branches.OrderBy(r => r.Name);
        }

        public void Delete(string id)
        {
            var branch = Get(id);
            if (branch != null)
            {
                branches.Remove(branch);
            }

        }
    }
}
