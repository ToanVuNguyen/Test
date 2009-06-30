﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewInvoiceCriteria.ascx.cs"
    Inherits="HPF.FutureState.Web.AppNewInvoice.AppNewInvoice" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />--%>
<asp:ScriptManager runat="server">
</asp:ScriptManager>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <table style="width: 100%;">
            <tr>
                <td colspan="7">
                    <h1 align="center">
                        New Invoice Criteria</h1>
                </td>
            </tr>
            <tr>
                <td colspan="7" class="ErrorMessage">
                    <asp:BulletedList ID="lblErrorMessage" runat="server" CssClass="ErrorMessage">
                    </asp:BulletedList>
                </td>
            </tr>
            <tr>
                <td class="sidelinks" colspan="4">
                    Primary Selection Criteria:
                </td>
                <td class="sidelinks" colspan="3" align="right">
                    <asp:Button ID="btnDraftNewInvoice" runat="server" CssClass="MyButton" Text="Draft New Invoice"
                        Width="120px" OnClick="DraftNewInvoice_Click" />
                </td>
            </tr>
            <tr>
                <td align="right" class="sidelinks">
                    &nbsp; Funding Source*:
                </td>
                <td colspan="4" align="left">
                    <asp:DropDownList ID="dropFundingSource" runat="server" CssClass="Text" AutoPostBack="True"
                        Width="100%" 
                        onselectedindexchanged="dropFundingSource_SelectedIndexChanged1">
                    </asp:DropDownList>
                </td>
                <td align="right" class="sidelinks">
                    Program*:
                </td>
                <td >
                    <asp:DropDownList ID="dropProgram" runat="server" CssClass="Text">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" class="sidelinks" rowspan="2" >
                    Servicers:
                </td>
                <td colspan="4" rowspan="2" >
                    <div style="height: 60px; overflow: auto; border: solid 1px #8FC4F6">
                        <asp:DataList ID="lst_FundingSourceGroup" runat="server" Width="100%" CellPadding="4"
                            ForeColor="#333333" CssClass="Text">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#EFF3FB" />
                            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%#Eval("ServicerName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </td>
                <td align="right" class="sidelinks" nowrap="nowrap">
                    Period Start*:
                </td>
                <td >
                    <asp:TextBox ID="txtPeriodStart" runat="server" CssClass="Text">1/1/2003</asp:TextBox>
                    <cc1:CalendarExtender ID="txtPeriodStart_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="txtPeriodStart">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td align="right" class="sidelinks" nowrap="nowrap">
                    Period End*:&nbsp;
                </td>
                <td >
                    <asp:TextBox ID="txtPeriodEnd" runat="server" CssClass="Text">1/1/2009</asp:TextBox>
                    <cc1:CalendarExtender ID="txtPeriodEnd_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="txtPeriodEnd">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
            <td align="right" class="sidelinks">Servicer Funding<br/>Source Opiton*</td>
            <td colspan="5">
                <div style="border-color: #8FC4F6; border-style: solid; border-width: 1px; width:390px;" >
                    <asp:CheckBox ID="chkUnableToLocateLoanInPortfolio" runat="server" CssClass="Text" 
                                Text="Include Servicer Rejected: Unable to locate loan in portfolio" />
                    <br />
                     <asp:CheckBox ID="chkInvalidCounselingDocument" runat="server" CssClass="Text" 
                                Text="Include Servicer Rejected: Invalid counseling documentation" />
               </div>
                </td>                            
            <td>&nbsp;</td>
            </tr>
            <tr>
                <td rowspan="4" align="right" class="sidelinks" >
                    Non-Servicer
                    <br />
                    Funding Source
                    <br />
                    Option* :
                </td>
                <td colspan="4" style="border-color: #8FC4F6; border-style: solid; border-width: 1px;
                    vertical-align: top" rowspan="4">
                    <asp:CheckBox ID="chkServicerRejected" runat="server" CssClass="Text" Text="Select Servicer Rejected (except Freddie)" /><br />
                    <asp:CheckBox ID="chkServicerFreddie" runat="server" CssClass="Text" Text="Select Servicer Rejected because it's Freddie" /><br />
                    <asp:CheckBox ID="chkNeighborworksRejected" runat="server" CssClass="Text" Text="Select Neighborworks Rejected as Freddie Dupe" /><br />
                    <asp:CheckBox ID="chkFundingAgreement" runat="server" CssClass="Text" Text="Select All Servicers w/o a Funding Agreement" /><br />
                    <asp:CheckBox ID="chkUnfunded" runat="server" CssClass="Text" 
                        Text="Select All Unfunded Cases" AutoPostBack="True" 
                        oncheckedchanged="chkUnfunded_CheckedChanged" />
                </td>
                <td align="right" class="sidelinks">
                    &nbsp;Completed?:&nbsp;
                </td>
                <td >
                    <asp:DropDownList ID="dropCaseCompleted" runat="server" CssClass="Text">
                        <asp:ListItem Value="0">Select Only Complete Cases</asp:ListItem>
                        <asp:ListItem Value="1">Select Only Incomplete Cases</asp:ListItem>
                        <asp:ListItem Value="-1">Select Both Complete & Incomplete Cases</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" class="sidelinks">
                    Duplicates?:
                </td>
                <td >
                    <asp:DropDownList ID="dropDuplicates" runat="server" CssClass="Text">
                        <asp:ListItem Value="1">Select Only Original Cases</asp:ListItem>
                        <asp:ListItem Value="0">Select Only Duplicate Cases</asp:ListItem>
                        <asp:ListItem Value="-1">Select Both Original & Duplicate Cases</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" class="sidelinks" >
                    Allow Multiple Billing?:
                </td>
                <td >
                    <asp:DropDownList ID="dropAlreadyBilled" runat="server" CssClass="Text">
                        <asp:ListItem Value="0">Yes</asp:ListItem>
                        <asp:ListItem Value="1">No</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" class="sidelinks" >
                    Ignore Funding Consent?:
                </td>
                <td >
                    <asp:DropDownList ID="dropFundingConsent" runat="server" CssClass="Text">
                        <asp:ListItem Value="0">Yes</asp:ListItem>
                        <asp:ListItem Value="1">No</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" class="sidelinks" colspan="7" style="border-bottom-style: solid;
                    border-bottom-width: 1px; border-bottom-color: #8FC4F6;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" class="sidelinks" colspan="7">
                    Secondary Selection Criteria:
                </td>
            </tr>
            <tr>
                <td align="right" class="sidelinks">
                    Max Number of Cases:
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txtMaxNumberofCases" runat="server" CssClass="Text"></asp:TextBox>
                </td>
                <td align="right" class="sidelinks">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" class="sidelinks">
                    Gender:
                </td>
                <td colspan="4">
                    <asp:DropDownList ID="dropGender" runat="server" CssClass="Text">
                    </asp:DropDownList>
                </td>
                <td align="right" class="sidelinks">
                    Household Code:
                </td>
                <td>
                    <asp:DropDownList ID="dropHouseholdCode" runat="server" CssClass="Text" Width="260px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" class="sidelinks">
                    Race:
                </td>
                <td colspan="4">
                    <asp:DropDownList ID="dropRace" runat="server" CssClass="Text">
                    </asp:DropDownList>
                </td>
                <td align="right" class="sidelinks">
                    City:
                </td>
                <td>
                    <asp:TextBox ID="txtCity" runat="server" CssClass="Text" Width="260px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="sidelinks">
                    Ethnicity:
                </td>
                <td colspan="4">
                    <asp:DropDownList ID="dropHispanic" runat="server" CssClass="Text">
                        <asp:ListItem Value="0">Yes – Hispanic or Latino</asp:ListItem>
                        <asp:ListItem Value="1">No – Not Hispanic or Latino</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right" class="sidelinks">
                    State:
                </td>
                <td>
                    <asp:DropDownList ID="dropState" runat="server" CssClass="Text">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" class="sidelinks">
                    Age:
                </td>
                <td align="right" class="sidelinks">
                    Min:
                </td>
                <td>
                    <asp:TextBox ID="txtAgeMin" runat="server" CssClass="Text" Width="120px"></asp:TextBox>
                </td>
                <td align="right" class="sidelinks">
                    Max:
                </td>
                <td align="left" class="Text">
                    <asp:TextBox ID="txtAgeMax" runat="server" CssClass="Text" Width="120px"></asp:TextBox>
                </td>
                <td align="right" class="sidelinks">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" class="sidelinks">
                    Household Gross
                    <br />
                    Annual Income:
                </td>
                <td align="right" class="sidelinks">
                    Min:
                </td>
                <td >
                    <asp:TextBox ID="txtIncomeMin" runat="server" CssClass="Text" Width="120px"></asp:TextBox>
                </td>
                <td align="right" class="sidelinks">
                    Max:
                </td>
                <td align="left" class="Text" >
                    <asp:TextBox ID="txtIncomeMax" runat="server" CssClass="Text" Width="120px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
