using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA33.Team12.SSIS.DAL.DTO
{
    public class StationerySearchDTO
    {
        public int StationeryID { get; set; }
        public int CategoryID { get; set; }
        public int LocationID { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public int ReorderLevel { get; set; }
        public int ReorderQuantity { get; set; }
        public int QuantityInHand { get; set; }
        public DateTime StartDateCreated { get; set; }
        public DateTime EndDateCreated { get; set; }
        public DateTime ExactDateCreated { get; set; }
        public DateTime StartDateModified { get; set; }
        public DateTime EndDateModified { get; set; }
        public DateTime ExactDateModified { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int ApprovedBy { get; set; }
        public bool IsApproved { get; set; }
    }
}
