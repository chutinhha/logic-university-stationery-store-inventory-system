/***
 * Author: Anthony
 * Initial Implementation: 30/Jan/2011
 ***/

using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;


namespace SA33.Team12.SSIS.BLL
{
    public class AdjustmentVoucherManager : BusinessLogic
    {
        private AdjustmentVoucherDAO adjustmentVoucherDAO;

        public AdjustmentVoucherManager()
        {
            adjustmentVoucherDAO = new AdjustmentVoucherDAO();
        }

        #region AdjustmentVoucherTransaction(Temporary Table) (Create, Update, Delete) Query(GetAll, GetID, GetCriteria)

        public AdjustmentVoucherTransaction CreateAdjustmentVoucherTransaction(AdjustmentVoucherTransaction adjustmentVoucherTransaction)
        {

            try
            {
                adjustmentVoucherDAO.CreateAdjustmentVoucherTransaction(adjustmentVoucherTransaction);
            }
            catch (Exception ex)
            {
                throw new Exception("Adjustment Voucher Transaction Creation Failed" + ex.Message);
            }
            return adjustmentVoucherTransaction;
        }

        public void ApproveAdjustmentVoucherTransaction(AdjustmentVoucherTransaction adjustmentVoucherTransaction)
        {
            //Still thinking what to do with this
            //AdjustmentVoucherDAO.ApproveAdjustmentVoucherTransaction(adjustmentVoucherTransaction);
        }

        public void UpdateAdjustmentVoucherTransaction(AdjustmentVoucherTransaction adjustmentVoucherTransaction)
        {
            try
            {
                adjustmentVoucherDAO.UpdateAdjustmentVoucherTransaction(adjustmentVoucherTransaction);
            }
            catch (Exception ex)
            {
                throw new Exception("Adjustment Voucher Transaction Update Failed" + ex.Message);
            }
        }

        public List<AdjustmentVoucherTransaction> GetAllAdjustmentVoucherTransaction()
        {
            return adjustmentVoucherDAO.GetAllAdjustmentVoucherTransaction();
        }
        #endregion

        #region StockLogTransaction (Temporary Table) (Create, Update, Delete) Query (GetAll, GetID, GetCriteria)
        public StockLogTransaction CreateStockLogTransaction(StockLogTransaction stockLogTransaction)
        {
            try
            {
                return adjustmentVoucherDAO.CreateStockLogTransaction(stockLogTransaction);
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Transaction Creation Failed" + ex.Message);
            }
        }

        public StockLogTransaction UpdateStockLogTransaction(StockLogTransaction stockLogTransaction)
        {
            try
            {
                return adjustmentVoucherDAO.UpdateStockLogTransaction(stockLogTransaction);
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Transaction Update Failed" + ex.Message);
            }
        }
        
        public void DeleteStockLogTransaction(StockLogTransaction stockLogTransaction)
        {
            try
            {
                adjustmentVoucherDAO.DeleteStockLogTransaction(stockLogTransaction);
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Transaction Delete Failed" + ex.Message);
            }
        }

        public List<StockLogTransaction> FindAllStockLogTransaction()
        {
            try
            {
                return adjustmentVoucherDAO.GetAllStockLogTransaction();
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Transaction Return Get All Records Error" + ex.Message);
            }

        }

        public StockLogTransaction GetAllStockLogTransactionByID(int ID)
        {
            try
            {
                return adjustmentVoucherDAO.GetAllStockLogTransactionByID(ID);
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Transaction Get All Records By ID Return Error" + ex.Message);
            }
        }

        public List<StockLogTransaction> GetAllStockLogTransactionByCriteria(AdjustmentVoucherTransactionSearchDTO criteria)
        {
            try
            {
                return adjustmentVoucherDAO.GetAllStockLogTransactionByCriteria(criteria);
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Transaction Get All Records By Criteria Return Error" + ex.Message);
            }
        }
        
        #endregion

        #region AdjustmentVoucher(Actual Table) (Create, Update, Delete) Query(GetAll, GetID, GetCriteria)

        public void CreateAdjustmentVoucher(AdjustmentVoucher adjustmentVoucher)
        {
            try
            {
                adjustmentVoucherDAO.CreateAdjustmentVoucher(adjustmentVoucher);
            }
            catch (Exception ex)
            {
                throw new Exception("Adjustment Voucher Creation Failed" + ex.Message);
            }
        }

        public void ApproveAdjustmentVoucher(AdjustmentVoucher adjustmentVoucher)
        {
            //Still thinking what to do with this
            //AdjustmentVoucherDAO.ApproveAdjustmentVoucherTransaction(adjustmentVoucherTransaction);
        }

        public void UpdateAdjustmentVoucher(AdjustmentVoucher adjustmentVoucher)
        {
            try
            {
                adjustmentVoucherDAO.UpdateAdjustmentVoucher(adjustmentVoucher);
            }
            catch (Exception ex)
            {
                throw new Exception("Adjustment Voucher Update Failed" + ex.Message);
            }
        }

        #endregion

        #region StockLog (Actual Table) (Create, Update, Delete) Query (GetAll, GetID, GetCriteria)
        public StockLog CreateStockLog(StockLog stockLog)
        {
            try
            {
                return adjustmentVoucherDAO.CreateStockLog(stockLog);
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Creation Failed" + ex.Message);
            }
        }

        public StockLog UpdateStockLog(StockLog stockLog)
        {
            try
            {
                return adjustmentVoucherDAO.UpdateStockLog(stockLog);
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Update Failed" + ex.Message);
            }
        }

        public void DeleteStockLog(StockLog stockLog)
        {
            try
            {
                adjustmentVoucherDAO.DeleteStockLog(stockLog);
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Delete Failed" + ex.Message);
            }
        }

        public List<StockLog> FindAllStockLog()
        {
            try
            {
                return adjustmentVoucherDAO.GetAllStockLog();
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Return Get All Records Error" + ex.Message);
            }

        }

        public StockLog GetAllStockLogByID(int ID)
        {
            try
            {
                return adjustmentVoucherDAO.GetAllStockLogByID(ID);
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Get All Records By ID Return Error" + ex.Message);
            }
        }

        public List<StockLog> GetAllStockLogByCriteria(AdjustmentVoucherSearchDTO criteria)
        {
            try
            {
                return adjustmentVoucherDAO.GetAllStockLogByCriteria(criteria);
            }
            catch (Exception ex)
            {
                throw new Exception("Stock Log Get All Records By Criteria Return Error" + ex.Message);
            }
        }

        #endregion        
    }
}
