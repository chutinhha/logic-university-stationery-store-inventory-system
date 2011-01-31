/***
 * Author: Sundar Aravind (A0076790U)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;
using SA33.Team12.SSIS.Exceptions;
using System.Diagnostics;
using System.Transactions;

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

        #region Requisition
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
                bool isTestOK = false;
                StatusSearchDTO sdto = new StatusSearchDTO() { Name = "Pending" };
                Status status = requisitionDAO.GetStatusByName(sdto);
                requisition.StatusID = status.StatusID;

                if (ValidateRequisition(requisition, RequisitionMethod.Create))
                {                   

                    if (requisition.RequisitionItems.Count > 0 || requisition.SpecialRequisitionItems.Count > 0)
                    {
                        foreach (RequisitionItem requisitionItem in requisition.RequisitionItems)
                        {                            
                            isTestOK = ValidateRequisitionItem(requisitionItem, RequisitionMethod.Create);
                            if (!isTestOK)
                            {
                                break;
                            }
                        }
                        foreach (SpecialRequisitionItem specialRequisitionItem in requisition.SpecialRequisitionItems)
                        {
                            isTestOK = ValidateSpecialRequisitionItem(specialRequisitionItem, RequisitionMethod.Create);
                            if (!isTestOK)
                            {
                                break;
                            }
                                
                        }
                    }

                    if (isTestOK)
                    {
                        requisitionDAO.CreateRequisition(requisition);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        public void UpdateRequisition(Requisition requisition)
        {
            try
            {
                bool isTrue = false;
                if (ValidateRequisition(requisition, RequisitionMethod.Update))
                {
                    foreach (RequisitionItem item in requisition.RequisitionItems)
                    {
                        isTrue = ValidateRequisitionItem(item, RequisitionMethod.Update);
                        if (!isTrue)
                        {
                            break;
                        }
                    }
                    foreach (SpecialRequisitionItem splItem in requisition.SpecialRequisitionItems)
                    {
                        isTrue = ValidateSpecialRequisitionItem(splItem, RequisitionMethod.Update);
                        if (!isTrue)
                        {
                            break;
                        }
                    }

                    if (isTrue)
                    {
                        requisitionDAO.UpdateRequisition(requisition);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateRequisitionStatus(Requisition requisition, Status status)
        {
            if (ValidateRequisition(requisition, RequisitionMethod.UpdateStatus))
            {
                requisitionDAO.UpdateRequisitionStatus(requisition, status);
            }
        }

        public void ApproveRequisition(Requisition requisition)
        {
            try
            {
                if (ValidateRequisition(requisition, RequisitionMethod.Approve))
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
            if (ValidateRequisition(requisition, RequisitionMethod.Cancel))
            {
                requisitionDAO.CancelRequisition(requisition);
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

        public List<VW_RequisitionsByCategory> GetRequisitionByCategory(Category category, RequisitionSearchDTO requisitionSearchDTO)
        {
            return requisitionDAO.GetRequisitionByCategoryID(category, requisitionSearchDTO);
        }

        public List<VW_RequisitionsByDepartment> GetRequisitionByDepartment(Department department, RequisitionSearchDTO requisitionSearchDTO)
        {
            return requisitionDAO.GetRequisitionByDepartmentID(department, requisitionSearchDTO);
        }

        public List<VW_RequisitionsByEmployee> GetRequisitionByEmployee(User user, RequisitionSearchDTO requisitionSearchDTO)
        {
            return requisitionDAO.GetRequisitionByEmployeeID(user, requisitionSearchDTO);
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
        public string GetRequisitionNumber(Requisition requisition)
        {
            return requisitionDAO.GetRequisitionNumber(requisition);
        }

        private bool ValidateRequisition(Requisition requisition, RequisitionMethod requisitionMethod)
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
                           (requisition.StatusID == 2 || requisition.Status != null) &&
                           (requisition.UrgencyID != 0 || requisition.Urgency != null) &&
                           (requisition.DateRequested != null && requisition.DateRequested.Date.ToShortDateString() != string.Empty) &&
                           (requisition.ApprovedBy != 0))
                        {
                            return true;
                        }
                        else
                        {
                            try
                            {
                                throw new RequisitionException("Update Requisition Failed");
                            }
                            catch (Exception)
                            {
                                
                                throw;
                            }
                           
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

        private bool ValidateRequisitionItem(RequisitionItem requisitionItem, RequisitionMethod requisitionMethod)
        {
            try
            {
                if (requisitionItem != null)
                {
                    if (requisitionMethod == RequisitionMethod.Create)
                    {
                        if ((requisitionItem.RequisitionID != 0 || requisitionItem.Requisition != null) &&
                           (requisitionItem.StationeryID != 0 || requisitionItem.Stationery != null) &&
                           (requisitionItem.QuantityRequested > 0))
                        {
                            return true;
                        }
                    }

                    if (requisitionMethod == RequisitionMethod.Update)
                    {
                        if ((requisitionItem.RequisitionID != 0 || requisitionItem.Requisition != null) &&
                         (requisitionItem.StationeryID != 0 || requisitionItem.Stationery != null) &&
                         (requisitionItem.QuantityRequested > 0 && requisitionItem.QuantityRequested < requisitionItem.QuantityRequested))
                        {
                            return true;
                        }
                    }

                    if (requisitionMethod == RequisitionMethod.Delete)
                    {
                        if (requisitionItem.RequisitionItemID != 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool ValidateSpecialRequisitionItem(SpecialRequisitionItem specialRequisitionItem, RequisitionMethod requisitionMethod)
        {
            try
            {
                if (specialRequisitionItem != null)
                {
                    if (requisitionMethod == RequisitionMethod.Create)
                    {
                        if ((specialRequisitionItem.RequisitionID != 0 || specialRequisitionItem.Requisition != null) &&
                           (specialRequisitionItem.SpecialStationeryID != 0 || specialRequisitionItem.SpecialStationeryID != null) &&
                           (specialRequisitionItem.QuantityRequested > 0))
                        {
                            return true;
                        }
                    }

                    if (requisitionMethod == RequisitionMethod.Update)
                    {
                        if ((specialRequisitionItem.RequisitionID != 0 || specialRequisitionItem.Requisition != null) &&
                         (specialRequisitionItem.SpecialStationeryID != 0 || specialRequisitionItem.SpecialStationery != null) &&
                         (specialRequisitionItem.SpecialStationery.IsApproved == false) &&
                         (specialRequisitionItem.QuantityRequested > 0))
                        {
                            return true;
                        }
                    }

                    if (requisitionMethod == RequisitionMethod.Delete)
                    {
                        if (specialRequisitionItem.SpecialRequisitionItemsID != 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
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
                if (requisitionItem != null && ValidateRequisitionItem(requisitionItem, RequisitionMethod.Create))
                {
                    requisitionDAO.CreateRequisitionItem(requisitionItem);
                }
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
                RequisitionItem temp = requisitionDAO.GetRequisitionItemsByID(requisitionItem);
                if (temp != null && ValidateRequisitionItem(temp, RequisitionMethod.Update))
                {
                    temp.Stationery = requisitionItem.Stationery;
                    temp.QuantityRequested = requisitionItem.QuantityRequested;
                    temp.Price = requisitionItem.Price;
                    requisitionDAO.UpdateRequisitionItem(temp);
                }
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
                RequisitionItem temp = requisitionDAO.GetRequisitionItemsByID(requisitionItem);
                if (temp != null)
                {
                    requisitionDAO.DeleteRequisitionItem(temp);
                }
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
                return requisitionDAO.GetAllRequisitionItems(requisition);
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
        public RequisitionItem GetRequisitionItemsByID(RequisitionItem requisitionItem)
        {
            try
            {
                return requisitionDAO.GetRequisitionItemsByID(requisitionItem);
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
                if (specialRequisitionItem != null)
                {
                    requisitionDAO.CreateSpecialSpecialRequisitionItem(specialRequisitionItem);
                }
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
                SpecialRequisitionItem temp = requisitionDAO.GetSpecialRequisitionItemsByID(specialRequisitionItem);

                if (temp != null)
                {
                    temp.SpecialStationery = specialRequisitionItem.SpecialStationery;
                    temp.QuantityRequested = specialRequisitionItem.QuantityRequested;
                    temp.Price = specialRequisitionItem.Price;
                    requisitionDAO.UpdateSpecialRequisitionItem(temp);
                }
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
        public void DeleteSpecialRequisitionItem(SpecialRequisitionItem specialRequisitionItem)
        {
            try
            {
                SpecialRequisitionItem temp = requisitionDAO.GetSpecialRequisitionItemsByID(specialRequisitionItem);

                if (temp != null && ValidateSpecialRequisitionItem(specialRequisitionItem, RequisitionMethod.Delete))
                {
                    requisitionDAO.DeleteSpecialRequisitionItem(temp);
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
                return requisitionDAO.GetAllSpecialRequisitionItems(requisition);
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
                return requisitionDAO.GetSpecialRequisitionItemsByID(specialRequisitionItem);
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
                if (status != null)
                {
                    requisitionDAO.CreateStatus(status);
                }
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
        public bool UpdateStatus(Status status)
        {
            try
            {
                Status st = requisitionDAO.GetStatusByID(status);

                if (st != null)
                {
                    st.Name = status.Name;
                    st.Group = status.Group;
                    st.Description = status.Description;
                    requisitionDAO.UpdateStatus(st);
                    return true;
                }
                return false;
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
        public bool DeleteStatus(Status status)
        {
            try
            {
                if (status != null && status.StatusID != 0)
                {
                    requisitionDAO.DeleteStatus(status);
                    return true;
                }
                return false;
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
            return requisitionDAO.GetAllStatuses();
        }

        /// <summary>
        /// Get status by primary key
        /// </summary>
        /// <param name="status">status object</param>
        /// <returns>status object</returns>
        public Status GetStatusByID(Status status)
        {
            return requisitionDAO.GetStatusByID(status);
        }

        /// <summary>
        /// Get status object by filter criteria
        /// </summary>
        /// <param name="statusSearchDTO">statusSearchDTO object</param>
        /// <returns>List of status objects</returns>
        public Status GetStatusByName(StatusSearchDTO statusSearchDTO)
        {
            return requisitionDAO.GetStatusByName(statusSearchDTO);
        }

        /// <summary>
        /// Get status object by filter criteria
        /// </summary>
        /// <param name="statusSearchDTO">statusSearchDTO object</param>
        /// <returns>List of status objects</returns>
        public List<Status> GetStatusByCriteria(StatusSearchDTO statusSearchDTO)
        {
            return requisitionDAO.GetStatusByCriteria(statusSearchDTO);
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
                if (urgency != null)
                {
                    requisitionDAO.CreateUrgency(urgency);
                }
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
        public bool UpdateUrgency(Urgency urgency)
        {
            try
            {
                Urgency ur = requisitionDAO.GetUrgencyByID(urgency);
                if (ur != null && ur.UrgencyID != 0)
                {
                    ur.Name = urgency.Name;
                    ur.Level = urgency.Level;
                    requisitionDAO.UpdateUrgency(ur);
                    return true;
                }
                return false;
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
        public bool DeleteUrgency(Urgency urgency)
        {
            try
            {
                if (urgency != null && urgency.UrgencyID != 0)
                {
                    requisitionDAO.DeleteUrgency(urgency);
                    return true;
                }
                return false;
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
            return requisitionDAO.GetAllUrgencies();
        }

        /// <summary>
        /// Get urgency by primary key
        /// </summary>
        /// <param name="urgency">Urgency object</param>
        /// <returns>Urgency object</returns>
        public Urgency GetUrgencyByID(Urgency urgency)
        {
            return requisitionDAO.GetUrgencyByID(urgency);
        }

        /// <summary>
        /// Get Urgency object by filter criteria
        /// </summary>
        /// <param name="urgencySearchDTO">urgencySearchDTO object</param>
        /// <returns>List of Urgency objects</returns>
        public List<Urgency> GetUrgencyByCriteria(UrgencySearchDTO urgencySearchDTO)
        {
            return requisitionDAO.GetUrgencyByCriteria(urgencySearchDTO);
        }
        #endregion
    }
}
