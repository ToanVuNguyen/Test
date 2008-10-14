Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Data.SqlClient
Imports System.Text
Imports CCRCEncryption



Partial Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub cmdLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLogin.Click
        Dim uName, pwd As String
        uName = txtUser.Text.Trim()
        
         uName = uName.Replace("irfc\", "")
        uName = uName.Replace("IRFC\", "")
        uName = uName.Replace("irfc/", "")
        uName = uName.Replace("IRFC/", "")
        uName = uName.Replace("rfc\", "")
        uName = uName.Replace("RFC\", "")
        uName = uName.Replace("rfc/", "")
        uName = uName.Replace("RFC/", "")



        Dim LoginBuisness As LoginBuisness = New LoginBuisness()
        Dim dr As SqlDataReader = LoginBuisness.GetHashedPwd(uName)
        Dim strHashedDbPwd As String = ""

        Dim profileSeqId As String = ""
        Dim profileRoles As String = ""
        Dim entitySeqId As String = ""
        Dim entityTypeCode As String = ""

        Dim entityIsActive As String = ""
        Dim userIsActive As String = ""

        While dr.Read()

            entityIsActive = dr("BUSINESS_ENTITY_ACTIVE_IND").ToString()
            userIsActive = dr("USER_ACTIVE_IND").ToString()
            profileSeqId = dr("CCRC_USER_SEQ_ID").ToString()
            profileRoles = dr("USER_ROLE_STRING").ToString()
            entitySeqId = dr("BSN_ENTITY_SEQ_ID").ToString()
            entityTypeCode = dr("BSN_ENTITY_TYPE_CODE").ToString()
            strHashedDbPwd = dr("USER_PASSWORD").ToString()
        End While
        If strHashedDbPwd.Length > 0 Then
            ' Verify that hashed user-entered password is the same
            ' as the hashed password from the database
            If SaltedHash.ValidatePassword(txtPassword.Text, strHashedDbPwd) Then

                DoOnAuthorisedUser(profileSeqId, profileRoles, entitySeqId, entityTypeCode, uName, entityIsActive, userIsActive)

                FormsAuthentication.RedirectFromLoginPage(txtUser.Text, False)

            Else

                lblMsg.Text = "Login attempt failed."

            End If
        Else

            lblMsg.Text = "Login attempt failed."
        End If

    End Sub


    Private Sub DoOnAuthorisedUser(ByVal profileSeqId As String, ByVal profileRoles As String, ByVal entitySeqId As String, ByVal entityTypeCode As String, ByVal sUserId As String, ByVal entityIsActive As String, ByVal userIsActive As String)

        'if(Response.Cookies("CCRCCookie")("profileSeqId") + "" == "" )
        '{
        If entityIsActive <> "Y" Then

            Response.Redirect("splash.aspx?exitCode=4")
            Response.End()
        End If


        If userIsActive <> "Y" Then

            Response.Redirect("splash.aspx?exitCode=3")
            Response.End()
        End If

        'user is valid so save session variables

        Dim login As LoginDAL = New LoginDAL()
        'Save the value when user last login
        Dim userSeqID As Integer = 0
        Int32.TryParse(profileSeqId, userSeqID)
        Login.UpdateLastLogin(userSeqID)

        Response.Cookies("CCRCCookie").Path = "/"

        Response.Cookies("CCRCCookie")("profileRoles") = profileRoles
        Response.Cookies("CCRCCookie")("profileSeqId") = profileSeqId
        'Response.Cookies("CCRCCookie")("profileRoles") = "0" 
        Response.Cookies("CCRCCookie")("entitySeqId") = entitySeqId
        Response.Cookies("CCRCCookie")("entityTypeCode") = entityTypeCode
        Response.Cookies("CCRCCookie")("CurrentUser") = sUserId
        Response.Cookies("CCRCCookie").Expires = DateTime.Now.AddDays(1)

        'Request.Cookies("CCRCCookie").Path = "/"
        'Request.Cookies("CCRCCookie")("profileSeqId") = profileSeqId
        'Request.Cookies("CCRCCookie")("profileRoles") = profileRoles
        ''Request.Cookies("CCRCCookie")("profileRoles") = "0" 
        'Request.Cookies("CCRCCookie")("entitySeqId") = entitySeqId
        'Request.Cookies("CCRCCookie")("entityTypeCode") = entityTypeCode
        'Request.Cookies("CCRCCookie")("CurrentUser") = sUserId
        'Request.Cookies("CCRCCookie").Expires = DateTime.Now.AddDays(1)
        '  }
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

     

        ' Response.Write(conStr)
    End Sub
End Class



