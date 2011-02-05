/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Data;
using System.Transactions;
using System.Web;
using System.ComponentModel;
using System.Linq;
using SA33.Team12.SSIS.DAL;
using System.Collections.Generic;
using System.Data.Objects;
using SA33.Team12.SSIS.DAL.DTO;

namespace SA33.Team12.SSIS.DAL
{
    /// <summary>
    /// Stationery Retrieval DAO Class
    /// </summary>
    public class StationeryRetrievalDAO : DALLogic
    {
        #region StationeryRetrievalForm
        /// <summary>
        /// Create Stationery from all requisitions that need to be processed
        /// </summary>
        /// <param name="createdBy">User who is creating the Stationery Retrieval</param>
        /// <param name="requisitions">List of requisitions Id separated by comma</param>
        /// <returns></returns>
        public StationeryRetrievalForm CreateStationeryRetrievalForm(User createdBy, bool allRequisition, String requisitions)
        {
            try
            {
                ObjectParameter newSRFId = new ObjectParameter("NewSRFID", typeof(int));
                ObjectParameter message = new ObjectParameter("Message", typeof(string));

                int errorCode = context.CreateStationeryRetrievalFormByAllRequisitions(
                    createdBy.UserID, allRequisition, requisitions, newSRFId, message);
                
                if (errorCode == -1)
                    throw new Exceptions.StationeryRetrievalException(message.Value.ToString());
                else
                {
                    return GetStationeryRetrievalFormByID((int) newSRFId.Value);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public StationeryRetrievalForm SetRecommendedQuantity(int stationeryRetrievalFormID)
        {
            try
            {
                // ObjectParameter srfID = new ObjectParameter("srfID", typeof(int));

                int errorCode = context.SetRecommendedQuantity(stationeryRetrievalFormID);

                if (errorCode == -1)
                    throw new Exceptions.StationeryRetrievalException();
                else
                {
                    return GetStationeryRetrievalFormByID(stationeryRetrievalFormID);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Create a new stationery retrieval form
        /// </summary>
        /// <param name="stationeryRetrievalForm">stationeryRetrievalForm object</param>
        public void CreateStationeryRetrievalForm(StationeryRetrievalForm stationeryRetrievalForm)
        {
            try
            {
                context.AddToStationeryRetrievalForms(stationeryRetrievalForm);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update Received Quantity in stationery retrieval form
        /// </summary>
        /// <param name="stationeryRetrievalForm">stationeryRetrievalForm object</param>
        public StationeryRetrievalForm UpdateReceivedQuantity(StationeryRetrievalForm stationeryRetrievalForm)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var temp = (from srf in context.StationeryRetrievalForms
                                where srf.StationeryRetrievalFormID == stationeryRetrievalForm.StationeryRetrievalFormID
                                select srf).FirstOrDefault<StationeryRetrievalForm>();
                    temp.IsRetrieved = true;
                    context.ObjectStateManager.ChangeObjectState(temp, EntityState.Modified);
                    context.SaveChanges();
                    foreach (StationeryRetrievalFormItem srfi in temp.StationeryRetrievalFormItems)
                    {
                        UpdateStationeryRetrievalFormItem(srfi);
                    }
                    ts.Complete();
                    return temp;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Update Actual Quantity in stationery retrieval form
        /// </summary>
        /// <param name="stationeryRetrievalForm">stationeryRetrievalForm object</param>
        public StationeryRetrievalForm UpdateActualQuantity(StationeryRetrievalForm stationeryRetrievalForm)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var temp = (from srf in context.StationeryRetrievalForms
                                where srf.StationeryRetrievalFormID == stationeryRetrievalForm.StationeryRetrievalFormID
                                select srf).FirstOrDefault<StationeryRetrievalForm>();
                    temp.IsCollected = true;
                    context.SaveChanges();
                    foreach (StationeryRetrievalFormItem srfi in temp.StationeryRetrievalFormItems)
                    {
                        foreach (StationeryRetrievalFormItemByDept srfid in srfi.StationeryRetrievalFormItemByDepts)
                        {
                            UpdateStationeryRetrievalFormItemByDept(srfid);
                        }
                    }
                    ts.Complete();
                    return temp;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Get All StationeryRetrievalForms
        /// </summary>
        /// <returns>List of StationeryRetrievalForm objects</returns>
        public List<StationeryRetrievalForm> GetAllStationeryRetrievalForms()
        {
            try
            {
                return context.StationeryRetrievalForms.ToList<StationeryRetrievalForm>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get StationeryRetrievalForm by primary key
        /// </summary>
        /// <param name="stationeryRetrievalForm">stationeryRetrievalForm object</param>
        /// <returns>stationeryRetrievalForm object</returns>
        public StationeryRetrievalForm GetStationeryRetrievalFormByID(int stationeryRetrievalFormID)
        {
            try
            {
                return GetAllStationeryRetrievalForms().
                    Where(srf => srf.StationeryRetrievalFormID == stationeryRetrievalFormID).FirstOrDefault<StationeryRetrievalForm>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Find stationeryRetrievalForms by Criteria
        /// </summary>
        /// <returns>stationeryRetrievalForm object</returns>
        public List<StationeryRetrievalForm> FindStationeryRetrievalFormByCriteria(DTO.StationeryRetrievalFormSearchDTO criteria)
        {
            return (from s in context.StationeryRetrievalForms
                    where
                        s.StationeryRetrievalFormID ==
                        (criteria.StationeryRetrievalFormID == 0
                             ? s.StationeryRetrievalFormID
                             : criteria.StationeryRetrievalFormID)
                        && s.IsRetrieved == (criteria.IsRetrieved == null ? s.IsRetrieved : criteria.IsRetrieved)
                        && s.IsCollected == (criteria.IsCollected == null ? s.IsCollected : criteria.IsCollected)
                        &&
                        s.IsDistributed == (criteria.IsDistributed == null ? s.IsDistributed : criteria.IsDistributed)
                        &&
                        (EntityFunctions.DiffDays(s.DateRetrieved,
                                                  (criteria.StartDateRetrieved == null ||
                                                   criteria.StartDateRetrieved == DateTime.MinValue
                                                       ? s.DateRetrieved
                                                       : criteria.StartDateRetrieved)) <= 0
                         &&
                         EntityFunctions.DiffDays(s.DateRetrieved,
                                                  (criteria.EndDateRetrieved == null ||
                                                   criteria.EndDateRetrieved == DateTime.MinValue
                                                       ? s.DateRetrieved
                                                       : criteria.EndDateRetrieved)) >= 0)
                        &&
                        (EntityFunctions.DiffDays(s.DateRetrieved,
                                                  (criteria.ExactDateRetrieved == null ||
                                                   criteria.ExactDateRetrieved == DateTime.MinValue
                                                       ? s.DateRetrieved
                                                       : criteria.ExactDateRetrieved)) == 0)

                    select s).ToList();
        }
        #endregion

        #region StationeryRetrievalFormItem
        /// <summary>
        /// Create a new Stationery Retrieval FormItem
        /// </summary>
        /// <param name="stationeryRetrievalFormItem">stationeryRetrievalFormItem object</param>
        public void CreateStationeryRetrievalFormItem(StationeryRetrievalFormItem stationeryRetrievalFormItem)
        {
            try
            {
                context.AddToStationeryRetrievalFormItems(stationeryRetrievalFormItem);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Update the Stationery Retrieval FormItem
        /// </summary>
        /// <param name="stationeryRetrievalFormItem">stationeryRetrievalFormItem object</param>
        public StationeryRetrievalFormItem UpdateStationeryRetrievalFormItem(StationeryRetrievalFormItem stationeryRetrievalFormItem)
        {
            try
            {
                var temp = (from srfi in context.StationeryRetrievalFormItems
                            where srfi.StationeryRetrievalFormItemID == stationeryRetrievalFormItem.StationeryRetrievalFormItemID
                            select srfi).First<StationeryRetrievalFormItem>();

                temp.QuantityRetrieved = stationeryRetrievalFormItem.QuantityRetrieved;

                context.SaveChanges();
                return temp;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get All Stationery Retrieval FormItems
        /// </summary>
        /// <returns>List of StationeryRetrievalFormItem objects</returns>
        public List<StationeryRetrievalFormItem> GetAllStationeryRetrievalFormItems()
        {
            try
            {
                return context.StationeryRetrievalFormItems.ToList<StationeryRetrievalFormItem>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get Stationery Retrieval FormItem by primary key
        /// </summary>
        /// <param name="stationeryRetrievalFormItem">stationeryRetrievalFormItem object</param>
        /// <returns>List of StationeryRetrievalFormItem objects</returns>
        public StationeryRetrievalFormItem GetStationeryRetrievalFormItemByID(StationeryRetrievalFormItem stationeryRetrievalFormItem)
        {
            try
            {
                return GetAllStationeryRetrievalFormItems().
                    Where(srfi => srfi.StationeryRetrievalFormItemID == stationeryRetrievalFormItem.StationeryRetrievalFormItemID)
                    .FirstOrDefault<StationeryRetrievalFormItem>();
            }
            catch (Exception)
            {

                throw;
            }
        } 

        public List<StationeryRetrievalFormItem> FindStationeryRetrievalFormItemsByCriteria(StationeryRetrievalFormItemSearchDTO criteria)
        {
            var Query = from srfi in context.StationeryRetrievalFormItems
                        where
                            srfi.StationeryRetrievalFormID ==
                            (criteria.StationeryRetrievalFormID == 0
                                 ? srfi.StationeryRetrievalFormID
                                 : criteria.StationeryRetrievalFormID)
                            &&
                            srfi.StationeryRetrievalFormItemID ==
                            (criteria.StationeryRetrievalFormItemID == 0
                                 ? srfi.StationeryRetrievalFormItemID
                                 : criteria.StationeryRetrievalFormItemID)
                            &&
                            srfi.StationeryID ==
                            (criteria.StationeryID == 0 ? srfi.StationeryID : criteria.StationeryID)
                        select srfi;
            return Query.ToList<StationeryRetrievalFormItem>();
        }
        #endregion

        #region StationeryRetrievalFormItemByDept
        /// <summary>
        /// Create a new Stationery Retrieval FormItemByDept
        /// </summary>
        /// <param name="stationeryRetrievalFormItemByDept">stationeryRetrievalFormItemByDept object</param>
        public void CreateStationeryRetrievalFormItemByDept(StationeryRetrievalFormItemByDept stationeryRetrievalFormItemByDept)
        {
            try
            {
                context.AddToStationeryRetrievalFormItemByDepts(stationeryRetrievalFormItemByDept);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Update Stationery Retrieval FormItemByDept
        /// </summary>
        /// <param name="stationeryRetrievalFormItemByDept">stationeryRetrievalFormItemByDept object</param>
        public void UpdateStationeryRetrievalFormItemByDept(StationeryRetrievalFormItemByDept stationeryRetrievalFormItemByDept)
        {
            try
            {
                var temp = (from x in context.StationeryRetrievalFormItemByDepts
                            where x.StationeryRetrievalFormItemByDeptID == stationeryRetrievalFormItemByDept.StationeryRetrievalFormItemByDeptID
                            select x).FirstOrDefault<StationeryRetrievalFormItemByDept>();

                temp.QuantityActual = stationeryRetrievalFormItemByDept.QuantityActual;
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get All Stationery Retrieval FormItemByDept
        /// </summary>
        /// <returns>List of StationeryRetrievalFormItemByDept objects</returns>
        public List<StationeryRetrievalFormItemByDept> GetAllStationeryRetrievalFormItemByDept()
        {
            try
            {
                return context.StationeryRetrievalFormItemByDepts.ToList<StationeryRetrievalFormItemByDept>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get Stationery Retrieval FormItemByDept by primary key
        /// </summary>
        /// <param name="stationeryRetrievalFormItemByDept">StationeryRetrievalFormItemByDept object</param>
        /// <returns>StationeryRetrievalFormItemByDept object</returns>
        public StationeryRetrievalFormItemByDept GetStationeryRetrievalFormItemByDeptByID(StationeryRetrievalFormItemByDept stationeryRetrievalFormItemByDept)
        {
            try
            {
                return GetAllStationeryRetrievalFormItemByDept()
                    .Where(x => x.StationeryRetrievalFormItemByDeptID == stationeryRetrievalFormItemByDept.StationeryRetrievalFormItemByDeptID)
                    .FirstOrDefault<StationeryRetrievalFormItemByDept>();
            }
            catch (Exception)
            {

                throw;
            }
        } 

        public List<StationeryRetrievalFormItemByDept> GetStationeryRetrievalFormItemByDeptByFormID(int stationeryRetrievalFormID)
        {
            var result = from dept in context.StationeryRetrievalFormItemByDepts
                         where
                             context.StationeryRetrievalFormItems.Any(
                                 item =>
                                 item.StationeryRetrievalFormItemID ==
                                 dept.StationeryRetrievalFormItem.StationeryRetrievalFormItemID &&
                                 item.StationeryRetrievalFormID == stationeryRetrievalFormID)
                         select dept;
            return result.ToList<StationeryRetrievalFormItemByDept>();
        }

        public List<vw_GetStationeryRetrievalFormItemByDept> GetVwStationeryRetrievalFormItemByDeptByFormID(int stationeryRetrievalFormID)
        {
            return (from d in context.vw_GetStationeryRetrievalFormItemByDept
                    where d.StationeryRetrievalFormID == stationeryRetrievalFormID
                    orderby d.StationeryID, d.SpecialStationeryID, d.UrgencyLevel
                    select d).ToList();
        }        
        #endregion

        #region StationeryRetrievalFormByRequisition
        /// <summary>
        /// Create Stationery Retrieval Form By Requisition
        /// </summary>
        /// <param name="stationeryRetrievalFormByRequisition">StationeryRetrievalFormByRequisition object</param>
        public void CreateStationeryRetrievalFormByRequisition(StationeryRetrievalFormByRequisition stationeryRetrievalFormByRequisition)
        {
            try
            {
                context.AddToStationeryRetrievalFormByRequisitions(stationeryRetrievalFormByRequisition);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update Stationery Retrieval Form By Requisition
        /// </summary>
        /// <param name="stationeryRetrievalFormByRequisition">StationeryRetrievalFormByRequisition object</param>
        public void UpdateStationeryRetrievalFormByRequisition(StationeryRetrievalFormByRequisition stationeryRetrievalFormByRequisition)
        {
            try
            {
                var temp = (from x in context.StationeryRetrievalFormByRequisitions
                            where x.StationeryRetrievalFormByRequisitionID == stationeryRetrievalFormByRequisition.StationeryRetrievalFormByRequisitionID
                            select x).FirstOrDefault<StationeryRetrievalFormByRequisition>();

                temp.RequisitionID = stationeryRetrievalFormByRequisition.RequisitionID;
                temp.StationeryRetrievalFormItemByDeptID = stationeryRetrievalFormByRequisition.StationeryRetrievalFormItemByDeptID;

                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get All Stationery Retrieval Form By Requisitions
        /// </summary>
        /// <returns>List of StationeryRetrievalFormByRequisition objects</returns>
        public List<StationeryRetrievalFormByRequisition> GetAllStationeryRetrievalFormByRequisitions()
        {
            try
            {
                return context.StationeryRetrievalFormByRequisitions.ToList<StationeryRetrievalFormByRequisition>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get Stationery Retrieval FormByRequistion by primary key
        /// </summary>
        /// <param name="stationeryRetrievalFormByRequisition">StationeryRetrievalFormByRequisition object</param>
        /// <returns>StationeryRetrievalFormByRequisition object</returns>
        public StationeryRetrievalFormByRequisition GetStationeryRetrievalFormByRequisitionByID(StationeryRetrievalFormByRequisition stationeryRetrievalFormByRequisition)
        {
            try
            {
                return GetAllStationeryRetrievalFormByRequisitions()
                    .Where(x => x.StationeryRetrievalFormByRequisitionID == stationeryRetrievalFormByRequisition.StationeryRetrievalFormByRequisitionID)
                    .FirstOrDefault<StationeryRetrievalFormByRequisition>();
            }
            catch (Exception)
            {

                throw;
            }
        } 
        #endregion
    }
}
