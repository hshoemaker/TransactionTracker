using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransactionAnalyticsApp.Models.ViewModels
{
    public class TransactionsViewModel
    {
        // TODO: Gather more info about transactions
        public List<Transaction> Transactions { get; set; }
        public double TotalDepost { get; set; }
        public double TotalSpent { get; set; }

        public TransactionsViewModel()
        {
            Transactions = new List<Transaction>();
        }
    }
}