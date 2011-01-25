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
using System.Transactions;

namespace SA33.Team12.SSIS.DAL
{
    public class UserDAO : DALLogic
    {
        public List<User> FindUserByCriteria(DTO.UserSearchDTO criteria)
        {
            try
            {
                // This seems so easy to me
                var Query =
                    from u in context.Users
                    where u.UserID == (criteria.UserID == 0 ? u.UserID : criteria.UserID)
                    && u.DepartmentID == (criteria.DepartmentID == 0 ? u.DepartmentID : criteria.DepartmentID)
                    && u.UserName.Contains((criteria.UserName == null ? u.UserName : criteria.UserName))
                    && u.MembershipProviderKey == (criteria.MembershipProviderKey == new Guid() ? u.MembershipProviderKey : criteria.MembershipProviderKey)
                    && u.FirstName.Contains((criteria.FirstName == null ? u.FirstName : criteria.FirstName))
                    && u.LastName.Contains((criteria.LastName == null ? u.LastName : criteria.LastName))
                    && u.Email == (criteria.Email == null ? u.Email : criteria.Email)
                    select u;
                List<User> users = Query.ToList<User>();
                return users;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<User> GetUserByDepartment(DAL.Department department)
        {
            // isn't it obvious?
            List<User> users = FindUserByCriteria(new DTO.UserSearchDTO() { DepartmentID = department.DepartmentID });
            return users;
        }

        public User GetUserByID(int UserID)
        {
            return (from u in context.Users
                    where u.UserID == UserID
                    select u).First<User>();
        }

        public User CreateUser(DAL.User user)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    context.Users.AddObject(user);
                    context.SaveChanges();
                    return user;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User UpdateUser(DAL.User user)
        {
            try
            {
                // First will throw excpetion if no user is found
                User tempUser = (from u in context.Users
                                 where u.UserID == user.UserID
                                 select u).First<User>();


                tempUser.FirstName = user.FirstName;
                tempUser.LastName = user.LastName;
                tempUser.Email = user.Email;

                using (TransactionScope ts = new TransactionScope())
                {
                    context.Attach(tempUser);
                    context.ObjectStateManager.ChangeObjectState(tempUser, EntityState.Modified);
                    context.SaveChanges();
                    return tempUser;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteUser(User user)
        {
            try
            {
                User persistedUser = (from u in context.Users
                                      where u.UserName.ToLower() == user.UserName.ToLower()
                                      select u).FirstOrDefault();

                using (TransactionScope ts = new TransactionScope())
                {
                    context.Users.DeleteObject(persistedUser);
                    context.SaveChanges();
                    ts.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
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
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    context.Departments.AddObject(department);
                    context.SaveChanges();
                    return department;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Department UpdateDepartment(Department department)
        {
            try
            {
                Department persistedDepartment = (from d in context.Departments
                                                  where d.DepartmentID == department.DepartmentID
                                                  select d).First<Department>();
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

                    return persistedDepartment;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void DeleteDepartment(Department department)
        {
            try
            {

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void GetDepartmentByUserID(string user)
        {
            throw new System.NotImplementedException();
        }
    }
}
