<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Budget.ascx.cs" Inherits="HPF.FutureState.Web.ForeclosureCaseDetail.Budget" %>
<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="cc1" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />
<div class="Text">
    <asp:Label ID="lblErrorMessage" runat="server" CssClass="ErrorMessage"></asp:Label>
</div>
<h1>
    Budget Set</h1>
<asp:ScriptManager runat="server">
</asp:ScriptManager>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <div style="border-bottom: solid 1 #8FC4F6">
            <asp:Panel ID="panForeClosureCaseSearch" runat="server" CssClass="ScrollTable" Width="100%"
                Height="110" Visible="true">
                <asp:GridView ID="grvBudgetSet" runat="server" CellPadding="2" ForeColor="#333333"
                    DataKeyNames="BudgetSetId" GridLines="Vertical" AutoGenerateColumns="false" CssClass="GridViewStyle"
                    Width="100%" OnSelectedIndexChanged="grvBudgetSet_SelectedIndexChanged" OnRowDataBound="grvBudgetSet_RowDataBound">
                    <RowStyle CssClass="RowStyle" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                    <HeaderStyle CssClass="FixedHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                    <Columns>
                        <asp:BoundField DataField="BudgetSetDt" DataFormatString="{0:d}" HeaderText="Budget Date" />
                        <asp:TemplateField HeaderText="Surplus/Deficit">
                            <ItemTemplate>
                                <asp:Label ID="lblSurplus" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="TotalIncome" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"
                            HeaderText="Total Income" />
                        <asp:BoundField DataField="TotalExpenses" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"
                            HeaderText="Total Expenses" />
                        <asp:BoundField DataField="TotalAssets" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"
                            HeaderText="Total Asset" />
                        <asp:CommandField ShowSelectButton="true" ButtonType="Button" ControlStyle-CssClass="MyButton"
                            ItemStyle-HorizontalAlign="Center" HeaderText="Select" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
            &nbsp; &nbsp;
        </div>
        <h1>
            Budget Detail</h1>
        <table style="width: 100%;">
            <tr>
                <td align="left">
                    <h3>
                        Income(s):
                    </h3>
                </td>
                <td align="left">
                    <h3>
                        Asset(s):
                    </h3>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top" align="right">
                    <asp:DataList ID="lstIncomes" runat="server" OnItemCreated="lstIncomes_ItemCreated">
                        <ItemTemplate>
                            <asp:Panel ID="Panel1" runat="server" Width="600">
                                <asp:GridView ID="grvIncome" runat="server" CellPadding="2" ForeColor="#333333" GridLines="Vertical"
                                    AutoGenerateColumns="false" CssClass="GridViewStyle" Width="100%">
                                    <RowStyle CssClass="RowStyle" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                                    <HeaderStyle CssClass="NormalHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="BudgetCategory" HeaderText="Budget Category" ItemStyle-Width="120" />
                                        <asp:BoundField DataField="BudgetSubCategory" HeaderText="Budget Subcategory" ItemStyle-Width="200" />
                                        <asp:BoundField DataField="BudgetItemAmt" DataFormatString="{0:C}"  ItemStyle-HorizontalAlign="Right"
                                            HeaderText="Amount"  />
                                        <asp:BoundField DataField="BudgetNote" HeaderText="Note" ItemStyle-Width="130"  />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                            <br />
                            <br />
                        </ItemTemplate>
                    </asp:DataList>
                </td>
                <td rowspan="3" style="vertical-align: top" align="left">
                    <asp:Panel ID="panelAsset" runat="server" CssClass="ScrollTable" Width="300" Height="90"
                        Visible="true">
                        <asp:GridView ID="grvAsset" runat="server" CellPadding="2" ForeColor="#333333" GridLines="Vertical"
                            AutoGenerateColumns="false" CssClass="GridViewStyle" Width="100%">
                            <RowStyle CssClass="RowStyle" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                            <HeaderStyle CssClass="FixedHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                            <Columns>
                                <asp:BoundField DataField="AssetName" HeaderText="Asset Name" ItemStyle-Width="180" />
                                <asp:BoundField DataField="AssetValue" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"
                                    HeaderText="Asset Value" ItemStyle-Width="120" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <h3>
                        Expense(s):
                    </h3>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top" align="right">
                    <asp:DataList ID="lstExpense" runat="server" OnItemCreated="lstExpense_ItemCreated">
                        <ItemTemplate>
                            <asp:Panel runat="server" Width="600">
                                <asp:GridView ID="grvExpense" runat="server" CellPadding="2" ForeColor="#333333"
                                    GridLines="Vertical" AutoGenerateColumns="false" CssClass="GridViewStyle" Width="600">
                                    <RowStyle CssClass="RowStyle" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                                    <HeaderStyle CssClass="NormalHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="BudgetCategory" HeaderText="Budget Category" ItemStyle-Width="120" />
                                        <asp:BoundField DataField="BudgetSubCategory" HeaderText="Budget Subcategory" ItemStyle-Width="200" />
                                        <asp:BoundField DataField="BudgetItemAmt" DataFormatString="{0:C}"  ItemStyle-HorizontalAlign="Right"
                                            HeaderText="Amount"  />
                                        <asp:BoundField DataField="BudgetNote" HeaderText="Note" ItemStyle-Width="130"  />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                            <br />
                            <br />
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <h3>
                        Totals:</h3>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <div style="width: 100%;">
                        <table width="320" id="tbTotal" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="NormalHeader">
                                    Income Total
                                </td>
                                <td align="right">
                                    <asp:Label runat="server" ID="lblIncomeTotal" CssClass="Text" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="NormalHeader">
                                    Expense Total
                                </td>
                                <td align="right">
                                    <asp:Label runat="server" ID="lblExpenseTotal" CssClass="Text" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="NormalHeader">
                                    Total Surplus or Deficit
                                </td>
                                <td align="right">
                                    <asp:Label runat="server" ID="lblSurplusTotal" CssClass="Text" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<script language="javascript" type="text/javascript">
    function ChangeData()
    { };
</script>
