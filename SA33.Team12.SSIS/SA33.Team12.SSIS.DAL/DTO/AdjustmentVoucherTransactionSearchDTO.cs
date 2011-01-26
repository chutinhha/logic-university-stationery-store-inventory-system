﻿using System;
using System.Web;
using System.ComponentModel;

namespace SA33.Team12.SSIS.DAL.DTO
{
    //Edit by Anthony 26 Jan 2011
    //This is for the Temp AdjustmentVoucherTransactions & StockLogTransactions
    public class AdjustmentVoucherTransactionSearchDTO
    {
        public int AdjustmentVoucherID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}