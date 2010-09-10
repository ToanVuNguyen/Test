<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QCReportUC.ascx.cs" Inherits="HPF.FutureState.Web.QCReport.QCReportUC" %>
<table width="100%">
    <tr>
        <td colspan="6" align="center" >
            <h1>
            Quality Control Report
            </h1>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Reporting Name*:</td>        
        <td  align="left" nowrap>
            <asp:DropDownList ID="ddlReportingName" runat="server" Height="16px" 
                CssClass="Text" Width="280px">
            </asp:DropDownList>
        </td>        
        <td  align="right" class="sidelinks" nowrap="nowrap">
            From Year/Month*:
        </td>
        <td>
            <asp:DropDownList ID="ddlYearMonthFrom" runat="server" Height="16px" 
                CssClass="Text" Width="80px">
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Button ID="btnGenerateReport" runat="server" Text="Generate Report" Width="120px"
                CssClass="MyButton" onclick="btnGenerateReport_Click"/></td>
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Agency*:</td>        
        <td  align="left" nowrap>
            <asp:DropDownList ID="ddlAgency" runat="server" Height="16px" 
                CssClass="Text" Width="280px">
            </asp:DropDownList>
        </td>  
        <td  align="right" class="sidelinks" nowrap="nowrap">
            To Year/Month*:
        </td>
        <td>
            <asp:DropDownList ID="ddlYearMonthTo" runat="server" Height="16px" 
                CssClass="Text" Width="80px">
            </asp:DropDownList>
        </td>        
        <td align="right"><asp:Button ID="btnClose" runat="server" Text="Close" Width="120px"
                CssClass="MyButton"/></td>
    </tr>
    <tr>
        <td class="sidelinks" align="right">
        Evaluation Type*:
        </td>
        <td width="280" >
        <asp:DropDownList ID="ddlEvaluationType" runat="server" Height="16px" 
                CssClass="Text" Width="280px">
            </asp:DropDownList>
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td colspan="6" class="ErrorMessage">
            <asp:BulletedList ID="lblErrorMessage" runat="server" CssClass="ErrorMessage">
            </asp:BulletedList>
        </td>
    </tr>
</table>