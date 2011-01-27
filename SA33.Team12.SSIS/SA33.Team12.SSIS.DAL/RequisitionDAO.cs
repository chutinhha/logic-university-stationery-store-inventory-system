/***
 * Author: Sundar Aravind (A0076790U)
 * Initial Implementation: 23/Jan/2011
 * Modified on: 25/Jan/2011
 ***/

using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;
using System.Transactions;
using SA33.Team12.SSIS.Exceptions;

namespace SA33.Team12.SSIS.DAL
{
    /// <summary>
    /// RequisitionDAO Class
    /// </summary>
    public class RequisitionDAO : DALLogic
    {
        #region Requisition
        /// <summary>
        /// Create a new requisition and persist with database
        /// </summary>
        /// <param name="requisition">requisition object</param>
        public void CreateRequisition(Requisition requisition)
        {
            try
            {
                //Create a transaction scope
                using (TransactionScope ts = new TransactionScope())
                {
                    //Add requisition to context                
                    context.AddToRequisitions(requisition);

                    //Get the status id for "Pending"
                    var status = (from s in context.Statuses where s.Name == "Pending" select s).FirstOrDefault<Status>();

                    //Update the status id for the new requistion to "Pending"
                    UpdateRequisitionStatus(requisition, status);

                    //Save the changes
                    context.SaveChanges();

                    //Notify Transaction completed
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                //Exception thrown incase if insert fails
                throw new RequisitionException("Create requisition failed." +ex.Message);
              
            }
        }

        /// <summary>
        /// Update the requisition before approval
        /// </summary>
        /// <param name="updateRequisition">requisition object</param>
        public void UpdateRequisition(Requisition updateRequisition)
        {
            try
            {
                var tempReq = (from r in context.Requisitions
                               where r.RequisitionID == updateRequisition.RequisitionID
                               select r).FirstOrDefault<Requisition>();

                tempReq = updateRequisition;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RequisitionException("Update requisition failed." + ex.Message);
            }
        }

        /// <summary>
        /// Update the requisition status
        /// </summary>
        /// <param name="requisition">requistion object</param>
        /// <param name="status">status object</param>
        public void UpdateRequisitionStatus(Requisition requisition, Status status)
        {
            try
            {
                requisition.StatusID = status.StatusID;
            }
            catch (Exception ex)
            {
                throw new RequisitionException("Update failed.");
            }
        }

        /// <summary>
        /// Approval of the requisition
        /// </summary>
        /// <param name="requisition">requisition object</param>
        public void ApproveRequisition(Requisition requisition)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var tempRequisition = (from p in context.Requisitions
                                           where p.RequisitionID == requisition.RequisitionID
                                           select p).FirstOrDefault<Requisition>();


                    var status = (from s in context.Statuses where s.Name == "Approved & Pending" select s).FirstOrDefault<Status>();

                    //requisition status will be changed to "Approved & Pending"
                    UpdateRequisitionStatus(tempRequisition, status);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new RequisitionException("Approval Failed.");
            }
        }

        /// <summary>
        /// Approve all the requisitions
        /// </summary>
        /// <param name="requisitions">requisition collection</param>
        public void ApproveRequisition(List<Requisition> requisitions)
        {
            if (requisitions != null)
            {
                foreach (Requisition tempReq in requisitions)
                {
                    ApproveRequisition(tempReq);
                }
            }
        }

        /// <summary>
        /// Cancel the requisition before approval
        /// </summary>
        /// <param name="cancelRequisition">requisition object</param>
        public void CancelRequisition(Requisition cancelRequisition)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var tempRequisition = (from p in context.Requisitions
                                           where p.RequisitionID == cancelRequisition.RequisitionID
                                           select p).FirstOrDefault<Requisition>();

                    var status = (from s in context.Statuses where s.Name == "Cancelled" select s).FirstOrDefault<Status>();

                    UpdateRequisitionStatus(tempRequisition, status);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw new RequisitionException("Approval Failed");
            }
        }

        /// <summary>
        /// Get All Requistions
        /// </summary>
        /// <returns></returns>
        public List<Requisition> GetAllRequisition()
        {
            return (from c in context.Requisitions select c).ToList<Requisition>();
        }

        /// <summary>
        /// Get Requistions by category
        /// </summary>
        /// <param name="category">category object</param>
        /// <param name="requisitionSearchDTO">requisitionSearchDTO object</param>
        /// <returns></returns>
        public List<Requisition> GetRequisitionByCategory(Category category, RequisitionSearchDTO requisitionSearchDTO)
        {
            return null;
        }

        /// <summary>
        /// Get Requisitions by department
        /// </summary>
        /// <param name="department">department object</param>
        /// <param name="requisitionSearchDTO">requisitionSearchDTO object</param>
        /// <returns></returns>
        public List<Requisition> GetRequisitionByDepartment(Department department, RequisitionSearchDTO requisitionSearchDTO)
        {
            return null;
        }

        /// <summary>
        /// Get Requisitions by Employee
        /// </summary>
        /// <param name="user">user object</param>
        /// <param name="requisitionSearchDTO">requisitionSearchDTO object</param>
        /// <returns></returns>
        public List<Requisition> GetRequisitionByEmployee(User user, RequisitionSearchDTO requisitionSearchDTO)
        {
            return null;
        }

        /// <summary>
        /// Get Requisition by primary key
        /// </summary>
        /// <param name="requisition">requisition object</param>
        /// <returns></returns>
        public Requisition GetRequisitionByID(Requisition requisition)
        {
            return GetAllRequisition().Where(r => r.RequisitionID == requisition.RequisitionID).FirstOrDefault<Requisition>();
        }

        /// <summary>
        /// Find the requisition by search criteria
        /// </summary>
        /// <param name="requisitioinSearchDTO">requisitioinSearchDTO object</param>
        /// <returns></returns>
        public List<Requisition> FindRequisitionByCriteria(RequisitionSearchDTO requisitioinSearchDTO)
        {
            var tempQuery = (from r in context.Requisitions
                             where 1 == 1
                             select r);

            if (requisitioinSearchDTO != null)
            {
                if (requisitioinSearchDTO.RequisitionID != -1)
                {
                    tempQuery = tempQuery.Where(r => r.RequisitionID == requisitioinSearchDTO.RequisitionID);
                }
                if (requisitioinSearchDTO.StartDateRequested != null && requisitioinSearchDTO.EndDateRequested != null && requisitioinSearchDTO.StartDateRequested >= DateTime.MinValue && requisitioinSearchDTO.EndDateRequested >= DateTime.MinValue)
                {
                    tempQuery = tempQuery.Where(r => r.DateRequested >= requisitioinSearchDTO.StartDateRequested && r.DateRequested <= requisitioinSearchDTO.EndDateRequested);
                }

                if (requisitioinSearchDTO.StartDateRequested != null && requisitioinSearchDTO.StartDateRequested >= DateTime.MinValue)
                {
                    tempQuery = tempQuery.Where(r => r.DateRequested == requisitioinSearchDTO.StartDateRequested);
                }

                if (requisitioinSearchDTO.EndDateRequested != null && requisitioinSearchDTO.EndDateRequested >= DateTime.MinValue)
                {
                    tempQuery = tempQuery.Where(r => r.DateRequested == requisitioinSearchDTO.EndDateRequested);
                }
            }

            return (from q in tempQuery select q).ToList<Requisition>();

        }
        
        /// <summary>
        /// Generate the requisitionID for each requisition
        /// </summary>
        /// <param name="requisition"></param>
        /// <returns></returns>
        public string GetRequisitionID(Requisition requisition)
        {
            var department = (from d in context.Departments where d.DepartmentID == requisition.DepartmentID select d).FirstOrDefault<Department>();
            return department.Name + "/" + DateTime.Now.Day + DateTime.Now.Month + "/" + DateTime.Now.Year;
        }
        #endregion

        #region RequisitionItem
        /// <summary>
        /// Create a new requisitionItem
        /// </summary>
        /// <param name="requisitionItem">requisitionItem object</param>
        public void CreateRequisitionItem(RequisitionItem requisitionItem)
        {
            try
            {
                context.AddToRequisitionItems(requisitionItem);
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Update the requisitionItem
        /// </summary>
        /// <param name="requisitionItem">requisitionItem object</param>
        public void UpdateRequisitionItem(RequisitionItem requisitionItem)
        {
            try
            {
                var temp = (from ri in context.RequisitionItems
                            where ri.RequisitionItemID == requisitionItem.RequisitionItemID
                            select ri).FirstOrDefault<RequisitionItem>();
                temp.Stationery = requisitionItem.Stationery;
                temp.QuantityRequested = requisitionItem.QuantityRequested;
                temp.Price = requisitionItem.Price;

                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Delete the requisitionItem
        /// </summary>
        /// <param name="requisitionItem">requisitionItem object</param>
        public void DeleteRequisitionItem(RequisitionItem requisitionItem)
        {
            try
            {
                var temp = (from ri in context.RequisitionItems
                            where ri.RequisitionID == requisitionItem.RequisitionID
                            select ri).FirstOrDefault<RequisitionItem>();

                context.RequisitionItems.DeleteObject(temp);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get All RequisitionItems in the requisition form
        /// </summary>
        /// <param name="requisition"></param>
        /// <returns></returns>
        public List<RequisitionItem> GetAllRequisitionItems(Requisition requisition)
        {
            try
            {
                return (from ri in context.RequisitionItems
                        where ri.RequisitionID == requisition.RequisitionID
                        select ri).ToList<RequisitionItem>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get RequisitionItems by primary key
        /// </summary>
        /// <param name="requisitionItem">requisitionItem object</param>
        /// <returns></returns>
        public RequisitionItem GetAllRequisitionItemsByID(RequisitionItem requisitionItem)
        {
            try
            {
                return (from ri in context.RequisitionItems
                        where ri.RequisitionItemID == requisitionItem.RequisitionItemID
                        select ri).FirstOrDefault<RequisitionItem>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region SpecialRequisitionItem
        /// <summary>
        /// Create a new requisitionItem
        /// </summary>
        /// <param name="requisitionItem">requisitionItem object</param>
        public void CreateSpecialSpecialRequisitionItem(SpecialRequisitionItem specialRequisitionItem)
        {
            try
            {
                context.AddToSpecialRequisitionItems(specialRequisitionItem);
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Update the requisitionItem
        /// </summary>
        /// <param name="requisitionItem">requisitionItem object</param>
        public void UpdateSpecialRequisitionItem(SpecialRequisitionItem specialRequisitionItem)
        {
            try
            {
                var temp = (from ri in context.SpecialRequisitionItems
                            where ri.SpecialRequisitionItemsID == specialRequisitionItem.SpecialRequisitionItemsID
                            select ri).FirstOrDefault<SpecialRequisitionItem>();

                temp.SpecialStationery = specialRequisitionItem.SpecialStationery;
                temp.QuantityRequested = specialRequisitionItem.QuantityRequested;
                temp.Price = specialRequisitionItem.Price;

                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Delete the requisitionItem
        /// </summary>
        /// <param name="requisitionItem">requisitionItem object</param>
        public void DeleteSpecialRequisitionItem(SpecialRequisitionItem requisitionItem)
        {
            try
            {
                var temp = (from ri in context.SpecialRequisitionItems
                            where ri.SpecialRequisitionItemsID == requisitionItem.SpecialRequisitionItemsID
                            select ri).FirstOrDefault<SpecialRequisitionItem>();

                context.SpecialRequisitionItems.DeleteObject(temp);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get All RequisitionItems in the requisition form
        /// </summary>
        /// <param name="requisition"></param>
        /// <returns></returns>
        public List<SpecialRequisitionItem> GetAllSpecialRequisitionItems(Requisition requisition)
        {
            try
            {
                return (from ri in context.SpecialRequisitionItems
                        where ri.RequisitionID == requisition.RequisitionID
                        select ri).ToList<SpecialRequisitionItem>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get RequisitionItems by primary key
        /// </summary>
        /// <param name="requisitionItem">requisitionItem object</param>
        /// <returns></returns>
        public SpecialRequisitionItem GetAllSpecialRequisitionItemsByID(SpecialRequisitionItem specialRequisitionItem)
        {
            try
            {
                return (from ri in context.SpecialRequisitionItems
                        where ri.SpecialRequisitionItemsID == specialRequisitionItem.SpecialRequisitionItemsID
                        select ri).FirstOrDefault<SpecialRequisitionItem>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Status
        public void CreateStatus(Status status)
        {
            try
            {
                context.AddToStatuses(status);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateStatus(Status status)
        {
            try
            {
                Status st = (from s in context.Statuses
                             where s.StatusID == status.StatusID
                             select s).FirstOrDefault<Status>();

                st.Name = status.Name;
                st.Group = status.Group;
                st.Description = status.Description;

                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteStatus(Status status)
        {
            try
            {
                context.Statuses.DeleteObject(status);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Status> GetAllStatuses()
        {
            return (from s in context.Statuses select s).ToList<Status>();
        }

        public Status GetStatusByID(Status status)
        {
            return GetAllStatuses().Where(s => s.StatusID == status.StatusID).FirstOrDefault<Status>();
        }

        public List<Status> GetStatusByCriteria(StatusSearchDTO statusSearchDTO)
        {
            return GetAllStatuses().Where(s => s.Name == statusSearchDTO.Name).ToList<Status>();
        }
        #endregion

        #region Urgency
        public void CreateUrgency(Urgency urgency)
        {
            try
            {
                context.AddToUrgencies(urgency);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateUrgency(Urgency urgency)
        {
            try
            {
                Urgency ur = (from u in context.Urgencies
                              where u.UrgencyID == urgency.UrgencyID
                              select u).FirstOrDefault<Urgency>();

                ur.Name = urgency.Name;
                ur.Level = urgency.Level;

                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteUrgency(Urgency urgency)
        {
            try
            {
                context.Urgencies.DeleteObject(urgency);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Urgency> GetAllUrgencies()
        {
            return (from s in context.Urgencies select s).ToList<Urgency>();
        }

        public Urgency GetUrgencyByID(Urgency urgency)
        {
            return GetAllUrgencies().Where(u => u.UrgencyID == urgency.UrgencyID).FirstOrDefault<Urgency>();
        }

        public List<Urgency> GetUrgencyByCriteria(UrgencySearchDTO urgencySearchDTO)
        {
            return GetAllUrgencies().Where(s => s.Name == urgencySearchDTO.Name).ToList<Urgency>();
        }
        #endregion
    }
}
