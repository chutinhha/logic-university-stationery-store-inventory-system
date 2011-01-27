
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
         public string AttentionTo { get; set; }

         [Required(ErrorMessage = "Please select a expected supply date!")]
         public string DateReceived { get; set; }
     }
}
