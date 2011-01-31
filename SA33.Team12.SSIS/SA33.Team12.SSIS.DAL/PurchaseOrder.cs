
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

         //This expression validates dates in the ITALIAN d/m/y format from 1/1/1600 - 31/12/9999.
         //The days are validated for the given month and year. Leap years are validated for all 4 digits years from 1600-9999,
         //and all 2 digits years except 00 since it could be any century (1900, 2000, 2100). 
         //Days and months must be 1 or 2 digits and may have leading zeros. Years must be 2 or 4 digit years. 
         //4 digit years must be between 1600 and 9999. Date separator may be a slash (/), dash (-), or period (.)
         [Required(ErrorMessage = "Please select a expected supply date!")]
         [RegularExpression("^(?:(?:31(\\/|-|\\.)(?:0?[13578]|1[02]))\\1|(?:(?:29|30)(\\/|-|\\.)(?:0?[1,3-9]|1[0-2])\\2))(?:(?:1[6-9]|[2-9]\\d)?\\d{2})$|^(?:29(\\/|-|\\.)0?2\\3(?:(?:(?:1[6-9]|[2-9]\\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468]", ErrorMessage = "Please enter valid date format.")]
         public DateTime DateToSupply { get; set; }

         [Required(ErrorMessage = "Please select a supplier from the list")]
         public int SupplierID { get; set; }


     }
}
