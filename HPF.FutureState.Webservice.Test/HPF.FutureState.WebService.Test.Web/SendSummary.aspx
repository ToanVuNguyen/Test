
<%@ Page Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="SendSummary.aspx.cs" Inherits="HPF.FutureState.WebService.Test.Web.SendSummary" %>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    
    <div style="text-align:left"><h1>Send Summary</h1>
    </div>
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
            
            <asp:Label CssClass="sidelinks"  ID="Label120" runat="server" Text="Username" ></asp:Label>
                    </td>
                    <td>
            <asp:TextBox CssClass="Text" ID="txtUsername" runat="server" Text="admin" Width="128px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
            <asp:Label CssClass="sidelinks" ID="Label121" runat="server" Text="Password"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox CssClass="Text" ID="txtPassword" runat="Server" Text="admin" Width="128px"></asp:TextBox>
            
                    </td>
                </tr>
                
            </table>
            <br />
            <br />
        </td>
        </tr>
    </table>
    <div style="text-align:left">
        
            <table width="100%">
                <tr>
                    <td align="center">
                        <h1>
                            Email Summary</h1>
                    </td>
                </tr>                
                <tr>
                    <td align="right">
                        <table width="100%">
                            <colgroup>
                                <col width="10%" />
                                <col width="90%" />
                            </colgroup>
                            <tr>
                                <td align="left" class="sidelinks" colspan="2">
                                    Email Information:
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    FcId:*
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFcId" runat="server" CssClass="Text" 
                                        Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    To*:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtTo" runat="server" CssClass="Text" MaxLength="255" 
                                        Width="98%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Subject*:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSubject" runat="server" CssClass="Text" MaxLength="255" 
                                        Width="98%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks" valign="top">
                                    Body:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtBody" runat="server" CssClass="Text" MaxLength="2000" 
                                        Rows="20" TextMode="MultiLine" Width="98%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="center">
                                    <asp:Button ID="btnSend" runat="server" CssClass="MyButton" 
                                        onclick="btnSend_Click" Text="Send" Width="100px" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="MyButton" 
                                        OnClientClick="window.close();" Text="Cancel" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
    <asp:Label ID="lblMessage" runat="server" Text="Message:"></asp:Label>
    
    <br />
    <asp:GridView ID="grdvMessages" runat="server">
    </asp:GridView>
    </div>
    
</asp:Content>