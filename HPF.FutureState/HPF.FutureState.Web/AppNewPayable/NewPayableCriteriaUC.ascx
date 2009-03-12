<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewPayableCriteriaUC.ascx.cs" Inherits="HPF.FutureState.Web.AppNewPayable.NewPayableCriteriaUC" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />

<table  width="100%">
    <colgroup>
    <col width="22%" />
    <col width="20%" />
    <col width="35%" />
    <col width="23%" />
    </colgroup>
    <tr>
        <td class="Header" colspan="4" >
            New Payable Criteria</td>
    </tr>
    <tr>
        <td colspan="4" >
            <h1>Primary Selection Criteria:</h1></td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Agency*:</td>
        <td>
            <asp:DropDownList ID="ddlAgency" runat="server" CssClass="Text" Width="350px">
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
            <asp:TextBox ID="txtPeriodStart" runat="server" CssClass="Text" Text="1/1/2003" Width="80px"> </asp:TextBox>
            
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
            <asp:TextBox ID="txtPeriodEnd" runat="server" CssClass="Text" Text="5/1/2008" Width="80px"></asp:TextBox>
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
            <asp:DropDownList ID="ddlCaseCompleted" runat="server" CssClass="Text" Width="350px">
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
    <tr>
        <td colspan="4">
           <asp:BulletedList ID="bulMessage" runat="server" CssClass="ErrorMessage"></asp:BulletedList>
           <%--<asp:RequiredFieldValidator ID="reqtxtPeriodStart"  Display="Dynamic" runat="server" ErrorMessage="Must input date" ControlToValidate="txtPeriodStart"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="reqtxtPeriodEnd" Display="Dynamic"  runat="server" ErrorMessage="Must input date" ControlToValidate="txtPeriodEnd"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="cmptxtPeriodStart" runat="server" Display="Dynamic" ErrorMessage="Input correct date format" ControlToValidate="txtPeriodStart" ValueToCompare="1/1/1900" Operator="GreaterThan"></asp:CompareValidator>
    <asp:CompareValidator ID="cmptxtPeriodEnd" Display="Dynamic" runat="server" ErrorMessage="Input correct date format" ControlToValidate="txtPeriodEnd" ValueToCompare="1/1/1900" Operator="GreaterThan"></asp:CompareValidator>--%>
   
           </td>
    </tr>
</table>
