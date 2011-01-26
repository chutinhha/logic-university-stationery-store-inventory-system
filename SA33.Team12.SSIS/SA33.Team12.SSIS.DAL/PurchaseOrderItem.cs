/***
 * Author: Wang Pinyi(A0076771W)
 * Initial Implementation: 25/Jan/2011
 ***/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SA33.Team12.SSIS.DAL
{
    public partial class PurchaseOrderItem
    {
        [MetadataType(typeof(PurchaseOrderItemMetaData))]
        public partial class PurchaseOrder
        {
        }

        public class PurchaseOrderItemMetaData
        {
            [Required(ErrorMessage = "Please fill in reorder quantity!")]
            public string QuantityToOrder { get; set; }

            [Required(ErrorMessage = "Please select stationery item!")]
            public string StationeryID { get; set; }
        }
    }
}
