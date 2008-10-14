<%@ Page Language="VBScript" aspcompat=true AutoEventWireup="false"%>
<!--#INCLUDE virtual='/ccrc/utilities/ccrc.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<!--#include virtual="/ccrc/utilities/ccrc_report.inc"-->
<!--#include virtual="/ccrc/utilities/global.inc"-->
<%
Dim eRetPage
Dim eRetBtnText
Dim Temp 
Dim TempStr as string
Temp = Request.QueryString().ToString()
If Temp = "" Then
  eRetPage = Request.ServerVariables("URL")
Else
    TempStr = Request.ServerVariables("URL").ToString() & "?" & Request.QueryString().ToString()
    eRetPage = TempStr
End If
eRetBtnText = "Retry"


	on error resume next
	
	'Server.ScriptTimeout = 1000
	
	Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
	Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)

	Dim programIds, agencyIds, servicerIds, locationIds
	Dim acctPrd1, acctPrd2, startDate, endDate 
	Dim orderBy, reportFormat, reportType

	programIds = Request("program_ids")
	agencyIds = Request("agency_ids")
	servicerIds = Request("servicer_ids")
	locationIds = Request("location_ids")
	acctPrd1 = Request("ACCT_PRD1")
	acctPrd2 = Request("ACCT_PRD2")
	startDate = Request("start_Date")
	endDate = Request("end_Date")
	orderBy = Request("order_by")
	reportFormat = Request("report_format")
	reportType = Request("report_type")
	
	Dim obj
	obj = CreateObject("CCRC_Main.CMain")
	If RedirectIfError("Creating CCRC_Main.CMain", eRetPage, eRetBtnText) Then
		obj = Nothing
		Response.Clear 
		Response.End 
	End If

	Dim Rs, Rs1, Rs2, Proc, outcomeCode, hasContract, actionPlan
	
	' To Delete - begin
	dim str
	str = Request.Cookies("CCRCCookie")("profileRoles")
	'orderBy =  16213 
	' - End
    
	If UCase(reportFormat) = "HTML" Then
		Proc = "Calling CCRC_Main.CMain.Get_Referral_Counts"
		Call obj.Get_Referral_Counts(programIds, agencyIds, servicerIds, locationIds, _
			acctPrd1, acctPrd2, startDate, endDate, reportType, orderBy, _
			Request.Cookies("CCRCCookie")("profileSeqId"), _
			sUserId, Rs, Rs1, Rs2, sSQL, iReturnCode, sMessage)
	Else
		If "Referrals" = Request("REPORT_TYPE") Then
			Proc = "Calling CCRC_Main.CMain.Get_Referral_List_Excel"
			Rs1 = obj.Get_Referral_List_Excel(programIds, agencyIds, servicerIds, locationIds, _
				acctPrd1,acctPrd2,startDate,endDate,reportType,outcomeCode, hasContract, actionPlan, _
				Request.Cookies("CCRCCookie")("profileSeqId"), _
				sUserId, sSQL, iReturnCode, sMessage)
		Else
			Proc = "Calling CCRC_Main.CMain.Get_Referral_List_Excel"
			Rs1 = obj.Get_Referral_List_Excel(programIds, agencyIds, servicerIds, locationIds, _
				acctPrd1,acctPrd2,startDate,endDate,reportType,outcomeCode, hasContract, actionPlan, _
				Request.Cookies("CCRCCookie")("profileSeqId"), _
				sUserId, sSQL, iReturnCode, sMessage)
		End If
	End If

	If RedirectIfError(Proc, eRetPage, eRetBtnText) Then
		obj = Nothing
		'If Rs1.State = adStateOpen Then
		'	Rs1.Close
		'End If
		'If Rs2.State = adStateOpen Then
		'	Rs2.Close
		'End If
        Rs1.Close
        Rs2.Close
		Rs1 = Nothing
		Rs2 = Nothing
		Response.Clear 
		Response.End 
	End If
	programIds = Request("program_ids")
	agencyIds = Request("agency_ids")
	servicerIds = Request("servicer_ids")
	locationIds = Request("location_ids")
	acctPrd1 = Request("ACCT_PRD1")
	acctPrd2 = Request("ACCT_PRD2")
	startDate = Request("start_Date")
	endDate = Request("end_Date")
	orderBy = Request("order_by")
	reportFormat = Request("report_format")
	reportType = Request("report_type")

	If UCase(reportFormat) = "EXCEL" Then
	' Output raw data to Excel
		Response.Clear
		Response.ContentType = "application/vnd.ms-excel"
		Call WriteRsToExcel(Rs1)
		'If Rs1.State = adStateOpen Then
		'	Rs1.Close
		'End If
		'If Rs2.State = adStateOpen Then
		'	Rs2.Close
		'End If
        'Rs1.Close
        'Rs2.Close
		Rs1 = Nothing
		Rs2 = Nothing
		Response.End 
	ElseIf UCase(reportFormat) = "XML" Then
	' Output raw data to XML
		Dim xDOM As System.Xml.XmlDocument
 		xDOM = New  System.Xml.XmlDocument()
	    If Not Rs1.EOF
		    Dim myDA As System.Data.OleDb.OleDbDataAdapter = New System.Data.OleDb.OleDbDataAdapter
            Dim myDS As System.Data.DataSet = New System.Data.DataSet("CCRCReport")
            myDA.Fill(myDS, Rs1, "XML")
            Response.Clear()
            Response.AddHeader("content-disposition", "attachment;filename=CCRC_Report.xml") 
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/vnd.xml"
            Response.Write(myDS.GetXml)
            Response.End()
		End If
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
			function submit_form(action, reporttype) {
				if ("REPORT_CONFIG" == action) {
					document.all.master.action = "ccrc_Report_Config.aspx";
				} 
				else {
					document.all.master.action = "";
				}
				document.all.master.submit();
				return;
			}
		</script>
	</head>
	<body>
		<%
			Response.Write("<form id=""master"" method=""post"" action="""">")				
			Response.Write("<input type=""hidden"" name=""order_by"" value=""" & orderBy & """>")
			Response.Write("<input type=""hidden"" name=""program_ids"" value=""" & programIds & """>")
			Response.Write("<input type=""hidden"" name=""agency_ids"" value=""" & agencyIds & """>")
			Response.Write("<input type=""hidden"" name=""servicer_ids"" value=""" & servicerIds & """>")
			Response.Write("<input type=""hidden"" name=""location_ids"" value=""" & locationIds & """>")
			Response.Write("<input type=""hidden"" name=""acct_prd1"" value=""" & acctPrd1 & """>")
			Response.Write("<input type=""hidden"" name=""acct_prd2"" value=""" & acctPrd2 & """>")
			Response.Write("<input type=""hidden"" name=""start_date"" value=""" & startDate & """>")
			Response.Write("<input type=""hidden"" name=""end_date"" value=""" & endDate & """>")
			Response.Write("<input type=""hidden"" name=""report_format"" value=""" & reportFormat & """>")
			Response.Write("<input type=""hidden"" name=""report_type"" value=""" & reportType & """>")
			
			Response.Write("  <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""95%"">")
			If reportType = "Billables" Then
				Response.Write("    <tr><td colspan=""2"" width=""100%"" class=""HeaderCell"">Completed Counseling Report: ")
				Response.Write(acctPrd1 & " - " & acctPrd2)
			Else
				Response.Write("    <tr><td colspan=""2"" width=""100%"" class=""HeaderCell"">" & reportType & " Report: ")
				Response.Write(startDate & " - " & endDate)
			End If

			Response.Write("</td></tr>")
			Response.Write("    <tr>")
			Response.Write("      <td bgcolor=""#CCCCCC"" Class=""DataRight"">")
			Response.Write("        <a HREF=""javascript:submit_form('REPORT_CONFIG', '');"">Customize</a>&nbsp;")
			Response.Write("      </td>")
			Response.Write("    </tr>")
			Response.Write("  </table>")
	
			Response.Write("<BR>")
			
			If Not isValidRS(Rs) Then
				Response.Write("<table>")
				Response.Write("<tr><td>No records found.</tr></td>")
				Response.Write("</table>")
			Else
				Dim Contracted, ContractedCount, Billable, BillableCount, Dscr, DscrCount

				Response.Write("<table border=""1"">")
				Rs.MoveFirst
				Do While Not Rs.EOF
					Response.Write("<tr><td nowrap class=""ReportRowGroup1"">" & Rs.Fields("HAS_CONTRACT").Value & "</td>")
					Response.Write("<td class=""ReportRowTotal"">" & Rs.Fields("COUNT").Value & "&nbsp;&nbsp;</td></tr>")
					Rs.MoveNext
				Loop
				Rs.Close
				Rs = Nothing
				Response.Write("</table>")
				
				Response.Write("<BR>")
				
				Response.Write("<table border=""1"">")
				Contracted = ""
				Billable = ""
				Dscr = ""
				ContractedCount = 0
				BillableCount = 0
				DscrCount = 0
				Rs1.MoveFirst
				Do While Not Rs1.EOF
					Contracted = Rs1.Fields("HAS_CONTRACT").Value
					ContractedCount = 0
					Response.Write("<tr><td nowrap class=""ReportRowGroup1"">" & Rs1.Fields("HAS_CONTRACT").Value & "</td></tr>" )
					Do While Not Rs1.EOF
						If Contracted <> Rs1.Fields("HAS_CONTRACT").Value Then
							Exit Do
						End If
						Billable = Rs1.Fields("BILLABLE").Value
						BillableCount = 0
						Response.Write("<tr><td nowrap class=""ReportRowGroup1"">&nbsp;&nbsp;&nbsp;&nbsp;" & Rs1.Fields("BILLABLE").Value & "</td>" )
						Do While Not Rs1.EOF
							If Billable <> Rs1.Fields("BILLABLE").Value Or _
									Contracted <> Rs1.Fields("HAS_CONTRACT").Value Then
								Exit Do
							Else
								ContractedCount = CInt(ContractedCount) + CInt(Rs1.Fields("COUNT").Value)
								BillableCount = CInt(BillableCount) + CInt(Rs1.Fields("COUNT").Value)							
							End If
							Rs1.MoveNext
						Loop
						Response.Write("<td class=""ReportRowTotal"">" & BillableCount & "&nbsp;&nbsp;</td></tr>")
					Loop
					Response.Write("<tr><td class=""ReportRowTotal"">" & Contracted & " Total: " & ContractedCount & "&nbsp;&nbsp;</td>")
				Loop
				Rs1.Close
				Rs1 = Nothing
				Response.Write("</table>")
				
				Response.Write("<BR>")
				
				Response.Write("<table border=""1"">")
				Contracted = ""
				Billable = ""
				Dscr = ""
				ContractedCount = 0
				BillableCount = 0
				DscrCount = 0
				Rs2.MoveFirst
				Do While Not Rs2.EOF
					Contracted = Rs2.Fields("HAS_CONTRACT").Value
					hasContract = Rs2.Fields("HAS_CONTRACT").Value
					Response.Write("<tr><td nowrap class=""ReportRowGroup1"">" & Rs2.Fields("HAS_CONTRACT").Value & "</td></tr>" )
					Do While Not Rs2.EOF
						If Contracted <> Rs2.Fields("HAS_CONTRACT").Value Then
							Exit Do
						End If
						Billable = Rs2.Fields("BILLABLE").Value
						actionPlan = Rs2.Fields("BILLABLE").Value
						Response.Write("<tr><td nowrap class=""ReportRowGroup1"">&nbsp;&nbsp;&nbsp;&nbsp;" & Rs2.Fields("BILLABLE").Value & "</td></tr>" )
						Do While Not Rs2.EOF
							If Billable <> Rs2.Fields("BILLABLE").Value Or _
									Contracted <> Rs2.Fields("HAS_CONTRACT").Value Then
								Exit Do
							End If
							Dscr = Rs2.Fields("DSCR").Value
							outcomeCode = Rs2.Fields("REFERRAL_RESULT_TYPE_CODE").Value
							Response.Write("<tr><td nowrap class=""ReportRowGroup1"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & Rs2.Fields("DSCR").Value & "</td>" )
							Do While Not Rs2.EOF
								If Dscr <> Rs2.Fields("DSCR").Value Or _
										Billable <> Rs2.Fields("BILLABLE").Value Or _
										Contracted <> Rs2.Fields("HAS_CONTRACT").Value Then
									Exit Do
								Else
									ContractedCount = CInt(ContractedCount) + CInt(Rs2.Fields("COUNT").Value)
									BillableCount = CInt(BillableCount) + CInt(Rs2.Fields("COUNT").Value)
									DscrCount = CInt(DscrCount) + CInt(Rs2.Fields("COUNT").Value)
								End If
								Rs2.MoveNext
							Loop
							Response.Write("<td class=""ReportRowTotal"">" & DscrCount & "&nbsp;&nbsp;</td>")							
							Response.Write("<td><A HREF='../referrals/referral_list.aspx?outcomeCode=" & outcomeCode & "&programIds=" & programIds & "&agencyIds=" & agencyIds & "&servicerIds=" & servicerIds & "&locationIds=" & locationIds & "&acctPrd1=" & acctPrd1 & "&acctPrd2=" & acctPrd2 & "&startDate=" & startDate & "&endDate=" & endDate & "&orderBy=" & orderBy & "&reportFormat=" & reportFormat & "&reportType=" & reportType & "&hasContract=" & hasContract & "&actionPlan=" & actionPlan & "'>[View]</A></td></tr>")
							DscrCount = 0
							outcomeCode = ""
						Loop
						Response.Write("<tr><td class=""ReportRowTotal"">" & Billable & " Total: " & BillableCount & "&nbsp;&nbsp;</td>")
						Response.Write("<td><A HREF='../referrals/referral_list.aspx?outcomeCode=" & outcomeCode & "&programIds=" & programIds & "&agencyIds=" & agencyIds & "&servicerIds=" & servicerIds & "&locationIds=" & locationIds & "&acctPrd1=" & acctPrd1 & "&acctPrd2=" & acctPrd2 & "&startDate=" & startDate & "&endDate=" & endDate & "&orderBy=" & orderBy & "&reportFormat=" & reportFormat & "&reportType=" & reportType & "&hasContract=" & hasContract & "&actionPlan=" & actionPlan & "'>[View]</A></td></tr>")
						BillableCount = 0
						actionPlan = ""
					Loop
					Response.Write("<tr><td class=""ReportRowTotal"">" & Contracted & " Total: " & ContractedCount & "&nbsp;&nbsp;</td>")
					Response.Write("<td><A HREF='../referrals/referral_list.aspx?outcomeCode=" & outcomeCode & "&programIds=" & programIds & "&agencyIds=" & agencyIds & "&servicerIds=" & servicerIds & "&locationIds=" & locationIds & "&acctPrd1=" & acctPrd1 & "&acctPrd2=" & acctPrd2 & "&startDate=" & startDate & "&endDate=" & endDate & "&orderBy=" & orderBy & "&reportFormat=" & reportFormat & "&reportType=" & reportType & "&hasContract=" & hasContract & "&actionPlan=" & actionPlan & "'>[View]</A></td></tr>")
					Response.Write(err.Description)
					ContractedCount = 0
					hasContract = ""
				Loop
				Rs2.Close
				Rs2 = Nothing
				Response.Write("</table>")
									
				Dim sortOrder									
				Dim grpLevel1Id, grpLevel2Id, grpLevel3Id, _
					grpLevel4Id, grpLevel5Id, grpLevel6Id
				Dim grpLevel1Field, grpLevel2Field, grpLevel3Field, _
					grpLevel4Field, grpLevel5Field, grpLevel6Field
				Dim grpLevel1Kount, grpLevel2Kount, grpLevel3Kount, _
					grpLevel4Kount, grpLevel5Kount, grpLevel6Kount

				If orderBy = "PROGRAM" Or _
						InStr(1,Request.Cookies("CCRCCookie")("profileRoles"),"84") > 0 Then
					grpLevel1Field = "PROGRAM_NAME"
					grpLevel2Field = "AGENCY"
					grpLevel3Field = "HAS_CONTRACT_IND"
					grpLevel4Field = "SERVICER"
					grpLevel5Field = "LOCTN_NAME"
					grpLevel6Field = "REFERRAL_RESULT"
					sortOrder = "PROGRAM_NAME, AGENCY, HAS_CONTRACT_IND, SERVICER, LOCTN_NAME, REFERRAL_RESULT"
				ElseIf orderBy = "SERVICER" Or _
						InStr(1,Request.Cookies("CCRCCookie")("profileRoles"),"34") > 0 Then
					grpLevel1Field = "TOTAL"
					grpLevel2Field = "HAS_CONTRACT_IND"
					grpLevel3Field = "SERVICER"
					grpLevel4Field = "LOCTN_NAME"
					grpLevel5Field = "AGENCY"
					grpLevel6Field = "REFERRAL_RESULT"
					sortOrder = "HAS_CONTRACT_IND, SERVICER, LOCTN_NAME, AGENCY, REFERRAL_RESULT"
				ElseIf orderBy = "AGENCY" Or _
						InStr(1,Request.Cookies("CCRCCookie")("profileRoles"),"85") > 0 Then
					grpLevel1Field = "TOTAL"
					grpLevel2Field = "AGENCY"
					grpLevel3Field = "HAS_CONTRACT_IND"
					grpLevel4Field = "SERVICER"
					grpLevel5Field = "LOCTN_NAME"
					grpLevel6Field = "REFERRAL_RESULT"
					sortOrder = "AGENCY, HAS_CONTRACT_IND, SERVICER, LOCTN_NAME, REFERRAL_RESULT"
				End If
				Rs = obj.Get_Referral_List(Request("program_ids"), Request("agency_ds"), _
					Request("servicer_ids"), Request("location_ids"), acctPrd1, acctPrd2, startDate, _
					endDate, reportType, "", "", "", _
					Request.Cookies("CCRCCookie")("profileSeqId"), _
					sUserId, sSQL, iReturnCode, sMessage)
				obj = Nothing

				Rs.Sort = sortOrder	
				If isValidRs(Rs) Then
'Dim fld
'for each fld in rs.fields
'Response.Write(fld.name & " - " & fld.value & "<BR>"
'next
'response.End
					Response.Write("<table>")
					Rs.MoveFirst
					Do While Not Rs.EOF
						If grpLevel1Field = "TOTAL" Then
							grpLevel1Id = "TOTAL"
						Else
							grpLevel1Id = Rs.Fields(grpLevel1Field).Value
						End If
						Response.Write("<tr><td nowrap class=""ReportRowGroup1"">" & grpLevel1Id & "</td></tr>")
						Do While Not Rs.EOF
							If grpLevel1Field <> "TOTAL" Then
								If grpLevel1Id <> Rs.Fields(grpLevel1Field).Value Then
									Exit Do
								End If
							End If
							grpLevel2Id = Rs.Fields(grpLevel2Field).Value
							Response.Write("<tr><td nowrap class=""ReportRowGroup2"">&nbsp;&nbsp;" & grpLevel2Id & "</td></tr>")
							Do While Not Rs.EOF
								If grpLevel1Field <> "TOTAL" Then
									If grpLevel2Id <> Rs.Fields(grpLevel2Field).Value Or _
											grpLevel1Id <> Rs.Fields(grpLevel1Field).Value Then
										Exit Do
									End If
								Else
								If grpLevel2Id <> Rs.Fields(grpLevel2Field).Value Then
										Exit Do
									End If
								End If
								grpLevel3Id = Rs.Fields(grpLevel3Field).Value
								Response.Write("<tr><td nowrap class=""ReportRowGroup2"">&nbsp;&nbsp;&nbsp;&nbsp;" & grpLevel3Id & "</td></tr>")
								Do While Not Rs.EOF
									If grpLevel1Field <> "TOTAL" Then
										If grpLevel3Id <> Rs.Fields(grpLevel3Field).Value Or _
												grpLevel2Id <> Rs.Fields(grpLevel2Field).Value Or _
												grpLevel1Id <> Rs.Fields(grpLevel1Field).Value Then
											Exit Do
										End If
									Else
										If grpLevel3Id <> Rs.Fields(grpLevel3Field).Value Or _
												grpLevel2Id <> Rs.Fields(grpLevel2Field).Value Then
											Exit Do
										End If
									End If
									grpLevel4Id = Rs.Fields(grpLevel4Field).Value
									Response.Write("<tr><td nowrap class=""ReportRowGroup2"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & grpLevel4Id & "</td></tr>")
									Do While Not Rs.EOF
										If grpLevel1Field <> "TOTAL" Then									
											If grpLevel4Id <> Rs.Fields(grpLevel4Field).Value Or _
													grpLevel3Id <> Rs.Fields(grpLevel3Field).Value Or _
													grpLevel2Id <> Rs.Fields(grpLevel2Field).Value Or _
													grpLevel1Id <> Rs.Fields(grpLevel1Field).Value Then
												Exit Do
											End If
										Else
											If grpLevel4Id <> Rs.Fields(grpLevel4Field).Value Or _
													grpLevel3Id <> Rs.Fields(grpLevel3Field).Value Or _
													grpLevel2Id <> Rs.Fields(grpLevel2Field).Value Then
												Exit Do
											End If
										End If									
										grpLevel5Id = Rs.Fields(grpLevel5Field).Value
										Response.Write("<tr><td nowrap class=""ReportRowGroup2"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & grpLevel5Id & "</td></tr>")
										Do While Not Rs.EOF
											If grpLevel1Field <> "TOTAL" Then									
												If grpLevel5Id <> Rs.Fields(grpLevel5Field).Value Or _
														grpLevel4Id <> Rs.Fields(grpLevel4Field).Value Or _
														grpLevel3Id <> Rs.Fields(grpLevel3Field).Value Or _
														grpLevel2Id <> Rs.Fields(grpLevel2Field).Value Or _
														grpLevel1Id <> Rs.Fields(grpLevel1Field).Value Then
													Exit Do
												End If
											Else
												If grpLevel5Id <> Rs.Fields(grpLevel5Field).Value Or _
														grpLevel4Id <> Rs.Fields(grpLevel4Field).Value Or _
														grpLevel3Id <> Rs.Fields(grpLevel3Field).Value Or _
														grpLevel2Id <> Rs.Fields(grpLevel2Field).Value Then
													Exit Do
												End If
											End If
											grpLevel6Id = Rs.Fields(grpLevel6Field).Value
											Do While Not Rs.EOF
												If grpLevel1Field <> "TOTAL" Then																				
													If grpLevel6Id = Rs.Fields(grpLevel6Field).Value And _
															grpLevel5Id = Rs.Fields(grpLevel5Field).Value And _
															grpLevel4Id = Rs.Fields(grpLevel4Field).Value And _
															grpLevel3Id = Rs.Fields(grpLevel3Field).Value And _
															grpLevel2Id = Rs.Fields(grpLevel2Field).Value And _
															grpLevel1Id = Rs.Fields(grpLevel1Field).Value Then
														grpLevel1Kount = grpLevel1Kount + 1
														grpLevel2Kount = grpLevel2Kount + 1
														grpLevel3Kount = grpLevel3Kount + 1
														grpLevel4Kount = grpLevel4Kount + 1
														grpLevel5Kount = grpLevel5Kount + 1
														grpLevel6Kount = grpLevel6Kount + 1
														Rs.MoveNext														
													Else
														Exit Do
													End If
												Else
													If grpLevel6Id = Rs.Fields(grpLevel6Field).Value And _
															grpLevel5Id = Rs.Fields(grpLevel5Field).Value And _
															grpLevel4Id = Rs.Fields(grpLevel4Field).Value And _
															grpLevel3Id = Rs.Fields(grpLevel3Field).Value And _
															grpLevel2Id = Rs.Fields(grpLevel2Field).Value Then
														grpLevel1Kount = grpLevel1Kount + 1
														grpLevel2Kount = grpLevel2Kount + 1
														grpLevel3Kount = grpLevel3Kount + 1
														grpLevel4Kount = grpLevel4Kount + 1
														grpLevel5Kount = grpLevel5Kount + 1
														grpLevel6Kount = grpLevel6Kount + 1
														Rs.MoveNext														
													Else
														Exit Do
													End If
												End If
											Loop
											Response.Write("<tr><td class=""ReportRow_LeftA"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & grpLevel6Id & "</td>")
											Response.Write("<td class=""ReportRow_RightA"">" & grpLevel6Kount & "</td></tr>")
											grpLevel6Kount = 0
										Loop
										Response.Write("<tr><td class=""ReportRow_RightA"">Total for " & grpLevel5Id & "</td><td class=""ReportRowTotal"">" & grpLevel5Kount & "</td></tr>")
										grpLevel5Kount = 0
									Loop
									Response.Write("<tr><td class=""ReportRow_RightA"">Total for " & grpLevel4Id & "</td><td class=""ReportRowTotal"">" & grpLevel4Kount & "</td></tr>")
									grpLevel4Kount = 0
								Loop
								Response.Write("<tr><td class=""ReportRow_RightA"">Total for " & grpLevel3Id & "</td><td class=""ReportRowTotal"">" & grpLevel3Kount & "</td></tr>")
								grpLevel3Kount = 0
							Loop
							Response.Write("<tr><td class=""ReportRow_RightA"">Total for " & grpLevel2Id & "</td><td class=""ReportRowTotal"">" & grpLevel2Kount & "</td></tr>")
							grpLevel2Kount = 0
						Loop
						Response.Write("<tr><td class=""ReportRow_RightA"">Total for " & grpLevel1Id & "</td><td class=""ReportRowTotal"">" & grpLevel1Kount & "</td></tr>")
						grpLevel1Kount = 0
					Loop
					Response.Write("</table>")
					Rs.Close
					Rs = Nothing
				End If
			End If								
			Response.Write("</form>")
		%>
	</body>
</html>
<%




 
%>
