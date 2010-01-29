<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="~/EditListFormCtrl.ascx.cs"
    Inherits="CustomEditWebpart.EditListFormCtrl, CustomEditWebpart, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Assembly Name="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"
    Namespace="Microsoft.SharePoint.WebControls" %>
<%@ Register TagPrefix="SPHttpUtility" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"
    Namespace="Microsoft.SharePoint.Utilities" %>
<%--<%@ Register TagPrefix="wssuc" TagName="ToolBar" src="12/TEMPLATE/CONTROLTEMPLATES/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" src="12/TEMPLATE/CONTROLTEMPLATES/_controltemplates/ToolBarButton.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormSection" src="12/TEMPLATE/CONTROLTEMPLATES/_controltemplates/InputFormSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" src="12/TEMPLATE/CONTROLTEMPLATES/_controltemplates/InputFormControl.ascx" %>
--%>
<span id='part1'>
    <%--<wssuc:ToolBar CssClass="ms-formtoolbar" id="toolBarTbltop" RightButtonSeparator="&nbsp;" runat="server">
					<Template_RightButtons>
						<SharePoint:SaveButton  runat="server" ID="btnSave"/>
						<SharePoint:GoBackButton runat="server"/>
					</Template_RightButtons>
			</wssuc:ToolBar>--%>
    <SharePoint:UserInfoListFormToolBar runat="server" />
    <table class="ms-formtable" style="margin-top: 8px;" border="0" cellpadding="0" cellspacing="0"
        width="100%">
        <tr>
            <SharePoint:CompositeField FieldName="Name" ControlMode="Display" runat="server" />
        </tr>
        <SharePoint:ListFieldIterator runat="server" ID="myList" />
        <SharePoint:FormComponent TemplateName="AttachmentRows" runat="server" />
    </table>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="ms-formline">
                <img src="/_layouts/images/blank.gif" width="1" height="1" alt="">
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="100%" style="padding-top: 7px">
        <tr>
            <td width="100%">
                <SharePoint:ItemHiddenVersion runat="server" />
                <%--<wssuc:toolbar cssclass="ms-formtoolbar" id="toolBarTbl" rightbuttonseparator="&nbsp;"
                    runat="server">
					<Template_Buttons>
						<SharePoint:CreatedModifiedInfo runat="server"/>
					</Template_Buttons>
					<Template_RightButtons>
						<SharePoint:SaveButton  runat="server"/>
						<SharePoint:GoBackButton runat="server"/>
					</Template_RightButtons>
			</wssuc:toolbar>--%>
            </td>
        </tr>
    </table>
</span>
<p>
    zdadasdasdasdas</p>
<p>
    <asp:Button ID="Button1" runat="server" Text="Button" />
</p>
