<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RetrieveServicerList.aspx.cs" Inherits="HPF.FutureState.WebService.Test.Web.WebForm2" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
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
                    <td width="150">
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
                                     <td>
   
        <asp:Button ID="btnSearch" runat="server" onclick="Button1_Click" 
                Text="Retrieve Servicers" CssClass="MyButton" />
                </td>
                </tr>
                </table>
            </td>
            </tr>
            </table>
            </td>            
        </tr>
        <tr>
            <td>
                &nbsp;
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
    <asp:GridView ID="grdMessage" runat="server" CssClass="GridViewStyle" 
        CellPadding="4" ForeColor="#333333" Visible="False">
        <RowStyle BackColor="#EFF3FB" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle CssClass="GridHeader" BackColor="#336699" Font-Size="XX-Small" 
            ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
            </td>
        </tr>
    </table>
    <asp:GridView ID="grdServicers" runat="server" CssClass="GridViewStyle" 
        CellPadding="4" ForeColor="#333333" Visible="False">
        <RowStyle BackColor="#EFF3FB" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle CssClass="GridHeader" BackColor="#336699" BorderStyle="Solid" 
            ForeColor="White" Font-Size="XX-Small" />
        <EditRowStyle BackColor="#2461BF" BorderStyle="Solid" BorderWidth="1px" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
</asp:Content>
