<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TemplateQuestion.ascx.cs" Inherits="HPF.FutureState.Web.ManageEvalTemplateTab.TemplateQuestion" %>
<table width="100%" border="1" style="border-collapse:collapse">
<tr>
 <td colspan="4" class="ErrorMessage">
   <asp:BulletedList ID="lblErrorMessage" runat="server" CssClass="ErrorMessage">
   </asp:BulletedList>
  </td>
</tr>
<tr>
  <td class="sidelinks" align="center">
    IN USE
  </td>
  <td class="sidelinks" align ="center">
  QUESTION
  </td>
  <td class="sidelinks" align ="center">
  SECTION
  </td>
  <td class="sidelinks" align ="center">
  QUESTION ORDER
  </td>
</tr>  
<!-- List of sections -->
    <asp:PlaceHolder ID="placeHolder" runat="server"></asp:PlaceHolder>
<!-- End List of sections -->
<tr>
    <td colspan="4" align="center" nowrap="nowrap">
        <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="120px" 
            CssClass="MyButton" onclick="btnUpdate_Click"/>
        <asp:Button ID="btnClose" runat="server" Text="Close" Width="120px" 
            CssClass="MyButton" onclick="btnClose_Click"/>
    </td>
    </tr>
</table>