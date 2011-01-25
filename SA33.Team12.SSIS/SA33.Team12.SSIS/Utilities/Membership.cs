using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;

using WebSecurity = System.Web.Security;

namespace SA33.Team12.SSIS.Utilities
{
    public static class Membership
    {
        public static void CreateUser(DAL.User user)
        {
            using (BLL.UserManager um = new BLL.UserManager())
            {
                WebSecurity.MembershipUser membershipUser = 
                    WebSecurity.Membership.CreateUser(user.UserName, user.Password, user.Email);

                //um.CreateUser(user);
            }
        }
    }
}