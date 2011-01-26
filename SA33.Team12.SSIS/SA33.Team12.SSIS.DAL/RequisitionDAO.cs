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
        /// <summary>
        /// Create a new requisition and persist with database
        /// </summary>
        /// <param name="requisition">requisition object</param>
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
                    context.AddToSpecialStationeries(splItem.SpecialStationery);
                    context.AddToSpecialRequisitionItems(splItem);
                }

                //Get the status id for "Pending"
                var status = (from s in context.Statuses where s.Name == "Pending" select s).FirstOrDefault<Status>();

                //Update the status id for the new requistion to "Pending"
                UpdateRequisitionStatus(requisition, status);

                //Save the changes
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                //Exception thrown incase if insert fails
                throw new RequisitionException("Create requisition failed.");
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

                throw new RequisitionException("Update requisition failed.");
            }
        }

        /// <summary>
        /// Find All Requistions
        /// </summary>
        /// <returns></returns>
        public List<Requisition> FindAllRequisition()
        {
            return (from c in context.Requisitions select c).ToList<Requisition>();
        }

        /// <summary>
        /// Get Requistions by category
        /// </summary>
        /// <param name="category">category object</param>
        /// <param name="requisitionSearchDTO">requisitionSearchDTO object</param>
        /// <returns></returns>
        public List<Requisition> FindRequisitionByCategory(Category category, RequisitionSearchDTO requisitionSearchDTO)
        {
            return null;
        }

        /// <summary>
        /// Get Requisitions by department
        /// </summary>
        /// <param name="department">department object</param>
        /// <param name="requisitionSearchDTO">requisitionSearchDTO object</param>
        /// <returns></returns>
        public List<Requisition> FindRequisitionByDepartment(Department department, RequisitionSearchDTO requisitionSearchDTO)
        {
            return null;
        }

        /// <summary>
        /// Get Requisitions by Employee
        /// </summary>
        /// <param name="user">user object</param>
        /// <param name="requisitionSearchDTO">requisitionSearchDTO object</param>
        /// <returns></returns>
        public List<Requisition> FindRequisitionByEmployee(User user, RequisitionSearchDTO requisitionSearchDTO)
        {
            return null;
        }

        /// <summary>
        /// Find the requisition by search criteria
        /// </summary>
        /// <param name="requisitioinSearchDTO">requisitioinSearchDTO object</param>
        /// <returns></returns>
        public List<Requisition> FindRequisitionByCriteria(RequisitionSearchDTO requisitioinSearchDTO)
        {
            var tempQuery = (from r in context.Requisitions
                        where 1 == 1 select r);

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

            return (from q in tempQuery select q).ToList<Requisition>() ;
            
 
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

    }
}
