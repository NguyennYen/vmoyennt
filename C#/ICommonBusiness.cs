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
using CommonDAL;
using Framework.Data;
using Framework.Data.SystemConfig;

// NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
namespace CommonBusiness.ServiceContracts
{
    [ServiceContract()]
    public interface ICommonBusiness
    {
        [OperationContract()]
        bool CheckOtherListExistInDatabase(List<decimal> lstID, decimal typeID);

        [OperationContract()]
        DataTable GetATOrgPeriod(decimal periodID);


        [OperationContract()]
        bool IsUsernameExist(string Username);

        [OperationContract()]
        UserDTO GetUserWithPermision(string Username);

        [OperationContract()]
        List<PermissionDTO> GetUserPermissions(string Username);

        [OperationContract()]
        bool CheckUserAdmin(string Username);

        [OperationContract()]
        bool ChangeUserPassword(string Username, string _oldpass, string _newpass, UserLog log);

        [OperationContract()]
        string GetPassword(string Username);

        [OperationContract()]
        bool UpdateUserStatus(string Username, string _ACTFLG, UserLog log);

        [OperationContract()]
        List<OrganizationDTO> GetOrganizationAll();
        /// <summary>
        ///         ''' Lấy danh sách sơ đồ tổ chức hàng dọc cho TreeView phân quyền
        ///         ''' </summary>
        ///         ''' <param name="_username">tên tài khoản</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<OrganizationDTO> GetOrganizationLocationTreeView(string _username);
        /// <summary>
        ///         ''' Lấy thông tin sơ đồ tổ chức Location
        ///         ''' </summary>
        ///         ''' <param name="_orgId">Org ID</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        OrganizationDTO GetOrganizationLocationInfo(decimal _orgId);
        /// <summary>
        ///         ''' Lấy thông tin cấu trúc sơ đồ tổ chức
        ///         ''' </summary>
        ///         ''' <param name="_orgId">Org ID</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<OrganizationStructureDTO> GetOrganizationStructureInfo(decimal _orgId);

        /// <summary>
        ///         ''' Lay danh sach tai khoan
        ///         ''' </summary>
        ///         ''' <returns>danh sach tai khoan</returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<UserDTO> GetUser(UserDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc");
        /// <summary>
        ///         ''' Them tai khoan
        ///         ''' </summary>
        ///         ''' <param name="_user">doi tuong chua cac thong tin can them</param>
        ///         ''' <returns>true?false</returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool InsertUser(UserDTO _user, UserLog log);
        /// <summary>
        ///         ''' Kiểm tra dữ liệu tài khoản
        ///         ''' </summary>
        ///         ''' <param name="_validate">Dữ liệu cần kiểm tra</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool ValidateUser(UserDTO _validate);
        /// <summary>
        ///         ''' Sua tai khoan
        ///         ''' </summary>
        ///         ''' <param name="_user">doi tuong chua cac thong tin can sua</param>
        ///         ''' <returns>true?false</returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool ModifyUser(UserDTO _user, UserLog log);
        /// <summary>
        ///         ''' Xoa tai khoan
        ///         ''' </summary>
        ///         ''' <param name="_lstUserID">doi tuong chua thong tin can xoa</param>
        ///         ''' <returns>true?false</returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool DeleteUser(List<decimal> _lstUserID, ref string _error, UserLog log);
        /// <summary>
        ///         ''' Lock or Unlock tài khoản
        ///         ''' </summary>
        ///         ''' <param name="_lstUserID"></param>
        ///         ''' <param name="_status"></param>
        ///         ''' <param name="log"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool UpdateUserListStatus(List<decimal> _lstUserID, string _status, UserLog log);
        /// <summary>
        ///         ''' Đồng bộ hóa
        ///         ''' </summary>
        ///         ''' <param name="_newUser"></param>
        ///         ''' <param name="_modifyUser"></param>
        ///         ''' <param name="_deleteUser"></param>
        ///         ''' <param name="log"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool SyncUserList(ref string _newUser, ref string _modifyUser, ref string _deleteUser, UserLog log);
        [OperationContract()]
        bool ResetUserPassword(List<decimal> _userid, int _minLength, bool _hasLowerChar, bool _hasUpperChar, bool _hasNumbericChar, bool _hasSpecialChar);

        /// <summary>
        ///         ''' Gửi Email thông báo mật khẩu
        ///         ''' </summary>
        ///         ''' <param name="_userid"></param>
        ///         ''' <param name="_subject"></param>
        ///         ''' <param name="_content"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool SendMailConfirmUserPassword(List<decimal> _userid, string _subject, string _content);

        /// <summary>
        ///         ''' Lấy danh sách user cần send mail từ Group
        ///         ''' </summary>
        ///         ''' <param name="_groupid"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<UserDTO> GetUserNeedSendMail(decimal _groupid);

        /// <summary>
        ///         ''' Lấy danh sách nhóm tài khoản cho Combo
        ///         ''' </summary>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<GroupDTO> GetGroupListToComboListBox();
        /// <summary>
        ///         ''' Lấy danh sách nhóm tài khoản
        ///         ''' </summary>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<GroupDTO> GetGroupList(GroupDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "MODIFIED_DATE desc");
        /// <summary>
        ///         ''' Kiểm tra dữ liệu nhóm tài khoản
        ///         ''' </summary>
        ///         ''' <param name="_validate"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool ValidateGroupList(GroupDTO _validate);
        /// <summary>
        ///         ''' Thêm nhóm tài khoản
        ///         ''' </summary>
        ///         ''' <param name="lst"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool InsertGroup(GroupDTO lst, UserLog log);
        /// <summary>
        ///         ''' Sửa nhóm tài khoản
        ///         ''' </summary>
        ///         ''' <param name="lst"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool UpdateGroup(GroupDTO lst, UserLog log);
        /// <summary>
        ///         ''' Xóa nhóm tài khoản
        ///         ''' </summary>
        ///         ''' <param name="GroupID"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool DeleteGroup(List<decimal> GroupID, ref string _error, UserLog log);
        /// <summary>
        ///         ''' Đổi trạng thái của Group
        ///         ''' </summary>
        ///         ''' <param name="_lstID"></param>
        ///         ''' <param name="_ACTFLG"></param>
        ///         ''' <param name="log"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool UpdateGroupStatus(List<decimal> _lstID, string _ACTFLG, UserLog log);

        /// <summary>
        ///         ''' Lấy danh sách chức năng hệ thống
        ///         ''' </summary>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<FunctionDTO> GetFunctionList(FunctionDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "NAME asc");
        /// <summary>
        ///         ''' Kiểm tra dữ liệu chức năng hệ thống
        ///         ''' </summary>
        ///         ''' <param name="_validate"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool ValidateFunctionList(FunctionDTO _validate);
        /// <summary>
        ///         ''' Cập nhập chức năng hệ thống
        ///         ''' </summary>
        ///         ''' <param name="_item"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool UpdateFunctionList(List<FunctionDTO> _item, UserLog log);

        [OperationContract()]
        bool InsertFunctionList(FunctionDTO _item, UserLog log);
        /// <summary>
        ///         ''' Lấy danh sách các Module
        ///         ''' </summary>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<ModuleDTO> GetModuleList();

        [OperationContract()]
        bool ActiveFunctions(List<FunctionDTO> lstFunction, string sActive, UserLog log);


        /// <summary>
        ///         ''' Lấy danh sách các tài khoản trong nhóm
        ///         ''' </summary>
        ///         ''' <param name="GroupID"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<UserDTO> GetUserListInGroup(decimal GroupID, UserDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc");
        /// <summary>
        ///         ''' Lấy danh sách các tài khoản ngoài nhóm
        ///         ''' </summary>
        ///         ''' <param name="GroupID"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<UserDTO> GetUserListOutGroup(decimal GroupID, UserDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc");

        /// <summary>
        ///         ''' Thêm tài khoản vào nhóm
        ///         ''' </summary>
        ///         ''' <param name="_groupID"></param>
        ///         ''' <param name="_lstUserID"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool InsertUserGroup(decimal _groupID, List<decimal> _lstUserID, UserLog log);
        /// <summary>
        ///         ''' Xóa tài khoản khỏi nhóm
        ///         ''' </summary>
        ///         ''' <param name="_groupID"></param>
        ///         ''' <param name="_lstUserID"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool DeleteUserGroup(decimal _groupID, List<decimal> _lstUserID, UserLog log);

        /// <summary>
        ///         ''' Lấy danh sách chức năng trong nhóm
        ///         ''' </summary>
        ///         ''' <param name="GroupID"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<GroupFunctionDTO> GetGroupFunctionPermision(decimal GroupID, GroupFunctionDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "FUNCTION_CODE asc");
        /// <summary>
        ///         ''' Lấy danh sách chức năng ngoài nhóm
        ///         ''' </summary>
        ///         ''' <param name="GroupID"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<FunctionDTO> GetGroupFunctionNotPermision(decimal GroupID, FunctionDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "NAME asc");
        /// <summary>
        ///         ''' Thêm chức năng vào nhóm
        ///         ''' </summary>
        ///         ''' <param name="lst"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool InsertGroupFunction(List<GroupFunctionDTO> lst, UserLog log);

        [OperationContract()]
        bool CopyGroupFunction(decimal groupCopyID, decimal groupID, UserLog log);
        /// <summary>
        ///         ''' Cập nhập phân quyền chức năng
        ///         ''' </summary>
        ///         ''' <param name="lst"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool UpdateGroupFunction(List<GroupFunctionDTO> lst, UserLog log);
        /// <summary>
        ///         ''' Xóa chức năng khỏi nhóm
        ///         ''' </summary>
        ///         ''' <param name="lst"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool DeleteGroupFunction(List<decimal> lst);

        /// <summary>
        ///         ''' Lấy danh sách Report đã phân quyền theo nhóm tài khoản
        ///         ''' </summary>
        ///         ''' <param name="_groupID">ID nhóm tài khoản</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<GroupReportDTO> GetGroupReportList(decimal _groupID);
        /// <summary>
        ///         ''' Lấy danh sách Report đã phân quyền theo nhóm tài khoản có Filter
        ///         ''' </summary>
        ///         ''' <param name="_groupID">ID nhóm tài khoản</param>
        ///         ''' <param name="_filter">bộ lọc</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<GroupReportDTO> GetGroupReportListFilter(decimal _groupID, GroupReportDTO _filter);
        /// <summary>
        ///         ''' Cập nhập danh sách Report
        ///         ''' </summary>
        ///         ''' <param name="_groupID">ID nhóm tài khoản</param>
        ///         ''' <param name="_lstReport">Danh sách report cần cập nhập</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool UpdateGroupReport(decimal _groupID, List<GroupReportDTO> _lstReport);

        /// <summary>
        ///         ''' Lấy danh sách chức năng trong nhóm
        ///         ''' </summary>
        ///         ''' <param name="UserID"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<UserFunctionDTO> GetUserFunctionPermision(decimal UserID, UserFunctionDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "FUNCTION_CODE asc");
        /// <summary>
        ///         ''' Lấy danh sách chức năng ngoài nhóm
        ///         ''' </summary>
        ///         ''' <param name="UserID"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<FunctionDTO> GetUserFunctionNotPermision(decimal UserID, FunctionDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "NAME asc");
        /// <summary>
        ///         ''' Thêm chức năng vào nhóm
        ///         ''' </summary>
        ///         ''' <param name="lst"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool InsertUserFunction(List<UserFunctionDTO> lst, UserLog log);

        [OperationContract()]
        bool CopyUserFunction(decimal UserCopyID, decimal UserID, UserLog log);
        /// <summary>
        ///         ''' Cập nhập phân quyền chức năng
        ///         ''' </summary>
        ///         ''' <param name="lst"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool UpdateUserFunction(List<UserFunctionDTO> lst, UserLog log);
        /// <summary>
        ///         ''' Xóa chức năng khỏi nhóm
        ///         ''' </summary>
        ///         ''' <param name="lst"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool DeleteUserFunction(List<decimal> lst);

        [OperationContract()]
        List<decimal> GetUserOrganization(decimal UserID);

        [OperationContract()]
        void DeleteUserOrganization(decimal _UserId);

        [OperationContract()]
        bool UpdateUserOrganization(List<UserOrgAccessDTO> OrgIDs);

        /// <summary>
        ///         ''' Lấy danh sách Report đã phân quyền theo nhóm tài khoản
        ///         ''' </summary>
        ///         ''' <param name="_UserID">ID nhóm tài khoản</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<UserReportDTO> GetUserReportList(decimal _UserID);
        /// <summary>
        ///         ''' Lấy danh sách Report đã phân quyền theo nhóm tài khoản có Filter
        ///         ''' </summary>
        ///         ''' <param name="_UserID">ID nhóm tài khoản</param>
        ///         ''' <param name="_filter">bộ lọc</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<UserReportDTO> GetUserReportListFilter(decimal _UserID, UserReportDTO _filter);
        /// <summary>
        ///         ''' Cập nhập danh sách Report
        ///         ''' </summary>
        ///         ''' <param name="_UserID">ID nhóm tài khoản</param>
        ///         ''' <param name="_lstReport">Danh sách report cần cập nhập</param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool UpdateUserReport(decimal _UserID, List<UserReportDTO> _lstReport);

        [OperationContract()]
        List<EmployeePopupFindListDTO> GetEmployeeToPopupFind(EmployeePopupFindListDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "EMPLOYEE_CODE asc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */, ParamDTO _param = null/* TODO Change to default(_) if this is not a reference type */);
        [OperationContract()]
        List<EmployeePopupFindDTO> GetEmployeeToPopupFind_EmployeeID(List<decimal> _empId);

        [OperationContract()]
        List<StudentPopupFindListDTO> GetStudentToPopupFind(StudentPopupFindListDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "Student_CODE asc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */, ParamDTO _param = null/* TODO Change to default(_) if this is not a reference type */);
        [OperationContract()]
        List<StudentPopupFindDTO> GetStudentToPopupFind_StudentID(List<decimal> _empId);

        /// <summary>
        ///         ''' Lay danh sach Title
        ///         ''' </summary>
        ///         ''' <returns>danh sach Title</returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<TitleDTO> GetTitle(TitleDTO Filter, int PageIndex, int PageSize, ref int Total, string Sorts = "NAME_VN asc");
        /// <summary>
        ///         ''' Lấy thông tin Title từ Id
        ///         ''' </summary>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<TitleDTO> GetTitleFromId(List<decimal> _lstIds);

        /// <summary>
        ///         ''' Lay danh sach Kitchen
        ///         ''' </summary>
        ///         ''' <returns>danh sach Kitchen</returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<AT_KITCHEN_DTO> GetKitchen(AT_KITCHEN_DTO Filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE asc");
        /// <summary>
        ///         ''' Lấy thông tin Kitchen từ Id
        ///         ''' </summary>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<AT_KITCHEN_DTO> GetKitchenFromId(List<decimal> _lstIds);

        [OperationContract()]
        List<AccessLog> GetAccessLog(AccessLogFilter filter, int PageIndex, int PageSize, ref int Total, string Sorts = "LoginDate desc");

        [OperationContract()]
        bool InsertAccessLog(AccessLog _accesslog);

        [OperationContract()]
        List<ActionLog> GetActionLog(ActionLogFilter filter, int PageIndex, int PageSize, ref int Total, string Sorts = "ActionDate desc");

        [OperationContract()]
        List<ActionLog> GetActionLogByObjectId(decimal ObjectId);

        [OperationContract()]
        List<AuditLogDtl> GetActionLogByID(decimal gID);

        [OperationContract()]
        int DeleteActionLogs(List<decimal> lstDeleteIds);

        [OperationContract()]
        bool GetComboList(ref ComboBoxDataDTO _combolistDTO);

        /// <summary>
        ///         ''' Lấy danh sách cấu hình hệ thống
        ///         ''' </summary>
        ///         ''' <returns>Danh sách dạng Dictionary</returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        Dictionary<string, string> GetConfig(ModuleID eModule);

        /// <summary>
        ///         ''' Cập nhập giá trị cấu hình hệ thống
        ///         ''' </summary>
        ///         ''' <param name="_lstConfig"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool UpdateConfig(Dictionary<string, string> _lstConfig, ModuleID eModule);


        /// <summary>
        ///         ''' Gọi hàm gửi mail queue trong DB
        ///         ''' </summary>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool SendMail();
        /// <summary>
        ///         ''' Thêm queue mail
        ///         ''' </summary>
        ///         ''' <param name="_from"></param>
        ///         ''' <param name="_to"></param>
        ///         ''' <param name="_subject"></param>
        ///         ''' <param name="_content"></param>
        ///         ''' <param name="_cc"></param>
        ///         ''' <param name="_bcc"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        void InsertMail(string _from, string _to, string _subject, string _content, string _cc = "", string _bcc = "", string _viewName = "");

        [OperationContract()]
        List<OtherListDTO> GetOtherListByTypeToCombo(string sType);
        /// <summary>
        ///         ''' Lay danh sach OtherList
        ///         ''' </summary>
        ///         ''' <returns>danh sach OtherList</returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<OtherListDTO> GetOtherList(string sACT);
        /// <summary>
        ///         ''' Lay danh sach OtherListByType
        ///         ''' </summary>
        ///         ''' <returns>danh sach OtherList</returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<OtherListDTO> GetOtherListByType(decimal gID, OtherListDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc");
        /// <summary>
        ///         ''' Them OtherList
        ///         ''' </summary>
        ///         ''' <param name="objOtherList">doi tuong chua cac thong tin can them</param>
        ///         ''' <returns>true?false</returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool InsertOtherList(OtherListDTO objOtherList, UserLog log);
        /// <summary>
        ///         ''' Sua OtherList
        ///         ''' </summary>
        ///         ''' <param name="objOtherList">doi tuong chua cac thong tin can sua</param>
        ///         ''' <returns>true?false</returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool ModifyOtherList(OtherListDTO objOtherList, UserLog log);

        [OperationContract()]
        bool ValidateOtherList(OtherListDTO _validate);


        /// <summary>
        ///         ''' Xoa OtherList
        ///         ''' </summary>
        ///         ''' <param name="lstOtherList">doi tuong chua thong tin can xoa</param>
        ///         ''' <returns>true?false</returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool ActiveOtherList(List<decimal> lstOtherList, string sActive, UserLog log);

        [OperationContract()]
        bool DeleteOtherList(List<OtherListDTO> lstOtherList);

        /// <summary>
        ///         ''' Lay danh sach OtherListGroup theo ma phan he
        ///         ''' </summary>
        ///         ''' <returns>danh sach OtherListGroup</returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<OtherListGroupDTO> GetOtherListGroupBySystem(string systemName);

        /// <summary>
        ///         ''' Lay danh sach OtherListGroup
        ///         ''' </summary>
        ///         ''' <returns>danh sach OtherListGroup</returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<OtherListGroupDTO> GetOtherListGroup(string sACT);
        /// <summary>
        ///         ''' Them OtherListGroup
        ///         ''' </summary>
        ///         ''' <param name="objOtherListGroup">doi tuong chua cac thong tin can them</param>
        ///         ''' <returns>true?false</returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool InsertOtherListGroup(OtherListGroupDTO objOtherListGroup, UserLog log);
        /// <summary>
        ///         ''' Sua OtherListGroup
        ///         ''' </summary>
        ///         ''' <param name="objOtherListGroup">doi tuong chua cac thong tin can sua</param>
        ///         ''' <returns>true?false</returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool ModifyOtherListGroup(OtherListGroupDTO objOtherListGroup, UserLog log);
        /// <summary>
        ///         ''' Xoa OtherListGroup
        ///         ''' </summary>
        ///         ''' <param name="lstOtherListGroup">doi tuong chua thong tin can xoa</param>
        ///         ''' <returns>true?false</returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool ActiveOtherListGroup(OtherListGroupDTO[] lstOtherListGroup, string sActive, UserLog log);

        [OperationContract()]
        bool DeleteOtherListGroup(List<OtherListGroupDTO> lstOtherListGroup);

        /// <summary>
        ///         ''' Lay danh sach OtherListType theo phan he
        ///         ''' </summary>
        ///         ''' <returns>danh sach OtherListType</returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<OtherListTypeDTO> GetOtherListTypeSystem(string systemName);
        /// <summary>
        ///         ''' Lay danh sach OtherListType
        ///         ''' </summary>
        ///         ''' <returns>danh sach OtherListType</returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        List<OtherListTypeDTO> GetOtherListType(string sACT);
        /// <summary>
        ///         ''' Them OtherListType
        ///         ''' </summary>
        ///         ''' <param name="objOtherListType">doi tuong chua cac thong tin can them</param>
        ///         ''' <returns>true?false</returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool InsertOtherListType(OtherListTypeDTO objOtherListType, UserLog log);
        /// <summary>
        ///         ''' Sua OtherListType
        ///         ''' </summary>
        ///         ''' <param name="objOtherListType">doi tuong chua cac thong tin can sua</param>
        ///         ''' <returns>true?false</returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool ModifyOtherListType(OtherListTypeDTO objOtherListType, UserLog log);
        /// <summary>
        ///         ''' Xoa OtherListType
        ///         ''' </summary>
        ///         ''' <param name="lstOtherListType">doi tuong chua thong tin can xoa</param>
        ///         ''' <returns>true?false</returns>
        ///         ''' <remarks></remarks>
        [OperationContract()]
        bool ActiveOtherListType(OtherListTypeDTO[] lstOtherListType, string sActive, UserLog log);

        [OperationContract()]
        bool DeleteOtherListType(List<OtherListTypeDTO> lstOtherListType);


        [OperationContract()]
        Dictionary<int, string> GetReminderConfig(string username);
        [OperationContract()]
        bool SetReminderConfig(string username, int type, string value);




        [OperationContract()]
        List<ApproveProcessDTO> GetApproveProcessList();
        [OperationContract()]
        ApproveProcessDTO GetApproveProcess(decimal processId);
        [OperationContract()]
        bool InsertApproveProcess(ApproveProcessDTO item, UserLog log);
        [OperationContract()]
        bool UpdateApproveProcess(ApproveProcessDTO item, UserLog log);
        [OperationContract()]
        bool UpdateApproveProcessStatus(List<decimal> itemUpdates, string status);

        [OperationContract()]
        List<ApproveSetupDTO> GetApproveSetupByEmployee(decimal employeeId, string Sorts = "CREATED_DATE desc");
        [OperationContract()]
        List<ApproveSetupDTO> GetApproveSetupByOrg(decimal orgId, string Sorts = "CREATED_DATE desc");
        [OperationContract()]
        ApproveSetupDTO GetApproveSetup(decimal id);
        [OperationContract()]
        bool InsertApproveSetup(ApproveSetupDTO item, UserLog log);
        [OperationContract()]
        bool UpdateApproveSetup(ApproveSetupDTO item, UserLog log);
        [OperationContract()]
        bool DeleteApproveSetup(List<decimal> itemDeletes);
        [OperationContract()]
        bool IsExistSetupByDate(ApproveSetupDTO itemCheck, decimal? idExclude = default(Decimal?));

        [OperationContract()]
        bool InsertApproveTemplate(ApproveTemplateDTO item, UserLog log);
        [OperationContract()]
        bool UpdateApproveTemplate(ApproveTemplateDTO item, UserLog log);
        [OperationContract()]
        ApproveTemplateDTO GetApproveTemplate(decimal id);
        [OperationContract()]
        List<ApproveTemplateDTO> GetApproveTemplateList();
        [OperationContract()]
        bool DeleteApproveTemplate(List<decimal> itemDeletes);
        [OperationContract()]
        bool IsApproveTemplateUsed(decimal templateID);


        [OperationContract()]
        bool InsertApproveTemplateDetail(ApproveTemplateDetailDTO item, UserLog log);
        [OperationContract()]
        bool UpdateApproveTemplateDetail(ApproveTemplateDetailDTO item, UserLog log);
        [OperationContract()]
        ApproveTemplateDetailDTO GetApproveTemplateDetail(decimal id);
        [OperationContract()]
        List<ApproveTemplateDetailDTO> GetApproveTemplateDetailList(decimal templateId);
        [OperationContract()]
        bool DeleteApproveTemplateDetail(List<decimal> itemDeletes);
        [OperationContract()]
        bool CheckLevelInsert(decimal level, decimal idExclude, decimal idTemplate);

        [OperationContract()]
        bool InsertApproveSetupExt(ApproveSetupExtDTO item, UserLog log);
        [OperationContract()]
        bool UpdateApproveSetupExt(ApproveSetupExtDTO item, UserLog log);
        [OperationContract()]
        ApproveSetupExtDTO GetApproveSetupExt(decimal id);
        [OperationContract()]
        List<ApproveSetupExtDTO> GetApproveSetupExtList(decimal employeeId, string Sorts = "CREATED_DATE desc");
        [OperationContract()]
        bool DeleteApproveSetupExt(List<decimal> itemDeletes);
        [OperationContract()]
        bool IsExistSetupExtByDate(ApproveSetupExtDTO itemCheck, decimal? idExclude = default(Decimal?));


        [OperationContract()]
        List<ApproveUserDTO> GetApproveUsers(decimal employeeId, string processCode);


        [OperationContract()]
        List<EmployeeDTO> GetListEmployee(List<decimal> _orgIds);



        [OperationContract()]
        List<LdapDTO> GetLdap(LdapDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "LDAP_NAME");

        [OperationContract()]
        bool InsertLdap(LdapDTO objLdap, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ModifyLdap(LdapDTO objLdap, UserLog log, ref decimal gID);

        [OperationContract()]
        bool DeleteLdap(List<decimal> lstID);
    }
}
