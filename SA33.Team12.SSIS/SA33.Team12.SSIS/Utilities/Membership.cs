﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Security;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;
using WebSecurity = System.Web.Security;

namespace SA33.Team12.SSIS.Utilities
{
    public static class Membership
    {
        public static User LoggedInuser
        {
            get { return Utilities.Membership.GetCurrentLoggedInUser(); }
        }

        public static string[] Roles
        {
            get { return Utilities.Membership.GetCurrentLoggedInUserRole(); }
        }

        public static bool IsLoggedIn
        {
            get { WebSecurity.MembershipUser muser = WebSecurity.Membership.GetUser();
            if (muser != null) return true;
            return false;
            }
        }

        public static bool IsAdmin
        {
            get
            {
                var isAdmin = (from r in Membership.Roles
                               where r.Contains("Administrators")
                               select r);
                return (isAdmin.Count() > 0);
            }
        }

        public static bool IsDeptHead
        {
            get
            {
                var isDeptHead = (from r in Membership.Roles
                                  where r.Contains("DepartmentHeads")
                                  select r);
                return (isDeptHead.Count() > 0);
            }
        }

        public static bool IsTempDeptHead
        {
            get
            {
                var isDeptHead = (from r in Membership.Roles
                                  where r.Contains("TemporaryDepartmentHeads")
                                  select r);
                return (isDeptHead.Count() > 0);
            }
        }

        public static bool IsDeptRepresentative
        {
            get
            {
                var isDeptHead = (from r in Membership.Roles
                                  where r.Contains("DepartmentRepresentatives")
                                  select r);
                return (isDeptHead.Count() > 0);
            }
        }

        public static bool IsStoreManager
        {
            get
            {
                var isStoreManager = (from r in Membership.Roles
                                  where r.Contains("StoreManagers")
                                  select r);
                return (isStoreManager.Count() > 0);
            }
        }

        public static bool IsStoreSupervisor
        {
            get
            {
                var q = (from r in Membership.Roles
                         where r.Contains("StoreSupervisors")
                         select r);
                return (q.Count() > 0);
            }
        }

        public static bool IsEmployee
        {
            get
            {
                var q = (from r in Membership.Roles
                         where r.Contains("Employees")
                         select r);
                return (q.Count() > 0);
            }
        }

        public static bool IsStoreClerk
        {
            get
            {
                var q = (from r in Membership.Roles
                         where r.Contains("StoreClerks")
                         select r);
                return (q.Count() > 0);
            }
        }
        
        private static void AddUserToRoles(string userName, string[] roles)
        {
            string[] allRoles = WebSecurity.Roles.GetAllRoles();
            foreach (string role in allRoles)
            {
                if (WebSecurity.Roles.IsUserInRole(userName, role))
                    WebSecurity.Roles.RemoveUserFromRole(userName, role);
            }
            WebSecurity.Roles.AddUserToRoles(userName, roles);
        }

        public static User GetCurrentLoggedInUser()
        {
            WebSecurity.MembershipUser membershipUser = WebSecurity.Membership.GetUser();
            if (membershipUser != null)
            {
                using (UserManager userManager = new UserManager())
                {
                    List<User> users =
                        userManager.FindUsersByCriteria(
                            new UserSearchDTO() { UserName = membershipUser.UserName });
                    if (users.Count > 0)
                        return users[0];
                    else
                    {
                        throw new Exceptions.UserException("No current logged in user.");
                    }
                }
            }
            else
            {
                throw new Exceptions.UserException("No current logged in user.");
            }
        }

        public static string[] GetCurrentLoggedInUserRole()
        {
            return WebSecurity.Roles.GetRolesForUser();
        }

        public static DAL.User CreateUser(DAL.User user)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    using (BLL.UserManager um = new BLL.UserManager())
                    {
                        if (user.Password == null || user.Password.Trim() == "")
                            user.Password = WebSecurity.Membership.GeneratePassword(8, 0);
                        WebSecurity.MembershipUser membershipUser =
                            WebSecurity.Membership.CreateUser(user.UserName, user.Password, user.Email);
                        Guid ProviderKey = (Guid)membershipUser.ProviderUserKey;
                        user.MembershipProviderKey = ProviderKey;
                        if (user.Role.Trim() != "")
                        {
                            string[] roles
                                = user.Role.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            if (roles.Length > 0)
                                AddUserToRoles(user.UserName, roles);
                        }
                        um.CreateUser(user);
                    }
                    ts.Complete();
                }
                return user;
            }
            catch (WebSecurity.MembershipCreateUserException)
            {
                throw new Exceptions.UserException("User account creation failed.");
            }
            catch (WebSecurity.MembershipPasswordException)
            {
                throw new Exceptions.UserException("Please provide a valid password.");
            }
            catch (Exception)
            {
                throw new Exceptions.UserException("User account creation failed.");
            }
        }

        public static DAL.User UpdateUser(DAL.User user)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    using (BLL.UserManager um = new BLL.UserManager())
                    {
                        DAL.User oldUser = um.GetUserByID(user.UserID);
                        if (oldUser != null)
                        {
                            if (oldUser.UserName != user.UserName)
                                throw new Exceptions.UserException("Changing user name is not allowed.");
                            string[] roles = user.Role.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            if (roles.Length > 0)
                                AddUserToRoles(user.UserName, roles);
                            MembershipUser mUser = WebSecurity.Membership.GetUser(user.UserName);
                            user.MembershipProviderKey = (Guid)mUser.ProviderUserKey;
                            user = um.UpdateUser(user);
                        }
                        else
                        {
                            throw new Exceptions.UserException("No user found to update.");
                        }
                    }
                    ts.Complete();
                    return user;
                }

            }
            catch (Exceptions.UserException userex)
            {
                throw userex;
            }
            catch (Exception)
            {
                throw new Exceptions.UserException("Updating user failed.");
            }
        }

        public static void DeleteUser(DAL.User user)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    using (BLL.UserManager um = new BLL.UserManager())
                    {
                        WebSecurity.Membership.DeleteUser(user.UserName);
                        um.DeleteUser(user);
                    }
                    ts.Complete();
                }
            }
            catch (Exception)
            {
                throw new Exceptions.UserException("Deleting user failed.");
            }
        }

        public static List<DAL.User> GetAllUsers()
        {
            using (BLL.UserManager um = new BLL.UserManager())
            {
                return um.GetAllUsers();
            }
        }

        public static DAL.User GetUserById(int userId)
        {
            using (BLL.UserManager um = new BLL.UserManager())
            {
                return um.GetUserByID(userId);
            }
        }

        public static List<DAL.User> FindUsersByCriteria(DAL.DTO.UserSearchDTO criteria)
        {
            using (BLL.UserManager um = new UserManager())
            {
                return um.FindUsersByCriteria(criteria);
            }
        }

        public static List<DAL.User> GetUsersById(int userId)
        {
            User user = GetUserById(userId);
            if (user != null) return new List<User>() { user };
            return null;
        }

        public static DAL.User DisableUser(DAL.User user)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    WebSecurity.MembershipUser membershipUser
                        = WebSecurity.Membership.GetUser(user.UserName);
                    membershipUser.IsApproved = false;
                    WebSecurity.Membership.UpdateUser(membershipUser);
                    user.IsEnabled = false;
                    Utilities.Membership.UpdateUser(user);
                    ts.Complete();
                }
                return user;
            }
            catch (NullReferenceException)
            {
                throw new Exceptions.UserException("User account is not found.");
            }
            catch (Exception)
            {
                throw new Exceptions.UserException("Disabling user failed.");
            }
        }

        public static DAL.User EnableUser(DAL.User user)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    WebSecurity.MembershipUser membershipUser
                        = WebSecurity.Membership.GetUser(user.UserName);
                    membershipUser.UnlockUser();
                    membershipUser.IsApproved = true;
                    WebSecurity.Membership.UpdateUser(membershipUser);
                    user.IsEnabled = true;
                    Utilities.Membership.UpdateUser(user);
                    ts.Complete();
                }
                return user;
            }
            catch (NullReferenceException)
            {
                throw new Exceptions.UserException("User account is not found.");
            }
            catch (Exception)
            {
                throw new Exceptions.UserException("Enabling user failed.");
            }
        }
    }
}