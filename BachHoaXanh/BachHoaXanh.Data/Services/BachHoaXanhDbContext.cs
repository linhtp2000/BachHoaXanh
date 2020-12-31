using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public partial class BachHoaXanhDbContext: DbContext
    {
        //public BachHoaXanhDbContext() : base("BachHoaXanhDbContext")
        //{
        //    var initializer = new MigrateDatabaseToLatestVersion<BachHoaXanhDbContext, Migrations.Configuration>();
        //    Database.SetInitializer(initializer);
        //}
        public DbSet<Area> Areas { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<Classify> Classifys { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<DetailsOfBill> DetailsOfBills { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<DetailsOfInvoice> DetailsOfInvoices { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Rating> Ratings { get; set; }
      
        public DbSet<Authorize> Authorizes { get; set; }
        public DbSet<User_Authorize> User_Authorizes { get; set; }
        //public DbSet<WorkAt> WorkAts { get; set; }
    }
}
