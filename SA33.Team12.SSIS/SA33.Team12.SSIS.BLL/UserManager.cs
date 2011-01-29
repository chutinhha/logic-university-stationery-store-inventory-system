/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Collections.Generic;
using System.Transactions;
using SA33.Team12.SSIS.DAL;
using SA33.Team12.SSIS.DAL.DTO;
namespace SA33.Team12.SSIS.BLL
{
    public class UserManager : BusinessLogic
    {
        private UserDAO udao;

        public UserManager() {
            udao = new UserDAO();
        }

        #region Users
        public User CreateUser(User user)
        {
            try
            {
                udao.CreateUser(user);
            }
            catch (Exception)
            {
                throw new Exceptions.UserException("User account creation failed.");
            }
            return user;
        }

        public User UpdateUser(User user)
        {
            try
            {
                udao.UpdateUser(user);
            }
            catch (Exception)
            {
                throw new Exceptions.UserException("User account updating failed.");
            }
            return user;
        }

        public void DeleteUser(User user)
        {
            try
            {
                udao.DeleteUser(user);
            }
            catch (Exception)
            {
                throw new Exceptions.UserException("User account deletion failed.");
            }
        }

        public List<User> GetAllUsers()
        {
            return udao.GetAllUsers();
        }

        public User GetUserByID(int UserID)
        {
            return udao.GetUserByID(UserID);
        }

        public List<User> FindUsersByCriteria(UserSearchDTO criteria)
        {
            return udao.FindUsersByCriteria(criteria);
        }

        public User DisableUser(User user)
        {
            if ("administrator".CompareTo(user.UserName.ToLower()) == 0)
            {
                throw new Exceptions.UserException(@"Oh, ho! You are not allow to disable
                    the almighty Administrator account!");
            }
            user.IsEnabled = false;
            return this.udao.UpdateUser(user);
        }

        public User EnableUser(User user)
        {
            if ("administrator".CompareTo(user.UserName.ToLower()) == 0)
            {
                throw new Exceptions.UserException(@"Oh, ho! You are not allow to enable
                    the almighty Administrator account!");
            }
            user.IsEnabled = true;
            return this.udao.UpdateUser(user);
        }
        #endregion

        #region Departments
        public List<Department> GetAllDepartments()
        {
            return udao.GetAllDepartment();
        }
        #endregion
    }
}
