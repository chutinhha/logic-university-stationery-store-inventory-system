using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA33.Team12.SSIS.BLL.DTO
{
    public class UserSearchDTO
    {
        public int UserID { get; set; }
        public int DepartmentID { get; set; }
        public string UserName { get; set; }
        public Guid MembershipProviderKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
