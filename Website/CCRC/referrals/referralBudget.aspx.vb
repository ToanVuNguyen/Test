
Partial Class referrals_referral
    Inherits System.Web.UI.Page
    Protected eRetPage
    Protected eRetBtnText

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub
    Public Function isValidRS(ByVal rsIn)
        isValidRS = False
        'If IsObject(rsIn) Then
        If Not (rsIn Is Nothing) Then
            If Not (rsIn.EOF And rsIn.BOF) Then
                isValidRS = True
            End If
        End If
        'End If
    End Function

    Protected Sub PrintDetailRow(ByVal irow, ByVal budgetCategoryType, ByVal category, ByVal budgetSubcategorySeqId, ByVal budgetSubcategory, ByVal amt, ByRef total, ByRef eTotal, ByRef yTotal)
        Dim sName, sObj
        '	sName = "txt" & irow & category & categoryType
        Dim Amount As Integer
        Amount = amt
        sName = "txt" & budgetSubcategorySeqId
        sObj = "<input type='text' id='" & sName & "' name='" & sName & "' value='" & Amount & "'>"

        If amt Is Nothing Then
            amt = 0
        End If
        If CInt(budgetCategoryType) = 1 Then
            total = CDbl(total) + CDbl(amt)
            yTotal = CDbl(yTotal) + CDbl(amt)
        Else
            total = CDbl(total) - CDbl(amt)
            eTotal = CDbl(eTotal) + CDbl(amt)
        End If
        If CInt(irow) = 1 Then
            Response.Write("<td>" & budgetSubcategory & "</td><td>" & sObj & "</td></tr>")
        Else
            Response.Write("<tr><td></td><td>" & budgetSubcategory & "</td><td>" & sObj & "</td></tr>")
        End If
    End Sub

    Protected Sub PrintSubtotalRow(ByVal subtotal)
        Response.Write("<tr><td></td><td nowrap class=""ReportRowGroup1"">Subtotal:</td><td nowrap class=""ReportRowGroup1"">" & subtotal & "</td></tr>")
    End Sub

    Protected Sub PrintTotalRow(ByVal total, ByVal eTotal, ByVal yTotal)
        Response.Write("<tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>")
        Response.Write("<tr><td></td><td nowrap class=""ReportRowGroup1"">Income Total:</td><td nowrap class=""ReportRowGroup1"">" & yTotal & "</td></tr>")
        Response.Write("<tr><td></td><td nowrap class=""ReportRowGroup1"">Expense Total:</td><td nowrap class=""ReportRowGroup1"">" & eTotal & "</td></tr>")
        Response.Write("<tr><td></td><td nowrap class=""ReportRowGroup1"">Total Surplus or Deficit:</td><td nowrap class=""ReportRowGroup1"">" & total & "</td></tr>")
    End Sub
    Public Function RedirectIfError(ByVal currentContext, ByVal eReturnPage, ByVal eReturnBtnText)
        If Err.Number > 0 Then
            If currentContext = "" Then
                currentContext = "Building Page"
            End If

            If Right(eRetPage, 1) = "?" Then
                eRetPage = Replace(eRetPage, "?", "")
            End If
            eRetPage = Server.UrlEncode(eRetPage)

            Response.Clear()
            Response.Redirect(getBasePath() & "error.aspx?errNum=" & Err.Number & "&errDesc=" & Server.UrlEncode(Err.Description) & "&errSrc=" & Server.UrlEncode(Err.Source) & "&URL=" & Request.ServerVariables("SERVER_NAME") & Request.ServerVariables("URL") & "&currContext=" & Server.UrlEncode(currentContext) & "&ERetPage=" & eReturnPage & "&ERetBtnText=" & eReturnBtnText)
            RedirectIfError = True
        Else
            RedirectIfError = False
        End If
    End Function

    Public Function getBasePath()
        Dim sURL
        Dim sPath

        sURL = Request.ServerVariables("SERVER_NAME")
        If instr(1, sURL, "avenue", vbTextCompare) > 0 Then
            getBasePath = "/"
        Else
            sPath = Request.ServerVariables("PATH_INFO")
            getBasePath = Left(sPath, InStr(2, sPath, "/", vbTextCompare))
        End If

    End Function

End Class
