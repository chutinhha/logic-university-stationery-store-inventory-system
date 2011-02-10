/***
 * Author: Naing Myo Aung (A0076803A)
 * Initial Implementation: 23/Jan/2011
 ***/

using System;
using System.Collections.Generic;
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

        public List<User> GetUserByDepartment(int departmentID)
        {
            if (departmentID > 0)
            {
                return udao.GetUserByDepartment(departmentID);
            }
            return null;
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
        public Department CreateDepartment(Department department)
        {
            return udao.CreateDepartment(department);
        }

        public Department UpdateDepartment(Department department)
        {
            return udao.UpdateDepartment(department);
        }

        public void DeleteDepartment(Department department)
        {
            udao.DeleteDepartment(department);
        }

        public List<Department> GetAllDepartments()
        {
            return udao.GetAllDepartment();
        }

        public Department GetDepartmentByID(int departmentID)
        {
            return udao.GetDepartmentByID(departmentID);
        }

        public List<Department> FindDepartmentByCriteria(DepartmentSearchDTO criteria)
        {
            return udao.FindDepartmentsByCriteria(criteria);
        }

        #endregion

        #region CollectionPoints
        public CollectionPoint CreateCollectionPoint(CollectionPoint collectionPoint)
        {
            return udao.CreateCollectionPoint(collectionPoint);
        }

        public CollectionPoint UpdateCollectionPoint(CollectionPoint collectionPoint)
        {
            return udao.UpdateCollectionPoint(collectionPoint);
        }

        public void DeleteCollectionPoint(CollectionPoint collectionPoint)
        {
            udao.DeleteCollectionPoint(collectionPoint);
        }

        public List<CollectionPoint> GetAllCollectionPoints()
        {
            return udao.GetAllCollectionPoints();
        }

        public CollectionPoint GetCollectionPointByID(int collectionPointID)
        {
            return udao.GetCollectionPointByID(collectionPointID);
        }

        public List<CollectionPoint> FindCollectionPointByCriteria(CollectionPointSearchDTO criteria)
        {
            return udao.FindCollectionPointsByCriteria(criteria);
        }
        #endregion

        #region BlacklistLogs
        public BlacklistLog CreateBlacklistLog(BlacklistLog blackListLog)
        {
            return udao.CreateBlacklistLog(blackListLog);
        }

        public BlacklistLog UpdateBlacklistLog(BlacklistLog blackListLog)
        {
            return udao.UpdateBlacklistLog(blackListLog);
        }

        public void DeleteBlacklistLog(BlacklistLog blackListLog)
        {
            udao.DeleteBlacklistLog(blackListLog);
        }

        public List<BlacklistLog> GetAllBlacklistLogs()
        {
            return udao.GetAllBlacklistLogs();
        }

        public BlacklistLog GetBlacklistLogByID(int blackListLogID)
        {
            return udao.GetBlacklistLogByID(blackListLogID);
        }

        public List<BlacklistLog> FindBlacklistLogByCriteria(BlackListLogSearchDTO criteria)
        {
            return udao.FindBlacklistLogsByCriteria(criteria);
        }
        #endregion

        #region ApprovalAudits
        public ApprovalAudit CreateApprovalAudit(ApprovalAudit approvalAudit)
        {
            return udao.CreateApprovalAudit(approvalAudit);
        }

        public ApprovalAudit UpdateApprovalAudit(ApprovalAudit approvalAudit)
        {
            return udao.UpdateApprovalAudit(approvalAudit);
        }

        public void DeleteApprovalAudit(ApprovalAudit approvalAudit)
        {
            udao.DeleteApprovalAudit(approvalAudit);
        }

        public List<ApprovalAudit> GetAllApprovalAudits()
        {
            return udao.GetAllApprovalAudits();
        }

        public ApprovalAudit GetApprovalAuditByID(int approvalAuditID)
        {
            return udao.GetApprovalAuditByID(approvalAuditID);
        }

        public List<ApprovalAudit> FindApprovalAuditByCriteria(ApprovalAuditSearchDTO criteria)
        {
            return udao.FindApprovalAuditsByCriteria(criteria);
        }
        #endregion
    
    }
}
