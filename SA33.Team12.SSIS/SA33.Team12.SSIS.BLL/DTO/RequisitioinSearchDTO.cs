using System;
using System.Web;
using System.ComponentModel;

namespace SA33.Team12.SSIS.BLL.DTO
{
    public class RequisitioinSearchDTO
    {
        public int RequisitionID { get; set; }
        public DateTime StartDateRequested { get; set; }
        public DateTime  EndDateRequested { get; set; }
    }
}
