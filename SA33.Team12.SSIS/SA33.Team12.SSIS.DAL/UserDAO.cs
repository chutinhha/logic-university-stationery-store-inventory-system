/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Transactions;
using System.Data.Objects;

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
                    orderby u.Department.Name, u.Role
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
                    select u).FirstOrDefault<User>();
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
                User persistedUser = (from u in context.Users
                                      where u.UserID == user.UserID
                                      select u).First<User>();
                Department department = (from d in context.Departments
                                         where d.DepartmentID == user.DepartmentID
                                         select d).FirstOrDefault<Department>();


                persistedUser.Department = department;

                persistedUser.UserName = user.UserName;
                persistedUser.Password = user.Password;
                persistedUser.MembershipProviderKey = user.MembershipProviderKey;
                persistedUser.FirstName = user.FirstName;
                persistedUser.LastName = user.LastName;
                persistedUser.Email = user.Email;
                persistedUser.Password = user.Password;
                persistedUser.Role = user.Role;
                persistedUser.IsEnabled = (user.IsEnabled ? true : false);

                context.ObjectStateManager.ChangeObjectState(persistedUser, EntityState.Modified);
                context.SaveChanges();
                return persistedUser;
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
                && d.IsBlackListed == (criteria.IsBlackListed == null ? d.IsBlackListed : criteria.IsBlackListed)
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
                CollectionPoint point = (from cp in context.CollectionPoints
                                      where cp.CollectionPointID == department.CollectionPointID
                                      select cp).First();
                using (TransactionScope ts = new TransactionScope())
                {
                    persistedDepartment.CollectionPoint = point;
                    persistedDepartment.Code = department.Code;
                    persistedDepartment.Name = department.Name;
                    persistedDepartment.IsBlackListed = department.IsBlackListed;
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
        public List<CollectionPoint> FindCollectionPointsByCriteria(DTO.CollectionPointSearchDTO criteria)
        {
            var Query = from cp in context.CollectionPoints
                        where cp.CollectionPointID == (criteria.CollectionPointID == 0
                            ? cp.CollectionPointID : criteria.CollectionPointID)
                        && cp.Name == (criteria.Name == null || criteria.Name == ""
                            ? cp.Name : criteria.Name)
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
            return (from cp in context.CollectionPoints
                    where cp.CollectionPointID == CollectionPointID
                    select cp).FirstOrDefault<CollectionPoint>();
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
                CollectionPoint tempCollectionPoint
                        = (from c in context.CollectionPoints
                           where c.CollectionPointID == collectionPoint.CollectionPointID
                           select c).First<CollectionPoint>();

                tempCollectionPoint.Name = collectionPoint.Name;

                using (TransactionScope ts = new TransactionScope())
                {
                    context.ObjectStateManager.ChangeObjectState(tempCollectionPoint,
                        EntityState.Modified);
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
                CollectionPoint persistedCollectionPoint
                    = (from c in context.CollectionPoints
                       where c.CollectionPointID == collectionPoint.CollectionPointID
                       select c).First<CollectionPoint>();

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
            var Query =
                from bll in context.BlacklistLogs
                where bll.BlacklistLogID == (criteria.BlackListLogID == 0
                    ? bll.BlacklistLogID : criteria.BlackListLogID)
                && bll.DepartmentID == (criteria.DepartmentID == 0
                    ? bll.DepartmentID : criteria.DepartmentID)
                && (EntityFunctions.DiffDays(bll.DateBlacklisted, (criteria.StartDateBlackListed == null || criteria.StartDateBlackListed == DateTime.MinValue ? bll.DateBlacklisted : criteria.StartDateBlackListed)) <= 0
                    && EntityFunctions.DiffDays(bll.DateBlacklisted, (criteria.EndDateBlackListed == null || criteria.EndDateBlackListed == DateTime.MinValue ? bll.DateBlacklisted : criteria.EndDateBlackListed)) >= 0)
                && (EntityFunctions.DiffDays(bll.DateBlacklisted, (criteria.ExactDateBlackListed == null || criteria.ExactDateBlackListed == DateTime.MinValue ? bll.DateBlacklisted : criteria.ExactDateBlackListed)) == 0)
                select bll;

            return Query.ToList<BlacklistLog>();
        }

        public List<BlacklistLog> GetAllBlacklistLogs()
        {
            return (from bll in context.BlacklistLogs
                    select bll).ToList<BlacklistLog>();
        }

        public BlacklistLog GetBlacklistLogByID(int BlacklistLogID)
        {
            return (from bll in context.BlacklistLogs
                    where bll.BlacklistLogID == BlacklistLogID
                    select bll).First<BlacklistLog>();
        }

        public BlacklistLog CreateBlacklistLog(DAL.BlacklistLog blackListLog)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    context.BlacklistLogs.AddObject(blackListLog);
                    context.SaveChanges();
                    ts.Complete();
                    return blackListLog;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BlacklistLog UpdateBlacklistLog(DAL.BlacklistLog blackListLog)
        {
            try
            {
                // First will throw exblletion if no user is found
                BlacklistLog tempBlacklistLog = (from c in context.BlacklistLogs
                                                 where c.BlacklistLogID == blackListLog.BlacklistLogID
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

        public void DeleteBlacklistLog(BlacklistLog blackListLog)
        {
            try
            {
                BlacklistLog persistedBlacklistLog = (from c in context.BlacklistLogs
                                                      where c.BlacklistLogID == blackListLog.BlacklistLogID
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

        #region ApprovalAudits
        public List<ApprovalAudit> FindApprovalAuditsByCriteria(DTO.ApprovalAuditSearchDTO criteria)
        {
            var Query =
                from aa in context.ApprovalAudits
                where aa.ApprovalAuditID == (criteria.ApprovalAuditID == 0 ? aa.ApprovalAuditID : criteria.ApprovalAuditID)
                && aa.ApprovedBy == (criteria.ApprovedBy == 0 ? aa.ApprovedBy : criteria.ApprovedBy)
                && aa.Reason.Contains((criteria.Reason == null || criteria.Reason == "" ? aa.Reason : criteria.Reason))
                && aa.StatusFrom.Contains((criteria.StatusFrom == null || criteria.StatusFrom == "" ? aa.StatusFrom : criteria.StatusFrom))
                && aa.StatusTo.Contains((criteria.StatusTo == null || criteria.StatusTo == "" ? aa.StatusTo : criteria.StatusTo))
                select aa;

            return Query.ToList<ApprovalAudit>();
        }

        public List<ApprovalAudit> GetAllApprovalAudits()
        {
            return (from aa in context.ApprovalAudits
                    select aa).ToList<ApprovalAudit>();
        }

        public ApprovalAudit GetApprovalAuditByID(int ApprovalAuditID)
        {
            return (from aa in context.ApprovalAudits
                    where aa.ApprovalAuditID == ApprovalAuditID
                    select aa).First<ApprovalAudit>();
        }

        public ApprovalAudit CreateApprovalAudit(DAL.ApprovalAudit ApprovalAudit)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    context.ApprovalAudits.AddObject(ApprovalAudit);
                    context.SaveChanges();
                    ts.Complete();
                    return ApprovalAudit;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ApprovalAudit UpdateApprovalAudit(DAL.ApprovalAudit ApprovalAudit)
        {
            try
            {
                ApprovalAudit tempApprovalAudit = (from aa in context.ApprovalAudits
                                                   where aa.ApprovalAuditID == ApprovalAudit.ApprovalAuditID
                                                   select aa).First<ApprovalAudit>();

                tempApprovalAudit.User = ApprovalAudit.User;
                tempApprovalAudit.Reason = ApprovalAudit.Reason;
                tempApprovalAudit.StatusFrom = ApprovalAudit.StatusFrom;
                tempApprovalAudit.StatusTo = ApprovalAudit.StatusTo;

                using (TransactionScope ts = new TransactionScope())
                {
                    context.ObjectStateManager.ChangeObjectState(tempApprovalAudit, EntityState.Modified);
                    context.SaveChanges();
                    ts.Complete();
                    return tempApprovalAudit;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteApprovalAudit(ApprovalAudit ApprovalAudit)
        {
            try
            {
                ApprovalAudit persistedApprovalAudit = (from aa in context.ApprovalAudits
                                                        where aa.ApprovalAuditID == ApprovalAudit.ApprovalAuditID
                                                        select aa).FirstOrDefault();

                using (TransactionScope ts = new TransactionScope())
                {
                    context.ApprovalAudits.DeleteObject(persistedApprovalAudit);
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
