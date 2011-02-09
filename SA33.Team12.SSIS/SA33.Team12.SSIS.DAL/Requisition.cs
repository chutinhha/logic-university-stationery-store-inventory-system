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
        [Required(ErrorMessage="Requisition form cannot be blank!")]
        [StringLength(13, ErrorMessage="Requisition form cannot be longer than 13 characters")]
        public string RequisitionForm { get; set; }
       
    }
}