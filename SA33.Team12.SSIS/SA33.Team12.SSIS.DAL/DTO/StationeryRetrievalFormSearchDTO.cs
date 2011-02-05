using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA33.Team12.SSIS.DAL.DTO
{
    public class StationeryRetrievalFormSearchDTO
    {
        public int StationeryRetrievalFormID { get; set; }
        public DateTime StartDateRetrieved { get; set; }
        public DateTime EndDateRetrieved { get; set; }
        public DateTime ExactDateRetrieved { get; set; }
        public bool? IsRetrieved { get; set; }
        public bool? IsCollected { get; set; }
        public bool? IsDistributed { get; set; }
    }
}
