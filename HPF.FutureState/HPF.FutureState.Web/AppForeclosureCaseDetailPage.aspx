<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AppForeclosureCaseDetailPage.aspx.cs" Inherits="HPF.FutureState.Web.AppForeclosureCaseDetailPage" Title="App Forclosure Case Detail"  EnableEventValidation="false"%>
<%@ Register assembly="HPF.FutureState.Web.HPFWebControls" namespace="HPF.FutureState.Web.HPFWebControls" tagprefix="HPF" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <table style="width:100%;" >
        <tr>
            <td align="center" colspan="6">
                <h1>Foreclosure Case Detail</h1></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblHpfID" runat="server" Text="100987654" CssClass="Text"></asp:Label>
            </td>
            <td align="right" class="sidelinks" >
                Agency Name:</td>
            <td>
                <asp:Label ID="lblAgencyName" runat="server" Text="Money Management Inc." 
                    CssClass="Text"></asp:Label>
            </td>
            <td align="right" class="sidelinks">
              Countselor*:  </td>
            <td>
                <asp:Label ID="lblCounselor" runat="server" CssClass="Text">Amada - Huggenkiss</asp:Label>
            </td>
            <td align="center">
                <asp:Button ID="btn_Print" runat="server" CssClass="MyButton" 
                    Text="Print Summary" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblBorrower" runat="server" CssClass="Text">Ivan A Mustang</asp:Label>
            </td>
            <td colspan="2">
                &nbsp;</td>
            <td align="right" class="sidelinks">
                Phone & Ext:</td>
            <td>
                <asp:Label ID="lblPhone" runat="server" CssClass="Text">877-123-1234 x55432</asp:Label>
            </td>
            <td align="center">
                <asp:Button ID="btnEmailSummary" runat="server" CssClass="MyButton" 
                    Text="Email Summary" onclick="btnEmailSummary_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPropertyAddress" runat="server" CssClass="Text">Yourtown, MN 55416</asp:Label>
            </td>
            <td colspan="2">
                &nbsp;</td>
            <td align="right" class="sidelinks">
                Counselor Email:</td>
            <td colspan="2">
                <asp:Label ID="lblCounselorEmail" runat="server" CssClass="Text">ahuggenkiss@moes.com</asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:Label ID="lblLoanList" runat="server" CssClass="Text">1298494593 - Citibank; 554587876 - 
                Bank of America</asp:Label>
            </td>
        </tr>
        <tr>
        <td>
        <asp:Label ID="lblErrorMessage" runat="server"  CssClass="ErrorMessage"></asp:Label>
        </td>
        </tr>
        
        <tr>
            <td colspan="6">
                <table cellpadding="0" cellspacing= "0">
                    <tr>
                        <td>
                            <HPF:TabControl ID="tabControl" runat="server">
                            </HPF:TabControl>        
                        </td>
                    </tr>
                    <tr>
                        <td  style="border:solid 1px #8FC4F6">
                            <HPF:UserControlLoader  ID="UserControlLoader" runat="server">
                            </HPF:UserControlLoader>
                        </td>
                    </tr>
                </table>
                
            </td>
        </tr>
    </table>

</asp:Content>
