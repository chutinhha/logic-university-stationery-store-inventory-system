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
     [MetadataType(typeof(PurchaseOrderMetaData))]
    public partial class PurchaseOrder
    {
    }

     public class PurchaseOrderMetaData
     {
         [Required(ErrorMessage = "Please select an attention person!")]
      //   [StringLength(255, ErrorMessage = "Category name cannot be longer than 255 characters")]
         public string AttentionTo { get; set; }

         [Required(ErrorMessage = "Please select a expected supply date!")]
 //        [StringLength(255, ErrorMessage = "Unit of measure cannot be longer than 255 characters")]
         public string DateReceived { get; set; }
     }
}
