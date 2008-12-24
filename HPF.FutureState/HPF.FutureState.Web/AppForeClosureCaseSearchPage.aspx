<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppForeClosureCaseSearchPage.aspx.cs" Inherits="HPF.FutureState.Web.AppForeClosureCaseSearchPage" %>
<%@ Register Src="~/BillingAdmin/AppForeClosureCaseSearch.ascx" TagName="AppForeClosureCaseSearch" TagPrefix="ucgrv" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucgrv:AppForeClosureCaseSearch ID="AppForeClosureCaseSearchPage1" runat="server" />
    </div>
    </form>
</body>
</html>
