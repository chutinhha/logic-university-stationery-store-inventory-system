using System;
using System.Web;
using System.ComponentModel;

namespace SA33.Team12.SSIS.BLL.DTO
{
    public class AdjustmentVoucherSearchDTO
    {
        public int AdjustmentVoucherID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
