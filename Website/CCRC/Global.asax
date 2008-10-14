<%@ Application Language="VB"  %>
<%@ import namespace="System.Diagnostics"%>

<script runat="server">

Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        Dim objErr As Exception = Server.GetLastError().GetBaseException()
        Dim err As String = "Error Caught in Application_Error event" & _
                            System.Environment.NewLine & _
                            "Error in: " & Request.Url.ToString() & _
                            System.Environment.NewLine & _
                            "Error Message: " & objErr.Message.ToString() & _
                            System.Environment.NewLine & _
                            "Stack Trace:" & objErr.StackTrace.ToString()

       ' EventLog.WriteEntry("Sample_WebApp", err, EventLogEntryType.Error)
        Server.ClearError()
        'additional actions...
    End Sub
    
</script>
