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
    public enum AdjustmentType
    {
        AdjustmentUp,
        AdjustmentDown,
        Consumption,
        Replenish,
        Damage
    }
    public class AdjustmentVoucherDAO : DALLogic
    {
        //Created by Anthony 30 Jan 2011

        #region AdjustmentVoucherTransaction (Temporary Table Parent) (Create, Update, Delete) Query(GetAll, GetID, GetCriteria)

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

        public void DeleteAdjustmentVoucherTransaction(AdjustmentVoucherTransaction adjustmentVoucherTransaction)
        {
            try
            {
                AdjustmentVoucherTransaction persistedTransaction = (from t in context.AdjustmentVoucherTransactions
                                              where t.AdjustmentVoucherTransactionID == adjustmentVoucherTransaction.AdjustmentVoucherTransactionID
                                                                  select t).First<AdjustmentVoucherTransaction>();

                using (TransactionScope ts = new TransactionScope())
                {
                    persistedTransaction.StockLogTransactions.Load();
                    context.AdjustmentVoucherTransactions.DeleteObject(persistedTransaction);
                    context.SaveChanges();
                    ts.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateAdjustmentVoucherTransaction(AdjustmentVoucherTransaction updateAdjustmentVoucherTransaction)
        {
            try
            {
                var tempReq = (from r in context.AdjustmentVoucherTransactions
                               where r.AdjustmentVoucherTransactionID == updateAdjustmentVoucherTransaction.AdjustmentVoucherTransactionID
                               select r).FirstOrDefault<AdjustmentVoucherTransaction>();

                tempReq = updateAdjustmentVoucherTransaction;
                context.SaveChanges();
                //                return (updateAdjustmentVoucherTransaction);
            }
            catch (Exception ex)
            {
                throw new AdjustmentVoucherException("Update Adjustment Voucher failed." + ex.Message);

            }
        }

        ////This for the approval and to convert from the temp database into the actual database (Thinking on how to copy from temp table to the actual table)


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
                if (adjustmentVoucherTransactionSearchDTO.AdjustmentVoucherTransactionID != -1)
                {
                    tempQuery = tempQuery.Where(r => r.AdjustmentVoucherTransactionID == adjustmentVoucherTransactionSearchDTO.AdjustmentVoucherTransactionID);
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
        public AdjustmentVoucherTransaction FindAdjustmentVoucherTransactionsByGetID(int adjustmentVoucherTransactionID)
        {
            return (from r in context.AdjustmentVoucherTransactions
                             where r.AdjustmentVoucherTransactionID == adjustmentVoucherTransactionID
                             select r).FirstOrDefault<AdjustmentVoucherTransaction>();
        }

        #endregion

        #region StockLogTransaction (Temporary Table Items)  (Create, Update, Delete) Query(GetAll, GetID, GetCriteria)
        //(Create, Update, Delete) (GetAll,GetID,GetCriteria)

        public StockLogTransaction CreateStockLogTransaction(StockLogTransaction stockLogTransaction)
        {
            try
            {
                context.AddToStockLogTransactions(stockLogTransaction);
                return stockLogTransaction;
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Transaction Creation Error" + ex.Message);
            }
        }

        public StockLogTransaction UpdateStockLogTransaction(StockLogTransaction stockLogTransaction)
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
                return stockLogTransaction;
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Transaction Update Error" + ex.Message);
            }
        }

        public void DeleteStockLogTransaction(StockLogTransaction stockLogTransaction)
        {
            try
            {
                var temp = (from ri in context.StockLogTransactions
                            where ri.StockLogTransactionID == stockLogTransaction.StockLogTransactionID
                            select ri).FirstOrDefault<StockLogTransaction>();

                using (TransactionScope ts = new TransactionScope())
                {
                    context.StockLogTransactions.DeleteObject(temp);
                    context.SaveChanges();
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Transaction Delete Error" + ex.Message);
            }
        }

        public List<StockLogTransaction> GetAllStockLogTransaction()
        {
            try
            {
                return (from ri in context.StockLogTransactions
                        select ri).ToList<StockLogTransaction>();
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Transaction Get All Records Return Error" + ex.Message);
            }
        }

        public StockLogTransaction GetAllStockLogTransactionByID(int ID)
        {
            try
            {
                return (from ri in context.StockLogTransactions
                       where ri.StockLogTransactionID == ID
                        select ri).FirstOrDefault<StockLogTransaction>();

            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Transaction Get All Records By ID Return Error" + ex.Message);
            }
        }

        public List<StockLogTransaction> GetAllStockLogTransactionByCriteria(AdjustmentVoucherTransactionSearchDTO adjustmentVoucherTransactionSearchDTO)
        {
            try
            {
                var Query =
                    from u in context.StockLogTransactions
                    where u.StockLogTransactionID == (adjustmentVoucherTransactionSearchDTO.StockLogTransactionID == 0 ? u.StockLogTransactionID : adjustmentVoucherTransactionSearchDTO.StockLogTransactionID)
                    && u.AdjustmentVoucherTransactionID == (adjustmentVoucherTransactionSearchDTO.AdjustmentVoucherTransactionID == 0 ? u.AdjustmentVoucherTransactionID : adjustmentVoucherTransactionSearchDTO.AdjustmentVoucherTransactionID)
                    && u.StationeryID == (adjustmentVoucherTransactionSearchDTO.StationeryID == 0 ? u.Type : adjustmentVoucherTransactionSearchDTO.StationeryID)
                    && u.Type == (adjustmentVoucherTransactionSearchDTO.Type == 0 ? u.Type : adjustmentVoucherTransactionSearchDTO.Type)
                    && u.Reason == (adjustmentVoucherTransactionSearchDTO.Reason == null ? u.Reason : adjustmentVoucherTransactionSearchDTO.Reason)
                    && u.Quantity == (adjustmentVoucherTransactionSearchDTO.Quantity == 0 ? u.Quantity : adjustmentVoucherTransactionSearchDTO.Quantity)
                    && u.Balance == (adjustmentVoucherTransactionSearchDTO.Balance == 0 ? u.Balance : adjustmentVoucherTransactionSearchDTO.Balance)
                    select u;
                List<StockLogTransaction> stockLogTransaction = Query.ToList<StockLogTransaction>();
                return stockLogTransaction;
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Transaction Get All Records By Criteria Return Error" + ex.Message);
            }
        }

        #endregion

        #region AdjustmentVoucher (Actual Table Parent) (Create, Update, Delete) Query(GetAll, GetID, GetCriteria)

        public void CreateAdjustmentVoucher(AdjustmentVoucher AdjustmentVoucher)
        {
            try
            {
                //Create a transaction scope
                using (TransactionScope ts = new TransactionScope())
                {      
                    this.context.AdjustmentVouchers.AddObject(AdjustmentVoucher);
                    this.context.SaveChanges();
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                //Exception thrown incase if insert fails
                throw new AdjustmentVoucherException("Create Adjustment Voucher Transaction failed." + ex.Message);
            }
        }

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


        public List<AdjustmentVoucher> GetAllAdjustmentVoucher()
        {
            return (from c in context.AdjustmentVouchers select c).ToList<AdjustmentVoucher>();
        }

        //Need to FindByCriteria Temp Table AdjustmentVoucherTransaction
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

        //Need to FindByGetID Temp Table AdjustmentVoucher
        public AdjustmentVoucher FindAdjustmentVoucherByID(int AdjustmentVoucherID)
        {
            var tempQuery = (from r in context.AdjustmentVouchers
                             where r.AdjustmentVoucherID == AdjustmentVoucherID
                             select r);

            return tempQuery.FirstOrDefault();
        }

        #endregion

        #region StockLog (Actual Table Items)  (Create, Update, Delete) Query(GetAll, GetID, GetCriteria)

        public StockLog CreateStockLog(StockLog stockLog)
        {
            try
            {
                context.AddToStockLogs(stockLog);
                return stockLog;
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Creation Error" + ex.Message);
            }
        }

        public StockLog UpdateStockLog(StockLog stockLog)
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
                return stockLog;
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Transaction Update Error" + ex.Message);
            }
        }

        public void DeleteStockLog(StockLog stockLog)
        {
            try
            {
                var temp = (from ri in context.StockLogs
                            where ri.StockLogID == stockLog.StockLogID
                            select ri).FirstOrDefault<StockLog>();

                context.StockLogs.DeleteObject(temp);
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Delete Error" + ex.Message);
            }
        }

        public List<StockLog> GetAllStockLog()
        {
            try
            {
                return (from ri in context.StockLogs
                        select ri).ToList<StockLog>();
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Get All Records Return Error" + ex.Message);
            }
        }

        public StockLog GetAllStockLogByID(int ID)
        {
            try
            {
                return (from ri in context.StockLogs
                        where ri.StockLogID == ID
                        select ri).FirstOrDefault<StockLog>();
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Get All Records By ID Return Error" + ex.Message);
            }
        }

        public List<StockLog> GetAllStockLogByCriteria(AdjustmentVoucherSearchDTO adjustmentVoucherSearchDTO)
        {
            try
            {
                var Query =
                    from u in context.StockLogs
                    where u.StockLogID == (adjustmentVoucherSearchDTO.StockLogID == 0 ? u.StockLogID : adjustmentVoucherSearchDTO.StockLogID)
                    && u.AdjustmentVoucherID == (adjustmentVoucherSearchDTO.AdjustmentVoucherID == 0 ? u.AdjustmentVoucherID : adjustmentVoucherSearchDTO.AdjustmentVoucherID)
                    && u.StationeryID == (adjustmentVoucherSearchDTO.StationeryID == 0 ? u.Type : adjustmentVoucherSearchDTO.StationeryID)
                    && u.Type == (adjustmentVoucherSearchDTO.Type == 0 ? u.Type : adjustmentVoucherSearchDTO.Type)
                    && u.Reason == (adjustmentVoucherSearchDTO.Reason == null ? u.Reason : adjustmentVoucherSearchDTO.Reason)
                    && u.Quantity == (adjustmentVoucherSearchDTO.Quantity == 0 ? u.Quantity : adjustmentVoucherSearchDTO.Quantity)
                    && u.Balance == (adjustmentVoucherSearchDTO.Balance == 0 ? u.Balance : adjustmentVoucherSearchDTO.Balance)
                    select u;
                List<StockLog> stockLog = Query.ToList<StockLog>();
                return stockLog;
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Transaction Get All Records By Criteria Return Error" + ex.Message);
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