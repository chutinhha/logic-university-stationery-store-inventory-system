/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Web;
using System.ComponentModel;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;

namespace SA33.Team12.SSIS.DAL
{
    public class DisbursementDAO : DALLogic
    {
        public Disbursement CreateDisbursement(DAL.Disbursement disbursement)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    context.Disbursements.AddObject(disbursement);
                    context.SaveChanges();
                    ts.Complete();
                    return disbursement;
                }
            }
            catch (Exception)
            {
                throw new Exceptions.DisbursmentException("Add object not successful");
            }
        }

        public Disbursement CreateDisbursementFromSRF(StationeryRetrievalForm SRF)
        {
            DAL.Disbursement disbursement;
            disbursement.CreatedBy=
            return disbursement;
        }

        public Boolean IsUnshownDisbursement(Disbursement disbursement)
        {
            bool disbursementStatus = false;

            return disbursementStatus;
        }

        public void CancelDisbursement()
        {
            throw new System.NotImplementedException();
        }

        public Disbursement UpdateDisbursementQuantity(DAL.Disbursement disbursement, int newQuantity)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    DAL.DisbursementItem tempDisbursementItem = (from d in context.DisbursementItems
                                                                 where d.DisbursementID == disbursement.DisbursementID
                                                                 select d).FirstOrDefault<DAL.DisbursementItem>();
                    tempDisbursementItem.QuantityDisbursed = newQuantity;
                    context.ObjectStateManager.ChangeObjectState(tempDisbursementItem, System.Data.EntityState.Modified);
                    context.SaveChanges();
                    ts.Complete();
                    return disbursement;
                }
            }
            catch (Exception)
            {
                throw new Exceptions.DisbursmentException("Update object not successful");
            }
            
        }
    }
}
