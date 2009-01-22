<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Budget.ascx.cs" Inherits="HPF.FutureState.Web.ForeclosureCaseDetail.Budget" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />
<div class="Text">
    <asp:Label ID="lblErrorMessage" runat="server" CssClass="ErrorMessage"></asp:Label>
</div>
<h1>Budget Set</h1>
<asp:ScriptManager runat="server"></asp:ScriptManager>
<asp:UpdatePanel runat="server">
<ContentTemplate>

<div style="border-bottom:solid 1 #8FC4F6">
    <asp:Panel ID="panForeClosureCaseSearch" runat="server" CssClass="ScrollTable"  
                 Width="100%" Height="90" Visible="true">
                <asp:GridView ID="grvBudgetSet" runat="server" CellPadding="2" ForeColor="#333333" DataKeyNames="BudgetSetId"
                    GridLines="Vertical" AutoGenerateColumns="false" CssClass="GridViewStyle" 
                    Width="100%"  SelectedRowStyle-BackColor="Yellow" 
                    onselectedindexchanged="grvBudgetSet_SelectedIndexChanged">
                    <RowStyle CssClass="RowStyle"  />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle CssClass="FixedHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                    <Columns>
                        <asp:BoundField DataField="BudgetSetDt" DataFormatString="{0:d}" HeaderText="Budget Date" />
                        <asp:BoundField DataField="TotalSurplus" DataFormatString="{0:C}" HeaderText="Surplus/Deficit" />
                        <asp:BoundField DataField="TotalIncome" DataFormatString="{0:C}" HeaderText="Total Income" />
                        <asp:BoundField DataField="TotalExpenses" DataFormatString="{0:C}" HeaderText="Total Expenses" />
                        <asp:BoundField DataField="TotalAssets" DataFormatString="{0:C}" HeaderText="Total Asset" />
                        <asp:CommandField ShowSelectButton="true" ButtonType="Button" ControlStyle-CssClass="MyButton" ItemStyle-HorizontalAlign="Center" HeaderText="Select" />
                    </Columns>
                </asp:GridView>
    </asp:Panel>
    &nbsp;
    &nbsp;
</div>
<h1>Budget Detail</h1>
<table style="width:100%;">
    <tr>
        <td style="vertical-align: top" >
            <h3>Income(s):
            </h3>
            <asp:DataList ID="lstIncomes" runat="server" 
                onitemcreated="lstIncomes_ItemCreated">
                <ItemTemplate>
                    <asp:Panel ID="Panel1" runat="server" Width = "600">
                            <asp:GridView ID="grvIncome" runat="server" CellPadding="2" ForeColor="#333333"
                            GridLines="Vertical" AutoGenerateColumns="false" CssClass="GridViewStyle" 
                            Width="100%"  SelectedRowStyle-BackColor="Yellow">
                            <RowStyle CssClass="RowStyle"  />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle CssClass="NormalHeader"  BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <Columns>
                                <asp:BoundField DataField="BudgetCategory" HeaderText="Budget Category" ItemStyle-Width="120"/>
                                <asp:BoundField DataField="BudgetSubCategory" HeaderText="Budget Subcategory" ItemStyle-Width="250"/>
                                <asp:BoundField DataField="BudgetItemAmt" DataFormatString="{0:C}" HeaderText="Amount" ItemStyle-Width="70"/>
                                <asp:BoundField DataField="BudgetNote" HeaderText="Note" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                    <br />
                    <br />
                </ItemTemplate>
            </asp:DataList>
        </td>
        <td rowspan="3" style="vertical-align: top">
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
                        <asp:BoundField DataField="AssetName" HeaderText="Asset Name" ItemStyle-Width="180" />
                        <asp:BoundField DataField="AssetValue" DataFormatString="{0:C}" HeaderText="Asset Value" ItemStyle-Width="120" />
                    </Columns>
                </asp:GridView>
    </asp:Panel>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top" >
                <h3>Expense(s):</h3>
            <asp:DataList ID="lstExpense" runat="server" onitemcreated="lstExpense_ItemCreated">
                <ItemTemplate>
                   <asp:Panel runat="server"  Width = "600">
                            <asp:GridView ID="grvExpense" runat="server" CellPadding="2" ForeColor="#333333"
                            GridLines="Vertical" AutoGenerateColumns="false" CssClass="GridViewStyle" 
                            Width="600"  SelectedRowStyle-BackColor="Yellow">
                            <RowStyle CssClass="RowStyle"  />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle CssClass="NormalHeader"  BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle"  />
                            <Columns>
                                <asp:BoundField DataField="BudgetCategory" HeaderText="Budget Category" ItemStyle-Width="120"/>
                                <asp:BoundField DataField="BudgetSubCategory" HeaderText="Budget Subcategory" ItemStyle-Width="250"/>
                                <asp:BoundField DataField="BudgetItemAmt" DataFormatString="{0:C}" HeaderText="Amount" ItemStyle-Width="70"/>
                                <asp:BoundField DataField="BudgetNote" HeaderText="Note" />
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
        <td  align="left">
            <h3>Totals:</h3>
        </td>
        </tr>
        <tr>
        <td align="center">
            <div style="width:100%;" >
            <table width="320" id="tbTotal" cellpadding="0" cellspacing="0" >
                <tr>
                    <td  class="NormalHeader"  >
                        Income Total</td>
                    <td>
                        <asp:Label runat="server" ID="lblIncomeTotal" CssClass="Text" Font-Bold="True"></asp:Label></td>
                </tr>
                <tr>
                    <td  class="NormalHeader">
                        Expense Total</td>
                    <td>
                        <asp:Label runat="server" ID="lblExpenseTotal" CssClass="Text" Font-Bold="True"></asp:Label></td>
                </tr>
                <tr>
                    <td class="NormalHeader">
                        Total Surplus or Deficit</td>
                    <td>
                        <asp:Label runat="server" ID="lblSurplusTotal"  CssClass="Text" 
                            Font-Bold="True"></asp:Label></td>
                </tr>
                </table>
                </div>
        </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>