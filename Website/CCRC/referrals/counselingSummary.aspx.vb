
Partial Class referrals_counselingSummary
    Inherits System.Web.UI.Page
    Protected eRetPage
    Protected eRetBtnText

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub PrintDetailRow(ByVal irow, ByVal budgetCategoryType, ByVal category, ByVal budgetSubcategorySeqId, ByVal budgetSubcategory, ByVal amt, ByVal total, ByVal eTotal, ByVal yTotal, ByVal emailBody3)
        If CInt(budgetCategoryType) = 1 Then
            total = CDbl(total) + CDbl(amt)
            yTotal = CDbl(yTotal) + CDbl(amt)
        Else
            total = CDbl(total) - CDbl(amt)
            eTotal = CDbl(eTotal) + CDbl(amt)
        End If
        If CInt(irow) = 1 Then
            emailBody3 = emailBody3 & "<td>" & budgetSubcategory & "</td><td>" & amt & "</td></tr>"
        Else
            emailBody3 = emailBody3 & "<tr><td></td><td>" & budgetSubcategory & "</td><td>" & amt & "</td></tr>"
        End If
    End Sub

    Protected Sub PrintSubtotalRow(ByVal subtotal, ByVal emailBody3)
        emailBody3 = emailBody3 & "<tr><td></td><td><b>Subtotal:</b></td><td><b>" & subtotal & "</b></td></tr>"
    End Sub

    Protected Sub PrintTotalRow(ByVal total, ByVal eTotal, ByVal yTotal, ByVal emailBody3)
        emailBody3 = emailBody3 & "<tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>"
        emailBody3 = emailBody3 & "<tr><td></td><td><b>Income Total:</b></td><td><b>" & yTotal & "</b></td></tr>"
        emailBody3 = emailBody3 & "<tr><td></td><td><b>Expense Total:</b></td><td><b>" & eTotal & "</b></td></tr>"
        emailBody3 = emailBody3 & "<tr><td></td><td><b>Total Surplus or Deficit:</b></td><td><b>" & total & "</b></td></tr>"
    End Sub


End Class
