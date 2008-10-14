Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient
Imports Microsoft.Win32
Imports System.IO
Public NotInheritable Class Sqlhelper


    Public Sub New()
    End Sub

    Public Function GetConnectionString() As SqlConnection
        Dim MyConnection As New SqlConnection(GetConnectionStringFromRegistry())
        Return MyConnection
    End Function

    Public Function GetReader(ByVal SQL As String) As SqlDataReader
        Dim rd As SqlDataReader = Nothing

        Dim MyConnection As SqlConnection = GetConnectionString()

        Using selectCommand As SqlCommand = MyConnection.CreateCommand()
            selectCommand.CommandText = SQL
            ' 
            selectCommand.Connection.Open()
            rd = selectCommand.ExecuteReader(CommandBehavior.CloseConnection)
        End Using


        Return rd
    End Function

    Public Sub ExecuteNonQuery(ByVal SQL As String)
        Using MyConnection As SqlConnection = GetConnectionString()
            Using command As New SqlCommand(SQL)
                command.Connection = MyConnection
                command.Connection.Open()
                command.ExecuteNonQuery()
                command.Connection.Close()
            End Using
        End Using
    End Sub

    Public Sub ExecuteNonQuery(ByVal storedProcedureName As String, ByRef outputParam As Integer, ByVal ParamArray parameterValues As Object())
        Using MyConnection As SqlConnection = GetConnectionString()
            Using command As New SqlCommand(storedProcedureName)
                command.CommandType = CommandType.StoredProcedure
                command.Connection = MyConnection
                For i As Integer = 0 To parameterValues.Length - 1
                    command.Parameters.Add(parameterValues(i))
                Next
                command.Connection.Open()
                command.ExecuteNonQuery()

                outputParam = CInt(command.Parameters(0).Value)
            End Using
        End Using
    End Sub


    Public Function ExecuteScalar(ByVal SQL As String, ByRef outputParam As String) As String
        Using MyConnection As SqlConnection = GetConnectionString()
            Using command As New SqlCommand(SQL)
                command.Connection = MyConnection
                command.Connection.Open()
                outputParam = command.ExecuteScalar().ToString()
                Return outputParam
            End Using
        End Using
    End Function
    Public Function GetConnectionStringFromRegistry() As String

        Dim myCo As RegistryKey
        Dim rootKey As String
        Dim conStr As String = ""
        rootKey = "SOFTWARE\MYCompany"
        myCo = Registry.LocalMachine.OpenSubKey(rootKey)
        If myCo IsNot Nothing Then
            conStr = DirectCast(myCo.GetValue("DOTNET_CONNECTION_STRING", ""), String)
            myCo.Close()
        End If
        Return conStr
    End Function
End Class