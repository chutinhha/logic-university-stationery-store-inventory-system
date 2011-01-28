using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;
using System.Transactions;
using SA33.Team12.SSIS.Exceptions;
using System.Data.Objects;

namespace SA33.Team12.SSIS.DAL
{
    public class AdjustmentVoucherDAO : DALLogic
    {
        /***
         * this is a sample do not use this
         * ***/
        //Created by Anthony 28 Jan 2011

        #region AdjustmentVoucherTransaction (Temporary Table Parent)

        ///This is for the Temporary AdjustmentVoucherTransaction tables (AdjustmentVoucherTransaction)
        /// <summary>
        /// Create a new AdjustmentVoucherTransaction and persist with database
        /// </summary>
        /// <param name="adjustmentVoucherTransaction">AdjustmentVoucherTransaction object</param>
        public void CreateAdjustmentVoucherTransaction(AdjustmentVoucherTransaction AdjustmentVoucherTransaction)
        {
            try
            {
                //Create a transaction scope
                using (TransactionScope ts = new TransactionScope())
                {
                    //Add adjustmentVoucherTransaction to context                
                    context.AddToAdjustmentVoucherTransactions(AdjustmentVoucherTransaction);

                    //Save the changes
                    context.SaveChanges();

                    //Notify Transaction completed
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                //Exception thrown incase if insert fails
                throw new AdjustmentVoucherException("Create Adjustment Voucher Transaction failed." + ex.Message);
            }
        }

        /// <summary>
        /// Update the AdjustmentVoucherTransaction before approval
        /// </summary>
        /// <param name="updateAdjustmentVoucherTransaction">AdjustmentVoucherTransaction object</param>
        public void UpdateAdjustmentVoucherTransaction(AdjustmentVoucherTransaction updateAdjustmentVoucherTransaction)
        {
            try
            {
                var tempReq = (from r in context.AdjustmentVoucherTransactions
                               where r.AdjustmentVoucherTransactionID == updateAdjustmentVoucherTransaction.AdjustmentVoucherTransactionID
                               select r).FirstOrDefault<AdjustmentVoucherTransaction>();

                tempReq = updateAdjustmentVoucherTransaction;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new AdjustmentVoucherException("Update Adjustment Voucher failed." + ex.Message);
            }
        }

        ////This for the approval and to convert from the temp database into the actual database (Thinking on how to copy from temp table to the actual table)


        /// <summary>
        /// Get All AdjustmentVoucherTransaction
        /// </summary>
        /// <returns></returns>
        public List<AdjustmentVoucherTransaction> GetAllAdjustmentVoucherTransaction()
        {
            return (from c in context.AdjustmentVoucherTransactions select c).ToList<AdjustmentVoucherTransaction>();
        }

        //Need to FindByCriteria Temp Table AdjustmentVoucherTransaction
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

        //Need to FindByGetID Temp Table AdjustmentVoucherTransaction
        public List<AdjustmentVoucherTransaction> FindAdjustmentVoucherTransactionsByGetID(AdjustmentVoucherTransactionSearchDTO adjustmentVoucherTransactionSearchDTO)
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
            }

            return (from q in tempQuery select q).ToList<AdjustmentVoucherTransaction>();

        }

        #endregion

        #region StockLogTransaction (Temporary Table Items)
        /// <summary>
        /// Create a new StockLogTransaction
        /// </summary>
        /// <param name="StockLogTransaction">StockLogTransaction object</param>
        public void CreateStockLogTransaction(StockLogTransaction stockLogTransaction)
        {
            try
            {
                context.AddToStockLogTransactions(stockLogTransaction);
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Update the StockLogTransaction
        /// </summary>
        /// <param name="stockLogTransaction">StockLogTransaction object</param>
        public void UpdateStockLogTransaction(StockLogTransaction stockLogTransaction)
        {
            try
            {
                var temp = (from ri in context.StockLogTransactions
                            where ri.StockLogTransactionID == stockLogTransaction.StockLogTransactionID
                            select ri).FirstOrDefault<StockLogTransaction>();
                temp.Type = stockLogTransaction.Type;
                temp.Reason = stockLogTransaction.Reason;
                temp.Quantity = stockLogTransaction.Quantity;
                temp.Balance = stockLogTransaction.Balance;
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Delete the StockLogTransaction
        /// </summary>
        /// <param name="stockLogTransaction">stockLogTransaction object</param>
        public void DeleteStockLogTransaction(StockLogTransaction stockLogTransaction)
        {
            try
            {
                var temp = (from ri in context.StockLogTransactions
                            where ri.StockLogTransactionID == stockLogTransaction.StockLogTransactionID
                            select ri).FirstOrDefault<StockLogTransaction>();

                context.StockLogTransactions.DeleteObject(temp);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get All StockLogTransaction in the AdjustmentVoucherTransaction form
        /// </summary>
        /// <param name="adjustmentVoucherTransaction"></param>
        /// <returns></returns>
        public List<StockLogTransaction> GetAllStockLogTransaction(AdjustmentVoucherTransaction adjustmentVoucherTransaction)
        {
            try
            {
                return (from ri in context.StockLogTransactions
                        where ri.AdjustmentVoucherTransactionID == adjustmentVoucherTransaction.AdjustmentVoucherTransactionID
                        select ri).ToList<StockLogTransaction>();
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Get StockLogTransaction by primary key
        /// </summary>
        /// <param name="stockLogTransaction">stockLogTransaction object</param>
        /// <returns></returns>
        public StockLogTransaction GetAllStockLogTransactionByID(StockLogTransaction stockLogTransaction)
        {
            try
            {
                return (from ri in context.StockLogTransactions
                        where ri.StockLogTransactionID == stockLogTransaction.StockLogTransactionID
                        select ri).FirstOrDefault<StockLogTransaction>();
            }
            catch (Exception)
            {

                throw;
            }
        }



        #endregion

        #region AdjustmentVoucher (Actual Table Parent)

        ///This is for the Actual AdjustmentVoucher tables (AdjustmentVoucher)
        /// <summary>
        /// Create a new AdjustmentVoucher and persist with database
        /// </summary>
        /// <param name="adjustmentVoucher">AdjustmentVoucher object</param>
        public void CreateAdjustmentVoucher(AdjustmentVoucher AdjustmentVoucher)
        {
            try
            {
                //Create a transaction scope
                using (TransactionScope ts = new TransactionScope())
                {
                    //Add AdjustmentVoucher to context                
                    context.AddToAdjustmentVouchers(AdjustmentVoucher);

                    //Save the changes
                    context.SaveChanges();

                    //Notify Transaction completed
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                //Exception thrown incase if insert fails
                throw new AdjustmentVoucherException("Create Adjustment Voucher failed." + ex.Message);
            }
        }

        /// <summary>
        /// Update the AdjustmentVoucher after approval
        /// </summary>
        /// <param name="updateAdjustmentVoucher">AdjustmentVoucher object</param>
        public void UpdateAdjustmentVoucher(AdjustmentVoucher updateAdjustmentVoucher)
        {
            try
            {
                var tempReq = (from r in context.AdjustmentVouchers
                               where r.AdjustmentVoucherID == updateAdjustmentVoucher.AdjustmentVoucherID
                               select r).FirstOrDefault<AdjustmentVoucher>();

                tempReq = updateAdjustmentVoucher;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new AdjustmentVoucherException("Update Adjustment Voucher failed." + ex.Message);
            }
        }

        ////This for the approval and to convert from the temp database into the actual database (Thinking on how to copy from temp table to the actual table)


        /// <summary>
        /// Get All AdjustmentVoucher
        /// </summary>
        /// <returns></returns>
        public List<AdjustmentVoucher> GetAllAdjustmentVoucher()
        {
            return (from c in context.AdjustmentVouchers select c).ToList<AdjustmentVoucher>();
        }

        //Need to FindByCriteria Temp Table AdjustmentVoucherTransaction
        public List<AdjustmentVoucher> FindAdjustmentVouchersByCriteria(AdjustmentVoucherSearchDTO adjustmentVoucherSearchDTO)
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

        //Need to FindByGetID Temp Table AdjustmentVoucher
        public List<AdjustmentVoucher> FindAdjustmentVouchersByGetID(AdjustmentVoucherSearchDTO adjustmentVoucherSearchDTO)
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
            }

            return (from q in tempQuery select q).ToList<AdjustmentVoucher>();

        }

        #endregion


        #region StockLog (Actual Table Items)
        /// <summary>
        /// Create a new StockLog
        /// </summary>
        /// <param name="StockLog">StockLog object</param>
        public void CreateStockLog(StockLog stockLog)
        {
            try
            {
                context.AddToStockLogs(stockLog);
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Update the StockLog
        /// </summary>
        /// <param name="stockLog">StockLog object</param>
        public void UpdateStockLog(StockLog stockLog)
        {
            try
            {
                var temp = (from ri in context.StockLogs
                            where ri.StockLogID == stockLog.StockLogID
                            select ri).FirstOrDefault<StockLog>();
                temp.Type = stockLog.Type;
                temp.Reason = stockLog.Reason;
                temp.Quantity = stockLog.Quantity;
                temp.Balance = stockLog.Balance;
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Delete the StockLog
        /// </summary>
        /// <param name="stockLog">stockLog object</param>
        public void DeleteStockLog(StockLog stockLog)
        {
            try
            {
                var temp = (from ri in context.StockLogs
                            where ri.StockLogID == stockLog.StockLogID
                            select ri).FirstOrDefault<StockLog>();

                context.StockLogs.DeleteObject(temp);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get All StockLog in the AdjustmentVoucher form
        /// </summary>
        /// <param name="adjustmentVoucher"></param>
        /// <returns></returns>
        public List<StockLog> GetAllStockLog(AdjustmentVoucher adjustmentVoucher)
        {
            try
            {
                return (from ri in context.StockLogs
                        where ri.AdjustmentVoucherID == adjustmentVoucher.AdjustmentVoucherID
                        select ri).ToList<StockLog>();
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Get StockLog by primary key
        /// </summary>
        /// <param name="stockLog">stockLog object</param>
        /// <returns></returns>
        public StockLog GetAllStockLogByID(StockLog stockLog)
        {
            try
            {
                return (from ri in context.StockLogs
                        where ri.StockLogID == stockLog.StockLogID
                        select ri).FirstOrDefault<StockLog>();
            }
            catch (Exception)
            {

                throw;
            }
        }



        #endregion


        /*
        ///My codes ---> Anthony

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


        //Need to FindByCriteria Actual
        public List<AdjustmentVoucher> FindAdjustmentVoucherByGetID(AdjustmentVoucherSearchDTO adjustmentVoucherSearchDTO)
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
            }

            return (from q in tempQuery select q).ToList<AdjustmentVoucher>();
        }
         * 
         * */
    }
}