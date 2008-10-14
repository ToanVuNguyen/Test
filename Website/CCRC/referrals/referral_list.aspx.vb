
Partial Class referrals_referral_list
    Inherits System.Web.UI.Page
    Protected eRetPage
    Protected eRetBtnText

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Function WriteRecord(ByVal Rs)
        On Error Resume Next
        '	dim x
        '	for each x in rs.fields
        '		response.Write x.name & " - " & x.value & "<BR>"
        '	next
        '	response.End
        Response.Write("<TR>")
        Response.Write("<td>" & Rs.Fields("LOAN_ID").Value & "</td>")
        Response.Write("<td>" & Rs.Fields("ZIP").Value & "</td>")
        Response.Write("<td>" & Rs.Fields("AGENCY_REFERRAL_ID").Value & "</td>")
        Response.Write("<td>" & Utilities.buildName(Rs.Fields("FIRST_NAME").Value, Rs.Fields("MI_NAME").Value, Rs.Fields("LAST_NAME").Value, "") & "</td>")
        Response.Write("<td>" & Utilities.buildName(Rs.Fields("ADDR_LINE_1").Value, Rs.Fields("CITY").Value, Rs.Fields("STATE").Value, Rs.Fields("ZIP").Value) & "</td>")
        Response.Write("<td>" & Rs.Fields("AGENCY").Value & "</td>")
        Response.Write("<td>" & Utilities.buildName(Rs.Fields("COUNSELOR_FIRST_NAME").Value, Rs.Fields("COUNSELOR_MID_INIT_NAME").Value, Rs.Fields("COUNSELOR_LAST_NAME").Value, "") & "</td>")
        Response.Write("<td>" & Rs.Fields("REFERRAL_RESULT_DATE").Value & "</td>")
        Response.Write("<td>" & Rs.Fields("REFERRAL_RESULT").Value & "</td>")
        Response.Write("<td><A HREF='referral.aspx?FormMode=EDIT&referralId=" & Rs.Fields("REFERRAL_SEQ_ID").Value & "'>[View]</A></td>")
        Response.Write("</TR>")
        If err.number <> 0 Then
            response.Write(err.Description)
            response.End()
        End If
    End Function
    
End Class
