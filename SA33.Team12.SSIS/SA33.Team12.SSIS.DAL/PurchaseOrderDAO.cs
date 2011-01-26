/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 * Edited by Wang Pinyi (A0076771W)
 * on 25/Jan/2011
 ***/

using System;
using System.Web;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;

namespace SA33.Team12.SSIS.DAL
{
    public class PurchaseOrderDAO : DALLogic
    {
        public PurchaseOrder CreatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            try
            {
                this.context.PurchaseOrders.AddObject(purchaseOrder);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new System.NotImplementedException();
            }

            return purchaseOrder;
        }
        public List<PurchaseOrder> GetAllPurchaseOrder()
        {
            return (from p in context.PurchaseOrders
                    select p).ToList();
        }

        public void DeletePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            this.context.PurchaseOrders.Attach(purchaseOrder);
            this.context.PurchaseOrders.DeleteObject(purchaseOrder);
            this.context.SaveChanges();
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
                    // add more properties here
                }
                this.context.SaveChanges();
                return purchaseOrder;
            }
            catch (Exception e)
            {
                    throw new System.NotImplementedException();
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
                    && u.UserName.Contains((criteria.UserName == null ? u.UserName : criteria.UserName))
                    && u.FirstName.Contains((criteria.FirstName == null ? u.FirstName : criteria.FirstName))
                    && u.LastName.Contains((criteria.LastName == null ? u.LastName : criteria.LastName))
                    && u.Email == (criteria.Email == null ? u.Email : criteria.Email)
                    select u;
                List<PurchaseOrder> purchaseOrders = Query.ToList<PurchaseOrder>();
                return purchaseOrders;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
