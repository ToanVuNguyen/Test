<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QCSelection.ascx.cs" Inherits="HPF.FutureState.Web.ForeclosureCaseDetail.QCSelection" %>
<div class="Text">
    <asp:Label ID="lblErrorMessage" runat="server" CssClass="ErrorMessage"></asp:Label>
</div>
  <table width="100%" border="0">
  <tr align="center"><td>
    <table width="50%" border="1" style="border-collapse:collapse">
    <tr>
        <td class="sidelinks">QC Template Name</td>
        <td>
            <asp:DropDownList ID="ddlEvalTemplate" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="sidelinks">Evaluation Type</td>
        <td class="Text">
            
            <asp:RadioButton ID="rbtnDesktop" runat="server" Checked="True" 
                GroupName="EvaluationType" />
                <asp:Label runat="server" ID="lblDesktop" Text="Desktop"/>
            <asp:RadioButton ID="rbtnOnSite" runat="server" GroupName="EvaluationType" />
                <asp:Label runat="server" ID="lblOnSite" Text="OnSite"/>
            
        </td>
    </tr>
    <tr>
    <td align="center" colspan="2">
        <asp:Button ID="btnSelectQC" runat="server" Text="Select For QC" 
            CssClass="MyButton" onclick="btnSelectQC_Click" />
        </td>
    </tr>
    </table>
 </td></tr>
 </table>