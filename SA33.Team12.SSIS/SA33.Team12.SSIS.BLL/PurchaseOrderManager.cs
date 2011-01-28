/***
 * Author: Wang Pinyi (A0076771W)
 * Initial Implementation: 25/Jan/2011
 ***/

using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using SA33.Team12.SSIS.DAL;
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

        //CRUD for PurchaseOrder
        public PurchaseOrder CreatePurchaseOrder(PurchaseOrder po)
        {
            return purchaseOrderDAO.CreatePurchaseOrder(po);
        }

        public PurchaseOrder UpdatePurchaseOrder(PurchaseOrder po)
        {
            return purchaseOrderDAO.UpdatePurchaseOrder(po);
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
            return purchaseOrderDAO.CreatePurchaseOrderItem(item);
        }

        public PurchaseOrderItem UpdatePurchaseOrderItem(PurchaseOrderItem item)
        {
            return purchaseOrderDAO.UpdatePurchaseOrderItem(item);
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
    }
}
