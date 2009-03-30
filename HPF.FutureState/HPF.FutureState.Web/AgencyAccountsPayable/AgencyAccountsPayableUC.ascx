<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgencyAccountsPayableUC.ascx.cs"
    Inherits="HPF.FutureState.Web.AgencyAccountsPayable.AgencyAccountsPayableUC" %>
<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%--<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />--%>
<asp:ScriptManager ID="myscript" runat="server">
</asp:ScriptManager>
<table width="100%">
    <colgroup>
        <col width="5%" />
        <col width="30%" />
        <col width="15%" />
        <col width="20%" />
        <col width="15%" />
        <col width="15%" />
    </colgroup>
    <tr>
        <td colspan="5" align="center" >
           <h1> Agency Accounts Payable page</h1>
        </td>
        <td rowspan="3" align="center">
            <asp:HyperLink ID="lblPortal" runat="server" Target="_blank">
            <img alt="Payables on Portal" src="Styles/Images/HPFLogo.jpg" style="width: 55px; height: 55px" border="0"/><br />            
            Payables on Portal</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Agency*:
        </td>
        <td>
            <asp:DropDownList ID="ddlAgency" runat="server" Height="16px" CssClass="Text" Width="210px">
            </asp:DropDownList>
        </td>
        <td class="sidelinks" align="right">
            Period Start*:
        </td>
        <td>
            <asp:TextBox ID="txtPeriodStart" runat="server" CssClass="Text" Width="150px" MaxLength="100"></asp:TextBox>
            <cc2:CalendarExtender ID="txtPeriodStart_CalendarExtender" runat="server" Enabled="True"
                TargetControlID="txtPeriodStart">
            </cc2:CalendarExtender>
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
        <td class="sidelinks" align="right">
            Period End:
        </td>
        <td>
            <asp:TextBox ID="txtPeriodEnd" runat="server" Width="150px" MaxLength="100" CssClass="Text"></asp:TextBox>
            <cc2:CalendarExtender ID="txtPeriodEnd_CalendarExtender" runat="server" Enabled="True"
                TargetControlID="txtPeriodEnd">
            </cc2:CalendarExtender>
        </td>
        <td align="right">
            <asp:Button ID="btnRefreshList" runat="server" Text="Refresh List" Width="100px"
                CssClass="MyButton" OnClick="btnRefreshList_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:BulletedList ID="bulMessage" runat="server" CssClass="ErrorMessage">
            </asp:BulletedList>
        </td>
    </tr>
    <tr>
        <td colspan="6" class="sidelinks">
            Invoice List:
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <cc1:StatefullScrollPanel ID="StatefullScrollPanel1" runat="server" CssClass="ScrollTable"
                Width="100%">
                <asp:UpdatePanel ID="myupdatepanel" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grvInvoiceList" runat="server" BorderStyle="None" Width="100%"
                            AutoGenerateColumns="false" DataKeyNames="AgencyPayableId" OnSelectedIndexChanged="grvInvoiceList_SelectedIndexChanged">
                            <HeaderStyle CssClass="FixedHeader" HorizontalAlign="Center" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                            <Columns>
                                <asp:BoundField HeaderText="Agency" DataField="AgencyName" />
                                <asp:BoundField HeaderText="Payable#" DataField="AgencyPayableId" />
                                <asp:BoundField HeaderText="Payable Dt" DataField="PaymentDate" DataFormatString="{0:d}" />
                                <asp:TemplateField HeaderText="Payable Period">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPayablePeriod" runat="server" Text='<%#Eval("PeriodStartDate","{0:d}")+" - "+Eval("PeriodEndDate","{0:d}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Amount" DataField="TotalAmount" DataFormatString="{0:C}"
                                    ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField HeaderText="Status" DataField="StatusDesc" />
                                <asp:BoundField HeaderText="Comments" DataField="PaymentComment" />
                                <asp:CommandField ShowSelectButton="true" ButtonType="Button" ControlStyle-CssClass="MyButton"
                                    ItemStyle-HorizontalAlign="Center" HeaderText="Select" />
                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="hidSelectedRowIndex" runat="server" Value="" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </cc1:StatefullScrollPanel>
        </td>
        <td valign="top" width="120">
                        <asp:Button ID="btnNewPayable" runat="server" Text="New Payable" Width="130px" CssClass="MyButton"
                            OnClick="btnNewPayable_Click" />
                        <asp:Button ID="btnViewPayable" runat="server" Text="View/Edit Payable" CssClass="MyButton"
                            Width="130px" OnClick="btnViewPayable_Click" />
                        <asp:Button ID="btnCancelPayable" runat="server" Text="Cancel Payable" CssClass="MyButton"
                            Width="130px" OnClick="btnCancelPayable_Click" />
        </td>
    </tr>
</table>

<script language="javascript" type="text/javascript">
    var id = '<%=hidSelectedRowIndex.ClientID %>'
    function CancelClientClick() {
        var SelectedIndex = document.getElementById(id);
        if (SelectedIndex.value == '') {
            return true;
        }
        else {
            Popup.showModal('modal');
            return false;
        }
    }
</script>

<div id="modal" style="border: 1px solid black; background-color: #60A5DE; padding: 1px;
    text-align: center; font-family: Verdana, Arial, Helvetica, sans-serif; display: none;">
    <div class="PopUpHeader">
        HPF Billing&amp;Admin</div>
    <table width="250" cellpadding="5">
        <tr>
            <td class="PopUpMessage">
                ERR586-Are you sure you wish to cancel the selected payable?
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnYes" runat="server" OnClientClick="Popup.hide('modal');" CssClass="MyButton"
                    Text="Yes" OnClick="btnYes_Click" Width="70px" />
                &nbsp;
                <asp:Button ID="btnNo" runat="server" OnClientClick="Popup.hide('modal');return false;"
                    CssClass="MyButton" Width="70px" Text="No" />
            </td>
        </tr>
    </table>
</div>
