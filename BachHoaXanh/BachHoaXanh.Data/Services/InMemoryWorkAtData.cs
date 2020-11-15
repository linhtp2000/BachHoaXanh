using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class InMemoryWorkAtData:IWorkAtData
    {
        List<WorkAt> workats;
        public InMemoryWorkAtData()
        {
            workats = new List<WorkAt>()
            {
                new WorkAt{EmployeeId="NV01", BranchId="CN01N",Position="Quan Ly"}
            };
        }
        public void Add(WorkAt workat)
        {
            workats.Add(workat);
        }
        public WorkAt GetWorkAt(string employee, string branch)
        {
            return workats.SingleOrDefault(r => (r.BranchId == branch&&r.EmployeeId==employee));
        }
        public WorkAt GetBranch(string id)
        {
            return workats.FirstOrDefault(r => r.BranchId == id);
        }

        public IEnumerable<WorkAt> GetAll()
        {
            return workats.OrderBy(r => r.BranchId);
        }
        public void Delete(string employee, string branch)
        {
            var workat = GetWorkAt(employee,branch);
            if (workat != null)
            {
                workats.Remove(workat);
            }

        }

        public void Update(WorkAt workat)
        {
            var existing = GetWorkAt(workat.EmployeeId, workat.BranchId);
            if (existing != null)
            {
                existing.Position = workat.Position;
            }
        }
    }
}
