/***
 * Author: Wang Pinyi (A0076771W)
 * Initial Implementation: 25/Jan/2011
 ***/

using System;
using System.Web;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using System.Transactions;
using System.Data.Objects;

namespace SA33.Team12.SSIS.DAL
{
    public class PurchaseOrderDAO : DALLogic
    {
        #region PurchaseOrder
        public PurchaseOrder CreatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    this.context.PurchaseOrders.AddObject(purchaseOrder);
                    this.context.SaveChanges();
                    ts.Complete();
                    return purchaseOrder;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<PurchaseOrder> GetAllPurchaseOrder()
        {
            return (from p in context.PurchaseOrders
                    select p).ToList();
        }
        public void DeletePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    this.context.PurchaseOrders.Attach(purchaseOrder);
                    this.context.PurchaseOrders.DeleteObject(purchaseOrder);
                    this.context.SaveChanges();
                    ts.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public PurchaseOrder UpdatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            try
            {
                PurchaseOrder purchaseOrderTemp = (from p in context.PurchaseOrders
                                                   where p.PurchaseOrderID == purchaseOrder.PurchaseOrderID
                                                   select p).FirstOrDefault();
                if (purchaseOrderTemp != null)
                {
                    purchaseOrderTemp.PONumber = purchaseOrder.PONumber;
                    purchaseOrderTemp.ReceivedBy = purchaseOrder.ReceivedBy;
                    purchaseOrderTemp.AttentionTo = purchaseOrder.AttentionTo;
                    purchaseOrderTemp.CreatedBy = purchaseOrder.CreatedBy;
                    purchaseOrderTemp.DateOfOrder = purchaseOrder.DateOfOrder;
                    purchaseOrderTemp.DateReceived = purchaseOrder.DateReceived;
                    purchaseOrderTemp.DONumber = purchaseOrder.DONumber;
                    purchaseOrderTemp.SupplierID = purchaseOrder.SupplierID;
                    purchaseOrderTemp.IsDelivered = purchaseOrder.IsDelivered;
                    purchaseOrderTemp.PurchaseOrderID = purchaseOrder.PurchaseOrderID;
                    // add more properties here
                }
                using (TransactionScope ts = new TransactionScope())
                {
                    this.context.SaveChanges();
                    ts.Complete();
                    return purchaseOrder;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<PurchaseOrder> FindPurchaseOrderByCriteria(DTO.PurchaseOrderSearchDTO criteria)
        {
            try
            {
                var Query =
                    from u in context.PurchaseOrders
                    where u.PurchaseOrderID == (criteria.PurchaseOrderID == 0 ? u.PurchaseOrderID : criteria.PurchaseOrderID)
                    && u.SupplierID == (criteria.SupplierID == 0 ? u.SupplierID : criteria.SupplierID)
                    && u.CreatedBy == (criteria.CreatedBy == 0 ? u.CreatedBy : criteria.CreatedBy)
                    && u.AttentionTo == (criteria.AttentionTo == 0 ? u.AttentionTo : criteria.AttentionTo)
                    && (EntityFunctions.DiffDays(u.DateOfOrder, (criteria.StartDateOfOrder == null || criteria.StartDateOfOrder == DateTime.MinValue ? u.DateOfOrder : criteria.StartDateOfOrder)) <= 0
                    && EntityFunctions.DiffDays(u.DateOfOrder, (criteria.EndDateOfOrder == null || criteria.EndDateOfOrder == DateTime.MinValue ? u.DateOfOrder : criteria.EndDateOfOrder)) >= 0)
                    && (EntityFunctions.DiffDays(u.DateOfOrder, (criteria.ExactDateOfOrder == null || criteria.ExactDateOfOrder == DateTime.MinValue ? u.DateOfOrder : criteria.ExactDateOfOrder)) == 0)
                    && u.IsDelivered == (criteria.IsDelivered == null ? u.IsDelivered : criteria.IsDelivered)
                    && u.PONumber == (criteria.PONumber == null ? u.PONumber : criteria.PONumber)
                    && (EntityFunctions.DiffDays(u.DateReceived, (criteria.StartDateReceived == null || criteria.StartDateReceived == DateTime.MinValue ? u.DateReceived : criteria.StartDateReceived)) <= 0
                    && EntityFunctions.DiffDays(u.DateReceived, (criteria.EndDateReceived == null || criteria.EndDateReceived == DateTime.MinValue ? u.DateReceived : criteria.EndDateReceived)) >= 0)
                    && (EntityFunctions.DiffDays(u.DateReceived, (criteria.ExactDateReceived == null || criteria.ExactDateReceived == DateTime.MinValue ? u.DateReceived : criteria.ExactDateReceived)) == 0)
                    && (EntityFunctions.DiffDays(u.DateToSuppy, (criteria.StartDateToSupply == null || criteria.StartDateToSupply == DateTime.MinValue ? u.DateToSuppy : criteria.StartDateToSupply)) <= 0
                    && EntityFunctions.DiffDays(u.DateToSuppy, (criteria.EndDateToSupply == null || criteria.EndDateToSupply == DateTime.MinValue ? u.DateToSuppy : criteria.EndDateToSupply)) >= 0)
                    && (EntityFunctions.DiffDays(u.DateToSuppy, (criteria.ExactDateToSupply == null || criteria.ExactDateToSupply == DateTime.MinValue ? u.DateToSuppy : criteria.ExactDateToSupply)) == 0)
                    && u.ReceivedBy == (criteria.ReceiveBy == 0 ? u.ReceivedBy : criteria.ReceiveBy)
                    && u.DONumber == (criteria.DONumber == null ? u.DONumber : criteria.DONumber)
                    select u;
                List<PurchaseOrder> purchaseOrders = Query.ToList<PurchaseOrder>();
                return purchaseOrders;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public PurchaseOrder FindPurchaseOrderByID(int poID)
        {
            try
            {
                PurchaseOrder purchaseOrderTemp = (from p in context.PurchaseOrders where p.PurchaseOrderID == poID select p).FirstOrDefault();
                return purchaseOrderTemp;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region PurchaseOrderItem

        public PurchaseOrderItem CreatePurchaseOrderItem(PurchaseOrderItem purchaseOrderItem)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    this.context.PurchaseOrderItems.AddObject(purchaseOrderItem);
                    this.context.SaveChanges();
                    ts.Complete();
                    return purchaseOrderItem;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public PurchaseOrderItem UpdatePurchaseOrderItem(PurchaseOrderItem purchaseOrderItem)
        {
            try
            {
                PurchaseOrderItem purchaseOrderItemTemp = (from p in context.PurchaseOrderItems
                                                           where p.PurchaseOrderItemID == purchaseOrderItem.PurchaseOrderItemID
                                                           select p).FirstOrDefault();
                if (purchaseOrderItemTemp != null)
                {
                    purchaseOrderItemTemp.PurchaseOrderItemID = purchaseOrderItem.PurchaseOrderItemID;
                    purchaseOrderItemTemp.StationeryID = purchaseOrderItem.StationeryID;
                    purchaseOrderItemTemp.QuantityToOrder = purchaseOrderItem.QuantityToOrder;
                    purchaseOrderItemTemp.PurchaseOrderID = purchaseOrderItem.PurchaseOrderID;
                    purchaseOrderItemTemp.Price = purchaseOrderItem.Price;
                    purchaseOrderItemTemp.DeliveryRemarks = purchaseOrderItem.DeliveryRemarks;
                    // add more properties here
                }
                using (TransactionScope ts = new TransactionScope())
                {
                    this.context.SaveChanges();
                    ts.Complete();
                    return purchaseOrderItem;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<PurchaseOrderItem> GetAllPurchaseOrderItem()
        {
            return (from p in context.PurchaseOrderItems
                    select p).ToList();
        }
        public void DeletePurchaseOrderItem(PurchaseOrderItem purchaseOrderItem)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    this.context.PurchaseOrderItems.Attach(purchaseOrderItem);
                    this.context.PurchaseOrderItems.DeleteObject(purchaseOrderItem);
                    this.context.SaveChanges();
                    ts.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<PurchaseOrderItem> FindPurchaseOrderItemByCriteria(DTO.PurchaseOrderItemSearchDTO criteria)
        {
            try
            {
                var Query =
                    from u in context.PurchaseOrderItems
                    where u.PurchaseOrderID == (criteria.PurchaseOrderID == 0 ? u.PurchaseOrderID : criteria.PurchaseOrderID)
                    && u.StationeryID == (criteria.StationeryID == 0 ? u.StationeryID : criteria.StationeryID)
                    && u.PurchaseOrderItemID == (criteria.PurchaseOrderItemID == 0 ? u.PurchaseOrderItemID : criteria.PurchaseOrderItemID)
                    && u.Price == (criteria.Price == 0 ? u.Price : criteria.Price)
                    && u.DeliveryRemarks == (criteria.DeliveryRemarks == null ? u.DeliveryRemarks : criteria.DeliveryRemarks)
                    && u.QuantityToOrder == (criteria.QuantityToOrder == 0 ? u.QuantityToOrder : criteria.QuantityToOrder)
                    select u;
                List<PurchaseOrderItem> purchaseOrderItems = Query.ToList<PurchaseOrderItem>();
                return purchaseOrderItems;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public PurchaseOrderItem FindPurchaseOrderItemByID(int poItemID)
        {
            try
            {
                PurchaseOrderItem purchaseOrderItemTemp = (from p in context.PurchaseOrderItems where p.PurchaseOrderItemID == poItemID select p).FirstOrDefault();
                return purchaseOrderItemTemp;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
