using System;
using System.Collections.Generic;
using System.Transactions;
using SA33.Team12.SSIS.BLL;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;
using WebSecurity = System.Web.Security;

namespace SA33.Team12.SSIS.Utilities
{
    public static class Membership
    {
        private static void AddUserToRoles(string userName, string[] roles)
        {
            string[] allRoles = WebSecurity.Roles.GetAllRoles();
            foreach (string role in roles)
            {
                if(WebSecurity.Roles.IsUserInRole(userName, role))
                    WebSecurity.Roles.RemoveUserFromRole(userName, role);
            }
            WebSecurity.Roles.AddUserToRoles(userName, roles);
        }

        public static User GetCurrentLoggedInUser()
        {
            WebSecurity.MembershipUser membershipUser  = WebSecurity.Membership.GetUser();
            if(membershipUser != null)
            {
                using (UserManager userManager  = new UserManager())
                {
                    List<User> users =
                        userManager.FindUsersByCriteria(
                            new UserSearchDTO() {UserName = membershipUser.UserName});
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
                        if(user.Role.Trim() != "")
                        {
                             string[] roles 
                                 = user.Role.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
                            if(roles.Length > 0)
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
                using (BLL.UserManager um = new BLL.UserManager())
                {
                    DAL.User oldUser = um.GetUserByID(user.UserID);
                    if (oldUser != null)
                    {
                        if (oldUser.UserName != user.UserName)
                            throw new Exceptions.UserException("Changing user name is not allowed.");
                        string[] roles = user.Role.Split(',');
                        if (roles.Length > 0)
                            AddUserToRoles(user.UserName, roles);
                        user = um.UpdateUser(user);
                    }
                    else
                    {
                        throw new Exceptions.UserException("No user found to update.");
                    }
                }
                return user;
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
                    using (BLL.UserManager um = new BLL.UserManager())
                    {
                        um.DisableUser(user);
                    }
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
    }
}