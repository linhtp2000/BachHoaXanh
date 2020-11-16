using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using BachHoaXanh.Data.Services;

namespace BachHoaXanh.Data.Migrations
{
    internal sealed class Configration:DbMigrationsConfiguration<BachHoaXanhDbContext>
    {
        public Configration()
        {
           AutomaticMigrationsEnabled = true;
           AutomaticMigrationDataLossAllowed = true;           
        }
        protected override void Seed(BachHoaXanhDbContext context)
        {
            //base.Seed(context);
        }
    }
}
