<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchForeclosureCase.aspx.cs" Inherits="HPF.FutureState.WebService.Test.Web.SearchForeclosureCase"%>
<asp:Content ID="ContentHeader" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Agency case number"></asp:Label>
<asp:TextBox ID="txtAgencyCaseNumber" runat="server"></asp:TextBox>
<br />
<asp:Label ID="Label2" runat="server" Text="First name"></asp:Label>
<asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
<br />
<asp:Label ID="Label3" runat="server" Text="Last name"></asp:Label>
<asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
<br />
<asp:Label ID="Label4" runat="server" Text="Loan number"></asp:Label>
<asp:TextBox ID="txtLoanNumber" runat="server"></asp:TextBox>
<br />
<asp:Label ID="Label5" runat="server" Text="Property Zip"></asp:Label>
<asp:TextBox ID="txtPropertyZip" runat="server"></asp:TextBox>
<br />
<asp:Label ID="Label6" runat="server" Text="last 4 ssn"></asp:Label>
<asp:TextBox ID="txtLast4SSN" runat="server" Width="128px"></asp:TextBox>
<br />
    <asp:Label ID="Label7" runat="server" Font-Bold="True" 
        Text="Test data: PropZip - 12345; last 4 ssn - 1234"></asp:Label>
    <br />
<asp:Button ID="btnSearch" runat="server" onclick="BtnSearch_Click" 
    Text="Search Foreclosure Case" />
    <br />
<asp:Label ID="lblResult" runat="server" Text="Label"></asp:Label>
<br />
    <br />
<asp:GridView ID="grdvResult" runat="server" 
    onselectedindexchanged="GridView1_SelectedIndexChanged">
</asp:GridView>
<br />
<br />
</asp:Content>
