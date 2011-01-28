using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA33.Team12.SSIS.DAL.DTO
{
    public class StationeryPriceSearchDTO
    {
        public int StationeryPriceID { get; set; }
        public int StationeryID { get; set; }
        public int SupplierID { get; set; }
        public decimal Price { get; set; }
    }
}
