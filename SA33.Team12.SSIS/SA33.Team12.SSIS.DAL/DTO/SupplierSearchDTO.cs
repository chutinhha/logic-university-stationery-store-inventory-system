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
    public class SupplierSearchDTO
    {
        public int SupplierID { get; set; }
        public string SupplierCode { get; set; }
        public string CompanyName { get; set; }
        public DateTime StartTenderedYear { get; set; }
        public DateTime EndTenderedYear { get; set; }
        public DateTime ExactTenderedYear { get; set; }
        public int PreferredRank { get; set; }
    }
}
