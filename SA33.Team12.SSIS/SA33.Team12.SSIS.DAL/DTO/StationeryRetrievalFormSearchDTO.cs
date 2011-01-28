using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA33.Team12.SSIS.DAL.DTO
{
    public class StationeryRetrievalFormSearchDTO
    {
        public int StationeryRetrievalFormID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ExactDate { get; set; }
    }
}
