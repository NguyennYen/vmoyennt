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
using AttendanceDAL;
using Framework.Data;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
// NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
namespace AttendanceBusiness.ServiceContracts
{
    [ServiceContract()]
    public interface IAttendanceBusiness
    {
        [OperationContract()]
        DataSet GetDataFromOrg(ParamDTO obj, UserLog log);

        [OperationContract()]
        bool GetComboboxData(ref ComboBoxDataDTO cbxData);

        [OperationContract()]
        DataTable LOAD_PERIOD(AT_PERIODDTO obj, UserLog log);
        [OperationContract()]
        List<AT_PERIODDTO> LOAD_PERIODBylinq(AT_PERIODDTO obj, UserLog log);
        [OperationContract()]
        AT_PERIODDTO LOAD_PERIODByID(AT_PERIODDTO obj, UserLog log);
        [OperationContract()]
        bool CLOSEDOPEN_PERIOD(ParamDTO param, Framework.Data.UserLog log);
        [OperationContract()]
        bool IS_PERIOD_PAYSTATUS(ParamDTO _param, bool isAfter, UserLog log);
        [OperationContract()]
        bool IS_PERIODSTATUS(ParamDTO _param, UserLog log);
        [OperationContract()]
        bool IS_PERIODSTATUS_BY_DATE(ParamDTO _param, UserLog log);

        [OperationContract()]
        List<AT_DATAINOUTDTO> GetDataInout(AT_DATAINOUTDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "EMPLOYEE_CODE, WORKINGDAY", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);
        [OperationContract()]
        bool InsertDataInout(List<AT_DATAINOUTDTO> lstDataInout, DateTime fromDate, DateTime toDate, UserLog log);

        [OperationContract()]
        bool ModifyDataInout(AT_DATAINOUTDTO objDataInout, UserLog log, ref decimal gID);

        [OperationContract()]
        bool DeleteDataInout(AT_DATAINOUTDTO[] lstDataInout);


        [OperationContract()]
        List<AT_REGISTER_OTDTO> GetRegisterOT(AT_REGISTER_OTDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);
        [OperationContract()]
        List<OT_OTHERLIST_DTO> GetListHsOT();
        [OperationContract()]
        bool InsertRegisterOT(AT_REGISTER_OTDTO objRegisterOT, UserLog log, ref decimal gID);
        [OperationContract()]
        bool InsertDataRegisterOT(List<AT_REGISTER_OTDTO> objRegisterOTList, AT_REGISTER_OTDTO objRegisterOT, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ModifyRegisterOT(AT_REGISTER_OTDTO objRegisterOT, UserLog log, ref decimal gID);
        [OperationContract()]
        AT_REGISTER_OTDTO GetRegisterById(decimal? _id);
        [OperationContract()]
        bool ValidateRegisterOT(AT_REGISTER_OTDTO _validate);
        [OperationContract()]
        bool DeleteRegisterOT(List<decimal> lstID, ParamDTO _param, decimal period_id, List<decimal?> listEmployeeId, UserLog log);
        [OperationContract()]
        bool CheckImporAddNewtOT(AT_REGISTER_OTDTO objRegisterOT);
        [OperationContract()]
        bool CheckDataListImportAddNew(List<AT_REGISTER_OTDTO> objRegisterOTList, AT_REGISTER_OTDTO objRegisterOT, ref string strEmployeeCode);


        [OperationContract()]
        List<AT_TIME_TIMESHEET_MACHINETDTO> GetMachines(AT_TIME_TIMESHEET_MACHINETDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "EMPLOYEE_ID, WORKINGDAY", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);
        [OperationContract()]
        bool Init_TimeTImesheetMachines(ParamDTO _param, UserLog log, DateTime p_fromdate, DateTime p_enddate, decimal P_ORG_ID, List<decimal?> lstEmployee);


        [OperationContract()]
        DataSet GetCCT(AT_TIME_TIMESHEET_DAILYDTO param, UserLog log);

        [OperationContract()]
        DataTable GetCCT_Origin(AT_TIME_TIMESHEET_DAILYDTO param, UserLog log);

        [OperationContract()]
        bool ModifyLeaveSheetDaily(AT_TIME_TIMESHEET_DAILYDTO objLeave, UserLog log, ref decimal gID);

        [OperationContract()]
        bool InsertLeaveSheetDaily(DataTable dtData, UserLog log, decimal PeriodID);

        [OperationContract()]
        AT_TIME_TIMESHEET_DAILYDTO GetTimeSheetDailyById(AT_TIME_TIMESHEET_DAILYDTO obj);

        [OperationContract()]
        bool Cal_TimeTImesheet_OT(ParamDTO _param, UserLog log, decimal? p_period_id, decimal P_ORG_ID, List<decimal?> lstEmployee);

        [OperationContract()]
        DataSet GetSummaryOT(AT_TIME_TIMESHEET_OTDTO param, UserLog log);

        [OperationContract()]
        bool Cal_TimeTImesheet_NB(ParamDTO _param, UserLog log, decimal? p_period_id, decimal P_ORG_ID, List<decimal?> lstEmployee);

        [OperationContract()]
        DataSet GetSummaryNB(AT_TIME_TIMESHEET_NBDTO param, UserLog log);

        [OperationContract()]
        bool ModifyLeaveSheetOt(AT_TIME_TIMESHEET_OTDTO objLeave, UserLog log, ref decimal gID);

        [OperationContract()]
        bool InsertLeaveSheetOt(AT_TIME_TIMESHEET_OTDTO objLeave, UserLog log, ref decimal gID);

        [OperationContract()]
        AT_TIME_TIMESHEET_OTDTO GetTimeSheetOtById(AT_TIME_TIMESHEET_OTDTO obj);

        [OperationContract()]
        List<AT_TIME_TIMESHEET_MONTHLYDTO> GetTimeSheet(AT_TIME_TIMESHEET_MONTHLYDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);
        [OperationContract()]
        bool CAL_TIME_TIMESHEET_MONTHLY(ParamDTO param, List<decimal?> lstEmployee, Framework.Data.UserLog log);

        [OperationContract()]
        DataTable GetTimeSheetPortal(AT_TIME_TIMESHEET_DAILYDTO _filter);

        [OperationContract()]
        void ValidateTimesheet(AT_TIME_TIMESHEET_MONTHLYDTO _validate, string sType, UserLog log);



        [OperationContract()]
        List<AT_TIME_RICEDTO> GetDelareRice(AT_TIME_RICEDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);
        [OperationContract()]
        bool InsertDelareRice(AT_TIME_RICEDTO objDelareRice, UserLog log, ref decimal gID);

        [OperationContract()]
        bool InsertDelareRiceList(List<AT_TIME_RICEDTO> objDelareRiceList, AT_TIME_RICEDTO objDelareRice, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ActiveDelareRice(List<decimal> lstID, UserLog log, string bActive);
        [OperationContract()]
        bool ModifyDelareRice(AT_TIME_RICEDTO objDelareRice, UserLog log, ref decimal gID);
        [OperationContract()]
        bool ValidateDelareRice(AT_TIME_RICEDTO _validate);
        [OperationContract()]
        AT_TIME_RICEDTO GetDelareRiceById(decimal? _id);
        [OperationContract()]
        bool DeleteDelareRice(List<decimal> lstID);


        [OperationContract()]
        List<AT_DECLARE_ENTITLEMENTDTO> GetDelareEntitlementNB(AT_DECLARE_ENTITLEMENTDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);
        [OperationContract()]
        bool InsertDelareEntitlementNB(AT_DECLARE_ENTITLEMENTDTO objDelareEntitlementNB, UserLog log, ref decimal gID, ref bool checkMonthNB, ref bool checkMonthNP);
        [OperationContract()]
        bool InsertMultipleDelareEntitlementNB(List<AT_DECLARE_ENTITLEMENTDTO> objDelareEntitlementlist, AT_DECLARE_ENTITLEMENTDTO objDelareEntitlementNB, UserLog log, ref decimal gID, ref bool checkMonthNB, ref bool checkMonthNP);
        [OperationContract()]
        bool ImportDelareEntitlementNB(DataTable dtData, UserLog log, ref decimal gID, ref bool checkMonthNB, ref bool checkMonthNP);
        [OperationContract()]
        bool ActiveDelareEntitlementNB(List<decimal> lstID, UserLog log, string bActive);
        [OperationContract()]
        bool ModifyDelareEntitlementNB(AT_DECLARE_ENTITLEMENTDTO objDelareEntitlementNB, UserLog log, ref decimal gID);
        [OperationContract()]
        AT_DECLARE_ENTITLEMENTDTO GetDelareEntitlementNBById(decimal? _id);
        [OperationContract()]
        bool DeleteDelareEntitlementNB(List<decimal> lstID);
        [OperationContract()]
        bool ValidateMonthThamNien(AT_DECLARE_ENTITLEMENTDTO _validate);
        [OperationContract()]
        bool ValidateMonthPhepNam(AT_DECLARE_ENTITLEMENTDTO _validate);
        [OperationContract()]
        bool ValidateMonthNghiBu(AT_DECLARE_ENTITLEMENTDTO _validate);


        [OperationContract()]
        bool Cal_TimeTImesheet_Rice(ParamDTO _param, UserLog log, decimal? p_period_id, decimal P_ORG_ID, List<decimal?> lstEmployee);
        [OperationContract()]
        DataSet GetSummaryRice(AT_TIME_TIMESHEET_RICEDTO param, UserLog log);
        [OperationContract()]
        bool ModifyLeaveSheetRice(AT_TIME_TIMESHEET_RICEDTO objLeave, UserLog log, ref decimal gID);
        [OperationContract()]
        bool ApprovedTimeSheetRice(AT_TIME_TIMESHEET_RICEDTO objLeave, UserLog log, ref decimal gID);
        [OperationContract()]
        bool InsertLeaveSheetRice(AT_TIME_TIMESHEET_RICEDTO objLeave, UserLog log, ref decimal gID);

        [OperationContract()]
        AT_TIME_TIMESHEET_RICEDTO GetTimeSheetRiceById(AT_TIME_TIMESHEET_RICEDTO obj);

        [OperationContract()]
        List<AT_LEAVESHEETDTO> GetLeaveSheet(AT_LEAVESHEETDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);
        [OperationContract()]
        bool InsertLeaveSheet(AT_LEAVESHEETDTO objRegisterOT, UserLog log, ref decimal gID);

        [OperationContract()]
        bool InsertLeaveSheetList(List<AT_LEAVESHEETDTO> objRegisterList, AT_LEAVESHEETDTO objRegisterOT, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ModifyLeaveSheet(AT_LEAVESHEETDTO objRegisterOT, UserLog log, ref decimal gID);
        [OperationContract()]
        DataTable GetTotalDAY(int P_EMPLOYEE_ID, int P_TYPE_MANUAL, DateTime P_FROM_DATE, DateTime P_TO_DATE);
        [OperationContract()]
        DataTable GetCAL_DAY_LEAVE_OLD(int P_EMPLOYEE_ID, DateTime P_FROM_DATE, DateTime P_TO_DATE);
        [OperationContract()]
        DataTable GetTotalPHEPNAM(int P_EMPLOYEE_ID, DateTime Date_cal, int P_TYPE_LEAVE_ID);
        [OperationContract()]
        DataTable GetTotalPHEPBU(int P_EMPLOYEE_ID, DateTime Date_cal, int P_TYPE_LEAVE_ID);
        [OperationContract()]
        List<AT_LEAVESHEETDTO> GetPHEPBUCONLAI(List<AT_LEAVESHEETDTO> lstEmpID, decimal? _year);

        [OperationContract()]
        AT_LEAVESHEETDTO GetLeaveById(decimal? _id);

        [OperationContract()]
        bool ValidateLeaveSheet(AT_LEAVESHEETDTO _validate);
        [OperationContract()]
        AT_ENTITLEMENTDTO GetPhepNam(decimal? _id, decimal? _year);
        [OperationContract()]
        AT_COMPENSATORYDTO GetNghiBu(decimal? _id, decimal? _year);
        [OperationContract()]
        bool DeleteLeaveSheet(List<AT_LEAVESHEETDTO> lstID, ParamDTO _param, decimal period_id, List<decimal?> listEmployeeId, UserLog log);
        [OperationContract()]
        DataTable checkLeaveImport(DataTable dtData);
        [OperationContract()]
        bool CheckDataCheckworksign(List<AT_REGISTER_OTDTO> objworksignList, AT_REGISTER_OTDTO objRegisterOT, ref string strEmployeeCode);
        [OperationContract()]
        bool CheckDataCheckworksignImport(AT_REGISTER_OTDTO objRegisterOT);
        [OperationContract()]
        bool Check_DataRegister_OT(ref string _param, UserLog log, DateTime? Startdate, DateTime? Enddate, decimal? period_id);
        [OperationContract()]
        bool Check_WorkSing_default(ParamDTO obj, UserLog log, ref string Employee_ID);

        [OperationContract()]
        List<AT_LATE_COMBACKOUTDTO> GetDSVM(AT_LATE_COMBACKOUTDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);
        [OperationContract()]
        AT_LATE_COMBACKOUTDTO GetLate_CombackoutById(decimal? _id);
        [OperationContract()]
        bool ImportLate_combackout(AT_LATE_COMBACKOUTDTO objDataInout, UserLog log, ref decimal gID);
        [OperationContract()]
        bool InsertLate_combackout(List<AT_LATE_COMBACKOUTDTO> objRegisterDMVSList, AT_LATE_COMBACKOUTDTO objDataInout, UserLog log, ref decimal gID);
        [OperationContract()]
        bool ModifyLate_combackout(AT_LATE_COMBACKOUTDTO objDataInout, UserLog log, ref decimal gID);
        [OperationContract()]
        bool ValidateLate_combackout(AT_LATE_COMBACKOUTDTO _validate);
        [OperationContract()]
        bool DeleteLate_combackout(List<decimal> lstID, ParamDTO _param, decimal period_id, List<decimal?> listEmployeeId, UserLog log);

        [OperationContract()]
        bool CALCULATE_ENTITLEMENT_NB(ParamDTO param, List<decimal?> listEmployeeId, UserLog log);
        [OperationContract()]
        List<AT_COMPENSATORYDTO> GetNB(AT_COMPENSATORYDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);

        [OperationContract()]
        bool CALCULATE_ENTITLEMENT(ParamDTO param, List<decimal?> listEmployeeId, UserLog log);
        [OperationContract()]
        List<AT_ENTITLEMENTDTO> GetEntitlement(AT_ENTITLEMENTDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);

        [OperationContract()]
        DataSet GET_WORKSIGN(AT_WORKSIGNDTO param, UserLog log);
        [OperationContract()]
        bool InsertWORKSIGNByImport(DataTable dtData, decimal period_id, UserLog log, ref string lstEmp);
        [OperationContract()]
        void InsertWorkSign(List<AT_WORKSIGNDTO> objWorkSigns, AT_WORKSIGNDTO objWork, DateTime p_fromdate, DateTime? p_endDate, UserLog log, ref decimal gID);
        [OperationContract()]
        bool ValidateWORKSIGN(AT_WORKSIGNDTO objWORKSIGN);

        [OperationContract()]
        bool ModifyWORKSIGN(AT_WORKSIGNDTO objWORKSIGN, UserLog log, ref decimal gID);

        [OperationContract()]
        bool DeleteWORKSIGN(AT_WORKSIGNDTO[] lstWORKSIGN);
        [OperationContract()]
        DataTable GETSIGNDEFAULT(ParamDTO param, UserLog log);
        [OperationContract()]
        bool Del_WorkSign_ByEmp(decimal employee_id, DateTime p_From, DateTime p_to);
        [OperationContract()]
        bool CheckOffInMonth(ParamDTO _param, UserLog log = null/* TODO Change to default(_) if this is not a reference type */);
        [OperationContract()]
        bool CheckOffInMonthTable(DataTable dtData, decimal p_period_id, ref DataTable dtDataError);
        [OperationContract()]
        AT_WORKSIGNDTO GET_WORKSIGN_BYEMP(decimal Emp_ID, DateTime working_day);

        [OperationContract()]
        List<AT_HOLIDAYDTO> GetHoliday(AT_HOLIDAYDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc");
        [OperationContract()]
        bool InsertHOLIDAY(AT_HOLIDAYDTO objHOLIDAY, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ValidateHOLIDAY(AT_HOLIDAYDTO objHOLIDAY);

        [OperationContract()]
        bool ModifyHOLIDAY(AT_HOLIDAYDTO objHOLIDAY, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ActiveHoliday(List<decimal> lstID, UserLog log, string bActive);

        [OperationContract()]
        bool DeleteHOLIDAY(List<decimal> lstID);


        [OperationContract()]
        List<AT_HOLIDAY_GENERALDTO> GetHolidayGerenal(AT_HOLIDAY_GENERALDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc");
        [OperationContract()]
        bool InsertHolidayGerenal(AT_HOLIDAY_GENERALDTO objHOLIDAYGR, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ValidateHolidayGerenal(AT_HOLIDAY_GENERALDTO objHOLIDAYGR);

        [OperationContract()]
        bool ModifyHolidayGerenal(AT_HOLIDAY_GENERALDTO objHOLIDAYGR, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ActiveHolidayGerenal(List<decimal> lstID, UserLog log, string bActive);

        [OperationContract()]
        bool DeleteHolidayGerenal(List<decimal> lstID);

        [OperationContract()]
        List<AT_TIME_MANUALDTO> GetSignByPage(string pagecode);
        [OperationContract()]
        List<AT_FMLDTO> GetAT_FML(AT_FMLDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc");
        [OperationContract()]
        bool InsertAT_FML(AT_FMLDTO objATFML, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ValidateAT_FML(AT_FMLDTO objATFML);

        [OperationContract()]
        bool ModifyAT_FML(AT_FMLDTO objATFML, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ActiveAT_FML(List<decimal> lstID, UserLog log, string bActive);

        [OperationContract()]
        bool DeleteAT_FML(List<decimal> lstID);

        [OperationContract()]
        List<AT_GSIGNDTO> GetAT_GSIGN(AT_GSIGNDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc");
        [OperationContract()]
        bool InsertAT_GSIGN(AT_GSIGNDTO objGSIGN, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ValidateAT_GSIGN(AT_GSIGNDTO objGSIGN);

        [OperationContract()]
        bool ModifyAT_GSIGN(AT_GSIGNDTO objGSIGN, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ActiveAT_GSIGN(List<decimal> lstID, UserLog log, string bActive);

        [OperationContract()]
        bool DeleteAT_GSIGN(List<decimal> lstID);

        [OperationContract()]
        List<AT_DMVSDTO> GetAT_DMVS(AT_DMVSDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc");
        [OperationContract()]
        bool InsertAT_DMVS(AT_DMVSDTO objData, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ValidateAT_DMVS(AT_DMVSDTO objData);

        [OperationContract()]
        bool ModifyAT_DMVS(AT_DMVSDTO objData, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ActiveAT_DMVS(List<decimal> lstID, UserLog log, string bActive);

        [OperationContract()]
        bool DeleteAT_DMVS(List<decimal> lstID);

        [OperationContract()]
        DataTable GetTerminal(AT_TERMINALSDTO obj, UserLog log);

        [OperationContract()]
        DataTable GetTerminalMeal(AT_TERMINALSDTO obj, UserLog log);

        [OperationContract()]
        DataTable GetTerminalAuto();

        [OperationContract()]
        bool UpdateTerminalLastTime(AT_TERMINALSDTO obj, bool isMeal = false);

        [OperationContract()]
        bool UpdateTerminalStatus(AT_TERMINALSDTO obj, bool isMeal = false);

        [OperationContract()]
        List<AT_SWIPE_DATADTO> GetSwipeData(AT_SWIPE_DATADTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "iTime_id, VALTIME desc");

        [OperationContract()]
        List<AT_SWIPE_DATA_MEALDTO> GetSwipeDataMeal(AT_SWIPE_DATA_MEALDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "iTime_id, VALTIME desc");

        [OperationContract()]
        bool ImportSwipeDataAuto(List<AT_SWIPE_DATADTO> lstSwipeData, UserLog log, bool isMeal = false);

        [OperationContract()]
        bool InsertSwipeDataImport(List<AT_SWIPE_DATADTO> objDelareRice, UserLog log, bool isMeal);


        [OperationContract()]
        List<AT_SHIFTDTO> GetAT_SHIFT(AT_SHIFTDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc");
        [OperationContract()]
        bool InsertAT_SHIFT(AT_SHIFTDTO objData, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ValidateAT_SHIFT(AT_SHIFTDTO objData);

        [OperationContract()]
        bool ModifyAT_SHIFT(AT_SHIFTDTO objData, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ActiveAT_SHIFT(List<decimal> lstID, UserLog log, string bActive);

        [OperationContract()]
        bool DeleteAT_SHIFT(List<decimal> lstID);

        [OperationContract()]
        DataTable GetAT_TIME_MANUALBINCOMBO();

        [OperationContract()]
        List<AT_HOLIDAY_OBJECTDTO> GetAT_Holiday_Object(AT_HOLIDAY_OBJECTDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc");
        [OperationContract()]
        bool InsertAT_Holiday_Object(AT_HOLIDAY_OBJECTDTO objHoliO, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ValidateAT_Holiday_Object(AT_HOLIDAY_OBJECTDTO objHoliO);

        [OperationContract()]
        bool ModifyAT_Holiday_Object(AT_HOLIDAY_OBJECTDTO objHoliO, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ActiveAT_Holiday_Object(List<decimal> lstID, UserLog log, string bActive);

        [OperationContract()]
        bool DeleteAT_Holiday_Object(List<decimal> lstID);

        [OperationContract()]
        List<AT_SETUP_SPECIALDTO> GetAT_SETUP_SPECIAL(AT_SETUP_SPECIALDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc");
        [OperationContract()]
        bool InsertAT_SETUP_SPECIAL(AT_SETUP_SPECIALDTO objData, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ValidateAT_SETUP_SPECIAL(AT_SETUP_SPECIALDTO objData);

        [OperationContract()]
        bool ModifyAT_SETUP_SPECIAL(AT_SETUP_SPECIALDTO objData, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ActiveAT_SETUP_SPECIAL(List<decimal> lstID, UserLog log, string bActive);

        [OperationContract()]
        bool DeleteAT_SETUP_SPECIAL(List<decimal> lstID);

        [OperationContract()]
        List<AT_SETUP_TIME_EMPDTO> GetAT_SETUP_TIME_EMP(AT_SETUP_TIME_EMPDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc");
        [OperationContract()]
        bool InsertAT_SETUP_TIME_EMP(AT_SETUP_TIME_EMPDTO objData, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ValidateAT_SETUP_TIME_EMP(AT_SETUP_TIME_EMPDTO objData);

        [OperationContract()]
        bool ModifyAT_SETUP_TIME_EMP(AT_SETUP_TIME_EMPDTO objData, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ActiveAT_SETUP_TIME_EMP(List<decimal> lstID, UserLog log, string bActive);

        [OperationContract()]
        bool DeleteAT_SETUP_TIME_EMP(List<decimal> lstID);

        [OperationContract()]
        List<AT_TERMINALSDTO> GetAT_TERMINAL(AT_TERMINALSDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);
        [OperationContract()]
        List<AT_TERMINALSDTO> GetAT_TERMINAL_STATUS(AT_TERMINALSDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);

        [OperationContract()]
        bool InsertAT_TERMINAL(AT_TERMINALSDTO objData, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ValidateAT_TERMINAL(AT_TERMINALSDTO objData);

        [OperationContract()]
        bool ModifyAT_TERMINAL(AT_TERMINALSDTO objData, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ActiveAT_TERMINAL(List<decimal> lstID, UserLog log, string bActive);

        [OperationContract()]
        bool DeleteAT_TERMINAL(List<decimal> lstID);

        [OperationContract()]
        List<AT_SIGNDEFAULTDTO> GetAT_SIGNDEFAULT(AT_SIGNDEFAULTDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);
        [OperationContract()]
        bool InsertAT_SIGNDEFAULT(AT_SIGNDEFAULTDTO objSIGNDEF, UserLog log, ref decimal gID);

        [OperationContract()]
        DataTable GetAT_ListShift();

        [OperationContract()]
        DataTable GetAT_PERIOD();

        [OperationContract()]
        DataTable GetEmployeeID(string employee_code, decimal period_id);

        [OperationContract()]
        DataTable GetEmployeeIDInSign(string employee_code);
        [OperationContract()]
        DataTable GetEmployeeByTimeID(decimal time_id);

        [OperationContract()]
        bool ModifyAT_SIGNDEFAULT(AT_SIGNDEFAULTDTO objSIGNDEF, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ValidateAT_SIGNDEFAULT(AT_SIGNDEFAULTDTO objSIGNDEF);

        [OperationContract()]
        bool ActiveAT_SIGNDEFAULT(List<decimal> lstID, UserLog log, string bActive);

        [OperationContract()]
        bool DeleteAT_SIGNDEFAULT(List<decimal> lstID);

        [OperationContract()]
        List<AT_TIMESHEET_REGISTERDTO> GetPlanningAppointmentByEmployee(decimal empid, DateTime startdate, DateTime enddate, List<AT_TIME_MANUALDTO> listSign);
        [OperationContract()]
        bool InsertPortalRegister(AT_PORTAL_REG_DTO itemRegister, UserLog log);
        [OperationContract()]
        List<DateTime> GetHolidayByCalender(DateTime startdate, DateTime enddate);
        [OperationContract()]
        List<AT_TIMESHEET_REGISTERDTO> GetRegisterAppointmentInPortalByEmployee(decimal empid, DateTime startdate, DateTime enddate, List<AT_TIME_MANUALDTO> listSign, List<short> status);
        [OperationContract()]
        decimal GetTotalLeaveInYear(decimal empid, decimal p_year);

        [OperationContract()]
        bool DeletePortalRegisterByDate(List<AT_TIMESHEET_REGISTERDTO> listappointment, List<AT_TIME_MANUALDTO> listSign);
        [OperationContract()]
        bool DeletePortalRegister(decimal id);
        [OperationContract()]
        string SendRegisterToApprove(List<decimal> objLstRegisterId, string process, string currentUrl);


        [OperationContract()]
        List<AT_TIME_MANUALDTO> GetListSignCode(string gSignCode);
        [OperationContract()]
        List<AT_PORTAL_REG_DTO> GetListWaitingForApprove(decimal approveId, string process, ATRegSearchDTO filter);
        [OperationContract()]
        bool ApprovePortalRegister(Guid regID, decimal approveId, int status, string note, string currentUrl, string process, UserLog log);
        [OperationContract()]
        DataTable GetEmployeeList();
        [OperationContract()]
        DataTable GetLeaveDay(DateTime dDate);

        [OperationContract()]
        List<AT_TIME_MANUALDTO> GetAT_TIME_MANUAL(AT_TIME_MANUALDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc");
        [OperationContract()]
        AT_TIME_MANUALDTO GetAT_TIME_MANUALById(decimal? _id);
        [OperationContract()]
        bool InsertAT_TIME_MANUAL(AT_TIME_MANUALDTO objHOLIDAY, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ValidateAT_TIME_MANUAL(AT_TIME_MANUALDTO objHOLIDAY);

        [OperationContract()]
        bool ModifyAT_TIME_MANUAL(AT_TIME_MANUALDTO objHOLIDAY, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ActiveAT_TIME_MANUAL(List<decimal> lstID, UserLog log, string bActive);

        [OperationContract()]
        bool DeleteAT_TIME_MANUAL(List<decimal> lstID);

        [OperationContract()]
        DataTable GetDataImportCO();

        [OperationContract()]
        List<AT_LISTPARAM_SYSTEAMDTO> GetListParamItime(AT_LISTPARAM_SYSTEAMDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc");
        [OperationContract()]
        bool InsertListParamItime(AT_LISTPARAM_SYSTEAMDTO objHOLIDAY, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ValidateListParamItime(AT_LISTPARAM_SYSTEAMDTO objHOLIDAY);

        [OperationContract()]
        bool ModifyListParamItime(AT_LISTPARAM_SYSTEAMDTO objHOLIDAY, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ActiveListParamItime(List<decimal> lstID, UserLog log, string bActive);

        [OperationContract()]
        bool DeleteListParamItime(List<decimal> lstID);

        [OperationContract()]
        DataTable GET_REPORT();
        [OperationContract()]
        List<Se_ReportDTO> GetReportById(Se_ReportDTO _filter, int PageIndex, int PageSize, ref int Total, UserLog log, string Sorts = "CODE ASC");

        [OperationContract()]
        DataTable GETORGNAME(ParamDTO obj, UserLog log);

        [OperationContract()]
        DataSet GET_AT001(ParamDTO obj, UserLog log);
        [OperationContract()]
        DataSet GET_AT002(ParamDTO obj, UserLog log);

        [OperationContract()]
        DataSet GET_AT003(ParamDTO obj, UserLog log);

        [OperationContract()]
        DataSet GET_AT004(ParamDTO obj, UserLog log);

        [OperationContract()]
        DataSet GET_AT005(ParamDTO obj, UserLog log);

        [OperationContract()]
        DataSet GET_AT006(ParamDTO obj, UserLog log);

        [OperationContract()]
        DataSet GET_AT007(ParamDTO obj, UserLog log);

        [OperationContract()]
        bool CheckPeriodClose(List<decimal> lstEmp, DateTime startdate, DateTime enddate, ref string sAction);
        [OperationContract()]
        string AutoGenCode(string firstChar, string tableName, string colName);
        [OperationContract()]
        bool CheckExistInDatabase(List<decimal> lstID, AttendanceCommon.TABLE_NAME table);
        [OperationContract()]
        bool CheckExistInDatabaseAT_SIGNDEFAULT(List<decimal> lstID, List<DateTime> lstWorking, List<decimal> lstShift, AttendanceCommon.TABLE_NAME table);


        [OperationContract()]
        bool CheckPeriod(int PeriodId, decimal EmployeeId);

        [OperationContract()]
        List<AT_ACTION_LOGDTO> GetActionLog(AT_ACTION_LOGDTO _filter, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "ACTION_DATE desc");
        [OperationContract()]
        int DeleteActionLogsAT(List<decimal> lstDeleteIds);


        [OperationContract()]
        List<AT_TIME_TIMESHEET_DAILYDTO> GetListExplanation(AT_TIME_TIMESHEET_DAILYDTO _filter, ParamDTO _param, ref int Total = 0, int PageIndex = 0, int PageSize = int.MaxValue, string Sorts = "EMPLOYEE_CODE desc,WORKINGDAY asc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);

        [OperationContract()]
        DataTable GetExplanationManual();

        [OperationContract()]
        DataTable GetExplanationEmployee(ParamDTO _param, UserLog log);

        [OperationContract()]
        bool ImportExplanation(DataTable dtData, UserLog log, ref decimal gID);




        [OperationContract()]
        DataTable Get_KITCHEN(decimal is_blank);

        [OperationContract()]
        DataTable Get_KITCHEN_BY_ORG(decimal is_blank, decimal Meal_ID, ParamDTO _param, UserLog log);

        [OperationContract()]
        DataTable Get_KITCHEN_BY_EMP(decimal is_blank, decimal employee_id, decimal Meal_ID);

        [OperationContract()]
        DataTable Get_KITCHEN_BY_STUDENT(decimal is_blank, decimal student_id, decimal Meal_ID);

        [OperationContract()]
        DataTable Get_MEAL_BY_EMP_EFFECT(decimal is_blank, decimal employee_id, DateTime effectDate);

        [OperationContract()]
        DataTable Get_MEAL_BY_EMP(decimal is_blank, decimal employee_id);


        [OperationContract()]
        DataTable Get_MEAL_BY_ORG(decimal is_blank, decimal org_id);



        [OperationContract()]
        bool Insert_AT_KITCHEN(AT_KITCHEN_DTO objData, UserLog log, ref decimal gID);
        [OperationContract()]
        bool Modify_AT_KITCHEN(AT_KITCHEN_DTO objData, UserLog log, ref decimal gID);
        [OperationContract()]
        bool Delete_AT_KITCHEN(List<decimal> lstID);
        [OperationContract()]
        AT_KITCHEN_DTO Get_AT_KITCHENbyID(decimal? _id);
        [OperationContract()]
        List<AT_KITCHEN_DTO> Get_AT_KITCHEN(AT_KITCHEN_DTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc");


        [OperationContract()]
        bool Validate_AT_KITCHEN(AT_KITCHEN_DTO _obj, string _action, ref string _error = "");



        [OperationContract()]
        List<AT_TERMINALS_MEALDTO> GetAT_TERMINAL_MEAL(AT_TERMINALS_MEALDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);
        [OperationContract()]
        List<AT_TERMINALS_MEALDTO> GetAT_TERMINAL_MEAL_STATUS(AT_TERMINALS_MEALDTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);

        [OperationContract()]
        bool InsertAT_TERMINAL_MEAL(AT_TERMINALS_MEALDTO objData, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ValidateAT_TERMINAL_MEAL(AT_TERMINALS_MEALDTO objData, string sAction);

        [OperationContract()]
        bool ModifyAT_TERMINAL_MEAL(AT_TERMINALS_MEALDTO objData, UserLog log, ref decimal gID);

        [OperationContract()]
        bool ActiveAT_TERMINAL_MEAL(List<decimal> lstID, UserLog log, string bActive);

        [OperationContract()]
        bool DeleteAT_TERMINAL_MEAL(List<decimal> lstID);




        [OperationContract()]
        bool Modify_AT_MEAL_SETUP(AT_MEAL_SETUP_DTO objData, UserLog log);

        [OperationContract()]
        bool Delete_AT_MEAL_SETUP(List<decimal> lstID);

        [OperationContract()]
        AT_MEAL_SETUP_DTO Get_AT_MEAL_SETUPbyID(decimal? _id);
        [OperationContract()]
        List<AT_MEAL_SETUP_DTO> Get_AT_MEAL_SETUP(AT_MEAL_SETUP_DTO _filter, ParamDTO _param = null/* TODO Change to default(_) if this is not a reference type */, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "ORG_PATH", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);





        [OperationContract()]
        List<AT_KITCHEN_ORG_DTO> GetAT_KITCHEN_ORG(AT_KITCHEN_ORG_DTO filter, int PageIndex, int PageSize, ref int Total, string Sorts = "CREATED_DATE desc");

        [OperationContract()]
        bool InsertAT_KITCHEN_ORG(List<AT_KITCHEN_ORG_DTO> objAT_KITCHEN_ORG, UserLog log, ref decimal gID);

        [OperationContract()]
        bool CheckKitchenInUsing(List<decimal> lstID, decimal orgID);

        [OperationContract()]
        bool DeleteAT_KITCHEN_ORG(List<decimal> objAT_KITCHEN_ORG, UserLog log);

        [OperationContract()]
        bool ActiveAT_KITCHEN_ORG(List<decimal> objAT_KITCHEN_ORG, string sActive, UserLog log);


        [OperationContract()]
        bool Insert_AT_MEAL_MANAGER_BY_EMP(List<AT_MEAL_MANAGER_DTO> lstData, List<EmployeeDTO> lstEmp, ParamDTO _param, UserLog log);
        [OperationContract()]
        bool Insert_AT_MEAL_MANAGER(List<AT_MEAL_MANAGER_DTO> lstData, UserLog log, ref decimal gID);

        [OperationContract()]
        bool Delete_AT_MEAL_MANAGER(List<decimal> lstID);

        [OperationContract()]
        List<AT_MEAL_MANAGER_DTO> Get_AT_MEAL_MANAGERbyID(AT_MEAL_MANAGER_DTO obj);

        [OperationContract()]
        List<AT_MEAL_MANAGER_DTO> Get_AT_MEAL_MANAGER(AT_MEAL_MANAGER_DTO _filter, ParamDTO _param, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "EMPLOYEE_CODE asc, EFFECT_DATE asc, MEAL_ID asc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);
        [OperationContract()]
        bool Insert_AT_MEAL_MANAGER_BY_ORG(List<AT_MEAL_MANAGER_DTO> lstData, ParamDTO _param, UserLog log);


        [OperationContract()]
        bool Swap_AT_MEAL_MANAGER(AT_MEAL_SWAP_DTO objData, UserLog log);

        [OperationContract()]
        bool Validate_AT_MEAL_SWAP(AT_MEAL_SWAP_DTO objData, string _action, ref string _error = "");

        [OperationContract()]
        DataSet GETDATA_MANAGER_IMPORT(ParamDTO obj, UserLog log);
        [OperationContract()]
        bool ImportMealManager(DataTable dtData, DateTime StartDate, DateTime EndDate, ref DataTable dtError, UserLog log);
        [OperationContract()]
        bool CHECK_TIME_BY_EMP(decimal Employee_ID, DateTime Effect_date);

        [OperationContract()]
        bool CHECK_TIME_BY_STUDENT(decimal STUDENT_ID, DateTime Effect_date);

        [OperationContract()]
        bool CHECK_TIME_BY_ORG(decimal Employee_ID, DateTime Effect_date);

        [OperationContract()]
        List<EmployeeDTO> GetListEmployee_ByOrg(ParamDTO _param, UserLog log);



        [OperationContract()]
        bool Insert_AT_MEAL_STUDENT_BY_EMP(List<AT_MEAL_STUDENT_DTO> lstData, List<EmployeeDTO> lstEmp, ParamDTO _param, UserLog log);
        [OperationContract()]
        bool Insert_AT_MEAL_STUDENT(List<AT_MEAL_STUDENT_DTO> lstData, UserLog log, ref decimal gID);

        [OperationContract()]
        bool Delete_AT_MEAL_STUDENT(List<decimal> lstID);

        [OperationContract()]
        List<AT_MEAL_STUDENT_DTO> Get_AT_MEAL_STUDENTbyID(AT_MEAL_STUDENT_DTO obj);

        [OperationContract()]
        List<AT_MEAL_STUDENT_DTO> Get_AT_MEAL_STUDENT(AT_MEAL_STUDENT_DTO _filter, ParamDTO _param, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "STUDENT_CODE asc, EFFECT_DATE asc, MEAL_ID asc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);
        [OperationContract()]
        bool Insert_AT_MEAL_STUDENT_BY_ORG(List<AT_MEAL_STUDENT_DTO> lstData, ParamDTO _param, UserLog log);

        [OperationContract()]
        DataSet GETDATA_STUDENT_IMPORT(ParamDTO obj, UserLog log);
        [OperationContract()]
        bool ImportMealSTUDENT(DataTable dtData, DateTime StartDate, DateTime EndDate, ref DataTable dtError, UserLog log);

        [OperationContract()]
        List<EmployeeDTO> GetListStudent_ByOrg(ParamDTO _param, UserLog log);



        [OperationContract()]
        List<AT_MEAL_FORECAST_SUM_DTO> Get_AT_MEAL_FORECAST_SUM(AT_MEAL_FORECAST_SUM_DTO _filter, ParamDTO _param, ref int Total, int PageIndex, int PageSize, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);

        [OperationContract()]
        bool CAL_AT_MEAL_FORECAST_SUM(ParamDTO _param, UserLog log);


        [OperationContract()]
        DataTable Get_AT_MEAL_FORECAST_SUM_IMPORT(AT_MEAL_FORECAST_SUM_DTO _param, UserLog log);

        [OperationContract()]
        bool Import_AT_MEAL_FORECAST_SUM(List<AT_MEAL_FORECAST_SUM_DTO> lstData, UserLog log);


        [OperationContract()]
        bool Insert_AT_MEAL_PARTNER(AT_MEAL_PARTNER_DTO objData, UserLog log, ref decimal gID);
        [OperationContract()]
        bool Modify_AT_MEAL_PARTNER(AT_MEAL_PARTNER_DTO objData, UserLog log, ref decimal gID);
        [OperationContract()]
        bool Delete_AT_MEAL_PARTNER(List<decimal> lstID);

        [OperationContract()]
        AT_MEAL_PARTNER_DTO Get_AT_MEAL_PARTNERbyID(decimal? _id);
        [OperationContract()]
        List<AT_MEAL_PARTNER_DTO> Get_AT_MEAL_PARTNER(AT_MEAL_PARTNER_DTO _filter, ParamDTO _param, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc", UserLog Log = null/* TODO Change to default(_) if this is not a reference type */);




        [OperationContract()]
        bool Insert_AT_MEAL_CHANGE(AT_MEAL_CHANGE_DTO objData, UserLog log, ref decimal gID);

        [OperationContract()]
        bool Modify_AT_MEAL_CHANGE(AT_MEAL_CHANGE_DTO objData, UserLog log, ref decimal gID);

        [OperationContract()]
        bool Delete_AT_MEAL_CHANGE(List<decimal> lstID, UserLog log);

        [OperationContract()]
        AT_MEAL_CHANGE_DTO Get_AT_MEAL_CHANGEbyID(decimal? _id);

        [OperationContract()]
        List<AT_MEAL_CHANGE_DTO> Get_AT_MEAL_CHANGE(AT_MEAL_CHANGE_DTO _filter, ParamDTO _param, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);


        [OperationContract()]
        List<AT_MEAL_CHANGE_DTO> Get_AT_MEAL_CHANGEApprove(AT_MEAL_CHANGE_DTO _filter, ParamDTO _param, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);


        [OperationContract()]
        bool Approve_AT_MEAL_CHANGE(List<decimal> lstID, decimal _status, string _reason, UserLog log);

        [OperationContract()]
        DataSet GETDATA_CHANGE_IMPORT(ParamDTO obj, UserLog log);
        [OperationContract()]
        bool ImportMealChange(List<string> lstID, DataTable dtData, ref DataTable dtError, UserLog log);

        [OperationContract()]
        bool Insert_AT_MEAL_COST_SETUP(List<AT_MEAL_COST_SETUP_DTO> lst, UserLog log, ref decimal gID);
        [OperationContract()]
        bool Modify_AT_MEAL_COST_SETUP(AT_MEAL_COST_SETUP_DTO objData, UserLog log, ref decimal gID);
        [OperationContract()]
        bool Delete_AT_MEAL_COST_SETUP(List<decimal> lstID);

        [OperationContract()]
        AT_MEAL_COST_SETUP_DTO Get_AT_MEAL_COST_SETUPbyID(decimal? _id);
        [OperationContract()]
        List<AT_MEAL_COST_SETUP_DTO> Get_AT_MEAL_COST_SETUP(AT_MEAL_COST_SETUP_DTO _filter, int PageIndex = 0, int PageSize = int.MaxValue, ref int Total = 0, string Sorts = "CREATED_DATE desc");



        [OperationContract()]
        List<AT_MEAL_REAL_DTO> Get_AT_MEAL_REAL(AT_MEAL_REAL_DTO _filter, ParamDTO _param, ref int Total, int PageIndex, int PageSize, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);

        [OperationContract()]
        bool CAL_AT_MEAL_REAL(ParamDTO _param, UserLog log);



        [OperationContract()]
        List<AT_MEAL_EXPLAN_DTO> Get_AT_MEAL_EXPLAN(AT_MEAL_EXPLAN_DTO _filter, ParamDTO _param, ref int Total, int PageIndex, int PageSize, string Sorts = "CREATED_DATE desc", UserLog log = null/* TODO Change to default(_) if this is not a reference type */);


        [OperationContract()]
        DataSet Get_AT_MEAL_EXPLAN_IMPORT(AT_MEAL_EXPLAN_DTO _param, UserLog log);

        [OperationContract()]
        bool Import_AT_MEAL_EXPLAN(List<AT_MEAL_EXPLAN_DTO> lstData, UserLog log, ref DataTable dtError);



        [OperationContract()]
        DataSet ExportReport(string _reportCode, string _pkgName, ParamDTO obj, UserLog log);
    }
}
