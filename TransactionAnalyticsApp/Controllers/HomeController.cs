using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TransactionAnalyticsApp.Models;
using TransactionAnalyticsApp.Models.ViewModels;

namespace TransactionAnalyticsApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        // TODO: Calculate the amount left after each transaction
        // TODO: Make it change pages (views) after upload
        // TODO: Add filtering for the table
        // TODO: Add useful charts and tables from data
        // TODO: Adjust csv reader to allow from different banks

        public ActionResult Index()
        {
            var model = new TransactionsViewModel();
            ViewBag.Message = "";

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            var model = new TransactionsViewModel();
            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/FileUploads"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);

                    model.Transactions = ConvertData(path);
                    model.TotalDepost = model.Transactions.Where(x => x.Amount > 0).Sum(x => x.Amount);
                    model.TotalSpent = model.Transactions.Where(x => x.Amount < 0).Sum(x => x.Amount);

                    System.IO.File.Delete(path);

                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    //ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    ViewBag.Message = "An error occured uploading the file.";
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }

            return View(model);
        }

        private double GetTotalDeposits(List<Transaction> transactions)
        {
            var positiveTransactions = transactions.Where(x => x.Amount > 0).ToList();
            double totalAmount = positiveTransactions.Sum(x => x.Amount);

            return totalAmount;
        }

        private List<Transaction> ConvertData(string path)
        {
            var transactions = new List<Transaction>();

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    //var csv = new CsvReader(reader);
                    //var records = csv.GetRecords<Transaction>()
                    // First Header
                    var line = reader.ReadLine();

                    // Beginning balance
                    line = reader.ReadLine();

                    // Total Credits
                    line = reader.ReadLine();

                    // Total Debits
                    line = reader.ReadLine();

                    // Ending Balance
                    line = reader.ReadLine();

                    // Space
                    line = reader.ReadLine();

                    // Transactions Header
                    line = reader.ReadLine();

                    // Beginning Balance
                    line = reader.ReadLine();

                    // Read Transactions
                    var csv = new CsvReader(reader);

                    while (csv.Read())
                    {
                        var transation = new Transaction();
                        transation.Date = DateTime.Parse(csv[0]);
                        transation.Description = csv[1];
                        string amount = csv[2].ToString();
                        transation.Amount = double.Parse(amount);
                        transactions.Add(transation);
                    }
                }
            }
            catch (Exception ex)
            {
                transactions = new List<Transaction>();
                ViewBag.Message = "An error occured processing the file.";
            }
            
            
            return transactions;
        }
    }
}