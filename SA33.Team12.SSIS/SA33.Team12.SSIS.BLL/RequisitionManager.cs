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
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace SA33.Team12.SSIS.BLL
{
    public class RequisitionManager : SA33.Team12.SSIS.BLL.BusinessLogic
    {
        private RequisitionDAO requisitionDAO;

        public RequisitionManager()
        {
            requisitionDAO = new RequisitionDAO();
        }

        public void CreateRequisition(Requsition requisition)
        {
            try
            {
                requisitionDAO.CreateRequisition(requisition);                
            }
            catch (Exception ex)
            {
                ExceptionManager exceptionManager = EnterpriseLibraryContainer.Current.GetInstance<ExceptionManager>();
                exceptionManager.HandleException(ex, "Policy");
            }
            
        }

        public void UpdateRequisitionStatus(Requsition requisition)
        {
            requisitionDAO.UpdateRequisitionStatus(requisition);
        }

        public void ApproveRequisition(Requsition requisition)
        {
            requisitionDAO.ApproveRequisition(requisition);
        }

        public void ApproveRequisition(List<Requsition> requisitions)
        {
            foreach(Requsition req in requisitions)
            requisitionDAO.ApproveRequisition(req);
        }

        public void CancelRequisition()
        {
            requisitionDAO.CancelRequisition();
        }

        public void UpdateRequisition(Requsition requisition)
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
