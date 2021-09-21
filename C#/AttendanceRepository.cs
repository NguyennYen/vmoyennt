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

public class AttendanceRepository : AttendanceRepositoryBase
{
    private bool _isAvailable;
    public bool GetComboboxData(ref ComboBoxDataDTO cbxData)
    {
        using (AttendanceBusinessClient rep = new AttendanceBusinessClient())
        {
            try
            {
                return rep.GetComboboxData(cbxData);
            }
            catch (Exception ex)
            {
                rep.Abort();
                throw ex;
            }
        }
    }
}
