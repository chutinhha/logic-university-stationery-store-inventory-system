using System;
using System.Web;
using System.ComponentModel;

namespace SA33.Team12.SSIS.DAL.DTO
{
    //Edit by Anthony 26 Jan 2011
    //This is for the Temp AdjustmentVoucherTransactions & StockLogTransactions
    public class AdjustmentVoucherTransactionSearchDTO
    {
        //For AdjustmentTransaction Table
        public int AdjustmentVoucherTransactionID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //For StockLogTransaction Table
        public int StockLogTransactionID { get; set; }
        //public int AdjustmentVoucherTransactionID { get; set; }
        public int StationeryID { get; set; }
        public int Type { get; set; }
        public String Reason { get; set; }
        public int Quantity { get; set; }
        public int Balance { get; set; }
    }
}
