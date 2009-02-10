<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintSummary.aspx.cs" Inherits="HPF.FutureState.Web.PrintSummary1" %>

<%@ Register src="PrintSummary/PrintSummaryUC.ascx" tagname="PrintSummaryUC" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Print Summary</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <uc1:PrintSummaryUC ID="PrintSummaryUC1" runat="server" />
    
    </div>
    </form>
</body>
</html>
