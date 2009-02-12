<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master"  CodeBehind="AgencyPayableInfo.aspx.cs" Inherits="HPF.FutureState.Web.AppViewEditAgencyPayable" %>

<%@ Register src="AppNewPayable/ViewEditAgencyPayable.ascx" tagname="ViewEditAgencyPayable" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    
    <uc1:ViewEditAgencyPayable ID="ViewEditAgencyPayable1" runat="server" />
    
</asp:Content>