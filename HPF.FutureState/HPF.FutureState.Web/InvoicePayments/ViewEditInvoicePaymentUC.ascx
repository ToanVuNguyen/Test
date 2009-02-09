<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewEditInvoicePaymentUC.ascx.cs" Inherits="HPF.FutureState.Web.InvoicePayments.ViewEditInvoicePayment" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />
<table width="800px">
<colgroup>
<col width="20%" />
<col width="80%" />
</colgroup>
    <tr>
        <td colspan="2">
            <h1>View/Edit Invoice Payment</h1></td>
    </tr>
    <tr>
        <td colspan="2" class="ErrorMessage">
            <asp:BulletedList ID="lblErrorMessage" runat="server" BulletStyle="Square">
            </asp:BulletedList>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" colspan="2">
            Payment Infomation:</td>
       
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            HPF Payment ID:</td>
        <td>
            <asp:Label ID="lblPaymentID" runat="server" CssClass="Text"></asp:Label></td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Funding Source*:</td>
        <td>
            <asp:DropDownList ID="ddlFundingSource" runat="server" CssClass="Text" >
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Payment Number*:</td>
        <td>
            <asp:TextBox ID="txtPaymentNum" runat="server" CssClass="Text" Width="150px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Payment Date*:</td>
        <td>
            <asp:TextBox ID="txtPaymentDt" runat="server" CssClass="Text" Width="150px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Payment Type*:</td>
        <td>
            <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="Text" >
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Payment Amount*:</td>
        <td>
            <asp:TextBox ID="txtPaymentAmt" runat="server"  CssClass="Text" Width="150px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Reconciliation File:</td>
        <td>
            <asp:FileUpload ID="fileUpload"  runat="server" Width="100%" CssClass="Text" 
                Height="18px" />
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
            Comments:</td>
        <td>
            <asp:TextBox ID="txtComment" runat="server" CssClass="Text" Rows="6" TextMode="MultiLine"  Width="100%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td align="center">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="MyButton" 
                width="100px" onclick="btnSave_Click"/>
            &nbsp;&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="MyButton" 
                Width="100px" onclick="btnCancel_Click"/>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
