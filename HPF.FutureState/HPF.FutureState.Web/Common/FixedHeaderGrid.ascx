<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FixedHeaderGrid.ascx.cs" Inherits="HPF.FutureState.Web.BillingAdmin.FixedHeaderGrid" %>
<div style="overflow:auto; height:300px">
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Table ID="tbHeader" runat="server" CellPadding="0" CellSpacing="0">
                    
                </asp:Table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="grid_container" style="overflow:auto; height:200px" >
                    <asp:GridView  ID="gridData" runat="server" ShowHeader="False" CellPadding="0" CellSpacing="0">
                        
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
</div>
