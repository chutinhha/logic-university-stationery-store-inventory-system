using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.Utilities
{
    public class RequisitionBuilder
    {
        Requisition requisition;
        public RequisitionBuilder(Requisition requisition)
        {
            this.requisition = requisition;
        }

        public void AddRequisitionItem(RequisitionItem item)
        {
            requisition.RequisitionItems.Add(item);
        }

        public void UpdateRequisitionItem(RequisitionItem item)
        {
            RequisitionItem temp = requisition.RequisitionItems
                .Where(x => x.RequisitionItemID == item.RequisitionItemID)
                .FirstOrDefault<RequisitionItem>();

            temp.QuantityRequested = item.QuantityRequested;
            temp.StationeryID = item.StationeryID;
        }

        public void RemoveRequisitionItem(RequisitionItem item)
        {
            RequisitionItem temp = requisition.RequisitionItems
     .Where(x => x.RequisitionItemID == item.RequisitionItemID)
     .FirstOrDefault<RequisitionItem>();

            requisition.RequisitionItems.Remove(temp);
        }
    }
}