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
            CssClass="MyButton" onclick="btnSelectQC_Click" Width="120px" />
        <asp:Button ID="btnRemoveQC" runat="server" Text="Remove From QC" 
            CssClass="MyButton" Width="120px"/>    
        </td>
    </tr>
    </table>
 </td></tr>
 </table>
 
 <script type="text/javascript" language="javascript">    
    function CancelClientClick()
    {
        Popup.showModal('modal');
            return false;
    }
    
</script>
 
 <div id="modal" style="border: 1px solid black;	background-color: #60A5DE;	padding: 1px;    text-align: center;     font-family: Verdana, Arial, Helvetica, sans-serif; display: none;">
        <div class="PopUpHeader">HPF Billing&amp;Admin</div>
        <table width="250" cellpadding="5">
        
            <tr>
                <td class="PopUpMessage">
                    Are you sure you want to remove this Foreclosure Case from QC?
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnYes" runat="server" OnClientClick="Popup.hide('modal');" 
                        CssClass="MyButton" Text="Yes" onclick="btnYes_Click" Width="70px" />
                    &nbsp;
                    <asp:Button ID="btnNo" runat="server" OnClientClick="Popup.hide('modal');return false;" CssClass="MyButton" Width="70px" Text="No" />
                </td>
            </tr>
        </table>        
    </div>