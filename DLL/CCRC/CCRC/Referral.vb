Imports System.Data.SqlClient

Public Class Referral
    Public Function GetAllServicers() As SqlDataReader
        Dim Sqlstr As String

        Sqlstr = "SELECT * FROM BSN_ENTITY WHERE BSN_ENTITY_TYPE_CODE='2' AND ACTIVE_IND = 'Y' order by BSN_ENTITY_NAME"

        Dim sqlhelper As New Sqlhelper

        Return sqlhelper.GetReader(Sqlstr)

    End Function
    
End Class
