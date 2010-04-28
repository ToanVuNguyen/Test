<%@ Control Language="C#"   AutoEventWireup="false" %>
<%@ Register TagPrefix="BewiseViewPermission" Assembly="SPViewPermissionSetting, Version=2.0.0.0, Culture=neutral, PublicKeyToken=1aa8227d10ccc3ee" namespace="Bewise.SharePoint.SPViewPermissionSetting"%>
<%@ Register TagPrefix="SharePoint" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" namespace="Microsoft.SharePoint.WebControls"%>
<SharePoint:RenderingTemplate ID="ViewSelector" runat="server">
	<Template>
		<table border=0 cellpadding=0 cellspacing=0 style='margin-right: 4px'>
		<tr>
		   <td nowrap class="ms-listheaderlabel"><SharePoint:EncodedLiteral runat="server" text="<%$Resources:wss,view_selector_view%>" EncodeMethod='HtmlEncode'/>&nbsp;</td>
		   <td nowrap class="ms-viewselector" id="Td1" onmouseover="this.className='ms-viewselectorhover'" onmouseout="this.className='ms-viewselector'" runat="server">
				<BewiseViewPermission:ViewPermissionSelectorMenu MenuAlignment="Right" AlignToParent="true" runat="server" id="ViewSelectorMenu" />
			</td>
		</tr>
		</table>
	</Template>
</SharePoint:RenderingTemplate>