using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA33.Team12.SSIS.DAL.DTO
{
    class CategorySearchDTO
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string UnitOfMeasure { get; set; }
        public bool IsApproved { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int ApprovedBy { get; set; }
    }
}