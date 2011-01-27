using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA33.Team12.SSIS.DAL.DTO
{
    public class ApprovalAuditSearchDTO
    {
        public int ApprovalAuditID { get; set; }
        public int ApprovedBy { get; set; }
        public string Reason { get; set; }
        public string StatusFrom { get; set; }
        public string StatusTo { get; set; }
    }
}
