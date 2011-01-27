/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;
using SA33.Team12.SSIS.Exceptions;

namespace SA33.Team12.SSIS.BLL
{
    public class RequisitionManager : SA33.Team12.SSIS.BLL.BusinessLogic
    {
        private RequisitionDAO requisitionDAO;

        public RequisitionManager()
        {
            requisitionDAO = new RequisitionDAO();
        }

        public void CreateRequisition(Requisition requisition)
        {
            try
            {
                requisitionDAO.CreateRequisition(requisition);                
            }
            catch (RequisitionException ex)
            {
                throw new ApplicationException("Requisition creation Failed");
            }
            
        }

        public void UpdateRequisitionStatus(Requisition requisition)
        {
           //requisitionDAO.UpdateRequisitionStatus(requisition, status);
        }

        public void ApproveRequisition(Requisition requisition)
        {
            requisitionDAO.ApproveRequisition(requisition);
        }

        public void ApproveRequisition(List<Requisition> requisitions)
        {
            foreach(Requisition req in requisitions)
            requisitionDAO.ApproveRequisition(req);
        }

        public void CancelRequisition()
        {
            //requisitionDAO.CancelRequisition();
        }

        public void UpdateRequisition(Requisition requisition)
        {
            requisitionDAO.UpdateRequisition(requisition);
        }

        /// <summary>
        /// Find All Requistions
        /// </summary>
        /// <returns></returns>
        public List<Requisition> GetAllRequisition()
        {
            return requisitionDAO.GetAllRequisition();
        }

        public void GetRequisitionByCategory(Category category, RequisitionSearchDTO requisitionSearchDTO)
        {
            //requisitionDAO.GetRequisitionByCategory(category, requisitionSearchDTO);
        }

        public void GetRequisitionByDepartment(Department department, RequisitionSearchDTO requisitionSearchDTO)
        {
            //requisitionDAO.GetRequisitionByDepartment(department, requisitionSearchDTO);
        }

        public void GetRequisitionByEmployee(User user, RequisitionSearchDTO requisitionSearchDTO)
        {
            //requisitionDAO.GetRequisitionByEmployee(user, requisitionSearchDTO);
        }

        /// <summary>
        /// Find the requisition by search criteria
        /// </summary>
        /// <param name="requisitioinSearchDTO">requisitioinSearchDTO object</param>
        /// <returns></returns>
        public List<Requisition> FindRequisitionByCriteria(RequisitionSearchDTO requisitioinSearchDTO)
        {
            return requisitionDAO.FindRequisitionByCriteria(requisitioinSearchDTO);
        }
           /// <summary>
        /// Generate the requisitionID for each requisition
        /// </summary>
        /// <param name="requisition"></param>
        /// <returns></returns>
        public string GetRequisitionID(Requisition requisition)
        {
            return requisitionDAO.GetRequisitionID(requisition);
        }
    }
}
