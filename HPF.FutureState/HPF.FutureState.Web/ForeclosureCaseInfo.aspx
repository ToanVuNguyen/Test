<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForeclosureCaseInfo.aspx.cs"
    Inherits="HPF.FutureState.Web.AppForeclosureCaseDetailPage" Title="Forclosure Case Info"
    EnableEventValidation="false" enableViewStateMac="false" viewstateencryptionmode="Never"%>

<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="HPF" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <table style="width: 100%;" align="left">
        <asp:HiddenField ID="hfDoSaving" runat="server" />
        <tr>
            <td align="center">
                <h1>
                    Foreclosure Case Detail</h1>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 975px">
                 <tr>
                        <td class="sidelinks" align="right">
                            HPF Case ID:
                        </td>
                        <td  class="Text">
                            <asp:Label ID="lblHpfID" runat="server" Text="100987654" ></asp:Label>
                        </td>
                        <td align="right" class="sidelinks">
                            Agency Name:
                        </td>
                        <td class="Text">
                            <asp:Label ID="lblAgencyName" runat="server" Text="Money Management Inc." ></asp:Label>
                        </td>
                        <td align="right" class="sidelinks" valign="top">
                            Counselor*:
                        </td>
                        <td class="Text">
                            <asp:Label ID="lblCounselor" runat="server" >Amada - Huggenkiss</asp:Label>
                        </td>
                        <td align="center">
                            <asp:Button ID="btn_Print" runat="server" CssClass="MyButton" Width="130px" Text="Print Summary"
                                OnClick="btn_Print_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="sidelinks" align="right" valign="top">
                            Borrower:
                        </td>
                        <td  class="Text" colspan="3">
                            <asp:Label ID="lblBorrower" runat="server">Ivan A Mustang</asp:Label>
                            &nbsp;
                        </td>
                        <td align="right" class="sidelinks">
                            Phone &amp; Ext:
                        </td>
                        <td class="Text">
                            <asp:Label ID="lblPhone" runat="server" >877-123-1234 x55432</asp:Label>
                        </td>
                        <td align="center">
                            <asp:Button ID="btnEmailSummary" runat="server" CssClass="MyButton" Width="130px"
                                Text="Email Summary" OnClick="btnEmailSummary_Click" OnClientClick="btnEmailSummary_Click();" />
                        </td>
                    </tr>
                    <tr>
                        <td class="sidelinks" align="right" >
                            City, State, Zip:
                        </td>
                        <td class="Text" colspan="3" >
                            <asp:Label ID="lblPropertyAddress" runat="server" >Yourtown, MN 55416</asp:Label>
                            &nbsp;
                        </td>
                        <td align="right" class="sidelinks">
                            Counselor Email:
                        </td>
                        <td class="Text">
                            <asp:Label ID="lblCounselorEmail" runat="server">ahuggenkiss@moes.com</asp:Label>
                        </td>
                        <td align="center">
                            <asp:Button ID="btnResendServicer" runat="server" CssClass="MyButton" Width="130px"
                                Text="Resend to Servicer" onclick="btnResendServicer_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="sidelinks" align="right">
                            Case Loans:
                        </td>
                        <td colspan="6">
                            <asp:Label ID="lblLoanList" runat="server" CssClass="Text">1298494593 - 
                            Citibank; 554587876 - Bank of America</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <asp:BulletedList ID="lblErrorMessage" runat="server" CssClass="ErrorMessage">
                            </asp:BulletedList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
       
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="100%" align="left">
                    <tr>
                        <td>
                            <HPF:TabControl ID="tabControl" runat="server">
                            </HPF:TabControl>
                        </td>
                    </tr>
                    <tr >
                        <td style="border: solid 1px #8FC4F6">
                            <HPF:UserControlLoader ID="UserControlLoader" runat="server">
                            </HPF:UserControlLoader>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="emailSummary"  runat="server"  Value=""/>
    <script language="jscript" type="text/jscript">
    var email = document.getElementById('<%=emailSummary.ClientID %>');
    function btnEmailSummary_Click()
    {
        email.value = "EMAILSUMMARY";
    }
    </script>
</asp:Content>
