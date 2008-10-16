Imports System.Data.SqlClient

Public Class Referral
    Public Function GetAllServicers() As SqlDataReader
        Dim Sqlstr As String

        Sqlstr = "SELECT * FROM BSN_ENTITY WHERE BSN_ENTITY_TYPE_CODE='2' AND ACTIVE_IND = 'Y' order by BSN_ENTITY_NAME"

        Dim sqlhelper As New Sqlhelper

        Return sqlhelper.GetReader(Sqlstr)

    End Function

    Public Function IsUseSecureEmail(ByVal Email As String) As Boolean
        Dim Sqlstr As String

        Sqlstr = "SELECT dbo.UseSecureEmail(@Param) as Result"

        Dim sqlhelper As New Sqlhelper
        '-----------------
        Dim connection As SqlConnection = sqlhelper.GetConnectionString()
        'Dim connection As SqlConnection = New SqlConnection("Data Source=HPF-01;Initial Catalog=CCRC;Persist Security Info=True;User ID=CCRC_APP_USER_1;Password=Lj7tC28s")

        Dim command As New SqlCommand(Sqlstr, connection)
        command.Parameters.AddWithValue("@Param", Email)
        '-----------------
        connection.Open()
        Dim result As Boolean = command.ExecuteScalar() = "Y"
        connection.Close()
        '-----------------
        Return result
    End Function
    
End Class
