<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompareResult.ascx.cs" Inherits="HPF.FutureState.Web.QCSelectionCaseDetail.CompareResult" %>
<div class="Text">
    <asp:Label ID="lblErrorMessage" runat="server" CssClass="ErrorMessage"></asp:Label>
</div>
<table width="100%" border="1" style="border-collapse:collapse">
                <tr>
                    <td class="sidelinks" align="left">Auditor </td>
                    <td class="sidelinks" align="center">Agency</td>
                    <td class="sidelinks" align="center">HPF</td>
                </tr>
                <!-- Scoring Section-->
                <tr style="background:gray">
                    <td class="sidelinks" align="left">SCORING</td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="color:Red" align="left"><b>Total Score</b></td>
                    <td style="color:Red" align="center"><label style="color:Red" id="lblAgencyScore" runat="server"/></td>
                    <td style="color:Red" align="center"><label style="color:Red" id="lblHPFScore" runat="server"/></td>
                </tr>
                <tr>
                    <td style="color:Red" align="left"><b>Level</b></td>
                    <td style="color:Red" align="center"><label style="color:Red" id="lblAgencyLevel" runat="server"/></td>
                    <td style="color:Red" align="center"><label style="color:Red" id="lblHPFLevel" runat="server"/></td>
                </tr>
                <tr>
                    <td style="color:Red" align="left"><b>Fatal Error</b></td>
                    <td align="center" class="sidelinks"><label style="color:Red" id="lblAgencyFatalError" runat="server"/></td>
                    <td align="center" class="sidelinks"><label style="color:Red" id="lblHPFFatalError" runat="server"/></td>
                </tr>
                <!-- List of questions -->
                <asp:PlaceHolder ID="placeHolder" runat="server"></asp:PlaceHolder>
                <!-- End List of questions -->
 </table>

