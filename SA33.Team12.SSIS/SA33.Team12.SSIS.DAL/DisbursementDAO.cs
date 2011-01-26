/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Web;
using System.ComponentModel;
using System.Transactions;
using System.Collections.Generic;

namespace SA33.Team12.SSIS.DAL
{
    public class DisbursementDAO : DALLogic
    {
        public Disbursement CreateDisbursement(Disbursement disbursement)
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

        public Boolean IsUnshownDisbursement(Disbursement disbursement)
        {
            bool disbursementStatus = false;

            return disbursementStatus;
        }

        public void CreateDisbursementFromSRF()
        {
            throw new System.NotImplementedException();
        }

        public void CancelDisbursement()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateDisbursement()
        {
            throw new System.NotImplementedException();
        }
    }
}
