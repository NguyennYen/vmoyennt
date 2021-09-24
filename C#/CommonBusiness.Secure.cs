using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using CommonBusiness.ServiceContracts;
using CommonDAL;
using Framework.Data;
using Framework.Data.SystemConfig;

namespace CommonBusiness.ServiceImplementations
{
    public partial class CommonBusiness : ICommonBusiness
    {
        public List<CommonDAL.UserDTO> GetUser(UserDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    var lst = rep.GetUser(_filter, PageIndex, PageSize, Total, Sorts);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateUser(CommonDAL.UserDTO _validate)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.ValidateUser(_validate);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertUser(UserDTO _user, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.InsertUser(_user, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyUser(UserDTO _user, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.ModifyUser(_user, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteUser(List<decimal> _lstUserID, ref string _error, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.DeleteUser(_lstUserID, _error, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool SyncUserList(ref string _newUser, ref string _modifyUser, ref string _deleteUser, Framework.Data.UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.SyncUserList(_newUser, _modifyUser, _deleteUser, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool UpdateUserListStatus(List<System.Decimal> _lstUserID, string _status, Framework.Data.UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.UpdateUserListStatus(_lstUserID, _status, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ResetUserPassword(List<System.Decimal> _userid, int _minLength, bool _hasLowerChar, bool _hasUpperChar, bool _hasNumbericChar, bool _hasSpecialChar)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.ResetUserPassword(_userid, _minLength, _hasLowerChar, _hasUpperChar, _hasNumbericChar, _hasSpecialChar);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool SendMailConfirmUserPassword(List<System.Decimal> _userid, string _subject, string _content)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.SendMailConfirmUserPassword(_userid, _subject, _content);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<CommonDAL.UserDTO> GetUserNeedSendMail(System.Decimal _groupid)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetUserNeedSendMail(_groupid);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<CommonDAL.GroupDTO> GetGroupListToComboListBox()
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    var lst = rep.GetGroupListToComboListBox();
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<CommonDAL.GroupDTO> GetGroupList(GroupDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "MODIFIED_DATE desc")
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    var lst = rep.GetGroupList(_filter, PageIndex, PageSize, Total, Sorts);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateGroupList(CommonDAL.GroupDTO _validate)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.ValidateGroupList(_validate);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertGroup(CommonDAL.GroupDTO lst, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.InsertGroup(lst, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool UpdateGroup(CommonDAL.GroupDTO lst, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.UpdateGroup(lst, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool DeleteGroup(List<decimal> GroupID, ref string _error, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.DeleteGroup(GroupID, _error, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool UpdateGroupStatus(List<System.Decimal> _lstID, string _ACTFLG, Framework.Data.UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.UpdateGroupStatus(_lstID, _ACTFLG, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<CommonDAL.FunctionDTO> GetFunctionList(FunctionDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "NAME asc")
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetFunctionList(_filter, PageIndex, PageSize, Total, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateFunctionList(CommonDAL.FunctionDTO _validate)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.ValidateFunctionList(_validate);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool UpdateFunctionList(List<CommonDAL.FunctionDTO> _item, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.UpdateFunctionList(_item, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertFunctionList(FunctionDTO _item, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.InsertFunctionList(_item, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<CommonDAL.ModuleDTO> GetModuleList()
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetModuleList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ActiveFunctions(List<FunctionDTO> lstFunction, string sActive, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.ActiveFunctions(lstFunction, sActive, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<CommonDAL.UserDTO> GetUserListInGroup(System.Decimal GroupID, UserDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetUserListInGroup(GroupID, _filter, PageIndex, PageSize, Total, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<CommonDAL.UserDTO> GetUserListOutGroup(System.Decimal GroupID, UserDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetUserListOutGroup(GroupID, _filter, PageIndex, PageSize, Total, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertUserGroup(decimal _groupID, List<decimal> _lstUserID, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.InsertUserGroup(_groupID, _lstUserID, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteUserGroup(decimal _groupID, List<decimal> _lstUserID, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.DeleteUserGroup(_groupID, _lstUserID, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<CommonDAL.GroupFunctionDTO> GetGroupFunctionPermision(System.Decimal GroupID, GroupFunctionDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "FUNCTION_CODE asc")
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetGroupFunctionPermision(GroupID, _filter, PageIndex, PageSize, Total, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<FunctionDTO> GetGroupFunctionNotPermision(decimal GroupID, FunctionDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "NAME asc")
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetGroupFunctionNotPermision(GroupID, _filter, PageIndex, PageSize, Total, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool DeleteGroupFunction(List<decimal> lst)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.DeleteGroupFunction(lst);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertGroupFunction(List<CommonDAL.GroupFunctionDTO> lst, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.InsertGroupFunction(lst, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public bool CopyGroupFunction(decimal groupCopyID, decimal groupID, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.CopyGroupFunction(groupCopyID, groupID, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool UpdateGroupFunction(List<CommonDAL.GroupFunctionDTO> lst, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.UpdateGroupFunction(lst, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<CommonDAL.GroupReportDTO> GetGroupReportList(System.Decimal _groupID)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetGroupReportList(_groupID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<CommonDAL.GroupReportDTO> GetGroupReportListFilter(System.Decimal _groupID, CommonDAL.GroupReportDTO _filter)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetGroupReportListFilter(_groupID, _filter);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool UpdateGroupReport(System.Decimal _groupID, List<CommonDAL.GroupReportDTO> _lstReport)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.UpdateGroupReport(_groupID, _lstReport);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<CommonDAL.UserFunctionDTO> GetUserFunctionPermision(System.Decimal UserID, UserFunctionDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "FUNCTION_CODE asc")
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetUserFunctionPermision(UserID, _filter, PageIndex, PageSize, Total, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<FunctionDTO> GetUserFunctionNotPermision(decimal UserID, FunctionDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "NAME asc")
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetUserFunctionNotPermision(UserID, _filter, PageIndex, PageSize, Total, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool DeleteUserFunction(List<decimal> lst)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.DeleteUserFunction(lst);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertUserFunction(List<CommonDAL.UserFunctionDTO> lst, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.InsertUserFunction(lst, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public bool CopyUserFunction(decimal UserCopyID, decimal UserID, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.CopyUserFunction(UserCopyID, UserID, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool UpdateUserFunction(List<CommonDAL.UserFunctionDTO> lst, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.UpdateUserFunction(lst, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<decimal> GetUserOrganization(System.Decimal UserID)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetUserOrganization(UserID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public object DeleteUserOrganization(System.Decimal _UserId)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.DeleteUserOrganization(_UserId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool UpdateUserOrganization(List<CommonDAL.UserOrgAccessDTO> OrgIDs)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.UpdateUserOrganization(OrgIDs);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<CommonDAL.UserReportDTO> GetUserReportList(System.Decimal _UserID)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetUserReportList(_UserID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<CommonDAL.UserReportDTO> GetUserReportListFilter(System.Decimal _UserID, CommonDAL.UserReportDTO _filter)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetUserReportListFilter(_UserID, _filter);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool UpdateUserReport(System.Decimal _UserID, List<CommonDAL.UserReportDTO> _lstReport)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.UpdateUserReport(_UserID, _lstReport);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<EmployeePopupFindListDTO> GetEmployeeToPopupFind(EmployeePopupFindListDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "EMPLOYEE_CODE asc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */, ParamDTO _param = null/* TODO Change to default(_) if this is not a reference type */)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetEmployeeToPopupFind(_filter, PageIndex, PageSize, Total, Sorts, log, _param);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<CommonDAL.EmployeePopupFindDTO> GetEmployeeToPopupFind_EmployeeID(List<System.Decimal> _empId)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetEmployeeToPopupFind_EmployeeID(_empId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<StudentPopupFindListDTO> GetStudentToPopupFind(StudentPopupFindListDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "Student_CODE asc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */, ParamDTO _param = null/* TODO Change to default(_) if this is not a reference type */)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetStudentToPopupFind(_filter, PageIndex, PageSize, Total, Sorts, log, _param);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<CommonDAL.StudentPopupFindDTO> GetStudentToPopupFind_StudentID(List<System.Decimal> _empId)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetStudentToPopupFind_StudentID(_empId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<TitleDTO> GetTitle(TitleDTO Filter, int PageIndex, int PageSize, ref int Total, string Sorts = "NAME_VN asc")
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    var lst = rep.GetTitle(Filter, PageIndex, PageSize, Total, Sorts);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<TitleDTO> GetTitleFromId(List<decimal> _lstIds)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetTitleFromId(_lstIds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<AT_KITCHEN_DTO> GetKitchen(AT_KITCHEN_DTO Filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE asc")
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    var lst = rep.GetKitchen(Filter, PageIndex, PageSize, Total, Sorts);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<AT_KITCHEN_DTO> GetKitchenFromId(List<decimal> _lstIds)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetKitchenFromId(_lstIds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AccessLog> GetAccessLog(AccessLogFilter filter, int PageIndex, int PageSize, ref int Total, string Sorts = "LoginDate desc")
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetAccessLog(filter, PageIndex, PageSize, Total, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertAccessLog(AccessLog _accesslog)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.InsertAccessLog(_accesslog);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<ActionLog> GetActionLog(ActionLogFilter filter, int PageIndex, int PageSize, ref int Total, string Sorts = "ActionDate desc")
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetActionLog(filter, PageIndex, PageSize, Total, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<ActionLog> GetActionLogByObjectId(decimal ObjectId)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetActionLog(ObjectId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<AuditLogDtl> GetActionLogByID(decimal gID)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetActionLogByID(gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public int DeleteActionLogs(List<decimal> lstDeleteIds)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.DeleteActionLogs(lstDeleteIds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public Dictionary<string, string> GetConfig(ModuleID eModule)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetConfig(eModule);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool UpdateConfig(Dictionary<string, string> _lstConfig, ModuleID eModule)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.UpdateConfig(_lstConfig, eModule);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
