<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompareResult.ascx.cs" Inherits="HPF.FutureState.Web.QCSelectionCaseDetail.CompareResult" %>
<div class="Text">
    <asp:Label ID="lblErrorMessage" runat="server" CssClass="ErrorMessage"></asp:Label>
</div>
<table width="100%" border="1" style="border-collapse:collapse">
                <tr>
                    <td width="50%" class="sidelinks" align="left">Auditor </td>
                    <td width="10%" class="sidelinks" align="center">Agency</td>
                    <td width="15%" class="sidelinks" align="center">Agency Comment</td>
                    <td width="10%" class="sidelinks" align="center">HPF</td>
                    <td width="15%" class="sidelinks" align="center">HPF Comment</td>
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
                    <td style="color:Red" align="left"><b>Total Score:</b></td>
                    <td style="color:Red" align="center"><label style="color:Red" id="lblAgencyScore" runat="server"/>/<label style="color:Red" id="lblAgencyCasePossibleScore" runat="server"/>=<label style="color:Red" id="lblAgencyLevelPercent" runat="server"/></td>
                    <td></td>
                    <td style="color:Red" align="center"><label style="color:Red" id="lblHPFScore" runat="server"/>/<label style="color:Red" id="lblHPFCasePossibleScore" runat="server"/>=<label style="color:Red" id="lblHPFLevelPercent" runat="server"/></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="color:Red" align="left"><b>Result Level:</b></td>
                    <td style="color:Red" align="center"><label style="color:Red" id="lblAgencyLevel" runat="server"/></td>
                    <td></td>
                    <td style="color:Red" align="center"><label style="color:Red" id="lblHPFLevel" runat="server"/></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="color:Red" align="left"><b>Fatal Error for this case?</b></td>
                    <td align="center"><label style="color:Red" id="lblAgencyFatalError" runat="server"/></td>
                    <td></td>
                    <td align="center"><label style="color:Red" id="lblHPFFatalError" runat="server"/></td>
                    <td></td>
                </tr>
                <!-- List of questions -->
                <asp:PlaceHolder ID="placeHolder" runat="server"></asp:PlaceHolder>
                <!-- End List of questions -->
                <tr>
                <td colspan="5" align="center"><asp:Button ID='btnPrintReport' runat='server' 
                        Text='Print Report' Width='120px'
                CssClass='MyButton' onclick="btnPrintReport_Click" /></td>
                </tr>
 </table>

