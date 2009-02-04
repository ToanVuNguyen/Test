<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewAgencyPayableResultsUC.ascx.cs" Inherits="HPF.FutureState.Web.AppNewPayable.NewAgencyPayableResultsUC" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />

   
<table width="100%">
<colgroup>
<col width="15%" />
<col width="15%" />
<col width="20%" />
<col width="20%" />
<col width="30%" />
</colgroup>
    <tr>
        <td  class="Header" colspan="5" >
            New Agency Payable Results</td>
    </tr>
    <tr>
        <td  class="Header" colspan="5" >
            &nbsp;</td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Agency:</td>
        <td >
            <asp:Label ID="lblAgency" runat="server"  CssClass="Text"></asp:Label>
        </td>
        <td class="sidelinks" align="right">
            Total Cases:</td>
        <td  >
            <asp:Label ID="lblTotalCases" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td >
            <asp:Button ID="btnRemoveMarkedCases" runat="server" Text="Remove Marked Cases" 
                Width="150px" CssClass="MyButton" onclick="btnRemoveMarkedCases_Click" />
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Period Start:</td>
        <td>
            <asp:Label ID="lblPeriodStart" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td class="sidelinks" align="right">
            Total Amount:</td>
        <td>
            <asp:Label ID="lblTotalAmount" runat="server"  CssClass="Text"></asp:Label>
        </td>
        <td>
            <asp:Button ID="btnExportToExcel" runat="server" Text="Export to Excel" Width="150px" CssClass="MyButton"   />
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Period End:</td>
        <td>
            <asp:Label ID="lblPeriodEnd" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="5">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorMessage" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" colspan="5" >
            Invoice Items:</td>
      
    </tr>
    <tr>
    <td colspan="5">
    </td>
    </tr>
    <tr>
        <td colspan="5">
        <asp:Panel ID="panInvoiceItems" runat="server" CssClass="ScrollTable">
        <asp:GridView ID="grvInvoiceItems" runat="server" BorderStyle="None" Width="100%" 
        AutoGenerateColumns="false" RowStyle-HorizontalAlign="Center" >
        <HeaderStyle CssClass="FixedHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
        <RowStyle CssClass="RowStyle" />
        <Columns>
        <asp:TemplateField>
        <HeaderTemplate>
        <asp:CheckBox ID="chkHeaderCaseID" runat="server" OnCheckedChanged="chkHeaderCaseIDCheck" AutoPostBack="true" />
        </HeaderTemplate>
        <ItemTemplate>
        <asp:CheckBox ID="chkCaseID" runat="server"  />
        </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="ForeclosureCaseId" HeaderText="Case ID" ItemStyle-HorizontalAlign="Left" />
        
        <asp:BoundField DataField="AgencyCaseId" HeaderText="Agency Case ID" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="CompletedDate" HeaderText="Complete Dt" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"/>
        <asp:BoundField DataField="AccountLoanNumber" HeaderText="Loan Num" ItemStyle-HorizontalAlign="Left"/>
        <asp:BoundField DataField="ServicerName" HeaderText="Servicer" ItemStyle-HorizontalAlign="Left"/>
        <asp:BoundField DataField="BorrowerName" HeaderText="Borrower Name" ItemStyle-HorizontalAlign="Left" />
        </Columns>
        <EmptyDataTemplate>There is no data match</EmptyDataTemplate>
        </asp:GridView>
        <table>
        <tr>
        <td>Total Cases:</td>
        <td><asp:Label ID="lblTotalCasesFooter" runat="server"></asp:Label></td>
        <td></td>
        <td>Invoice Total:</td>
        <td><asp:Label ID="lblInvoiceTotalFooter" runat="server"></asp:Label></td>
        </tr>
        </table>
        </asp:Panel>    
        
        </td>
    </tr>
    <tr>
        <td>
            </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="5">
            <table width="30%" align="center">
                <tr>
                    <td >
                        <asp:Button ID="btnGeneratePayable" runat="server" Text="Generate Payable"  
                            CssClass="MyButton" Width="120px" onclick="btnGeneratePayable_Click"/>
&nbsp;<asp:Button ID="btnCancelPayable" runat="server" Text="Cancel Payable" CssClass="MyButton" 
                            Width="120px" onclick="btnCancelPayable_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
