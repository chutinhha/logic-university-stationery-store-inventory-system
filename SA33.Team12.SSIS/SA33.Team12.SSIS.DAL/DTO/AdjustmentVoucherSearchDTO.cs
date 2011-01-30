using System;
using System.Web;
using System.ComponentModel;

namespace SA33.Team12.SSIS.DAL.DTO
{
    //Edit by Anthony 30 Jan 2011
    //This is for the Actual AdjustmentVouchers & StockLogs
    public class AdjustmentVoucherSearchDTO
    {
        public int AdjustmentVoucherID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //For StockLog Table
        public int StockLogID { get; set; }
        //public int AdjustmentVoucherID { get; set; }
        public int StationeryID { get; set; }
        public int Type { get; set; }
        public String Reason { get; set; }
        public int Quantity { get; set; }
        public int Balance { get; set; }
    }
}
