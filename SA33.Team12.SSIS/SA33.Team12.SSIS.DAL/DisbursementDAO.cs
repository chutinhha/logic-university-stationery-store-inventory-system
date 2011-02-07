/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Data.Objects;
using System.Web;
using System.ComponentModel;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using SA33.Team12.SSIS.DAL.DTO;


namespace SA33.Team12.SSIS.DAL
{
    public class DisbursementDAO : DALLogic
    {
        #region Disbursement
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
                tempDisbursement.DisbursementID = disbursement.DisbursementID;
                tempDisbursement.CreatedBy = disbursement.CreatedBy;
                tempDisbursement.DateCreated = disbursement.DateCreated;
                tempDisbursement.StationeryRetrievalFormID = disbursement.StationeryRetrievalFormID;
                using (TransactionScope ts = new TransactionScope())
                {
                    //context.Attach(tempDisbursement);
                    //context.ObjectStateManager.ChangeObjectState(tempDisbursement, EntityState.Modified);
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
                      && d.DepartmentID == (criteria.DepartmentID == 0 ? d.DepartmentID : criteria.DepartmentID)
                      && d.CreatedBy == (criteria.CreatedBy == 0 ? d.CreatedBy : criteria.CreatedBy)
                      &&
                      d.DateCreated ==
                      (criteria.DateCreated == null || criteria.DateCreated == DateTime.MinValue
                           ? d.DateCreated
                           : criteria.DateCreated)
                      &&
                      d.StationeryRetrievalFormID ==
                      (criteria.StationeryRetrievalFormID == 0
                           ? d.StationeryRetrievalFormID
                           : criteria.StationeryRetrievalFormID)
                select d;
            List<Disbursement> disbursements = Query.ToList<Disbursement>();
            return disbursements;
        }

        public List<Disbursement> CreateDisbursementBySRF(User createdBy, int stationeryRetrievalFormId)
        {
            try
            {
                ObjectParameter message = new ObjectParameter("Message", typeof(string));

                int errorCode = context.CreateDisbursementsBySRFId(stationeryRetrievalFormId,
                                                                   createdBy.UserID, message);

                if (errorCode == -1)
                    throw new Exceptions.StationeryRetrievalException(message.Value.ToString());
                else
                {
                    return
                        FindDisbursementByCriteria(new DisbursementSearchDTO()
                            {
                                StationeryRetrievalFormID = stationeryRetrievalFormId
                            });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<vw_GetStationeryDistributionList> GetDistributionListByDisbursementID(int disbursementId)
        {
            List<vw_GetStationeryDistributionList> distributionLists = context.vw_GetStationeryDistributionList.ToList();
            var Query = from d in distributionLists
                        where d.DisbursementID == disbursementId
                        select d;
            return Query.ToList();
        }

        #endregion

        #region DisbursementItem
        public DisbursementItem CreateDisbursementItem(DAL.DisbursementItem disbursementItem)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    context.DisbursementItems.AddObject(disbursementItem);
                    context.SaveChanges();
                    ts.Complete();
                    return disbursementItem;
                }
            }
            catch (Exception)
            {
                throw new Exceptions.DisbursmentItemException("Add object not successful");
            }
        }

        public DisbursementItem UpdateDisbursementItem(DAL.DisbursementItem disbursementItem)
        {
            try
            {
                DisbursementItem tempDisbursementItem = (from d in context.DisbursementItems
                                                         where d.DisbursementItemID == disbursementItem.DisbursementItemID
                                                         select d).FirstOrDefault();
                if (tempDisbursementItem == null)
                {
                    tempDisbursementItem.DisbursementItemID = disbursementItem.DisbursementItemID;
                    tempDisbursementItem.DisbursementID = disbursementItem.DisbursementID;
                    tempDisbursementItem.StationeryRetrievalFormItemByDeptID = disbursementItem.StationeryRetrievalFormItemByDeptID;
                    tempDisbursementItem.AdjustmentVoucherID = disbursementItem.AdjustmentVoucherID;
                    tempDisbursementItem.StationeryID = disbursementItem.StationeryID;
                    tempDisbursementItem.SpecialStationeryID = disbursementItem.SpecialStationeryID;
                    tempDisbursementItem.QuantityDisbursed = disbursementItem.QuantityDisbursed;
                    tempDisbursementItem.QuantityDamaged = disbursementItem.QuantityDamaged;
                    tempDisbursementItem.Reason = disbursementItem.Reason;
                }
                using (TransactionScope ts = new TransactionScope())
                {
                    //context.Attach(tempDisbursementItem);
                    //context.ObjectStateManager.ChangeObjectState(tempDisbursementItem, EntityState.Modified);
                    context.SaveChanges();
                    ts.Complete();
                    return tempDisbursementItem;
                }
            }
            catch
            {
                throw new Exceptions.DisbursmentItemException("Update object not successful");
            }
        }

        public void DeleteDisbursementItem(DAL.DisbursementItem disbursementItem)
        {
            try
            {
                DisbursementItem persistedDisbursementItem = (from d in context.DisbursementItems
                                                              where d.DisbursementItemID == disbursementItem.DisbursementItemID
                                                              select d).FirstOrDefault();
                using (TransactionScope ts = new TransactionScope())
                {
                    context.DisbursementItems.DeleteObject(persistedDisbursementItem);
                    context.SaveChanges();
                    ts.Complete();
                }
            }
            catch
            {
                throw new Exceptions.DisbursmentItemException("Delete object not successful");
            }
        }

        public List<DisbursementItem> GetAllDisbursementItem()
        {
            return (from d in context.DisbursementItems
                    select d).ToList();
        }

        public DisbursementItem GetDisbursementItemByID(int disbursementItemID)
        {
            return (from d in context.DisbursementItems
                    where d.DisbursementItemID == disbursementItemID
                    select d).FirstOrDefault();
        }

        public List<DisbursementItem> FindDisbursementItemsByCriteria(DTO.DisbursementItemSearchDTO criteria)
        {
            var Query =
                from d in context.DisbursementItems
                where d.DisbursementItemID == (criteria.DisbursementItemID == 0 ? d.DisbursementItemID : criteria.DisbursementItemID)
                && d.DisbursementID == (criteria.DisbursementID == 0 ? d.DisbursementID : criteria.DisbursementID)
                && d.StationeryRetrievalFormItemByDeptID == (criteria.StationeryRetrievalFormItemByDeptID == 0 ? d.StationeryRetrievalFormItemByDeptID : criteria.StationeryRetrievalFormItemByDeptID)
                && d.AdjustmentVoucherID == (criteria.AdjustmentVoucherID == 0 ? d.AdjustmentVoucherID : criteria.AdjustmentVoucherID)
                && d.StationeryID == (criteria.StationeryID == 0 ? d.StationeryID : criteria.StationeryID)
                && d.SpecialStationeryID == (criteria.SpecialStationeryID == 0 ? d.SpecialStationeryID : criteria.SpecialStationeryID)
                && d.QuantityDisbursed == (criteria.QuantityDisbursed == 0 ? d.QuantityDisbursed : criteria.QuantityDisbursed)
                && d.QuantityDamaged == (criteria.QuantityDamaged == 0 ? d.QuantityDamaged : criteria.QuantityDamaged)
                && d.Reason == (criteria.Reason == null || criteria.Reason == "" ? d.Reason : criteria.Reason)
                select d;
            List<DisbursementItem> disbursementItems = Query.ToList<DisbursementItem>();
            return disbursementItems;
        }
        #endregion
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
