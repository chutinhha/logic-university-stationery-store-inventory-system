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
            Create, Update, Approve, Reject, Cancel, UpdateStatus, Delete
        };

        #region Requisition
        /// <summary>
        /// Requisition Manager Constructor
        /// </summary>
        public RequisitionManager()
        {
            requisitionDAO = new RequisitionDAO();
        }

        /// <summary>
        /// Create a new requisition and persist with database
        /// </summary>
        /// <param name="requisition">requisition object</param>
        public Requisition CreateRequisition(Requisition requisition)
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
                        return requisition;
                    }

                    else
                    {
                        ErrorMessage("Create Requisition Failed. Please check the input.");
                    }
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;

        }

        /// <summary>
        /// Update requisition and persist with database
        /// </summary>
        /// <param name="requisition">requisition object</param>
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
                    else
                    {
                        ErrorMessage("Update Requisition Failed");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Update requisition status and persist with database
        /// </summary>
        /// <param name="requisition">requisition object</param>
        /// <param name="status">status object</param>
        public void UpdateRequisitionStatus(Requisition requisition, Status status)
        {
            if (ValidateRequisition(requisition, RequisitionMethod.UpdateStatus))
            {
                requisitionDAO.UpdateRequisitionStatus(requisition, status);
            }
        }

        /// <summary>
        /// Approve single requisition and persist with database
        /// </summary>
        /// <param name="requisition">requisition object</param>
        public void ApproveRequisition(Requisition requisition)
        {
            try
            {
                if (ValidateRequisition(requisition, RequisitionMethod.Approve))
                {
                    requisitionDAO.ApproveRequisition(requisition);
                }
                else
                {
                    ErrorMessage("Approval of Requisition Failed");
                }
            }
            catch (Exception)
            {

                throw new ApprovalException("Approval Failed.");
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
        /// Reject single requisition and persist with database
        /// </summary>
        /// <param name="requisition">requisition object</param>
        public void RejectRequisition(Requisition requisition)
        {
            try
            {
                if (ValidateRequisition(requisition, RequisitionMethod.Reject))
                {
                    requisitionDAO.RejectRequisition(requisition);
                }
                else
                {
                    ErrorMessage("Rejection of Requisition Failed");
                }
            }
            catch (Exception)
            {

                throw new ApprovalException("Rejection Failed for all requisitions.");
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
        /// Cancel a new requisition and persist with database
        /// </summary>
        /// <param name="requisition">requisition object</param>
        public void CancelRequisition(Requisition requisition)
        {
            if (ValidateRequisition(requisition, RequisitionMethod.Cancel))
            {
                requisitionDAO.CancelRequisition(requisition);
            }
            else
            {
                ErrorMessage("Cancel Requisition Failed.");
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
        /// Find All Requistions
        /// </summary>
        /// <returns></returns>
        public List<Requisition> GetAllRequisition()
        {
            List<Requisition> temp = requisitionDAO.GetAllRequisition();
            if (temp != null)
            {
                return temp;
            }
            ErrorMessage("Result Not Found.");
            return null;
        }

        /// <summary>
        /// Find All Requistions by EmpID
        /// </summary>
        /// <returns></returns>
        public List<Requisition> GetAllRequisition(int EmployeeID, RequisitionSearchDTO reqSearchDTO)
        {
            List<Requisition> temp = requisitionDAO.GetAllRequisition(EmployeeID, reqSearchDTO);
            if (temp != null)
            {
                return temp;
            }
            ErrorMessage("Result Not Found.");
            return null;
        }

        /// <summary>
        /// Get Requistion By ID
        /// </summary>
        /// <returns></returns>
        public Requisition GetRequisitionByID(int RequisitionID)
        {
            Requisition temp = requisitionDAO.GetRequisitionByID(RequisitionID);
            if (temp != null)
            {
                return temp;
            }
            
            return null;
        }

        /// <summary>
        /// Get All Approved Requistions
        /// </summary>
        /// <returns></returns>
        public List<Requisition> GetAllUnApprovedRequisitionByDepartmentID(int departmentID)
        {
            var result = requisitionDAO.GetAllUnApprovedRequisitionByDepartmentID(departmentID);
           
            if (result!= null)
            {
                return result;
            }
            ErrorMessage("Result Not Found.");
            return null;
           
        }

        /// <summary>
        /// Get requisition List by category
        /// </summary>
        /// <param name="category">category object</param>
        /// <param name="requisitionSearchDTO">RequisitionSearchDTO object</param>
        /// <returns>List of VW_RequisitionsByCategory</returns>
        public List<VW_RequisitionsByCategory> GetRequisitionByCategory(Category category, RequisitionSearchDTO requisitionSearchDTO)
        {
            List<VW_RequisitionsByCategory> temp = requisitionDAO.GetRequisitionByCategoryID(category, requisitionSearchDTO);
            if (temp != null)
            {
                return temp;
            }
            else
            {
                ErrorMessage("Result Not Found");
                return null;
            }
        }

        /// <summary>
        /// Get requisition List by department
        /// </summary>
        /// <param name="department">department object</param>
        /// <param name="requisitionSearchDTO">RequisitionSearchDTO object</param>
        /// <returns>List of VW_RequisitionsByDepartment</returns>
        public List<VW_RequisitionsByDepartment> GetRequisitionByDepartment(Department department, RequisitionSearchDTO requisitionSearchDTO)
        {
            List<VW_RequisitionsByDepartment> temp = requisitionDAO.GetRequisitionByDepartmentID(department, requisitionSearchDTO);
            if (temp != null)
            {
                return temp;
            }
            ErrorMessage("Result Not Found");
            return null;
        }

        /// <summary>
        /// Get requisition List by employeeId
        /// </summary>
        /// <param name="user">user object</param>
        /// <param name="requisitionSearchDTO">RequisitionSearchDTO object</param>
        /// <returns>List of VW_RequisitionsByEmployee</returns>
        public List<VW_RequisitionsByEmployee> GetRequisitionByEmployee()
        {
            List<VW_RequisitionsByEmployee> temp = requisitionDAO.GetAllRequisitionByEmployee();
            if (temp != null)
            {
                return temp;
            }
            ErrorMessage("Result Not Found");
            return null;
        }

        /// <summary>
        /// Find the requisition by search criteria
        /// </summary>
        /// <param name="requisitioinSearchDTO">requisitioinSearchDTO object</param>
        /// <returns></returns>
        public List<Requisition> FindRequisitionByCriteria(RequisitionSearchDTO requisitioinSearchDTO)
        {
            List<Requisition> temp = requisitionDAO.FindRequisitionByCriteria(requisitioinSearchDTO);
            if (temp != null)
            {
                return temp;
            }
            ErrorMessage("Result Not Found");
            return null;
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

        /// <summary>
        /// Validate Requisition
        /// </summary>
        /// <param name="requisition">Requisition object</param>
        /// <param name="requisitionMethod">enum requisitionMethod</param>
        /// <returns>boolean</returns>
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

                    if (requisitionMethod == RequisitionMethod.Approve || requisitionMethod == RequisitionMethod.Reject)
                    {
                        if (requisition.ApprovedBy != 0 && requisition.ApprovedByUser.Role == "DepartmentHeads")
                        {
                            return true;
                        }
                    }

                    if (requisitionMethod == RequisitionMethod.Cancel)
                    {
                        if (requisition.ApprovedBy == 0 || requisition.ApprovedBy == null)
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
                else
                {
                    ErrorMessage("Add item failed");
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
                RequisitionItem temp = requisitionDAO.GetRequisitionItemsByID(requisitionItem.RequisitionItemID);
                if (ValidateRequisitionItem(temp, RequisitionMethod.Update))
                {
                    temp.Stationery = requisitionItem.Stationery;
                    temp.QuantityRequested = requisitionItem.QuantityRequested;
                    //temp.Price = requisitionItem.Price;
                    requisitionDAO.UpdateRequisitionItem(temp);
                }
                else
                {
                    ErrorMessage("Update item failed");
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
                RequisitionItem temp = requisitionDAO.GetRequisitionItemsByID(requisitionItem.RequisitionItemID);
                if (temp != null)
                {
                    requisitionDAO.DeleteRequisitionItem(temp);
                }
                else
                {
                    ErrorMessage("Delete item failed");
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
                List<RequisitionItem> temp = requisitionDAO.GetAllRequisitionItems(requisition);
                if (temp != null)
                    return temp;

                ErrorMessage("Result Not Found");
                return null;
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
                var temp = requisitionDAO.GetRequisitionItemsByID(requisitionItemID);
                if (temp != null)
                {
                    return temp;
                }

               // ErrorMessage("Result Not Found");
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Validate RequisitionItem
        /// </summary>
        /// <param name="requisitionItem">requisitionItem object</param>
        /// <param name="requisitionMethod">enum requisitionMethod</param>
        /// <returns>boolean</returns>
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
                         (requisitionItem.QuantityRequested > 0))
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
                if (specialRequisitionItem != null && ValidateSpecialRequisitionItem(specialRequisitionItem, RequisitionMethod.Create))
                {
                    requisitionDAO.CreateSpecialSpecialRequisitionItem(specialRequisitionItem);
                }
                else
                {
                    ErrorMessage("Create Special Requisition Item Failed");
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
                else
                {
                    ErrorMessage("Update special requisition failed");
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
                else
                {
                    ErrorMessage("Delete special requisition failed");
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
                List<SpecialRequisitionItem> temp = requisitionDAO.GetAllSpecialRequisitionItems(requisition);
                if (temp != null)
                {
                    return temp;
                }
                else
                {
                    ErrorMessage("Result not found");
                    return null;
                }
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
                var temp = requisitionDAO.GetSpecialRequisitionItemsByID(specialRequisitionItem);
                if (temp != null)
                {
                    return temp;
                }
                ErrorMessage("Result Not Found");
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Validate SpecialRequisitionItem
        /// </summary>
        /// <param name="specialRequisitionItem">specialRequisitionItem object</param>
        /// <param name="requisitionMethod">enum requisitionMethod</param>
        /// <returns>boolean</returns>
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
                else
                {
                    ErrorMessage("Create staus failed");
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
                ErrorMessage("Update status record failed");
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
                ErrorMessage("Delete status record failed");
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
            List<Status> temp = requisitionDAO.GetAllStatuses();
            if (temp != null)
                return temp;

            ErrorMessage("Result not found");
            return null;
        }

        /// <summary>
        /// Get status by primary key
        /// </summary>
        /// <param name="status">status object</param>
        /// <returns>status object</returns>
        public Status GetStatusByID(Status status)
        {
            var temp = requisitionDAO.GetStatusByID(status);

            if (temp != null)
                return temp;

            ErrorMessage("Result not found");
            return null;
        }

        /// <summary>
        /// Get status object by filter criteria
        /// </summary>
        /// <param name="statusSearchDTO">statusSearchDTO object</param>
        /// <returns>List of status objects</returns>
        public Status GetStatusByName(StatusSearchDTO statusSearchDTO)
        {
            var temp = requisitionDAO.GetStatusByName(statusSearchDTO);

            if (temp != null)
                return temp;

            ErrorMessage("Result not found");
            return null;
        }

        /// <summary>
        /// Get status object by filter criteria
        /// </summary>
        /// <param name="statusSearchDTO">statusSearchDTO object</param>
        /// <returns>List of status objects</returns>
        public List<Status> GetStatusByCriteria(StatusSearchDTO statusSearchDTO)
        {
            List<Status> temp = requisitionDAO.GetStatusByCriteria(statusSearchDTO);

            if (temp != null)
                return temp;

            ErrorMessage("Result not found");
            return null;
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
                else
                {
                    ErrorMessage("Create urgency record failed");
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
                ErrorMessage("Update urgency record failed");
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
                ErrorMessage("Delete urgency record failed");
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
            List<Urgency> temp = requisitionDAO.GetAllUrgencies();
            if (temp != null)
                return temp;

            ErrorMessage("Result not found");
            return null;
        }

        /// <summary>
        /// Get urgency by primary key
        /// </summary>
        /// <param name="urgency">Urgency object</param>
        /// <returns>Urgency object</returns>
        public Urgency GetUrgencyByID(Urgency urgency)
        {
            Urgency temp = requisitionDAO.GetUrgencyByID(urgency);
            if (temp != null)
                return temp;

            ErrorMessage("Result not found");
            return null;

        }

        /// <summary>
        /// Get Urgency object by filter criteria
        /// </summary>
        /// <param name="urgencySearchDTO">urgencySearchDTO object</param>
        /// <returns>List of Urgency objects</returns>
        public List<Urgency> GetUrgencyByCriteria(UrgencySearchDTO urgencySearchDTO)
        {
            var temp = requisitionDAO.GetUrgencyByCriteria(urgencySearchDTO);
            if (temp != null)
                return temp;

            ErrorMessage("Result not found");
            return null;
        }
        #endregion

        #region ErrorMessage
        /// <summary>
        /// Validation error message
        /// </summary>
        /// <param name="message">message string</param>
        private static void ErrorMessage(string message)
        {
            try
            {
                throw new RequisitionException(message);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
