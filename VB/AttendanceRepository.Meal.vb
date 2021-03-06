Imports Attendance.AttendanceBusiness

Partial Public Class AttendanceRepository
    Inherits AttendanceRepositoryBase

#Region "List"


    Public Function Get_KITCHEN(ByVal is_blank As Decimal) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Get_KITCHEN(is_blank)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function Get_KITCHEN_BY_EMP(ByVal is_blank As Decimal,
                                       ByVal employee_id As Decimal,
                                       ByVal Meal_ID As Decimal) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Get_KITCHEN_BY_EMP(is_blank, employee_id, Meal_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Get_KITCHEN_BY_STUDENT(ByVal is_blank As Decimal,
                                       ByVal student_id As Decimal,
                                       ByVal Meal_ID As Decimal) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Get_KITCHEN_BY_STUDENT(is_blank, student_id, Meal_ID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Get_KITCHEN_BY_ORG(ByVal is_blank As Decimal,
                                       ByVal Meal_ID As Decimal,
                                       ByVal _param As ParamDTO) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Get_KITCHEN_BY_ORG(is_blank, Meal_ID, _param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Get_MEAL_BY_EMP_EFFECT(ByVal is_blank As Decimal,
                                 ByVal employee_id As Decimal,
                                 ByVal effectDate As Date) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Get_MEAL_BY_EMP_EFFECT(is_blank, employee_id, effectDate)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Get_MEAL_BY_EMP(ByVal is_blank As Decimal,
                                 ByVal employee_id As Decimal) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Get_MEAL_BY_EMP(is_blank, employee_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Get_MEAL_BY_ORG(ByVal is_blank As Decimal,
                                 ByVal org_id As Decimal) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Get_MEAL_BY_ORG(is_blank, org_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


#End Region

#Region "AT_KITCHEN"
    Public Function Insert_AT_KITCHEN(ByVal objData As AT_KITCHEN_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Insert_AT_KITCHEN(objData, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function Modify_AT_KITCHEN(ByVal objData As AT_KITCHEN_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Modify_AT_KITCHEN(objData, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function Delete_AT_KITCHEN(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Delete_AT_KITCHEN(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Get_AT_KITCHENById(ByVal _id As Decimal?) As AT_KITCHEN_DTO
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Get_AT_KITCHENbyID(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function Get_AT_KITCHEN(ByVal _filter As AT_KITCHEN_DTO,
                                       Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_KITCHEN_DTO)
        Dim lst As List(Of AT_KITCHEN_DTO)

        Using rep As New AttendanceBusinessClient
            Try
                lst = rep.Get_AT_KITCHEN(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function Validate_AT_KITCHEN(ByVal _obj As AT_KITCHEN_DTO,
                                        ByVal _action As String,
                                        Optional ByRef _error As String = "") As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Validate_AT_KITCHEN(_obj, _action, _error)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "AT_TERMINALS_MEAL"
    Public Function GetAT_TERMINAL_MEAL(ByVal _filter As AT_TERMINALS_MEALDTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                   Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_TERMINALS_MEALDTO)
        Dim lstTerminal As List(Of AT_TERMINALS_MEALDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstTerminal = rep.GetAT_TERMINAL_MEAL(_filter, PageIndex, PageSize, Total, Sorts, Log)
                Return lstTerminal
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function GetAT_TERMINAL_MEAL_STATUS(ByVal _filter As AT_TERMINALS_MEALDTO,
                                        Optional ByVal PageIndex As Integer = 0,
                                            Optional ByVal PageSize As Integer = Integer.MaxValue,
                                            Optional ByRef Total As Integer = 0,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_TERMINALS_MEALDTO)
        Dim lstTerminal As List(Of AT_TERMINALS_MEALDTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstTerminal = rep.GetAT_TERMINAL_MEAL_STATUS(_filter, PageIndex, PageSize, Total, Sorts, Log)
                Return lstTerminal
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
        Return Nothing
    End Function

    Public Function InsertAT_TERMINAL_MEAL(ByVal objTerminal As AT_TERMINALS_MEALDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_TERMINAL_MEAL(objTerminal, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ValidateAT_TERMINAL_MEAL(ByVal objTerminal As AT_TERMINALS_MEALDTO,
                                                 ByVal sAction As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ValidateAT_TERMINAL_MEAL(objTerminal, sAction)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ModifyAT_TERMINAL_MEAL(ByVal objTerminal As AT_TERMINALS_MEALDTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ModifyAT_TERMINAL_MEAL(objTerminal, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAT_TERMINAL_MEAL(ByVal lstTerminal As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveAT_TERMINAL_MEAL(lstTerminal, Me.Log, sActive)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAT_TERMINAL_MEAL(ByVal lstTerminal As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_TERMINAL_MEAL(lstTerminal)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "AT_MEAL_SETUP"


    Public Function Modify_AT_MEAL_SETUP(ByVal objData As AT_MEAL_SETUP_DTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Modify_AT_MEAL_SETUP(objData, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function Delete_AT_MEAL_SETUP(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Delete_AT_MEAL_SETUP(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Get_AT_MEAL_SETUPById(ByVal _id As Decimal?) As AT_MEAL_SETUP_DTO
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Get_AT_MEAL_SETUPbyID(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function Get_AT_MEAL_SETUP(ByVal _filter As AT_MEAL_SETUP_DTO,
                                           Optional ByVal _param As ParamDTO = Nothing,
                                           Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                           Optional ByVal Sorts As String = "ORG_PATH") As List(Of AT_MEAL_SETUP_DTO)
        Dim lst As List(Of AT_MEAL_SETUP_DTO)

        Using rep As New AttendanceBusinessClient
            Try
                lst = rep.Get_AT_MEAL_SETUP(_filter, _param, PageIndex, PageSize, Total, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

#Region "AT_KITCHEN_ORG"

    Public Function GetAT_KITCHEN_ORG(ByVal filter As AT_KITCHEN_ORG_DTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_KITCHEN_ORG_DTO)
        Dim lstAT_KITCHEN_ORG As List(Of AT_KITCHEN_ORG_DTO)

        Using rep As New AttendanceBusinessClient
            Try
                lstAT_KITCHEN_ORG = rep.GetAT_KITCHEN_ORG(filter, PageIndex, PageSize, Total, Sorts)
                Return lstAT_KITCHEN_ORG
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function InsertAT_KITCHEN_ORG(ByVal lstAT_KITCHEN_ORG As List(Of AT_KITCHEN_ORG_DTO), Optional ByRef gID As Decimal = Nothing) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.InsertAT_KITCHEN_ORG(lstAT_KITCHEN_ORG, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function



    Public Function CheckKitchenInUsing(ByVal lstID As List(Of Decimal), ByVal orgID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.CheckKitchenInUsing(lstID, orgID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function DeleteAT_KITCHEN_ORG(ByVal lstAT_KITCHEN_ORG As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.DeleteAT_KITCHEN_ORG(lstAT_KITCHEN_ORG, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function ActiveAT_KITCHEN_ORG(ByVal lstAT_KITCHEN_ORG As List(Of Decimal), ByVal sActive As String) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ActiveAT_KITCHEN_ORG(lstAT_KITCHEN_ORG, sActive, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "AT_MEAL_MANAGER"
    Public Function Insert_AT_MEAL_MANAGER(ByVal lstData As List(Of AT_MEAL_MANAGER_DTO), ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Insert_AT_MEAL_MANAGER(lstData, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Insert_AT_MEAL_MANAGER_BY_ORG(ByVal lstData As List(Of AT_MEAL_MANAGER_DTO), ByVal _param As ParamDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Insert_AT_MEAL_MANAGER_BY_ORG(lstData, _param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CHECK_TIME_BY_EMP(ByVal Employee_ID As Decimal, ByVal Effect_date As Date) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.CHECK_TIME_BY_EMP(Employee_ID, Effect_date)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CHECK_TIME_BY_STUDENT(ByVal STUDENT_ID As Decimal, ByVal Effect_date As Date) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.CHECK_TIME_BY_STUDENT(STUDENT_ID, Effect_date)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function CHECK_TIME_BY_ORG(ByVal ORG_ID As Decimal, ByVal Effect_date As Date) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.CHECK_TIME_BY_ORG(ORG_ID, Effect_date)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Insert_AT_MEAL_MANAGER_BY_EMP(ByVal lstData As List(Of AT_MEAL_MANAGER_DTO),
                                                  ByVal lstEmp As List(Of Attendance.AttendanceBusiness.EmployeeDTO),
                                                  ByVal _param As ParamDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Insert_AT_MEAL_MANAGER_BY_EMP(lstData, lstEmp, _param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function



    Public Function Delete_AT_MEAL_MANAGER(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Delete_AT_MEAL_MANAGER(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Get_AT_MEAL_MANAGERById(ByVal obj As AT_MEAL_MANAGER_DTO) As List(Of AT_MEAL_MANAGER_DTO)
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Get_AT_MEAL_MANAGERbyID(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function Get_AT_MEAL_MANAGER(ByVal _filter As AT_MEAL_MANAGER_DTO,
                                       ByVal _param As ParamDTO,
                                       Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "EMPLOYEE_CODE asc, EFFECT_DATE asc, MEAL_ID asc") As List(Of AT_MEAL_MANAGER_DTO)
        Dim lst As List(Of AT_MEAL_MANAGER_DTO)

        Using rep As New AttendanceBusinessClient
            Try
                lst = rep.Get_AT_MEAL_MANAGER(_filter, _param, PageIndex, PageSize, Total, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function Validate_AT_MEAL_SWAP(ByVal objData As AT_MEAL_SWAP_DTO,
                                          ByVal _action As String,
                                          Optional ByRef _error As String = "") As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Validate_AT_MEAL_SWAP(objData, _action, _error)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Swap_AT_MEAL_MANAGER(ByVal objData As AT_MEAL_SWAP_DTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Swap_AT_MEAL_MANAGER(objData, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function GETDATA_MANAGER_IMPORT(ByVal obj As ParamDTO) As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GETDATA_MANAGER_IMPORT(obj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using


    End Function

    Public Function ImportMealManager(ByVal dtData As DataTable,
                                      ByVal StartDate As Date,
                                      ByVal EndDate As Date,
                                      ByRef dtError As DataTable) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ImportMealManager(dtData, StartDate, EndDate, dtError, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function GetListStudent_ByOrg(ByVal _param As ParamDTO) As List(Of EmployeeDTO)
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetListStudent_ByOrg(_param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "AT_MEAL_STUDENT"
    Public Function Insert_AT_MEAL_STUDENT(ByVal lstData As List(Of AT_MEAL_STUDENT_DTO), ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Insert_AT_MEAL_STUDENT(lstData, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Insert_AT_MEAL_STUDENT_BY_ORG(ByVal lstData As List(Of AT_MEAL_STUDENT_DTO), ByVal _param As ParamDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Insert_AT_MEAL_STUDENT_BY_ORG(lstData, _param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Insert_AT_MEAL_STUDENT_BY_EMP(ByVal lstData As List(Of AT_MEAL_STUDENT_DTO),
                                                  ByVal lstEmp As List(Of Attendance.AttendanceBusiness.EmployeeDTO),
                                                  ByVal _param As ParamDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Insert_AT_MEAL_STUDENT_BY_EMP(lstData, lstEmp, _param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function



    Public Function Delete_AT_MEAL_STUDENT(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Delete_AT_MEAL_STUDENT(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Get_AT_MEAL_STUDENTById(ByVal obj As AT_MEAL_STUDENT_DTO) As List(Of AT_MEAL_STUDENT_DTO)
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Get_AT_MEAL_STUDENTbyID(obj)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function Get_AT_MEAL_STUDENT(ByVal _filter As AT_MEAL_STUDENT_DTO,
                                       ByVal _param As ParamDTO,
                                       Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "STUDENT_CODE asc, EFFECT_DATE asc, MEAL_ID asc") As List(Of AT_MEAL_STUDENT_DTO)
        Dim lst As List(Of AT_MEAL_STUDENT_DTO)

        Using rep As New AttendanceBusinessClient
            Try
                lst = rep.Get_AT_MEAL_STUDENT(_filter, _param, PageIndex, PageSize, Total, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function GETDATA_STUDENT_IMPORT(ByVal obj As ParamDTO) As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GETDATA_STUDENT_IMPORT(obj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using


    End Function

    Public Function ImportMealSTUDENT(ByVal dtData As DataTable,
                                      ByVal StartDate As Date,
                                      ByVal EndDate As Date,
                                      ByRef dtError As DataTable) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ImportMealSTUDENT(dtData, StartDate, EndDate, dtError, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function GetListEmployee_ByOrg(ByVal _param As ParamDTO) As List(Of EmployeeDTO)
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GetListEmployee_ByOrg(_param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
#End Region

#Region "AT_FORECAST_SUM"

    Function Get_AT_MEAL_FORECAST_SUM(ByVal _filter As AT_MEAL_FORECAST_SUM_DTO,
                                 ByVal _param As ParamDTO,
                                 ByRef Total As Integer,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 Optional ByVal Sorts As String = "EFFECT_DATE,ORG_PATH,ORG_NAME,KITCHEN_NAME,MEAL_NAME,RATION_NAME") As List(Of AT_MEAL_FORECAST_SUM_DTO)
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.Get_AT_MEAL_FORECAST_SUM(_filter, _param, Total, PageIndex, PageSize, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Function Get_AT_MEAL_FORECAST_SUM(ByVal _filter As AT_MEAL_FORECAST_SUM_DTO,
                                 ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "EFFECT_DATE,ORG_PATH,ORG_NAME,KITCHEN_NAME,MEAL_NAME,RATION_NAME") As List(Of AT_MEAL_FORECAST_SUM_DTO)
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.Get_AT_MEAL_FORECAST_SUM(_filter, _param, 0, 0, Integer.MaxValue, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Function CAL_AT_MEAL_FORECAST_SUM(ByVal _param As ParamDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.CAL_AT_MEAL_FORECAST_SUM(_param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function


    Function Get_AT_MEAL_FORECAST_SUM_IMPORT(ByVal _param As AT_MEAL_FORECAST_SUM_DTO) As DataTable
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.Get_AT_MEAL_FORECAST_SUM_IMPORT(_param, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function Import_AT_MEAL_FORECAST_SUM(ByVal lstData As List(Of AT_MEAL_FORECAST_SUM_DTO)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Import_AT_MEAL_FORECAST_SUM(lstData, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "AT_MEAL_PARTNER"
    Public Function Insert_AT_MEAL_PARTNER(ByVal objData As AT_MEAL_PARTNER_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Insert_AT_MEAL_PARTNER(objData, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function Modify_AT_MEAL_PARTNER(ByVal objData As AT_MEAL_PARTNER_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Modify_AT_MEAL_PARTNER(objData, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function Delete_AT_MEAL_PARTNER(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Delete_AT_MEAL_PARTNER(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Get_AT_MEAL_PARTNERById(ByVal _id As Decimal?) As AT_MEAL_PARTNER_DTO
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Get_AT_MEAL_PARTNERbyID(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function
    Public Function Get_AT_MEAL_PARTNER(ByVal _filter As AT_MEAL_PARTNER_DTO,
                                       ByVal _param As ParamDTO,
                                       Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_MEAL_PARTNER_DTO)
        Dim lst As List(Of AT_MEAL_PARTNER_DTO)

        Using rep As New AttendanceBusinessClient
            Try
                lst = rep.Get_AT_MEAL_PARTNER(_filter, _param, PageIndex, PageSize, Total, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

#Region "AT_MEAL_CHANGE"
    Public Function Insert_AT_MEAL_CHANGE(ByVal objData As AT_MEAL_CHANGE_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Insert_AT_MEAL_CHANGE(objData, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function
    Public Function Modify_AT_MEAL_CHANGE(ByVal objData As AT_MEAL_CHANGE_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Modify_AT_MEAL_CHANGE(objData, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Delete_AT_MEAL_CHANGE(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Delete_AT_MEAL_CHANGE(lstID, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Get_AT_MEAL_CHANGEById(ByVal _id As Decimal?) As AT_MEAL_CHANGE_DTO
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Get_AT_MEAL_CHANGEbyID(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function Get_AT_MEAL_CHANGE(ByVal _filter As AT_MEAL_CHANGE_DTO,
                                       ByVal _param As ParamDTO,
                                       Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_MEAL_CHANGE_DTO)
        Dim lst As List(Of AT_MEAL_CHANGE_DTO)

        Using rep As New AttendanceBusinessClient
            Try
                lst = rep.Get_AT_MEAL_CHANGE(_filter, _param, PageIndex, PageSize, Total, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function

    Public Function Get_AT_MEAL_CHANGEApprove(ByVal _filter As AT_MEAL_CHANGE_DTO,
                                       ByVal _param As ParamDTO,
                                       Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_MEAL_CHANGE_DTO)
        Dim lst As List(Of AT_MEAL_CHANGE_DTO)

        Using rep As New AttendanceBusinessClient
            Try
                lst = rep.Get_AT_MEAL_CHANGEApprove(_filter, _param, PageIndex, PageSize, Total, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function


    Public Function Approve_AT_MEAL_CHANGE(ByVal lstID As List(Of Decimal),
                                          ByVal _status As Decimal,
                                         Optional ByVal _reason As String = "") As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Approve_AT_MEAL_CHANGE(lstID, _status, _reason, Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function GETDATA_CHANGE_IMPORT(ByVal obj As ParamDTO) As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.GETDATA_CHANGE_IMPORT(obj, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using


    End Function

    Public Function ImportMealChange(ByVal lstID As List(Of String), ByVal dtData As DataTable,
                                     ByRef dtError As DataTable) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.ImportMealChange(lstID, dtData, dtError, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

#End Region

#Region "AT_MEAL_COST_CHANGE"

    Public Function Insert_AT_MEAL_COST_SETUP(ByVal lst As List(Of AT_MEAL_COST_SETUP_DTO), ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Insert_AT_MEAL_COST_SETUP(lst, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Modify_AT_MEAL_COST_SETUP(ByVal objData As AT_MEAL_COST_SETUP_DTO, ByRef gID As Decimal) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Modify_AT_MEAL_COST_SETUP(objData, Me.Log, gID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function

    Public Function Delete_AT_MEAL_COST_SETUP(ByVal lstID As List(Of Decimal)) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Delete_AT_MEAL_COST_SETUP(lstID)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

    End Function


    Public Function Get_AT_MEAL_COST_SETUPById(ByVal _id As Decimal?) As AT_MEAL_COST_SETUP_DTO
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Get_AT_MEAL_COST_SETUPbyID(_id)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

    Public Function Get_AT_MEAL_COST_SETUP(ByVal _filter As AT_MEAL_COST_SETUP_DTO,
                                       Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_MEAL_COST_SETUP_DTO)
        Dim lst As List(Of AT_MEAL_COST_SETUP_DTO)

        Using rep As New AttendanceBusinessClient
            Try
                lst = rep.Get_AT_MEAL_COST_SETUP(_filter, PageIndex, PageSize, Total, Sorts)
                Return lst
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using

        Return Nothing
    End Function
#End Region

#Region "AT_MEAL_REAL"

    Function Get_AT_MEAL_REAL(ByVal _filter As AT_MEAL_REAL_DTO,
                                 ByVal _param As ParamDTO,
                                 ByRef Total As Integer,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 Optional ByVal Sorts As String = "EFFECT_DATE,EMPLOYEE_CODE,ORG_PATH,ORG_NAME,KITCHEN_NAME,MEAL_NAME,RATION_NAME") As List(Of AT_MEAL_REAL_DTO)
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.Get_AT_MEAL_REAL(_filter, _param, Total, PageIndex, PageSize, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Function Get_AT_MEAL_REAL(ByVal _filter As AT_MEAL_REAL_DTO,
                                 ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "EFFECT_DATE,EMPLOYEE_CODE,ORG_PATH,ORG_NAME,KITCHEN_NAME,MEAL_NAME,RATION_NAME") As List(Of AT_MEAL_REAL_DTO)
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.Get_AT_MEAL_REAL(_filter, _param, 0, 0, Integer.MaxValue, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Function CAL_AT_MEAL_REAL(ByVal _param As ParamDTO) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.CAL_AT_MEAL_REAL(_param, Me.Log)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "AT_MEAL_EXPLAN"

    Function Get_AT_MEAL_EXPLAN(ByVal _filter As AT_MEAL_EXPLAN_DTO,
                                 ByVal _param As ParamDTO,
                                 ByRef Total As Integer,
                                 ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 Optional ByVal Sorts As String = "WORKINGDAY,EMPLOYEE_CODE,ORG_PATH,ORG_NAME,VALTIME,KITCHEN_NAME,MEAL_NAME,RATION_NAME") As List(Of AT_MEAL_EXPLAN_DTO)
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.Get_AT_MEAL_EXPLAN(_filter, _param, Total, PageIndex, PageSize, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Function Get_AT_MEAL_EXPLAN(ByVal _filter As AT_MEAL_EXPLAN_DTO,
                                 ByVal _param As ParamDTO,
                                 Optional ByVal Sorts As String = "WORKINGDAY,EMPLOYEE_CODE,ORG_PATH,ORG_NAME,VALTIME,KITCHEN_NAME,MEAL_NAME,RATION_NAME") As List(Of AT_MEAL_EXPLAN_DTO)
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.Get_AT_MEAL_EXPLAN(_filter, _param, 0, 0, Integer.MaxValue, Sorts, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Function Get_AT_MEAL_EXPLAN_IMPORT(ByVal _param As AT_MEAL_EXPLAN_DTO) As DataSet
        Using rep As New AttendanceBusinessClient
            Try
                Dim lst = rep.Get_AT_MEAL_EXPLAN_IMPORT(_param, Me.Log)
                Return lst
            Catch ex As Exception
                Throw ex
            End Try
        End Using
    End Function

    Public Function Import_AT_MEAL_EXPLAN(ByVal lstData As List(Of AT_MEAL_EXPLAN_DTO),
                                          ByRef dtError As DataTable) As Boolean
        Using rep As New AttendanceBusinessClient
            Try
                Return rep.Import_AT_MEAL_EXPLAN(lstData, Me.Log, dtError)
            Catch ex As Exception
                rep.Abort()
                Throw ex
            End Try
        End Using
    End Function

#End Region

End Class

