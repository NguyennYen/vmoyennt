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
using SystemConfig;


// NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in both code and config file together.
namespace CommonBusiness.ServiceImplementations
{
    public partial class CommonBusiness : ICommonBusiness
    {
        public bool CheckOtherListExistInDatabase(List<decimal> lstID, decimal typeID)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.CheckOtherListExistInDatabase(lstID, typeID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return false;
        }

        public DataTable GetATOrgPeriod(decimal periodID)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetATOrgPeriod(periodID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public bool IsUsernameExist(string Username)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.IsUsernameExist(Username);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return false;
        }

        public UserDTO GetUserWithPermision(string Username)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetUserWithPermision(Username);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return null/* TODO Change to default(_) if this is not a reference type */;
        }

        public List<CommonDAL.PermissionDTO> GetUserPermissions(string Username)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetUserPermissions(Username);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return null;
        }

        public bool CheckUserAdmin(string Username)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.CheckUserAdmin(Username);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ChangeUserPassword(string Username, string _oldpass, string _newpass, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.ChangeUserPassword(Username, _oldpass, _newpass, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return default(Boolean);
        }

        public string GetPassword(string Username)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetPassword(Username);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool UpdateUserStatus(string Username, string _ACTFLG, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.UpdateUserStatus(Username, _ACTFLG, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return default(Boolean);
        }

        public List<OrganizationDTO> GetOrganizationAll()
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    var lst = rep.GetOrganizationAll();
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<CommonDAL.OrganizationDTO> GetOrganizationLocationTreeView(string _username)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    var lst = rep.GetOrganizationLocationTreeView(_username);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public CommonDAL.OrganizationDTO GetOrganizationLocationInfo(System.Decimal _orgId)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    var lst = rep.GetOrganizationLocationInfo(_orgId);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<CommonDAL.OrganizationStructureDTO> GetOrganizationStructureInfo(System.Decimal _orgId)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    var lst = rep.GetOrganizationStructureInfo(_orgId);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool GetComboList(ref CommonDAL.ComboBoxDataDTO _combolistDTO)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetComboList(_combolistDTO);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public object InsertMail(string _from, string _to, string _subject, string _content, string _cc = "", string _bcc = "", string _viewName = "")
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.InsertMail(_from, _to, _subject, _content, _cc, _bcc, _viewName);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool SendMail()
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.SendMail();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<CommonDAL.OtherListDTO> GetOtherListByTypeToCombo(string sType)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    var lst = rep.GetOtherListByTypeToCombo(sType);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<CommonDAL.OtherListDTO> GetOtherList(string sACT)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    var lst = rep.GetOtherList(sACT);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<CommonDAL.OtherListDTO> GetOtherListByType(decimal gID, OtherListDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    var lst = rep.GetOtherListByType(gID, _filter, PageIndex, PageSize, Total, Sorts);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertOtherList(OtherListDTO objOtherList, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.InsertOtherList(objOtherList, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyOtherList(OtherListDTO objOtherList, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.ModifyOtherList(objOtherList, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ValidateOtherList(OtherListDTO _validate)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.ValidateOtherList(_validate);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ActiveOtherList(List<decimal> lstOtherList, string sActive, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.ActiveOtherList(lstOtherList, sActive, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteOtherList(List<OtherListDTO> lstOtherList)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.DeleteOtherList(lstOtherList);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<OtherListGroupDTO> GetOtherListGroupBySystem(string systemName)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetOtherListGroupBySystem(systemName);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<CommonDAL.OtherListGroupDTO> GetOtherListGroup(string sACT)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    var lst = rep.GetOtherListGroup(sACT);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertOtherListGroup(OtherListGroupDTO objOtherListGroup, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.InsertOtherListGroup(objOtherListGroup, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyOtherListGroup(OtherListGroupDTO objOtherListGroup, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.ModifyOtherListGroup(objOtherListGroup, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ActiveOtherListGroup(OtherListGroupDTO[] lstOtherListGroup, string sActive, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.ActiveOtherListGroup(lstOtherListGroup, sActive, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteOtherListGroup(List<OtherListGroupDTO> lstOtherListGroup)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.DeleteOtherListGroup(lstOtherListGroup);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<OtherListTypeDTO> GetOtherListTypeSystem(string systemName)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    var lst = rep.GetOtherListTypeSystem(systemName);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<OtherListTypeDTO> GetOtherListType(string sACT)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    var lst = rep.GetOtherListType(sACT);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertOtherListType(OtherListTypeDTO objOtherListType, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.InsertOtherListType(objOtherListType, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyOtherListType(OtherListTypeDTO objOtherListType, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.ModifyOtherListType(objOtherListType, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ActiveOtherListType(OtherListTypeDTO[] lstOtherListType, string sActive, UserLog log)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.ActiveOtherListType(lstOtherListType, sActive, log);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteOtherListType(List<OtherListTypeDTO> lstOtherListType)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.DeleteOtherListType(lstOtherListType);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public Dictionary<int, string> GetReminderConfig(string username)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.GetReminderConfig(username);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public bool SetReminderConfig(string username, int type, string value)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.SetReminderConfig(username, type, value);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }




        public List<LdapDTO> GetLdap(LdapDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "LDAP_NAME")
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    var lst = rep.GetLdap(_filter, PageIndex, PageSize, Total, Sorts);
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool InsertLdap(LdapDTO objLdap, UserLog log, ref decimal gID)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.InsertLdap(objLdap, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool ModifyLdap(LdapDTO objLdap, UserLog log, ref decimal gID)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.ModifyLdap(objLdap, log, gID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DeleteLdap(List<decimal> lstID)
        {
            using (CommonRepository rep = new CommonRepository())
            {
                try
                {
                    return rep.DeleteLdap(lstID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
