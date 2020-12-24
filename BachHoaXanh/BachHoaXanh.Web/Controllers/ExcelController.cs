using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BachHoaXanh.Data.ModelView;
using BachHoaXanh.Data.Models;
using BachHoaXanh.Data.Services;
using BachHoaXanh.Data;
using System.Data;
using System.Text;
using System.IO;

using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;

namespace BachHoaXanh.Web.Controllers
{
    public class ExcelController : Controller
    {
        private BachHoaXanhDbContext db = new BachHoaXanhDbContext();

        public object ExcelReaderFactory { get; private set; }


        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            string filePath = string.Empty;
            if (file != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                file.SaveAs(filePath);

                string conString = string.Empty;

                switch (extension)
                {
                    //case ".xls": //Excel 97-03.
                    //    conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    //    break;
                    //case ".xlsx": //Excel 07 and above.
                    //    conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    //    break;


                    //case ".xls": //Excel 97-03.
                    //    conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=(localdb)/MSSQLLocalDB;Extended Properties='Excel 8.0;HDR=YES'";
                    //    break;
                    //case ".xlsx": //Excel 07 and above.     cái cũ Microsoft.ACE.OLEDB.12.0
                    //    conString = "Provider=Microsoft.ACE.OLEDB.15.0;Data Source=(localdb)/MSSQLLocalDB;Extended Properties='Excel 8.0;HDR=YES'";
                    //    break;

                    case ".xls": //Excel 97-03.
                        conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES',providerName=System.Data.OleDb ";
                        break;
                    case ".xlsx": //Excel 07 and above.
                        conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES', providerName=System.Data.OleDb";
                        break;
                }

                DataTable dt = new DataTable();
                conString = string.Format(conString, filePath);

                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;

                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();

                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }
                // conString = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;

                conString = @"Server=(localdb)\MSSQLLocalDB;Database=BachHoaXanhMVC;Trusted_Connection=True; providerName=ystem.Data.OleDb";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.Areas";

                        // Map the Excel columns with that of the database table, this is optional but good if you do
                        // 
                        sqlBulkCopy.ColumnMappings.Add("Id", "Id");
                        sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                        //sqlBulkCopy.ColumnMappings.Add("Price", "Price");
                        //sqlBulkCopy.ColumnMappings.Add("Discount", "Discount");
                        //sqlBulkCopy.ColumnMappings.Add("Amount", "Amount");
                        //sqlBulkCopy.ColumnMappings.Add("Details", "Details");
                        //sqlBulkCopy.ColumnMappings.Add("ClassifyId", "ClassifyId");
                        //sqlBulkCopy.ColumnMappings.Add("BrachId", "BrachId");
                        //sqlBulkCopy.ColumnMappings.Add("ProviderId", "ProviderId");
                        //sqlBulkCopy.ColumnMappings.Add("Image1", "Image1");
                        //sqlBulkCopy.ColumnMappings.Add("Image2", "Image2");
                        //sqlBulkCopy.ColumnMappings.Add("Image3", "Image3");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }
            //if the code reach here means everthing goes fine and excel data is imported into database
            ViewBag.Success = "File Imported and excel data saved into database";

            return View();
        }


    }
}

