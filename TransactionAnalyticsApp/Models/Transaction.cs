using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransactionAnalyticsApp.Models
{
    public enum Categories
    {
        None,
        Paycheck,
        AccountTransfer,
        PersonalCheck,
        Utilities,
        Groceries,
        Food,
        Entertainment,
        Home,
        Transportation,
        Subscription,
        Other
    }

    public class Transaction
    {
        public String Description { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public Enum Category { get; set; }
        // TODO: Add category to transactions
    }
}