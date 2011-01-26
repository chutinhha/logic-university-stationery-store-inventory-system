using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;
using SA33.Team12.SSIS;
using SA33.Team12.SSIS.DAL.DTO;

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
                //To be continued
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
                //To be continued
                throw ex;
            }
        }


        //To get All from the Temp StockLogTransactions 
        public List<DAL.StockLogTransaction> GetAllStockLogTransactionTemp()
        {
            return (from s in context.StockLogTransactions select s).ToList<DAL.StockLogTransaction>();
        }

        //To get All from the Actual StockLogs 
        public List<DAL.StockLog> GetAllStockLogTransactionActual()
        {
            return (from s in context.StockLogs select s).ToList<DAL.StockLog>();
        }

        //Need to FindByCriteria Temp
        public List<AdjustmentVoucherTransaction> FindAdjustmentVoucherTransactionsByCriteria(AdjustmentVoucherTransactionSearchDTO adjustmentVoucherTransactionSearchDTO)
        {
            var tempQuery = (from r in context.AdjustmentVoucherTransactions
                             where 1 == 1
                             select r);

            if (adjustmentVoucherTransactionSearchDTO != null)
            {
                if (adjustmentVoucherTransactionSearchDTO.AdjustmentVoucherID != -1)
                {
                    tempQuery = tempQuery.Where(r => r.AdjustmentVoucherTransactionID == adjustmentVoucherTransactionSearchDTO.AdjustmentVoucherID);
                }
                if (adjustmentVoucherTransactionSearchDTO.StartDate != null && adjustmentVoucherTransactionSearchDTO.EndDate != null)
                {
                    tempQuery = tempQuery.Where(r => r.DateIssued >= adjustmentVoucherTransactionSearchDTO.StartDate && r.DateIssued <= adjustmentVoucherTransactionSearchDTO.EndDate);
                }

                if (adjustmentVoucherTransactionSearchDTO.StartDate != null)
                {
                    tempQuery = tempQuery.Where(r => r.DateIssued == adjustmentVoucherTransactionSearchDTO.StartDate);
                }

                if (adjustmentVoucherTransactionSearchDTO.EndDate != null)
                {
                    tempQuery = tempQuery.Where(r => r.DateIssued == adjustmentVoucherTransactionSearchDTO.EndDate);
                }
            }

            return (from q in tempQuery select q).ToList<AdjustmentVoucherTransaction>();

        }

        //Need to FindByCriteria Actual
        public List<AdjustmentVoucher> FindAdjustmentVoucherByCriteria(AdjustmentVoucherSearchDTO adjustmentVoucherSearchDTO)
        {
            var tempQuery = (from r in context.AdjustmentVouchers
                             where 1 == 1
                             select r);

            if (adjustmentVoucherSearchDTO != null)
            {
                if (adjustmentVoucherSearchDTO.AdjustmentVoucherID != -1)
                {
                    tempQuery = tempQuery.Where(r => r.AdjustmentVoucherID == adjustmentVoucherSearchDTO.AdjustmentVoucherID);
                }
                if (adjustmentVoucherSearchDTO.StartDate != null && adjustmentVoucherSearchDTO.EndDate != null)
                {
                    tempQuery = tempQuery.Where(r => r.DateIssued >= adjustmentVoucherSearchDTO.StartDate && r.DateIssued <= adjustmentVoucherSearchDTO.EndDate);
                }

                if (adjustmentVoucherSearchDTO.StartDate != null)
                {
                    tempQuery = tempQuery.Where(r => r.DateIssued == adjustmentVoucherSearchDTO.StartDate);
                }

                if (adjustmentVoucherSearchDTO.EndDate != null)
                {
                    tempQuery = tempQuery.Where(r => r.DateIssued == adjustmentVoucherSearchDTO.EndDate);
                }
            }

            return (from q in tempQuery select q).ToList<AdjustmentVoucher>();
        }


        //Do we want to allow the user to modify the "Adjust Inventory after he has created it?"
        // the Temp Tables AjustmentVoucherTransactions and StockLog =Transactions only has "Create" Method without delete or anything else"
        public void UpdateStockLogTransaction(DAL.StockLogTransaction adjustmentVoucher)
        {
            DAL.StockLogTransaction tempStockLogTransaction = (from s in context.StockLogTransactions
                                                                                 where s.StockLogTransactionID == adjustmentVoucher.StockLogTransactionID
                                             select s).FirstOrDefault<DAL.StockLogTransaction>();
            //tempStockLogTransaction.Type = StockLogTransactions.Type;
            //tempStockLogTransaction.Reason = StockLogTransaction.Reason;
            //tempStockLogTransaction.Quantity = StockLogTransaction.Quantity;
            //tempStockLogTransaction.Balance = StockLogTransaction.Balance;
            //context.ObjectStateManager.ChangeObjectState(tempStockLogTransaction, System.Data.EntityState.Modified);
            //context.SaveChanges();
        }





        //To Delete from Temp StockLogTransactions those orders that has not yet been approved
        public void DeleteStockLogTransaction(DAL.StockLogTransaction adjustmentVoucher)
        {
            context.StockLogTransactions.Attach(adjustmentVoucher);
            context.StockLogTransactions.DeleteObject(adjustmentVoucher);
            context.SaveChanges();
        }
    }
}