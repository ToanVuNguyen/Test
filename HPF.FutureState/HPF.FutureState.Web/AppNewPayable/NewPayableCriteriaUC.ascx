<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewPayableCriteriaUC.ascx.cs" Inherits="HPF.FutureState.Web.AppNewPayable.NewPayableCriteriaUC" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%--<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />--%>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<table  width="100%">    
    <tr>
        <td  align="center" colspan="4" >
            <h1>New Payable Criteria</h1></td>
    </tr>
    <tr>
        <td colspan="4">
           <asp:BulletedList ID="bulMessage" runat="server" CssClass="ErrorMessage"></asp:BulletedList>
           </td>
    </tr>
    <tr>
        <td colspan="4" >
            <h1>Primary Selection Criteria:</h1></td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Agency*:</td>
        <td>
            &nbsp;<asp:DropDownList ID="ddlAgency" runat="server" CssClass="Text" Width="350px">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
        <td>
            <asp:Button ID="btnDraftNewPayable" runat="server"  Text="Draft New Payable"  
                CssClass="MyButton" Width="120px" onclick="btnDraftNewPayable_Click" />
       
        </td>
        
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Period Start*:</td>
        <td>
            &nbsp;<asp:TextBox ID="txtPeriodStart" runat="server" CssClass="Text" Text="1/1/2003" Width="80px"> </asp:TextBox>
            
            <cc1:CalendarExtender ID="txtPeriodStart_CalendarExtender" runat="server" 
                Enabled="True" TargetControlID="txtPeriodStart">
            </cc1:CalendarExtender>
            
        </td>
        <td>
            &nbsp;</td>
        <td>
           </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Period End*:</td>
        <td>
            &nbsp;<asp:TextBox ID="txtPeriodEnd" runat="server" CssClass="Text" Text="5/1/2008" Width="80px"></asp:TextBox>
            <cc1:CalendarExtender ID="txtPeriodEnd_CalendarExtender" runat="server" 
                Enabled="True" TargetControlID="txtPeriodEnd">
            </cc1:CalendarExtender>
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Completed?*:</td>
        <td>
            &nbsp;<asp:DropDownList ID="ddlCaseCompleted" runat="server" CssClass="Text" Width="350px">
             <asp:ListItem Value="B" Text="Select Both Complete & Incomplete Cases"></asp:ListItem>
            <asp:ListItem Value="Y" Text="Select Only Complete Cases" Selected="True"></asp:ListItem>
            <asp:ListItem Value="N" Text="Select Only Incomplete Cases"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Include No-No Consent?:</td>
        <td>
            <asp:CheckBox ID="ChkInclude" runat="server" Text="" />
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
   
</table>
