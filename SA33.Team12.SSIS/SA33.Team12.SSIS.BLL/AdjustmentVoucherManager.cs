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
using SA33.Team12.SSIS.DAL.AdjustmentVoucherDAO;

namespace SA33.Team12.SSIS.BLL
{
    public class AdjustmentVoucherManager : BusinessLogic
    {
        /// <summary>
        /// Create new adjustment voucher
        /// </summary>
        /// <param name="adjustmentVoucher">The adjustment voucher object</param>
        /// Created by Anthony 26 Jan 2011
        public void CreateAdjustmentVoucherTemp(DAL.AdjustmentVoucher adjustmentVoucher)
        {
            //CreateStockLogTransactionTemp(adjustmentVoucher);
        }

        public void CreateAdjustmentVoucherActual(DAL.AdjustmentVoucher adjustmentVoucher)
        {
            //CreateStockLogTransactionActual(adjustmentVoucher);
        }

        /// <summary>
        /// Update adjustment voucher before approval
        /// </summary>
        /// <param name="adjustmentVoucher">The adjustment voucher object</param>
        public void UpdateAdjustmentVoucher(DAL.AdjustmentVoucher adjustmentVoucher)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Approval of adjustment voucher by supervisor or manager.
        /// Throws NoAccessRightToApproveException when approved by user other than those mentioned above
        /// </summary>
        /// <param name="adjustmentVoucher">The adjustment voucher object</param>
        /// <param name="user">User object of supervisor or manager</param>
        public void ApproveAdjustmentVoucher(DAL.AdjustmentVoucher adjustmentVoucher, DAL.User user)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Find adjustment by provide criteria in AdjustmentVoucherSearchDTO object
        /// </summary>
        /// <param name="adjustmentVoucherSearchDTO">The adjustment voucher search object</param>
        public void Find(AdjustmentVoucherSearchDTO adjustmentVoucherSearchDTO)
        {
            throw new System.NotImplementedException();
        }
    }
}
