<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AppForeClosureCaseSearchPage.aspx.cs" Inherits="HPF.FutureState.Web.AppForeClosureCaseSearchPage"  EnableEventValidation="false" Title="App Foreclosure Case Search"%>
<%@ Register Src="~/AppForeclosureCaseSearch/AppForeClosureCaseSearchUC.ascx" TagName="AppForeClosureCaseSearch" TagPrefix="ucgrv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<html >
<body>
    <div>
        <ucgrv:AppForeClosureCaseSearch ID="AppForeClosureCaseSearchPage1" runat="server" />
    </div>
</body>
</html>
</asp:Content>


