<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Budget.ascx.cs" Inherits="HPF.FutureState.Web.ForeclosureCaseDetail.Budget" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />
<h1>Budget Set</h1>
<div style="border-bottom:solid 1 #8FC4F6">
    <asp:Panel ID="panForeClosureCaseSearch" runat="server" CssClass="ScrollTable"  
                 Width="800" Height="90" Visible="true">
                <asp:GridView ID="grvBudgetSet" runat="server" CellPadding="2" ForeColor="#333333"
                    GridLines="Vertical" AutoGenerateColumns="false" CssClass="GridViewStyle" 
                    Width="100%"  SelectedRowStyle-BackColor="Yellow">
                    <RowStyle CssClass="RowStyle"  />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle CssClass="FixedHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle"  />
                    <Columns>
                        <asp:BoundField DataField="BudgetSetDt" HeaderText="Budget Date" />
                        <asp:BoundField DataField="TotalSurplus" DataFormatString="{0:C}" HeaderText="Surplus/Deficit" />
                        <asp:BoundField DataField="TotalIncome" DataFormatString="{0:C}" HeaderText="Total Income" />
                        <asp:BoundField DataField="TotalExpenses" DataFormatString="{0:C}" HeaderText="Total Expenses" />
                        <asp:BoundField DataField="TotalAssets" DataFormatString="{0:C}" HeaderText="Total Asset" />
                    </Columns>
                    <EmptyDataTemplate>
                    There is no data match !
                    </EmptyDataTemplate>
                </asp:GridView>
    </asp:Panel>
    &nbsp;
    &nbsp;
</div>
<h1>Budget Detail</h1>
<table style="width:100%;">
    <tr>
        <td style="vertical-align: top">
            <h3>Income(s):
            </h3>
            <asp:DataList ID="lstIncomes" runat="server" 
                onitemcreated="lstIncomes_ItemCreated">
                <ItemTemplate>
                        <asp:Panel ID="panelIncome" runat="server" CssClass="ScrollTable"  
                     Width="400" Height="90" Visible="true">
                            <asp:GridView ID="grvIncome" runat="server" CellPadding="2" ForeColor="#333333"
                            GridLines="Vertical" AutoGenerateColumns="false" CssClass="GridViewStyle" 
                            Width="100%"  SelectedRowStyle-BackColor="Yellow">
                            <RowStyle CssClass="RowStyle"  />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle CssClass="FixedHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle"  />
                            <Columns>
                                <asp:BoundField DataField="BudgetCategory" HeaderText="Budget Category" />
                                <asp:BoundField DataField="BudgetSubCategory" HeaderText="Budget Subcategory" />
                                <asp:BoundField DataField="Amount" DataFormatString="{0:C}" HeaderText="Amount" />
                                <asp:BoundField DataField="Note" HeaderText="Note" />
                            </Columns>
                            <EmptyDataTemplate>
                            There is no data match !
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td rowspan="2" style="vertical-align: top">
            <h3>Asset(s):</h3>
            <asp:Panel ID="panelAsset" runat="server" CssClass="ScrollTable"  
                 Width="300" Height="90" Visible="true">
                <asp:GridView ID="grvAsset" runat="server" CellPadding="2" ForeColor="#333333"
                    GridLines="Vertical" AutoGenerateColumns="false" CssClass="GridViewStyle" 
                    Width="100%"  SelectedRowStyle-BackColor="Yellow">
                    <RowStyle CssClass="RowStyle"  />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle CssClass="FixedHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle"  />
                    <Columns>
                        <asp:BoundField DataField="AssetName" HeaderText="Asset Name" />
                        <asp:BoundField DataField="AssetValue" DataFormatString="{0:C}" HeaderText="Asset Value" />
                    </Columns>
                    <EmptyDataTemplate>
                    There is no data match !
                    </EmptyDataTemplate>
                </asp:GridView>
    </asp:Panel>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top">
                <h3>Expense(s):</h3>
            <asp:DataList ID="lstExpense" runat="server">
                <ItemTemplate>
                        <asp:Panel ID="panelExpense" runat="server" CssClass="ScrollTable"  
                     Width="400" Height="90" Visible="true">
                            <asp:GridView ID="grvExpense" runat="server" CellPadding="2" ForeColor="#333333"
                            GridLines="Vertical" AutoGenerateColumns="false" CssClass="GridViewStyle" 
                            Width="100%"  SelectedRowStyle-BackColor="Yellow">
                            <RowStyle CssClass="RowStyle"  />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle CssClass="FixedHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle"  />
                            <Columns>
                                <asp:BoundField DataField="BudgetCategory" HeaderText="Budget Category" />
                                <asp:BoundField DataField="BudgetSubCategory" HeaderText="Budget Subcategory" />
                                <asp:BoundField DataField="Amount" DataFormatString="{0:C}" HeaderText="Amount" />
                                <asp:BoundField DataField="Note" HeaderText="Note" />
                            </Columns>
                            <EmptyDataTemplate>
                            There is no data match !
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </ItemTemplate>
            </asp:DataList>
        </td>
    </tr>
</table>
