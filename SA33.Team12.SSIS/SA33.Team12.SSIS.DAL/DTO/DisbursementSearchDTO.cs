﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA33.Team12.SSIS.DAL.DTO
{
    public class DisbursementSearchDTO
    {
        public int DisbursementID {get;set;}
        public int DepartmentID { get; set; }
        public DateTime DateCreated{get;set;}
        public int CreatedBy{get;set;}
        public int StationeryRetrievalFormID { get; set; }
    }
}
