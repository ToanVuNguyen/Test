Imports Microsoft.VisualBasic

Public Class Utilities
    Public Shared Function isNullOrEmpty(ByVal test)
        If IsDBNull(test) Then
            isNullOrEmpty = True
        ElseIf test Is Nothing Then
            isNullOrEmpty = True
        ElseIf test = "" Then
            isNullOrEmpty = True
        Else
            isNullOrEmpty = False
        End If
    End Function

    Public Shared Function buildName(ByVal firstName, ByVal middleInitial, ByVal lastName, ByVal suffix)
        On Error Resume Next
        Dim sRet
        sRet = lastName
        If Not isNullOrEmpty(suffix) Then
            sRet = sRet & " " & getNameSuffixDscr(suffix)
        End If
        sRet = sRet & ", " & firstName
        If Not isNullOrEmpty(middleInitial) Then
            sRet = sRet & " " & middleInitial & "."
        End If

        buildName = sRet
    End Function

    Public Shared Function getNameSuffixDscr(ByVal suffixCode)
        Select Case (suffixCode)
            Case "JR"
                getNameSuffixDscr = "Jr."
            Case "SR"
                getNameSuffixDscr = "Sr."
            Case "II", "III", "IV", "V"
                getNameSuffixDscr = suffixCode
            Case Else
                getNameSuffixDscr = suffixCode
        End Select
    End Function


    Public Shared Sub PrintDetailRow(ByVal irow, ByVal budgetCategoryType, ByVal category, ByVal budgetSubcategorySeqId, ByVal budgetSubcategory, ByVal amt, ByRef total, ByRef eTotal, ByRef yTotal)
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
            HttpContext.Current.Response.Write("<td>" & budgetSubcategory & "</td><td>" & amt & "</td></tr>")
        Else
            HttpContext.Current.Response.Write("<tr><td></td><td>" & budgetSubcategory & "</td><td>" & amt & "</td></tr>")
        End If
    End Sub

    Public Shared Sub PrintSubtotalRow(ByVal subtotal)
        HttpContext.Current.Response.Write("<tr><td></td><td><b>Subtotal:</td><td><b>" & subtotal & "</b></td></tr>")
    End Sub

    Public Shared Sub PrintTotalRow(ByVal total, ByVal eTotal, ByVal yTotal)
        HttpContext.Current.Response.Write("<tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>")
        HttpContext.Current.Response.Write("<tr><td></td><td><b>Income Total:</td><td><b>" & yTotal & "</b></td></tr>")
        HttpContext.Current.Response.Write("<tr><td></td><td><b>Expense Total:</td><td><b>" & eTotal & "</b></td></tr>")
        HttpContext.Current.Response.Write("<tr><td></td><td><b>Total Surplus or Deficit:</td><td><b>" & total & "</b></td></tr>")
    End Sub


End Class
