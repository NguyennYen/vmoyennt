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
using System.Dynamic;
using System.Linq.Expressions;
using System.Configuration;
using Framework.Data;
using Aspose.Cells;

public static class Utility
{
    /// <summary>
    ///     ''' Writes the exception log.
    ///     ''' </summary>
    ///     ''' <param name="objErr">The obj err.</param>
    ///     ''' <param name="sFunc">The s func.</param>
    public static void WriteExceptionLog(Exception objErr, string sFunc, string OtherText = "")
    {
        StringBuilder err = new StringBuilder();
        string logFile = string.Empty;
        System.IO.StreamWriter logWriter = null;
        string logfolder;
        string logformat;
        try
        {
            err.AppendLine("Function: " + sFunc);
            err.AppendLine("Datetime: " + DateTime.Now);
            err.AppendLine("Error message: " + objErr.Message);
            err.AppendLine("Stack trace: " + objErr.StackTrace);

            if (objErr.InnerException != null)
            {
                err.AppendLine("Inner error message: " + objErr.InnerException.Message);
                err.AppendLine("Inner stack trace: " + objErr.InnerException.StackTrace);
            }

            err.AppendLine("Other Text: " + OtherText);

            logfolder = ConfigurationManager.AppSettings("EXFILEFOLDER");
            if (string.IsNullOrEmpty(logfolder))
                logfolder = "Exception";

            logformat = ConfigurationManager.AppSettings("EXFILEFORMAT");
            if (string.IsNullOrEmpty(logformat))
                logformat = "yyyyMMdd.log";

            logFile = AppDomain.CurrentDomain.BaseDirectory + @"\" + logfolder + @"\" + DateTime.Now.ToString(logformat);

            if (System.IO.File.Exists(logFile))
                logWriter = System.IO.File.AppendText(logFile);
            else
                logWriter = System.IO.File.CreateText(logFile);

            logWriter.WriteLine(err.ToString());
            logWriter.Close();
        }
        catch (Exception e)
        {
        }
    }

    public static string FormatRegisterAppointmentSubjectPortal(AT_TIME_MANUAL sign, AT_PORTAL_REG val, List<OT_OTHER_LIST> lstValue = null)
    {
        StringBuilder rtnVal = new StringBuilder();
        // Dim rtnExt As String = ATConstant.EXTENVALUE_FORMAT
        bool isValid = false;

        switch (sign.CODE)
        {
            case object _ when ATConstant.SIGNTYPECODE_NUMBER:
                {
                    rtnVal.Append(IIf(val.NVALUE == 1, "", Format(val.NVALUE, "0.##").ToString()));
                    break;
                }

            case object _ when ATConstant.SIGNTYPECODE_STRING:
                {
                    rtnVal.Append(val.SVALUE);
                    break;
                }

            case object _ when ATConstant.SIGNTYPECODE_DATETIME:
                {
                    rtnVal.Append(val.DVALUE.ToString("HH:mm"));
                    break;
                }
        }
        rtnVal.Append(sign.CODE);

        if (sign.CODE == ATConstant.GSIGNCODE_OVERTIME)
            return string.Format("{0}[{1}-{2}]", rtnVal.ToString(), val.FROM_HOUR.HasValue ? val.FROM_HOUR.Value.ToString("HH:mm") : "", val.TO_HOUR.HasValue ? val.TO_HOUR.Value.ToString("HH:mm") : "");

        if (sign.CODE == ATConstant.GSIGNCODE_LEAVE & val.NVALUE == 1)
            return rtnVal.ToString();

        if (val.NVALUE_ID != null)
        {
            if (lstValue != null)
            {
                var otValue = (from p in lstValue
                               where p.ID == val.NVALUE_ID
                               select p).FirstOrDefault;
                if (otValue != null && otValue.CODE != 1)
                    rtnVal.Append("(" + otValue.NAME_VN + ")");
            }
        }
        return rtnVal.ToString();
    }


    public static void GetContentTemplate(string templatefile, ref string subject, ref string content)
    {
        try
        {
            var sqlPath = ConfigurationManager.AppSettings("PATH_MAILFILE");
            var sqlFileName = AppDomain.CurrentDomain.BaseDirectory + @"\" + sqlPath + @"\" + templatefile;

            content = System.IO.File.ReadAllText(sqlFileName, Encoding.UTF8);

            string[] contentsplit = content.Split(Environment.NewLine);
            if (contentsplit.Length > 1)
                // get subject
                subject = contentsplit[0];
            // remove subject from content
            content = content.Replace(subject + Environment.NewLine, string.Empty);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static decimal? NVL(string _value, decimal? _default)
    {
        try
        {
            if (!Information.IsNumeric(_value))
                return _default;
            return _value;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
