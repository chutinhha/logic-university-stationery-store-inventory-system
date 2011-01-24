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
        [Required(ErrorMessage="Please enter item code.")]
        [RegularExpression(".{4}", ErrorMessage="Pleaes enter valid item code.")]
        public string ItemCode { get; set; }
    }
}