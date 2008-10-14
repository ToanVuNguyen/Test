
Partial Class Logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cookies.Clear()
        Session.Abandon()
        lblMsg.Text = "Log out Successfull"
    End Sub
End Class
