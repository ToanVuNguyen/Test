<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AppNewInvoice.ascx.cs" Inherits="HPF.FutureState.Web.AppNewInvoice.AppNewInvoice" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />
<table style="width: 100%;">
    <tr>
        <td colspan="8">
            <h1 align="center">New Invoice Criteria</h1>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" colspan="8">
            Primary Selection Criteria:</td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            &nbsp;
            Funding Source:</td>
        <td colspan="4">
            &nbsp;
            <asp:DropDownList ID="dropFundingSource" runat="server" CssClass="Text" 
                AutoPostBack="True" 
                onselectedindexchanged="dropFundingSource_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="right" class="sidelinks">
            &nbsp;
            Case Completed:</td>
        <td>
            <asp:DropDownList ID="dropCaseCompleted" runat="server" CssClass="Text">
            </asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="Button1" runat="server" CssClass="MyButton" 
                Text="Draft New Invoice" Width="120px" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td colspan="4" rowspan="3">
            <asp:DataList ID="lst_FundingSourceGroup" runat="server" Height="40px">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%#Eval("ServicerName") %>'></asp:Label>
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td align="right" class="sidelinks">
            Already Billed:</td>
        <td>
            <asp:DropDownList ID="dropAlreadyBilled" runat="server" CssClass="Text">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td align="right" class="sidelinks">
            &nbsp;
            Servicer Consent:</td>
        <td>
            <asp:DropDownList ID="dropServicerConsent" runat="server" CssClass="Text">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td align="right" class="sidelinks">
            &nbsp;
            Funding Consent:</td>
        <td>
            <asp:DropDownList ID="dropFundingConsent" runat="server" CssClass="Text">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            &nbsp;
            Program:</td>
        <td colspan="4">
            <asp:DropDownList ID="dropCaseCompleted0" runat="server" CssClass="Text">
            </asp:DropDownList>
        </td>
        <td align="right" class="sidelinks">
            &nbsp;
            Max Number of Cases:</td>
        <td>
            <asp:DropDownList ID="dropMaxNoCase" runat="server" CssClass="Text">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            &nbsp;
            Period Start:</td>
        <td colspan="4">
            <asp:TextBox ID="txtPeriodStart" runat="server" CssClass="Text"></asp:TextBox>
        </td>
        <td align="right" class="sidelinks">
            &nbsp;
            
                1<sup>st</sup>2<sup>nd</sup>Indicator:
        </td>
        <td>
            <asp:DropDownList ID="dropIndicators" runat="server" CssClass="Text">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            &nbsp;
            Period End:</td>
        <td class="Text" colspan="4">
            <asp:TextBox ID="txtPeriodEnd" runat="server" CssClass="Text"></asp:TextBox>
        </td>
        <td align="right" class="sidelinks">
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            &nbsp;
            Duplicates:</td>
        <td colspan="4">
            <asp:DropDownList ID="dropDuplicates" runat="server" CssClass="Text">
            </asp:DropDownList>
        </td>
        <td align="right" class="sidelinks">
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            &nbsp;</td>
        <td colspan="4">
            &nbsp;</td>
        <td align="right" class="sidelinks">
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" class="sidelinks" colspan="8">
            Secondary Selection Criteria:</td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            Gender:</td>
        <td colspan="4">
            <asp:DropDownList ID="dropGender" runat="server" CssClass="Text">
            </asp:DropDownList>
        </td>
        <td align="right" class="sidelinks">
            Household Code:</td>
        <td colspan="2">
            <asp:DropDownList ID="dropHouseholdCode" runat="server" CssClass="Text">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            Race:</td>
        <td colspan="4">
            <asp:DropDownList ID="dropRace" runat="server" CssClass="Text">
            </asp:DropDownList>
        </td>
        <td align="right" class="sidelinks">
            City:</td>
        <td colspan="2">
            <asp:DropDownList ID="dropCity" runat="server" CssClass="Text">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            Ethnicity:</td>
        <td colspan="4">
            <asp:DropDownList ID="dropEthnicity" runat="server" CssClass="Text">
            </asp:DropDownList>
        </td>
        <td align="right" class="sidelinks">
            State:</td>
        <td colspan="2">
            <asp:DropDownList ID="dropState" runat="server" CssClass="Text">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            Age:</td>
        <td align="right" class="sidelinks">
            Min:</td>
        <td>
            <asp:TextBox ID="txtAgeMin" runat="server" CssClass="Text" Width="50px"></asp:TextBox>
        </td>
        <td align="right" class="sidelinks">
            Max:</td>
            
        <td align="left" class="Text">
            <asp:TextBox ID="txtAgeMax" runat="server" CssClass="Text" Width="50px"></asp:TextBox>
        </td>
            
        <td align="right" class="sidelinks">
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            Household Gross Annual Income:</td>
        <td align="right" class="sidelinks">
            Min:</td>
        <td>
            <asp:TextBox ID="txtIncomeMin" runat="server" CssClass="Text" Width="50px"></asp:TextBox>
        </td>
        <td align="right" class="sidelinks">
            Max:</td>
            
        <td align="left" class="Text">
            <asp:TextBox ID="txtIncomeMax" runat="server" CssClass="Text" Width="50px"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
            <td>
            &nbsp;</td>
    </tr>
</table>
