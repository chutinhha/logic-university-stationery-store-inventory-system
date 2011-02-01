﻿/***
 * Author: Wang Pinyi (A0076771W)
 * Initial Implementation: 25/Jan/2011
 ***/

using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.Transactions;
using System.ComponentModel;
using System.Diagnostics;
using System.Data.Objects;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.Exceptions;
using SA33.Team12.SSIS.DAL.DTO;


namespace SA33.Team12.SSIS.BLL
{
    public class PurchaseOrderManager : SA33.Team12.SSIS.BLL.BusinessLogic
    {
        private PurchaseOrderDAO purchaseOrderDAO;

        public PurchaseOrderManager()
        {
            purchaseOrderDAO = new PurchaseOrderDAO();
        }

        private enum PurchaseOrderMethod
        {
            Create, Update
        };

        //CRUD for PurchaseOrder
        public PurchaseOrder CreatePurchaseOrder(PurchaseOrder po)
        {
            bool isValid = false;
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            try
            {
                if (ValidatePurchaseOrder(po, PurchaseOrderMethod.Create))
                {
                    foreach (PurchaseOrderItem item in po.PurchaseOrderItems)
                    {
                        isValid = ValidatePurchaseOrderItem(item, PurchaseOrderMethod.Create);
                        if (!isValid)
                            break;
                    }
                    if (isValid)
                    {
                        purchaseOrder = purchaseOrderDAO.CreatePurchaseOrder(po);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return purchaseOrder;
        }

        public PurchaseOrder UpdatePurchaseOrder(PurchaseOrder po)
        {
            bool isValid = false;
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            try
            {
                if (ValidatePurchaseOrder(po, PurchaseOrderMethod.Update))
                {
                    foreach (PurchaseOrderItem item in po.PurchaseOrderItems)
                    {
                        isValid = ValidatePurchaseOrderItem(item, PurchaseOrderMethod.Create);
                        if (!isValid)
                            break;
                    }
                    if (isValid)
                    {
                        purchaseOrder = purchaseOrderDAO.UpdatePurchaseOrder(po);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return purchaseOrder;
        }

        public List<PurchaseOrder> FindPurchaseOrderByCriteria(PurchaseOrderSearchDTO poSearchDTO)
        {
            return purchaseOrderDAO.FindPurchaseOrderByCriteria(poSearchDTO);
        }

        public void DeletePurchaseOrder(PurchaseOrder po)
        {
            purchaseOrderDAO.DeletePurchaseOrder(po);
        }

        public List<PurchaseOrder> FindAllPurchaseOrder()
        {
            return purchaseOrderDAO.GetAllPurchaseOrder();
        }

        public PurchaseOrder FindPurchaseOrderByID(int poID)
        {
            return purchaseOrderDAO.FindPurchaseOrderByID(poID);
        }

        // CRUD for PurchaseOrderItem
        public PurchaseOrderItem CreatePurchaseOrderItem(PurchaseOrderItem item)
        {
            PurchaseOrderItem poItem = new PurchaseOrderItem();
            try
            {
                if (item != null && ValidatePurchaseOrderItem(item, PurchaseOrderMethod.Create))
                {
                    poItem = purchaseOrderDAO.CreatePurchaseOrderItem(item);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return poItem;
        }

        public PurchaseOrderItem UpdatePurchaseOrderItem(PurchaseOrderItem item)
        {
            PurchaseOrderItem poItem = new PurchaseOrderItem();
            try
            {
                if (item != null && ValidatePurchaseOrderItem(item, PurchaseOrderMethod.Update))
                {
                    poItem = purchaseOrderDAO.UpdatePurchaseOrderItem(item);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return poItem;
        }

        public void DeletePurchaseOrderItem(PurchaseOrderItem item)
        {
            purchaseOrderDAO.DeletePurchaseOrderItem(item);
        }

        public PurchaseOrderItem FindPurchaseOrderItemByID(int ID)
        {
            return purchaseOrderDAO.FindPurchaseOrderItemByID(ID);
        }

        public List<PurchaseOrderItem> FindPurchaseOrderItemByCriteria(PurchaseOrderItemSearchDTO criteria)
        {
            return purchaseOrderDAO.FindPurchaseOrderItemByCriteria(criteria);
        }

        public List<PurchaseOrderItem> FindAllPurchaseOrderItem()
        {
            return purchaseOrderDAO.GetAllPurchaseOrderItem();
        }

        // puchase order reorder automation, will handle both standard stationery and special stationery
        public int GetQuantityToOrder(Stationery item)
        {
            int orderQuantity = 0;

            // get all the requisitions/requisition items that are appoved by temp department head

            RequisitionSearchDTO criteria = new RequisitionSearchDTO();
            criteria.StatusID = 1;  // respoding to the status "Approved & pending"
            using (RequisitionManager rm = new RequisitionManager())
            {
                List<Requisition> requisitions = rm.FindRequisitionByCriteria(criteria);
                foreach (Requisition r in requisitions)
                {
                 
                        foreach (RequisitionItem ri in r.RequisitionItems)
                        {
                            if (ri.StationeryID == item.StationeryID)
                            {
                                orderQuantity += ri.QuantityRequested;
                            }
                        }
                        using (CatalogManager cm = new CatalogManager())
                        {
                            Stationery stationeryToOrder = cm.FindStationeryByID(item.StationeryID);
                            orderQuantity += stationeryToOrder.ReorderLevel - stationeryToOrder.QuantityInHand + stationeryToOrder.ReorderQuantity;
                        }
                    }
            }
            return orderQuantity;
        }

        public int GetQuantityToOrderSpecial(SpecialStationery item)
        {
            int orderQuantity = 0;
            RequisitionSearchDTO criteria = new RequisitionSearchDTO();
            criteria.StatusID = 1;  // respoding to the status "Approved & pending"
            using (RequisitionManager rm = new RequisitionManager())
            {
                List<Requisition> requisitions = rm.FindRequisitionByCriteria(criteria);
                foreach (Requisition r in requisitions)
                {
                    foreach (SpecialRequisitionItem ri in r.SpecialRequisitionItems)
                    {
                        if (ri.SpecialStationeryID == item.SpecialStationeryID)
                        {
                            orderQuantity += ri.QuantityRequested;
                        }
                    }
                }
            }
            return orderQuantity;
        }
        // validate puchase order
        private bool ValidatePurchaseOrder(PurchaseOrder purchaseOrder, PurchaseOrderMethod purchaseOrderMethod)
        {
            string errMsg = "";
            try
            {
                if (purchaseOrder != null)
                {
                    if (purchaseOrderMethod == PurchaseOrderMethod.Create)
                    {
                        errMsg = "Create Purchase Order failed. Please try again later";
                        if ((purchaseOrder.AttentionTo != 0 || purchaseOrder.AttentionToUser != null) &&
                            (purchaseOrder.CreatedBy != 0 || purchaseOrder.CreatedByUser != null) &&
                            (purchaseOrder.SupplierID != 0 || purchaseOrder.Supplier != null) &&
                            (purchaseOrder.PurchaseOrderItems != null) &&
                            (purchaseOrder.DateToSupply != null && DateTime.Compare(purchaseOrder.DateToSupply, DateTime.Now) >= 0) &&
                            (purchaseOrder.DateOfOrder != null && purchaseOrder.DateOfOrder.Date.ToShortDateString() == DateTime.Now.Date.ToShortDateString()))
                        {
                            return true;
                        }
                    }
                    if (purchaseOrderMethod == PurchaseOrderMethod.Update)
                    {
                        errMsg = "Update Purchase Order failed. Please try again later";
                        if ((purchaseOrder.AttentionTo != 0 || purchaseOrder.AttentionToUser != null) &&
                            (purchaseOrder.CreatedBy != 0 || purchaseOrder.CreatedByUser != null) &&
                            (purchaseOrder.SupplierID != 0 || purchaseOrder.Supplier != null) &&
                            (purchaseOrder.PurchaseOrderItems != null) &&
                            (purchaseOrder.DateToSupply != null) &&
                            (purchaseOrder.DateOfOrder != null))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

            catch (Exception)
            {
                throw new PurchaseOrderException(errMsg);
            }
        }

        // validate purchase order item
        private bool ValidatePurchaseOrderItem(PurchaseOrderItem item, PurchaseOrderMethod purchaseOrderItemMethod)
        {
            string errMsg = "";
            try
            {
                if (item != null)
                {
                    if (purchaseOrderItemMethod == PurchaseOrderMethod.Create)
                    {
                        errMsg = "Create Purchase Order item failed. Please try again later";
                        if ((item.PurchaseOrderID != 0 || item.PurchaseOrder != null) &&
                            ((item.StationeryID != 0 && item.SpecialStationeryID == 0) || ((item.StationeryID == 0 && item.SpecialStationeryID != 0))) &&
                            (item.QuantityToOrder != 0))
                        {
                            return true;
                        }
                    }
                    if (purchaseOrderItemMethod == PurchaseOrderMethod.Update)
                    {
                        errMsg = "Update Purchase Order item failed. Please try again later";
                        if ((item.PurchaseOrderID != 0 || item.PurchaseOrder != null) &&
                             ((item.StationeryID != 0 && item.SpecialStationeryID == 0) || ((item.StationeryID == 0 && item.SpecialStationeryID != 0))) &&
                             (item.QuantityToOrder != 0))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

            catch (Exception)
            {
                throw new PurchaseOrderException(errMsg);
            }
        }
    }
}
