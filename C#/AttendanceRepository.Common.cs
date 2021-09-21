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
using Attendance.AttendanceBusiness;
using Framework.UI;

partial class AttendanceRepository : AttendanceRepositoryBase
{
    public DataTable LOAD_PERIOD(AT_PERIODDTO obj)
    {
        DataTable dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.LOAD_PERIOD(obj, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public List<AT_PERIODDTO> LOAD_PERIODBylinq(AT_PERIODDTO obj)
    {
        List<AT_PERIODDTO> dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.LOAD_PERIODBylinq(obj, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public AT_PERIODDTO LOAD_PERIODByID(AT_PERIODDTO obj)
    {
        AT_PERIODDTO dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.LOAD_PERIODByID(obj, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataTable GetTerminal(AT_TERMINALSDTO obj)
    {
        DataTable dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.GetTerminal(obj, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataTable GetTerminalMeal(AT_TERMINALSDTO obj)
    {
        DataTable dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.GetTerminalMeal(obj, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }

    public DataSet GetDataFromOrg(PARAMDTO obj)
    {
        DataSet dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.GetDataFromOrg(obj, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public bool CLOSEDOPEN_PERIOD(PARAMDTO param)
    {
        bool dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.CLOSEDOPEN_PERIOD(param, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public bool IS_PERIODSTATUS(PARAMDTO param)
    {
        bool dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.IS_PERIODSTATUS(param, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public bool IS_PERIOD_PAYSTATUS(ParamDTO param, bool isAfter = false)
    {
        bool dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.IS_PERIOD_PAYSTATUS(param, isAfter, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public bool IS_PERIODSTATUS_BY_DATE(ParamDTO param)
    {
        bool dt;
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                dt = rep.IS_PERIODSTATUS_BY_DATE(param, this.Log);
                return dt;
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
    public bool CheckPeriodClose(List<decimal> lstEmp, DateTime startdate, DateTime enddate, ref string sAction)
    {
        try
        {
            _isAvailable = false;
            using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
            {
                try
                {
                    _isAvailable = rep.CheckPeriodClose(lstEmp, startdate, enddate, sAction);
                    return _isAvailable;
                }
                catch (Exception ex)
                {
                    rep.Abort();
                    throw ex;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _isAvailable = true;
        }
    }
}
