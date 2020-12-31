using Autofac;
using BachHoaXanh.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using Autofac.Integration.WebApi;
//using Autofac.Integration.WebApi;

namespace BachHoaXanh.Web.App_Start
{
    public class ContainerConfig
    {
        internal static void RegisterContainer(HttpConfiguration httpConfiguration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
           builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<SqlAreaData>()
                   .As<IAreaData>()
                   .InstancePerRequest();
            builder.RegisterType<SqlCustomerData>()
                   .As<ICustomerData>()
                   .InstancePerRequest();
            builder.RegisterType<SqlCategoryData>()
                   .As<ICategoryData>()
                   .InstancePerRequest();
            builder.RegisterType<SqlUserData>()
                 .As<IUserData>()
                 .InstancePerRequest();
            builder.RegisterType<SqlEmployeeData>()
                  .As<IEmployeesData>()
                  .InstancePerRequest();
            builder.RegisterType<SqlBranchData>()
                   .As<IBranchData>()
                  .InstancePerRequest();
            builder.RegisterType<SqlClassifyData>()
                   .As<IClassifyData>()
                   .InstancePerRequest();
            builder.RegisterType<SqlProviderData>()
                   .As<IProvidersData>()
                   .InstancePerRequest();
            builder.RegisterType<SqlProductData>()
                  .As<IProductData>()
                  .InstancePerRequest();
            builder.RegisterType<SqlInvoicesData>()
                   .As<IInvoiceData>()
                    .InstancePerRequest();
            builder.RegisterType<SqlBillData>()
                   .As<IBillData>()
                   .InstancePerRequest();
            builder.RegisterType<SqlDetailsOfBillData>()
                  .As<IDetailsOfBillData>()
                  .InstancePerRequest();
            builder.RegisterType<SqlDetailsOfInvoiceData>()
                   .As<IDetailsOfInvoiceData>()
                   .InstancePerRequest();
            builder.RegisterType<SqlRatingData>()
                   .As<IRatingData>()
                   .InstancePerRequest();
            builder.RegisterType<SqlCartData>()
                   .As<ICartData>()
                   .InstancePerRequest();
            builder.RegisterType<SqlAuthorizeData>()
                   .As<IAuthorizeData>()
                   .InstancePerRequest();
            builder.RegisterType<SqlUser_AuthData>()
                   .As<IUser_AuthData>()
                   .InstancePerRequest();
            //builder.RegisterType<SqlWorkAtData>()
            //       .As<IWorkAtData>()
            //       .InstancePerRequest();
            builder.RegisterType<BachHoaXanhDbContext>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}