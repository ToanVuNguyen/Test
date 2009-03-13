<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Audit.ascx.cs" Inherits="HPF.FutureState.Web.ForeclosureCaseDetail.Audit" %>
<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="cc1" %>
    
    <br />
<asp:BulletedList ID="errorList" runat="server" CssClass="ErrorMessage">
</asp:BulletedList>
<table>
    <tr>
        <td colspan="2"  class="sidelinks">
            Audit List
            
        </td>
    </tr>
    <tr>
        <td>
                <asp:Panel ID="pnlAudit" runat="server" CssClass="ScrollTable" BorderStyle="Inset"
                BorderColor="Gray" BorderWidth="1px" Width="820px">
                        <asp:GridView ID="grdvCaseAudit" runat="server" CellPadding="2" ForeColor="#333333"
                            GridLines="Vertical" AutoGenerateColumns="False" CssClass="GridViewStyle"                             
                            DataKeyNames="CaseAuditId" 
                            OnRowCreated="grdvCaseAudit_RowCreated">
                            <RowStyle CssClass="RowStyle" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1"  ForeColor="#333333" />
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
                                <asp:BoundField DataField="AuditDt" HeaderText="Audit Date" 
                                    ItemStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AuditTypeCodeDesc" HeaderText="Audit Type" ItemStyle-Width="150px">
                                    <ItemStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ReviewedByName" HeaderText="Reviewed By" ItemStyle-Width="200px">
                                    <ItemStyle Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CompliantInd" HeaderText="Compliant" ItemStyle-Width="70px">
                                    <ItemStyle Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AuditFailureReasonCodeDesc" HeaderText="Audit Failure Reason"
                                    ItemStyle-Width="200px">
                                    <ItemStyle Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AppropriateOutcomeInd" HeaderText="Appropriate Outcome"
                                    ItemStyle-Width="200px">
                                    <ItemStyle Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ReasonForDefaultInd" HeaderText="Appropriate Reason for Default"
                                    ItemStyle-Width="200px">
                                    <ItemStyle Width="200px" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEdit" Text = "View/Edit" runat="server" CssClass="MyButton"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:ButtonField ButtonType="Button" CommandName="Select" ControlStyle-CssClass="MyButton" Text="View/Edit" />
                                                                              
                                    <asp:CommandField ShowSelectButton="true" SelectText="View/Edit" ButtonType="Button" ControlStyle-CssClass="MyButton"
                                        ItemStyle-HorizontalAlign="Center" HeaderText="Edit" />--%>
                                
                            </Columns>
                            <EmptyDataTemplate>
                                There is no data match !
                            </EmptyDataTemplate>
                        </asp:GridView>
                </asp:Panel>
        </td>
        <td valign="bottom">
            <asp:Button ID="btnNew" runat="server" Text="New" CssClass="MyButton" OnClick="btnNew_Click" Width = "70px"/>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <hr  style="color:#8FC4F6; border-style:solid; border-width:1px"/>
        </td>
    </tr>
    <tr align="center">
        <td>
            <table width="820px">
                <tr>
                    <td>
                        <asp:HiddenField ID="hfAction" runat="server" />
                    </td>
                    <td>
                        <asp:HiddenField ID="hfDoSaving" runat = "server"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">
                        <asp:Label ID="lblFormTitle" runat="server" Text="Audit Detail" class="sidelinks"></asp:Label>
                    </td>
                    <td align="right" class="sidelinks" valign="bottom">
                        Budget completed?:
                       <td align="left" valign="bottom">
                        <asp:DropDownList ID="ddlBudgetCompleted" runat="server" CssClass="Text" 
                            Width="150px"/>
                        
                    </td>
                </tr>
                <tr>
                    <td align="right" class="sidelinks">
                        Audit Date*:                     </td>
                    <td align="left">
                        <asp:TextBox ID="txtAuditDate" runat="server"  CssClass="Text" Width="150px"/>
                    </td>
                    <td align="right" class="sidelinks">
                        Appropriate Outcome:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlAppropriateOutcome" runat="server"  CssClass="Text" 
                            Width="150px"/>
                        
                    </td>
                </tr>
                <tr>
                    <td align="right" class="sidelinks">
                        Audit Type:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlAuditType" runat="server"  CssClass="Text" 
                            Height="16px" Width="150px"/>
                    </td>
                    <td align="right" class="sidelinks">
                        Appropriate Reason for Default:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlReasonForDefault" runat="server"  CssClass="Text" 
                            Width="150px"/>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="sidelinks">
                        Reviewed by:                     </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlReviewedBy" runat="server"  CssClass="Text" 
                            Width="150px"/>
                    </td>
                    <td align="right" class="sidelinks">
                        Client Action Plan?:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlClientActionPlan" runat="server"  CssClass="Text" 
                            Width="150px"/>
                        
                    </td>
                </tr>
                <tr>
                    <td align="right" class="sidelinks">
                        Compliant:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlCompliant" runat="server" CssClass="Text" 
                            Width="150px"/>
                      
                        
                    </td >
                    <td align="right" class="sidelinks">
                        Verbal Privacy Consent?:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlVerbalPrivacyConsent" runat="server" CssClass="Text" 
                            Width="150px"/>
                        
                    </td>
                </tr>
                <tr>
                    <td align="right" class="sidelinks">
                        Audit Failure Reason:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlAuditFailureReason" runat="server" CssClass="Text" 
                            Width="150px"/>
                        
                        
                    </td>
                    <td align="right"  class="sidelinks">
                        Written Privacy Consent?:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlWrittenPrivacyConsent" runat="server" CssClass="Text" 
                            Width="150px" />
                        
                    </td>
                </tr>
                <tr>
                    <td align="right"  class="sidelinks">
                        Audit Comments:                      <td align="left" colspan="3">
                        <asp:TextBox ID="txtAuditComment" runat="server" Rows="4" Columns="100" Height="65px" CssClass="Text"></asp:TextBox>
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="4">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="MyButton" OnClick="btnSave_Click" />
                        
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="MyButton" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<script type="text/javascript" language="javascript">
    //var msfWARN0450 = '<%= msgWARN0450 %>';
    var hfDoSaving = document.getElementById('<%=hfDoSaving.ClientID %>');
    var ddlAppropriateOutcome = document.getElementById('<%=ddlAppropriateOutcome.ClientID %>');
    var txtAuditComment  = document.getElementById('<%=txtAuditComment.ClientID %>'); 
    var txtAuditDate  = document.getElementById('<%=txtAuditDate.ClientID %>');
    var ddlAuditFailureReason = document.getElementById('<%=ddlAuditFailureReason.ClientID %>');
    var ddlAuditType  = document.getElementById('<%=ddlAuditType.ClientID %>');
    var ddlBudgetCompleted  = document.getElementById('<%=ddlBudgetCompleted.ClientID %>');
    var ddlClientActionPlan = document.getElementById('<%=ddlClientActionPlan.ClientID %>');
    var ddlCompliant = document.getElementById('<%=ddlCompliant.ClientID %>');
    var ddlReasonForDefault = document.getElementById('<%=ddlReasonForDefault.ClientID %>');
    var ddlReviewedBy = document.getElementById('<%=ddlReviewedBy.ClientID %>');
    var ddlVerbalPrivacyConsent = document.getElementById('<%=ddlVerbalPrivacyConsent.ClientID %>');
    var ddlWrittenPrivacyConsent = document.getElementById('<%=ddlWrittenPrivacyConsent.ClientID %>');
    
    
    var CaseAudit = function(appropriateOutcomeInd, auditComments, auditDt, auditFailureReasonCode, auditTypeCode, 
    budgetCompletedInd, clientActionPlanInd, compliantInd, reasonForDefaultInd, reviewedBy, verbalPrivacyConsentInd, writtenActionConsentInd)
    {                        
        this.AppropriateOutcomeInd = appropriateOutcomeInd
        this.AuditComments = auditComments
        this.AuditDt = auditDt
        this.AuditFailureReasonCode = auditFailureReasonCode
        this.AuditTypeCode = auditTypeCode
        this.BudgetCompletedInd = budgetCompletedInd
        this.ClientActionPlanInd = clientActionPlanInd
        this.CompliantInd = compliantInd       
        this.ReasonForDefaultInd = reasonForDefaultInd
        this.ReviewedBy = reviewedBy
        this.VerbalPrivacyConsentInd = verbalPrivacyConsentInd
        this.WrittenActionConsentInd = writtenActionConsentInd
    }
    
    var caseAuditBefore = new CaseAudit(ddlAppropriateOutcome.value, txtAuditComment.value, txtAuditDate.value
            , ddlAuditFailureReason.value, ddlAuditType.value, ddlBudgetCompleted.value, ddlClientActionPlan.value
            , ddlCompliant.value, ddlReasonForDefault.value, ddlReviewedBy.value
            , ddlVerbalPrivacyConsent.value, ddlWrittenPrivacyConsent.value);
    var caseAuditAfter = new CaseAudit();
    
    
    function ConfirmToSave(message)
    {   
        caseAuditAfter = new CaseAudit(ddlAppropriateOutcome.value, txtAuditComment.value, txtAuditDate.value
            , ddlAuditFailureReason.value, ddlAuditType.value, ddlBudgetCompleted.value, ddlClientActionPlan.value
            , ddlCompliant.value, ddlReasonForDefault.value, ddlReviewedBy.value, ddlVerbalPrivacyConsent.value, ddlWrittenPrivacyConsent.value);
        
        if(IsDifferent(caseAuditBefore, caseAuditAfter))
        {
            var b = confirm(message);
            if (b)
                hfDoSaving.value = "YES";       
            else
                hfDoSaving.value = "NO";
            return true;
        }
        
        hfDoSaving.value = "NO";    
        return true;
    }
    
    function IsDifferent(caseAuditBefore, caseAuditAfter)
    {
        if (caseAuditBefore.AuditDt != caseAuditAfter.AuditDt
            || caseAuditBefore.AuditTypeCode != caseAuditAfter.AuditTypeCode
            || caseAuditBefore.ReviewedBy != caseAuditAfter.ReviewedBy
            || caseAuditBefore.AuditFailureReasonCode != caseAuditAfter.AuditFailureReasonCode
            || caseAuditBefore.AuditComments != caseAuditAfter.AuditComments
            || caseAuditBefore.CompliantInd != caseAuditAfter.CompliantInd
            || caseAuditBefore.ReasonForDefaultInd != caseAuditAfter.ReasonForDefaultInd
            || caseAuditBefore.BudgetCompletedInd != caseAuditAfter.BudgetCompletedInd
            || caseAuditBefore.AppropriateOutcomeInd != caseAuditAfter.AppropriateOutcomeInd
            || caseAuditBefore.ClientActionPlanInd != caseAuditAfter.ClientActionPlanInd
            || caseAuditBefore.VerbalPrivacyConsentInd != caseAuditAfter.VerbalPrivacyConsentInd
            || caseAuditBefore.WrittenActionConsentInd != caseAuditAfter.WrittenActionConsentInd) 
                return true;
                
         return false;        
    }
    TabControl.onChanged = function() {
        ConfirmToSave("Your data is changed. Do you want to save it?");
    };
    function ChangeData()
    {
        ConfirmToSave("Your data is changed. Do you want to save it?");
    };    
</script>