Imports AttendanceBusiness.ServiceContracts
Imports AttendanceDAL
Imports Framework.Data
Imports System.ServiceModel.Activation
Imports System.Reflection
Imports System.Configuration

Namespace AttendanceBusiness.ServiceImplementations
    Partial Class AttendanceBusiness

#Region "List"

        Public Function Get_KITCHEN(ByVal is_blank As Decimal) As DataTable Implements ServiceContracts.IAttendanceBusiness.Get_KITCHEN
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_KITCHEN(is_blank)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Get_KITCHEN_BY_ORG(ByVal is_blank As Decimal, ByVal Meal_ID As Decimal, ByVal _param As ParamDTO, ByVal log As UserLog) As DataTable Implements ServiceContracts.IAttendanceBusiness.Get_KITCHEN_BY_ORG
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_KITCHEN_BY_ORG(is_blank, Meal_ID, _param, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Get_KITCHEN_BY_EMP(ByVal is_blank As Decimal, ByVal employee_id As Decimal, ByVal Meal_ID As Decimal) As DataTable Implements ServiceContracts.IAttendanceBusiness.Get_KITCHEN_BY_EMP
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_KITCHEN_BY_EMP(is_blank, employee_id, Meal_ID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Get_KITCHEN_BY_STUDENT(ByVal is_blank As Decimal, ByVal student_id As Decimal, ByVal Meal_ID As Decimal) As DataTable Implements ServiceContracts.IAttendanceBusiness.Get_KITCHEN_BY_STUDENT
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_KITCHEN_BY_STUDENT(is_blank, student_id, Meal_ID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Get_MEAL_BY_EMP(ByVal is_blank As Decimal,
                                    ByVal employee_id As Decimal) As DataTable Implements ServiceContracts.IAttendanceBusiness.Get_MEAL_BY_EMP
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_MEAL_BY_EMP(is_blank, employee_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Get_MEAL_BY_EMP_EFFECT(ByVal is_blank As Decimal,
                                    ByVal employee_id As Decimal,
                                    ByVal effectDate As Date) As DataTable Implements ServiceContracts.IAttendanceBusiness.Get_MEAL_BY_EMP_EFFECT
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_MEAL_BY_EMP_EFFECT(is_blank, employee_id, effectDate)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Get_MEAL_BY_ORG(ByVal is_blank As Decimal,
                                    ByVal org_id As Decimal) As DataTable Implements ServiceContracts.IAttendanceBusiness.Get_MEAL_BY_ORG
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_MEAL_BY_ORG(is_blank, org_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "AT_KITCHEN"
        Public Function Insert_AT_KITCHEN(ByVal lstData As AT_KITCHEN_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.Insert_AT_KITCHEN
            Using rep As New AttendanceRepository
                Try

                    Return rep.Insert_AT_KITCHEN(lstData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Modify_AT_KITCHEN(ByVal lstData As AT_KITCHEN_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.Modify_AT_KITCHEN
            Using rep As New AttendanceRepository
                Try

                    Return rep.Modify_AT_KITCHEN(lstData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Delete_AT_KITCHEN(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.Delete_AT_KITCHEN
            Using rep As New AttendanceRepository
                Try

                    Return rep.Delete_AT_KITCHEN(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function Get_AT_KITCHENbyID(ByVal _id As Decimal?) As AT_KITCHEN_DTO Implements ServiceContracts.IAttendanceBusiness.Get_AT_KITCHENbyID
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_AT_KITCHENById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Get_AT_KITCHEN(ByVal _filter As AT_KITCHEN_DTO,
                                           Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                           Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_KITCHEN_DTO) Implements ServiceContracts.IAttendanceBusiness.Get_AT_KITCHEN
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_AT_KITCHEN(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Validate_AT_KITCHEN(ByVal _obj As AT_KITCHEN_DTO,
                                            ByVal _action As String,
                                            Optional ByRef _error As String = "") As Boolean _
            Implements ServiceContracts.IAttendanceBusiness.Validate_AT_KITCHEN
            Using rep As New AttendanceRepository
                Try

                    Return rep.Validate_AT_KITCHEN(_obj, _action, _error)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function CHECK_TIME_BY_EMP(ByVal Employee_ID As Decimal, ByVal Effect_date As Date) As Boolean Implements ServiceContracts.IAttendanceBusiness.CHECK_TIME_BY_EMP

            Using rep As New AttendanceRepository
                Try

                    Return rep.CHECK_TIME_BY_EMP(Employee_ID, Effect_date)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Function CHECK_TIME_BY_STUDENT(ByVal STUDENT_ID As Decimal, ByVal Effect_date As Date) As Boolean Implements ServiceContracts.IAttendanceBusiness.CHECK_TIME_BY_STUDENT

            Using rep As New AttendanceRepository
                Try

                    Return rep.CHECK_TIME_BY_STUDENT(STUDENT_ID, Effect_date)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Function CHECK_TIME_BY_ORG(ByVal ORG_ID As Decimal, ByVal Effect_date As Date) As Boolean Implements ServiceContracts.IAttendanceBusiness.CHECK_TIME_BY_ORG

            Using rep As New AttendanceRepository
                Try

                    Return rep.CHECK_TIME_BY_ORG(ORG_ID, Effect_date)
                Catch ex As Exception

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
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AT_TERMINALS_MEALDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_TERMINAL_MEAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_TERMINAL_MEAL(_filter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAT_TERMINAL_MEAL_STATUS(ByVal _filter As AT_TERMINALS_MEALDTO,
                                  Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                    Optional ByVal Sorts As String = "CREATED_DATE desc",
                                   Optional ByVal log As UserLog = Nothing) As List(Of AT_TERMINALS_MEALDTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_TERMINAL_MEAL_STATUS
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetAT_TERMINAL_MEAL_STATUS(_filter, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
        Public Function InsertAT_TERMINAL_MEAL(ByVal lstData As AT_TERMINALS_MEALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_TERMINAL_MEAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_TERMINAL_MEAL(lstData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateAT_TERMINAL_MEAL(ByVal lstData As AT_TERMINALS_MEALDTO,
                                                 ByVal sAction As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ValidateAT_TERMINAL_MEAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.ValidateAT_TERMINAL_MEAL(lstData, sAction)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyAT_TERMINAL_MEAL(ByVal lstData As AT_TERMINALS_MEALDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.ModifyAT_TERMINAL_MEAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.ModifyAT_TERMINAL_MEAL(lstData, log, gID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_TERMINAL_MEAL(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_TERMINAL_MEAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_TERMINAL_MEAL(lstID, log, bActive)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_TERMINAL_MEAL(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.DeleteAT_TERMINAL_MEAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_TERMINAL_MEAL(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "AT_MEAL_SETUP"


        Public Function Modify_AT_MEAL_SETUP(ByVal objData As AT_MEAL_SETUP_DTO,
                                         ByVal log As UserLog) As Boolean Implements ServiceContracts.IAttendanceBusiness.Modify_AT_MEAL_SETUP
            Using rep As New AttendanceRepository
                Try

                    Return rep.Modify_AT_MEAL_SETUP(objData, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Delete_AT_MEAL_SETUP(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.Delete_AT_MEAL_SETUP
            Using rep As New AttendanceRepository
                Try

                    Return rep.Delete_AT_MEAL_SETUP(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function Get_AT_MEAL_SETUPbyID(ByVal _id As Decimal?) As AT_MEAL_SETUP_DTO Implements ServiceContracts.IAttendanceBusiness.Get_AT_MEAL_SETUPbyID
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_AT_MEAL_SETUPById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Get_AT_MEAL_SETUP(ByVal _filter As AT_MEAL_SETUP_DTO,
                                           Optional ByVal _param As ParamDTO = Nothing,
                                           Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                           Optional ByVal Sorts As String = "ORG_PATH",
                                           Optional ByVal log As UserLog = Nothing) As List(Of AT_MEAL_SETUP_DTO) Implements ServiceContracts.IAttendanceBusiness.Get_AT_MEAL_SETUP
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_AT_MEAL_SETUP(_filter, _param, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "AT_KITCHEN_ORG"

        Public Function GetAT_KITCHEN_ORG(ByVal filter As AT_KITCHEN_ORG_DTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_KITCHEN_ORG_DTO) Implements ServiceContracts.IAttendanceBusiness.GetAT_KITCHEN_ORG
            Using rep As New AttendanceRepository
                Try

                    Dim lst = rep.GetAT_KITCHEN_ORG(filter, PageIndex, PageSize, Total, Sorts)
                    Return lst
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertAT_KITCHEN_ORG(ByVal objAT_KITCHEN_ORG As List(Of AT_KITCHEN_ORG_DTO), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.InsertAT_KITCHEN_ORG
            Using rep As New AttendanceRepository
                Try

                    Return rep.InsertAT_KITCHEN_ORG(objAT_KITCHEN_ORG, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckKitchenInUsing(ByVal lstID As List(Of Decimal), ByVal orgID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.CheckKitchenInUsing
            Using rep As New AttendanceRepository
                Try

                    Return rep.CheckKitchenInUsing(lstID, orgID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteAT_KITCHEN_ORG(ByVal objAT_KITCHEN_ORG As List(Of Decimal), ByVal log As UserLog) As Boolean _
            Implements ServiceContracts.IAttendanceBusiness.DeleteAT_KITCHEN_ORG
            Using rep As New AttendanceRepository
                Try

                    Return rep.DeleteAT_KITCHEN_ORG(objAT_KITCHEN_ORG, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ActiveAT_KITCHEN_ORG(ByVal objAT_KITCHEN_ORG As List(Of Decimal), ByVal sActive As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.IAttendanceBusiness.ActiveAT_KITCHEN_ORG
            Using rep As New AttendanceRepository
                Try

                    Return rep.ActiveAT_KITCHEN_ORG(objAT_KITCHEN_ORG, sActive, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "AT_MEAL_MANAGER"
        Public Function Insert_AT_MEAL_MANAGER(ByVal lstData As List(Of AT_MEAL_MANAGER_DTO), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.Insert_AT_MEAL_MANAGER
            Using rep As New AttendanceRepository
                Try

                    Return rep.Insert_AT_MEAL_MANAGER(lstData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Insert_AT_MEAL_MANAGER_BY_ORG(ByVal lstData As List(Of AT_MEAL_MANAGER_DTO), ByVal _param As ParamDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IAttendanceBusiness.Insert_AT_MEAL_MANAGER_BY_ORG
            Using rep As New AttendanceRepository
                Try

                    Return rep.Insert_AT_MEAL_MANAGER_BY_ORG(lstData, _param, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Insert_AT_MEAL_MANAGER_BY_EMP(ByVal lstData As List(Of AT_MEAL_MANAGER_DTO), ByVal lstEmp As List(Of EmployeeDTO), ByVal _param As ParamDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IAttendanceBusiness.Insert_AT_MEAL_MANAGER_BY_EMP
            Using rep As New AttendanceRepository
                Try

                    Return rep.Insert_AT_MEAL_MANAGER_BY_EMP(lstData, lstEmp, _param, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Delete_AT_MEAL_MANAGER(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.Delete_AT_MEAL_MANAGER
            Using rep As New AttendanceRepository
                Try

                    Return rep.Delete_AT_MEAL_MANAGER(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function Get_AT_MEAL_MANAGERbyID(ByVal obj As AT_MEAL_MANAGER_DTO) As List(Of AT_MEAL_MANAGER_DTO) Implements ServiceContracts.IAttendanceBusiness.Get_AT_MEAL_MANAGERbyID
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_AT_MEAL_MANAGERById(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Get_AT_MEAL_MANAGER(ByVal _filter As AT_MEAL_MANAGER_DTO,
                                            ByVal _param As ParamDTO,
                                            Optional ByVal PageIndex As Integer = 0,
                                            Optional ByVal PageSize As Integer = Integer.MaxValue,
                                            Optional ByRef Total As Integer = 0,
                                            Optional ByVal Sorts As String = "EMPLOYEE_CODE asc, EFFECT_DATE asc, MEAL_ID asc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of AT_MEAL_MANAGER_DTO) Implements ServiceContracts.IAttendanceBusiness.Get_AT_MEAL_MANAGER
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_AT_MEAL_MANAGER(_filter, _param, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Validate_AT_MEAL_SWAP(ByVal objData As AT_MEAL_SWAP_DTO,
                                       ByVal _action As String,
                                       Optional ByRef _error As String = "") As Boolean _
                                   Implements ServiceContracts.IAttendanceBusiness.Validate_AT_MEAL_SWAP
            Using rep As New AttendanceRepository
                Try

                    Return rep.Validate_AT_MEAL_SWAP(objData, _action, _error)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function Swap_AT_MEAL_MANAGER(ByVal objData As AT_MEAL_SWAP_DTO,
                                             ByVal log As UserLog) As Boolean Implements ServiceContracts.IAttendanceBusiness.Swap_AT_MEAL_MANAGER
            Using rep As New AttendanceRepository
                Try
                    Return rep.Swap_AT_MEAL_MANAGER(objData, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GETDATA_MANAGER_IMPORT(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet Implements ServiceContracts.IAttendanceBusiness.GETDATA_MANAGER_IMPORT
            Using rep As New AttendanceRepository
                Try

                    Return rep.GETDATA_MANAGER_IMPORT(obj, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ImportMealManager(ByVal dtData As DataTable,
                                      ByVal StartDate As Date,
                                      ByVal EndDate As Date,
                                      ByRef dtError As DataTable,
                                      ByVal log As UserLog) As Boolean Implements ServiceContracts.IAttendanceBusiness.ImportMealManager
            Using rep As New AttendanceRepository
                Try

                    Return rep.ImportMealManager(dtData, StartDate, EndDate, dtError, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GETDATA_CHANGE_IMPORT(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet Implements ServiceContracts.IAttendanceBusiness.GETDATA_CHANGE_IMPORT
            Using rep As New AttendanceRepository
                Try

                    Return rep.GETDATA_CHANGE_IMPORT(obj, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ImportMealChange(ByVal lstID As List(Of String), ByVal dtData As DataTable,
                                     ByRef dtError As DataTable,
                                      ByVal log As UserLog) As Boolean Implements ServiceContracts.IAttendanceBusiness.ImportMealChange
            Using rep As New AttendanceRepository
                Try

                    Return rep.ImportMealChange(lstID, dtData, dtError, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetListEmployee_ByOrg(ByVal _param As ParamDTO, ByVal log As UserLog) As List(Of EmployeeDTO) Implements ServiceContracts.IAttendanceBusiness.GetListEmployee_ByOrg
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetListEmployee_ByOrg(_param, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "AT_MEAL_STUDENT"
        Public Function Insert_AT_MEAL_STUDENT(ByVal lstData As List(Of AT_MEAL_STUDENT_DTO), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.Insert_AT_MEAL_STUDENT
            Using rep As New AttendanceRepository
                Try

                    Return rep.Insert_AT_MEAL_STUDENT(lstData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Insert_AT_MEAL_STUDENT_BY_ORG(ByVal lstData As List(Of AT_MEAL_STUDENT_DTO), ByVal _param As ParamDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IAttendanceBusiness.Insert_AT_MEAL_STUDENT_BY_ORG
            Using rep As New AttendanceRepository
                Try

                    Return rep.Insert_AT_MEAL_STUDENT_BY_ORG(lstData, _param, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Insert_AT_MEAL_STUDENT_BY_EMP(ByVal lstData As List(Of AT_MEAL_STUDENT_DTO), ByVal lstEmp As List(Of EmployeeDTO), ByVal _param As ParamDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.IAttendanceBusiness.Insert_AT_MEAL_STUDENT_BY_EMP
            Using rep As New AttendanceRepository
                Try

                    Return rep.Insert_AT_MEAL_STUDENT_BY_EMP(lstData, lstEmp, _param, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Delete_AT_MEAL_STUDENT(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.Delete_AT_MEAL_STUDENT
            Using rep As New AttendanceRepository
                Try

                    Return rep.Delete_AT_MEAL_STUDENT(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


        Public Function Get_AT_MEAL_STUDENTbyID(ByVal obj As AT_MEAL_STUDENT_DTO) As List(Of AT_MEAL_STUDENT_DTO) Implements ServiceContracts.IAttendanceBusiness.Get_AT_MEAL_STUDENTbyID
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_AT_MEAL_STUDENTById(obj)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Get_AT_MEAL_STUDENT(ByVal _filter As AT_MEAL_STUDENT_DTO,
                                            ByVal _param As ParamDTO,
                                            Optional ByVal PageIndex As Integer = 0,
                                            Optional ByVal PageSize As Integer = Integer.MaxValue,
                                            Optional ByRef Total As Integer = 0,
                                            Optional ByVal Sorts As String = "STUDENT_CODE asc, EFFECT_DATE asc, MEAL_ID asc",
                                            Optional ByVal log As UserLog = Nothing) As List(Of AT_MEAL_STUDENT_DTO) Implements ServiceContracts.IAttendanceBusiness.Get_AT_MEAL_STUDENT
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_AT_MEAL_STUDENT(_filter, _param, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GETDATA_STUDENT_IMPORT(ByVal obj As ParamDTO, ByVal log As UserLog) As DataSet Implements ServiceContracts.IAttendanceBusiness.GETDATA_STUDENT_IMPORT
            Using rep As New AttendanceRepository
                Try

                    Return rep.GETDATA_STUDENT_IMPORT(obj, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function ImportMealSTUDENT(ByVal dtData As DataTable,
                                      ByVal StartDate As Date,
                                      ByVal EndDate As Date,
                                      ByRef dtError As DataTable,
                                      ByVal log As UserLog) As Boolean Implements ServiceContracts.IAttendanceBusiness.ImportMealSTUDENT
            Using rep As New AttendanceRepository
                Try

                    Return rep.ImportMealStudent(dtData, StartDate, EndDate, dtError, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetListStudent_ByOrg(ByVal _param As ParamDTO, ByVal log As UserLog) As List(Of EmployeeDTO) Implements ServiceContracts.IAttendanceBusiness.GetListStudent_ByOrg
            Using rep As New AttendanceRepository
                Try

                    Return rep.GetListStudent_ByOrg(_param, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "AT_MEAL_FORECAST_SUM"

        Function Get_AT_MEAL_FORECAST_SUM(ByVal _filter As AT_MEAL_FORECAST_SUM_DTO,
                                          ByVal _param As ParamDTO,
                                          ByRef Total As Integer,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          Optional ByVal Sorts As String = "CREATED_DATE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of AT_MEAL_FORECAST_SUM_DTO) _
                                      Implements ServiceContracts.IAttendanceBusiness.Get_AT_MEAL_FORECAST_SUM
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_AT_MEAL_FORECAST_SUM(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using

        End Function

        Function CAL_AT_MEAL_FORECAST_SUM(ByVal _param As ParamDTO,
                                     ByVal log As UserLog) As Boolean _
                                 Implements ServiceContracts.IAttendanceBusiness.CAL_AT_MEAL_FORECAST_SUM

            Using rep As New AttendanceRepository
                Try

                    Return rep.CAL_AT_MEAL_FORECAST_SUM(_param, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function


        Public Function Get_AT_MEAL_FORECAST_SUM_IMPORT(ByVal _param As AT_MEAL_FORECAST_SUM_DTO,
                                           ByVal log As UserLog) As DataTable _
                                       Implements ServiceContracts.IAttendanceBusiness.Get_AT_MEAL_FORECAST_SUM_IMPORT
            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.Get_AT_MEAL_FORECAST_SUM_IMPORT(_param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function Import_AT_MEAL_FORECAST_SUM(ByVal lstData As List(Of AT_MEAL_FORECAST_SUM_DTO),
                                           ByVal log As UserLog) As Boolean _
                                       Implements IAttendanceBusiness.Import_AT_MEAL_FORECAST_SUM

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.Import_AT_MEAL_FORECAST_SUM(lstData, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


#End Region

#Region " AT_MEAL_PARTNER"
        Public Function Insert_AT_MEAL_PARTNER(ByVal objData As AT_MEAL_PARTNER_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.Insert_AT_MEAL_PARTNER
            Using rep As New AttendanceRepository
                Try

                    Return rep.Insert_AT_MEAL_PARTNER(objData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Modify_AT_MEAL_PARTNER(ByVal objData As AT_MEAL_PARTNER_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.Modify_AT_MEAL_PARTNER
            Using rep As New AttendanceRepository
                Try

                    Return rep.Modify_AT_MEAL_PARTNER(objData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Delete_AT_MEAL_PARTNER(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.Delete_AT_MEAL_PARTNER
            Using rep As New AttendanceRepository
                Try

                    Return rep.Delete_AT_MEAL_PARTNER(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function



        Public Function Get_AT_MEAL_PARTNERbyID(ByVal _id As Decimal?) As AT_MEAL_PARTNER_DTO Implements ServiceContracts.IAttendanceBusiness.Get_AT_MEAL_PARTNERbyID
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_AT_MEAL_PARTNERById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Get_AT_MEAL_PARTNER(ByVal _filter As AT_MEAL_PARTNER_DTO,
                                       ByVal _param As ParamDTO,
                                           Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                           Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal Log As UserLog = Nothing) As List(Of AT_MEAL_PARTNER_DTO) Implements ServiceContracts.IAttendanceBusiness.Get_AT_MEAL_PARTNER
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_AT_MEAL_PARTNER(_filter, _param, PageIndex, PageSize, Total, Sorts, Log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "AT_MEAL_CHANGE"

        Public Function Insert_AT_MEAL_CHANGE(ByVal objData As AT_MEAL_CHANGE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.Insert_AT_MEAL_CHANGE
            Using rep As New AttendanceRepository
                Try

                    Return rep.Insert_AT_MEAL_CHANGE(objData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Modify_AT_MEAL_CHANGE(ByVal objData As AT_MEAL_CHANGE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.Modify_AT_MEAL_CHANGE
            Using rep As New AttendanceRepository
                Try

                    Return rep.Modify_AT_MEAL_CHANGE(objData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Delete_AT_MEAL_CHANGE(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean Implements ServiceContracts.IAttendanceBusiness.Delete_AT_MEAL_CHANGE
            Using rep As New AttendanceRepository
                Try

                    Return rep.Delete_AT_MEAL_CHANGE(lstID, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function Get_AT_MEAL_CHANGEbyID(ByVal _id As Decimal?) As AT_MEAL_CHANGE_DTO Implements ServiceContracts.IAttendanceBusiness.Get_AT_MEAL_CHANGEbyID
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_AT_MEAL_CHANGEById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Get_AT_MEAL_CHANGE(ByVal _filter As AT_MEAL_CHANGE_DTO,
                                       ByVal _param As ParamDTO,
                                           Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                           Optional ByVal Sorts As String = "CREATED_DATE desc",
                                           Optional ByVal log As UserLog = Nothing) As List(Of AT_MEAL_CHANGE_DTO) Implements ServiceContracts.IAttendanceBusiness.Get_AT_MEAL_CHANGE
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_AT_MEAL_CHANGE(_filter, _param, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Get_AT_MEAL_CHANGEApprove(ByVal _filter As AT_MEAL_CHANGE_DTO,
                                           ByVal _param As ParamDTO,
                                           Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                           Optional ByVal Sorts As String = "CREATED_DATE desc",
                                           Optional ByVal log As UserLog = Nothing) As List(Of AT_MEAL_CHANGE_DTO) _
                                       Implements ServiceContracts.IAttendanceBusiness.Get_AT_MEAL_CHANGEApprove
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_AT_MEAL_CHANGEApprove(_filter, _param, PageIndex, PageSize, Total, Sorts, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Approve_AT_MEAL_CHANGE(ByVal lstID As List(Of Decimal),
                                          ByVal _status As Decimal,
                                          ByVal _reason As String,
                                          ByVal log As UserLog) As Boolean _
                                      Implements ServiceContracts.IAttendanceBusiness.Approve_AT_MEAL_CHANGE
            Using rep As New AttendanceRepository
                Try

                    Return rep.Approve_AT_MEAL_CHANGE(lstID, _status, _reason, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "AT_MEAL_COST_SETUP"

        Public Function Insert_AT_MEAL_COST_SETUP(ByVal lst As List(Of AT_MEAL_COST_SETUP_DTO), ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.Insert_AT_MEAL_COST_SETUP
            Using rep As New AttendanceRepository
                Try

                    Return rep.Insert_AT_MEAL_COST_SETUP(lst, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Modify_AT_MEAL_COST_SETUP(ByVal objData As AT_MEAL_COST_SETUP_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean Implements ServiceContracts.IAttendanceBusiness.Modify_AT_MEAL_COST_SETUP
            Using rep As New AttendanceRepository
                Try

                    Return rep.Modify_AT_MEAL_COST_SETUP(objData, log, gID)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Delete_AT_MEAL_COST_SETUP(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.IAttendanceBusiness.Delete_AT_MEAL_COST_SETUP
            Using rep As New AttendanceRepository
                Try

                    Return rep.Delete_AT_MEAL_COST_SETUP(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function Get_AT_MEAL_COST_SETUPbyID(ByVal _id As Decimal?) As AT_MEAL_COST_SETUP_DTO Implements ServiceContracts.IAttendanceBusiness.Get_AT_MEAL_COST_SETUPbyID
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_AT_MEAL_COST_SETUPById(_id)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function

        Public Function Get_AT_MEAL_COST_SETUP(ByVal _filter As AT_MEAL_COST_SETUP_DTO,
                                           Optional ByVal PageIndex As Integer = 0,
                                           Optional ByVal PageSize As Integer = Integer.MaxValue,
                                           Optional ByRef Total As Integer = 0,
                                           Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AT_MEAL_COST_SETUP_DTO) Implements ServiceContracts.IAttendanceBusiness.Get_AT_MEAL_COST_SETUP
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_AT_MEAL_COST_SETUP(_filter, PageIndex, PageSize, Total, Sorts)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "AT_MEAL_REAL"

        Function Get_AT_MEAL_REAL(ByVal _filter As AT_MEAL_REAL_DTO,
                                          ByVal _param As ParamDTO,
                                          ByRef Total As Integer,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          Optional ByVal Sorts As String = "CREATED_DATE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of AT_MEAL_REAL_DTO) _
                                      Implements ServiceContracts.IAttendanceBusiness.Get_AT_MEAL_REAL
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_AT_MEAL_REAL(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using

        End Function

        Function CAL_AT_MEAL_REAL(ByVal _param As ParamDTO,
                                     ByVal log As UserLog) As Boolean _
                                 Implements ServiceContracts.IAttendanceBusiness.CAL_AT_MEAL_REAL

            Using rep As New AttendanceRepository
                Try

                    Return rep.CAL_AT_MEAL_REAL(_param, log)
                Catch ex As Exception

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
                                          Optional ByVal Sorts As String = "CREATED_DATE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of AT_MEAL_EXPLAN_DTO) _
                                      Implements ServiceContracts.IAttendanceBusiness.Get_AT_MEAL_EXPLAN
            Using rep As New AttendanceRepository
                Try

                    Return rep.Get_AT_MEAL_EXPLAN(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                Catch ex As Exception

                    Throw ex
                End Try
            End Using

        End Function

        Public Function Get_AT_MEAL_EXPLAN_IMPORT(ByVal _param As AT_MEAL_EXPLAN_DTO,
                                           ByVal log As UserLog) As DataSet _
                                       Implements ServiceContracts.IAttendanceBusiness.Get_AT_MEAL_EXPLAN_IMPORT
            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.Get_AT_MEAL_EXPLAN_IMPORT(_param, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Function Import_AT_MEAL_EXPLAN(ByVal lstData As List(Of AT_MEAL_EXPLAN_DTO),
                                           ByVal log As UserLog,
                                          ByRef dtError As DataTable) As Boolean _
                                       Implements IAttendanceBusiness.Import_AT_MEAL_EXPLAN

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.Import_AT_MEAL_EXPLAN(lstData, log, dtError)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


#End Region

#Region "Report"

        Public Function ExportReport(ByVal _reportCode As String,
                                     ByVal _pkgName As String,
                                     ByVal obj As ParamDTO,
                                     ByVal log As UserLog) As DataSet _
                                       Implements IAttendanceBusiness.ExportReport

            Using rep As New AttendanceRepository
                Try
                    Dim lst = rep.ExportReport(_reportCode, _pkgName, obj, log)
                    Return lst
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function


#End Region

    End Class
End Namespace

