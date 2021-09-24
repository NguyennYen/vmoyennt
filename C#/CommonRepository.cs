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
using System.Threading;

public partial class CommonRepository : CommonRepositoryBase
{
    public DataTable GetATOrgPeriod(decimal periodID)
    {
        try
        {
            using (DataAccess.QueryData cls = new DataAccess.QueryData())
            {
                DataTable dtData = cls.ExecuteStore("PKG_COMMON_LIST.GET_AT_ORG_PERIOD", new { P_PERIOD = periodID, P_CUR = cls.OUT_CURSOR });

                return dtData;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// Kiểm tra dữ liệu Other list đã được sử dụng hay chưa?
    ///     ''' </summary>
    ///     ''' <param name="table">Enum Table_Name</param>
    ///     ''' <returns>true:chưa có/false:có rồi</returns>
    ///     ''' <remarks></remarks>
    public bool CheckOtherListExistInDatabase(List<decimal> lstID, decimal typeID)
    {
        bool isExist = false;
        try
        {
            string strListID = lstID.Select(x => x.ToString()).Aggregate((x, y) => x + "," + y);
            var sql = "";
            switch (typeID)
            {
                case 1 // ACADEMY
               :
                    {
                        isExist = Execute_ExistInDatabase("HU_EMPLOYEE_EDUCATION", "ACADEMY", strListID);
                        if (!isExist)
                            return isExist;
                        break;
                    }

                case 3 // ACTION_TYPE
         :
                    {
                        break;
                    }

                case 5 // ASSET_GROUP
       :
                    {
                        break;
                    }

                case 7 // ATTATCH_TYPE
       :
                    {
                        break;
                    }

                case 20 // COMMEND_LEVEL
       :
                    {
                        break;
                    }

                case 21 // COMMEND_OBJECT
       :
                    {
                        break;
                    }

                case 22 // COMMEND_TYPE
       :
                    {
                        break;
                    }

                case 24 // CONTRACT_STATUS
       :
                    {
                        isExist = Execute_ExistInDatabase("HU_CONTRACT", "STATUS_ID", strListID);
                        if (!isExist)
                            return isExist;
                        break;
                    }

                case 25 // DB_CHANGE
         :
                    {
                        break;
                    }

                case 26 // DB_EMP
       :
                    {
                        break;
                    }

                case 28 // DECISION_TYPE
       :
                    {
                        break;
                    }

                case 29 // DISCIPLINE_LEVEL
       :
                    {
                        break;
                    }

                case 30 // DISCIPLINE_OBJECT
       :
                    {
                        break;
                    }

                case 31 // DISCIPLINE_TYPE
       :
                    {
                        break;
                    }

                case 32 // FAMILY_STATUS
       :
                    {
                        isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "MARITAL_STATUS", strListID);
                        if (!isExist)
                            return isExist;
                        break;
                    }

                case 34 // GENDER
         :
                    {
                        isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "GENDER", strListID);
                        if (!isExist)
                            return isExist;
                        break;
                    }

                case 38 // LANGUAGE_LEVEL
         :
                    {
                        isExist = Execute_ExistInDatabase("HU_EMPLOYEE_EDUCATION", "LANGUAGE_LEVEL", strListID);
                        if (!isExist)
                            return isExist;
                        break;
                    }

                case 39 // LEARNING_LEVEL
         :
                    {
                        isExist = Execute_ExistInDatabase("HU_EMPLOYEE_EDUCATION", "LEARNING_LEVEL", strListID);
                        if (!isExist)
                            return isExist;
                        break;
                    }

                case 40 // MAJOR
         :
                    {
                        isExist = Execute_ExistInDatabase("HU_EMPLOYEE_EDUCATION", "MAJOR", strListID);
                        if (!isExist)
                            return isExist;
                        break;
                    }

                case 42 // NATIVE
         :
                    {
                        isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "NATIVE", strListID);
                        if (!isExist)
                            return isExist;
                        break;
                    }

                case 43 // ORG_LEVEL
         :
                    {
                        isExist = Execute_ExistInDatabase("HU_ORGANIZATION", "LEVEL_ID", strListID);
                        if (!isExist)
                            return isExist;
                        break;
                    }

                case 48 // RELATION
         :
                    {
                        break;
                    }

                case 49 // RELIGION
       :
                    {
                        isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "RELIGION", strListID);
                        if (!isExist)
                            return isExist;
                        break;
                    }

                case 50 // SYSTEM_LANGUAGE
         :
                    {
                        break;
                    }

                case 51 // TER_REASON
       :
                    {
                        break;
                    }

                case 52 // TER_STATUS
       :
                    {
                        break;
                    }

                case 53 // TER_TYPE
       :
                    {
                        break;
                    }

                case 54 // TRANSFER_REASON
       :
                    {
                        break;
                    }

                case 55 // DECISION_STATUS
       :
                    {
                        break;
                    }

                case 56 // TRANSFER_TYPE
       :
                    {
                        break;
                    }

                case 57 // UNIT
       :
                    {
                        break;
                    }

                case 59 // WORK_STATUS
       :
                    {
                        isExist = Execute_ExistInDatabase("HU_EMPLOYEE", "WORK_STATUS", strListID);
                        if (!isExist)
                            return isExist;
                        break;
                    }

                case 141 // MARK_EDU
         :
                    {
                        break;
                    }

                case 142 // TRAINING_FORM
       :
                    {
                        isExist = Execute_ExistInDatabase("HU_EMPLOYEE_EDUCATION", "TRAINING_FORM", strListID);
                        if (!isExist)
                            return isExist;
                        break;
                    }

                case 180 // COMMEND_STATUS
         :
                    {
                        break;
                    }

                case 181 // DISCIPLINE_STATUS
       :
                    {
                        break;
                    }

                case 961 // WORK_TIME
       :
                    {
                        break;
                    }

                case 1000 // TR_CER_GROUP
       :
                    {
                        break;
                    }

                case 1001 // TR_TRAIN_FORM
       :
                    {
                        break;
                    }

                case 1002 // TR_TRAIN_ENTRIES
       :
                    {
                        break;
                    }

                case 1003 // TR_DURATION_UNIT
       :
                    {
                        break;
                    }

                case 1004 // TR_CURRENCY
       :
                    {
                        break;
                    }

                case 1005 // TR_REQUEST_STATUS
       :
                    {
                        break;
                    }

                case 1006 // TR_DURATION_STUDY
       :
                    {
                        break;
                    }

                case 1007 // TR_LANGUAGE
       :
                    {
                        break;
                    }

                case 1008 // TR_LIST_PREPARE
       :
                    {
                        break;
                    }

                case 1009 // TR_RANK
       :
                    {
                        break;
                    }

                case 1010 // RC_RECRUIT_REASON
       :
                    {
                        break;
                    }

                case 1011 // RC_PLAN_REG_STATUS
       :
                    {
                        break;
                    }

                case 1012 // RC_REQUEST_STATUS
       :
                    {
                        break;
                    }

                case 1013 // RC_RECRUIT_TYPE
       :
                    {
                        break;
                    }

                case 1014 // RC_GROUP_WORK
       :
                    {
                        break;
                    }

                case 1015 // RC_PRIORITY_LEVEL
       :
                    {
                        break;
                    }

                case 1016 // RC_TRAINING_LEVEL
       :
                    {
                        break;
                    }

                case 1017 // RC_TRAINING_SCHOOL
       :
                    {
                        break;
                    }

                case 1019 // RC_GRADUATION_TYPE
       :
                    {
                        break;
                    }

                case 1020 // RC_LANGUAGE
       :
                    {
                        break;
                    }

                case 1021 // RC_LANGUAGE_LEVEL
       :
                    {
                        break;
                    }

                case 1022 // RC_COMPUTER_LEVEL
       :
                    {
                        break;
                    }

                case 1023 // RC_APPEARANCE
       :
                    {
                        break;
                    }

                case 1024 // RC_HEALTH_STATUS
       :
                    {
                        break;
                    }

                case 1025 // RC_PROGRAM_STATUS
       :
                    {
                        break;
                    }

                case 1026 // RC_RECRUIT_SCOPE
       :
                    {
                        break;
                    }

                case 1027 // RC_SOFT_SKILL
       :
                    {
                        break;
                    }

                case 1028 // RC_CHARACTER
       :
                    {
                        break;
                    }

                case 1029 // RC_CANDIDATE_STATUS
       :
                    {
                        break;
                    }

                case 1032 // RC_FORM
       :
                    {
                        break;
                    }

                case 1035 // TYPE_PUNISH
       :
                    {
                        break;
                    }

                case 1036 // TYPE_SHIFT
       :
                    {
                        break;
                    }

                case 1037 // TYPE_PAYMENT
       :
                    {
                        break;
                    }

                case 1038 // TYPE_EMPLOYEE
       :
                    {
                        break;
                    }

                case 1039 // TYPE_REST_DAY
       :
                    {
                        break;
                    }

                case 2000 // HU_TITLE_GROUP
       :
                    {
                        isExist = Execute_ExistInDatabase("HU_TITLE", "TITLE_GROUP_ID", strListID);
                        if (!isExist)
                            return isExist;
                        break;
                    }

                case 2001 // PA_RESIDENT
         :
                    {
                        break;
                    }

                case 2002 // CALCULATION
       :
                    {
                        break;
                    }

                case 2003 // INSURANCE_CHANGE_TYPE
       :
                    {
                        break;
                    }

                case 2021 // GROUP_ARISING_TYPE
       :
                    {
                        break;
                    }

                case 2022 // STATUS_NOBOOK
       :
                    {
                        break;
                    }

                case 2023 // STATUS_CARD
       :
                    {
                        break;
                    }

                case 2024 // LOCATION
       :
                    {
                        break;
                    }

                case 2025 // ORG_INS
       :
                    {
                        break;
                    }

                case 2026 // REASON
       :
                    {
                        break;
                    }

                case 2027 // TYPE_DSVM
       :
                    {
                        break;
                    }

                case 2028 // HS_OT
       :
                    {
                        break;
                    }

                case 2029 // DEFAULT_RICE
       :
                    {
                        break;
                    }

                case 2052:
                    {
                        isExist = Execute_ExistInDatabase("AT_MEAL_MANAGER", "RATION_ID", strListID);
                        if (!isExist)
                            return isExist;

                        isExist = Execute_ExistInDatabase("AT_MEAL_COST_SETUP", "RATION_ID", strListID);
                        if (!isExist)
                            return isExist;

                        isExist = Execute_ExistInDatabase("AT_MEAL_PARTNER", "RATION_ID", strListID);
                        if (!isExist)
                            return isExist;
                        break;
                    }
            }
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    private void Execute_ExistInDatabase(string tableName, string colName, string strListID)
    {
        try
        {
            decimal count = 0;
            var Sql = "SELECT COUNT(" + colName + ") FROM " + tableName + " WHERE " + colName + " IN (" + strListID + ")";
            count = Context.ExecuteStoreQuery<decimal>(Sql).FirstOrDefault;
            if (count > 0)
                return false;
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public List<OtherListDTO> GetOtherListByTypeToCombo(string sType)
    {
        try
        {
            var query = (from p in Context.OT_OTHER_LIST
                         where p.ACTFLG == "A" & p.OT_OTHER_LIST_TYPE.CODE.ToUpper == sType.ToUpper()
                         orderby p.NAME_VN
                         select new OtherListDTO() { ID = p.ID, NAME_VN = p.NAME_VN, NAME_EN = p.NAME_EN, CODE = p.CODE });
            return query.ToList;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<OtherListDTO> GetOtherList(string sACT)
    {
        ObjectQuery<OtherListDTO> query;
        if (sACT == "")
            query = (from p in Context.OT_OTHER_LIST
                     join q in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals q.ID
                     orderby q.NAME, p.NAME_VN
                     select new OtherListDTO() { ID = p.ID, NAME_VN = p.NAME_VN, NAME_EN = p.NAME_EN, CODE = p.CODE, TYPE_ID = p.TYPE_ID, TYPE_NAME = q.NAME, TYPE_CODE = q.CODE, ACTFLG = p.ACTFLG == "A" ? "Áp dụng" : "Ngừng áp dụng" });
        else
            query = (from p in Context.OT_OTHER_LIST
                     join q in Context.OT_OTHER_LIST_TYPE on p.TYPE_ID equals q.ID
                     orderby q.NAME, p.NAME_VN
                     where p.ACTFLG == sACT
                     select new OtherListDTO() { ID = p.ID, NAME_VN = p.NAME_VN, NAME_EN = p.NAME_EN, CODE = p.CODE, TYPE_ID = p.TYPE_ID, TYPE_NAME = q.NAME, TYPE_CODE = q.CODE, ACTFLG = p.ACTFLG == "A" ? "Áp dụng" : "Ngừng áp dụng" });

        /// Logger.LogInfo(query)
        return query.ToList;
    }

    public List<OtherListDTO> GetOtherListByType(decimal gID, OtherListDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc")
    {
        var query = from p in Context.OT_OTHER_LIST
                    where p.TYPE_ID == gID
                    select new OtherListDTO() { ID = p.ID, NAME_VN = p.NAME_VN, NAME_EN = p.NAME_EN, CODE = p.CODE, REMARK = p.REMARK, TYPE_ID = p.TYPE_ID, TYPE_NAME = p.OT_OTHER_LIST_TYPE.NAME, TYPE_CODE = p.OT_OTHER_LIST_TYPE.CODE, ACTFLG = p.ACTFLG == "A" ? "Áp dụng" : "Ngừng áp dụng", CREATED_DATE = p.CREATED_DATE };

        if (_filter.CODE != "")
            query = query.Where(p => p.CODE.ToUpper.Contains(_filter.CODE.ToUpper));
        if (_filter.NAME_EN != "")
            query = query.Where(p => p.NAME_EN.ToUpper.Contains(_filter.NAME_EN.ToUpper));
        if (_filter.NAME_VN != "")
            query = query.Where(p => p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper));
        if (_filter.ACTFLG != "")
            query = query.Where(p => p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper));
        if (_filter.REMARK != "")
            query = query.Where(p => p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper));
        query = query.OrderBy(Sorts);
        Total = query.Count;
        query = query.Skip(PageIndex * PageSize).Take(PageSize);

        return query.ToList;
    }

    public bool InsertOtherList(OtherListDTO objOtherList, UserLog log)
    {
        try
        {
            OT_OTHER_LIST objOtherListData = new OT_OTHER_LIST();
            objOtherListData.ID = Utilities.GetNextSequence(Context, Context.OT_OTHER_LIST.EntitySet.Name);
            objOtherListData.NAME_EN = objOtherList.NAME_EN;
            objOtherListData.NAME_VN = objOtherList.NAME_VN;
            objOtherListData.TYPE_ID = objOtherList.TYPE_ID;
            objOtherListData.CODE = objOtherList.CODE;
            objOtherListData.ACTFLG = objOtherList.ACTFLG;
            objOtherListData.REMARK = objOtherList.REMARK;
            Context.OT_OTHER_LIST.AddObject(objOtherListData);
            Context.SaveChanges(log);
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool ModifyOtherList(OtherListDTO objOtherList, UserLog log)
    {
        OT_OTHER_LIST objOtherListData = new OT_OTHER_LIST() { ID = objOtherList.ID };
        Context.OT_OTHER_LIST.Attach(objOtherListData);
        objOtherListData.NAME_EN = objOtherList.NAME_EN;
        objOtherListData.NAME_VN = objOtherList.NAME_VN;
        objOtherListData.TYPE_ID = objOtherList.TYPE_ID;
        objOtherListData.CODE = objOtherList.CODE;
        objOtherListData.REMARK = objOtherList.REMARK;
        Context.SaveChanges(log);
        return true;
    }


    public void ValidateOtherList(OtherListDTO _validate)
    {
        var query;
        try
        {
            if (_validate.CODE != null/* TODO Change to default(_) if this is not a reference type */ )
            {
                if (_validate.ID != 0)
                    query = (from p in Context.OT_OTHER_LIST
                             where p.CODE.ToUpper == _validate.CODE.ToUpper
                                                          & p.TYPE_ID == _validate.TYPE_ID
                                                          & p.ID != _validate.ID
                             select p).FirstOrDefault;
                else
                    query = (from p in Context.OT_OTHER_LIST
                             where p.TYPE_ID == _validate.TYPE_ID
                                                          & p.CODE.ToUpper == _validate.CODE.ToUpper
                             select p).FirstOrDefault;
                return (query == null);
            }
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool ActiveOtherList(List<decimal> lstOtherList, string sActive, UserLog log)
    {
        List<OT_OTHER_LIST> lstOtherListData;
        lstOtherListData = (from p in Context.OT_OTHER_LIST
                            where lstOtherList.Contains(p.ID)
                            select p).ToList;
        for (var index = 0; index <= lstOtherListData.Count - 1; index++)
            lstOtherListData[index].ACTFLG = sActive;
        Context.SaveChanges(log);
        return true;
    }

    public bool DeleteOtherList(List<OtherListDTO> lstOtherList)
    {
        List<OT_OTHER_LIST> lstOtherListData;
        List<decimal> lstIDOtherList = (from p in lstOtherList.ToList
                                        select p.ID).ToList;
        try
        {
            lstOtherListData = (from p in Context.OT_OTHER_LIST
                                where lstIDOtherList.Contains(p.ID)
                                select p).ToList;
            for (var index = 0; index <= lstOtherListData.Count - 1; index++)
                Context.OT_OTHER_LIST.DeleteObject(lstOtherListData[index]);
            Context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public List<OtherListGroupDTO> GetOtherListGroupBySystem(string systemName)
    {
        // Dim query As ObjectQuery(Of OtherListGroupDTO)
        if (systemName == "")
        {
            var query = (from p in Context.OT_OTHER_LIST_GROUP
                         select new OtherListGroupDTO() { ID = p.ID, NAME = p.NAME, ACTFLG = p.ACTFLG == "A" ? "Áp dụng" : "Ngừng áp dụng" }).ToList;

            return query;
        }
        else
        {
            decimal queryParam;
            // Do đang lấy tất cả danh mục dùng chung lên form profile nên lấy theo id mặc định.

            switch (systemName.Trim())
            {
                case object _ when SystemCodes.Attendance.ToString():
                    {
                        queryParam = SystemCodes.Attendance;
                        break;
                    }

                case object _ when SystemCodes.Payroll.ToString():
                    {
                        queryParam = SystemCodes.Payroll;
                        break;
                    }

                case object _ when SystemCodes.Profile.ToString():
                    {
                        queryParam = SystemCodes.Profile;
                        break;
                    }

                case object _ when SystemCodes.Recruitment.ToString():
                    {
                        queryParam = SystemCodes.Recruitment;
                        break;
                    }

                case object _ when SystemCodes.Training.ToString():
                    {
                        queryParam = SystemCodes.Training;
                        break;
                    }

                case object _ when SystemCodes.Meal.ToString():
                    {
                        queryParam = SystemCodes.Meal;
                        break;
                    }
            }

            var query = (from p in Context.OT_OTHER_LIST_GROUP
                         where p.ID == queryParam
                         select new OtherListGroupDTO() { ID = p.ID, NAME = p.NAME, ACTFLG = p.ACTFLG == "A" ? "Áp dụng" : "Ngừng áp dụng" }).ToList;

            return query;
        }
    }

    public List<OtherListGroupDTO> GetOtherListGroup(string sACT)
    {
        ObjectQuery<OtherListGroupDTO> query;
        if (sACT == "")
            query = (from p in Context.OT_OTHER_LIST_GROUP
                     select new OtherListGroupDTO() { ID = p.ID, NAME = p.NAME, ACTFLG = p.ACTFLG == "A" ? "Áp dụng" : "Ngừng áp dụng" });
        else
            query = (from p in Context.OT_OTHER_LIST_GROUP
                     select new OtherListGroupDTO() { ID = p.ID, NAME = p.NAME, ACTFLG = p.ACTFLG == "A" ? "Áp dụng" : "Ngừng áp dụng" });
        // 'Logger.LogInfo(query)
        return query.ToList;
    }

    public bool InsertOtherListGroup(OtherListGroupDTO objOtherListGroup, UserLog log)
    {
        OT_OTHER_LIST_GROUP objOtherListGroupData = new OT_OTHER_LIST_GROUP();
        objOtherListGroupData.ID = Utilities.GetNextSequence(Context, Context.OT_OTHER_LIST_GROUP.EntitySet.Name);
        objOtherListGroupData.NAME = objOtherListGroup.NAME;
        objOtherListGroupData.ACTFLG = objOtherListGroup.ACTFLG;
        objOtherListGroupData.CREATED_DATE = DateTime.Now;
        objOtherListGroupData.CREATED_BY = log.Username;
        objOtherListGroupData.CREATED_LOG = log.ComputerName;
        objOtherListGroupData.MODIFIED_DATE = DateTime.Now;
        objOtherListGroupData.MODIFIED_BY = log.Username;
        objOtherListGroupData.MODIFIED_LOG = log.ComputerName;
        Context.OT_OTHER_LIST_GROUP.AddObject(objOtherListGroupData);
        Context.SaveChanges(log);
        return true;
    }

    public bool ModifyOtherListGroup(OtherListGroupDTO objOtherListGroup, UserLog log)
    {
        OT_OTHER_LIST_GROUP objOtherListGroupData = new OT_OTHER_LIST_GROUP() { ID = objOtherListGroup.ID };
        Context.OT_OTHER_LIST_GROUP.Attach(objOtherListGroupData);
        objOtherListGroupData.ID = objOtherListGroup.ID;
        objOtherListGroupData.NAME = objOtherListGroup.NAME;
        objOtherListGroupData.MODIFIED_DATE = DateTime.Now;
        objOtherListGroupData.MODIFIED_BY = log.Username;
        objOtherListGroupData.MODIFIED_LOG = log.ComputerName;
        Context.SaveChanges(log);
        return true;
    }

    public bool ActiveOtherListGroup(OtherListGroupDTO[] lstOtherListGroup, string sActive, UserLog log)
    {
        List<OT_OTHER_LIST_GROUP> lstOtherListGroupData;
        List<decimal> lstIDOtherListGroup = (from p in lstOtherListGroup.ToList
                                             select p.ID).ToList;
        lstOtherListGroupData = (from p in Context.OT_OTHER_LIST_GROUP
                                 where lstIDOtherListGroup.Contains(p.ID)
                                 select p).ToList;
        for (var index = 0; index <= lstOtherListGroupData.Count - 1; index++)
        {
            lstOtherListGroupData[index].ACTFLG = sActive;
            lstOtherListGroupData[index].MODIFIED_DATE = DateTime.Now;
            lstOtherListGroupData[index].MODIFIED_BY = log.Username;
            lstOtherListGroupData[index].MODIFIED_LOG = log.ComputerName;
        }
        Context.SaveChanges(log);
        return true;
    }

    public bool DeleteOtherListGroup(List<OtherListGroupDTO> lstOtherListGroup)
    {
        List<OT_OTHER_LIST_GROUP> lstOtherListGroupData;
        List<decimal> lstIDOtherListGroup = (from p in lstOtherListGroup.ToList
                                             select p.ID).ToList;
        try
        {
            lstOtherListGroupData = (from p in Context.OT_OTHER_LIST_GROUP
                                     where lstIDOtherListGroup.Contains(p.ID)
                                     select p).ToList;
            for (var index = 0; index <= lstOtherListGroupData.Count - 1; index++)
                Context.OT_OTHER_LIST_GROUP.DeleteObject(lstOtherListGroupData[index]);
            Context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<OtherListTypeDTO> GetOtherListTypeSystem(string systemName)
    {
        ObjectQuery<OtherListTypeDTO> query;
        decimal queryParam;
        if (systemName != "")
        {
            switch (systemName.Trim())
            {
                case object _ when SystemCodes.Attendance.ToString():
                    {
                        queryParam = SystemCodes.Attendance;
                        break;
                    }

                case object _ when SystemCodes.Payroll.ToString():
                    {
                        queryParam = SystemCodes.Payroll;
                        break;
                    }

                case object _ when SystemCodes.Profile.ToString():
                    {
                        queryParam = SystemCodes.Profile;
                        break;
                    }

                case object _ when SystemCodes.Recruitment.ToString():
                    {
                        queryParam = SystemCodes.Recruitment;
                        break;
                    }

                case object _ when SystemCodes.Training.ToString():
                    {
                        queryParam = SystemCodes.Training;
                        break;
                    }

                case object _ when SystemCodes.Meal.ToString():
                    {
                        queryParam = SystemCodes.Meal;
                        break;
                    }
            }

            query = (from p in Context.OT_OTHER_LIST_TYPE
                     where p.GROUP_ID == queryParam
                     orderby p.OT_OTHER_LIST_GROUP.NAME, p.NAME
                     select new OtherListTypeDTO() { ID = p.ID, NAME = p.NAME, CODE = p.CODE, GROUP_ID = p.GROUP_ID, GROUP_NAME = p.OT_OTHER_LIST_GROUP.NAME, IS_SYSTEM = p.IS_SYSTEM, ACTFLG_DB = p.ACTFLG, ACTFLG = p.ACTFLG == "A" ? "Áp dụng" : "Ngừng áp dụng" });
        }
        return query.ToList;
    }

    public List<OtherListTypeDTO> GetOtherListType(string sACT)
    {
        ObjectQuery<OtherListTypeDTO> query;
        if (sACT == "")
            query = (from p in Context.OT_OTHER_LIST_TYPE
                     orderby p.OT_OTHER_LIST_GROUP.NAME, p.NAME
                     select new OtherListTypeDTO() { ID = p.ID, NAME = p.NAME, CODE = p.CODE, GROUP_ID = p.GROUP_ID, GROUP_NAME = p.OT_OTHER_LIST_GROUP.NAME, IS_SYSTEM = p.IS_SYSTEM, ACTFLG = p.ACTFLG == "A" ? "Áp dụng" : "Ngừng áp dụng" });
        else
            query = (from p in Context.OT_OTHER_LIST_TYPE
                     where p.ACTFLG == sACT
                     orderby p.OT_OTHER_LIST_GROUP.NAME, p.NAME
                     select new OtherListTypeDTO() { ID = p.ID, NAME = p.NAME, CODE = p.CODE, GROUP_ID = p.GROUP_ID, GROUP_NAME = p.OT_OTHER_LIST_GROUP.NAME, IS_SYSTEM = p.IS_SYSTEM, ACTFLG = p.ACTFLG == "A" ? "Áp dụng" : "Ngừng áp dụng" });

        // 'Logger.LogInfo(query)
        return query.ToList;
    }

    public bool InsertOtherListType(OtherListTypeDTO objOtherListType, UserLog log)
    {
        OT_OTHER_LIST_TYPE objOtherListTypeData = new OT_OTHER_LIST_TYPE();
        objOtherListTypeData.ID = Utilities.GetNextSequence(Context, Context.OT_OTHER_LIST_TYPE.EntitySet.Name);
        objOtherListTypeData.CODE = objOtherListType.CODE;
        objOtherListTypeData.NAME = objOtherListType.NAME;
        objOtherListTypeData.ACTFLG = objOtherListType.ACTFLG;
        objOtherListTypeData.GROUP_ID = IIf(objOtherListType.GROUP_ID == null, null/* TODO Change to default(_) if this is not a reference type */, objOtherListType.GROUP_ID);
        objOtherListTypeData.CREATED_DATE = DateTime.Now;
        objOtherListTypeData.CREATED_BY = log.Username;
        objOtherListTypeData.CREATED_LOG = log.ComputerName;
        objOtherListTypeData.MODIFIED_DATE = DateTime.Now;
        objOtherListTypeData.MODIFIED_BY = log.Username;
        objOtherListTypeData.MODIFIED_LOG = log.ComputerName;
        Context.OT_OTHER_LIST_TYPE.AddObject(objOtherListTypeData);
        Context.SaveChanges(log);
        return true;
    }

    public bool ModifyOtherListType(OtherListTypeDTO objOtherListType, UserLog log)
    {
        OT_OTHER_LIST_TYPE objOtherListTypeData;
        objOtherListTypeData = (from p in Context.OT_OTHER_LIST_TYPE
                                where p.ID == objOtherListType.ID
                                select p).SingleOrDefault;
        objOtherListTypeData.ID = objOtherListType.ID;
        objOtherListTypeData.CODE = objOtherListType.CODE;
        objOtherListTypeData.NAME = objOtherListType.NAME;
        objOtherListTypeData.GROUP_ID = IIf(objOtherListType.GROUP_ID == null, null/* TODO Change to default(_) if this is not a reference type */, objOtherListType.GROUP_ID);
        objOtherListTypeData.MODIFIED_DATE = DateTime.Now;
        objOtherListTypeData.MODIFIED_BY = log.Username;
        objOtherListTypeData.MODIFIED_LOG = log.ComputerName;
        Context.SaveChanges(log);
        return true;
    }

    public bool ActiveOtherListType(OtherListTypeDTO[] lstOtherListType, string sActive, UserLog log)
    {
        List<OT_OTHER_LIST_TYPE> lstOtherListTypeData;
        List<decimal> lstIDOtherListType = (from p in lstOtherListType.ToList
                                            select p.ID).ToList;
        lstOtherListTypeData = (from p in Context.OT_OTHER_LIST_TYPE
                                where lstIDOtherListType.Contains(p.ID)
                                select p).ToList;
        for (var index = 0; index <= lstOtherListTypeData.Count - 1; index++)
        {
            lstOtherListTypeData[index].ACTFLG = sActive;
            lstOtherListTypeData[index].MODIFIED_DATE = DateTime.Now;
            lstOtherListTypeData[index].MODIFIED_BY = log.Username;
            lstOtherListTypeData[index].MODIFIED_LOG = log.ComputerName;
        }
        Context.SaveChanges(log);
        return true;
    }

    public bool DeleteOtherListType(List<OtherListTypeDTO> lstOtherListType)
    {
        List<OT_OTHER_LIST_TYPE> lstOtherListTypeData;
        List<decimal> lstIDOtherListType = (from p in lstOtherListType.ToList
                                            select p.ID).ToList;
        try
        {
            lstOtherListTypeData = (from p in Context.OT_OTHER_LIST_TYPE
                                    where lstIDOtherListType.Contains(p.ID)
                                    select p).ToList;
            for (var index = 0; index <= lstOtherListTypeData.Count - 1; index++)
                Context.OT_OTHER_LIST_TYPE.DeleteObject(lstOtherListTypeData[index]);
            Context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    ///     ''' Lấy dữ liệu cho combobox
    ///     ''' </summary>
    ///     ''' <param name="_combolistDTO">Trả về dữ liệu combobox</param>
    ///     ''' <returns>TRUE: Success</returns>
    ///     ''' <remarks></remarks>
    public bool GetComboList(ref ComboBoxDataDTO _combolistDTO)
    {
        var query;
        try
        {
            if (_combolistDTO.GET_MODULE)
            {
                query = (from p in Context.SE_MODULE
                         orderby p.NAME.ToUpper
                         select new ModuleDTO() { ID = p.ID, MID = p.MID, NAME = p.NAME }).ToList;
                _combolistDTO.LIST_MODULE = query;
            }
            if (_combolistDTO.GET_FUNCTION_GROUP)
            {
                query = (from p in Context.SE_FUNCTION_GROUP
                         orderby p.NAME.ToUpper
                         select new FunctionGroupDTO() { ID = p.ID, CODE = p.CODE, NAME = p.NAME }).ToList;
                _combolistDTO.LIST_FUNCTION_GROUP = query;
            }

            if (_combolistDTO.GET_USER_GROUP)
            {
                query = (from p in Context.SE_GROUP
                         orderby p.NAME.ToUpper
                         select new GroupDTO() { ID = p.ID, CODE = p.CODE, NAME = p.NAME }).ToList;
                _combolistDTO.LIST_USER_GROUP = query;
            }

            if (_combolistDTO.GET_FUNCTION)
            {
                query = (from p in Context.SE_FUNCTION
                         orderby p.NAME.ToUpper
                         select new FunctionDTO() { ID = p.ID, FID = p.FID, NAME = p.NAME }).ToList;
                _combolistDTO.LIST_FUNCTION = query;
            }

            if (_combolistDTO.GET_ACTION_NAME)
            {
                query = (from p in Context.OT_OTHER_LIST
                         where p.OT_OTHER_LIST_TYPE.CODE == "ACTION_TYPE"
                         orderby p.NAME_VN.ToUpper
                         select new OtherListDTO() { ID = p.ID, CODE = p.CODE, NAME_VN = p.NAME_VN, NAME_EN = p.NAME_EN }).ToList;
                _combolistDTO.LIST_ACTION_NAME = query;
            }

            if (_combolistDTO.GET_OTHER_LIST_GROUP)
            {
                query = (from p in Context.OT_OTHER_LIST_GROUP
                         orderby p.NAME.ToUpper
                         select new OtherListGroupDTO() { ID = p.ID, NAME = p.NAME }).ToList;
                _combolistDTO.LIST_OTHER_LIST_GROUP = query;
            }
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    /// <summary>
    ///     ''' Gọi hàm gửi mail queue trong DB
    ///     ''' </summary>
    ///     ''' <returns></returns>
    ///     ''' <remarks></remarks>
    public bool SendMail()
    {
        // Dim sMail As System.Net.Mail.SmtpClient
        string _server;
        string _port;
        string _account;
        string _password;
        bool _ssl;
        bool _authen;
        try
        {
            Dictionary<string, string> config;
            config = GetConfig(ModuleID.All);
            _server = config.ContainsKey("MailServer") ? config["MailServer"] : "";
            _port = config.ContainsKey("MailPort") ? config["MailPort"] : "";
            _account = config.ContainsKey("MailAccount") ? config["MailAccount"] : "";
            _password = config.ContainsKey("MailAccountPassword") ? config["MailAccountPassword"] : "";
            _ssl = config.ContainsKey("MailIsSSL") ? config["MailIsSSL"] : 0;
            _authen = config.ContainsKey("MailIsAuthen") ? config["MailIsAuthen"] : 0;
            if (_password != "")
                _password = new EncryptData().DecryptString(_password);
            if (_server == "")
                return false;

            // Dim lstAtt = (From p In Context.SE_MAIL
            // Select p
            // Where p.ACTFLG = "A").ToList

            // For Each itm In lstAtt
            // If itm.ATTACHMENT <> "" Then
            // For Each item In itm.ATTACHMENT.Split(";")
            // If File.Exists(item) Then

            // Try

            // File.Delete(item)
            // Catch ex As Exception
            // WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iCommon")

            // End Try
            // End If
            // Next
            // End If
            // Next

            var isExist = (from p in Context.SE_MAIL
                           where p.ACTFLG == "P"
                           select p).Any;

            if (!isExist)
            {
                ;/* Cannot convert LocalDeclarationStatementSyntax, System.NotImplementedException: Conversion for query clause with kind 'SkipClause' not implemented
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.<>c__DisplayClass99_0.<ConvertQueryBodyClause>b__0(QueryClauseSyntax _)
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
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.ConvertInitializer(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.CommonConversions.SplitVariableDeclarations(VariableDeclaratorSyntax declarator)
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.LocalDeclarationStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 
                Dim query = (From p In Context.SE_MAIL Select p
                             Where p.ACTFLG = "I"
                             Order By p.ACTFLG
                             Skip 0 Take 50).ToList

 */
                if (query.Count == 0)
                    return true;

                foreach (var item in query)
                    item.ACTFLG = "P";
                Context.SaveChanges();
                // Config mail server

                using (System.Net.Mail.SmtpClient sMail = new System.Net.Mail.SmtpClient(_server))
                {
                    try
                    {
                        if (_port != "")
                            sMail.Port = _port;
                        sMail.EnableSsl = _ssl;
                        if (_authen)
                            sMail.Credentials = new NetworkCredential(_account, _password);

                        for (var i = 0; i <= query.Count - 1; i++)
                        {
                            try
                            {
                                using (System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage())
                                {
                                    message.From = new System.Net.Mail.MailAddress(query(i).MAIL_FROM);
                                    foreach (var item in query(i).MAIL_TO.Split(";"))
                                    {
                                        if (item.Trim != "")
                                        {
                                            try
                                            {
                                                message.To.Add(new System.Net.Mail.MailAddress(item.Trim));
                                            }
                                            catch (Exception ex)
                                            {
                                            }
                                        }
                                    }

                                    if (query(i).MAIL_CC != null)
                                    {
                                        foreach (var item in query(i).MAIL_CC.Split(";"))
                                        {
                                            if (item.Trim != "")
                                            {
                                                try
                                                {
                                                    message.CC.Add(new System.Net.Mail.MailAddress(item));
                                                }
                                                catch (Exception ex)
                                                {
                                                }
                                            }
                                        }
                                    }


                                    message.IsBodyHtml = true;
                                    message.BodyEncoding = ASCIIEncoding.UTF8;
                                    message.Subject = query(i).SUBJECT;
                                    message.Priority = System.Net.Mail.MailPriority.Normal;
                                    message.Body = query(i).CONTENT;

                                    if (query(i).ATTACHMENT != null)
                                    {
                                        foreach (var item in query(i).ATTACHMENT.Split(";"))
                                        {
                                            if (item.Trim != "")
                                            {
                                                if (File.Exists(item.Trim))
                                                {
                                                    System.Net.Mail.Attachment attachment;
                                                    attachment = new System.Net.Mail.Attachment(item.Trim);
                                                    message.Attachments.Add(attachment);
                                                }
                                            }
                                        }
                                    }


                                    sMail.Send(message);
                                    foreach (var att in message.Attachments)
                                        att.Dispose();
                                }
                                query(i).ACTFLG = "A";
                                query(i).SEND_DATE = DateTime.Now;
                                if (query(i).ATTACHMENT != null)
                                {
                                    foreach (var item in query(i).ATTACHMENT.Split(";"))
                                    {
                                        if (item.Trim != "")
                                        {
                                            try
                                            {
                                                File.Delete(item.Trim);
                                            }
                                            catch (Exception ex)
                                            {
                                                WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iCommon");
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iCommon");
                                query(i).ACTFLG = "I";
                                if (query(i).FAIL_COUNT == null)
                                    query(i).FAIL_COUNT = 1;
                                else
                                    query(i).FAIL_COUNT = query(i).FAIL_COUNT + 1;

                                if (query(i).FAIL_COUNT > 10)
                                    query(i).ACTFLG = "E";
                            }
                        }
                        Context.SaveChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        if (sMail != null)
                            sMail.Dispose();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod().Name, "iCommon");
        }
    }

    public void InsertMail(string _from, string _to, string _subject, string _content, string _cc = "", string _bcc = "", string _viewName = "")
    {
        try
        {
            SE_MAIL _newMail = new SE_MAIL();
            _newMail.ID = Utilities.GetNextSequence(Context, Context.SE_MAIL.EntitySet.Name);
            _newMail.MAIL_FROM = _from;
            _newMail.MAIL_TO = _to;
            _newMail.MAIL_CC = _cc;
            _newMail.MAIL_BCC = _bcc;
            _newMail.SUBJECT = _subject;
            _newMail.CONTENT = _content;
            _newMail.VIEW_NAME = _viewName;
            _newMail.ACTFLG = "I";
            Context.SE_MAIL.AddObject(_newMail);
            Context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }




    public List<LdapDTO> GetLdap(LdapDTO _filter, int PageIndex, int PageSize, ref int Total, string Sorts = "LDAP_NAME desc")
    {
        try
        {
            var query = from p in Context.SE_LDAP
                        select new LdapDTO() { ID = p.ID, BASE_DN = p.BASE_DN, DOMAIN_NAME = p.DOMAIN_NAME, LDAP_NAME = p.LDAP_NAME };

            var lst = query;

            if (_filter.BASE_DN != "")
                lst = lst.Where(p => p.BASE_DN.ToUpper.Contains(_filter.BASE_DN.ToUpper));
            if (_filter.DOMAIN_NAME != "")
                lst = lst.Where(p => p.DOMAIN_NAME.ToUpper.Contains(_filter.DOMAIN_NAME.ToUpper));
            if (_filter.LDAP_NAME != "")
                lst = lst.Where(p => p.LDAP_NAME.ToUpper.Contains(_filter.LDAP_NAME.ToUpper));
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

    public bool InsertLdap(LdapDTO objLdap, UserLog log, ref decimal gID)
    {
        SE_LDAP objLdapData = new SE_LDAP();
        int iCount = 0;
        try
        {
            objLdapData.ID = Utilities.GetNextSequence(Context, Context.SE_LDAP.EntitySet.Name);
            objLdapData.BASE_DN = objLdap.BASE_DN;
            objLdapData.DOMAIN_NAME = objLdap.DOMAIN_NAME;
            objLdapData.LDAP_NAME = objLdap.LDAP_NAME;
            Context.SE_LDAP.AddObject(objLdapData);
            Context.SaveChanges(log);
            gID = objLdapData.ID;
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool ModifyLdap(LdapDTO objLdap, UserLog log, ref decimal gID)
    {
        SE_LDAP objLdapData;
        try
        {
            objLdapData = (from p in Context.SE_LDAP
                           where p.ID == objLdap.ID
                           select p).FirstOrDefault;
            objLdapData.BASE_DN = objLdap.BASE_DN;
            objLdapData.DOMAIN_NAME = objLdap.DOMAIN_NAME;
            objLdapData.LDAP_NAME = objLdap.LDAP_NAME;
            Context.SaveChanges(log);
            gID = objLdapData.ID;
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool DeleteLdap(List<decimal> lstID)
    {
        List<SE_LDAP> lstLdapData;
        try
        {
            lstLdapData = (from p in Context.SE_LDAP
                           where lstID.Contains(p.ID)
                           select p).ToList;
            for (var index = 0; index <= lstLdapData.Count - 1; index++)
                Context.SE_LDAP.DeleteObject(lstLdapData[index]);

            Context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
