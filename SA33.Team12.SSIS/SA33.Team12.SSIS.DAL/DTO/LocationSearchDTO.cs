using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA33.Team12.SSIS.DAL.DTO
{
    public class LocationSearchDTO
    {
        public int LocationID { get; set; }
        public string Name { get; set; }
        public int CreatedBy { get; set; }
        public DateTime StartCreatedDate { get; set; }
        public DateTime EndCreatedDate { get; set; }
        public DateTime ExactCreatedDate { get; set; }

    }
}
