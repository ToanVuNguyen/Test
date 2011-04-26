<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MarkRebillables.ascx.cs" Inherits="HPF.FutureState.Web.MarkRebillables.MarkRebillables" %>
<table style="width:100%;">
    <tr>
        <td width="10">
            &nbsp;</td>
        <td>
            <asp:BulletedList ID="lstErrorMessage" runat="server" CssClass="ErrorMessage">
            </asp:BulletedList>
        </td>
        <td width="10">
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td align="center">
            <h3>Mark Rebillable Invoice Cases</h3>
            <asp:Panel ID="pnlContent" runat="server">
                <table cellpadding="0" cellspacing="0" border="1" align="center" bordercolor="#60A5DE">
                    <tr>
                        <td>                            
                            <table>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                 </tr>
                                <tr>              
                                    <td></td>                      
                                    <td colspan="2" class="sidelinks">
                                        Select the Excel file that contains fc ids
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:FileUpload ID="fileUpload" runat="server" BackColor="#EBEBE4" 
                                            CssClass="Text" Height="18px" onkeydown="return false;" 
                                            onkeypress="return false;" Width="400px" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>                                    
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td align="center">
                                        <asp:Button ID="btnProcess" runat="server" CssClass="MyButton" Text="Process" 
                                            onclick="btnProcess_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" CssClass="MyButton" Text="Cancel" 
                                            Width="70px" onclick="btnCancel_Click" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>            
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
