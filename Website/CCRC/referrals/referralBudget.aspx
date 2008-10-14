<%@ Page Language="VB" AutoEventWireup="false"  aspcompat=true CodeFile="referralBudget.aspx.vb" inherits=referrals_referral %>
<!--#INCLUDE virtual='/ccrc/utilities/ccrc.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<%


If Request.QueryString().ToString() & "" = "" Then
  eRetPage = Request.ServerVariables("URL")
Else
  eRetPage = Request.ServerVariables("URL") & "?" & Request.QueryString().ToString()
End If
eRetBtnText = "Retry"



	on error resume next
	
	'Server.ScriptTimeout = 1000
	
	Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
	Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)

	Dim referralId
	
	referralId = Request("referralId")
    
	Dim obj
	obj = CreateObject("CCRC_Main.CMain")
	If RedirectIfError("Creating CCRC_Main.CMain", eRetPage, eRetBtnText) Then
		obj = Nothing
		Response.Clear 
		Response.End 
	End If

	Dim Rs
    Dim Proc	
	Proc = "Calling CCRC_Main.CMain.Get_Referral_Budget"
	Rs = obj.Get_Budget_Items(referralId, sUserId, sSQL, iReturnCode, sMessage)
	If RedirectIfError(Proc, eRetPage, eRetBtnText) Then
		obj = Nothing
		Response.Clear 
		Response.End 
	End If
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
	<head>
		<title>Report</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link rel="stylesheet" href="/ccrc/css/main_w.css" type="text/css">
		<link rel="stylesheet" href="/ccrc/css/report.css" type="text/css">
		<script language="javascript">
			function submitForm() {
				var frm = document.master;
				document.master.submit();
				return true;
			}
			
			function cancelForm() {
				location.replace("/splash.aspx");
			}							
		</script>
		
	</head>
	<body>
		<%
			response.Write ("<form id=""master"" name=""master"" method=""post"" action=""referralBudget_process.aspx"">")
			response.Write ("<input type=""hidden"" name=""referralId"" id=""referralId"" value=""" & referralId & """>")
			
			response.Write ("  <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""95%"">")
			response.Write ("    <tr><td colspan=""2"" width=""100%"" class=""HeaderCell"">Monthly Budget</td></tr>")
			response.Write ("  </table>")
	
			Response.Write ("<BR>")
			
			Dim budgetCategory, amt, eTotal, yTotal, subtotal, total, i
			subtotal = 0
			eTotal = 0
			yTotal = 0
			total = 0


			Response.Write ("<table border=""1"">")
			If isValidRS(Rs) Then
			'Response.Write (Rs.RecordCount)
				i = 0
				Rs.MoveFirst
				budgetCategory = Rs.Fields("BUDGET_CATEGORY").Value
				Do While Not Rs.EOF				
					i = CInt(i) + 1
					If CInt(i) = 1 Then
						Response.Write ("<tr><td nowrap class=""ReportRowGroup1"">" & Rs.Fields("BUDGET_CATEGORY").Value & "</td>")
					End If
					If budgetCategory = Rs.Fields("BUDGET_CATEGORY").Value Then
						If Rs.Fields("AMT").Value Is Nothing Then
							amt = 0
						Else
							amt = Rs.Fields("AMT").Value
						End If
						subtotal = CDbl(subtotal) + CDbl(amt)
						PrintDetailRow (i, Rs.Fields("BUDGET_CATEGORY_TYPE_CODE").Value, Rs.Fields("BUDGET_CATEGORY").Value, Rs.Fields("BUDGET_SUBCATEGORY_SEQ_ID").Value, Rs.Fields("BUDGET_SUBCATEGORY").Value, Rs.Fields("AMT").Value, total, eTotal, yTotal)
						Rs.MoveNext
					Else
						PrintSubTotalRow (subtotal)
						budgetCategory = Rs.Fields("BUDGET_CATEGORY").Value
						subtotal = 0
						i = 0
					End If
				Loop
				PrintSubTotalRow (subtotal				)
				PrintTotalRow (total, eTotal, yTotal)
			End If
			Rs.Close
			 Rs = Nothing
			Response.Write ("</table>")
		%>
			<table ID="Table1">
				<tr>
					<td>
						<input name="save" id="save" value="Save" class="btn" type="button" accesskey="s" onclick="submitForm();"
							title="Save the changed information and continue">
					</td>
					<td>
						<input name="cancel" id="cancel" value="Cancel" class="btn" type="button" accesskey="c"
							onclick="cancelForm();" title="Cancel all changes">
					</td>
				</tr>
				<tr><td><a href="referral.aspx?FORMMODE=EDIT&referralId=<%=referralId%>">Click here for Referral page</a></td></tr>				
			</table>
		<%								
			Response.Write("</form>")
		%>
	</body>
</html>
<%





%>
