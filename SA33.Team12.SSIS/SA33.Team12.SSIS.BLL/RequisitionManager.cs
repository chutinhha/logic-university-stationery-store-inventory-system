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
    /// <summary>
    /// RequisitionManager Business Logic class
    /// </summary>
    public class RequisitionManager : SA33.Team12.SSIS.BLL.BusinessLogic
    {
        private RequisitionDAO requisitionDAO;
        private enum RequisitionMethod
        {
            Create, Update, Approve, Cancel, UpdateStatus, Delete
        };
        private enum RequisitionType
        {
            Normal, Special
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public RequisitionManager()
        {
            requisitionDAO = new RequisitionDAO();
        }

        /// <summary>
        /// Create a new requisition and persist with database
        /// </summary>
        /// <param name="requisition">requisition object</param>
        public void CreateRequisition(Requisition requisition)
        {
            try
            {
                if (validateRequisition(requisition, RequisitionMethod.Create))
                {
                    StatusSearchDTO sdto = new StatusSearchDTO() { Name = "Pending" };
                    Status status = requisitionDAO.GetStatusByName(sdto);
                    requisitionDAO.UpdateRequisitionStatus(requisition, status);
                    requisitionDAO.CreateRequisition(requisition);
                }
            }
            catch (RequisitionException ex)
            {
                throw;
            }

        }

        public void UpdateRequisitionStatus(Requisition requisition, Status status)
        {
            if (validateRequisition(requisition, RequisitionMethod.UpdateStatus))
            {
                requisitionDAO.UpdateRequisitionStatus(requisition, status);
            }
        }

        public void ApproveRequisition(Requisition requisition)
        {
            try
            {
                if (validateRequisition(requisition, RequisitionMethod.Approve))
                {
                    requisitionDAO.ApproveRequisition(requisition);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ApproveRequisition(List<Requisition> requisitions)
        {
            try
            {
                foreach (Requisition req in requisitions)
                    requisitionDAO.ApproveRequisition(req);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void CancelRequisition(Requisition requisition)
        {
            if (validateRequisition(requisition, RequisitionMethod.Cancel))
            {
                requisitionDAO.CancelRequisition(requisition);
            }
        }

        public void UpdateRequisition(Requisition requisition)
        {
            try
            {
                if (validateRequisition(requisition, RequisitionMethod.Update))
                {
                    requisitionDAO.UpdateRequisition(requisition);
                }
            }
            catch (Exception)
            {

                throw;
            }
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

        private bool validateRequisition(Requisition requisition, RequisitionMethod requisitionMethod)
        {
            try
            {
                if (requisition != null)
                {
                    if (requisitionMethod == RequisitionMethod.Create)
                    {
                        if ((requisition.CreatedBy != 0 || requisition.CreatedByUser != null) &&
                            (requisition.DepartmentID != 0 || requisition.Department != null) &&
                            (requisition.RequisitionForm != string.Empty || requisition.RequisitionForm != null) &&
                            (requisition.StatusID != 0 || requisition.Status != null) &&
                            (requisition.UrgencyID != 0 || requisition.Urgency != null) &&
                            (requisition.DateRequested != null && requisition.DateRequested.Date.ToShortDateString() == DateTime.Now.Date.ToShortDateString()) &&
                            (requisition.ApprovedByUser == null) && (requisition.DateApproved == null))
                        {
                            return true;
                        }
                    }
                    if (requisitionMethod == RequisitionMethod.Update)
                    {
                        if ((requisition.CreatedBy != 0 || requisition.CreatedByUser != null) &&
                           (requisition.DepartmentID != 0 || requisition.Department != null) &&
                           (requisition.RequisitionForm != string.Empty || requisition.RequisitionForm != null) &&
                           (requisition.StatusID != 0 || requisition.Status != null) &&
                           (requisition.UrgencyID != 0 || requisition.Urgency != null) &&
                           (requisition.DateRequested != null && requisition.DateRequested.Date.ToShortDateString() == string.Empty) &&
                           (requisition.ApprovedByUser == null) && (requisition.DateApproved == null))
                        {
                            return true;
                        }
                    }

                    if (requisitionMethod == RequisitionMethod.UpdateStatus)
                    {
                        if (requisition.RequisitionID != 0)
                        {
                            return true;
                        }
                    }

                    if (requisitionMethod == RequisitionMethod.Approve || requisitionMethod == RequisitionMethod.Cancel)
                    {
                        if (requisition.ApprovedBy == 0 && requisition.ApprovedByUser == null && requisition.DateApproved == null)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw new RequisitionException("Create Requisition failed. Please try again later");
            }
        }

        private bool validateRequisitionItem(RequisitionItem requisitionItem, RequisitionMethod requisitionMethod)
        {
            if (requisitionItem != null)
            {
                if (requisitionMethod == RequisitionMethod.Create)
                {
                    if ((requisitionItem.RequisitionItemID != 0 && requisitionItem.RequisitionItemID != null) &&
                       (requisitionItem.RequisitionID != 0 || requisitionItem.Requisition != null) &&
                       (requisitionItem.StationeryID != 0 || requisitionItem.Stationery != null) &&
                       (requisitionItem.QuantityRequested > 0))
                    {
                        return true;
                    }
                }

                if (requisitionMethod == RequisitionMethod.Update)
                {

                }

                if (requisitionMethod == RequisitionMethod.Delete)
                {

                }
            }
            return false;
        }

        private bool validateSpecialRequisitionItem(SpecialRequisitionItem specialRequisitionItem, RequisitionMethod requisitionMethod)
        {
            if (specialRequisitionItem != null)
            {
                if (requisitionMethod == RequisitionMethod.Create)
                {
                    if ((specialRequisitionItem.SpecialRequisitionItemsID != 0 && specialRequisitionItem.SpecialRequisitionItemsID != null) &&
                       (specialRequisitionItem.RequisitionID != 0 || specialRequisitionItem.Requisition != null) &&
                       (specialRequisitionItem.SpeicalStationeryID != 0 || specialRequisitionItem.SpecialStationery != null) &&
                       (specialRequisitionItem.QuantityRequested > 0))
                    {
                        return true;
                    }
                }

                if (requisitionMethod == RequisitionMethod.Update)
                {

                }

                if (requisitionMethod == RequisitionMethod.Delete)
                {

                }
            }
            return false;
        }
    }
}
