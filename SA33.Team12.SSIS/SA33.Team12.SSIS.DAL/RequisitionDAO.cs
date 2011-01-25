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
    public class RequisitionDAO : DALLogic
    {
        public void CreateRequisition(Requisition requisition)
        {
            try
            {                
               context.AddToRequisitions(requisition);
               foreach (RequisitionItem reqItem in requisition.RequisitionItems)
               {
                   context.AddToRequisitionItems(reqItem);                   
               }             
             
               foreach (SpecialRequisitionItem splItem in requisition.SpecialRequisitionItems)
               {                              
                   context.AddToSpeicalStationeries(splItem.SpeicalStationery);
                   context.AddToSpecialRequisitionItems(splItem);                   
               }

               var status = (from s in context.Statuses where s.Name == "Pending" select s).FirstOrDefault<Status>();

               UpdateRequisitionStatus(requisition, status);
               context.SaveChanges();
            }
            catch (Exception ex)
            {
                
            }
            
        }


        public void UpdateRequisitionStatus(Requisition requisition, Status status)
 {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var tempRequisition = (from p in context.Requisitions
                                           where p.RequisitionID == requisition.RequisitionID
                                           select p).FirstOrDefault<Requisition>();

                    tempRequisition.Status = status;
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

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

                    UpdateRequisitionStatus(tempRequisition, status);
                    //tempRequisition.ApprovedBy = requisition.ApprovedBy;
                    //tempRequisition.DateApproved = requisition.DateApproved;
                    //context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

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
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateRequisition(Requisition updateRequisition)

        {
            var tempReq = (from r in context.Requisitions
                           where r.RequisitionID == updateRequisition.RequisitionID
                           select r).FirstOrDefault<Requisition>();

            tempReq = updateRequisition;
            context.SaveChanges();
        }

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
    }
}
