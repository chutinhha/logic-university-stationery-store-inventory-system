using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SA33.Team12.SSIS.DAL
    //Created by Anthony 26 Jan 2011
{
    [MetadataType(typeof(AdjustmentVoucherMetaData))]
    public partial class AdjustmentVoucher
    {
    }

    public class AdjustmentVoucherMetaData
    {
        [Required(ErrorMessage="Please enter item code.")]
        [RegularExpression(".{4}", ErrorMessage="Please enter valid item code.")]
        public string StationeryID { get; set; }
    }
}