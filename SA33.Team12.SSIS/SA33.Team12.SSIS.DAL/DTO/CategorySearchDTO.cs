/***
 * Author: Victor Tong(A0066920E)
 * Initial Implementation: 26/Jan/2011
 ***/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA33.Team12.SSIS.DAL.DTO
{
    public class CategorySearchDTO
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public bool IsApproved { get; set; }
        public DateTime StartDateCreated { get; set; }
        public DateTime EndDateCreated { get; set; }
        public DateTime ExactDateCreated { get; set; }
        public DateTime StartDateModified { get; set; }
        public DateTime EndDateModified { get; set; }
        public DateTime ExactDateModified { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int ApprovedBy { get; set; }
    }
}