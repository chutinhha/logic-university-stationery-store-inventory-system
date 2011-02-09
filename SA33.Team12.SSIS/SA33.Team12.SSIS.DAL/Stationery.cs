using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SA33.Team12.SSIS.DAL
{
    [MetadataType(typeof(StationeryMetaData))]
    public partial class Stationery
    {
    }

    public class StationeryMetaData
    {
        // item code format must be letter A-Z followed by 3 digits
        [Required(ErrorMessage="Please enter item code.")]
        [RegularExpression(".^[A-Z]{1}[0-9]{3}$", ErrorMessage = "Pleaes enter valid item code.")]
        public string ItemCode { get; set; }

        [Required(ErrorMessage = "Please enter item description.")]
        [StringLength(255, ErrorMessage = "Item description cannot be longer than 255 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter reorder level.")]
        [Range(0, 10000, ErrorMessage = "Value should be 0 - 10000")]
        public int ReorderLevel { get; set; }

        [Required(ErrorMessage = "Please enter reorder quantity.")]
        [Range(1,10000, ErrorMessage="Value should be 0 - 10000")]
        public int ReorderQuantity { get; set; }

        [Required(ErrorMessage = "Please enter quantity in hand.")]
        public int QuantityInHand { get; set; }

        [Required(ErrorMessage = "Please select category.")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Please select location.")]
        public int LocationID { get; set; }

        [Required(ErrorMessage = "Unit of measure name cannot be blank!")]
        [StringLength(255, ErrorMessage = "Unit of measure cannot be longer than 255 characters")]
        public string UnitOfMeasure { get; set; }

    }
}