<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailSummary.aspx.cs" Inherits="HPF.FutureState.Web.EmailSummary" %>

<%@ Register src="SummaryEmail/SummaryEmailUC.ascx" tagname="SummaryEmailUC" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Email Summary</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <uc1:SummaryEmailUC ID="SummaryEmailUC1" runat="server" />
    
    </div>
    </form>
</body>
</html>
