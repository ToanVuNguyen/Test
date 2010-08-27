<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageEvalQuestion.aspx.cs" Inherits="HPF.FutureState.Web.ManageEvalQuestion" %>

<%@ Register src="AppManageEvalQuestion/AppManageEvalQuestionUC.ascx" tagname="AppManageEvalQuestion" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<html >
<body>
    <div>
        <uc1:AppManageEvalQuestion ID="AppManageEvalQuestionPage1" runat="server" />
    </div>
</body>
</html>
</asp:Content>
