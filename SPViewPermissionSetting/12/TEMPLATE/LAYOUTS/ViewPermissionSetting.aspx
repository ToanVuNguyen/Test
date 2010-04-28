<%@ Assembly Name="SPViewPermissionSetting, Version=2.0.0.0, Culture=neutral, PublicKeyToken=1aa8227d10ccc3ee"%> 
<%@ Page Language="C#" Inherits="Bewise.SharePoint.SPViewPermissionSetting.ViewPermissionPage" MasterPageFile="~/_layouts/application.master" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ID="Content2" ContentPlaceHolderId="PlaceHolderPageTitleInTitleArea" runat="server">
	<SharePoint:FormattedStringWithListType ID="FormattedStringWithListType1" runat="server"
		String="View Permission Settings:" LowerCase="false" />
	<a id=onetidListHlink HREF=<% SPHttpUtility.AddQuote(SPHttpUtility.UrlPathEncode(CurrentList.DefaultViewUrl,true),Response.Output);%>><%SPHttpUtility.HtmlEncode(CurrentList.Title, Response.Output);%></a>
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="PlaceHolderMain" runat="server">   
    <%= RenderPage() %>
    <div style="float:right;margin-top:10px;">
        <asp:Button ID="OK" runat="server" Text="OK" OnClick="SaveCustomPermission" CssClass="ms-ButtonHeightWidth" />
        <asp:Button ID="Reset" runat="server" Text="Reset" OnClick="ResetCustomPermission" CssClass="ms-ButtonHeightWidth" />
        <asp:Button ID="Cancel" runat="server" Text="Cancel" CssClass="ms-ButtonHeightWidth" />
    </div>    
</asp:Content>
