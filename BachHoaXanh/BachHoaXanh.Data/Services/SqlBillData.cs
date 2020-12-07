using BachHoaXanh.Data.ModelView;
using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BachHoaXanh.Data.Services
{
    public class SqlBillData: IBillData
    {
        private readonly BachHoaXanhDbContext db;

        string UserCurrentId { get; set; }
        public const string UserSessionKey = "CustomerId";
        public static SqlBillData GetCustomer(HttpContextBase context)
        {
            var bill = new SqlBillData();
             bill.UserCurrentId= bill.GetUserId(context);
            return bill;
        }
        public static SqlBillData GetCustomer(Controller controller)
        {
            return GetCustomer(controller.HttpContext);
        }       
        public void Add(Bill bill)
        {
            db.Bills.Add(bill);
            db.SaveChanges();

        }
        public void Delete(string id)
        {
            var bill = db.Bills.Find(id);
            db.Bills.Remove(bill);
            db.SaveChanges();
        }
        public int GetBillId()
        {
            var stringlist = (from bill in db.Bills
                      select bill.Id).ToList();
            //Doi BillId kieu string sang int
            List<int> intlist = stringlist.Select(s => int.Parse(s)).ToList();
            return intlist.Max();
        }

        public Bill Get(string id)
        {
            return db.Bills.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Models.Bill> GetAll()
        {
            return from r in db.Bills
                   orderby r.CustomerId
                   select r;
        }

        public void Update(Bill bill)
        {
            var entry = db.Entry(bill);   //provides information, ability to perform actions on the entity
            entry.State = EntityState.Modified;
            db.SaveChanges();

        }
        // We're using HttpContextBase to allow access to cookies.
        public string GetUserId(HttpContextBase context)
        {
            if (context.Session[UserSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[UserSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCusId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[UserSessionKey] = tempCusId.ToString();
                }
            }
            return context.Session[UserSessionKey].ToString();
        }
        //public string CreateOrder(Bill bill)
        //{
        //    decimal Total = 0;

        //    var cartItems = new Bill();
        //    // Iterate over the items in the cart, 
        //    // adding the order details for each
        //    foreach (var item in cartItems)
        //    {
        //        DetailsOfBill orderDetail = new DetailsOfBill()
        //        {
        //            Product = item.Product,
        //            ProductId = item.ProductId,
        //            CustomerId = User.identify.,
        //            BillId = item.,
        //            Price = product.Price,
        //            Total = product.Price * product.Amount * (100 - product.Discount) / 100,
        //            Image = product.Image1,
        //            Amount = 1,

        //        };
        //        // Set the order total of the shopping cart
        //        Total += item.Total;

        //        db.DetailsOfBills.Add(orderDetail);
        //    }
        //    // Set the order's total to the orderTotal count
        //    bill.Total = Total;

        //    // Save the order
        //    db.SaveChanges();
        //    // Empty the shopping cart
        //    EmptyCart();
        //    // Return the OrderId as the confirmation number
        //    return bill.Id;
        //}

    }
}
