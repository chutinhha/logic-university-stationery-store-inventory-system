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
using System.Data;


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

        public Disbursement UpdateDisbursement(DAL.Disbursement disbursement)
        {
            try
            {
                Disbursement tempDisbursement = (from d in context.Disbursements
                                                 where d.DisbursementID == disbursement.DisbursementID
                                                 select d).First<Disbursement>();
                tempDisbursement.CreatedBy = disbursement.CreatedBy;
                tempDisbursement.DateCreated = disbursement.DateCreated;
                using (TransactionScope ts = new TransactionScope())
                {
                    context.Attach(tempDisbursement);
                    context.ObjectStateManager.ChangeObjectState(tempDisbursement, EntityState.Modified);
                    context.SaveChanges();
                    ts.Complete();
                    return tempDisbursement;
                }
            }
            catch(Exception)
            {
                throw new Exceptions.DisbursmentException("Update object not successful");
            }
        }

        public void DeleteDisbursement(DAL.Disbursement disbursement)
        {
            try
            {
                Disbursement persistedDisbursement = (from d in context.Disbursements
                                                      where d.DisbursementID == disbursement.DisbursementID
                                                      select d).FirstOrDefault();
                using (TransactionScope ts = new TransactionScope())
                {
                    context.Disbursements.DeleteObject(persistedDisbursement);
                    context.SaveChanges();
                    ts.Complete();
                }
            }
            catch (Exception)
            {
                throw new Exceptions.DisbursmentException("Delete object not successful");
            }
        }

        public List<Disbursement> GetAllDisbursement()
        {
            return (from d in context.Disbursements
                    select d).ToList();
        }

        public Disbursement GetDisbursementByID(int disbursementID)
        {
            return (from d in context.Disbursements
                    where d.DisbursementID == disbursementID
                    select d).FirstOrDefault();
        }

        public List<Disbursement> FindDisbursementByCriteria(DTO.DisbursementSearchDTO criteria) 
        {
            var Query =
                from d in context.Disbursements
                where d.DisbursementID == (criteria.DisbursementID == 0 ? d.DisbursementID : criteria.DisbursementID)
                && d.CreatedBy == (criteria.CreatedBy == 0 ? d.CreatedBy : criteria.CreatedBy)
                && d.DateCreated == (criteria.DateCreated == null ? d.DateCreated : criteria.DateCreated)
                select d;
            List<Disbursement> disbursements = Query.ToList<Disbursement>();
            return disbursements;
        }


        /*public Disbursement CreateDisbursementFromSRF(StationeryRetrievalForm SRF)
        {
            DAL.Disbursement disbursement;
            System.DateTime currentTime=new System.DateTime();
            currentTime=System.DateTime.Now;
            string UserName =
            disbursement.CreatedBy =
            disbursement.DateCreated = currentTime;
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
        }*/

        /*public Disbursement UpdateDisbursementQuantity(DAL.Disbursement disbursement, int newQuantity)
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
            
        }*/
    }
}
