<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgencyAudit.ascx.cs" Inherits="HPF.FutureState.Web.QCSelectionCaseDetail.AgencyAudit" %>
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
                            <td>Evaluation Date: </td>
                            <td><asp:TextBox ID="txtEvaluationDate" runat="server" CssClass="Text"  MaxLength="100"></asp:TextBox></td>
                        </tr>
                        </table>
                        Auditor Name: 
                    </td>
                    <td class="sidelinks" align="center">Y</td>
                    <td class="sidelinks" align="center">N</td>
                    <td class="sidelinks" align="center">N/A</td>
                    <td class="sidelinks" align="center">Comments</td>
                </tr>
                <!-- Scoring Section-->
                <tr style="background:gray">
                    <td class="sidelinks" align="left">SCORING</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="color:Red" align="left"><b>Totals</b></td>
                    <td style="color:Red" align="center">5</td>
                    <td style="color:Red" align="center">1</td>
                    <td style="color:Red" align="center">1</td>
                    <td align="center"></td>
                </tr>
                <tr>
                    <td style="color:Red" align="left"><b>Level</b></td>
                    <td style="color:Red" align="center">83%</td>
                    <td style="color:Red" align="center"></td>
                    <td style="color:Red" align="center"></td>
                    <td style="color:Red" align="right">Novice</td>
                </tr>
                <tr>
                    <td style="color:Red" align="left"><b>Fatal Error</b></td>
                    <td align="center" colspan="3">
                        
                        <asp:CheckBox ID="chkFatalError" runat="server" />
                        
                    </td>
                    <td style="color:Red" align="right"><b>Level Override:</b> Remediation</td>
                </tr>
                <!-- End Scoring Section -->
                <!-- List of questions -->
                <asp:Label ID="lblSectionQuestion" runat="server"></asp:Label>
                <!-- End List of questions -->
                <tr><td colspan='5' align='right'>
                <asp:Button ID='btnCalculate' runat='server' Text='Calculate Score' Width='120px'
                CssClass='MyButton'/>
                <asp:Button ID='btnSaveNew' runat='server' Text='Save New' Width='120px' 
                        CssClass='MyButton' onclick="btnSaveNew_Click"/>
                <asp:Button ID='btnClose' runat='server' Text='Close' Width='120px'
                CssClass='MyButton'/></td>
                </tr>
                </table>
                
                
