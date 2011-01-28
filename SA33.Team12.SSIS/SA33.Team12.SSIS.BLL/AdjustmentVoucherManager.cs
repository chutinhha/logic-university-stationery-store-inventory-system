/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Web;
using System.ComponentModel;

using SA33.Team12.SSIS;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;
//using SA33.Team12.SSIS.DAL.AdjustmentVoucherDAO;

namespace SA33.Team12.SSIS.BLL
{
    public class AdjustmentVoucherManager : BusinessLogic
    {
        /// <summary>
        /// Create new adjustment voucher
        /// </summary>
        /// <param name="adjustmentVoucher">The adjustment voucher object</param>
        /// Created by Anthony 26 Jan 2011

        private AdjustmentVoucherDAO adjustmentVoucherDAO;
        
        public AdjustmentVoucherManager()
        {
            adjustmentVoucherDAO = new AdjustmentVoucherDAO();
        }
        #region Temp Table
        //Create Temp Parent Table
        public void CreateAdjustmentVoucherTransaction(AdjustmentVoucherTransaction adjustmentVoucherTransaction)
        {
            try
            {
                adjustmentVoucherDAO.CreateAdjustmentVoucherTransaction(adjustmentVoucherTransaction);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Adjustment Voucher Transaction creation Failed");
            }
        }

        public void ApproveAdjustmentVoucherTransaction(AdjustmentVoucherTransaction adjustmentVoucherTransaction)
        {
            //Still thinking what to do with this
            //AdjustmentVoucherDAO.ApproveAdjustmentVoucherTransaction(adjustmentVoucherTransaction);
        }



        #endregion

        #region Acutal Table
        //Create Actual Parent Table
        public void CreateAdjustmentVoucher(AdjustmentVoucher adjustmentVoucher)
        {
            try
            {
                adjustmentVoucherDAO.CreateAdjustmentVoucher(adjustmentVoucher);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Adjustment Voucher creation Failed");
            }
        }



        #endregion




    }
}
