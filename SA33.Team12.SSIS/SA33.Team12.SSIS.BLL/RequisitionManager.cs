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
            catch (Exception ex)
            {
                
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

        public void GetRequisitionByCategory(Category category, RequisitioinSearchDTO requisitionSearchDTO)
        {
            requisitionDAO.GetRequisitionByCategory(category, requisitionSearchDTO);
        }

        public void GetRequisitionByDepartment(Department department, RequisitioinSearchDTO requisitionSearchDTO)
        {
            requisitionDAO.GetRequisitionByDepartment(department, requisitionSearchDTO);
        }

        public void GetRequisitionByEmployee(User user, RequisitioinSearchDTO requisitionSearchDTO)
        {
            requisitionDAO.GetRequisitionByEmployee(user, requisitionSearchDTO);
        }
    }
}
