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
using Framework.Data;
using System.Data.Objects;
using System.Transactions;
using System.Data.Entity;
using System.Data.Common;
using Framework.Data.System.Linq.Dynamic;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using Framework.Data.SystemConfig;
using System.Configuration;
using Framework.Data.DataAccess;
using Oracle.DataAccess.Client;

public partial class CommonRepository
{
    public List<UserDTO> GetUser(UserDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
    {
        var query = (from p in Context.SE_USER
                     where (from n in p.SE_GROUPS
                            where n.IS_ADMIN == true
                            select n).Count == 0
                     select new UserDTO() { ID = p.ID, EMAIL = p.EMAIL, PASSWORD = p.PASSWORD, TELEPHONE = p.TELEPHONE, USERNAME = p.USERNAME, FULLNAME = p.FULLNAME, IS_APP = p.IS_APP, IS_PORTAL = p.IS_PORTAL, IS_AD = p.IS_AD, ACTFLG = p.ACTFLG, EMPLOYEE_CODE = p.EMPLOYEE_CODE, EFFECT_DATE = p.EFFECT_DATE, EXPIRE_DATE = p.EXPIRE_DATE, CREATED_DATE = p.CREATED_DATE });

        if (_filter.USERNAME != "")
            query = query.Where(p => p.USERNAME.ToUpper.Contains(_filter.USERNAME.ToUpper));
        if (_filter.FULLNAME != "")
            query = query.Where(p => p.FULLNAME.ToUpper.Contains(_filter.FULLNAME.ToUpper));
        if (_filter.EMPLOYEE_CODE != "")
            query = query.Where(p => p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper));
        if (_filter.EMAIL != "")
            query = query.Where(p => p.EMAIL.ToUpper.Contains(_filter.EMAIL.ToUpper));
        if (_filter.IS_APP != null)
        {
            var is_short = _filter.IS_APP ? 1 : 0;
            query = query.Where(p => p.IS_APP == is_short);
        }
        if (_filter.IS_PORTAL != null)
        {
            var is_short = _filter.IS_PORTAL ? 1 : 0;
            query = query.Where(p => p.IS_PORTAL == is_short);
        }
        if (_filter.IS_AD != null)
        {
            var is_short = _filter.IS_AD ? 1 : 0;
            query = query.Where(p => p.IS_AD == is_short);
        }
        if (_filter.ACTFLG != "")
            query = query.Where(p => p.ACTFLG == _filter.ACTFLG);
        query = query.OrderBy(Sorts);
        Total = query.Count;
        query = query.Skip(PageIndex * PageSize).Take(PageSize);

        return query.ToList;
    }

    public bool ValidateUser(UserDTO _validate)
    {
        var query;
        if (_validate.USERNAME != "")
        {
            query = (from p in Context.SE_USER
                     where p.USERNAME.ToUpper == _validate.USERNAME.ToUpper
                     select p).FirstOrDefault;
            return (query == null);
        }
        if (_validate.EMAIL != "")
        {
            query = (from p in Context.SE_USER
                     where p.EMAIL.ToUpper == _validate.EMAIL.ToUpper
                     select p).FirstOrDefault;
            return (query == null);
        }
        if (_validate.EMPLOYEE_CODE != "")
        {
            query = (from p in Context.HU_EMPLOYEE
                     where p.EMPLOYEE_CODE.ToUpper == _validate.EMPLOYEE_CODE.ToUpper
                     select p).FirstOrDefault;
            return (query != null);
        }
        return true;
    }

    public bool InsertUser(UserDTO _user, UserLog log)
    {
        SE_USER objUserData = new SE_USER();
        try
        {
            objUserData.ID = Utilities.GetNextSequence(Context, Context.SE_USER.EntitySet.Name);
            objUserData.PASSWORD = _user.PASSWORD;
            objUserData.EMAIL = _user.EMAIL;
            objUserData.TELEPHONE = _user.TELEPHONE;
            objUserData.USERNAME = _user.USERNAME;
            objUserData.FULLNAME = _user.FULLNAME;
            objUserData.IS_APP = _user.IS_APP;
            objUserData.IS_PORTAL = _user.IS_PORTAL;
            objUserData.IS_AD = _user.IS_AD;
            objUserData.ACTFLG = "A";
            objUserData.IS_CHANGE_PASS = "0";
            objUserData.EMPLOYEE_CODE = _user.EMPLOYEE_CODE;

            if (_user.EFFECT_DATE != null)
                objUserData.EFFECT_DATE = _user.EFFECT_DATE;
            if (_user.EXPIRE_DATE != null)
                objUserData.EXPIRE_DATE = _user.EXPIRE_DATE;


            Context.SE_USER.AddObject(objUserData);
            Context.SaveChanges(log);
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool ModifyUser(UserDTO _user, UserLog log)
    {
        try
        {
            SE_USER objUserData = (from p in Context.SE_USER
                                   where p.ID == _user.ID
                                   select p).FirstOrDefault;
            if (objUserData != null)
            {
                objUserData.PASSWORD = _user.PASSWORD;
                objUserData.EMAIL = _user.EMAIL;
                objUserData.TELEPHONE = _user.TELEPHONE;
                objUserData.USERNAME = _user.USERNAME;
                objUserData.FULLNAME = _user.FULLNAME;
                objUserData.IS_APP = _user.IS_APP;
                objUserData.IS_PORTAL = _user.IS_PORTAL;
                objUserData.IS_AD = _user.IS_AD;
                objUserData.EMPLOYEE_CODE = _user.EMPLOYEE_CODE;
                objUserData.EFFECT_DATE = _user.EFFECT_DATE;
                objUserData.EXPIRE_DATE = _user.EXPIRE_DATE;
                objUserData.MODIFIED_DATE = DateTime.Now;
                objUserData.MODIFIED_BY = log.Username;
                objUserData.MODIFIED_LOG = log.ComputerName;
                Context.SaveChanges(log);
            }
            else
                return false;
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool DeleteUser(List<decimal> _lstUserID, ref string _error, UserLog log)
    {
        try
        {
            List<SE_USER> lstIDUser = (from p in Context.SE_USER
                                       where _lstUserID.Contains(p.ID)
                                       select p).ToList;
            if (lstIDUser != null)
            {
                for (int index = 0; index <= lstIDUser.Count - 1; index++)
                {
                    decimal userid = lstIDUser[index].ID;
                    if (lstIDUser[index].SE_GROUPS.Count > 0)
                    {
                        _error = "MESSAGE_DELETE_DATA_USED";
                        return false;
                    }
                    if ((from p in Context.SE_USER_ORG_ACCESS
                         where userid == p.USER_ID
                         select p).Count)
                    {
                        _error = "MESSAGE_DELETE_DATA_USED";
                        return false;
                    }
                    if ((from p in Context.SE_USER_PERMISSION
                         where userid == p.USER_ID
                         select p).Count)
                    {
                        _error = "MESSAGE_DELETE_DATA_USED";
                        return false;
                    }
                    Context.SE_USER.DeleteObject(lstIDUser[index]);
                }
                Context.SaveChanges(log);
                return true;
            }
            else
                return false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool UpdateUserListStatus(List<decimal> _lstUserID, string _status, UserLog log)
    {
        try
        {
            // Dim lstIDUser As List(Of SE_USER) = (From p In Context.SE_USER Where _lstUserID.Contains(p.ID) Select p).ToList
            // If lstIDUser IsNot Nothing Then
            for (int index = 0; index <= _lstUserID.Count - 1; index++)
            {
                var _user = new SE_USER() { ID = _lstUserID[index] };
                Context.SE_USER.Attach(_user);
                _user.ACTFLG = _status;
            }
            Context.SaveChanges(log);
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public bool SyncUserList(ref string _newUser, ref string _modifyUser, ref string _deleteUser, UserLog log)
    {
        try
        {
            _newUser = "";
            _modifyUser = "";
            _deleteUser = "";
            decimal idTer = CommonCommon.OT_WORK_STATUS.TERMINATE_ID;
            // Kiểm tra nhân viên mới
            List<UserDTO> lstUser = (from p in Context.SE_USER
                                     select new UserDTO() { ID = p.ID, USERNAME = p.USERNAME, FULLNAME = p.FULLNAME, TELEPHONE = p.TELEPHONE, EMAIL = p.EMAIL, EMPLOYEE_CODE = p.EMPLOYEE_CODE, MODULE_ADMIN = p.MODULE_ADMIN, ACTFLG = p.ACTFLG }).ToList;

            List<string> lst = (from p in lstUser
                                select p.USERNAME.ToUpper).ToList();
            ;/* Cannot convert LocalDeclarationStatementSyntax, System.InvalidOperationException: Sequence contains more than one element
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.MemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.MemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertInitializer(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.SplitVariableDeclarations(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.LocalDeclarationStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 

            Dim lstNew As List(Of UserDTO) = (From p In Context.HU_EMPLOYEE
                                              From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID)
                                              Where Not lst.Contains(cv.WORK_EMAIL.ToUpper) And
                                              (p.WORK_STATUS <> idTer Or p.WORK_STATUS Is Nothing) And
                                              cv.WORK_EMAIL IsNot Nothing
                                              Select New UserDTO With {
                                                 .EFFECT_DATE = DateTime.Now,
                                                 .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                                 .FULLNAME = p.FULLNAME_VN,
                                                 .EMAIL = cv.WORK_EMAIL,
                                                 .TELEPHONE = cv.MOBILE_PHONE,
                                                 .IS_AD = True,
                                                 .IS_APP = False,
                                                 .IS_PORTAL = True,
                                                 .IS_CHANGE_PASS = "-1",
                                                 .ACTFLG = "A",
                                                 .PASSWORD = p.EMPLOYEE_CODE,
                                                 .USERNAME = cv.WORK_EMAIL.ToUpper}).ToList

 */
            if (lstNew.Count > 0)
            {
                using (EncryptData EncryptData = new EncryptData())
                {
                    for (var i = 0; i <= lstNew.Count - 1; i++)
                    {
                        _newUser += ", " + (lstNew[i].USERNAME);
                        SE_USER _new = new SE_USER();
                        _new.ID = Utilities.GetNextSequence(Context, Context.SE_USER.EntitySet.Name);
                        _new.EFFECT_DATE = lstNew[i].EFFECT_DATE;
                        _new.EMPLOYEE_CODE = lstNew[i].EMPLOYEE_CODE;
                        _new.FULLNAME = lstNew[i].FULLNAME;
                        _new.EMAIL = lstNew[i].EMAIL;
                        _new.TELEPHONE = lstNew[i].TELEPHONE;
                        _new.IS_AD = lstNew[i].IS_AD;
                        _new.IS_APP = lstNew[i].IS_APP;
                        _new.IS_PORTAL = lstNew[i].IS_PORTAL;
                        _new.IS_CHANGE_PASS = lstNew[i].IS_CHANGE_PASS;
                        _new.ACTFLG = lstNew[i].ACTFLG;
                        _new.PASSWORD = EncryptData.EncryptString(lstNew[i].PASSWORD);
                        _new.USERNAME = lstNew[i].USERNAME;
                        _new.MODULE_ADMIN = "";
                        Context.SE_USER.AddObject(_new);
                    }
                }
            }
            if (_newUser != "")
                _newUser = _newUser.Substring(2);

            // Kiểm tra nhân viên có thay đổi thông tin
            List<UserDTO> lstCompare;
            ;/* Cannot convert AssignmentStatementSyntax, System.InvalidOperationException: Sequence contains more than one element
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.MemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.MemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitAssignmentStatement(AssignmentStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.AssignmentStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 
            lstCompare = (From p In Context.HU_EMPLOYEE
                          From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID)
                          From user In Context.SE_USER.Where(Function(f) f.EMPLOYEE_CODE = p.EMPLOYEE_CODE And
                                                                 f.MODULE_ADMIN Is Nothing)
                          Where (p.FULLNAME_VN <> user.FULLNAME Or _
                          cv.MOBILE_PHONE <> user.TELEPHONE Or _
                          cv.WORK_EMAIL <> user.EMAIL) And cv.WORK_EMAIL IsNot Nothing
                          Select New UserDTO With {
                              .ID = user.ID,
                              .USERNAME = user.USERNAME,
                              .FULLNAME = p.FULLNAME_VN,
                              .TELEPHONE = cv.MOBILE_PHONE,
                              .EMAIL = cv.WORK_EMAIL}).ToList

 */

            for (var i = 0; i <= lstCompare.Count - 1; i++)
            {
                var id = lstCompare[i].ID;
                _modifyUser += ", " + (lstCompare[i].USERNAME);
                var query = (from p in Context.SE_USER
                             where p.ID == id
                             select p).FirstOrDefault;
                query.FULLNAME = lstCompare[i].FULLNAME;
                query.TELEPHONE = lstCompare[i].TELEPHONE;
                query.EMAIL = lstCompare[i].EMAIL;
                query.USERNAME = lstCompare[i].EMAIL.ToUpper;
            }
            if (_modifyUser != "")
                _modifyUser = _modifyUser.Substring(2);
            ;/* Cannot convert LocalDeclarationStatementSyntax, System.InvalidOperationException: Sequence contains more than one element
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.MemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.MemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertInitializer(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.SplitVariableDeclarations(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.LocalDeclarationStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 

            'Kiểm tra nhân viên bị xóa
            Dim lstUserDelete = (From p In Context.HU_EMPLOYEE
                   From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID)
                   From user In Context.SE_USER.Where(Function(f) f.EMPLOYEE_CODE = p.EMPLOYEE_CODE)
                   Where (p.WORK_STATUS <> idTer Or p.WORK_STATUS Is Nothing Or cv.WORK_EMAIL Is Nothing) And
                      user.ACTFLG = "A" And user.MODULE_ADMIN.Length = 0
                      Select user).ToList

 */
            for (var i = 0; i <= lstUserDelete.Count - 1; i++)
            {
                _deleteUser += ", " + (lstUserDelete(i).USERNAME);
                decimal id = lstUserDelete(i).ID;
                lstUserDelete(i).ACTFLG = "I";
            }
            if (_deleteUser != "")
                _deleteUser = _deleteUser.Substring(2);

            Context.SaveChanges(log);
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool ResetUserPassword(List<decimal> _userid, int _minLength, bool _hasLowerChar, bool _hasUpperChar, bool _hasNumbericChar, bool _hasSpecialChar)
    {
        try
        {
            // Lấy danh sách user
            List<SE_USER> lst = (from p in Context.SE_USER
                                 where _userid.Contains(p.ID)
                                 select p).ToList;
            // Lấy thông tin config password
            RandomPassword rndPass = new RandomPassword();
            rndPass.HAS_LOWER_CHAR = _hasLowerChar;
            rndPass.HAS_NUMERIC_CHAR = _hasNumbericChar;
            rndPass.HAS_SPECIAL_CHAR = _hasSpecialChar;
            rndPass.HAS_UPPER_CHAR = _hasUpperChar;
            for (var i = 0; i <= lst.Count - 1; i++)
            {
                using (EncryptData EncryptData = new EncryptData())
                {
                    lst[i].PASSWORD = EncryptData.EncryptString(rndPass.Generate(_minLength));
                    lst[i].IS_CHANGE_PASS = 1;
                }
            }
            Context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public List<GroupDTO> GetGroupListToComboListBox()
    {
        var query = (from p in Context.SE_GROUP.ToList
                     where System.Convert.ToBoolean(p.IS_ADMIN) == false & p.ACTFLG.ToUpper == "A"
                     orderby p.NAME
                     select new GroupDTO() { ID = p.ID, NAME = p.NAME, CODE = p.CODE });
        return query.ToList;
    }
    public List<GroupDTO> GetGroupList(GroupDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "MODIFIED_DATE desc")
    {
        var query = (from p in Context.SE_GROUP
                     where System.Convert.ToBoolean(p.IS_ADMIN) == false
                     orderby p.NAME
                     select new GroupDTO() { ID = p.ID, NAME = p.NAME, CODE = p.CODE, EFFECT_DATE = p.EFFECT_DATE, EXPIRE_DATE = p.EXPIRE_DATE, ACTFLG = p.ACTFLG, MODIFIED_DATE = p.MODIFIED_DATE });

        if (_filter.CODE != "")
            query = query.Where(p => p.CODE.ToUpper.Contains(_filter.CODE.ToUpper));
        if (_filter.NAME != "")
            query = query.Where(p => p.NAME.ToUpper.Contains(_filter.NAME.ToUpper));
        if (_filter.ACTFLG != "")
            query = query.Where(p => p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper));
        if (_filter.EFFECT_DATE != null)
            query = query.Where(p => p.EFFECT_DATE == _filter.EFFECT_DATE);
        if (_filter.EXPIRE_DATE != null)
            query = query.Where(p => p.EXPIRE_DATE == _filter.EXPIRE_DATE);
        query = query.AsQueryable().OrderBy(Sorts);
        Total = query.Count;
        query = query.Skip(PageIndex * PageSize).Take(PageSize);

        var result = (from p in query
                      select p);

        return result.ToList;
    }

    public bool ValidateGroupList(GroupDTO _validate)
    {
        var query;

        if (_validate.CODE != null/* TODO Change to default(_) if this is not a reference type */ )
        {
            query = (from p in Context.SE_GROUP
                     where p.CODE.ToUpper == _validate.CODE.ToUpper && p.ID != _validate.ID
                     select p).FirstOrDefault;
            return (query == null);
        }
        if (_validate.NAME != null/* TODO Change to default(_) if this is not a reference type */ )
        {
            query = (from p in Context.SE_GROUP
                     where p.NAME.ToUpper == _validate.NAME.ToUpper && p.ID != _validate.ID
                     select p).FirstOrDefault;
            return (query == null);
        }
        return true;
    }

    public bool InsertGroup(GroupDTO _group, UserLog log)
    {
        try
        {
            SE_GROUP _new = new SE_GROUP();
            _new.ID = Utilities.GetNextSequence(Context, Context.SE_GROUP.EntitySet.Name);
            _new.CODE = _group.CODE;
            _new.NAME = _group.NAME;
            _new.IS_ADMIN = _group.IS_ADMIN;
            _new.ACTFLG = "A";
            _new.EFFECT_DATE = _group.EFFECT_DATE;
            _new.EXPIRE_DATE = _group.EXPIRE_DATE;
            Context.SE_GROUP.AddObject(_new);
            Context.SaveChanges(log);
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public bool UpdateGroup(GroupDTO _group, UserLog log)
    {
        try
        {
            SE_GROUP lstGroup = (from p in Context.SE_GROUP
                                 where p.ID == _group.ID
                                 select p).FirstOrDefault;
            if (lstGroup != null)
            {
                lstGroup.NAME = _group.NAME;
                lstGroup.IS_ADMIN = _group.IS_ADMIN;
                lstGroup.CODE = _group.CODE;
                lstGroup.EFFECT_DATE = _group.EFFECT_DATE;
                lstGroup.EXPIRE_DATE = _group.EXPIRE_DATE;
                Context.SaveChanges(log);
            }
            else
                return false;
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public bool DeleteGroup(List<decimal> _lstID, ref string _error, UserLog log)
    {
        try
        {
            _error = "";
            List<SE_GROUP> lstGroup = (from p in Context.SE_GROUP
                                       where _lstID.Contains(p.ID)
                                       select p).ToList;
            List<SE_GROUP> lstDeletes = new List<SE_GROUP>();
            for (int i = 0; i <= lstGroup.Count - 1; i++)
            {
                if (lstGroup[i].SE_USERS.Count > 0)
                {
                    if (_error == "")
                        _error = lstGroup[i].NAME;
                    else
                        _error = _error + "," + lstGroup[i].NAME;

                    if (_lstID.Contains(lstGroup[i].ID))
                        _lstID.Remove(lstGroup[i].ID);
                }
                else
                    lstDeletes.Add(lstGroup[i]);
            }
            List<SE_GROUP_PERMISSION> lstPermissions = (from p in Context.SE_GROUP_PERMISSION
                                                        where _lstID.Contains(p.GROUP_ID)
                                                        select p).ToList;
            if (lstGroup.Count > 0)
            {
                for (var i = 0; i <= lstPermissions.Count - 1; i++)
                    Context.SE_GROUP_PERMISSION.DeleteObject(lstPermissions[i]);
                for (i = 0; i <= lstDeletes.Count - 1; i++)
                {
                    lstDeletes[i].SE_REPORTS.Clear();
                    Context.SE_GROUP.DeleteObject(lstDeletes[i]);
                }
                Context.SaveChanges(log);
            }
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return false;
    }
    public bool UpdateGroupStatus(List<decimal> _lstID, string _ACTFLG, UserLog log)
    {
        try
        {
            List<SE_GROUP> lstGroup = (from p in Context.SE_GROUP
                                       where _lstID.Contains(p.ID)
                                       select p).ToList;
            if (lstGroup.Count > 0)
            {
                for (var i = 0; i <= lstGroup.Count - 1; i++)
                {
                    lstGroup[i].ACTFLG = _ACTFLG;
                    lstGroup[i].MODIFIED_DATE = DateTime.Now;
                    lstGroup[i].MODIFIED_BY = log.Username;
                    lstGroup[i].MODIFIED_LOG = log.ComputerName;
                }
                Context.SaveChanges(log);
            }
            else
                return false;
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public List<FunctionDTO> GetFunctionList(FunctionDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "NAME asc")
    {
        var query = (from p in Context.SE_FUNCTION
                     select new FunctionDTO() { ID = p.ID, FID = p.FID, NAME = p.NAME, MODULE_NAME = p.SE_MODULE.NAME, FUNCTION_GROUP_ID = p.SE_FUNCTION_GROUP.ID, FUNCTION_GROUP_NAME = p.SE_FUNCTION_GROUP.NAME, ACTFLG = p.ACTFLG, MODULE_ID = p.MODULE_ID });
        if (_filter.ID != 0)
            query = query.Where(p => p.ID == _filter.ID);
        if (_filter.FID != "")
            query = query.Where(p => p.FID.ToUpper.Contains(_filter.FID.ToUpper));
        if (_filter.NAME != "")
            query = query.Where(p => p.NAME.ToUpper.Contains(_filter.NAME.ToUpper));
        if (_filter.MODULE_ID != 0)
            query = query.Where(p => p.MODULE_ID == _filter.MODULE_ID);
        if (_filter.FUNCTION_GROUP_ID != 0)
            query = query.Where(p => p.FUNCTION_GROUP_ID == _filter.FUNCTION_GROUP_ID);
        if (_filter.ACTFLG != "")
            query = query.Where(p => p.ACTFLG == _filter.ACTFLG);
        if (PageSize != -1)
        {
            query = query.OrderBy(Sorts);
            Total = query.Count;
            query = query.Skip(PageIndex * PageSize).Take(PageSize);
        }
        var result = (from p in query
                      select p);
        return result.ToList;
    }

    public bool ValidateFunctionList(FunctionDTO _validate)
    {
        var query;
        if (_validate.NAME != "")
        {
            query = (from p in Context.SE_FUNCTION
                     where p.NAME.ToUpper == _validate.NAME.ToUpper
                     select p).FirstOrDefault;
            return (query == null);
        }
        if (_validate.FID != "")
        {
            query = (from p in Context.SE_FUNCTION
                     where p.FID.ToUpper == _validate.FID.ToUpper
                     select p).FirstOrDefault;
            return (query == null);
        }
        return true;
    }

    public bool UpdateFunctionList(List<FunctionDTO> _function, UserLog log)
    {
        int i;
        for (i = 0; i <= _function.Count - 1; i++)
        {
            SE_FUNCTION obj = new SE_FUNCTION() { ID = _function[0].ID };

            Context.SE_FUNCTION.Attach(obj);
            obj.NAME = _function[i].NAME;
            obj.FID = _function[i].FID;
            obj.GROUP_ID = _function[i].FUNCTION_GROUP_ID;
            obj.MODULE_ID = _function[i].MODULE_ID;
            obj.MODIFIED_DATE = DateTime.Now;
            obj.MODIFIED_BY = log.Username;
            obj.MODIFIED_LOG = log.ComputerName;
            Context.SaveChanges(log);
        }
        return true;
    }

    public bool InsertFunctionList(FunctionDTO _item, UserLog log)
    {
        try
        {
            SE_FUNCTION _new = new SE_FUNCTION();
            _new.ID = Utilities.GetNextSequence(Context, Context.SE_FUNCTION.EntitySet.Name);
            _new.ACTFLG = _item.ACTFLG;
            _new.NAME = _item.NAME;
            _new.FID = _item.FID;
            _new.GROUP_ID = _item.FUNCTION_GROUP_ID;
            _new.MODULE_ID = _item.MODULE_ID;
            _new.MODIFIED_DATE = DateTime.Now;
            _new.MODIFIED_BY = log.Username;
            _new.MODIFIED_LOG = log.ComputerName;
            Context.SE_FUNCTION.AddObject(_new);
            Context.SaveChanges(log);
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<ModuleDTO> GetModuleList()
    {
        var query = (from p in Context.SE_MODULE
                     select new ModuleDTO() { ID = p.ID, NAME = p.NAME, MID = p.MID });
        return query.ToList;
    }

    public bool ActiveFunctions(List<FunctionDTO> lstFunction, string sActive, UserLog log)
    {
        try
        {
            List<SE_FUNCTION> lstFunctionData;
            List<decimal> lstIDFunction = (from p in lstFunction.ToList
                                           select p.ID).ToList;
            lstFunctionData = (from p in Context.SE_FUNCTION
                               where lstIDFunction.Contains(p.ID)
                               select p).ToList;
            for (var index = 0; index <= lstFunctionData.Count - 1; index++)
            {
                lstFunctionData[index].ACTFLG = sActive;
                lstFunctionData[index].MODIFIED_DATE = DateTime.Now;
                lstFunctionData[index].MODIFIED_BY = log.Username;
                lstFunctionData[index].MODIFIED_LOG = log.ComputerName;
                return false;
            }
            Context.SaveChanges(log);
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public List<UserDTO> GetUserListInGroup(decimal _groupID, UserDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
    {
        ;/* Cannot convert LocalDeclarationStatementSyntax, System.InvalidOperationException: Sequence contains more than one element
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertInitializer(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.SplitVariableDeclarations(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.LocalDeclarationStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 

        Dim query = (From p In Context.SE_USER
                     From m In p.SE_GROUPS
                     Where m.ID = _groupID
                     Select p)

 */
        if (_filter.USERNAME != "")
            query = query.Where(p => p.USERNAME.ToUpper.Contains(_filter.USERNAME.ToUpper));
        if (_filter.FULLNAME != "")
            query = query.Where(p => p.FULLNAME.ToUpper.Contains(_filter.FULLNAME.ToUpper));
        if (_filter.EMAIL != "")
            query = query.Where(p => p.EMAIL.ToUpper.Contains(_filter.EMAIL.ToUpper));
        if (_filter.TELEPHONE != "")
            query = query.Where(p => p.TELEPHONE.ToUpper.Contains(_filter.TELEPHONE.ToUpper));
        if (_filter.IS_APP != null)
            query = query.Where(p => p.IS_APP == _filter.IS_APP);
        if (_filter.IS_PORTAL != null)
            query = query.Where(p => p.IS_PORTAL == _filter.IS_PORTAL);
        if (_filter.IS_AD != null)
            query = query.Where(p => p.IS_AD == _filter.IS_AD);

        query = query.OrderBy(Sorts);
        Total = query.Count;
        query = query.Skip(PageIndex * PageSize).Take(PageSize);

        return (from p in query
                select new UserDTO() { ID = p.ID, USERNAME = p.USERNAME, FULLNAME = p.FULLNAME, EMAIL = p.EMAIL, TELEPHONE = p.TELEPHONE, IS_AD = p.IS_AD, IS_APP = p.IS_APP, IS_PORTAL = p.IS_PORTAL }).ToList;
    }

    public List<UserDTO> GetUserListOutGroup(decimal _groupID, UserDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
    {
        ;/* Cannot convert LocalDeclarationStatementSyntax, System.InvalidOperationException: Sequence contains more than one element
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertInitializer(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.SplitVariableDeclarations(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.LocalDeclarationStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 
        Dim userIn = (From p In Context.SE_USER
                From m In p.SE_GROUPS
                Where m.ID = _groupID
                Select p)

 */
        var query = (from p in Context.SE_USER
                     where p.ACTFLG.ToUpper == "A"
                     select p);
        if (_filter.USERNAME != "")
            query = query.Where(p => p.USERNAME.ToUpper.Contains(_filter.USERNAME.ToUpper));
        if (_filter.FULLNAME != "")
            query = query.Where(p => p.FULLNAME.ToUpper.Contains(_filter.FULLNAME.ToUpper));
        if (_filter.IS_APP != null)
            query = query.Where(p => p.IS_APP == _filter.IS_APP);
        if (_filter.IS_PORTAL != null)
            query = query.Where(p => p.IS_PORTAL == _filter.IS_PORTAL);
        if (_filter.IS_AD != null)
            query = query.Where(p => p.IS_AD == _filter.IS_AD);
        query = query.Except(userIn);

        query = query.OrderBy(Sorts);
        Total = query.Count;
        query = query.Skip(PageIndex * PageSize).Take(PageSize);

        var lst = (from p in query
                   select new UserDTO() { ID = p.ID, USERNAME = p.USERNAME, FULLNAME = p.FULLNAME, EMAIL = p.EMAIL, TELEPHONE = p.TELEPHONE, IS_AD = p.IS_AD, IS_APP = p.IS_APP, IS_PORTAL = p.IS_PORTAL });
        return lst.ToList;
    }

    public bool InsertUserGroup(decimal _groupID, List<decimal> _lstUserID, UserLog log)
    {
        SE_GROUP lstGroup = (from p in Context.SE_GROUP
                             where p.ID == _groupID
                             select p).FirstOrDefault;
        for (int i = 0; i <= _lstUserID.Count - 1; i++)
        {
            decimal id = _lstUserID[i];
            SE_USER user = (from p in Context.SE_USER
                            where p.ID == id
                            select p).FirstOrDefault;
            lstGroup.SE_USERS.Add(user);
        }
        Context.SaveChanges(log);
        using (ConnectionManager conMng = new ConnectionManager())
        {
            using (OracleConnection conn = new OracleConnection(conMng.GetConnectionString()))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.Transaction = cmd.Connection.BeginTransaction();
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "PKG_COMMON_BUSINESS.TRANSFER_GROUP_TO_USER";
                        for (int i = 0; i <= _lstUserID.Count - 1; i++)
                        {
                            cmd.Parameters.Clear();
                            using (DataAccess.OracleCommon resource = new DataAccess.OracleCommon())
                            {
                                var objParam = new { P_USER_ID = _lstUserID[i], P_GROUP_ID = _groupID, P_USERNAME = log.Username };

                                if (objParam != null)
                                {
                                    foreach (PropertyInfo info in objParam.GetType().GetProperties())
                                    {
                                        bool bOut = false;
                                        var para = resource.GetParameter(info.Name, info.GetValue(objParam, null), bOut);
                                        if (para != null)
                                            cmd.Parameters.Add(para);
                                    }
                                }
                                cmd.ExecuteNonQuery();
                            }
                        }
                        cmd.Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        cmd.Transaction.Rollback();
                    }
                    finally
                    {
                        // Dispose all resource
                        cmd.Dispose();
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
        }

        return true;
    }

    public bool DeleteUserGroup(decimal _groupID, List<decimal> _lstUserID, UserLog log)
    {
        SE_GROUP lstGroup = (from p in Context.SE_GROUP
                             where p.ID == _groupID
                             select p).FirstOrDefault;
        if (lstGroup != null)
        {
            for (int i = 0; i <= _lstUserID.Count - 1; i++)
            {
                SE_USER user = (from p in lstGroup.SE_USERS.ToList
                                where p.ID == _lstUserID[i]
                                select p).FirstOrDefault;
                if (user != null)
                    lstGroup.SE_USERS.Remove(user);
            }
            Context.SaveChanges(log);
        }
        else
            return false;
        return true;
    }

    public bool SendMailConfirmUserPassword(List<decimal> _userid, string _subject, string _content)
    {
        try
        {
            string defaultFrom = "";
            Dictionary<string, string> config;
            config = GetConfig(ModuleID.All);
            defaultFrom = config.ContainsKey("MailFrom") ? config["MailFrom"] : "";

            if (defaultFrom == "")
                return false;
            // Lấy danh sách user
            List<UserDTO> lst = (from p in Context.SE_USER
                                 where _userid.Contains(p.ID) & p.IS_CHANGE_PASS >= 0 & p.EMAIL != null
                                 select new UserDTO() { FULLNAME = p.FULLNAME, EMAIL = p.EMAIL, PASSWORD = p.PASSWORD }).ToList;
            foreach (UserDTO user in lst)
            {
                using (EncryptData EncryptData = new EncryptData())
                {
                    if (user.EMAIL != "" && System.Text.RegularExpressions.Regex.IsMatch(user.EMAIL, @"^([a-zA-Z0-9_\.-]+)@([a-zA-Z0-9_\.-]+)\.([a-zA-Z\.]{2,6})$"))
                        InsertMail(defaultFrom, user.EMAIL, _subject, string.Format(_content, EncryptData.DecryptString(user.PASSWORD)));
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<UserDTO> GetUserNeedSendMail(decimal _groupid)
    {
        try
        {
            ;/* Cannot convert LocalDeclarationStatementSyntax, System.InvalidOperationException: Sequence contains more than one element
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.MemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.MemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertInitializer(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.SplitVariableDeclarations(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.LocalDeclarationStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 
            Dim lst As List(Of UserDTO) = (From p In Context.SE_USER
                                           From n In p.SE_GROUPS
                                           Where n.ID = _groupid And p.IS_CHANGE_PASS >= 0
                                           Select New UserDTO With {
                                               .ID = p.ID,
                                               .FULLNAME = p.FULLNAME,
                                               .USERNAME = p.USERNAME,
                                               .IS_CHANGE_PASS = p.IS_CHANGE_PASS,
                                               .EMAIL = p.EMAIL}).ToList

 */
            for (var i = lst.Count - 1; i >= 0; i += -1)
            {
                if (lst[i].EMAIL == "" || !System.Text.RegularExpressions.Regex.IsMatch(lst[i].EMAIL, @"^([a-zA-Z0-9_\.-]+)@([a-zA-Z0-9_\.-]+)\.([a-zA-Z\.]{2,6})$"))
                    lst.RemoveAt(i);
            }
            return lst;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public List<GroupFunctionDTO> GetGroupFunctionPermision(decimal _groupID, GroupFunctionDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "FUNCTION_NAME asc")
    {
        var query = (from p in Context.SE_GROUP_PERMISSION
                     where p.GROUP_ID == _groupID
                     select new GroupFunctionDTO() { ID = p.ID, FUNCTION_ID = p.FUNCTION_ID, FUNCTION_CODE = p.SE_FUNCTION.FID, FUNCTION_NAME = p.SE_FUNCTION.NAME, GROUP_ID = p.GROUP_ID, MODULE_NAME = p.SE_FUNCTION.SE_MODULE.NAME, ALLOW_CREATE = p.ALLOW_CREATE, ALLOW_DELETE = p.ALLOW_DELETE, ALLOW_EXPORT = p.ALLOW_EXPORT, ALLOW_IMPORT = p.ALLOW_IMPORT, ALLOW_MODIFY = p.ALLOW_MODIFY, ALLOW_PRINT = p.ALLOW_PRINT, ALLOW_SPECIAL1 = p.ALLOW_SPECIAL1, ALLOW_SPECIAL2 = p.ALLOW_SPECIAL2, ALLOW_SPECIAL3 = p.ALLOW_SPECIAL3, ALLOW_SPECIAL4 = p.ALLOW_SPECIAL4, ALLOW_SPECIAL5 = p.ALLOW_SPECIAL5 });

        if (_filter.FUNCTION_NAME != "")
            query = query.Where(p => p.FUNCTION_NAME.ToUpper.Contains(_filter.FUNCTION_NAME.ToUpper) | p.FUNCTION_CODE.ToUpper.Contains(_filter.FUNCTION_NAME.ToUpper));

        if (_filter.MODULE_NAME != "")
            query = query.Where(p => p.MODULE_NAME == _filter.MODULE_NAME);

        query = query.OrderBy(Sorts);
        Total = query.Count;
        query = query.Skip(PageIndex * PageSize).Take(PageSize);
        return query.ToList;
    }

    public List<FunctionDTO> GetGroupFunctionNotPermision(decimal _groupID, FunctionDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "NAME asc")
    {
        var lstTemp = (from p in Context.SE_FUNCTION
                       select p);
        if (_filter.MODULE_NAME != "")
            lstTemp = lstTemp.Where(p => p.SE_MODULE.NAME.ToUpper.Contains(_filter.MODULE_NAME.ToUpper));
        if (_filter.FUNCTION_GROUP_NAME != "")
            lstTemp = lstTemp.Where(p => p.SE_FUNCTION_GROUP.NAME.ToUpper.Contains(_filter.FUNCTION_GROUP_NAME.ToUpper));
        if (_filter.NAME != "")
            lstTemp = lstTemp.Where(p => p.NAME.ToUpper.Contains(_filter.NAME.ToUpper));
        var lst1 = (from p in lstTemp
                    select p.ID);
        var lst2 = (from p in Context.SE_GROUP_PERMISSION
                    where p.GROUP_ID == _groupID
                    select p.FUNCTION_ID);
        List<decimal> lstID = lst1.Except(lst2).ToList;

        var query = (from p in Context.SE_FUNCTION
                     where lstID.Contains(p.ID)
                     orderby p.NAME
                     select new FunctionDTO() { ID = p.ID, NAME = p.NAME, MODULE_NAME = p.SE_MODULE.NAME, FID = p.FID, FUNCTION_GROUP_NAME = p.SE_FUNCTION_GROUP.NAME });

        query = query.AsQueryable().OrderBy(Sorts);
        Total = query.Count;
        query = query.Skip(PageIndex * PageSize).Take(PageSize);
        return query.ToList;
    }

    public bool InsertGroupFunction(List<GroupFunctionDTO> _lstGroupFunc, UserLog log)
    {
        for (int i = 0; i <= _lstGroupFunc.Count - 1; i++)
        {
            var itemAdd = _lstGroupFunc[i];

            var functionCheck = Context.SE_GROUP_PERMISSION.FirstOrDefault(p => p.FUNCTION_ID == itemAdd.FUNCTION_ID && p.GROUP_ID == itemAdd.GROUP_ID);

            if (functionCheck != null)
                continue;

            SE_GROUP_PERMISSION _new = new SE_GROUP_PERMISSION();
            _new.ID = Utilities.GetNextSequence(Context, Context.SE_GROUP_PERMISSION.EntitySet.Name);
            _new.ALLOW_CREATE = _lstGroupFunc[i].ALLOW_CREATE;
            _new.ALLOW_MODIFY = _lstGroupFunc[i].ALLOW_MODIFY;
            _new.ALLOW_DELETE = _lstGroupFunc[i].ALLOW_DELETE;
            _new.ALLOW_PRINT = _lstGroupFunc[i].ALLOW_PRINT;
            _new.ALLOW_IMPORT = _lstGroupFunc[i].ALLOW_IMPORT;
            _new.ALLOW_EXPORT = _lstGroupFunc[i].ALLOW_EXPORT;
            _new.ALLOW_SPECIAL1 = _lstGroupFunc[i].ALLOW_SPECIAL1;
            _new.ALLOW_SPECIAL2 = _lstGroupFunc[i].ALLOW_SPECIAL2;
            _new.ALLOW_SPECIAL3 = _lstGroupFunc[i].ALLOW_SPECIAL3;
            _new.ALLOW_SPECIAL4 = _lstGroupFunc[i].ALLOW_SPECIAL4;
            _new.ALLOW_SPECIAL5 = _lstGroupFunc[i].ALLOW_SPECIAL5;
            _new.FUNCTION_ID = _lstGroupFunc[i].FUNCTION_ID;
            _new.GROUP_ID = _lstGroupFunc[i].GROUP_ID;
            _new.CREATED_DATE = DateTime.Now;
            _new.CREATED_BY = log.Username;
            _new.CREATED_LOG = log.ComputerName;
            _new.MODIFIED_DATE = DateTime.Now;
            _new.MODIFIED_BY = log.Username;
            _new.MODIFIED_LOG = log.ComputerName;
            Context.SE_GROUP_PERMISSION.AddObject(_new);
        }
        Context.SaveChanges(log);
        return true;
    }

    public bool UpdateGroupFunction(List<GroupFunctionDTO> _lstGroupFunc, UserLog log)
    {
        int i;
        List<decimal> lstID = (from p in _lstGroupFunc
                               select p.ID).ToList();
        List<SE_GROUP_PERMISSION> objUpdate = (from p in Context.SE_GROUP_PERMISSION
                                               where lstID.Contains(p.ID)
                                               select p).ToList;
        for (i = 0; i <= objUpdate.Count - 1; i++)
        {
            GroupFunctionDTO func = _lstGroupFunc.Find(GroupFunctionDTO item => item.ID == objUpdate[i].ID);
            if (func != null)
            {
                objUpdate[i].ALLOW_CREATE = func.ALLOW_CREATE ? 1 : 0;
                objUpdate[i].ALLOW_DELETE = func.ALLOW_DELETE ? 1 : 0;
                objUpdate[i].ALLOW_EXPORT = func.ALLOW_EXPORT ? 1 : 0;
                objUpdate[i].ALLOW_IMPORT = func.ALLOW_IMPORT ? 1 : 0;
                objUpdate[i].ALLOW_MODIFY = func.ALLOW_MODIFY ? 1 : 0;
                objUpdate[i].ALLOW_PRINT = func.ALLOW_PRINT ? 1 : 0;
                objUpdate[i].ALLOW_SPECIAL1 = func.ALLOW_SPECIAL1 ? 1 : 0;
                objUpdate[i].ALLOW_SPECIAL2 = func.ALLOW_SPECIAL2 ? 1 : 0;
                objUpdate[i].ALLOW_SPECIAL3 = func.ALLOW_SPECIAL3 ? 1 : 0;
                objUpdate[i].ALLOW_SPECIAL4 = func.ALLOW_SPECIAL4 ? 1 : 0;
                objUpdate[i].ALLOW_SPECIAL5 = func.ALLOW_SPECIAL5 ? 1 : 0;
                objUpdate[i].MODIFIED_DATE = DateTime.Now;
                objUpdate[i].MODIFIED_BY = log.Username;
                objUpdate[i].MODIFIED_LOG = log.ComputerName;
            }
        }
        Context.SaveChanges(log);
        return true;
    }

    public bool DeleteGroupFunction(List<decimal> _lstID)
    {
        try
        {
            using (ConnectionManager conMng = new ConnectionManager())
            {
                using (OracleConnection conn = new OracleConnection(conMng.GetConnectionString()))
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        try
                        {
                            cmd.Connection = conn;
                            cmd.Transaction = cmd.Connection.BeginTransaction();
                            for (int i = 0; i <= _lstID.Count - 1; i++)
                            {
                                cmd.CommandText = "DELETE SE_GROUP_PERMISSION WHERE ID =" + _lstID[i];
                                cmd.ExecuteNonQuery();
                            }
                            cmd.Transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            cmd.Transaction.Rollback();
                        }
                        finally
                        {
                            // Dispose all resource
                            cmd.Dispose();
                            conn.Close();
                            conn.Dispose();
                        }
                    }
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public bool CopyGroupFunction(decimal groupCopyID, decimal groupID, UserLog log)
    {
        // bổ sung delete phục vụ cho chức năng copy function
        var lstDel = (from p in Context.SE_GROUP_PERMISSION
                      where p.GROUP_ID == groupID
                      select p).ToList;
        foreach (var itm in lstDel)
            Context.SE_GROUP_PERMISSION.DeleteObject(itm);
        Context.SaveChanges(log);

        var _lstGroupFunc = (from p in Context.SE_GROUP_PERMISSION
                             where p.GROUP_ID == groupCopyID
                             select new GroupFunctionDTO() { GROUP_ID = groupID, FUNCTION_ID = p.FUNCTION_ID, ALLOW_CREATE = p.ALLOW_CREATE, ALLOW_DELETE = p.ALLOW_DELETE, ALLOW_EXPORT = p.ALLOW_EXPORT, ALLOW_IMPORT = p.ALLOW_IMPORT, ALLOW_MODIFY = p.ALLOW_MODIFY, ALLOW_PRINT = p.ALLOW_PRINT, ALLOW_SPECIAL1 = p.ALLOW_SPECIAL1, ALLOW_SPECIAL2 = p.ALLOW_SPECIAL2, ALLOW_SPECIAL3 = p.ALLOW_SPECIAL3, ALLOW_SPECIAL4 = p.ALLOW_SPECIAL4, ALLOW_SPECIAL5 = p.ALLOW_SPECIAL5 }).ToList;
        for (int i = 0; i <= _lstGroupFunc.Count - 1; i++)
        {
            var itemAdd = _lstGroupFunc(i);

            var functionCheck = Context.SE_GROUP_PERMISSION.FirstOrDefault(p => p.FUNCTION_ID == itemAdd.FUNCTION_ID && p.GROUP_ID == itemAdd.GROUP_ID);

            if (functionCheck != null)
                continue;

            SE_GROUP_PERMISSION _new = new SE_GROUP_PERMISSION();
            _new.ID = Utilities.GetNextSequence(Context, Context.SE_GROUP_PERMISSION.EntitySet.Name);
            _new.ALLOW_CREATE = _lstGroupFunc(i).ALLOW_CREATE;
            _new.ALLOW_MODIFY = _lstGroupFunc(i).ALLOW_MODIFY;
            _new.ALLOW_DELETE = _lstGroupFunc(i).ALLOW_DELETE;
            _new.ALLOW_PRINT = _lstGroupFunc(i).ALLOW_PRINT;
            _new.ALLOW_IMPORT = _lstGroupFunc(i).ALLOW_IMPORT;
            _new.ALLOW_EXPORT = _lstGroupFunc(i).ALLOW_EXPORT;
            _new.ALLOW_SPECIAL1 = _lstGroupFunc(i).ALLOW_SPECIAL1;
            _new.ALLOW_SPECIAL2 = _lstGroupFunc(i).ALLOW_SPECIAL2;
            _new.ALLOW_SPECIAL3 = _lstGroupFunc(i).ALLOW_SPECIAL3;
            _new.ALLOW_SPECIAL4 = _lstGroupFunc(i).ALLOW_SPECIAL4;
            _new.ALLOW_SPECIAL5 = _lstGroupFunc(i).ALLOW_SPECIAL5;
            _new.FUNCTION_ID = _lstGroupFunc(i).FUNCTION_ID;
            _new.GROUP_ID = _lstGroupFunc(i).GROUP_ID;
            Context.SE_GROUP_PERMISSION.AddObject(_new);
            Context.SaveChanges(log);
        }
        return true;
    }



    public List<GroupReportDTO> GetGroupReportList(decimal _groupID)
    {
        var query = (from p in Context.SE_REPORT
                     orderby p.NAME
                     select new GroupReportDTO() { ID = p.ID, REPORT_NAME = p.NAME, MODULE_NAME = p.SE_MODULE.NAME, IS_USE = ((from n in p.SE_GROUPS where n.ID == _groupID select n).Count > 0) });
        return query.ToList;
    }

    public List<GroupReportDTO> GetGroupReportListFilter(decimal _groupID, GroupReportDTO _filter)
    {
        var query = (from p in Context.SE_REPORT
                     orderby p.NAME
                     where (_filter.REPORT_NAME == null/* TODO Change to default(_) if this is not a reference type */ | p.NAME.ToUpper == _filter.REPORT_NAME.ToUpper) & (_filter.MODULE_ID == null/* TODO Change to default(_) if this is not a reference type */ | p.SE_MODULE.ID == _filter.MODULE_ID)
                     select new GroupReportDTO() { ID = p.ID, REPORT_NAME = p.NAME, MODULE_NAME = p.SE_MODULE.NAME, IS_USE = ((from n in p.SE_GROUPS where n.ID == _groupID select n).Count > 0) });
        return query.ToList;
    }

    public bool UpdateGroupReport(decimal _groupID, List<GroupReportDTO> _lstReport)
    {
        SE_GROUP query;
        query = (from p in Context.SE_GROUP
                 where p.ID == _groupID
                 select p).FirstOrDefault;
        List<decimal> Ids = (from p in _lstReport
                             select p.ID).ToList();
        var lst = (from p in Context.SE_REPORT
                   where Ids.Contains(p.ID)
                   select p).ToList;
        if (query != null)
        {
            query.SE_REPORTS.Clear();
            for (int i = 0; i <= lst.Count - 1; i++)
                query.SE_REPORTS.Add(lst(i));
            Context.SaveChanges();
        }
        else
            return false;
        return true;
    }



    public List<UserFunctionDTO> GetUserFunctionPermision(decimal _UserID, UserFunctionDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "FUNCTION_NAME asc")
    {
        try
        {
            ;/* Cannot convert LocalDeclarationStatementSyntax, System.InvalidOperationException: Sequence contains more than one element
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertInitializer(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.SplitVariableDeclarations(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.LocalDeclarationStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 
            Dim query = (From p In Context.SE_USER_PERMISSION
                         From f In Context.SE_FUNCTION.Where(Function(f) f.ID = p.FUNCTION_ID)
                         Where p.USER_ID = _UserID
                         Select New UserFunctionDTO With {
                             .ID = p.ID,
                             .FUNCTION_ID = p.FUNCTION_ID,
                             .FUNCTION_CODE = f.FID,
                             .FUNCTION_NAME = f.NAME,
                             .USER_ID = p.USER_ID,
                             .MODULE_NAME = f.SE_MODULE.NAME,
                             .ALLOW_CREATE = p.ALLOW_CREATE,
                             .ALLOW_DELETE = p.ALLOW_DELETE,
                             .ALLOW_EXPORT = p.ALLOW_EXPORT,
                             .ALLOW_IMPORT = p.ALLOW_IMPORT,
                             .ALLOW_MODIFY = p.ALLOW_MODIFY,
                             .ALLOW_PRINT = p.ALLOW_PRINT,
                             .ALLOW_SPECIAL1 = p.ALLOW_SPECIAL1,
                             .ALLOW_SPECIAL2 = p.ALLOW_SPECIAL2,
                             .ALLOW_SPECIAL3 = p.ALLOW_SPECIAL3,
                             .ALLOW_SPECIAL4 = p.ALLOW_SPECIAL4,
                             .ALLOW_SPECIAL5 = p.ALLOW_SPECIAL5})

 */
            if (_filter.FUNCTION_NAME != "")
                query = query.Where(p => p.FUNCTION_NAME.ToUpper.Contains(_filter.FUNCTION_NAME.ToUpper) | p.FUNCTION_CODE.ToUpper.Contains(_filter.FUNCTION_NAME.ToUpper));

            if (_filter.MODULE_NAME != "")
                query = query.Where(p => p.MODULE_NAME == _filter.MODULE_NAME);

            query = query.OrderBy(Sorts);
            Total = query.Count;
            query = query.Skip(PageIndex * PageSize).Take(PageSize);
            return query.ToList;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<FunctionDTO> GetUserFunctionNotPermision(decimal _UserID, FunctionDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "NAME asc")
    {
        try
        {
            var lstTemp = (from p in Context.SE_FUNCTION
                           select p);
            if (_filter.MODULE_NAME != "")
                lstTemp = lstTemp.Where(p => p.SE_MODULE.NAME.ToUpper.Contains(_filter.MODULE_NAME.ToUpper));
            if (_filter.FUNCTION_GROUP_NAME != "")
                lstTemp = lstTemp.Where(p => p.SE_FUNCTION_GROUP.NAME.ToUpper.Contains(_filter.FUNCTION_GROUP_NAME.ToUpper));
            if (_filter.NAME != "")
                lstTemp = lstTemp.Where(p => p.NAME.ToUpper.Contains(_filter.NAME.ToUpper));
            var lst1 = (from p in lstTemp
                        select p.ID);
            var lst2 = (from p in Context.SE_USER_PERMISSION
                        where p.USER_ID == _UserID
                        select p.FUNCTION_ID);
            List<decimal> lstID = lst1.Except(lst2).ToList;

            var query = (from p in Context.SE_FUNCTION
                         where lstID.Contains(p.ID)
                         orderby p.NAME
                         select new FunctionDTO() { ID = p.ID, NAME = p.NAME, MODULE_NAME = p.SE_MODULE.NAME, FID = p.FID, FUNCTION_GROUP_NAME = p.SE_FUNCTION_GROUP.NAME });

            query = query.AsQueryable().OrderBy(Sorts);
            Total = query.Count;
            query = query.Skip(PageIndex * PageSize).Take(PageSize);
            return query.ToList;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool InsertUserFunction(List<UserFunctionDTO> _lstUserFunc, UserLog log)
    {
        try
        {
            for (int i = 0; i <= _lstUserFunc.Count - 1; i++)
            {
                var itemAdd = _lstUserFunc[i];

                var functionCheck = (from p in Context.SE_USER_PERMISSION
                                     where p.FUNCTION_ID == itemAdd.FUNCTION_ID && p.USER_ID == itemAdd.USER_ID
                                     select p).FirstOrDefault;

                if (functionCheck != null)
                    continue;

                SE_USER_PERMISSION _new = new SE_USER_PERMISSION();
                _new.ID = Utilities.GetNextSequence(Context, Context.SE_USER_PERMISSION.EntitySet.Name);
                _new.ALLOW_CREATE = _lstUserFunc[i].ALLOW_CREATE;
                _new.ALLOW_MODIFY = _lstUserFunc[i].ALLOW_MODIFY;
                _new.ALLOW_DELETE = _lstUserFunc[i].ALLOW_DELETE;
                _new.ALLOW_PRINT = _lstUserFunc[i].ALLOW_PRINT;
                _new.ALLOW_IMPORT = _lstUserFunc[i].ALLOW_IMPORT;
                _new.ALLOW_EXPORT = _lstUserFunc[i].ALLOW_EXPORT;
                _new.ALLOW_SPECIAL1 = _lstUserFunc[i].ALLOW_SPECIAL1;
                _new.ALLOW_SPECIAL2 = _lstUserFunc[i].ALLOW_SPECIAL2;
                _new.ALLOW_SPECIAL3 = _lstUserFunc[i].ALLOW_SPECIAL3;
                _new.ALLOW_SPECIAL4 = _lstUserFunc[i].ALLOW_SPECIAL4;
                _new.ALLOW_SPECIAL5 = _lstUserFunc[i].ALLOW_SPECIAL5;
                _new.FUNCTION_ID = _lstUserFunc[i].FUNCTION_ID;
                _new.USER_ID = _lstUserFunc[i].USER_ID;
                Context.SE_USER_PERMISSION.AddObject(_new);
            }
            Context.SaveChanges(log);
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool UpdateUserFunction(List<UserFunctionDTO> _lstUserFunc, UserLog log)
    {
        try
        {
            int i;
            List<decimal> lstID = (from p in _lstUserFunc
                                   select p.ID).ToList();
            List<SE_USER_PERMISSION> objUpdate = (from p in Context.SE_USER_PERMISSION
                                                  where lstID.Contains(p.ID)
                                                  select p).ToList;
            for (i = 0; i <= objUpdate.Count - 1; i++)
            {
                UserFunctionDTO func = _lstUserFunc.Find(UserFunctionDTO item => item.ID == objUpdate[i].ID);
                if (func != null)
                {
                    objUpdate[i].ALLOW_CREATE = func.ALLOW_CREATE ? 1 : 0;
                    objUpdate[i].ALLOW_DELETE = func.ALLOW_DELETE ? 1 : 0;
                    objUpdate[i].ALLOW_EXPORT = func.ALLOW_EXPORT ? 1 : 0;
                    objUpdate[i].ALLOW_IMPORT = func.ALLOW_IMPORT ? 1 : 0;
                    objUpdate[i].ALLOW_MODIFY = func.ALLOW_MODIFY ? 1 : 0;
                    objUpdate[i].ALLOW_PRINT = func.ALLOW_PRINT ? 1 : 0;
                    objUpdate[i].ALLOW_SPECIAL1 = func.ALLOW_SPECIAL1 ? 1 : 0;
                    objUpdate[i].ALLOW_SPECIAL2 = func.ALLOW_SPECIAL2 ? 1 : 0;
                    objUpdate[i].ALLOW_SPECIAL3 = func.ALLOW_SPECIAL3 ? 1 : 0;
                    objUpdate[i].ALLOW_SPECIAL4 = func.ALLOW_SPECIAL4 ? 1 : 0;
                    objUpdate[i].ALLOW_SPECIAL5 = func.ALLOW_SPECIAL5 ? 1 : 0;
                }
            }
            Context.SaveChanges(log);
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool DeleteUserFunction(List<decimal> _lstID)
    {
        try
        {
            using (ConnectionManager conMng = new ConnectionManager())
            {
                using (OracleConnection conn = new OracleConnection(conMng.GetConnectionString()))
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        try
                        {
                            cmd.Connection = conn;
                            cmd.Transaction = cmd.Connection.BeginTransaction();
                            for (int i = 0; i <= _lstID.Count - 1; i++)
                            {
                                cmd.CommandText = "DELETE SE_USER_PERMISSION WHERE ID =" + _lstID[i];
                                cmd.ExecuteNonQuery();
                            }
                            cmd.Transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            cmd.Transaction.Rollback();
                        }
                        finally
                        {
                            // Dispose all resource
                            cmd.Dispose();
                            conn.Close();
                            conn.Dispose();
                        }
                    }
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public bool CopyUserFunction(decimal UserCopyID, decimal UserID, UserLog log)
    {
        // bổ sung delete phục vụ cho chức năng copy function
        var lstDel = (from p in Context.SE_USER_PERMISSION
                      where p.USER_ID == UserID
                      select p).ToList;
        foreach (var itm in lstDel)
            Context.SE_USER_PERMISSION.DeleteObject(itm);
        Context.SaveChanges(log);

        var _lstUserFunc = (from p in Context.SE_USER_PERMISSION
                            where p.USER_ID == UserCopyID
                            select new UserFunctionDTO() { USER_ID = UserID, FUNCTION_ID = p.FUNCTION_ID, ALLOW_CREATE = p.ALLOW_CREATE, ALLOW_DELETE = p.ALLOW_DELETE, ALLOW_EXPORT = p.ALLOW_EXPORT, ALLOW_IMPORT = p.ALLOW_IMPORT, ALLOW_MODIFY = p.ALLOW_MODIFY, ALLOW_PRINT = p.ALLOW_PRINT, ALLOW_SPECIAL1 = p.ALLOW_SPECIAL1, ALLOW_SPECIAL2 = p.ALLOW_SPECIAL2, ALLOW_SPECIAL3 = p.ALLOW_SPECIAL3, ALLOW_SPECIAL4 = p.ALLOW_SPECIAL4, ALLOW_SPECIAL5 = p.ALLOW_SPECIAL5 }).ToList;
        for (int i = 0; i <= _lstUserFunc.Count - 1; i++)
        {
            var itemAdd = _lstUserFunc(i);

            var functionCheck = Context.SE_USER_PERMISSION.FirstOrDefault(p => p.FUNCTION_ID == itemAdd.FUNCTION_ID && p.USER_ID == itemAdd.USER_ID);

            if (functionCheck != null)
                continue;

            SE_USER_PERMISSION _new = new SE_USER_PERMISSION();
            _new.ID = Utilities.GetNextSequence(Context, Context.SE_USER_PERMISSION.EntitySet.Name);
            _new.ALLOW_CREATE = _lstUserFunc(i).ALLOW_CREATE;
            _new.ALLOW_MODIFY = _lstUserFunc(i).ALLOW_MODIFY;
            _new.ALLOW_DELETE = _lstUserFunc(i).ALLOW_DELETE;
            _new.ALLOW_PRINT = _lstUserFunc(i).ALLOW_PRINT;
            _new.ALLOW_IMPORT = _lstUserFunc(i).ALLOW_IMPORT;
            _new.ALLOW_EXPORT = _lstUserFunc(i).ALLOW_EXPORT;
            _new.ALLOW_SPECIAL1 = _lstUserFunc(i).ALLOW_SPECIAL1;
            _new.ALLOW_SPECIAL2 = _lstUserFunc(i).ALLOW_SPECIAL2;
            _new.ALLOW_SPECIAL3 = _lstUserFunc(i).ALLOW_SPECIAL3;
            _new.ALLOW_SPECIAL4 = _lstUserFunc(i).ALLOW_SPECIAL4;
            _new.ALLOW_SPECIAL5 = _lstUserFunc(i).ALLOW_SPECIAL5;
            _new.FUNCTION_ID = _lstUserFunc(i).FUNCTION_ID;
            _new.USER_ID = _lstUserFunc(i).USER_ID;
            Context.SE_USER_PERMISSION.AddObject(_new);
            Context.SaveChanges(log);
        }
        return true;
    }



    public List<decimal> GetUserOrganization(decimal _UserID)
    {
        ;/* Cannot convert ReturnStatementSyntax, System.InvalidOperationException: Sequence contains more than one element
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.MemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.MemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitReturnStatement(ReturnStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ReturnStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 
        Return (From p In Context.SE_USER_ORG_ACCESS
                From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID)
                Where p.USER_ID = _UserID
                Select p.ORG_ID).ToList

 */
    }

    public void DeleteUserOrganization(decimal _UserId)
    {
        try
        {
            using (DataAccess.NonQueryData cls = new DataAccess.NonQueryData())
            {
                cls.ExecuteSQL("DELETE SE_USER_ORG_ACCESS WHERE USER_ID =" + _UserId);
            }
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool UpdateUserOrganization(List<UserOrgAccessDTO> _lstOrg)
    {
        if (_lstOrg.Count > 0)
        {
            int i;
            for (i = 0; i <= _lstOrg.Count - 1; i++)
            {
                SE_USER_ORG_ACCESS _item = new SE_USER_ORG_ACCESS();
                _item.ID = Utilities.GetNextSequence(Context, Context.SE_USER_ORG_ACCESS.EntitySet.Name);
                _item.USER_ID = _lstOrg[i].USER_ID;
                _item.ORG_ID = _lstOrg[i].ORG_ID;
                Context.SE_USER_ORG_ACCESS.AddObject(_item);
            }
            Context.SaveChanges();
            return true;
        }
        else
            return false;
    }



    public List<UserReportDTO> GetUserReportList(decimal _UserID)
    {
        var query = (from p in Context.SE_REPORT
                     orderby p.NAME
                     select new UserReportDTO() { ID = p.ID, REPORT_NAME = p.NAME, MODULE_NAME = p.SE_MODULE.NAME, IS_USE = ((from n in p.SE_USER where n.ID == _UserID select n).Count > 0) });
        return query.ToList;
    }

    public List<UserReportDTO> GetUserReportListFilter(decimal _UserID, UserReportDTO _filter)
    {
        var query = (from p in Context.SE_REPORT
                     orderby p.NAME
                     where (_filter.REPORT_NAME == null/* TODO Change to default(_) if this is not a reference type */ | p.NAME.ToUpper == _filter.REPORT_NAME.ToUpper) & (_filter.MODULE_ID == null/* TODO Change to default(_) if this is not a reference type */ | p.SE_MODULE.ID == _filter.MODULE_ID)
                     select new UserReportDTO() { ID = p.ID, REPORT_NAME = p.NAME, MODULE_NAME = p.SE_MODULE.NAME, IS_USE = ((from n in p.SE_USER where n.ID == _UserID select n).Count > 0) });
        return query.ToList;
    }

    public bool UpdateUserReport(decimal _UserID, List<UserReportDTO> _lstReport)
    {
        SE_USER query;
        query = (from p in Context.SE_USER
                 where p.ID == _UserID
                 select p).FirstOrDefault;

        List<decimal> Ids = (from p in _lstReport
                             select p.ID).ToList();
        var lst = (from p in Context.SE_REPORT
                   where Ids.Contains(p.ID)
                   select p).ToList;
        if (query != null)
        {
            query.SE_REPORT.Clear();
            for (int i = 0; i <= lst.Count - 1; i++)
                query.SE_REPORT.Add(lst(i));
            Context.SaveChanges();
        }
        else
            return false;
        return true;
    }



    public List<AccessLog> GetAccessLog(AccessLogFilter filter, int PageIndex, int PageSize, ref int Total, string Sorts = "LoginDate desc")
    {
        try
        {
            return AuditLogHelper.GetAccessLog(filter, PageIndex, PageSize, Total, Sorts);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool InsertAccessLog(AccessLog _accesslog)
    {
        try
        {
            return AuditLogHelper.InsertAccessLog(_accesslog);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public List<ActionLog> GetActionLog(ActionLogFilter filter, int PageIndex, int PageSize, ref int Total, string Sorts = "ActionDate desc")
    {
        try
        {
            return (AuditLogHelper.GetActionLog(filter, PageIndex, PageSize, Total, Sorts));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<ActionLog> GetActionLog(decimal objectId)
    {
        try
        {
            return AuditLogHelper.GetActionLog(objectId);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<AuditLogDtl> GetActionLogByID(decimal gID)
    {
        try
        {
            return AuditLogHelper.GetActionLogByID(gID);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int DeleteActionLogs(List<decimal> lstDeleteIds)
    {
        try
        {
            return AuditLogHelper.DeleteActionLogs(lstDeleteIds);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private Entity.DbContext mydbcontext = new Entity.DbContext(Context, true);



    public bool IsUsernameExist(string Username)
    {
        SE_USER u = (from p in Context.SE_USER
                     where p.USERNAME.ToUpper.Equals(Username.ToUpper())
                     select p).FirstOrDefault;
        if (u != null)
            return true;
        return false;
    }

    public List<PermissionDTO> GetUserPermissions(string Username)
    {
        try
        {
            if (Username == "")
                return new List<PermissionDTO>();
            ;/* Cannot convert LocalDeclarationStatementSyntax, System.InvalidOperationException: Sequence contains more than one element
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertInitializer(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.SplitVariableDeclarations(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.LocalDeclarationStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 

            Dim query1 = From p In Context.SE_USER_PERMISSION
                         From func In Context.SE_FUNCTION.Where(Function(f) f.ID = p.FUNCTION_ID)
                         From user In Context.SE_USER.Where(Function(f) f.ID = p.USER_ID)
                         Where user.USERNAME.ToUpper = Username.ToUpper
                         Select New PermissionDTO With {.ID = p.ID,
                                                        .FunctionID = p.FUNCTION_ID,
                                                        .GroupID = 0,
                                                        .FID = func.FID,
                                                        .MID = func.SE_MODULE.MID,
                                                        .AllowCreate = p.ALLOW_CREATE,
                                                        .AllowModify = p.ALLOW_MODIFY,
                                                        .AllowDelete = p.ALLOW_DELETE,
                                                        .AllowImport = p.ALLOW_IMPORT,
                                                        .AllowExport = p.ALLOW_EXPORT,
                                                        .AllowPrint = p.ALLOW_PRINT,
                                                        .AllowSpecial1 = p.ALLOW_SPECIAL1,
                                                        .AllowSpecial2 = p.ALLOW_SPECIAL2,
                                                        .AllowSpecial3 = p.ALLOW_SPECIAL3,
                                                        .AllowSpecial4 = p.ALLOW_SPECIAL4,
                                                        .AllowSpecial5 = p.ALLOW_SPECIAL5,
                                                        .IS_REPORT = False}

 */
            ;/* Cannot convert LocalDeclarationStatementSyntax, System.InvalidOperationException: Sequence contains more than one element
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertInitializer(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.SplitVariableDeclarations(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.LocalDeclarationStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 

            Dim query2 = From p In Context.SE_REPORT
                         From n In p.SE_USER
                         Where n.USERNAME.ToUpper = Username.ToUpper
                         Select New PermissionDTO With {.ID = p.ID,
                                                        .FunctionID = p.ID,
                                                        .GroupID = 0,
                                                        .FID = p.CODE,
                                                        .MID = p.SE_MODULE.MID,
                                                        .AllowCreate = False,
                                                        .AllowModify = False,
                                                        .AllowDelete = False,
                                                        .AllowImport = False,
                                                        .AllowExport = False,
                                                        .AllowPrint = False,
                                                        .AllowSpecial1 = False,
                                                        .AllowSpecial2 = False,
                                                        .AllowSpecial3 = False,
                                                        .AllowSpecial4 = False,
                                                        .AllowSpecial5 = False,
                                                        .IS_REPORT = True}

 */

            List<PermissionDTO> lstPermissions = query1.Union(query2).ToList;

            return lstPermissions;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool CheckUserAdmin(string Username)
    {
        try
        {
            if (Username == "")
                return false;
            var u = (from p in Context.SE_USER
                     where p.USERNAME.ToUpper == Username.ToUpper()
                     select p).FirstOrDefault;

            if (u == null)
                return false;

            if (u.MODULE_ADMIN == "")
                return false;
            else
                return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public UserDTO GetUserWithPermision(string Username)
    {
        try
        {
            if (Username == "")
                return new UserDTO();
            var query = (from p in Context.SE_USER
                         where p.USERNAME.ToUpper == Username.ToUpper()
                         orderby p.EMPLOYEE_CODE descending
                         select p);
            var objUser = query.FirstOrDefault;
            if (objUser != null)
            {
                bool isUserPermission = true;
                if (objUser.MODULE_ADMIN == null)
                {
                    if ((from p in Context.SE_USER_ORG_ACCESS
                         where p.USER_ID == objUser.ID
                         select p).Count == 0)
                        isUserPermission = false;

                    if ((from p in Context.SE_USER_PERMISSION
                         where p.USER_ID == objUser.ID
                         select p).Count == 0)
                        isUserPermission = false;
                };/* Cannot convert ReturnStatementSyntax, System.InvalidOperationException: Stack empty.
   at System.ThrowHelper.ThrowInvalidOperationException(ExceptionResource resource)
   at System.Collections.Generic.Stack`1.Peek()
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.MemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.MemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitBinaryExpression(BinaryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.BinaryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitBinaryExpression(BinaryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.BinaryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.ConvertWhereClause(WhereClauseSyntax ws)
   at ICSharpCode.CodeConverter.Util.ObjectExtensions.TypeSwitch[TBaseType,TDerivedType1,TDerivedType2,TDerivedType3,TDerivedType4,TResult](TBaseType obj, Func`2 matchFunc1, Func`2 matchFunc2, Func`2 matchFunc3, Func`2 matchFunc4, Func`2 defaultFunc)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.ConvertQueryBodyClause(QueryClauseSyntax node)
   at System.Linq.Enumerable.WhereSelectEnumerableIterator`2.MoveNext()
   at Microsoft.CodeAnalysis.SyntaxList`1.CreateNode(IEnumerable`1 nodes)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.MemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.MemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitNamedFieldInitializer(NamedFieldInitializerSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.NamedFieldInitializerSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitNamedFieldInitializer(NamedFieldInitializerSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.NamedFieldInitializerSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.<VisitObjectMemberInitializer>b__89_0(FieldInitializerSyntax initializer)
   at System.Linq.Enumerable.WhereSelectEnumerableIterator`2.MoveNext()
   at System.Linq.Enumerable.<CastIterator>d__97`1.MoveNext()
   at Microsoft.CodeAnalysis.CSharp.SyntaxFactory.SeparatedList[TNode](IEnumerable`1 nodes)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitObjectMemberInitializer(ObjectMemberInitializerSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ObjectMemberInitializerSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitObjectMemberInitializer(ObjectMemberInitializerSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ObjectMemberInitializerSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ObjectCreationExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ObjectCreationExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitReturnStatement(ReturnStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ReturnStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 

                Return New UserDTO With {.ID = objUser.ID,
                                         .EMAIL = objUser.EMAIL,
                                         .TELEPHONE = objUser.TELEPHONE,
                                         .USERNAME = objUser.USERNAME,
                                         .FULLNAME = objUser.FULLNAME,
                                         .PASSWORD = objUser.PASSWORD,
                                         .IS_APP = objUser.IS_APP,
                                         .IS_PORTAL = objUser.IS_PORTAL,
                                         .IS_AD = objUser.IS_AD,
                                         .EMPLOYEE_CODE = objUser.EMPLOYEE_CODE,
                                         .ACTFLG = objUser.ACTFLG,
                                         .IS_CHANGE_PASS = objUser.IS_CHANGE_PASS,
                                         .MODULE_ADMIN = objUser.MODULE_ADMIN,
                                         .EMPLOYEE_ID = (From p In Context.HU_EMPLOYEE
                                                         Where p.EMPLOYEE_CODE = .EMPLOYEE_CODE
                                                         Order By p.ID Descending
                                                         Select p.ID).FirstOrDefault,
                                         .EFFECT_DATE = objUser.EFFECT_DATE,
                                         .EXPIRE_DATE = objUser.EXPIRE_DATE,
                                         .IS_USER_PERMISSION = isUserPermission}

 */
            }
            return null/* TODO Change to default(_) if this is not a reference type */;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool ChangeUserPassword(string Username, string _oldpass, string _newpass, UserLog log)
    {
        try
        {
            SE_USER query;
            query = (from p in Context.SE_USER
                     where p.USERNAME.ToUpper == Username.ToUpper()
                     select p).FirstOrDefault;
            if (query != null)
            {
                using (EncryptData EncryptData = new EncryptData())
                {
                    if (EncryptData.DecryptString(query.PASSWORD) == _oldpass)
                    {
                        query.PASSWORD = EncryptData.EncryptString(_newpass);
                        query.IS_CHANGE_PASS = -1;
                        query.CHANGE_PASS_DATE = DateTime.Now.Date;
                        Context.SaveChanges(log);
                    }
                    else
                        return false;
                }
            }
            else
                return false;
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string GetPassword(string Username)
    {
        try
        {
            SE_USER query;
            query = (from p in Context.SE_USER
                     where p.USERNAME.ToUpper == Username.ToUpper()
                     select p).FirstOrDefault;
            return query.PASSWORD;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool UpdateUserStatus(string Username, string _ACTFLG, UserLog log)
    {
        try
        {
            SE_USER query;
            query = (from p in Context.SE_USER
                     where p.USERNAME.ToUpper == Username.ToUpper()
                     select p).FirstOrDefault;

            if (query != null)
            {
                query.ACTFLG = _ACTFLG;
                Context.SaveChanges(log);
            }
            else
                return false;
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public List<OrganizationDTO> GetOrganizationAll()
    {
        List<OrganizationDTO> lstOrgs = new List<OrganizationDTO>();
        lstOrgs = (from p in Context.HU_ORGANIZATION
                   orderby p.ORD_NO, p.CODE, p.NAME_VN
                   select new OrganizationDTO() { ID = p.ID, CODE = p.CODE, NAME_VN = p.NAME_VN, NAME_EN = p.NAME_EN, PARENT_ID = p.PARENT_ID, PARENT_NAME = p.PARENT.NAME_VN, ORD_NO = p.ORD_NO, ACTFLG = p.ACTFLG, DISSOLVE_DATE = p.DISSOLVE_DATE, DESCRIPTION_PATH = p.DESCRIPTION_PATH, FOUNDATION_DATE = p.FOUNDATION_DATE }).ToList;
        return lstOrgs;
    }
    // so do to chuc Location da duoc phan quyen
    public List<OrganizationDTO> GetOrganizationLocationTreeView(string _username)
    {
        List<OrganizationDTO> lstOrgs = new List<OrganizationDTO>();
        SE_USER u = (from p in Context.SE_USER
                     where p.USERNAME.ToUpper == _username.ToUpper()
                     select p).FirstOrDefault;
        if (u != null)
        {
            var query1 = (from p in u.SE_GROUPS
                          where System.Convert.ToBoolean(p.IS_ADMIN) == true
                          select p.ID).FirstOrDefault;
            if (query1 == null/* TODO Change to default(_) if this is not a reference type */ )
            {
                List<decimal> lstGroupIds = (from p in u.SE_GROUPS
                                             select p.ID).ToList;

                var query = (from org in Context.HU_ORGANIZATION
                             where (from user in Context.SE_USER_ORG_ACCESS
                                    where user.USER_ID == u.ID
                                    select user.ORG_ID).Contains(org.ID)
                             select new OrganizationDTO() { ID = org.ID, CODE = org.CODE, NAME_VN = org.NAME_VN, NAME_EN = org.NAME_EN, PARENT_ID = org.PARENT_ID, PARENT_NAME = org.PARENT.NAME_VN, ORD_NO = org.ORD_NO, ACTFLG = org.ACTFLG, DISSOLVE_DATE = org.DISSOLVE_DATE, HIERARCHICAL_PATH = org.HIERARCHICAL_PATH, DESCRIPTION_PATH = org.DESCRIPTION_PATH, FOUNDATION_DATE = org.FOUNDATION_DATE }).Distinct;

                lstOrgs = query.ToList;

                if (lstOrgs.Count > 0 && (from p in lstOrgs
                                          where p.HIERARCHICAL_PATH == "1"
                                          select p).Count() == 0)
                {
                    var lstOrgPer = lstOrgs.Select(f => f.ID).ToList;
                    List<decimal> lstOrgID = new List<decimal>();
                    foreach (var org in lstOrgs)
                    {
                        if (org.HIERARCHICAL_PATH != "")
                        {
                            if (org.HIERARCHICAL_PATH.Split(";").Length > 1)
                            {
                                for (int i = 0; i <= org.HIERARCHICAL_PATH.Split(";").Length - 2; i++)
                                {
                                    var str = org.HIERARCHICAL_PATH.Split(";")(i);
                                    if (str != "")
                                    {
                                        var orgid = decimal.Parse(str);
                                        if (!lstOrgPer.Contains(orgid) & !lstOrgID.Contains(orgid))
                                            lstOrgID.Add(orgid);
                                    }
                                }
                            }
                        }
                    }

                    if (lstOrgID.Count > 0)
                    {
                        var lstOrgNotPer = (from p in Context.HU_ORGANIZATION
                                            where lstOrgID.Contains(p.ID)
                                            select new OrganizationDTO() { ID = p.ID, CODE = p.CODE, NAME_VN = p.NAME_VN, NAME_EN = p.NAME_EN, PARENT_ID = p.PARENT_ID, PARENT_NAME = p.PARENT.NAME_VN, ORD_NO = p.ORD_NO, ACTFLG = p.ACTFLG, DISSOLVE_DATE = p.DISSOLVE_DATE, FOUNDATION_DATE = p.FOUNDATION_DATE, DESCRIPTION_PATH = p.DESCRIPTION_PATH, IS_NOT_PER = true }).ToList;

                        lstOrgs.AddRange(lstOrgNotPer);
                    }
                }

                lstOrgs = (from p in lstOrgs
                           orderby p.ORD_NO, p.CODE, p.NAME_VN
                           select p).ToList();
            }
            else
                lstOrgs = (from p in Context.HU_ORGANIZATION
                           orderby p.ORD_NO, p.CODE, p.NAME_VN
                           select new OrganizationDTO() { ID = p.ID, CODE = p.CODE, NAME_VN = p.NAME_VN, NAME_EN = p.NAME_EN, PARENT_ID = p.PARENT_ID, PARENT_NAME = p.PARENT.NAME_VN, ORD_NO = p.ORD_NO, ACTFLG = p.ACTFLG, DISSOLVE_DATE = p.DISSOLVE_DATE, DESCRIPTION_PATH = p.DESCRIPTION_PATH, FOUNDATION_DATE = p.FOUNDATION_DATE }).ToList;
        }
        return lstOrgs;
    }

    public OrganizationDTO GetOrganizationLocationInfo(decimal _orgId)
    {
        OrganizationDTO query;
        query = (from p in Context.HU_ORGANIZATION
                 where p.ID == _orgId
                 select new OrganizationDTO() { ACTFLG = p.ACTFLG, CODE = p.CODE, DISSOLVE_DATE = p.DISSOLVE_DATE, FOUNDATION_DATE = p.FOUNDATION_DATE, ID = p.ID, NAME_EN = p.NAME_EN, NAME_VN = p.NAME_VN, PARENT_ID = p.PARENT_ID, PARENT_NAME = p.PARENT.NAME_VN, REMARK = p.REMARK }).FirstOrDefault;
        return query;
    }

    public List<OrganizationStructureDTO> GetOrganizationStructureInfo(decimal _orgId)
    {
        OrganizationStructureDTO query = new OrganizationStructureDTO();
        List<OrganizationStructureDTO> list = new List<OrganizationStructureDTO>();
        query.PARENT_ID = _orgId;

        while (query.PARENT_ID != null)
        {
            query = (from p in Context.HU_ORGANIZATION
                     where p.ID == query.PARENT_ID
                     select new OrganizationStructureDTO() { ID = p.ID, CODE = p.CODE, NAME_VN = p.NAME_VN, NAME_EN = p.NAME_EN, PARENT_ID = p.PARENT_ID }).FirstOrDefault;
            list.Add(query);
        }
        return list;
    }



    public List<EmployeePopupFindListDTO> GetEmployeeToPopupFind(EmployeePopupFindListDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "EMPLOYEE_CODE asc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */, ParamDTO _param = null/* TODO Change to default(_) if this is not a reference type */)
    {
        try
        {
            string userName;
            using (DataAccess.QueryData cls = new DataAccess.QueryData())
            {
                userName = log.Username.ToUpper;
                if (_filter.LoadAllOrganization)
                    userName = "ADMIN";
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG", new { P_USERNAME = userName, P_ORGID = _param.ORG_ID, P_ISDISSOLVE = _param.IS_DISSOLVE });
            };/* Cannot convert LocalDeclarationStatementSyntax, System.InvalidOperationException: Sequence contains more than one element
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertInitializer(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.SplitVariableDeclarations(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.LocalDeclarationStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 


            Dim query = From p In Context.HU_EMPLOYEE
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.GENDER And f.TYPE_ID = 34).DefaultIfEmpty
                        From work_status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.WORK_STATUS And f.TYPE_ID = 59).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = userName)

 */
            if (_filter.EMPLOYEE_CODE != "")
                query = query.Where(f => f.p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper) | f.p.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper));

            if (_filter.MustHaveContract)
                query = query.Where(f => f.p.CONTRACT_ID.HasValue);
            var dateNow = DateTime.Now.Date;
            if (!_filter.IsOnlyWorkingWithoutTer)
            {
                if (_filter.IS_TER)
                    query = query.Where(f => f.p.WORK_STATUS == 257 & f.p.TER_EFFECT_DATE <= dateNow);
                else
                    query = query.Where(f => f.p.WORK_STATUS == null | (f.p.WORK_STATUS != null & (f.p.WORK_STATUS != 257 | (f.p.WORK_STATUS == 257 & f.p.TER_EFFECT_DATE > dateNow))));
            }
            else
                query = query.Where(f => f.p.WORK_STATUS == null | (f.p.WORK_STATUS != null & f.p.WORK_STATUS != 257));
            switch (_filter.IS_3B)
            {
                case 1:
                    {
                        query = query.Where(f => f.p.IS_3B == true);
                        break;
                    }

                case 2:
                    {
                        query = query.Where(f => f.p.IS_3B == false);
                        break;
                    }
            }

            var lst = query.Select(f => new EmployeePopupFindListDTO() { EMPLOYEE_CODE = f.p.EMPLOYEE_CODE, ID = f.p.ID, EMPLOYEE_ID = f.p.ID, FULLNAME_VN = f.p.FULLNAME_VN, FULLNAME_EN = f.p.FULLNAME_EN, JOIN_DATE = f.p.JOIN_DATE, ORG_NAME = f.o.NAME_VN, ORG_CODE = f.o.CODE, ORG_DESC = f.o.DESCRIPTION_PATH, GENDER = f.gender.NAME_VN, WORK_STATUS = f.work_status.NAME_VN, TITLE_NAME = f.t.NAME_VN });

            lst = lst.OrderBy(Sorts);
            Total = lst.Count;
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize);
            return lst.ToList;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<EmployeePopupFindDTO> GetEmployeeToPopupFind_EmployeeID(List<decimal> _empId)
    {
        ;/* Cannot convert LocalDeclarationStatementSyntax, System.InvalidOperationException: Sequence contains more than one element
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertInitializer(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.SplitVariableDeclarations(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.LocalDeclarationStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 
        Dim result = (From p In Context.HU_EMPLOYEE
                      From cv In Context.HU_EMPLOYEE_CV.Where(Function(cv) cv.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                      From s In Context.HU_STAFF_RANK.Where(Function(s) s.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                      Order By p.EMPLOYEE_CODE
                      Where _empId.Contains(p.ID)
                      Select New EmployeePopupFindDTO With {
                          .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                          .ID = p.ID,
                          .EMPLOYEE_ID = p.ID,
                          .JOIN_DATE = p.JOIN_DATE,
                          .FULLNAME_VN = p.FULLNAME_VN,
                          .ORG_ID = p.ORG_ID,
                          .ORG_CODE = p.HU_ORGANIZATION.CODE,
                          .ORG_NAME = p.HU_ORGANIZATION.NAME_VN,
                          .ORG_DESC = p.HU_ORGANIZATION.DESCRIPTION_PATH,
                          .ORG_HIER = p.HU_ORGANIZATION.HIERARCHICAL_PATH,
                          .TITLE_ID = p.TITLE_ID,
                          .TITLE_NAME = p.HU_TITLE.NAME_VN,
                          .BIRTH_DATE = cv.BIRTH_DATE,
                          .BIRTH_PLACE = cv.BIRTH_PLACE,
                          .STAFF_RANK_ID = p.STAFF_RANK_ID,
                          .STAFF_RANK_NAME = s.NAME})

 */
        return result.ToList;
    }



    public List<StudentPopupFindListDTO> GetStudentToPopupFind(StudentPopupFindListDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "Student_CODE asc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */, ParamDTO _param = null/* TODO Change to default(_) if this is not a reference type */)
    {
        try
        {
            string userName;
            using (DataAccess.QueryData cls = new DataAccess.QueryData())
            {
                userName = log.Username.ToUpper;
                if (_filter.LoadAllOrganization)
                    userName = "ADMIN";
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG", new { P_USERNAME = userName, P_ORGID = _param.ORG_ID, P_ISDISSOLVE = _param.IS_DISSOLVE });
            };/* Cannot convert LocalDeclarationStatementSyntax, System.InvalidOperationException: Sequence contains more than one element
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertInitializer(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.SplitVariableDeclarations(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.LocalDeclarationStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 


            Dim query = From p In Context.HU_STUDENT
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.GENDER And f.TYPE_ID = 34).DefaultIfEmpty
                        From work_status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.WORK_STATUS And f.TYPE_ID = 59).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = userName)

 */
            if (_filter.Student_CODE != "")
                query = query.Where(f => f.p.Student_CODE.ToUpper.Contains(_filter.Student_CODE.ToUpper) | f.p.FULLNAME_VN.ToUpper.Contains(_filter.Student_CODE.ToUpper));

            var dateNow = DateTime.Now.Date;
            if (!_filter.IsOnlyWorkingWithoutTer)
            {
                if (_filter.IS_TER)
                    query = query.Where(f => f.p.WORK_STATUS == 257 & f.p.TER_EFFECT_DATE <= dateNow);
                else
                    query = query.Where(f => f.p.WORK_STATUS == null | (f.p.WORK_STATUS != null & (f.p.WORK_STATUS != 257 | (f.p.WORK_STATUS == 257 & f.p.TER_EFFECT_DATE > dateNow))));
            }
            else
                query = query.Where(f => f.p.WORK_STATUS == null | (f.p.WORK_STATUS != null & f.p.WORK_STATUS != 257));

            var lst = query.Select(f => new StudentPopupFindListDTO() { ID = f.p.ID, STUDENT_ID = f.p.ID, STUDENT_CODE = f.p.STUDENT_CODE, FULLNAME_VN = f.p.FULLNAME_VN, JOIN_DATE = f.p.JOIN_DATE, ORG_NAME = f.o.NAME_VN, ORG_CODE = f.o.CODE, ORG_DESC = f.o.DESCRIPTION_PATH, GENDER = f.gender.NAME_VN, WORK_STATUS = f.work_status.NAME_VN });

            lst = lst.OrderBy(Sorts);
            Total = lst.Count;
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize);
            return lst.ToList;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<StudentPopupFindDTO> GetStudentToPopupFind_StudentID(List<decimal> _empId)
    {
        ;/* Cannot convert LocalDeclarationStatementSyntax, System.InvalidOperationException: Sequence contains more than one element
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertInitializer(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.SplitVariableDeclarations(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.LocalDeclarationStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 
        Dim result = (From p In Context.HU_STUDENT
                      From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID)
                      Order By p.STUDENT_CODE
                      Where _empId.Contains(p.ID)
                      Select New StudentPopupFindDTO With {
                          .STUDENT_CODE = p.STUDENT_CODE,
                          .ID = p.ID,
                          .STUDENT_ID = p.ID,
                          .JOIN_DATE = p.JOIN_DATE,
                          .FULLNAME_VN = p.FULLNAME_VN,
                          .ORG_ID = p.ORG_ID,
                          .ORG_CODE = o.CODE,
                          .ORG_NAME = o.NAME_VN,
                          .ORG_DESC = o.DESCRIPTION_PATH,
                          .ORG_HIER = o.HIERARCHICAL_PATH,
                          .BIRTH_DATE = p.BIRTH_DATE,
                          .BIRTH_PLACE = p.BIRTH_PLACE})

 */
        return result.ToList;
    }



    public List<TitleDTO> GetTitle(TitleDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
    {
        try
        {
            ;/* Cannot convert LocalDeclarationStatementSyntax, System.InvalidOperationException: Sequence contains more than one element
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertInitializer(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.SplitVariableDeclarations(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.LocalDeclarationStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 
            Dim query = From p In Context.HU_TITLE
                        From group In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TITLE_GROUP_ID).DefaultIfEmpty
                        Where p.ACTFLG = "A"

 */
            var lst = query.Select(p => new TitleDTO() { ID = p.p.ID, CODE = p.p.CODE, NAME_EN = p.p.NAME_EN, NAME_VN = p.p.NAME_VN, REMARK = p.p.REMARK, TITLE_GROUP_ID = p.p.TITLE_GROUP_ID, TITLE_GROUP_NAME = p.group.NAME_VN, CREATED_DATE = p.p.CREATED_DATE, ACTFLG = p.p.ACTFLG });

            if (_filter.CODE != "")
                lst = lst.Where(p => p.CODE.ToUpper.Contains(_filter.CODE.ToUpper));
            if (_filter.NAME_EN != "")
                lst = lst.Where(p => p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper));
            if (_filter.NAME_VN != "")
                lst = lst.Where(p => p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper));
            if (_filter.TITLE_GROUP_NAME != "")
                lst = lst.Where(p => p.TITLE_GROUP_NAME.ToUpper.Contains(_filter.TITLE_GROUP_NAME.ToUpper));
            if (_filter.REMARK != "")
                lst = lst.Where(p => p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper));

            lst = lst.OrderBy(Sorts);
            Total = lst.Count;
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize);

            return lst.ToList;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<TitleDTO> GetTitleFromId(List<decimal> _lstIds)
    {
        try
        {
            var query = (from p in Context.HU_TITLE
                         where _lstIds.Contains(p.ID)
                         select new TitleDTO() { ID = p.ID, CODE = p.CODE, NAME_EN = p.NAME_EN, NAME_VN = p.NAME_VN, ACTFLG = p.ACTFLG }).ToList;
            return query;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public List<AT_KITCHEN_DTO> GetKitchen(AT_KITCHEN_DTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
    {
        try
        {
            var query = from p in Context.AT_KITCHEN
                        select new AT_KITCHEN_DTO() { ID = p.ID, KITCHEN_CODE = p.KITCHEN_CODE, KITCHEN_NAME = p.KITCHEN_NAME, REMARK = p.REMARK, CREATED_DATE = p.CREATED_DATE };

            var lst = query;

            if (_filter.KITCHEN_CODE != "")
                lst = lst.Where(p => p.KITCHEN_CODE.ToUpper.Contains(_filter.KITCHEN_CODE.ToUpper));
            if (_filter.KITCHEN_NAME != "")
                lst = lst.Where(p => p.KITCHEN_NAME.ToUpper.Contains(_filter.KITCHEN_NAME.ToUpper));
            if (_filter.REMARK != "")
                lst = lst.Where(p => p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper));

            lst = lst.OrderBy(Sorts);
            Total = lst.Count;
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize);

            return lst.ToList;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<AT_KITCHEN_DTO> GetKitchenFromId(List<decimal> _lstIds)
    {
        try
        {
            var query = (from p in Context.AT_KITCHEN
                         where _lstIds.Contains(p.ID)
                         select new AT_KITCHEN_DTO() { ID = p.ID, KITCHEN_CODE = p.KITCHEN_CODE, KITCHEN_NAME = p.KITCHEN_NAME }).ToList;
            return query;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public Dictionary<string, string> GetConfig(ModuleID eModule)
    {
        using (SystemConfig cofig = new SystemConfig())
        {
            try
            {
                return cofig.GetConfig(eModule);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public bool UpdateConfig(Dictionary<string, string> _lstConfig, ModuleID eModule)
    {
        using (SystemConfig cofig = new SystemConfig())
        {
            try
            {
                return cofig.UpdateConfig(_lstConfig, eModule);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }



    public Dictionary<int, string> GetReminderConfig(string username)
    {
        using (SystemConfig cofig = new SystemConfig())
        {
            try
            {
                return cofig.GetReminderConfig(username);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

    public bool SetReminderConfig(string username, int type, string value)
    {
        using (SystemConfig cofig = new SystemConfig())
        {
            try
            {
                return cofig.SetReminderConfig(username, type, value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
