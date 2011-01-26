using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA33.Team12.SSIS.DAL.DTO
{
    public class DepartmentSearchDTO
    {
        public int DepartmentID { get; set; }
        public int CollectionPointID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsBlackListed { get; set; }
    }
}
