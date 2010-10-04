<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AppManageUserPermissionUC.ascx.cs" Inherits="HPF.FutureState.Web.AppManageUserPermission.AppManageUserPermissionUC" %>
<div class="Text">
    <asp:BulletedList ID="lblErrorMessage" runat="server" CssClass="ErrorMessage">
            </asp:BulletedList>
</div>
<div align="center">
<h3>Manage User Permission</h3></div>
<table width="50%">
<tr>
        <td class="sidelinks" align="left" nowrap="nowrap">User Login</td>        
        <td  align="left">
            <asp:TextBox ID="txtUserLoginId" runat="server"></asp:TextBox>
        </td>        
</tr>
<tr>
        <td class="sidelinks" align="left" nowrap="nowrap">Password</td>        
        <td  align="left">
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
        </td>        
</tr>
<tr>
        <td class="sidelinks" align="left" nowrap="nowrap">First Name</td>        
        <td  align="left">
            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
        </td>        
</tr>
<tr>
        <td class="sidelinks" align="left" nowrap="nowrap">Last Name</td>        
        <td  align="left">
            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
        </td>        
</tr>
<tr>
        <td class="sidelinks" align="left" nowrap="nowrap">Email</td>        
        <td  align="left">
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
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
<tr>
        <td class="sidelinks" align="left" nowrap="nowrap">Active:</td>        
        <td  align="left">
            <asp:CheckBox ID="chkActive" Checked="true" runat="server" />
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
        <asp:Button ID="btnAddNew" runat="server" Text="Add New" Width="120px" 
            CssClass="MyButton" onclick="btnAddNew_Click"/>
        <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="120px" 
            CssClass="MyButton" onclick="btnUpdate_Click"/>
        <asp:Button ID="btnClose" runat="server" Text="Close" Width="120px" 
            CssClass="MyButton" onclick="btnClose_Click"/>
    </td>
    </tr>
</table>