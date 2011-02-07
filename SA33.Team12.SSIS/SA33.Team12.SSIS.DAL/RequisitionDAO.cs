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
using System.Data.Objects;

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
                    if (requisition != null)
                    {
                        //Add requisition to context                
                        context.AddToRequisitions(requisition);

                        //Save the changes
                        context.SaveChanges();

                        //Notify Transaction completed
                        ts.Complete();
                    }
                }
                
            }
            catch (Exception ex)
            {
                //Exception thrown incase if insert fails
                throw new RequisitionException("Create requisition failed." +ex.Message);
              
            }
        }

        /// <summary>
        /// 
        /// Update the requisition before approval
        /// </summary>
        /// <param name="updateRequisition">requisition object</param>
        public void UpdateRequisition(Requisition updateRequisition)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var tempReq = (from r in context.Requisitions
                                   where r.RequisitionID == updateRequisition.RequisitionID
                                   select r).FirstOrDefault<Requisition>();

                    tempReq = updateRequisition;
                    context.SaveChanges();
                    ts.Complete();
                }
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
                requisition.DateApproved = DateTime.Now;                
            }
            catch (Exception)
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
                    ts.Complete();
                }
            }
            catch (Exception)
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
        /// Reject of the requisition
        /// </summary>
        /// <param name="requisition">requisition object</param>
        public void RejectRequisition(Requisition requisition)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var tempRequisition = (from p in context.Requisitions
                                           where p.RequisitionID == requisition.RequisitionID
                                           select p).FirstOrDefault<Requisition>();


                    var status = (from s in context.Statuses where s.Name == "Rejected" select s).FirstOrDefault<Status>();

                    //requisition status will be changed to "Rejected"
                    UpdateRequisitionStatus(tempRequisition, status);
                    context.SaveChanges();
                    ts.Complete();
                }
            }
            catch (Exception)
            {
                throw new RequisitionException("Reject request Failed.");
            }
        }

        /// <summary>
        /// Reject all the requisitions
        /// </summary>
        /// <param name="requisitions">requisition collection</param>
        public void RejectRequisition(List<Requisition> requisitions)
        {
            if (requisitions != null)
            {
                foreach (Requisition tempReq in requisitions)
                {
                    RejectRequisition(tempReq);
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
                    ts.Complete();
                }
            }
            catch (Exception)
            {

                throw new RequisitionException("Approval Failed");
            }
        }

        /// <summary>
        /// Cancel all the requisitions
        /// </summary>
        /// <param name="requisitions">requisition collection</param>
        public void CancelRequisition(List<Requisition> requisitions)
        {
            if (requisitions != null)
            {
                foreach (Requisition tempReq in requisitions)
                {
                    CancelRequisition(tempReq);
                }
            }
        }

        /// <summary>
        /// Get All Requistions
        /// </summary>
        /// <returns></returns>
        public List<Requisition> GetAllRequisition()
        {
            try
            {
                return (from r in context.Requisitions select r).ToList<Requisition>();
            }
            catch (Exception)
            {
                
                throw;
            }            
        }

        /// <summary>
        /// Get All Requistions by Employee ID
        /// </summary>
        /// <returns></returns>
        public List<Requisition> GetAllRequisition(int EmployeeID, RequisitionSearchDTO reqSearchDTO)
        {
            try
            {
                if (reqSearchDTO != null)
                {
                    return GetAllRequisition().Where(x => x.CreatedBy == EmployeeID && x.DateRequested.Month == reqSearchDTO.ExactDateRequested.Month && x.DateRequested.Year == reqSearchDTO.ExactDateRequested.Year).ToList<Requisition>();

                }
                return GetAllRequisition().Where(x => x.CreatedBy == EmployeeID).ToList<Requisition>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get Requistion by primary key
        /// </summary>
        /// <returns></returns>
        public Requisition GetRequisitionByID(int RequisitionID)
        {
            try
            {
                return GetAllRequisition().Where(t => t.RequisitionID == RequisitionID).FirstOrDefault<Requisition>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get All Approved Requistions
        /// </summary>
        /// <returns></returns>
        public List<Requisition> GetAllUnApprovedRequisitionByDepartmentID(int departmentID)
        {
            try
            {
                return GetAllRequisition().Where(t => (t.ApprovedBy == 0 || t.ApprovedByUser == null) && t.DepartmentID == departmentID && t.Status.Name != "Cancelled").ToList<Requisition>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get All Requistions by category
        /// </summary>
        /// <returns></returns>
        public List<VW_RequisitionsByCategory> GetAllRequisitionByCategory()
        {
            try
            {
                return context.VW_RequisitionsByCategory.ToList<VW_RequisitionsByCategory>();
            }
            catch (Exception)
            {
                
                throw;
            }            
        }

        /// <summary>
        /// Get Requistions by category filter
        /// </summary>
        /// <param name="category">category object</param>
        /// <param name="requisitionSearchDTO">requisitionSearchDTO object</param>
        /// <returns>List of Requisitions by category</returns>
        public List<VW_RequisitionsByCategory> GetRequisitionByCategoryID(Category category, RequisitionSearchDTO requisitionSearchDTO)
        {
            try
            {
                return GetAllRequisitionByCategory()
                .Where(ri => ri.CategoryID == (category.CategoryID == 0 ? ri.CategoryID : category.CategoryID))                
                 .ToList<VW_RequisitionsByCategory>();
            }
            catch (Exception)
            {
                
                throw;
            }            
        }

        /// <summary>
        /// Get All Requistions by department
        /// </summary>
        /// <returns>List of Requisitions by department</returns>
        public List<VW_RequisitionsByDepartment> GetAllRequisitionByDepartment()
        {
            try
            {
                return context.VW_RequisitionsByDepartment.ToList<VW_RequisitionsByDepartment>();
            }
            catch (Exception)
            {
                
                throw;
            }            
        }

        /// <summary>
        /// Get Requisitions by department
        /// </summary>
        /// <param name="department">department object</param>
        /// <param name="requisitionSearchDTO">requisitionSearchDTO object</param>
        /// <returns>List of VW_RequisitionsByDepartment objects</returns>
        public List<VW_RequisitionsByDepartment> GetRequisitionByDepartmentID(Department department, RequisitionSearchDTO requisitionSearchDTO)
        {
            try
            {
                return GetAllRequisitionByDepartment()
                .Where(ri => ri.DepartmentID == (department.DepartmentID == 0 ? ri.DepartmentID : department.DepartmentID))
                .ToList<VW_RequisitionsByDepartment>();
            }
            catch (Exception)
            {
                
                throw;
            }                          
        }

        /// <summary>
        /// Get All Requistions by employee
        /// </summary>
        /// <returns>List of Requisitions by employee</returns>
        public List<VW_RequisitionsByEmployee> GetAllRequisitionByEmployee()
        {
            try
            {
                return context.VW_RequisitionsByEmployee.ToList<VW_RequisitionsByEmployee>();
            }
            catch (Exception)
            {

                throw new RequisitionException("No Data Found");
            }            
        }

        /// <summary>
        /// Get Requisitions by Employee
        /// </summary>
        /// <param name="user">user object</param>
        /// <param name="requisitionSearchDTO">requisitionSearchDTO object</param>
        /// <returns>List of VW_RequisitionsByEmployee objects</returns>
        public List<VW_RequisitionsByEmployee> GetRequisitionByEmployeeID(User user, RequisitionSearchDTO requisitionSearchDTO)
        {
            try
            {
                return GetAllRequisitionByEmployee().
                  Where(re => re.DateRequested.Month == (requisitionSearchDTO.ExactDateRequested.Month == 0 ? re.DateRequested.Month : requisitionSearchDTO.ExactDateRequested.Month)
                  && re.DateRequested.Year == (requisitionSearchDTO.ExactDateRequested.Year == 0 ? re.DateRequested.Year : requisitionSearchDTO.ExactDateRequested.Year)
                  && re.UserName == (user.UserName == "" ? re.UserName : user.UserName)
                  )
            .ToList<VW_RequisitionsByEmployee>();
            }
            catch (Exception)
            {
                
                throw new RequisitionException("No Data Found");
            }      
            
        }

        /// <summary>
        /// Get Requisition by primary key
        /// </summary>
        /// <param name="requisition">requisition object</param>
        /// <returns>requisition object</returns>
        public Requisition GetRequisitionByID(Requisition requisition)
        {
            try
            {
                return GetAllRequisition().Where(r => r.RequisitionID == requisition.RequisitionID).FirstOrDefault<Requisition>();
            }
            catch (Exception)
            {                
                throw;
            }           
        }

        /// <summary>
        /// Find the requisition by search criteria
        /// </summary>
        /// <param name="requisitioinSearchDTO">requisitioinSearchDTO object</param>
        /// <returns>List of Requisition objects</returns>
        public List<Requisition> FindRequisitionByCriteria(RequisitionSearchDTO requisitioinSearchDTO)
        {
            try
            {
                var tempQuery = GetAllRequisition();

                if (requisitioinSearchDTO != null)
                {                    
                    if (requisitioinSearchDTO.RequisitionID != 0)
                    {                        
                        tempQuery.Where(x => x.RequisitionID == requisitioinSearchDTO.RequisitionID).FirstOrDefault<Requisition>();                        
                    }

                    if (requisitioinSearchDTO.ExactDateRequested >= DateTime.MinValue)
                    {
                       // tempQuery.Where(x => EntityFunctions.DiffDays(x.DateApproved, requisitioinSearchDTO.ExactDateRequested) >= 0).ToList<Requisition>();
                        //GetAllRequisition().Where(x => EntityFunctions.DiffDays(x.DateApproved, requisitioinSearchDTO.ExactDateRequested) >= 0).ToList<Requisition>();
                    }
                }
                

                //if (requisitioinSearchDTO != null)
                //{
                //    if (requisitioinSearchDTO.RequisitionID != 0)
                //    {
                //        tempQuery = tempQuery.Where(r => r.RequisitionID == requisitioinSearchDTO.RequisitionID);
                //    }
                //    if (requisitioinSearchDTO.StartDateRequested != null && requisitioinSearchDTO.EndDateRequested != null && requisitioinSearchDTO.StartDateRequested >= DateTime.MinValue && requisitioinSearchDTO.EndDateRequested >= DateTime.MinValue)
                //    {
                //        tempQuery = tempQuery.Where(r => r.DateRequested >= requisitioinSearchDTO.StartDateRequested && r.DateRequested <= requisitioinSearchDTO.EndDateRequested);
                //    }

                //    if (requisitioinSearchDTO.StartDateRequested != null && requisitioinSearchDTO.StartDateRequested >= DateTime.MinValue)
                //    {
                //        tempQuery = tempQuery.Where(r => r.DateRequested == requisitioinSearchDTO.StartDateRequested);
                //    }

                //    if (requisitioinSearchDTO.EndDateRequested != null && requisitioinSearchDTO.EndDateRequested >= DateTime.MinValue)
                //    {
                //        tempQuery = tempQuery.Where(r => r.DateRequested == requisitioinSearchDTO.EndDateRequested);
                //    }
                //}

                return tempQuery.ToList<Requisition>();
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
        
        /// <summary>
        /// Generate the requisitionID for each requisition
        /// </summary>
        /// <param name="requisition">requisition object</param>
        /// <returns>string</returns>
        public string GetRequisitionNumber(Requisition requisition)
        {
            var department = (from d in context.Departments where d.DepartmentID == requisition.DepartmentID select d).FirstOrDefault<Department>();
            return department.Name.Substring(0,3) + "/" + DateTime.Now.Day + DateTime.Now.Month + "/" + DateTime.Now.Year;
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
                temp.StationeryID = requisitionItem.StationeryID;
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

                context.SaveChanges();
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
        public RequisitionItem GetRequisitionItemsByID(int requisitionItemID)
        {
            try
            {
                return (from ri in context.RequisitionItems
                        where ri.RequisitionItemID == requisitionItemID
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
        /// Create a new specialRequisitionItem
        /// </summary>
        /// <param name="requisitionItem">specialRequisitionItem object</param>
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
        /// Update the special requisitionItem
        /// </summary>
        /// <param name="requisitionItem">specialRequisitionItem object</param>
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
        /// Delete the special requisitionItem
        /// </summary>
        /// <param name="requisitionItem">specialRequisitionItem object</param>
        public void DeleteSpecialRequisitionItem(SpecialRequisitionItem requisitionItem)
        {
            try
            {
                var temp = (from ri in context.SpecialRequisitionItems
                            where ri.SpecialRequisitionItemsID == requisitionItem.SpecialRequisitionItemsID
                            select ri).FirstOrDefault<SpecialRequisitionItem>();

                if (temp != null)
                {                    
                    context.SpecialRequisitionItems.DeleteObject(temp);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get All special RequisitionItems in the requisition form
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
        /// Get special RequisitionItems by primary key
        /// </summary>
        /// <param name="requisitionItem">requisitionItem object</param>
        /// <returns></returns>
        public SpecialRequisitionItem GetSpecialRequisitionItemsByID(SpecialRequisitionItem specialRequisitionItem)
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
        /// <summary>
        /// Create a status object
        /// </summary>
        /// <param name="status">Status object</param>
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

        /// <summary>
        /// Update status object
        /// </summary>
        /// <param name="status">status object</param>
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

        /// <summary>
        /// Delete status object
        /// </summary>
        /// <param name="status">status object</param>
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

        /// <summary>
        /// Get All status levels
        /// </summary>
        /// <returns>List of status objects</returns>
        public List<Status> GetAllStatuses()
        {
            return (from s in context.Statuses select s).ToList<Status>();
        }

        /// <summary>
        /// Get status by primary key
        /// </summary>
        /// <param name="status">status object</param>
        /// <returns>status object</returns>
        public Status GetStatusByID(Status status)
        {
            return GetAllStatuses().Where(s => s.StatusID == status.StatusID).FirstOrDefault<Status>();
        }

        /// <summary>
        /// Get status object by filter criteria
        /// </summary>
        /// <param name="statusSearchDTO">statusSearchDTO object</param>
        /// <returns>List of status objects</returns>
        public Status GetStatusByName(StatusSearchDTO statusSearchDTO)
        {
            return GetAllStatuses().Where(s => s.Name == statusSearchDTO.Name).FirstOrDefault<Status>();
        }
        
        /// <summary>
        /// Get status object by filter criteria
        /// </summary>
        /// <param name="statusSearchDTO">statusSearchDTO object</param>
        /// <returns>List of status objects</returns>
        public List<Status> GetStatusByCriteria(StatusSearchDTO statusSearchDTO)
        {
            return GetAllStatuses().Where(s => s.Name == statusSearchDTO.Name).ToList<Status>();
        }
        #endregion

        #region Urgency
        /// <summary>
        /// Create a urgency object
        /// </summary>
        /// <param name="urgency">Urgency object</param>
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

        /// <summary>
        /// Update urgency object
        /// </summary>
        /// <param name="urgency">Urgency object</param>
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

        /// <summary>
        /// Delete urgency object
        /// </summary>
        /// <param name="urgency">Urgency object</param>
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

        /// <summary>
        /// Get All Urgency levels
        /// </summary>
        /// <returns>List of Urgency objects</returns>
        public List<Urgency> GetAllUrgencies()
        {
            return (from s in context.Urgencies select s).ToList<Urgency>();
        }

        /// <summary>
        /// Get urgency by primary key
        /// </summary>
        /// <param name="urgency">Urgency object</param>
        /// <returns>Urgency object</returns>
        public Urgency GetUrgencyByID(Urgency urgency)
        {
            return GetAllUrgencies().Where(u => u.UrgencyID == urgency.UrgencyID).FirstOrDefault<Urgency>();
        }

        /// <summary>
        /// Get Urgency object by filter criteria
        /// </summary>
        /// <param name="urgencySearchDTO">urgencySearchDTO object</param>
        /// <returns>List of Urgency objects</returns>
        public List<Urgency> GetUrgencyByCriteria(UrgencySearchDTO urgencySearchDTO)
        {
            return GetAllUrgencies().Where(s => s.Name == urgencySearchDTO.Name).ToList<Urgency>();
        }
        #endregion
    }
}
