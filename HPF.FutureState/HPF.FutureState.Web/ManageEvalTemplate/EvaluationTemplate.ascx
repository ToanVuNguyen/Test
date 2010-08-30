<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EvaluationTemplate.ascx.cs" Inherits="HPF.FutureState.Web.ManageEvalTemplateTab.EvaluationTemplate" %>
<table>
<tr>
    <td colspan ="2">
        <asp:BulletedList ID="lblErrorMessage" runat="server" CssClass="ErrorMessage">
            </asp:BulletedList>
    </td>
</tr>
<tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Template Name:</td>        
        <td  align="left"><asp:TextBox ID="txtTemplateName" Columns="100" Rows="2" runat="server"/>
        </td>        
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Template Description:</td>        
        <td  align="left"><asp:TextBox ID="txtTemplateDescription" Columns="100" Rows="2" runat="server"/>
        </td>        
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Active:</td>        
        <td  align="left"><asp:CheckBox ID="chkActive" runat="server" /></td>        
    </tr>
    <tr>
    <td colspan="2" align="center" nowrap="nowrap">
        <asp:Button ID="btnAddNew" runat="server" Text="Add New" Width="120px" 
            CssClass="MyButton" onclick="btnAddNew_Click"/>
        <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="120px"  
            CssClass="MyButton" onclick="btnUpdate_Click"/>
        <asp:Button ID="btnClose" runat="server" Text="Close" Width="120px"    CssClass="MyButton"/>
    </td>
    </tr>
</table>