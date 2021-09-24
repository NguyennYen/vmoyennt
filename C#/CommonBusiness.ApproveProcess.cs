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
        public List<ApproveProcessDTO> GetApproveProcessList()
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetApproveProcess;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public ApproveProcessDTO GetApproveProcess(decimal processId)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetApproveProcess(processId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertApproveProcess(ApproveProcessDTO item, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.InsertApproveProcess(item);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool UpdateProcess(ApproveProcessDTO item, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.UpdateApproveProcess(item);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool UpdateProcessStatus(List<decimal> itemUpdates, string status)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.UpdateApproveProcessStatus(itemUpdates, status);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        public ApproveSetupDTO GetApproveSetup(decimal id)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetApproveSetup(id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<ApproveSetupDTO> GetApproveSetupByEmployee(decimal employeeId, string Sorts = "CREATED_DATE desc")
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetApproveSetupByEmployee(employeeId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<ApproveSetupDTO> GetApproveSetupByOrg(decimal orgId, string Sorts = "CREATED_DATE desc")
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetApproveSetupByOrg(orgId, Sorts);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertApproveSetup(ApproveSetupDTO item, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.InsertApproveSetup(item, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool UpdateApproveSetup(ApproveSetupDTO item, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.UpdateApproveSetup(item, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteApproveSetup(List<decimal> itemDeletes)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.DeleteApproveSetup(itemDeletes);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool IsExistSetupByDate(ApproveSetupDTO itemCheck, decimal? idExclude = default(Decimal?))
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.IsExistSetupByDate(itemCheck, idExclude);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public bool InsertApproveTemplate(ApproveTemplateDTO item, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.InsertApproveTemplate(item, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool UpdateApproveTemplate(ApproveTemplateDTO item, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.UpdateApproveTemplate(item, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public ApproveTemplateDTO GetApproveTemplate(decimal id)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetApproveTemplate(id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<ApproveTemplateDTO> GetApproveTemplateList()
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetApproveTemplate;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteApproveTemplate(List<decimal> itemDeletes)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.DeleteApproveTemplate(itemDeletes);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool IsApproveTemplateUsed(decimal templateID)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.IsApproveTemplateUsed(templateID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        public bool InsertApproveTemplateDetail(ApproveTemplateDetailDTO item, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.InsertApproveTemplateDetail(item, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool UpdateApproveTemplateDetail(ApproveTemplateDetailDTO item, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.UpdateApproveTemplateDetail(item, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public ApproveTemplateDetailDTO GetApproveTemplateDetail(decimal id)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetApproveTemplateDetail(id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<ApproveTemplateDetailDTO> GetApproveTemplateDetailList(decimal templateId)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetApproveTemplateDetailList(templateId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteApproveTemplateDetail(List<decimal> itemDeeltes)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.DeleteApproveTemplateDetail(itemDeeltes);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool CheckLevelInsert(decimal level, decimal idExclude, decimal idTemplate)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.CheckLevelInsert(level, idExclude, idTemplate);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        public bool InsertApproveSetupExt(ApproveSetupExtDTO item, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.InsertApproveSetupExt(item, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool UpdateApproveSetupExt(ApproveSetupExtDTO item, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.UpdateApproveSetupExt(item, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public ApproveSetupExtDTO GetApproveSetupExt(decimal id)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetApproveSetupExt(id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<ApproveSetupExtDTO> GetApproveSetupExtList(decimal employeeId, string Sorts = "CREATED_DATE desc")
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetApproveSetupExtList(employeeId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteApproveSetupExt(List<decimal> itemDeletes)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.DeleteApproveSetupExt(itemDeletes);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool IsExistSetupExtByDate(ApproveSetupExtDTO itemCheck, decimal? idExclude = default(Decimal?))
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.IsExistSetupExtByDate(itemCheck, idExclude);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<ApproveUserDTO> GetApproveUsers(decimal employeeId, string processCode)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetApproveUsers(employeeId, processCode);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<EmployeeDTO> GetListEmployee(List<decimal> _orgIds)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetListEmployee(_orgIds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
