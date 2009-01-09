<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewPayableCriteriaUC.ascx.cs" Inherits="HPF.FutureState.Web.AppNewPayable.NewPayableCriteriaUC" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />

<table  width="100%">
    <colgroup>
    <col width="15%" />
    <col width="20%" />
    <col width="20%" />
    <col width="20%" />
    <col width="25%" />
    </colgroup>
    <tr>
        <td class="Header" colspan="5" >
            New Payable Criteria</td>
    </tr>
    <tr>
        <td colspan="5" >
            <h1>Primary Selection Criteria:</h1></td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Agency:</td>
        <td>
            <asp:DropDownList ID="ddlAgency" runat="server" CssClass="Text">
            </asp:DropDownList>
        </td>
        <td class="sidelinks" align="right">
            Case Completed:</td>
        <td>
            <asp:DropDownList ID="ddlCaseCompleted" runat="server" CssClass="Text">
            <asp:ListItem Value="Y" Text="Yes" Selected="True"></asp:ListItem>
            <asp:ListItem Value="N" Text="No"></asp:ListItem>
            <asp:ListItem Value="" Text=""></asp:ListItem>
            
            </asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="btnDraftNewPayable" runat="server"  Text="Draft New Payable"  
                CssClass="MyButton" Width="120px" onclick="btnDraftNewPayable_Click" />
       
        </td>
        
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Period Start:</td>
        <td>
            <asp:TextBox ID="txtPeriodStart" runat="server" CssClass="Text" Text="1/1/2003"> </asp:TextBox>
        </td>
        <td class="sidelinks" align="right">
            Servicer Consent:</td>
        <td>
            <asp:DropDownList ID="ddlServicerConsent" runat="server" CssClass="Text">
            <asp:ListItem Value="Y" Text="Yes" Selected="True"></asp:ListItem>
            <asp:ListItem Value="N" Text="No"></asp:ListItem>
            <asp:ListItem Value="" Text=""></asp:ListItem>
            
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Period End:</td>
        <td>
            <asp:TextBox ID="txtPeriodEnd" runat="server" CssClass="Text" Text="5/1/2008"></asp:TextBox>
        </td>
        <td class="sidelinks" align="right">
            Funding Consent:</td>
        <td>
            <asp:DropDownList ID="ddlFundingConsent" runat="server" CssClass="Text">
            <asp:ListItem Value="Y" Text="Yes" Selected="True"></asp:ListItem>
            <asp:ListItem Value="N" Text="No"></asp:ListItem>
            <asp:ListItem Value="" Text=""></asp:ListItem>
            
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td class="sidelinks" align="right">
            Max Number of Cases:</td>
        <td>
            <asp:TextBox ID="txtMaxNumberCase" runat="server" CssClass="Text" MaxLength="6" ></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td class="sidelinks" align="right">
            1<sup>st</sup> 2<sup>nd </sup>Indicator:</td>
        <td>
            <asp:DropDownList ID="ddlIndicator" runat="server" CssClass="Text">
            <asp:ListItem Value="" Text=""></asp:ListItem>
            <asp:ListItem Value="1st" Text="1st Mortgage"></asp:ListItem>
            <asp:ListItem Value="2nd" Text="2nd Mortgate"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="5">
           <asp:Label ID="lblMessage" runat="server" CssClass="ErrorMessage" Text="" ></asp:Label>
           
           </td>
    </tr>
</table>
