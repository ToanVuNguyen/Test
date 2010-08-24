<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgencyAudit.ascx.cs" Inherits="HPF.FutureState.Web.QCSelectionCaseDetail.AgencyAudit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<div class="Text">
    <asp:Label ID="lblErrorMessage" runat="server" CssClass="ErrorMessage"></asp:Label>
</div>
<table width="100%" border="1" style="border-collapse:collapse">
                <tr>
                    <td align="left">
                        <table>
                        <tr>
                            <td class="sidelinks">Auditor Name: </td>
                            <td><asp:Label ID="lblAuditorName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblEvaluationDate" runat="server" Text="Evaluation Date:" CssClass="sidelinks"/></td>
                            <td><asp:TextBox ID="txtEvaluationDate" runat="server" CssClass="Text"  MaxLength="100"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtEvaluationDate_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="txtEvaluationDate">
                    </cc1:CalendarExtender>
                            </td>
                        </tr>
                        </table>
                    </td>
                    <td class="sidelinks" align="center"></td>
                    <td class="sidelinks" align="center"></td>
                    <td class="sidelinks" align="center"></td>
                    <td class="sidelinks" align="center"></td>
                </tr>
                <!-- Scoring Section-->
                <tr style="background:#CDC9C9">
                    <td class="sidelinks" align="left">SCORING</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="color:Red" align="left"><b>Total Score</b></td>
                    <td align="center"><label style="color:Red" id="lblYesScore" runat="server"/></td>
                    <td align="center"><label style="color:Red" id="lblNoScore" runat="server"/></td>
                    <td align="center"><label style="color:Red" id="lblNAScore" runat="server"/></td>
                    <td align="center"></td>
                </tr>
                <tr>
                    <td style="color:Red" align="left"><b>Result Level</b></td>
                    <td align="center"><label style="color:Red" id="lblLevelPercent" runat="server"/></td>
                    <td style="color:Red" align="center"></td>
                    <td style="color:Red" align="center"></td>
                    <td align="right"><label style="color:Red" id="lblLevelName" runat="server"/></td>
                </tr>
                <tr>
                    <td style="color:Red" align="left"></td>
                    <td align="center" colspan="3" style="color:Red" class="Text">
                        <asp:CheckBox ID="chkFatalError" runat="server" />Fatal Error<br /><br />
                    </td>
                    <td align="right">
                    </td>
                </tr>
                <!-- End Scoring Section -->
                <tr>
                    <td align="left">
                    </td>
                    <td class="sidelinks" align="center">Y</td>
                    <td class="sidelinks" align="center">N</td>
                    <td class="sidelinks" align="center">N/A</td>
                    <td class="sidelinks" align="center">Comments</td>
                </tr>
                <!-- List of questions -->
                <asp:PlaceHolder ID="placeHolder" runat="server"></asp:PlaceHolder>
                <!-- End List of questions -->
                <tr>
                    <td style="color:Red" align="left"><b>Total</b></td>
                    <td align="center"><label style="color:Red" id="lblYesTotal" runat="server"/></td>
                    <td align="center"><label style="color:Red" id="lblNoTotal" runat="server"/></td>
                    <td align="center"><label style="color:Red" id="lblNATotal" runat="server"/></td>
                    <td align="center"></td>
                </tr>
                <tr>
                <td colspan="4" align="left" class="sidelinks">Reason Changed Comment</td>
                <td align="center">
                    <asp:TextBox runat="server" ID="txtComments" Columns="50" Rows="10" />
                </td>
                </tr>
                <tr><td colspan='5' align='right'>
                <asp:Button ID='btnCloseAudit' runat='server' Text='Close Audit' Width='120px'
                CssClass='MyButton' onclick="btnCloseAudit_Click"/>
                <asp:Button ID='btnCalculate' runat='server' Text='Calculate Score' Width='120px'
                CssClass='MyButton' onclick="btnCalculate_Click"/>
                <asp:Button ID='btnUpdate' runat='server' Text='Update' Width='120px' 
                        CssClass='MyButton' onclick="btnUpdate_Click"/>
                <asp:Button ID='btnSaveNew' runat='server' Text='Save New' Width='120px' 
                        CssClass='MyButton' onclick="btnSaveNew_Click"/>
                <asp:Button ID='btnClose' runat='server' Text='Close' Width='120px'
                CssClass='MyButton' onclick="btnClose_Click"/></td>
                </tr>
                </table>
                
                
