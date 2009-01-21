<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchForeclosureCase.aspx.cs" Inherits="HPF.FutureState.WebService.Test.Web.SearchForeclosureCase" Title="HPF Webservice Test Application - Search Foreclosure Case"%>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<div style="text-align:left"><h1>Search Foreclosure Case</h1></div>

<table style="border-bottom-style: solid; border-bottom-width: medium; border-bottom-color: #49A3FF" width="100%">
    <tr>
        <td align="left">
            <table>
                <tr>
                    <td align="center" class="sidelinks" colspan="2">
                        Authentication Info</td>
                </tr>
                <tr>
                    <td align="right">
            
            <asp:Label CssClass="sidelinks"  ID="Label28" runat="server" Text="Username" ></asp:Label>
                    </td>
                    <td>
            <asp:TextBox CssClass="Text" ID="txtUsername" runat="server" Text="admin" Width="128px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
            <asp:Label CssClass="sidelinks" ID="Label29" runat="server" Text="Password"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox CssClass="Text" ID="txtPassword" runat="Server" Text="admin" Width="128px"></asp:TextBox>
            
                    </td>
                </tr>
                <tr>
                    <td align="right">
            <asp:Label CssClass="sidelinks" ID="Label8" runat="server" Text="User type"></asp:Label>
                    </td>
                    <td>
            <asp:DropDownList runat="server" ID="ddlUserType">
            <asp:ListItem>Both</asp:ListItem>
            <asp:ListItem>Agency</asp:ListItem>
            <asp:ListItem>Call Center</asp:ListItem>
            </asp:DropDownList>
            
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </td>
        </tr>
        </table>
    <div>
    <table>
    <br />    

<asp:Table ID="Table1" runat="server">
        <asp:TableRow ID="TableRow1" runat="server">
            <asp:TableCell ID="TableCell1" runat="server">
                <asp:Label ID="Label1" runat="server" Text="Agency case number" CssClass="sidelinks"></asp:Label>
             
             </asp:TableCell>
            <asp:TableCell ID="TableCell2" runat="server">
            <asp:TextBox ID="txtAgencyCaseNumber" runat="server" CssClass="Text"></asp:TextBox>
            
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow2" runat="server">
         <asp:TableCell ID="TableCell3" runat="server">
            <asp:Label ID="Label2" runat="server" Text="First name" CssClass="sidelinks"></asp:Label>
         
         </asp:TableCell>
            <asp:TableCell ID="TableCell4" runat="server">
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="Text"></asp:TextBox>
            
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow3" runat="server">
         <asp:TableCell ID="TableCell5" runat="server">
         <asp:Label ID="Label3" runat="server" Text="Last name" CssClass="sidelinks"></asp:Label>
         </asp:TableCell>
            <asp:TableCell ID="TableCell6" runat="server">
<asp:TextBox ID="txtLastName" runat="server" CssClass="Text"></asp:TextBox>
            
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow4" runat="server">
         <asp:TableCell ID="TableCell7" runat="server">
         <asp:Label ID="Label4" runat="server" Text="Loan number" CssClass="sidelinks"></asp:Label>
         </asp:TableCell>
            <asp:TableCell ID="TableCell8" runat="server">
<asp:TextBox ID="txtLoanNumber" runat="server" CssClass="Text"></asp:TextBox>
            
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow5" runat="server">
         <asp:TableCell ID="TableCell9" runat="server">
         <asp:Label ID="Label5" runat="server" Text="Property Zip" CssClass="sidelinks"></asp:Label>
         </asp:TableCell>
            <asp:TableCell ID="TableCell10" runat="server">
<asp:TextBox ID="txtPropertyZip" runat="server" CssClass="Text"></asp:TextBox>
            
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow6" runat="server">
            <asp:TableCell ID="TableCell11" runat="server">
            <asp:Label ID="Label6" runat="server" Text="last 4 ssn" CssClass="sidelinks"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell12" runat="server">
                <asp:TextBox ID="txtLast4SSN" runat="server"  CssClass="Text"></asp:TextBox>
            
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
   
<asp:Button ID="btnSearch" runat="server" onclick="BtnSearch_Click" 
    Text="Search Foreclosure Case" CssClass="MyButton" />
    <br />
<asp:Label ID="lblResult" runat="server" Text="Rows found: " CssClass="Text"></asp:Label>
<br />
    
<asp:GridView ID="grdvResult" runat="server" CssClass="GridViewStyle" 
        CellPadding="4" ForeColor="#333333" GridLines="None">
    <RowStyle BackColor="#EFF3FB" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <HeaderStyle CssClass="GridHeader" />
    <EditRowStyle BackColor="#2461BF" />
    <AlternatingRowStyle BackColor="White" />
</asp:GridView>
<br />
<br />
</asp:Content>
