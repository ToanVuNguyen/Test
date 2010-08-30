<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TemplateSection.ascx.cs" Inherits="HPF.FutureState.Web.ManageEvalTemplateTab.TemplateSection" %>
<table width="100%" border="1" style="border-collapse:collapse">
<tr>
 <td colspan="3" class="ErrorMessage">
   <asp:BulletedList ID="lblErrorMessage" runat="server" CssClass="ErrorMessage">
   </asp:BulletedList>
  </td>
</tr>
<tr>
  <td class="sidelinks" align="center">
    IN USE
  </td>
  <td class="sidelinks" align ="center">
  SECTION NAME
  </td>
  <td class="sidelinks" align ="center">
  SECTION ORDER
  </td>
</tr>  
<!-- List of sections -->
    <asp:PlaceHolder ID="placeHolder" runat="server"></asp:PlaceHolder>
<!-- End List of sections -->
<tr>
    <td colspan="3" align="center" nowrap="nowrap">
        <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="120px" 
            CssClass="MyButton" onclick="btnUpdate_Click"/>
        <asp:Button ID="btnClose" runat="server" Text="Close" Width="120px" CssClass="MyButton"/>
    </td>
    </tr>
</table>
