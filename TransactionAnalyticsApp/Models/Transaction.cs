using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransactionAnalyticsApp.Models
{
    public class Transaction
    {
        public String Description { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        // TODO: Add category to transactions
    }
}