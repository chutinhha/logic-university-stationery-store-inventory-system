/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Web;
using System.ComponentModel;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;
using System.Collections.Generic;
using System.Diagnostics;

namespace SA33.Team12.SSIS.BLL
{
    public class DisbursementManager : SA33.Team12.SSIS.BLL.BusinessLogic
    {
        private DisbursementDAO disbursementDAO;
        private enum DisbursementMethod
        {
            Create, Update
        };

        public DisbursementManager()
        {
            disbursementDAO = new DisbursementDAO();
        }

        //CRUD for Disbursement
        public Disbursement CreateDisbursement(Disbursement disbursement)
        {
            bool isValid = false;
            Disbursement newDisbursement = new Disbursement();
            try
            {
                if (ValidateDisbursement(disbursement, DisbursementMethod.Create))
                {
                    foreach (DisbursementItem item in disbursement.DisbursementItems)
                    {
                        isValid = ValidateDisbursementItem(item, DisbursementMethod.Create);
                        if (!isValid)
                            break;
                    }
                    if (isValid)
                    {
                        newDisbursement = disbursementDAO.CreateDisbursement(disbursement);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return newDisbursement;
        }

        public Disbursement UpdateDisbursement(Disbursement disbursement)
        {
            bool isValid = false;
            Disbursement newDisbursement = new Disbursement();
            try
            {
                if (ValidateDisbursement(disbursement, DisbursementMethod.Update))
                {
                    foreach (DisbursementItem item in disbursement.DisbursementItems)
                    {
                        isValid = ValidateDisbursementItem(item, DisbursementMethod.Update);
                        if (!isValid)
                            break;
                    }
                    if (isValid)
                    {
                        newDisbursement = disbursementDAO.UpdateDisbursement(disbursement);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return newDisbursement;
        }

        public List<Disbursement> FindDisbursementByCriteria(DisbursementSearchDTO disbursementSearchDTO)
        {
            return disbursementDAO.FindDisbursementByCriteria(disbursementSearchDTO);
        }

        public void DeleteDisbursement(Disbursement disbursement)
        {
            disbursementDAO.DeleteDisbursement(disbursement);
        }

        public List<Disbursement> FindAllDisbursement()
        {
            return disbursementDAO.GetAllDisbursement();
        }

        public Disbursement FindDisbursementByID(int disbursementID)
        {
            return disbursementDAO.GetDisbursementByID(disbursementID);
        }

        // CRUD for Disbursement Item
        public DisbursementItem CreateDisbursementItem(DisbursementItem item)
        {
            DisbursementItem newItem = new DisbursementItem();
            try
            {
                if (item != null && ValidateDisbursementItem(item, DisbursementMethod.Create))
                {
                    newItem = disbursementDAO.CreateDisbursementItem(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return newItem;
        }

        public DisbursementItem UpdateDisbursementItem(DisbursementItem item)
        {
            DisbursementItem newItem = new DisbursementItem();
            try
            {
                if (item != null && ValidateDisbursementItem(item, DisbursementMethod.Update))
                {
                    newItem = disbursementDAO.UpdateDisbursementItem(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return newItem;
        }

        public void DeleteDisbursementItem(DisbursementItem item)
        {
            disbursementDAO.DeleteDisbursementItem(item);
        }

        public DisbursementItem FindDisbursementItemByID(int ItemID)
        {
            return disbursementDAO.GetDisbursementItemByID(ItemID);
        }

        public List<DisbursementItem> FindDisbursementItemByCriteria(DisbursementItemSearchDTO criteria)
        {
            return disbursementDAO.FindDisbursementItemsByCriteria(criteria);
        }

        public List<DisbursementItem> FindAllDisbursementItem()
        {
            return disbursementDAO.GetAllDisbursementItem();
        }

        // validate disbursement
        private bool ValidateDisbursement(Disbursement disbursement, DisbursementMethod disbursementMethod)
        {
            string errMsg = "";
            try
            {
                if (disbursement != null)
                {
                    if (disbursementMethod == DisbursementMethod.Create)
                    {
                        errMsg = "Create Disbursement failed. Please try again later";
                        if ((disbursement.CreatedBy != 0 || disbursement.User != null) &&
                            (disbursement.DateCreated != null && disbursement.DateCreated.Date.ToShortDateString() == DateTime.Now.Date.ToShortDateString()))
                        {
                            return true;
                        }
                    }
                    if (disbursementMethod == DisbursementMethod.Update)
                    {
                        errMsg = "Update Disbursement failed. Please try again later";
                        if ((disbursement.CreatedBy != 0 || disbursement.User != null) &&
                            (disbursement.DateCreated != null))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

            catch (Exception)
            {
                throw new Exceptions.DisbursmentException(errMsg);
            }
        }

        // validate disbursementItem
        private bool ValidateDisbursementItem(DisbursementItem item, DisbursementMethod disbursementItemMethod)
        {
            string errMsg = "";
            try
            {
                if (item != null)
                {
                    if (disbursementItemMethod == DisbursementMethod.Create)
                    {
                        errMsg = "Create disbursement item failed. Please try again later";
                        if ((item.DisbursementID != 0 || item.Disbursement != null) &&
                            (item.StationeryRetrievalFormItemByDeptID != 0 || item.StationeryRetrievalFormItemByDept != null) &
                            (item.QuantityDisbursed != 0))
                        {
                            return true;
                        }
                    }
                    if (disbursementItemMethod == DisbursementMethod.Update)
                    {
                        errMsg = "Update disbursement item failed. Please try again later";
                        if ((item.DisbursementID != 0 || item.Disbursement != null) &&
                            (item.StationeryRetrievalFormItemByDeptID != 0 || item.StationeryRetrievalFormItemByDept != null) &
                            (item.QuantityDisbursed != 0))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

            catch (Exception)
            {
                throw new Exceptions.DisbursmentException(errMsg);
            }
        }
    }
}
