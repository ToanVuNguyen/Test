<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewAgencyPayableResultsUC.ascx.cs"
    Inherits="HPF.FutureState.Web.AppNewPayable.NewAgencyPayableResultsUC" %>
<%--<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />--%>
<style type="text/css">
    .style1
    {
        text-align: center;
        font-size: 20px;
        font-weight: bold;
        color: #8FC4F6;
        height: 6px;
    }
    .style2
    {
        font-family: Verdana, Arial, Helvetica, sans-serif;
        color: #2271A0;
        font-size: 11px;
        font-weight: bold;
        height: 8px;
    }
    .style3
    {
        height: 8px;
    }
</style>
<table width="100%">
    <colgroup>
        <col width="15%" />
        <col width="20%" />
        <col width="20%" />
        <col width="30%" />
        <col width="15%" />
    </colgroup>
    <tr>
        <td colspan="5" align="center">
            <h1>
                New Agency Payable Results</h1>
        </td>
    </tr>
    <tr>
        <td class="style1" colspan="5">
        </td>
    </tr>
  
    <tr>
        <td class="style2" align="right">
            Agency:
        </td>
        <td class="style3">
            <asp:Label ID="lblAgency" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td class="style2" align="right">
            Total Cases:
        </td>
        <td class="style3">
            <asp:Label ID="lblTotalCases" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td  align="right">
            <asp:Button ID="btnRemoveMarkedCases" runat="server" Text="Remove Marked Cases" Width="150px"
                CssClass="MyButton" OnClick="btnRemoveMarkedCases_Click" />
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Period Start:
        </td>
        <td>
            <asp:Label ID="lblPeriodStart" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td class="sidelinks" align="right">
            Total Amount:
        </td>
        <td>
            <asp:Label ID="lblTotalAmount" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Period End:
        </td>
        <td>
            <asp:Label ID="lblPeriodEnd" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Payment Comments:
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtComment" runat="server" CssClass="Text" Rows="5" TextMode="MultiLine"
                Width="100%"></asp:TextBox>
            &nbsp;
        </td>
    </tr>
      <tr  >
        <td class="ErrorMessage" colspan="5" >
        <asp:BulletedList ID="bulErrorMessage" runat="server" ></asp:BulletedList>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" colspan="5">
            Payable Items:
        </td>
    </tr>
    <tr>
        <td colspan="5">
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <asp:Panel ID="panInvoiceItems" runat="server" CssClass="ScrollTable">
                
                <asp:GridView ID="grvInvoiceItems" runat="server" BorderStyle="None" Width="100%"
                    AutoGenerateColumns="false">
                    <HeaderStyle CssClass="FixedHeader" Wrap="false" />
                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                    <RowStyle CssClass="RowStyle" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkHeaderCaseID" runat="server" OnCheckedChanged="chkHeaderCaseIDCheck"
                                     AutoPostBack="true"/>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCaseID" runat="server" OnCheckedChanged="chkCaseIDCheck" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ForeclosureCaseId" HeaderText="HPF Case ID" />
                        <asp:BoundField DataField="AgencyCaseId" HeaderText="Agency Case ID" />
                        <asp:BoundField DataField="CreateDate" HeaderText="Create Date" 
                            DataFormatString="{0:d}" />
                        <asp:BoundField DataField="CompletedDate" HeaderText="Complete Dt"
                            DataFormatString="{0:d}" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:C}" />
                        <asp:BoundField DataField="AccountLoanNumber" HeaderText="Primary Loan Num" />
                        <asp:BoundField DataField="ServicerName" HeaderText="Servicer"  />
                        <asp:BoundField DataField="BorrowerName" HeaderText="Borrower Name" />
                        <asp:BoundField DataField="Srvcr" HeaderText="Srvcr" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Fund" HeaderText="Fund" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                   
                </asp:GridView>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lbl1" runat="server" Visible="false">Total Cases:</asp:Label> 
                        </td>
                        <td>
                            <asp:Label ID="lblTotalCasesFooter" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                           <asp:Label ID="lbl2" runat="server" Visible="false" >Invoice Total:</asp:Label> 
                        </td>
                        <td>
                            <asp:Label ID="lblInvoiceTotalFooter" Visible="false" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <table width="40%" align="center">
                <tr>
                    <td>
                        <asp:Button ID="btnGeneratePayable" runat="server" Text="Generate Payable & Export File"
                            CssClass="MyButton" OnClick="btnGeneratePayable_Click" Width="200px" />
                        &nbsp;<asp:Button ID="btnCancelPayable" runat="server" Text="Cancel Payable" CssClass="MyButton"
                            Width="120px" OnClick="btnCancelPayable_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
