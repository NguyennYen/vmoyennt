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
using System.Configuration;

public class AttendanceRepositoryStatic
{
    public static AttendanceRepository Instance
    {
        get
        {
            return new AttendanceRepository();
        }
    }
}
