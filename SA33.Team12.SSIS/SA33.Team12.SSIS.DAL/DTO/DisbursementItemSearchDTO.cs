using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA33.Team12.SSIS.DAL.DTO
{
    public class DisbursementItemSearchDTO
    {
        public int DisbursementItemID { get; set; }
        public int DisbursementID { get; set; }
        public int StationeryRetrievalFormItemByDeptID { get; set; }
        public int AdjustmentVoucherID { get; set; }
        public int StationeryID { get; set; }
        public int SpecialStationeryID { get; set; }
        public int QuantityDisbursed { get; set; }
        public int QuantityDamaged { get; set; }
        public string Reason { get; set; }
    }
}