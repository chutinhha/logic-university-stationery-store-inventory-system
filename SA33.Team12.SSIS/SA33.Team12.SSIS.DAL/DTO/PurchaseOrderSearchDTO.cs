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
    public class PurchaseOrderSearchDTO
    {
        public int PurchaseOrderID { get; set; }
        public int SupplierID { get; set; }
        public DateTime StartDateOfOrder { get; set; }
        public DateTime EndDateOfOrder { get; set; }
        public int PONumber { get; set; }
        public int AttentionTo { get; set; }
        public int CreatedBy { get; set; }
        public String DONumber { get; set; }
        public DateTime DateReceived { get; set; }
        public DateTime ReceiveBy { get; set; }
        public Boolean IsDelivered { get; set; }
    }
}
