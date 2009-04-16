<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewAgencyPayableResultsUC.ascx.cs"
    Inherits="HPF.FutureState.Web.AppNewPayable.NewAgencyPayableResultsUC" %>
<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="cc1" %>
<%--<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />--%>
<asp:ScriptManager runat="server" ID="myscriptManager">
</asp:ScriptManager>
<table width="100%" cellpadding="1" cellspacing="1">
    <tr>
        <td colspan="5" align="center">
            <h1>
                New Agency Payable Results</h1>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Agency:
        </td>
        <td class="Text" width="200">
            <asp:Label ID="lblAgency" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td class="sidelinks" align="right">
            Total Cases:
        </td>
        <td width="200">
            <asp:Label ID="lblTotalCases" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td align="right">
            <asp:Button ID="btnRemoveMarkedCases" runat="server" Text="Remove Marked Cases" Width="150px"
                CssClass="MyButton" OnClick="btnRemoveMarkedCases_Click" />
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Period Start:
        </td>
        <td class="Text" width="200">
            <asp:Label ID="lblPeriodStart" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td class="sidelinks" align="right">
            Total Amount:
        </td>
        <td width="200">
            <asp:Label ID="lblTotalAmount" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Period End:
        </td>
        <td width="200">
            <asp:Label ID="lblPeriodEnd" runat="server" CssClass="Text"></asp:Label>
        </td>
        <td>
            &nbsp;
        </td>
        <td width="200">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right" valign="top" nowrap="nowrap">
            Payment Comments:
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtComment" runat="server" CssClass="Text" Rows="5" TextMode="MultiLine"
                Width="100%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="ErrorMessage" colspan="5">
            <asp:BulletedList ID="bulErrorMessage" runat="server">
            </asp:BulletedList>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" colspan="5">
            Payable Items:
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <cc1:StatefullScrollPanel ID="pnlPayableCases" runat="server" CssClass="ScrollTable"
                Width="100%" Visible="true" ScrollBars="Auto" BorderColor="LightGray" BorderWidth="1">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grvInvoiceItems" runat="server" BorderStyle="None" Width="100%"
                            AutoGenerateColumns="false">
                            <HeaderStyle CssClass="FixedHeader" Wrap="false" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeaderCaseID" runat="server" OnClick="selectAll(this)"/>
                                    </HeaderTemplate>
                                    
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCaseID" runat="server"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ForeclosureCaseId" HeaderText="HPF Case ID" />
                                <asp:BoundField DataField="AgencyCaseId" HeaderText="Agency Case ID" />
                                <asp:BoundField DataField="CreateDate" HeaderText="Create Date" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="CompletedDate" HeaderText="Complete Dt" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:C}" />
                                <asp:BoundField DataField="AccountLoanNumber" HeaderText="Primary Loan Num" />
                                <asp:BoundField DataField="ServicerName" HeaderText="Servicer" />
                                <asp:BoundField DataField="BorrowerName" HeaderText="Borrower Name" />
                                <asp:BoundField DataField="Srvcr" HeaderText="Srvcr" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Fund" HeaderText="Fund" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                        </asp:GridView>
                        <table width="100%">
                            <tr align="right">
                                <td>
                                    <asp:Label ID="lbl1" runat="server" CssClass="sidelinks">Total Cases:</asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblTotalCasesFooter" runat="server" CssClass="Text"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl2" runat="server" CssClass="sidelinks">Invoice Total:</asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblInvoiceTotalFooter" runat="server" CssClass="Text"></asp:Label>
                                </td>
                                <td>
                                </td>
                                
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </cc1:StatefullScrollPanel>
        </td>
    </tr>
    <tr>
    <td colspan="5" align="center">
        <asp:Button ID="btnGeneratePayable" runat="server" Text="Generate Payable & Export File"
            CssClass="MyButton" OnClick="btnGeneratePayable_Click" Width="200px" />
        &nbsp;<asp:Button ID="btnCancelPayable" runat="server" Text="Cancel Payable" CssClass="MyButton"
            Width="120px" OnClick="btnCancelPayable_Click" />
    </td>
    </tr>
</table>

<script type="text/javascript">
    var mypanel = document.getElementById('<%=pnlPayableCases.ClientID %>');
    if(mypanel != null)
    {                
        mypanel.style.height = screen.height - 570;
    }
    function selectAll(involker) 
    {
        // Since ASP.NET checkboxes are really HTML input elements
        //  let's get all the inputs 
        var inputElements = document.getElementsByTagName('input');
        for (var i = 0 ; i < inputElements.length ; i++) 
        {
            var myElement = inputElements[i];
            // Filter through the input types looking for checkboxes
            if (myElement.type === "checkbox") 
            {
               // Use the involker (our calling element) as the reference 
               //  for our checkbox status
                myElement.checked = involker.checked;
            }
        }
    }
    
</script>

