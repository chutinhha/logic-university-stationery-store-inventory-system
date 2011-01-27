/***
 * Author: Wang Pinyi (A0076771W)
 * Initial Implementation: 25/Jan/2011
 ***/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA33.Team12.SSIS.DAL.DTO
{
    public class PurchaseOrderItemSearchDTO
    {
        public int PurchaseOrderID { get; set; }
        public int StationeryID { get; set; }
        public int PurchaseOrderItemID { get; set; }
        public decimal Price { get; set; }
        public int QuantityToOrder { get; set; }
        public String DeliveryRemarks { get; set; }
    }
}
