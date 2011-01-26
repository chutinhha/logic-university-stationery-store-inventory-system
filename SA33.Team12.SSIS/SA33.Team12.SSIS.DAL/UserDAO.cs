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
        #region Users
        public List<User> FindUsersByCriteria(DTO.UserSearchDTO criteria)
        {
            try
            {
                // This seems so easy to me
                var Query =
                    from u in context.Users
                    where u.UserID == (criteria.UserID == 0 ? u.UserID : criteria.UserID)
                    && u.DepartmentID == (criteria.DepartmentID == 0 ? u.DepartmentID : criteria.DepartmentID)
                    && u.UserName.Contains((criteria.UserName == null || criteria.UserName == "" ? u.UserName : criteria.UserName))
                    && u.MembershipProviderKey == (criteria.MembershipProviderKey == new Guid() ? u.MembershipProviderKey : criteria.MembershipProviderKey)
                    && u.FirstName.Contains((criteria.FirstName == null || criteria.FirstName == "" ? u.FirstName : criteria.FirstName))
                    && u.LastName.Contains((criteria.LastName == null || criteria.LastName == "" ? u.LastName : criteria.LastName))
                    && u.Email == (criteria.Email == null || criteria.Email == "" ? u.Email : criteria.Email)
                    select u;
                List<User> users = Query.ToList<User>();
                return users;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<User> GetAllUsers()
        {
            return FindUsersByCriteria(new DTO.UserSearchDTO());
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
                    ts.Complete();
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
                    ts.Complete();
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
        #endregion

        #region Departments
        public List<Department> FindDepartmentsByCriteria(DTO.DepartmentSearchDTO criteria)
        {
            var Query =
                from d in context.Departments
                where d.DepartmentID == (criteria.DepartmentID == 0 ? d.DepartmentID : criteria.DepartmentID)
                && d.CollectionPointID == (criteria.CollectionPointID == 0 ? d.CollectionPointID : criteria.CollectionPointID)
                && d.Code == (criteria.Code == null || criteria.Code == "" ? d.Code : criteria.Code)
                && d.Name == (criteria.Name == null || criteria.Name == "" ? d.Name : criteria.Name)
                && d.isBlackListed == (criteria.IsBlackListed == null ? d.isBlackListed : criteria.IsBlackListed)
                select d;
            List<Department> departments = Query.ToList<Department>();
            return departments;
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
                    ts.Complete();
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
                    ts.Complete();
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
                using (TransactionScope ts = new TransactionScope())
                {
                    context.Departments.Attach(department);
                    context.Departments.DeleteObject(department);
                    context.SaveChanges();
                    ts.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region CollectionPoints
        public List<CollectionPoint> FindCollectionPointsByCriteria(DTO.CollectionPoint criteria)
        {
            var Query = from cp in context.CollectionPoints
                        where cp.CollectionPointID == (criteria.CollectionPointID == 0 ? cp.CollectionPointID : criteria.CollectionPointID)
                        && cp.Name == (criteria.Name == null || criteria.Name == "" ? cp.Name : criteria.Name)
                        && cp.Time == (criteria.Time == null || criteria.Time == "" ? cp.Time : criteria.Time)
                        select cp;
            return Query.ToList<CollectionPoint>();
        }

        public List<CollectionPoint> GetAllCollectionPoints()
        {
            return (from cp in context.CollectionPoints
                    select cp).ToList<CollectionPoint>();
        }

        public CollectionPoint GetCollectionPointByID(int CollectionPointID)
        {
            return (from u in context.CollectionPoints
                    where u.CollectionPointID == CollectionPointID
                    select u).First<CollectionPoint>();
        }

        public CollectionPoint CreateCollectionPoint(DAL.CollectionPoint collectionPoint)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    context.CollectionPoints.AddObject(collectionPoint);
                    context.SaveChanges();
                    ts.Complete();
                    return collectionPoint;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CollectionPoint UpdateCollectionPoint(DAL.CollectionPoint collectionPoint)
        {
            try
            {
                // First will throw excpetion if no user is found
                CollectionPoint tempCollectionPoint = (from c in context.CollectionPoints
                                                       where c.CollectionPointID == collectionPoint.CollectionPointID
                                 select c).First<CollectionPoint>();

                tempCollectionPoint.Name = collectionPoint.Name;
                tempCollectionPoint.Time = collectionPoint.Time;

                using (TransactionScope ts = new TransactionScope())
                {
                    context.ObjectStateManager.ChangeObjectState(tempCollectionPoint, EntityState.Modified);
                    context.SaveChanges();
                    ts.Complete();
                    return tempCollectionPoint;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteCollectionPoint(CollectionPoint collectionPoint)
        {
            try
            {
                CollectionPoint persistedCollectionPoint = (from c in context.CollectionPoints
                                      where c.CollectionPointID == collectionPoint.CollectionPointID
                                      select c).FirstOrDefault();

                using (TransactionScope ts = new TransactionScope())
                {
                    context.CollectionPoints.DeleteObject(persistedCollectionPoint);
                    context.SaveChanges();
                    ts.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region BlacklistLogs
        public List<BlacklistLog> FindBlacklistLogsByCriteria(DTO.BlackListLogSearchDTO criteria)
        {
            var Query = from cp in context.BlacklistLogs
                        where cp.BlacklistLogID == (criteria.BlackListLogID == 0 ? cp.BlacklistLogID : criteria.BlackListLogID)
                        select cp;
            return Query.ToList<BlacklistLog>();
        }

        public List<BlacklistLog> GetAllBlacklistLogs()
        {
            return (from cp in context.BlacklistLogs
                    select cp).ToList<BlacklistLog>();
        }

        public BlacklistLog GetBlacklistLogByID(int BlacklistLogID)
        {
            return (from u in context.BlacklistLogs
                    where u.BlacklistLogID == BlacklistLogID
                    select u).First<BlacklistLog>();
        }

        public BlacklistLog CreateBlacklistLog(DAL.BlacklistLog collectionPoint)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    context.BlacklistLogs.AddObject(collectionPoint);
                    context.SaveChanges();
                    ts.Complete();
                    return collectionPoint;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BlacklistLog UpdateBlacklistLog(DAL.BlacklistLog collectionPoint)
        {
            try
            {
                // First will throw excpetion if no user is found
                BlacklistLog tempBlacklistLog = (from c in context.BlacklistLogs
                                                       where c.BlacklistLogID == collectionPoint.BlacklistLogID
                                                       select c).First<BlacklistLog>();

                using (TransactionScope ts = new TransactionScope())
                {
                    context.ObjectStateManager.ChangeObjectState(tempBlacklistLog, EntityState.Modified);
                    context.SaveChanges();
                    ts.Complete();
                    return tempBlacklistLog;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteBlacklistLog(BlacklistLog collectionPoint)
        {
            try
            {
                BlacklistLog persistedBlacklistLog = (from c in context.BlacklistLogs
                                                            where c.BlacklistLogID == collectionPoint.BlacklistLogID
                                                            select c).FirstOrDefault();

                using (TransactionScope ts = new TransactionScope())
                {
                    context.BlacklistLogs.DeleteObject(persistedBlacklistLog);
                    context.SaveChanges();
                    ts.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion 
    }
}
