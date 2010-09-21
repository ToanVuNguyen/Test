<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SendSummaryToServicer.ascx.cs" Inherits="HPF.FutureState.Web.SendSummaryToServicer.SendSummaryToServicer" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:ScriptManager runat="server" ID="ScriptManager1">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
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
            <h3>Send Summaries to Servicer</h3>
            <asp:Panel ID="pnlContent" runat="server">
                <table cellpadding="0" cellspacing="0" border="1" align="center" bordercolor="#60A5DE">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:RadioButton ID="rbtnSendBasedDateRange" runat="server" Checked="True" 
                                            GroupName="SendType" oncheckedchanged="rbtnSendBasedDateRange_CheckedChanged" AutoPostBack="true" />
                                        Choose Date Range</td>
                                </tr>
                                <tr>
                                    <td width="80" class="sidelinks" align="right">
                                        Servicer:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlServicer" runat="server" CssClass="Text" TabIndex="12">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                            <tr>
                                    <td class="sidelinks" align="right">
                                        Start Date:</td>
                                    <td>
                                        <asp:TextBox ID="txtPeriodStart" runat="server" CssClass="Text" Width="120px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtPeriodStart_CalendarExtender" runat="server" 
                                            Enabled="True" TargetControlID="txtPeriodStart"></cc1:CalendarExtender>
                                    </td>
                                </tr>
                                 <tr>
                                    <td class="sidelinks" align="right">
                                        End Date:</td>
                                    <td>
                                        <asp:TextBox ID="txtPeriodEnd" runat="server" CssClass="Text" Width="120px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtPeriodEnd_CalendarExtender" runat="server" 
                                            Enabled="True" TargetControlID="txtPeriodEnd"></cc1:CalendarExtender>
                                    </td>
                                 </tr>
                                 <tr>
                                    <td></td><td></td>
                                 </tr>
                                 
                                    
                            </table>
                        </td>
                        <td>                            
                            <table>
                                <tr>
                                    <td colspan="3"></td>
                                 </tr>
                                 <tr>
                                    <td></td> <td colspan="2">
                                     <asp:RadioButton ID="rbtnSendBasedFiled" runat="server" GroupName="SendType" 
                                         oncheckedchanged="rbtnSendBasedFiled_CheckedChanged" AutoPostBack="true" />
                                     Choose from file</td>
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
                                    <td colspan="3">&nbsp;</td>
                                </tr>
                                <tr>                                    
                                    <td colspan="3">&nbsp;</td>
                                </tr>
                            </table>
                        </td>                        
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <table>
                                <tr><td></td></tr>
                                <tr>
                                <td>
                                    <asp:Button ID="btnSend" runat="server" CssClass="MyButton" Text="Send" 
                                        Width="70px" onclick="btnSend_Click"/>&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" CssClass="MyButton" Text="Cancel" 
                                        Width="70px" onclick="btnCancel_Click" />
                                    </td>
                                 </tr>
                                 <tr><td></td></tr>
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
</ContentTemplate>
<Triggers>
     <asp:PostBackTrigger ControlID="btnSend" />                   
</Triggers>
</asp:UpdatePanel>
