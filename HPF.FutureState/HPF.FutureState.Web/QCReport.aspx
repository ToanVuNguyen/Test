<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QCReport.aspx.cs" Inherits="HPF.FutureState.Web.QCReport1" %>

<%@ Register src="QCReport/QCReportUC.ascx" tagname="QCReportUC" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<html >
<body>
    <div>
        <uc1:QCReportUC ID="QCReportUC1" runat="server" />
    </div>
</body>
</html>
</asp:Content>
