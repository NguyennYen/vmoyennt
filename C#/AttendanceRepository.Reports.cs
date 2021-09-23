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
using LinqKit;
using System.Data.Objects.DataClasses;
using System.Data.Common;
using System.Data.Entity;
using System.Threading;
using Framework.Data.System.Linq.Dynamic;
using System.Configuration;

public partial class AttendanceRepository
{
    public DataTable GET_REPORT()
    {
        using (DataAccess.QueryData cls = new DataAccess.QueryData())
        {
            DataTable dtData = cls.ExecuteStore("PKG_COMMON_LIST.GET_REPORT", new { P_LIKE = "AT", CUR = cls.OUT_CURSOR });
            return dtData;
        }
        return null/* TODO Change to default(_) if this is not a reference type */;
    }

    public List<Se_ReportDTO> GetReportById(Se_ReportDTO _filter, int PageIndex, int PageSize, ref int Total, UserLog log, string Sorts = "CODE ASC")
    {
        try
        {
            IQueryable<Se_ReportDTO> query;
            if (log.Username.ToUpper != "ADMIN" & log.Username.ToUpper != "SYS.ADMIN" & log.Username.ToUpper != "HR.ADMIN")
                ;/* Cannot convert AssignmentStatementSyntax, System.InvalidOperationException: Sequence contains more than one element
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitQueryExpression(QueryExpressionSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.QueryExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitAssignmentStatement(AssignmentStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.AssignmentStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 
                query = From u In Context.SE_USER
                        From p In u.SE_REPORT
                        Where u.USERNAME.ToUpper = log.Username.ToUpper And p.MODULE_ID = _filter.MODULE_ID
                        Select New Se_ReportDTO With {
                            .ID = p.ID,
                            .CODE = p.CODE,
                            .NAME = p.NAME,
                            .MODULE_ID = p.MODULE_ID}

 */
            else
                query = from p in Context.SE_REPORT
                        where p.MODULE_ID == _filter.MODULE_ID
                        select new Se_ReportDTO() { ID = p.ID, CODE = p.CODE, NAME = p.NAME, MODULE_ID = p.MODULE_ID };

            var lst = query;

            if (_filter.CODE != "")
                lst = lst.Where(p => p.CODE.ToUpper.Contains(_filter.CODE.ToUpper));
            if (_filter.NAME != "")
                lst = lst.Where(p => p.NAME.ToUpper.Contains(_filter.NAME.ToUpper));


            lst = lst.OrderBy(Sorts);
            Total = lst.Count;
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize);

            return lst.ToList;
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iProfile");
            throw ex;
        }
    }

    public DataTable GETORGNAME(ParamDTO obj, UserLog log)
    {
        using (DataAccess.QueryData cls = new DataAccess.QueryData())
        {
            DataTable dtData = cls.ExecuteStore("PKG_ATTENDANCE_REPORT.GETORGNAME", new { P_USERNAME = log.Username, P_ORG_ID = obj.S_ORG_ID, P_CUR = cls.OUT_CURSOR });
            return dtData;
        }
        return null/* TODO Change to default(_) if this is not a reference type */;
    }

    public DataSet GET_AT001(ParamDTO obj, UserLog log)
    {
        using (DataAccess.QueryData cls = new DataAccess.QueryData())
        {
            DataSet dtData = cls.ExecuteStore("PKG_ATTENDANCE_REPORT.AT001", new { P_USERNAME = log.Username.ToUpper, P_ORG_ID = obj.S_ORG_ID, P_PERIOD_ID = obj.PERIOD_ID, P_COL_PERIOD = cls.OUT_CURSOR, P_COL_MANUAL = cls.OUT_CURSOR, P_CUR_PERIOD = cls.OUT_CURSOR, P_CUR_MANUAL = cls.OUT_CURSOR }, false);
            return dtData;
        }
        return null/* TODO Change to default(_) if this is not a reference type */;
    }

    public DataSet GET_AT002(ParamDTO obj, UserLog log)
    {
        using (DataAccess.QueryData cls = new DataAccess.QueryData())
        {
            DataSet dtData = cls.ExecuteStore("PKG_ATTENDANCE_REPORT.AT002", new { P_USERNAME = log.Username.ToUpper, P_ORG_ID = obj.S_ORG_ID, P_PERIOD_ID = obj.PERIOD_ID, P_COL_PERIOD = cls.OUT_CURSOR, P_CUR_PERIOD = cls.OUT_CURSOR }, false);
            return dtData;
        }
        return null/* TODO Change to default(_) if this is not a reference type */;
    }

    public DataSet GET_AT003(ParamDTO obj, UserLog log)
    {
        using (DataAccess.QueryData cls = new DataAccess.QueryData())
        {
            DataSet dtData = cls.ExecuteStore("PKG_ATTENDANCE_REPORT.AT003", new { P_USERNAME = log.Username.ToUpper, P_YEAR = obj.YEAR, P_ORG_ID = obj.S_ORG_ID, P_LEAVE = obj.IS_FULL, P_CUR_DETAILS = cls.OUT_CURSOR, P_CUR1 = cls.OUT_CURSOR }, false);
            return dtData;
        }
        return null/* TODO Change to default(_) if this is not a reference type */;
    }

    public DataSet GET_AT004(ParamDTO obj, UserLog log)
    {
        using (DataAccess.QueryData cls = new DataAccess.QueryData())
        {
            DataSet dtData = cls.ExecuteStore("PKG_ATTENDANCE_REPORT.AT004", new { P_ORG = obj.S_ORG_ID, P_ISDISSOLVE = obj.IS_DISSOLVE, P_USERNAME = log.Username, P_PERIOD = obj.PERIOD_ID, P_CUR = cls.OUT_CURSOR, P_CUR1 = cls.OUT_CURSOR, P_CUR2 = cls.OUT_CURSOR }, false);
            return dtData;
        }
        return null/* TODO Change to default(_) if this is not a reference type */;
    }

    public DataSet GET_AT005(ParamDTO obj, UserLog log)
    {
        using (DataAccess.QueryData cls = new DataAccess.QueryData())
        {
            DataSet dtData = cls.ExecuteStore("PKG_ATTENDANCE_REPORT.AT005", new { P_USERNAME = log.Username.ToUpper, P_YEAR = obj.YEAR, P_ORG_ID = obj.S_ORG_ID, P_LEAVE = obj.IS_FULL, P_CUR_DETAILS = cls.OUT_CURSOR }, false);
            return dtData;
        }
        return null/* TODO Change to default(_) if this is not a reference type */;
    }

    public DataSet GET_AT006(ParamDTO obj, UserLog log)
    {
        using (DataAccess.QueryData cls = new DataAccess.QueryData())
        {
            DataSet dtData = cls.ExecuteStore("PKG_ATTENDANCE_REPORT.AT006", new { P_ORG = obj.S_ORG_ID, P_ISDISSOLVE = obj.IS_DISSOLVE, P_USERNAME = log.Username, P_PERIOD = obj.PERIOD_ID, P_CUR = cls.OUT_CURSOR, P_CUR1 = cls.OUT_CURSOR, P_CUR2 = cls.OUT_CURSOR }, false);
            return dtData;
        }
        return null/* TODO Change to default(_) if this is not a reference type */;
    }

    public DataSet GET_AT007(ParamDTO obj, UserLog log)
    {
        using (DataAccess.QueryData cls = new DataAccess.QueryData())
        {
            DataSet dtData = cls.ExecuteStore("PKG_ATTENDANCE_REPORT.AT007", new { P_ORG = obj.S_ORG_ID, P_ISDISSOLVE = obj.IS_DISSOLVE, P_USERNAME = log.Username, P_PERIOD = obj.PERIOD_ID, P_CUR = cls.OUT_CURSOR, P_CUR1 = cls.OUT_CURSOR }, false);
            return dtData;
        }
        return null/* TODO Change to default(_) if this is not a reference type */;
    }



    public DataSet ExportReport(string _reportCode, string _pkgName, ParamDTO _param, UserLog log)
    {
        object obj;
        using (DataAccess.QueryData cls = new DataAccess.QueryData())
        {
            switch (_reportCode)
            {
                case "ME_001":
                    {
                        obj = new { P_ORG = _param.S_ORG_ID, P_USERNAME = log.Username, P_ISDISSOLVE = _param.IS_DISSOLVE, P_STARTDATE = _param.FROMDATE, P_ENDDATE = _param.ENDDATE, P_CUR = cls.OUT_CURSOR, P_CUR1 = cls.OUT_CURSOR };
                        break;
                    }

                case "ME_002":
                    {
                        obj = new { P_ORG = _param.S_ORG_ID, P_USERNAME = log.Username, P_ISDISSOLVE = _param.IS_DISSOLVE, P_STARTDATE = _param.FROMDATE.Value, P_ENDDATE = _param.ENDDATE.Value, P_CUR = cls.OUT_CURSOR, P_CUR1 = cls.OUT_CURSOR };
                        break;
                    }

                case "ME_003":
                    {
                        obj = new { P_ORG = _param.S_ORG_ID, P_USERNAME = log.Username, P_ISDISSOLVE = _param.IS_DISSOLVE, P_STARTDATE = _param.FROMDATE.Value, P_ENDDATE = _param.ENDDATE.Value, P_CUR = cls.OUT_CURSOR, P_CUR1 = cls.OUT_CURSOR };
                        break;
                    }

                case "ME_004":
                    {
                        obj = new { P_ORG = _param.S_ORG_ID, P_USERNAME = log.Username, P_ISDISSOLVE = _param.IS_DISSOLVE, P_STARTDATE = _param.FROMDATE.Value.FirstDateOfMonth, P_ENDDATE = _param.ENDDATE.Value.LastDateOfMonth, P_CUR = cls.OUT_CURSOR, P_CUR1 = cls.OUT_CURSOR };
                        break;
                    }

                case "ME_005":
                    {
                        obj = new { P_ORG = _param.S_ORG_ID, P_USERNAME = log.Username, P_ISDISSOLVE = _param.IS_DISSOLVE, P_STARTDATE = _param.FROMDATE.Value, P_ENDDATE = _param.ENDDATE.Value, P_CUR = cls.OUT_CURSOR, P_CUR1 = cls.OUT_CURSOR };
                        break;
                    }
            }
            if (obj == null)
                return new DataSet();
            DataSet dtData = cls.ExecuteStore(_pkgName, obj, false);
            return dtData;
        }
        return null/* TODO Change to default(_) if this is not a reference type */;
    }
}
