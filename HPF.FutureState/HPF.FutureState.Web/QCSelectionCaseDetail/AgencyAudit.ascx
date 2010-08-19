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
                    <td align="center"><label style="color:Red" id="lblYesScore" runat="server"/></td>
                    <td align="center"><label style="color:Red" id="lblNoScore" runat="server"/></td>
                    <td align="center"><label style="color:Red" id="lblNAScore" runat="server"/></td>
                    <td align="center"></td>
                </tr>
                <tr>
                    <td style="color:Red" align="left"><b>Level</b></td>
                    <td align="center"><label style="color:Red" id="lblLevelPercent" runat="server"/></td>
                    <td style="color:Red" align="center"></td>
                    <td style="color:Red" align="center"></td>
                    <td align="right"><label style="color:Red" id="lblLevelName" runat="server"/></td>
                </tr>
                <tr>
                    <td style="color:Red" align="left"><b>Fatal Error</b></td>
                    <td align="center" colspan="3">
                        <asp:CheckBox ID="chkFatalError" runat="server" />
                    </td>
                    <td style="color:Red" align="right"><b>Level Override:</b> <label style="color:Red" id="lblLevelNameOverride" runat="server"/></td>
                </tr>
                <!-- End Scoring Section -->
                <!-- List of questions -->
                <asp:PlaceHolder ID="placeHolder" runat="server"></asp:PlaceHolder>
                <!-- End List of questions -->
                <tr>
                <td colspan="4" align="left" class="sidelinks">Reason Changed Comment</td>
                <td align="center">
                    <asp:TextBox runat="server" ID="txtComments" Columns="50" Rows="10" />
                </td>
                </tr>
                <tr><td colspan='5' align='right'>
                <asp:Button ID='btnCalculate' runat='server' Text='Calculate Score' Width='120px'
                CssClass='MyButton' onclick="btnCalculate_Click"/>
                <asp:Button ID='btnSaveNew' runat='server' Text='Save New' Width='120px' 
                        CssClass='MyButton' onclick="btnSaveNew_Click"/>
                <asp:Button ID='btnClose' runat='server' Text='Close' Width='120px'
                CssClass='MyButton'/></td>
                </tr>
                </table>
                
                
