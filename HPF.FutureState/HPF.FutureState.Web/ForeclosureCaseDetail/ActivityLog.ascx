<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityLog.ascx.cs"
    Inherits="HPF.FutureState.Web.ForeclosureCaseDetail.ActivityLog" %>
<asp:Panel ID="pnlActivity" runat="server" CssClass="ScrollTable" Width="100%" BorderStyle="Inset"
    BorderColor="Gray" BorderWidth="0px" Height="337px">
    <asp:GridView ID="grdvActivityLogs" runat="server" CellPadding="2" ForeColor="#333333"
        GridLines="Vertical" AutoGenerateColumns="False" Width="100%" BorderStyle="None">
        <RowStyle CssClass="RowStyle" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
        <HeaderStyle CssClass="FixedHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White"
            HorizontalAlign="Left" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
        <Columns>
            <asp:BoundField DataField="ActivityCdDesc" HeaderText="Activity" ItemStyle-Width="170px">
                <ItemStyle Width="170px" />
            </asp:BoundField>
            <asp:BoundField DataField="ActivityDt" HeaderText="Activity Date" DataFormatString="{0:MM/dd/yyyy}"
                ItemStyle-Width="60px">
                <ItemStyle Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="CreateUserId" HeaderText="User" ItemStyle-Width="100px">
                <ItemStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="ActivityNote" HeaderText="Activity Note" ItemStyle-Width="265px">
                <ItemStyle Width="265px" />
            </asp:BoundField>
        </Columns>
        <EmptyDataTemplate>
            There is no Activity logs!
        </EmptyDataTemplate>
    </asp:GridView>

    <script language="javascript" type="text/javascript">
       function ChangeData() {
        }
    </script>

</asp:Panel>
