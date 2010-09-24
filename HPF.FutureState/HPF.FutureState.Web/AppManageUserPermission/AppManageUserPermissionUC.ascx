<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AppManageUserPermissionUC.ascx.cs" Inherits="HPF.FutureState.Web.AppManageUserPermission.AppManageUserPermissionUC" %>
<div class="Text">
    <asp:Label ID="lblErrorMessage" runat="server" CssClass="ErrorMessage"></asp:Label>
</div>
<div align="center">
<h3>Manage User Permission</h3></div>
<table width="50%">
<tr>
        <td class="sidelinks" align="left" nowrap="nowrap">User Login</td>        
        <td  align="left">
            <asp:Label ID="lblUserLoginId" runat="server"></asp:Label>
        </td>        
</tr>
<tr>
        <td class="sidelinks" align="left" nowrap="nowrap">User Name</td>        
        <td  align="left">
            <asp:Label ID="lblUserName" runat="server"></asp:Label>
        </td>        
</tr>
<tr>
        <td class="sidelinks" align="left" nowrap="nowrap">User Role:</td>        
        <td  align="left">
            <asp:TextBox ID="txtUserRole" Columns="50" Rows="1" runat="server"/>
        </td>        
</tr>
<tr>
        <td class="sidelinks" align="left" nowrap="nowrap">User Phone:</td>        
        <td  align="left">
            <asp:TextBox ID="txtPhone" Columns="50" Rows="1" runat="server"/>
        </td>        
</tr>
<tr>
        <td class="sidelinks" align="left" nowrap="nowrap">User Type:</td>        
        <td  align="left">
            <asp:DropDownList ID="ddlUserType" runat="server"></asp:DropDownList>
        </td>        
</tr>
<tr>
        <td class="sidelinks" align="left" nowrap="nowrap">Agency:</td>        
        <td  align="left">
            <asp:DropDownList ID="ddlAgency" runat="server"></asp:DropDownList>
        </td>        
</tr>
</table>
<table width="100%" border="1" style="border-collapse:collapse">
<tr>
  <td class="sidelinks" align="center">
    IN USE
  </td>
  <td class="sidelinks" align ="center">
  MENU NAME
  </td>
  <td class="sidelinks" align ="center">
  PERMISSION
  </td>
</tr>  
<!-- List of menu permission -->
    <asp:PlaceHolder ID="placeHolder" runat="server"></asp:PlaceHolder>
<!-- End List of menu permission -->
<tr>
    <td colspan="4" align="center" nowrap="nowrap">
        <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="120px" 
            CssClass="MyButton" onclick="btnUpdate_Click"/>
        <asp:Button ID="btnClose" runat="server" Text="Close" Width="120px" 
            CssClass="MyButton" onclick="btnClose_Click"/>
    </td>
    </tr>
</table>