/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Web;
using System.ComponentModel;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.BLL
{
    public class PurchaseOrderManager : SA33.Team12.SSIS.BLL.BusinessLogic
    {
        private PurchaseOrderDAO purchaseOrderDAO;

        public PurchaseOrderManager()
        {
            purchaseOrderDAO = new PurchaseOrderDAO();
        }

        public PurchaseOrder CreatePurchaseOrder(PurchaseOrder po)
        {
            return purchaseOrderDAO.CreatePurchaseOrder(po);
        }

        public PurchaseOrder UpdatePurchaseOrder(PurchaseOrder po)
        {
            return purchaseOrderDAO.UpdatePurchaseOrder(po);
        }
    }
}
