<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgencyAccountsPayableUC.ascx.cs" Inherits="HPF.FutureState.Web.AgencyAccountsPayable.AgencyAccountsPayableUC" %>

<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />
 <%--<asp:ScriptManager ID="myscript" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="myupdatepanel" runat="server">
        <ContentTemplate>--%>
<table width="90%">
    <colgroup>
    <col width="10%" />
    <col width="10%" />
    <col width="10%" />
    <col width="20%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td colspan="5" class="Header">
            Agency Accounts Payable page</td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Agency:</td>
        <td>
            <asp:DropDownList ID="ddlAgency" runat="server" Height="16px" CssClass="Text">
            </asp:DropDownList>
        </td>
        <td class="sidelinks" align="right">
            Period Start:</td>
        <td>
            <asp:TextBox ID="txtPeriodStart" runat="server" CssClass="Text" Width="150px" MaxLength="100"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td >
            </td>
        <td>
            </td>
        <td  class="sidelinks" align="right">
            Period End:</td>
        <td >
            <asp:TextBox ID="txtPeriodEnd" runat="server" Width="150px" MaxLength="100" CssClass="Text"></asp:TextBox>
        </td>
        <td >
            <asp:Button ID="btnRefreshList" runat="server" Text="Refresh List"  
                Width="100px" CssClass="MyButton" onclick="btnRefreshList_Click"/>
        </td>
    </tr>
    <tr>
    <td colspan="5">
    <asp:Label ID="lblMessage" runat="server" Text=""  CssClass="ErrorMessage" ></asp:Label>
    <asp:RequiredFieldValidator ID="reqtxtPeriodStart"  Display="Dynamic" runat="server" ErrorMessage="Must input date" ControlToValidate="txtPeriodStart"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="reqtxtPeriodEnd" Display="Dynamic"  runat="server" ErrorMessage="Must input date" ControlToValidate="txtPeriodEnd"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="cmptxtPeriodStart" runat="server" Display="Dynamic" ErrorMessage="Input correct date format" ControlToValidate="txtPeriodStart" ValueToCompare="1/1/1900" Operator="GreaterThan" Type="Date"></asp:CompareValidator>
    <asp:CompareValidator ID="cmptxtPeriodEnd" Display="Dynamic" runat="server" ErrorMessage="Input correct date format" ControlToValidate="txtPeriodEnd" ValueToCompare="1/1/1900" Operator="GreaterThan" Type="Date"></asp:CompareValidator>
    </td></tr><tr>
        <td colspan="5" class="sidelinks" >
            Invoice List:</td></tr><tr>
        <td colspan="4">
       
       
        <asp:Panel ID="panInvoiceList" runat="server"  CssClass="ScrollTable" Width="840px">
        <asp:GridView ID="grvInvoiceList" runat="server"  BorderStyle="None" Width="100%"   
                AutoGenerateColumns="false" 
                onselectedindexchanged="grvInvoiceList_SelectedIndexChanged"   >
        <HeaderStyle CssClass="FixedHeader" />
        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
        <RowStyle CssClass="RowStyle" />
        <SelectedRowStyle CssClass="SelectedRowStyle" />
        <Columns>
        <asp:BoundField HeaderText="Agency" DataField="AgencyName" />
        <asp:BoundField HeaderText="Payable#" DataField="AgencyPayableId" />
        <asp:BoundField HeaderText="Payable Dt" DataField="PaymentDate" DataFormatString="{0:d}" />
        <asp:TemplateField HeaderText="Payable Period">
        <ItemTemplate >
        <asp:Label ID="lblPayablePeriod" runat="server" Text='<%#Eval("PeriodStartDate","{0:d}")+" - "+Eval("PeriodEndDate","{0:d}") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="Amount" DataField="TotalAmount" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
       <asp:BoundField HeaderText="Status" DataField="StatusCode" />
        <asp:BoundField HeaderText="Comments" DataField="PaymentComment" />
        <asp:CommandField ShowSelectButton="true" ButtonType="Button" ControlStyle-CssClass="MyButton" ItemStyle-HorizontalAlign="Center" HeaderText="Select" />
        </Columns>
        <EmptyDataTemplate> There is no data match.</EmptyDataTemplate>
        </asp:GridView>
        </asp:Panel>
        
        </td>
        <td  valign="top">
            <table style="vertical-align:top;">
                <tr>
                    <td>
                        <asp:Button ID="btnNewPayable" runat="server" Text="New Payable" Width="100px" 
                            CssClass="MyButton" onclick="btnNewPayable_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnViewPayable" runat="server" Text="View/Edit Payable" 
                            CssClass="MyButton" Width="100px" onclick="btnViewPayable_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnCancelPayable" runat="server" Text="Cancel Payable"  
                            CssClass="MyButton" Width="100px" onclick="btnCancelPayable_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td><td>
            &nbsp;</td><td>
            &nbsp;</td><td>
            &nbsp;</td><td>
            &nbsp;</td></tr></table>
       <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>