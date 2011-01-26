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

namespace SA33.Team12.SSIS.DAL
{
    /// <summary>
    /// RequisitionDAO Class
    /// </summary>
    public class RequisitionDAO : DALLogic
    {
        /// <summary>
        /// Create a new requisition and persist with database
        /// </summary>
        /// <param name="requisition"></param>
        public void CreateRequisition(Requisition requisition)
        {
            try
            {
                
                //Add requisition to context                
                context.AddToRequisitions(requisition);

                //Add requisition items to context
                foreach (RequisitionItem reqItem in requisition.RequisitionItems)
                {
                    context.AddToRequisitionItems(reqItem);
                }

                //Add special requisition items to context
                foreach (SpecialRequisitionItem splItem in requisition.SpecialRequisitionItems)
                {
                    context.AddToSpeicalStationeries(splItem.SpeicalStationery);
                    context.AddToSpecialRequisitionItems(splItem);
                }

                //Get the status id for "Pending"
                var status = (from s in context.Statuses where s.Name == "Pending" select s).FirstOrDefault<Status>();

                //Update the status id for the new requistion to pending
                UpdateRequisitionStatus(requisition, status);

                //Save the changes
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                //Exception thrown incase if insert fails
                throw new ApplicationException("Create requisition failed.\n" + ex.Message);
            }

        }

        /// <summary>
        /// Update the requisition status
        /// </summary>
        /// <param name="requisition">requistion</param>
        /// <param name="status">status</param>
        public void UpdateRequisitionStatus(Requisition requisition, Status status)
        {
            try
            {
                //Transcation processing
                using (TransactionScope ts = new TransactionScope())
                {
                    //Get the requistion to update the status
                    var tempRequisition = (from p in context.Requisitions
                                           where p.RequisitionID == requisition.RequisitionID
                                           select p).FirstOrDefault<Requisition>();

                    
                    tempRequisition.Status = status;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Update failed.\n" + ex.Message);
            }
        }

        /// <summary>
        /// Approval of the requisition
        /// </summary>
        /// <param name="requisition"></param>
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
                throw new ApplicationException("Approval Failed.\n" + ex.Message);
            }
        }

        /// <summary>
        /// Approve all the requisitions
        /// </summary>
        /// <param name="requisitions"></param>
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
        /// <param name="cancelRequisition"></param>
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
                    //tempRequisition.ApprovedBy = requisition.ApprovedBy;
                    //tempRequisition.DateApproved = requisition.DateApproved;
                    //context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Approval Failed.\n" + ex.Message);
            }
        }

        /// <summary>
        /// Update the requisition before approval
        /// </summary>
        /// <param name="updateRequisition"></param>
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
                
                throw new ApplicationException("Update requisition failed.\n" + ex.Message);
            }
            
        }

        /// <summary>
        /// Get Requistion by category
        /// </summary>
        /// <param name="category"></param>
        /// <param name="requisitionSearchDTO"></param>
        /// <returns></returns>
        public List<Requisition> GetRequisitionByCategory(Category category, RequisitioinSearchDTO requisitionSearchDTO)
        {
            return null;
        }

        public List<Requisition> GetRequisitionByDepartment(Department department, RequisitioinSearchDTO requisitionSearchDTO)
        {
            return null;
        }

        public List<Requisition> GetRequisitionByEmployee(User user, RequisitioinSearchDTO requisitionSearchDTO)
        {
            return null;
        }

        private string GetRequisitionID(Requisition requisition)
        {
            var department = (from d in context.Departments where d.DepartmentID == requisition.DepartmentID select d).FirstOrDefault<Department>();
            return department.Name + "/" + DateTime.Now.Day + DateTime.Now.Month + "/" + DateTime.Now.Year;
        }
    }
}
