<%@ Page Language="VB" AutoEventWireup="false"  aspcompat=true %>
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<%
    'On Error Resume Next
    Response.Clear()
    Dim obj, RC, agency, counselor, referral, listPriceAmt

    Dim sDomainUser, sDomain, sUserId, sSQL, iReturnCode, sMessage
    Call GetAuthorizedUser(sDomainUser, sDomain, sUserId)

    obj = CreateObject("CCRC_Main.CMain")
    'If RedirectIfError("Creating CCRC_Main.CMain", eRetPage, eRetBtnText) Then
    'obj = Nothing
    'Response.Clear()
    'Response.End()
    'End If

    If Len(Request("counselorId")) > 0 Then
        agency = Request("agencyId")
        counselor = Request("counselorId")
    Else
        agency = Request.Cookies("CCRCCookie")("entitySeqId")
        counselor = Request.Cookies("CCRCCookie")("profileSeqId")
    End If

    If Len(Request("listPriceAmt")) = 0 Then
        listPriceAmt = "null"
    Else
        listPriceAmt = Request("listPriceAmt")
    End If

    'Response.Write(Request("FormMode"))
    'Response.End()
    Err.Clear()
    Select Case UCase(Request("FormMode"))
        Case "ADD"
            referral = obj.Referral_Add( _
             Request("loanId"), _
             Request("program"), _
             Request("servicer"), _
             Request("theLocation"), _
             Request("newServicer"), _
             Request("newLocation"), _
             agency, _
             counselor, _
             Request("agencyTrackingId"), _
             Request("lastName"), _
             Request("firstName"), _
             Request("miName"), _
             Request("addrLine1"), _
             Request("city"), _
             Request("state"), _
             Request("zipCode"), _
             Request("referralType"), _
             Request("referralDate"), _
             Request("referralComment"), _
             Request("approvedBy"), _
             Request("hiddenSessionRating"), _
             Request("sessionType"), _
             Request("referralResultType"), _
             Request("referralResultDate"), _
             "Y", _
             IIf(Request("internal"), "Y", "N"), _
             Request("summaryRptSentDate"), _
             Request("phn"), _
             Request("occupants"), _
             Request("loanDefaultRsn"), _
             Request("actionItems"), _
             Request("followupNotes"), _
             Request("successStoryInd"), _
             Request("firstContactStatusType"), _
             Request("contactedServicerInd"), _
             Request("referralSourceTypeCode"), _
             Request("counselingDurationType"), _
             Request("mthlyNetIncomeType"), _
             Request("mthlyExpenseType"), _
             Request("firstMortgageType"), _
             Request("creditScore"), _
             Request("mothersMaidenName"), _
             Request("privacyConsentInd"), _
             Request("priorityTypeCode"), _
             Request("secondaryContactNumber"), _
             Request("emailAddr"), _
             Request("bankruptcyInd"), _
             Request("bankruptcyAttorneyName"), _
             Request("ownerOccupiedInd"), _
             Request("primaryDfltRsnType"), _
             Request("secondaryDfltRsnType"), _
             Request("hispanicInd"), _
             Request("raceType"), _
             Request("dupeInd"), _
             Request("discussedSolutionWithSrvcr"), _
             sUserId, sSQL, iReturnCode, sMessage)
            If RedirectIfError("Calling CCRC_Main.CMain.Referral_Add", eRetPage, eRetBtnText) Then
                obj = Nothing
                Response.Clear()
                Response.End()
            End If

            
            Dim secondPitiAmt
            secondPitiAmt = Request("pitiAmt2")
            If secondPitiAmt.Length = 0 Then
                secondPitiAmt = 0
            End If
            
            Dim secondservicer2
            secondservicer2 = Request("servicer2")
            If secondservicer2.Length = 0 Then
                secondservicer2 = -1
            End If
            
            RC = obj.Referral_Update2( _
             referral, _
             Request("age"), _
             Request("genderType"), _
             Request("householdType"), _
             Request("householdIncome"), _
             Request("incomeCategoryType"), _
             Request("intakeScoreType"), _
             Request("pitiAmt"), _
             Request("armResetInd"), _
             Request("propertyForSaleInd"), _
             listPriceAmt, _
             Request("realtyCompanyName"), _
             Request("fcNoticeRecdInd"), _
             Request("incomeEarnersType"), _
             Request("summarySentOtherType"), _
             secondservicer2, _
             Request("secondMortgageType"), _
             Request("varloanId2"), _
             secondPitiAmt, _
             Request("secondLoanStatusTypeCode"), _
             Request("summarySentOtherDate"), _
             Request("hUDOutcome"), _
             Request("hUDTermiReason"), _
             Request("referraltermdate"), _
             Request("workedWithAnotherAgencyInd"), _
             sUserId, sSQL, iReturnCode, sMessage)
            If RedirectIfError("Calling CCRC_Main.CMain.Referral_Add2", eRetPage, eRetBtnText) Then
                obj = Nothing
                Response.Clear()
                Response.End()
            End If
		
            obj = Nothing
		
            If Request("referralResultType") <> "9" And Request("referralResultType") = "10" or Request("referralResultType") = "11" Then
                Response.Redirect("NEWcounselingSummary.aspx?referralId=" & referral & "&zipCode=" & Request("zipCode"))
            Else
                Response.Redirect("referral.aspx?FormMode=EDIT&referralId=" & referral & "&zipCode=" & Request("zipCode"))
            End If

        Case "EDIT"
            RC = obj.Referral_Update( _
             Request("referralId"), _
             Request("loanId"), _
             Request("program"), _
             Request("servicer"), _
             Request("theLocation"), _
             Request("newServicer"), _
             Request("newLocation"), _
             agency, _
             counselor, _
             Request("agencyTrackingId"), _
             Request("lastName"), _
             Request("firstName"), _
             Request("miName"), _
             Request("addrLine1"), _
             Request("city"), _
             Request("state"), _
             Request("zipCode"), _
             Request("referralType"), _
             Request("referralDate"), _
             Request("referralComment"), _
             Request("approvedBy"), _
             Request("hiddenSessionRating"), _
             Request("sessionType"), _
             Request("referralResultType"), _
             Request("referralResultDate"), _
             Request("summaryRptSentDate"), _
             Request("phn"), _
             Request("occupants"), _
             Request("loanDefaultRsn"), _
             Request("actionItems"), _
             Request("followupNotes"), _
             Request("successStoryInd"), _
             Request("firstContactStatusType"), _
             Request("contactedServicerInd"), _
             Request("referralSourceTypeCode"), _
             Request("counselingDurationType"), _
             Request("mthlyNetIncomeType"), _
             Request("mthlyExpenseType"), _
             Request("firstMortgageType"), _
             Request("creditScore"), _
             Request("mothersMaidenName"), _
             Request("privacyConsentInd"), _
             Request("priorityTypeCode"), _
             Request("secondaryContactNumber"), _
             Request("emailAddr"), _
             Request("bankruptcyInd"), _
             Request("bankruptcyAttorneyName"), _
             Request("ownerOccupiedInd"), _
             Request("primaryDfltRsnType"), _
             Request("secondaryDfltRsnType"), _
             Request("hispanicInd"), _
             Request("raceType"), _
             Request("dupeInd"), _
             Request("discussedSolutionWithSrvcr"), _
             sUserId, sSQL, iReturnCode, sMessage)
            If RedirectIfError("Calling CCRC_Main.CMain.Referral_Update", eRetPage, eRetBtnText) Then
                obj = Nothing
                Response.Clear()
                Response.End()
            End If

            Dim secondPitiAmt
            secondPitiAmt = Request("pitiAmt2")
            If secondPitiAmt.Length = 0 Then
                secondPitiAmt = 0
            End If
            
            Dim secondservicer2
            secondservicer2 = Request("servicer2")
            If secondservicer2.Length = 0 Then
                secondservicer2 = -1
            End If

            
            
            Err.Clear()
            RC = obj.Referral_Update2( _
             Request("referralId"), _
             Request("age"), _
             Request("genderType"), _
             Request("householdType"), _
             Request("householdIncome"), _
             Request("incomeCategoryType"), _
             Request("intakeScoreType"), _
             Request("pitiAmt"), _
             Request("armResetInd"), _
             Request("propertyForSaleInd"), _
             listPriceAmt, _
             Request("realtyCompanyName"), _
             Request("fcNoticeRecdInd"), _
             Request("incomeEarnersType"), _
             Request("summarySentOtherType"), _
             secondservicer2, _
             Request("secondMortgageType"), _
             Request("varloanId2"), _
             secondPitiAmt, _
             Request("secondLoanStatusTypeCode"), _
             Request("summarySentOtherDate"), _
            Request("hUDOutcome"), _
            Request("hUDTermiReason"), _
            Request("referraltermdate"), _
            Request("workedWithAnotherAgencyInd"), _
             sUserId, sSQL, iReturnCode, sMessage)
            If RedirectIfError("Calling CCRC_Main.CMain.Referral_Update2", eRetPage, eRetBtnText) Then
                obj = Nothing
                Response.Clear()
                Response.End()
            End If

            obj = Nothing
		
            If Request("referralResultType") <> "9" And Request("referralResultType") = "10" And Request("referralResultType") = "11" Then
                Response.Redirect("NewCounselingSummary.aspx?referralId=" & RC & "&zipCode=" & Request("zipCode"))
            Else
                Response.Redirect("referral.aspx?FormMode=EDIT&referralId=" & Request("referralId") & "&zipCode=" & Request("zipCode"))
            End If
	
        Case "DELETE"
            RC = obj.Referral_Delete(Request("referralId"), sUserId, sSQL, iReturnCode, sMessage)
            If RedirectIfError("Calling CCRC_Main.CMain.Referral_Delete", eRetPage, eRetBtnText) Then
                obj = Nothing
                Response.Clear()
                Response.End()
            End If
            obj = Nothing
            Response.Redirect("search.aspx")
	
        Case Else
            Response.Write("Request context lost.  Check and reenter data if necessary")
            Response.End()
    End Select

%>
<html></html>
