<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HPF.FutureState.WebService.Test.Web.Default"%>
<asp:Content ID="ContentHeader" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Table ID="Table1" runat="server" Visible="false">
    <asp:TableRow>
        <asp:TableCell>
        Username:
        </asp:TableCell>
        <asp:TableCell>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
        Password
        </asp:TableCell>
        <asp:TableCell>
        </asp:TableCell>
    </asp:TableRow>
    </asp:Table>
    <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/SearchForeclosureCase.aspx">Search Foreclosure Case</asp:LinkButton>
    <br />
    <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/InsertCallLog.aspx">Insert Call Log</asp:LinkButton>
    <br />
    <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="~/RetrieveCallLog.aspx">Retrieve Call Log</asp:LinkButton>
</asp:Content>
