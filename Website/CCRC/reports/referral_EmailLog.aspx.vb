Imports System.Data.SqlClient
Imports CCRC



Partial Class referrals_referral_EmailLog
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dr As SqlDataReader
        Dim objReferral As New CCRC.Referral
        dr = objReferral.GetAllServicers()

        Dim i As Integer


        While (dr.Read())
            'Response.Write((dr.Item("BSN_ENTITY_NAME"))
            Dim item As New ListItem
            'item.Text = dr.Item("BSN_ENTITY_NAME").ToString()
            'item.Value = dr.Item("BSN_ENTITY_SEQ_ID").ToString()
            i = i + 1
            item.Text = dr.Item("BSN_ENTITY_NAME")
            item.Value = dr.Item("BSN_ENTITY_SEQ_ID")
            lstbServicers.Items.Add(item)

        End While

        lstbServicers.Rows = 10


    End Sub

    Protected Sub BtnReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnReport.Click
        Dim objReferral As New CCRC.ReferralEmailHelper
        Dim rs As SqlDataReader

        Dim StDate As DateTime
        Dim EndDate As DateTime
        Dim ServicersList As String

        ServicersList = ""


        StDate = hdnStartDate.Value
        EndDate = hdnEndDate.Value

        'If (hdnStartDate.Value.Trim() = hdnEndDate.Value.Trim()) Then
            hdnStartDate.Value = hdnStartDate.Value + " 00:00 AM"
            hdnEndDate.Value = hdnEndDate.Value + " 11:59 PM"
        'End If

        For Each lstITem As ListItem In lstbServicers.Items
            If lstITem.Selected Then
                ServicersList += lstITem.Value + ","
            End If
        Next lstITem

        If (ServicersList.Length > 0) Then
            ServicersList = ServicersList.Substring(0, ServicersList.Length - 1)
        End If



        rs = objReferral.GetEmailLog(hdnStartDate.Value, hdnEndDate.Value, ServicersList)
        Dim i As Integer

        Response.Clear()
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + "Email_Summary_Report.xls")

        Response.Write("<table style='white-space: nowrap;'>")
        Response.Write("<tr >")
        For i = 0 To rs.FieldCount - 3
            Response.Write("<td><b>" & rs.GetName(i) & "</b></td>")
        Next
        Response.Write("</tr>")

        While (rs.Read())
            Dim fieldName As String
            '------------Modify by Long Cao to support ZIP and LOAN_ID in string when exported to excel
            Response.Write("<tr>")
            For i = 0 To rs.FieldCount - 3
                fieldName = rs.GetName(i)
                If fieldName = "ZIP" Or fieldName = "LOAN_ID" Then
                    Response.Write("<td>'" & rs.Item(i).ToString() & "</td>")
                Else
                    Response.Write("<td>" & rs.Item(i).ToString() & "</td>")
                End If
            Next
            Response.Write("</tr>")
        End While
        Response.Write("</table>")
        Response.End()

        'Response.Write(hdnStartDate.Value & ":" & hdnEndDate.Value & ":" & lstbServicers.SelectedValue & " :" & i)

    End Sub
End Class
