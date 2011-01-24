/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;

using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DTO;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace SA33.Team12.SSIS.BLL
{
    public class RequisitionManager : SA33.Team12.SSIS.BLL.BusinessLogic
    {
        public void CreateRequisition(Requsition requisition)
        {
            try
            {                
               // context.AddToRequsitions(requisition);                
            }
            catch (Exception ex)
            {
                ExceptionManager exceptionManager = EnterpriseLibraryContainer.Current.GetInstance<ExceptionManager>();
                exceptionManager.HandleException(ex, "Policy");
            }
            
        }

        public void UpdateRequisitionStatus(Requsition requisition)
        {
            throw new System.NotImplementedException();
        }

        public void ApproveRequisition(Requsition requisition)
        {
            throw new System.NotImplementedException();
        }

        public void ApproveRequisition(List<Requsition> requisitions)
        {
            throw new System.NotImplementedException();
        }

        public void CancelRequisition()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateRequisition(Requsition requisition)
        {
            throw new System.NotImplementedException();
        }

        public void GetRequisitionByCategory(Category category, RequisitioinSearchDTO requisitionSearchDTO)
        {
            throw new System.NotImplementedException();
        }

        public void GetRequisitionByDepartment(Department department, RequisitioinSearchDTO requisitionSearchDTO)
        {
            throw new System.NotImplementedException();
        }

        public void GetRequisitionByEmployee(User user, RequisitioinSearchDTO requisitionSearchDTO)
        {
            throw new System.NotImplementedException();
        }
    }
}
