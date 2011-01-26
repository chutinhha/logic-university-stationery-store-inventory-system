using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SA33.Team12.SSIS;

namespace SA33.Team12.SSIS.DAL
{
    public class AdjustmentVoucherDAO : DALLogic
    {
        /***
         * this is a sample do not use this
         * ***/
        //Created by Anthony 26 Jan 2011

        //Writing to Temp Tables (AdjustmentVoucherTransactions & StockLogTransactions)
        public void CreateStockLogTransactionTemp(DAL.AdjustmentVoucherTransaction adjustmentVoucher)
        {
            try
            {
                context.AddToAdjustmentVoucherTransactions(adjustmentVoucher);
                foreach (StockLogTransaction adjustmentVoucherItems in adjustmentVoucher.StockLogTransactions)
                {
                    context.AddToStockLogTransactions(adjustmentVoucherItems);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Writing to Real Approved Tables (AdjustmentVouchers & StockLogs)
        public void CreateStockLogTransactionActual(DAL.AdjustmentVoucherTransaction adjustmentVoucher)
        {
            try
            {
                //Reading from Temp Tables (AdjustmentVoucherTransactions & StockLogTransactions)
                //Writing to Real Approved Tables (AdjustmentVouchers & StockLogs)

                AdjustmentVoucher aj = new AdjustmentVoucher();
                aj.CreatedBy = (adjustmentVoucher.CreatedBy).Value;
                aj.ApprovedBy = 00;
                aj.DateIssued = adjustmentVoucher.DateIssued;
                aj.DateApproved = DateTime.Now;
                context.AddToAdjustmentVouchers(aj);

                foreach (StockLogTransaction aji in adjustmentVoucher.StockLogTransactions)
                {
                    StockLog sl = new StockLog();
                    {
                        sl.StationeryID = aji.StationeryID;
                        sl.Type = aji.Type;
                        sl.Reason = aji.Reason;
                        sl.Quantity = aji.Quantity;
                        sl.Balance = aji.Balance;
                        context.AddToStockLogs(sl);
                     }
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DAL.StockLogTransaction> GetAllStockLogTransaction()
        {
            return (from s in context.StockLogTransactions select s).ToList<DAL.StockLogTransaction>();
        }

        //Do we want to allow the user to modify the "Adjust Inventory after he has created it?"
        // the Temp Tables AjustmentVoucherTransactions and StockLog =Transactions only has "Create" Method without delete or anything else"
        public void UpdateStockLogTransaction(DAL.StockLogTransaction adjustmentVoucher)
        {
            DAL.StockLogTransaction tempStockLogTransaction = (from s in context.StockLogTransactions
                                                                                 where s.StockLogTransactionID == adjustmentVoucher.StockLogTransactionID
                                             select s).FirstOrDefault<DAL.StockLogTransaction>();
            //tempStockLogTransaction.Type = StockLogTransaction.Type;
            //tempStockLogTransaction.Reason = StockLogTransaction.Reason;
            //tempStockLogTransaction.Quantity = StockLogTransaction.Quantity;
            //tempStockLogTransaction.Balance = StockLogTransaction.Balance;
            context.ObjectStateManager.ChangeObjectState(tempStockLogTransaction, System.Data.EntityState.Modified);
            context.SaveChanges();
        }

        public void DeleteStockLogTransaction(DAL.StockLogTransaction adjustmentVoucher)
        {
            context.StockLogTransactions.Attach(adjustmentVoucher);
            context.StockLogTransactions.DeleteObject(adjustmentVoucher);
            context.SaveChanges();
        }
    }
}