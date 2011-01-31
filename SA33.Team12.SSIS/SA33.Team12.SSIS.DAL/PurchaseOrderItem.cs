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
        [MetadataType(typeof(PurchaseOrderItemMetaData))]
        public partial class PurchaseOrderItem
        {
        }

        public class PurchaseOrderItemMetaData
        {
            // reorde quantity has to be a positive integer
            [Required(ErrorMessage = "Please fill in reorder quantity!")]
            [RegularExpression("^\\d+$", ErrorMessage = "Please enter a positive integer value.")]
            public int QuantityToOrder { get; set; }

            [Required(ErrorMessage = "Please select a stationery item!")]
            public int StationeryID { get; set; }

            [StringLength(255, ErrorMessage = "Delivery remarks cannot be longer than 255 characters")]
            public string DeliveryRemarks { get; set; }
        }
}
