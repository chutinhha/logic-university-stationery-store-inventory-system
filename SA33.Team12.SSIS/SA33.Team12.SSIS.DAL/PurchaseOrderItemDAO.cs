/***
 * Author: Wang Pinyi (A0076771W)
 * Initial Implementation: 25/Jan/2011
 ***/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SA33.Team12.SSIS.DAL
{
    class PurchaseOrderItemDAO : DALLogic
    {
        public PurchaseOrderItem CreatePurchaseOrderItem(PurchaseOrderItem purchaseOrderItem)
        {
            try
            {
                this.context.PurchaseOrderItems.AddObject(purchaseOrderItem);
                this.context.SaveChanges();
                return purchaseOrderItem;
            }
            catch (Exception e)
            {
                throw new System.NotImplementedException();
            }


        }
        public List<PurchaseOrderItem> GetAllPurchaseOrder()
        {
            return (from p in context.PurchaseOrderItems
                    select p).ToList();
        }
        public void DeletePurchaseOrderItem(PurchaseOrderItem purchaseOrderItem)
        {
            this.context.PurchaseOrderItems.Attach(purchaseOrderItem);
            this.context.PurchaseOrderItems.DeleteObject(purchaseOrderItem);
            this.context.SaveChanges();
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
            catch (Exception e)
            {
                throw;
            }
        }

    }
}
