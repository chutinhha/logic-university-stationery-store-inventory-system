using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SA33.Team12.SSIS.DAL
{
    [MetadataType(typeof(RequisitionMetaData))]
    public partial class Requisition
    {
    }

    public class RequisitionMetaData
    {
        [Required(ErrorMessage="Category name cannot be blank!")]
        [StringLength(255, ErrorMessage="Category name cannot be longer than 255 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Unit of measure name cannot be blank!")]
        [StringLength(255, ErrorMessage = "Unit of measure cannot be longer than 255 characters")]
        public string UnitOfMeasure { get; set; }
    }
}