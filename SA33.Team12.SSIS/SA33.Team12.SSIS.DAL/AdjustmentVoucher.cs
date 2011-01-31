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
        // voucher number must be five digits
        [Required(ErrorMessage="Please enter voucher number")]
        [RegularExpression("^[0-9]{5}$", ErrorMessage="Please enter valid voucher number.")]
        public string VoucherNumber { get; set; }
    }
}