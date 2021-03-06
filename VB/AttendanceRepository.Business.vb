Imports Attendance.AttendanceBusiness
Imports Framework.UI
Partial Class AttendanceRepository

#Region " Business"

#Region "quan ly vao ra"
    Function GetDataInout(ByVal _filter As AT_DATAINOUTDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "EMPLOYEE_CODE, WORKINGDAY") As List(Of AT_DATAINOUTDTO)
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.GetDataInout(_filter, _param, Total, PageIndex, PageSize, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertDataInout(ByVal lstDataInout As List(Of AT_DATAINOUTDTO), ByVal fromDate As Date, ByVal toDate As Date) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertDataInout(lstDataInout, fromDate, toDate, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyDataInout(ByVal objDataInout As AT_DATAINOUTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyDataInout(objDataInout, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteDataInout(ByVal lstDataInout As List(Of AT_DATAINOUTDTO)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteDataInout(lstDataInout)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Đăng ký đi muộn về sớm"
    Function GetLate_combackout(ByVal _filter As AT_LATE_COMBACKOUTDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_LATE_COMBACKOUTDTO)
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.GetDSVM(_filter, _param, Total, PageIndex, PageSize, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function ImportLate_combackout(ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ImportLate_combackout(objLate_combackout, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertLate_combackout(ByVal objRegisterDMVSList As List(Of AT_LATE_COMBACKOUTDTO), ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertLate_combackout(objRegisterDMVSList, objLate_combackout, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetLate_CombackoutById(ByVal _id As Decimal?) As AT_LATE_COMBACKOUTDTO
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetLate_CombackoutById(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateLate_combackout(ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateLate_combackout(objLate_combackout)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyLate_combackout(ByVal objLate_combackout As AT_LATE_COMBACKOUTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyLate_combackout(objLate_combackout, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteLate_combackout(ByVal lstID As List(Of Decimal),
                                          ByVal _param As ParamDTO,
                                     ByVal period_id As Decimal,
                                     ByVal listEmployeeId As List(Of Decimal?)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteLate_combackout(lstID, _param, period_id, listEmployeeId, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "lam them"
    Function GetRegisterOT(ByVal _filter As AT_REGISTER_OTDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_REGISTER_OTDTO)
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.GetRegisterOT(_filter, _param, Total, PageIndex, PageSize, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Function GetListHsOT() As List(Of OT_OTHERLIST_DTO)
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.GetListHsOT()
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertRegisterOT(ByVal objRegisterOT As AT_REGISTER_OTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertRegisterOT(objRegisterOT, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertDataRegisterOT(ByVal objRegisterOTList As List(Of AT_REGISTER_OTDTO), ByVal objRegisterOT As AT_REGISTER_OTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertDataRegisterOT(objRegisterOTList, objRegisterOT, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetRegisterById(ByVal _id As Decimal?) As AT_REGISTER_OTDTO
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetRegisterById(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateRegisterOT(ByVal objRegisterOT As AT_REGISTER_OTDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateRegisterOT(objRegisterOT)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyRegisterOT(ByVal objRegisterOT As AT_REGISTER_OTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyRegisterOT(objRegisterOT, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteRegisterOT(ByVal lstID As List(Of Decimal),
                                     ByVal _param As ParamDTO,
                                     ByVal period_id As Decimal,
                                     ByVal listEmployeeId As List(Of Decimal?)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteRegisterOT(lstID, _param, period_id, listEmployeeId, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CheckImporAddNewtOT(ByVal objRegisterOT As AT_REGISTER_OTDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.CheckImporAddNewtOT(objRegisterOT)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function CheckDataListImportAddNew(ByVal objRegisterOTList As List(Of AT_REGISTER_OTDTO), ByVal objRegisterOT As AT_REGISTER_OTDTO, ByRef strEmployeeCode As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.CheckDataListImportAddNew(objRegisterOTList, objRegisterOT, strEmployeeCode)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "bảng công làm thêm"

    Function Cal_TimeTImesheet_OT(ByVal _param As ParamDTO, ByVal p_period_id As Decimal?, ByVal P_ORG_ID As Decimal, ByVal lstEmployee As List(Of Decimal?)) As Boolean
        Dim dt As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.Cal_TimeTImesheet_OT(_param, Me.Log, p_period_id, P_ORG_ID, lstEmployee)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetSummaryOT(ByVal param As AT_TIME_TIMESHEET_OTDTO) As System.Data.DataSet
        Dim dt As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.GetSummaryOT(param, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Function Cal_TimeTImesheet_NB(ByVal _param As ParamDTO, ByVal p_period_id As Decimal?, ByVal P_ORG_ID As Decimal, ByVal lstEmployee As List(Of Decimal?)) As Boolean
        Dim dt As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.Cal_TimeTImesheet_NB(_param, Me.Log, p_period_id, P_ORG_ID, lstEmployee)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetSummaryNB(ByVal param As AT_TIME_TIMESHEET_NBDTO) As System.Data.DataSet
        Dim dt As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.GetSummaryNB(param, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyLeaveSheetOt(ByVal objRegister As AT_TIME_TIMESHEET_OTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyLeaveSheetOt(objRegister, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertLeaveSheetOt(ByVal objRegister As AT_TIME_TIMESHEET_OTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertLeaveSheetOt(objRegister, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetTimeSheetOtById(ByVal obj As AT_TIME_TIMESHEET_OTDTO) As AT_TIME_TIMESHEET_OTDTO
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetTimeSheetOtById(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Đăng ký công"
    Function GetLeaveSheet(ByVal _filter As AT_LEAVESHEETDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_LEAVESHEETDTO)
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.GetLeaveSheet(_filter, _param, Total, PageIndex, PageSize, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertLeaveSheet(ByVal objRegister As AT_LEAVESHEETDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertLeaveSheet(objRegister, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertLeaveSheetList(ByVal objRegisterList As List(Of AT_LEAVESHEETDTO), ByVal objRegister As AT_LEAVESHEETDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertLeaveSheetList(objRegisterList, objRegister, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetLeaveById(ByVal _id As Decimal?) As AT_LEAVESHEETDTO
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetLeaveById(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetTotalPHEPNAM(ByVal P_EMPLOYEE_ID As Integer,
                                      ByVal Date_cal As Date,
                                      ByVal P_TYPE_LEAVE_ID As Integer) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetTotalPHEPNAM(P_EMPLOYEE_ID, Date_cal, P_TYPE_LEAVE_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetTotalDAY(ByVal P_EMPLOYEE_ID As Integer,
                                ByVal P_TYPE_MANUAL As Integer,
                                ByVal P_FROM_DATE As Date,
                                ByVal P_TO_DATE As Date) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetTotalDAY(P_EMPLOYEE_ID, P_TYPE_MANUAL, P_FROM_DATE, P_TO_DATE)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetCAL_DAY_LEAVE_OLD(ByVal P_EMPLOYEE_ID As Integer,
                               ByVal P_FROM_DATE As Date,
                               ByVal P_TO_DATE As Date) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetCAL_DAY_LEAVE_OLD(P_EMPLOYEE_ID, P_FROM_DATE, P_TO_DATE)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetTotalPHEPBU(ByVal P_EMPLOYEE_ID As Integer,
                                    ByVal Date_cal As Date,
                                     ByVal P_TYPE_LEAVE_ID As Integer) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetTotalPHEPBU(P_EMPLOYEE_ID, Date_cal, P_TYPE_LEAVE_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetPhepNam(ByVal _id As Decimal?, ByVal _year As Decimal?) As AT_ENTITLEMENTDTO
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetPhepNam(_id, _year)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetPHEPBUCONLAI(ByVal lstEmpID As List(Of AT_LEAVESHEETDTO), ByVal _year As Decimal?) As List(Of AT_LEAVESHEETDTO)
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetPHEPBUCONLAI(lstEmpID, _year)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetNghiBu(ByVal _id As Decimal?, ByVal _year As Decimal?) As AT_COMPENSATORYDTO
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetNghiBu(_id, _year)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateLeaveSheet(ByVal objRegister As AT_LEAVESHEETDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateLeaveSheet(objRegister)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyLeaveSheet(ByVal objRegister As AT_LEAVESHEETDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyLeaveSheet(objRegister, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteLeaveSheet(ByVal lstID As List(Of AT_LEAVESHEETDTO),
                                     ByVal _param As ParamDTO,
                                     ByVal period_id As Decimal,
                                     ByVal listEmployeeId As List(Of Decimal?)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteLeaveSheet(lstID, _param, period_id, listEmployeeId, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function checkLeaveImport(ByVal dtData As DataTable) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.checkLeaveImport(dtData)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CheckDataCheckworksign(ByVal objRegisterOTList As List(Of AT_REGISTER_OTDTO), ByVal objRegisterOT As AT_REGISTER_OTDTO, ByRef strEmployeeCode As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.CheckDataCheckworksign(objRegisterOTList, objRegisterOT, strEmployeeCode)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function CheckDataCheckworksignImport(ByVal objRegisterOT As AT_REGISTER_OTDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.CheckDataCheckworksignImport(objRegisterOT)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function Check_DataRegister_OT(ByRef _param As String, ByVal Startdate As Date?, ByVal Enddate As Date?, ByVal period_id As Decimal?) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Check_DataRegister_OT(_param, Me.Log, Startdate, Enddate, period_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function Check_WorkSing_default(ByVal obj As ParamDTO, ByRef Employee_ID As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Check_WorkSing_default(obj, Me.Log, Employee_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "bảng cham cong máy"
    Function GetMachines(ByVal _filter As AT_TIME_TIMESHEET_MACHINETDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "EMPLOYEE_ID, WORKINGDAY") As List(Of AT_TIME_TIMESHEET_MACHINETDTO)
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.GetMachines(_filter, _param, Total, PageIndex, PageSize, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Function Init_TimeTImesheetMachines(ByVal _param As ParamDTO, ByVal p_fromdate As Date, ByVal p_enddate As Date, ByVal P_ORG_ID As Decimal, ByVal lstEmployee As List(Of Decimal?)) As Boolean
        Dim dt As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.Init_TimeTImesheetMachines(_param, Me.Log, p_fromdate, p_enddate, P_ORG_ID, lstEmployee)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "bảng cham cong tay"

    Public Function GetCCT(ByVal param As AT_TIME_TIMESHEET_DAILYDTO) As System.Data.DataSet
        Dim dt As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.GetCCT(param, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetCCT_Origin(ByVal param As AT_TIME_TIMESHEET_DAILYDTO) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetCCT_Origin(param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyLeaveSheetDaily(ByVal objRegister As AT_TIME_TIMESHEET_DAILYDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyLeaveSheetDaily(objRegister, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertLeaveSheetDaily(ByVal dtData As DataTable, ByVal PeriodID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertLeaveSheetDaily(dtData, Me.Log, PeriodID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetTimeSheetDailyById(ByVal obj As AT_TIME_TIMESHEET_DAILYDTO) As AT_TIME_TIMESHEET_DAILYDTO
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetTimeSheetDailyById(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Tong hop cong"
    Function GetTimeSheet(ByVal _filter As AT_TIME_TIMESHEET_MONTHLYDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "EMPLOYEE_CODE,DECISION_START") As List(Of AT_TIME_TIMESHEET_MONTHLYDTO)
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.GetTimeSheet(_filter, _param, Total, PageIndex, PageSize, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function CAL_TIME_TIMESHEET_MONTHLY(ByVal param As ParamDTO, ByVal lstEmployee As List(Of Decimal?)) As Boolean
        Dim dt As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.CAL_TIME_TIMESHEET_MONTHLY(param, lstEmployee, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ValidateTimesheet(ByVal _validate As AT_TIME_TIMESHEET_MONTHLYDTO, ByVal sType As String)
        Dim dt As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.ValidateTimesheet(_validate, sType, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "PHEP NAM"
    Function GetEntitlement(ByVal _filter As AT_ENTITLEMENTDTO,
                                ByVal _param As ParamDTO,
                                  Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_ENTITLEMENTDTO)
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.GetEntitlement(_filter, _param, Total, PageIndex, PageSize, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function CALCULATE_ENTITLEMENT(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.CALCULATE_ENTITLEMENT(param, listEmployeeId, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Nghỉ bu"
    Public Function CALCULATE_ENTITLEMENT_NB(ByVal param As ParamDTO, ByVal listEmployeeId As List(Of Decimal?)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.CALCULATE_ENTITLEMENT_NB(param, listEmployeeId, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Function GetNB(ByVal _filter As AT_COMPENSATORYDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_COMPENSATORYDTO)
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.GetNB(_filter, _param, Total, PageIndex, PageSize, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "WORKSIGN"
    Public Function GET_WORKSIGN(ByVal param As AT_WORKSIGNDTO) As System.Data.DataSet
        Dim dt As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.GET_WORKSIGN(param, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GET_WORKSIGN_BYEMP(ByVal Emp_ID As Decimal, ByVal working_day As DateTime) As AT_WORKSIGNDTO

        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GET_WORKSIGN_BYEMP(Emp_ID, working_day)

            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertWorkSign(ByVal objWorkSigns As List(Of AT_WORKSIGNDTO), ByVal objWork As AT_WORKSIGNDTO, ByVal p_fromdate As Date, ByVal p_endDate As Date?, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertWorkSign(objWorkSigns, objWork, p_fromdate, p_endDate, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertWORKSIGNByImport(ByVal dtData As DataTable,
                                           ByVal period_id As Decimal, ByRef lstEmp As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertWORKSIGNByImport(dtData, period_id, Me.Log, lstEmp)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateWORKSIGN(ByVal objWORKSIGN As AT_WORKSIGNDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateWORKSIGN(objWORKSIGN)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyWORKSIGN(ByVal objWORKSIGN As AT_WORKSIGNDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyWORKSIGN(objWORKSIGN, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteWORKSIGN(ByVal lstWORKSIGN As List(Of AT_WORKSIGNDTO)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteWORKSIGN(lstWORKSIGN)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GETSIGNDEFAULT(ByVal param As ParamDTO) As System.Data.DataTable
        Dim dt As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.GETSIGNDEFAULT(param, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function Del_WorkSign_ByEmp(ByVal employee_id As Decimal, ByVal p_From As Date, ByVal p_to As Date) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Del_WorkSign_ByEmp(employee_id, p_From, p_to)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetSwipeData(ByVal _filter As AT_SWIPE_DATADTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "iTime_id, VALTIME desc") As List(Of AT_SWIPE_DATADTO)
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.GetSwipeData(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetSwipeDataMeal(ByVal _filter As AT_SWIPE_DATA_MEALDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal Sorts As String = "iTime_id, VALTIME desc") As List(Of AT_SWIPE_DATA_MEALDTO)
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.GetSwipeDataMeal(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function CheckOffInMonth(ByVal _param As ParamDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.CheckOffInMonth(_param, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function CheckOffInMonthTable(ByVal dtData As DataTable,
                                   ByVal p_period_id As Decimal,
                                   ByRef dtDataError As DataTable) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.CheckOffInMonthTable(dtData, p_period_id, dtDataError)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function


    Public Function InsertSwipeDataImport(ByVal objDelareRice As List(Of AT_SWIPE_DATADTO),
                                          ByVal isMeal As Boolean) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.InsertSwipeDataImport(objDelareRice, Me.Log, isMeal)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Khai bao cong cơm"
    Function GetDelareRice(ByVal _filter As AT_TIME_RICEDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_TIME_RICEDTO)
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.GetDelareRice(_filter, _param, Total, PageIndex, PageSize, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertDelareRice(ByVal objDelareRice As AT_TIME_RICEDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertDelareRice(objDelareRice, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertDelareRiceList(ByVal objDelareRiceList As List(Of AT_TIME_RICEDTO), ByVal objDelareRice As AT_TIME_RICEDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertDelareRiceList(objDelareRiceList, objDelareRice, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateDelareRice(ByVal objDelareRice As AT_TIME_RICEDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateDelareRice(objDelareRice)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetDelareRiceById(ByVal _id As Decimal?) As AT_TIME_RICEDTO
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetDelareRiceById(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyDelareRice(ByVal objDelareRice As AT_TIME_RICEDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyDelareRice(objDelareRice, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveDelareRice(ByVal lstHoliday As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveDelareRice(lstHoliday, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteDelareRice(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteDelareRice(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "Khai bao điều chỉnh thâm niên phép"
    Function GetDelareEntitlementNB(ByVal _filter As AT_DECLARE_ENTITLEMENTDTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_DECLARE_ENTITLEMENTDTO)
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.GetDelareEntitlementNB(_filter, _param, Total, PageIndex, PageSize, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertDelareEntitlementNB(ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertDelareEntitlementNB(objDelareEntitlementNB, Me.Log, gID, checkMonthNB, checkMonthNP)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function InsertMultipleDelareEntitlementNB(ByVal objDelareEntitlementlist As List(Of AT_DECLARE_ENTITLEMENTDTO), ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertMultipleDelareEntitlementNB(objDelareEntitlementlist, objDelareEntitlementNB, Me.Log, gID, checkMonthNB, checkMonthNP)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ImportDelareEntitlementNB(ByVal dtData As DataTable, ByRef gID As Decimal, ByRef checkMonthNB As Boolean, ByRef checkMonthNP As Boolean) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ImportDelareEntitlementNB(dtData, Me.Log, gID, checkMonthNB, checkMonthNP)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function GetDelareEntitlementNBById(ByVal _id As Decimal?) As AT_DECLARE_ENTITLEMENTDTO
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetDelareEntitlementNBById(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyDelareEntitlementNB(ByVal objDelareEntitlementNB As AT_DECLARE_ENTITLEMENTDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyDelareEntitlementNB(objDelareEntitlementNB, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveDelareEntitlementNB(ByVal lstHoliday As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveDelareEntitlementNB(lstHoliday, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteDelareEntitlementNB(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteDelareEntitlementNB(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateMonthThamNien(ByVal objHoliday As AT_DECLARE_ENTITLEMENTDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateMonthThamNien(objHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateMonthPhepNam(ByVal objHoliday As AT_DECLARE_ENTITLEMENTDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateMonthPhepNam(objHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateMonthNghiBu(ByVal objHoliday As AT_DECLARE_ENTITLEMENTDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateMonthNghiBu(objHoliday)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "bảng công cơm"
    Function Cal_TimeTImesheet_Rice(ByVal _param As ParamDTO, ByVal p_period_id As Decimal?, ByVal P_ORG_ID As Decimal?, ByVal lstEmployee As List(Of Decimal?)) As Boolean
        Dim dt As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.Cal_TimeTImesheet_Rice(_param, Me.Log, p_period_id, P_ORG_ID, lstEmployee)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetSummaryRice(ByVal param As AT_TIME_TIMESHEET_RICEDTO) As System.Data.DataSet
        Dim dt As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                dt = rep.GetSummaryRice(param, Me.Log)
                Return dt
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ModifyLeaveSheetRice(ByVal objRegister As AT_TIME_TIMESHEET_RICEDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyLeaveSheetRice(objRegister, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function ApprovedTimeSheetRice(ByVal objRegister As AT_TIME_TIMESHEET_RICEDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ApprovedTimeSheetRice(objRegister, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function InsertLeaveSheetRice(ByVal objRegister As AT_TIME_TIMESHEET_RICEDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertLeaveSheetRice(objRegister, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function GetTimeSheetRiceById(ByVal obj As AT_TIME_TIMESHEET_RICEDTO) As AT_TIME_TIMESHEET_RICEDTO
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetTimeSheetRiceById(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "LOG"
    Function GetActionLog(ByVal _filter As AT_ACTION_LOGDTO,
                                 Optional ByRef Total As Integer = 0,
                                 Optional ByVal PageIndex As Integer = 0,
                                 Optional ByVal PageSize As Integer = Integer.MaxValue,
                                 Optional ByVal Sorts As String = "ACTION_DATE desc") As List(Of AT_ACTION_LOGDTO)
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.GetActionLog(_filter, Total, PageIndex, PageSize, Sorts)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Function DeleteActionLogsAT(ByVal lstDeleteIds As List(Of Decimal)) As Integer
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.DeleteActionLogsAT(lstDeleteIds)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function
#End Region

#Region "Giải trình chấm công"
    Public Function GetListExplanation(ByVal _filter As AT_TIME_TIMESHEET_DAILYDTO,
                                     ByVal _param As ParamDTO,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByVal Sorts As String = "EMPLOYEE_CODE desc,WORKINGDAY asc") As List(Of AT_TIME_TIMESHEET_DAILYDTO)

        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetListExplanation(_filter, _param, Total, PageIndex, PageSize, Sorts, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Function GetExplanationManual() As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.GetExplanationManual()
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Function GetExplanationEmployee(ByVal _param As ParamDTO) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.GetExplanationEmployee(_param, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function ImportExplanation(ByVal dtData As DataTable, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ImportExplanation(dtData, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#End Region

#Region "IPORTAL - View bảng công"
    Public Function CheckPeriod(ByVal PeriodId As Integer, ByVal EmployeeId As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.CheckPeriod(PeriodId, EmployeeId)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Function GetTimeSheetPortal(ByVal _filter As AT_TIME_TIMESHEET_DAILYDTO) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.GetTimeSheetPortal(_filter)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

#End Region

End Class