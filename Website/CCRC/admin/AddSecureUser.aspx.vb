Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Data.SqlClient
Imports System.Web.Security
Imports CCRCEncryption
Imports System.Configuration


Partial Public Class AddSecureUser
    Inherits System.Web.UI.Page

    Protected Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Dim doimain As String = System.Configuration.ConfigurationManager.AppSettings("DEFUALT_DOMAIN")
        Dim CreatedUser As String = Request.Cookies("CCRCCookie")("CurrentUser")
        Dim sProgname As String = System.Configuration.ConfigurationManager.AppSettings("PRAGRAM_NAME")
        Dim Login As LoginBuisness = New LoginBuisness()
        Try

            Dim BSN_ENTITY_SEQ_ID As Integer = 0

            'Int32.TryParse(ENTITY.SelectedValue, BSN_ENTITY_SEQ_ID)
            BSN_ENTITY_SEQ_ID = Request.QueryString("entityId")

            Dim locationID As Integer = 0
            'Int32.TryParse(ddENTITYLocation.SelectedValue, locationID)
            Dim sActiveInd As String = rdoActive.SelectedValue

            Dim lastName As String = txtLastName.Text
            Dim FirstName As String = txtFirstName.Text

            Dim title As String = txtTitle.Text
            Dim email As String = txtEmail.Text
            Dim priPhone As String = txtPhone.Text
            Dim uName As String

            Dim agencyUserId As String = txtAgencyUserID.Text

            Dim iBSN_ENTITY_LOCTN_SEQ_ID As Integer
            Int32.TryParse(hidBSN_ENTITY_LOCTN_SEQ_ID.Value, iBSN_ENTITY_LOCTN_SEQ_ID)

            Dim iPROFILE_SEQ_ID As Integer
            Int32.TryParse(hidPROFILE_SEQ_ID.Value, iPROFILE_SEQ_ID)
            Dim accessString As String = ""
            If Not Request.QueryString("ntUserId") Is Nothing Then
                uName = Request.QueryString("ntUserId")
                If uName.Trim.Length > 0 Then
                    locationID = Login.UpdateSecureUser(BSN_ENTITY_SEQ_ID, doimain, txtUserID.Text, sActiveInd, getAccesslevels(accessString), sProgname, CreatedUser, _
                                           lastName, FirstName, title, email, priPhone, txtPassword.Text, _
                                           iBSN_ENTITY_LOCTN_SEQ_ID, iPROFILE_SEQ_ID, agencyUserId)
                    lblMsg.Text = "User Updated successfully."

                End If
            Else
                locationID = Login.AddSecureUser(BSN_ENTITY_SEQ_ID, doimain, txtUserID.Text, sActiveInd, getAccesslevels(accessString), sProgname, CreatedUser, _
                                                lastName, FirstName, title, email, priPhone, txtPassword.Text, agencyUserId) ', locationID
                lblMsg.Text = "User Created successfully."
            End If
            hidLocationId.Value = locationID
            divEnterInfo.Visible = False
            DivShowSavedData.Visible = True
            lblAccessLabel.Text = accessString
            If sActiveInd = "Y" Then
                lblActive.Text = "Active"
            Else
                lblActive.Text = "Inactive"
            End If
            lblEmail.Text = txtEmail.Text
            lblFirstName.Text = txtFirstName.Text
            lblLastName.Text = txtLastName.Text
            lblPhone.Text = txtPhone.Text
            lblTitle.Text = txtTitle.Text
            lblUserID.Text = txtUserID.Text
            lblPassword.Text = txtPassword.Text
            lblAgencyUserId.Text = txtAgencyUserID.Text
         Catch ccex As CCRCException

            lblMsg.Text = ccex.Message

        Catch ex As Exception

            lblMsg.Text = ex.Message
        End Try
    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Response.Redirect("../Home.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim userRoles As String

        userRoles = Request.Cookies("CCRCCookie")("profileRoles")

        If (Request.QueryString("FormMode") = "ADD") Then
            idPageTitle.Text = "User Account Request"
            txtLastName.Attributes.Add("onblur", "UpdateLoginPassword('" + txtFirstName.ClientID + "','" + txtLastName.ClientID + "')")
            txtFirstName.Attributes.Add("onblur", "UpdateLoginPassword('" + txtFirstName.ClientID + "','" + txtLastName.ClientID + "')")
        Else
            idPageTitle.Text = "User Administration"
            ReqPassword.Enabled = False
        End If

        'Register Script to do Javascript OnChange for FirstName and LastName for LoginId and UserPassword


        If InStr(1, userRoles, "83") > 0 Or InStr(1, userRoles, "29") > 0 Then
            If Not IsPostBack Then
                 Dim uName As String
                If Not Request.QueryString("ntUserId") Is Nothing Then
                    uName = Request.QueryString("ntUserId")
                    If uName.Trim.Length > 0 Then
                        SetDataForEdit(uName)
                    End If
                End If
            End If
        Else
            Response.Redirect("../Home.aspx")
        End If
    End Sub

    Private Sub SetDataForEdit(ByVal uName As String)
        Dim Logindal As LoginDAL = New LoginDAL()
        Dim dr As SqlDataReader = Logindal.GetUserInfo(uName)

        Dim BSN_ENTITY_SEQ_ID As String = ""
        Dim sActiveInd As String = rdoActive.SelectedValue
        Dim lastName As String = txtLastName.Text
        Dim FirstName As String = txtFirstName.Text

        Dim Agency_UserID As String = ""


        Dim title As String = txtTitle.Text
        Dim email As String = txtEmail.Text
        Dim priPhone As String = txtPhone.Text
        Dim roleString As String = ""
        Dim strBSN_ENTITY_LOCTN_SEQ_ID As String = ""
        Dim strPROFILE_SEQ_ID As String = ""
        While dr.Read()
            BSN_ENTITY_SEQ_ID = dr("BSN_ENTITY_SEQ_ID")
            roleString = dr("USER_ROLE_STRING").ToString()
            sActiveInd = dr("ACTIVE_IND").ToString()
            lastName = dr("LAST_NAME").ToString()
            FirstName = dr("FIRST_NAME").ToString()
            title = dr("TITLE_TXT").ToString()
            email = dr("PRIMARY_EMAIL_ADDR").ToString()
            priPhone = dr("PRIMARY_PHN").ToString()
            Agency_UserID = dr("AGENCY_USER_ID").ToString()
            strBSN_ENTITY_LOCTN_SEQ_ID = dr("BSN_ENTITY_LOCTN_SEQ_ID").ToString()
            strPROFILE_SEQ_ID = dr("PROFILE_SEQ_ID").ToString()

        End While
        dr.Close()

        Dim lst As ListItem = ENTITY.Items.FindByValue(BSN_ENTITY_SEQ_ID)
        ENTITY.SelectedIndex = ENTITY.Items.IndexOf(lst)
        Dim roleArrry() As String
        If roleString.Length > 0 Then
            roleArrry = roleString.Split(",")
            If Not roleArrry Is Nothing And roleArrry.Length > 0 Then
                Dim chk As ListItem
                For Each chk In AccessLevel.Items
                    If Array.IndexOf(roleArrry, chk.Value) > -1 Then
                        chk.Selected = True
                    End If
                Next
            End If
        End If
        If sActiveInd.Trim.ToUpper = "Y" Then
            rdoActive.SelectedIndex = 0
        Else
            rdoActive.SelectedIndex = 1
        End If
        txtEmail.Text = email
        txtFirstName.Text = FirstName
        txtLastName.Text = lastName
        ' txtPassword.Text = "*******"
        'txtPasswordConfirm.Text = "*******"
        IsEditMode.Value = "1"
        txtPhone.Text = priPhone
        txtTitle.Text = title
        txtUserID.Text = uName
        txtAgencyUserID.Text = Agency_UserID
        hidBSN_ENTITY_LOCTN_SEQ_ID.Value = strBSN_ENTITY_LOCTN_SEQ_ID
        hidPROFILE_SEQ_ID.Value = strPROFILE_SEQ_ID
        ' For Issue Id 62
        'txtUserID.Enabled = False
    End Sub

    Private Function getAccesslevels(ByRef accessString As String) As String
        Dim selValue As String = ""
        Dim chk As ListItem
        For Each chk In AccessLevel.Items
            If chk.Selected = True Then
                accessString += chk.Text & "<br>"
                selValue += chk.Value + ","
            End If
        Next
        If selValue.Length > 1 Then
            selValue = selValue.Substring(0, selValue.Length - 1)
        End If
        Return selValue
    End Function

    Protected Sub Finish_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Finish.Click
        Dim sURL As String
        Dim BSN_ENTITY_SEQ_ID As Integer = 0
        BSN_ENTITY_SEQ_ID = Request.QueryString("entityId")

        SendEmail()

        Dim sReturnPage As String = Request.QueryString("returnpage")
        sURL = sReturnPage & "?entityId=" & BSN_ENTITY_SEQ_ID.ToString() & "&FormMode=" & Request.QueryString("FormMode") & "&userLocation=" & hidLocationId.Value
        Response.Redirect(sURL)

    End Sub
    Private Function SendEmail()
        Dim myCDOMail
        myCDOMail = CreateObject("CDO.Message")
        If Err.Number <> 0 Then
            Throw New Exception(Err.Description)
        End If

        myCDOMail.MimeFormatted = True
        'txtEmail.Text = "sanikodevait@csc.com"
        myCDOMail.To = txtEmail.Text
        myCDOMail.From = txtEmail.Text
        myCDOMail.Subject = "Your CCRC account has been activated"
        myCDOMail.HTMLBody = "Test Email"

        Dim strEmailBody As String

        strEmailBody = "<div> <font size='3'>Your CCRC account has been activated.<br> <br> IMPORTANT: </font><b><font size='3'>PLEASE SAVE THE BELOW INFORMATION. YOU WILL NEED YOUR LOGIN, PASSWORD AND URL ADDRESS</font></b><font size='3'> TO ACCESS THE CCRC APPLICATION. <br> <br> Below is your new USER ID and PASSWORD. It is important that you do not"
        strEmailBody += "share your USER ID or PASSWORD with anyone. Each time you access this application, you will be required to enter your user id and password. <br> <br> Type in your USER ID and PASSWORD when prompted to access the CCRC application.<br> <br> Please select and save the following link to access CCRC:: <br> </font><b><font size='3'> <br> URL: </font></b><b><font size='3'><a href='https://www.homeownershopenetwork.org/'>https://www.homeownershopenetwork.org/</a></font></b><font size='3'> <br> <br>"
        strEmailBody += "	First Name: " + txtFirstName.Text
        strEmailBody += "<br>         Last(Name): " + lblLastName.Text
        strEmailBody += "<br> 	<br> 	Please use the following: 	<br> 	</font><b><font size='3'> 	<br> 	UserID:</font></b><font size='3'> " + lblUserID.Text
        strEmailBody += "<br> 	</font><b><font size='3'> 	<br> 	Password:</font></b><font size='3'> " + lblPassword.Text
        strEmailBody += "	<br> 	<br> 	Please do not hesitate to contact us at help@ccrconline.com should you have any comments, concerns or suggestions. </font> 	</div>"
        myCDOMail.HTMLBody = strEmailBody

        myCDOMail.Send()
        myCDOMail = Nothing
    End Function
End Class


