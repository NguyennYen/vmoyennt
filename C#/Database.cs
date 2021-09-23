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
using System.Data.Common;
using System.Data.Entity;

public class Database
{
    public static DbConnection GetDbCtxConnection(string ConnString)
    {
        var db = new DbContext(ConnString);
        return db.Database.Connection;
    }
}
