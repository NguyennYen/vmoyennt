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


public partial class CommonContext
{
    public new int SaveChanges(UserLog log)
    {
        var entries = this.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified | EntityState.Deleted);
        int intRet = -1;
        List<AuditLog> lstAuditLogs = new List<AuditLog>();
        foreach (ObjectStateEntry entry in entries)
        {
            var Auditable = entry.Entity;
            decimal ObjectId;
            string ObjectState = "";
            bool IsAudit = false;

            if (Auditable != null)
            {
                using (EntityHelper EntityHelper = new EntityHelper())
                {
                    string ObjectName = entry.EntitySet.Name;
                    if (entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
                        ObjectId = entry.EntityKey.EntityKeyValues(0).Value;
                    else
                        ObjectId = EntityHelper.GetProperty(Auditable, "ID");

                    if (entry.State == EntityState.Added)
                    {
                        EntityHelper.SetProperty("CREATED_BY", log.Username, Auditable);
                        EntityHelper.SetProperty("CREATED_DATE", DateTime.Now, Auditable);
                        EntityHelper.SetProperty("CREATED_LOG", log.Ip + "-" + log.ComputerName, Auditable);
                        EntityHelper.SetProperty("MODIFIED_BY", log.Username, Auditable);
                        EntityHelper.SetProperty("MODIFIED_DATE", DateTime.Now, Auditable);
                        EntityHelper.SetProperty("MODIFIED_LOG", log.Ip + "-" + log.ComputerName, Auditable);
                        ObjectState = "I";
                        IsAudit = true;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        EntityHelper.SetProperty("MODIFIED_BY", log.Username, Auditable);
                        EntityHelper.SetProperty("MODIFIED_DATE", DateTime.Now, Auditable);
                        EntityHelper.SetProperty("MODIFIED_LOG", log.Ip + "-" + log.ComputerName, Auditable);
                        ObjectState = "U";
                        IsAudit = true;
                    }
                    else if (entry.State == EntityState.Deleted)
                    {
                        ObjectState = "D";
                        IsAudit = true;
                    }


                    if (IsAudit && AuditLogHelper.IsAuditLog(ObjectName))
                    {
                        var lstLogDtl = EntityHelper.GetEntryValueInString(entry);
                        lstAuditLogs.Add(new AuditLog() { ObjectId = ObjectId, ObjectName = ObjectName, ObjectState = ObjectState, lstLogDtl = lstLogDtl });
                    }
                }
            }
        }
        intRet = this.SaveChanges();
        AuditLogHelper.TryAuditLog(lstAuditLogs, log);

        return intRet;
    }
}
