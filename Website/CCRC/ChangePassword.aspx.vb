Imports CCRCEncryption
Imports System.Data.SqlClient

Partial Class ChangePassword
    Inherits System.Web.UI.Page

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Response.Redirect("Home.aspx")
    End Sub

    Protected Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Dim uName, newPwd, oldPassword As String
        uName = Request.Cookies("CCRCCookie")("CurrentUser")
        oldPassword = txtOldPassword.Text.Trim()

        newPwd = txtPassword.Text.Trim()
        Dim loginBuisness As LoginBuisness = New LoginBuisness()
        Dim dr As SqlDataReader = LoginBuisness.GetHashedPwd(uName)
        Dim strHashedDbPwd As String = ""


        While dr.Read()
            strHashedDbPwd = dr("USER_PASSWORD").ToString()
        End While
        If strHashedDbPwd.Length > 0 Then
            ' Verify that hashed user-entered password is the same
            ' as the hashed password from the database
            If SaltedHash.ValidatePassword(oldPassword, strHashedDbPwd) Then
                loginBuisness.ChangePassword(uName, newPwd, uName)
                lblMsg.Text = "Password changed successfully"
            Else

                lblMsg.Text = "old password invalid ."

            End If
        Else

            lblMsg.Text = "Login attempt failed."
        End If
    End Sub
End Class
