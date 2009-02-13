<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Audit.ascx.cs" Inherits="HPF.FutureState.Web.ForeclosureCaseDetail.Audit" %>
<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="cc1" %>
<asp:BulletedList ID="errorList" runat="server" CssClass="ErrorMessage">
</asp:BulletedList>
<table>
    <tr>
        <td colspan="2">
            <asp:Label ID="Label1" runat="server" Text="Audit List" CssClass="sidelinks"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
                <asp:Panel ID="pnlAudit" runat="server" CssClass="ScrollTable" BorderStyle="Inset"
                BorderColor="Gray" BorderWidth="1px" Width="820px">
                        <asp:GridView ID="grdvCaseAudit" runat="server" CellPadding="2" ForeColor="#333333"
                            GridLines="Vertical" AutoGenerateColumns="False" CssClass="GridViewStyle" SelectedRowStyle-BackColor="Yellow"
                            DataKeyNames="CaseAuditId" OnRowCommand="grdvCaseAudit_RowCommand" OnRowCreated="grdvCaseAudit_RowCreated"
                            OnRowDataBound="grdvCaseAudit_RowDataBound" 
                            onselectedindexchanged="grdvCaseAudit_SelectedIndexChanged"
                            >
                            <RowStyle CssClass="RowStyle" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle CssClass="FixedHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White"
                                HorizontalAlign="Left" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCaseAuditId" runat="server" Text='<%#Eval("CaseAuditId") %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="AuditDt" HeaderText="Audit Date" DataFormatString="{0:MM/dd/yyyy}"
                                    ItemStyle-Width="100px">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AuditTypeCode" HeaderText="Audit Type" ItemStyle-Width="150px">
                                    <ItemStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ReviewedBy" HeaderText="Reviewed By" ItemStyle-Width="200px">
                                    <ItemStyle Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CompliantInd" HeaderText="Compliant" ItemStyle-Width="70px">
                                    <ItemStyle Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AuditFailureReasonCode" HeaderText="Audit Failure Reason"
                                    ItemStyle-Width="200px">
                                    <ItemStyle Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AppropriateOutcomeInd" HeaderText="Appr. Outcome Ind"
                                    ItemStyle-Width="200px">
                                    <ItemStyle Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ReasonForDefaultInd" HeaderText="Reason For Dflt. Ind"
                                    ItemStyle-Width="200px">
                                    <ItemStyle Width="200px" />
                                </asp:BoundField>
                                <asp:CommandField ShowSelectButton="true" SelectText="Edit" ButtonType="Button" ControlStyle-CssClass="MyButton"
                                    ItemStyle-HorizontalAlign="Center" HeaderText="Edit" />
                            </Columns>
                            <EmptyDataTemplate>
                                There is no data match !
                            </EmptyDataTemplate>
                        </asp:GridView>
                </asp:Panel>
        </td>
        <td valign="bottom">
            <asp:Button ID="btnNew" runat="server" Text="New" CssClass="MyButton" OnClick="btnNew_Click" />
        </td>
    </tr>
    <tr align="center">
        <td>
            <table>
                <tr>
                    <td>
                        <asp:HiddenField ID="hfAction" runat="server" />
                    </td>
                    <td>
                        <asp:HiddenField ID="hfWarn0450" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblFormTitle" runat="server" Text="Audit Detail" CssClass="sidelinks"></asp:Label>
                    </td>
                    <td align="right" class="sidelinks">
                        Budget completed:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlBudgetCompleted" runat="server" CssClass="Text"/>
                        
                    </td>
                </tr>
                <tr>
                    <td align="right" class="sidelinks">
                        Audit Date:
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtAuditDate" runat="server"  CssClass="Text"/>
                    </td>
                    <td align="right" class="sidelinks">
                        Appropriate Outcome:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlAppropriateOutcome" runat="server"  CssClass="Text"/>
                        
                    </td>
                </tr>
                <tr>
                    <td align="right" class="sidelinks">
                        Audit Type:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlAuditType" runat="server"  CssClass="Text"/>
                    </td>
                    <td align="right" class="sidelinks">
                        Appropriate Reason for Default:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlReasonForDefault" runat="server"  CssClass="Text"/>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="sidelinks">
                        Review by:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlReviewedBy" runat="server"  CssClass="Text"/>
                    </td>
                    <td align="right" class="sidelinks">
                        Client Action Plan:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlClientActionPlan" runat="server"  CssClass="Text"/>
                        
                    </td>
                </tr>
                <tr>
                    <td align="right" class="sidelinks">
                        Compliant:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlCompliant" runat="server" CssClass="Text"/>
                      
                        
                    </td >
                    <td align="right" class="sidelinks">
                        Verbal Privacy Consent:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlVerbalPrivacyConsent" runat="server" CssClass="Text"/>
                        
                    </td>
                </tr>
                <tr>
                    <td align="right" class="sidelinks">
                        Audit Failure Reason:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlAuditFailureReason" runat="server" CssClass="Text"/>
                        
                        
                    </td>
                    <td align="right"  class="sidelinks">
                        Written Privacy Consent:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlWrittenPrivacyConsent" runat="server" CssClass="Text" />
                        
                    </td>
                </tr>
                <tr>
                    <td align="right"  class="sidelinks">
                        Audit Comment:
                    </td>
                    <td align="left" colspan="3">
                        <asp:TextBox ID="txtAuditComment" runat="server" Rows="4" Columns="80" Height="65px" CssClass="Text"></asp:TextBox>
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="4">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="MyButton" OnClick="btnSave_Click" />
                        <%--<span onunload = "return confirm(document.getElementById('hfWarn0450').value);">                            
                    </span>--%>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="MyButton" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
