<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewInvoiceCriteria.ascx.cs" Inherits="HPF.FutureState.Web.AppNewInvoice.AppNewInvoice" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />
<table style="width: 100%;">
    <tr>
        <td colspan="8">
            <h1 align="center">New Invoice Criteria</h1>
        </td>
    </tr>
    <tr>
        <td colspan="8">
            &nbsp;
            <asp:RegularExpressionValidator CssClass="ErrorMessage" ControlToValidate="txtAgeMin" ID="RegularExpressionValidator1" Display="Dynamic" runat="server" ErrorMessage="Age Min: Only numeric characters allowed; " ValidationExpression="[\d]*"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMessage" ControlToValidate="txtAgeMax" ID="RegularExpressionValidator2" Display="Dynamic" runat="server" ErrorMessage="Age Max: Only numeric characters allowed; " ValidationExpression="[\d]*"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMessage" ControlToValidate="txtIncomeMin" ID="RegularExpressionValidator3" Display="Dynamic" runat="server" ErrorMessage="Household Income Min: Only numeric characters allowed; " ValidationExpression="[\d]*"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMessage" ControlToValidate="txtIncomeMax" ID="RegularExpressionValidator4" Display="Dynamic" runat="server" ErrorMessage="Household Income Max: Only numeric characters allowed; " ValidationExpression="[\d]*"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMessage" ControlToValidate="txtMaxNumberofCases" ID="RegularExpressionValidator5" Display="Dynamic" runat="server" ErrorMessage="Max Number of Cases: Only numeric characters allowed; " ValidationExpression="[\d]*"></asp:RegularExpressionValidator>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="ErrorMessage"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPeriodStart" CssClass="ErrorMessage" runat="server" ErrorMessage="Period Start is required; " Display="Dynamic" ></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPeriodEnd" CssClass="ErrorMessage" runat="server" ErrorMessage="Period End is required; " Display="Dynamic" ></asp:RequiredFieldValidator>
        </td>
        
    </tr>
    <tr>
        <td class="sidelinks" colspan="8">
            Primary Selection Criteria:
            </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            &nbsp;
            Funding Source*:</td>
        <td colspan="4">
            &nbsp;
            <asp:DropDownList ID="dropFundingSource" runat="server" CssClass="Text" 
                AutoPostBack="True" 
                onselectedindexchanged="dropFundingSource_SelectedIndexChanged1">
            </asp:DropDownList>
        </td>
        <td align="right" class="sidelinks">
            &nbsp;
            Case Completed:</td>
        <td>
            <asp:DropDownList ID="dropCaseCompleted" runat="server" CssClass="Text">
                <asp:ListItem Selected="True" Value="0">Not Selected
                </asp:ListItem>
                <asp:ListItem Value="1">Yes</asp:ListItem>
                <asp:ListItem Value="2">No</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="btnDraftNewInvoice" runat="server" CssClass="MyButton" 
                Text="Draft New Invoice" Width="120px" onclick="Button1_Click" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td colspan="4" rowspan="3">
            <div style="height:60px; overflow:auto;border:solid 1px #8FC4F6">
            
            <asp:DataList ID="lst_FundingSourceGroup" runat="server" Width="100%" 
                CellPadding="4" ForeColor="#333333" CssClass="Text">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <AlternatingItemStyle BackColor="White" />
                <ItemStyle  BackColor="#EFF3FB" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%#Eval("ServicerName") %>'></asp:Label>
                </ItemTemplate>
            </asp:DataList>
            
            </div>
        </td>
        <td align="right" class="sidelinks">
            Already Billed:</td>
        <td>
            <asp:DropDownList ID="dropAlreadyBilled" runat="server" CssClass="Text">
                <asp:ListItem Selected="True" Value="0">Not Selected
                </asp:ListItem>
                <asp:ListItem Value="1">Yes</asp:ListItem>
                <asp:ListItem Value="2">No</asp:ListItem>
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
                <asp:ListItem Selected="True" Value="0">Not Selected
                </asp:ListItem>
                <asp:ListItem Value="1">Yes</asp:ListItem>
                <asp:ListItem Value="2">No</asp:ListItem>
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
                <asp:ListItem Selected="True" Value="0">Not Selected
                </asp:ListItem>
                <asp:ListItem Value="1">Yes</asp:ListItem>
                <asp:ListItem Value="2">No</asp:ListItem>
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
            <asp:DropDownList ID="dropProgram" runat="server" CssClass="Text">
            </asp:DropDownList>
        </td>
        <td align="right" class="sidelinks">
            &nbsp;
            Max Number of Cases:</td>
        <td>
            <asp:TextBox ID="txtMaxNumberofCases" runat="server" CssClass="Text"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            &nbsp;
            Period Start*:</td>
        <td colspan="4">
            <asp:TextBox ID="txtPeriodStart" runat="server" CssClass="Text">1/1/2003</asp:TextBox>
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
            Period End*:</td>
        <td class="Text" colspan="4">
            <asp:TextBox ID="txtPeriodEnd" runat="server" CssClass="Text">1/1/2009</asp:TextBox>
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
                <asp:ListItem Selected="True" Value="0">Not Selected
                </asp:ListItem>
                <asp:ListItem Value="1">Yes</asp:ListItem>
                <asp:ListItem Value="2">No</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right" class="sidelinks">
            </td>
        <td>
            </td>
        <td>
            </td>
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
            <asp:TextBox ID="txtCity" runat="server" CssClass="Text"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            Hispanic:</td>
        <td colspan="4">
            <asp:DropDownList ID="dropHispanic" runat="server" CssClass="Text">
                <asp:ListItem Selected="True" Value="0">Not Selected
                </asp:ListItem>
                <asp:ListItem Value="1">Yes</asp:ListItem>
                <asp:ListItem Value="2">No</asp:ListItem>
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

                
