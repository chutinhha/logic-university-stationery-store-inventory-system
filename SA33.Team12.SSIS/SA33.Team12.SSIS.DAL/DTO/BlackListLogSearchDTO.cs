using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA33.Team12.SSIS.DAL.DTO
{
    public class BlackListLogSearchDTO
    {
        public int BlackListLogID { get; set; }
        public int DepartmentID { get; set; }
        public DateTime StartDateBlackListed { get; set; }
        public DateTime EndDateBlackListed { get; set; }
    }
}
