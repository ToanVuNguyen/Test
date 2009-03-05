<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Outcome.ascx.cs" Inherits="HPF.FutureState.Web.ForeclosureCaseDetail.Outcome" %>
<asp:BulletedList ID="errorList" runat="server" CssClass="ErrorMessage">    
</asp:BulletedList>
<asp:Label ID="Label1" runat="server" Text="Outcome List" CssClass="sidelinks"></asp:Label>
<table>
    <tr>
        <td>
            <asp:Panel ID="pnlOutcome" runat="server" CssClass="ScrollTable" BorderStyle="Inset" BorderColor="Gray" BorderWidth="1px" Width="820px">
                <asp:GridView ID="grdvOutcomeItems" runat="server" CellPadding="2" ForeColor="#333333"
                    GridLines="Vertical" AutoGenerateColumns="False" CssClass="GridViewStyle"                     
                    onrowcreated="grvForeClosureCaseSearch_RowCreated" 
                    onrowdatabound="grdvOutcomeItems_RowDataBound"                     
                    DataKeyNames="OutcomeItemId">
                    <RowStyle CssClass="RowStyle"  />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle CssClass="FixedHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"/>
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                    <Columns>   
                                              
                        <asp:TemplateField ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="lblOutcomeItemId" runat="server" Text='<%#Eval("OutcomeItemId") %>' Width="50px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>         
                        <asp:BoundField DataField="OutcomeTypeName" HeaderText="Outcome Type"  
                            ItemStyle-Width="200px">
                            <ItemStyle Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OutcomeDt" HeaderText="Outcome Date" 
                            DataFormatString="{0:MM/dd/yyyy}" ItemStyle-Width="100px">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:TemplateField ItemStyle-Width="200px" HeaderText="Delete Date">
                            <ItemTemplate>
                                <asp:Label ID="lblOutcomeDeletedDt" runat="server" Text='<%#Eval("OutcomeDeletedDt", "{0:MM/dd/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="200px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="NonprofitreferralKeyNum" 
                            HeaderText="NPR Referal ID"  ItemStyle-Width="100px">            
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ExtRefOtherName" HeaderText="Ext. Referal Name" 
                            ItemStyle-Width="200px">            
                            <ItemStyle Width="200px" />
                        </asp:BoundField>
                        <%--<asp:CommandField ShowSelectButton="true" ButtonType="Button" ControlStyle-CssClass="MyButton" ItemStyle-HorizontalAlign="Center" HeaderText="Select" />--%>
                    </Columns>
                    <EmptyDataTemplate>
                        There is no data match !
                    </EmptyDataTemplate>
                </asp:GridView>
            </asp:Panel>    
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass = "MyButton"
                onclick="btnDelete_Click" Visible = "false" />
            <asp:Button ID="btnReinstate" runat="server" Text="Reinstate" CssClass = "MyButton"
                onclick="btnReinstate_Click" Visible = "false" />
        </td>
        
    </tr>
</table>
    <script language="javascript" type="text/javascript">
       function ChangeData() {
        }
    </script>
