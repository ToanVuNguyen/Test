<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchForeclosureCase.aspx.cs" Inherits="HPF.FutureState.WebService.Test.Web.SearchForeclosureCase"%>
<asp:Content ID="ContentHeader" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">


<asp:Table ID="Table1" runat="server">
        <asp:TableRow ID="TableRow1" runat="server">
            <asp:TableCell ID="TableCell1" runat="server">
                <asp:Label ID="Label1" runat="server" Text="Agency case number"></asp:Label>
             
             </asp:TableCell>
            <asp:TableCell ID="TableCell2" runat="server">
            <asp:TextBox ID="txtAgencyCaseNumber" runat="server"></asp:TextBox>
            
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow2" runat="server">
         <asp:TableCell ID="TableCell3" runat="server">
            <asp:Label ID="Label2" runat="server" Text="First name"></asp:Label>
         
         </asp:TableCell>
            <asp:TableCell ID="TableCell4" runat="server">
                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
            
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow3" runat="server">
         <asp:TableCell ID="TableCell5" runat="server">
         <asp:Label ID="Label3" runat="server" Text="Last name"></asp:Label>
         </asp:TableCell>
            <asp:TableCell ID="TableCell6" runat="server">
<asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
            
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow4" runat="server">
         <asp:TableCell ID="TableCell7" runat="server">
         <asp:Label ID="Label4" runat="server" Text="Loan number"></asp:Label>
         </asp:TableCell>
            <asp:TableCell ID="TableCell8" runat="server">
<asp:TextBox ID="txtLoanNumber" runat="server"></asp:TextBox>
            
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow5" runat="server">
         <asp:TableCell ID="TableCell9" runat="server">
         <asp:Label ID="Label5" runat="server" Text="Property Zip"></asp:Label>
         </asp:TableCell>
            <asp:TableCell ID="TableCell10" runat="server">
<asp:TextBox ID="txtPropertyZip" runat="server"></asp:TextBox>
            
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow6" runat="server">
            <asp:TableCell ID="TableCell11" runat="server">
            <asp:Label ID="Label6" runat="server" Text="last 4 ssn"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell12" runat="server">
                <asp:TextBox ID="txtLast4SSN" runat="server" Width="128px"></asp:TextBox>
            
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
    <asp:Label ID="Label7" runat="server" Font-Bold="True" 
        Text="Test data: PropZip - 12345; last 4 ssn - 1234"></asp:Label>
    <br />
<asp:Button ID="btnSearch" runat="server" onclick="BtnSearch_Click" 
    Text="Search Foreclosure Case" />
    <br />
<asp:Label ID="lblResult" runat="server" Text="Rows found: "></asp:Label>
<br />
    
<asp:GridView ID="grdvResult" runat="server" >
</asp:GridView>
<br />
<br />
</asp:Content>
