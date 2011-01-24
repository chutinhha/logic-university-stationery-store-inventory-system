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
        public List<User> FindUserByCriteria(DTO.UserSearchDTO criteria)
        {
            var Query = 
                from u in context.Users
                where u.UserID == (criteria.UserID == null ? u.UserID : criteria.UserID)
                && u.DepartmentID == (criteria.DepartmentID == null ? u.DepartmentID : criteria.DepartmentID)
                && u.UserName.Contains((criteria.UserName == null ? u.UserName : criteria.UserName))
                && u.MembershipProviderKey == (criteria.MembershipProviderKey == null ? u.MembershipProviderKey : criteria.MembershipProviderKey)
                && u.FirstName.Contains((criteria.FirstName == null ? u.FirstName : criteria.FirstName))
                && u.LastName.Contains((criteria.LastName == null ? u.LastName : criteria.LastName))
                && u.FirstName == (criteria.FirstName == null ? u.FirstName : criteria.FirstName)
                select u;
            List<User> users = Query.ToList<User>();
            return users;
        }

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
                throw new Exceptions.UserException("User data not found!");
            }
        }

        public void CreateUser(DAL.User user)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    context.Users.AddObject(user);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
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
                try
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        context.Attach(user);
                        context.ObjectStateManager.ChangeObjectState(user, EntityState.Modified);
                        context.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
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
                try
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        context.Attach(user);
                        context.Users.DeleteObject(user);
                        context.SaveChanges();
                        ts.Complete();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                throw new Exceptions.UserException("No user to delete.");
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

        public User GetUserByID(int userID)
        {
            return (from u in context.Users
                    where u.UserID == userID
                    select u).FirstOrDefault<User>();
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

        public Department CreateDepartment(Department department)
        {
            context.Departments.AddObject(department);
            context.SaveChanges();
            return department;
        }

        public Department UpdateDepartment(Department department)
        {
            Department persistedDepartment = (from d in context.Departments
                                              where d.DepartmentID == department.DepartmentID
                                              select d).FirstOrDefault<Department>();
            if (persistedDepartment != null)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        persistedDepartment.Code = department.Code;
                        persistedDepartment.Name = department.Name;
                        persistedDepartment.isBlackListed = department.isBlackListed;

                        CollectionPoint newCollectionPoint = 
                                (from c in context.CollectionPoints
                                 where c.CollectionPointID == department.CollectionPointID
                                 select c).First<CollectionPoint>();

                        persistedDepartment.CollectionPoint = newCollectionPoint;
                        context.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return persistedDepartment;
            }
            else
            {
                throw new Exceptions.DepartmentException("No department found to update.");
            }
        }

        public void DeleteDepartment()
        {
            throw new System.NotImplementedException();
        }

        public void GetDepartmentByUserID(string user)
        {
            throw new System.NotImplementedException();
        }
    }
}
