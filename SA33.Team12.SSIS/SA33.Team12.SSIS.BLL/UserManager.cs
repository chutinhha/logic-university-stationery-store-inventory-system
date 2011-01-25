/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Web;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.Security;

using System.Transactions;
using SA33.Team12.SSIS.DAL;

namespace SA33.Team12.SSIS.BLL
{
    public class UserManager : BusinessLogic
    {

        public User DisableUser(User user)
        {
            if ("administrator".CompareTo(user.UserName.ToLower()) == 0)
            {
                throw new Exceptions.UserException(@"Oh, ho! You are not allow to disable
                    the almighty Administrator account!");
            }
            return user;
        }

        public User EnableUser(User user)
        {
            if ("administrator".CompareTo(user.UserName.ToLower()) == 0)
            {
                throw new Exceptions.UserException(@"Oh, ho! You are not allow to enable
                    the almighty Administrator account!");
            }
            return user;
        }

        /*
        public User GetUserByMemberShip(MembershipUser membershipUser)
        {
            Guid providerKey = (Guid)membershipUser.ProviderUserKey;
            User user = (from u in context.Users
                         where u.MembershipProviderKey == providerKey
                         select u).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new NullReferenceException("User data not found!");
            }
        }

        public void CreateUser(DAL.User user, MembershipUser membershipUser)
        {
            try
            {
                Guid providerKey = (Guid)membershipUser.ProviderUserKey;
                user.MembershipProviderKey = providerKey;

                context.Users.AddObject(user);
                context.SaveChanges();
            }
            catch (MembershipCreateUserException exception)
            {
                throw new Exceptions.UserException(exception.Message);
            }
            catch (MembershipPasswordException exception)
            {
                throw new Exceptions.UserException(exception.Message);
            }
        }

        public void UpdateUser(User user)
        {
            User tempUser = (from u in context.Users
                             where u.UserID == user.UserID
                             select u).FirstOrDefault();
            if (tempUser != null)
            {
                tempUser.FirstName = user.FirstName;
                tempUser.LastName = user.LastName;
                tempUser.Email = user.Email;

                context.Attach(user);
                context.ObjectStateManager.ChangeObjectState(user, EntityState.Modified);
                context.SaveChanges();
            }
            else
            {
                throw new NullReferenceException("User not found!");
            }
        }

        public void DeleteUser(string userName)
        {
            User user = (from u in context.Users
                         where u.UserName.ToLower() == userName.ToLower()
                         select u).FirstOrDefault();
            if (user != null)
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    context.Attach(user);
                    context.Users.DeleteObject(user);
                    context.SaveChanges();
                    ts.Complete();
                }
            }
        }

        public void DisableUser(string userName)
        {
            if ("administrator".CompareTo(userName.ToLower()) == 0)
            {
                throw new Exceptions.UserException(@"Oh, ho! You are not allow to disable
                    the almighty Administrator account!");
            }
        }

        public void GetUserByID()
        {
            throw new System.NotImplementedException();
        }

        public void GetUserByProfile()
        {
            throw new System.NotImplementedException();
        }

        public List<Department> GetAllDepartment()
        {
            return (from d in context.Departments
                    select d).ToList();
        }

        public Department GetDepartmentByID(int departmentID)
        {
            return (from d in context.Departments
                    where d.DepartmentID == departmentID
                    select d).FirstOrDefault();
        }

        public void CreateDepartment()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateDepartment()
        {
            throw new System.NotImplementedException();
        }

        public void DeleteDepartment()
        {
            throw new System.NotImplementedException();
        }

        public void GetDepartmentByUserID(string user)
        {
            throw new System.NotImplementedException();
        }*/
    }
}
