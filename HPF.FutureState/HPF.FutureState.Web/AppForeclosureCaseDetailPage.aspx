<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AppForeclosureCaseDetailPage.aspx.cs" Inherits="HPF.FutureState.Web.AppForeclosureCaseDetailPage" Title="Untitled Page" %>
<%@ Register assembly="HPF.FutureState.Web.HPFWebControls" namespace="HPF.FutureState.Web.HPFWebControls" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <table style="width:100%;" >
        <tr>
            <td align="center" colspan="6">
                <h1>Foreclosure Case Detail</h1></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_HpfID" runat="server" Text="100987654" CssClass="Text"></asp:Label>
            </td>
            <td align="right" class="sidelinks" >
                Agency Name:</td>
            <td>
                <asp:Label ID="lbl_AgencyName" runat="server" Text="Money Management Inc." 
                    CssClass="Text"></asp:Label>
            </td>
            <td align="right" class="sidelinks">
              Countselor*:  </td>
            <td>
                <asp:Label ID="lbl_Counselor" runat="server" CssClass="Text">Amada - Huggenkiss</asp:Label>
            </td>
            <td align="center">
                <asp:Button ID="btn_Print" runat="server" CssClass="MyButton" 
                    Text="Print Summary" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_Borrower" runat="server" CssClass="Text">Ivan A Mustang</asp:Label>
            </td>
            <td colspan="2">
                &nbsp;</td>
            <td align="right" class="sidelinks">
                Phone & Ext:</td>
            <td>
                <asp:Label ID="lbl_Phone" runat="server" CssClass="Text">877-123-1234 x55432</asp:Label>
            </td>
            <td align="center">
                <asp:Button ID="btn_Print0" runat="server" CssClass="MyButton" 
                    Text="Email Summary" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_PropertyAddress" runat="server" CssClass="Text">Yourtown, MN 55416</asp:Label>
            </td>
            <td colspan="2">
                &nbsp;</td>
            <td align="right" class="sidelinks">
                Counselor Email:</td>
            <td colspan="2">
                <asp:Label ID="lbl_CounselorEmail" runat="server" CssClass="Text">ahuggenkiss@moes.com</asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:Label ID="lbl_LoanList" runat="server" CssClass="Text">1298494593 - Citibank; 554587876 - 
                Bank of America</asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <cc1:TabControl ID="tabControl" runat="server">
                </cc1:TabControl>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:Label ID="lbl_selected" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>

</asp:Content>
