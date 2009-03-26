<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaseFollowUp.ascx.cs" Inherits="HPF.FutureState.Web.ForeclosureCaseDetail.CaseFollowUp" %>
<%--<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />--%>
<asp:HiddenField ID="selRow" runat="server" />
<asp:HiddenField ID="hfAction" runat="server" />
<asp:BulletedList ID="errorList" runat="server" CssClass="ErrorMessage"></asp:BulletedList>
<table width="100%" id="tbl_main">
    <tr>
        <td colspan="4" class="sidelinks">
            <h3>Follow-Up List:</h3>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <table id="tbl_FollowUpList" width="100%">
                <tr>
                    <td align="left" valign="bottom" width="95%">
                    <!--Display Follow-Up list-->
                    <asp:Panel ID="pnlActivity" runat="server" CssClass="ScrollTable"  
                    BorderStyle="Inset" BorderColor="Gray" BorderWidth="1px" Width="100%">
                        <asp:GridView ID="grd_FollowUpList" runat="server" 
                        CellPadding="2" ForeColor="#333333"
                        GridLines="Vertical" AutoGenerateColumns="False"                         
                        DataKeyNames="CasePostCounselingStatusId"                        
                        onrowcreated="grd_FollowUpList_RowCreated" Width="100%" BorderStyle="None">
                            <RowStyle CssClass="RowStyle"  />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle  CssClass="SelectedRowStyle" />
                            <HeaderStyle CssClass="FixedHeader" BackColor="#507CD1" Font-Bold="True" 
                            ForeColor="White" HorizontalAlign="Center"/>
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <Columns>
                                <asp:BoundField DataField="CasePostCounselingStatusId" HeaderText="ID" />                                     
                                <asp:BoundField DataField="FollowUpDt" HeaderText="Follow-Up Date"  
                                DataFormatString="{0:MM/dd/yyyy}" ItemStyle-Width="13%" >
                                    <ItemStyle Width="13%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FollowUpSourceCdDesc" HeaderText="Source" 
                                ItemStyle-Width="37%"  >
                                    <ItemStyle Width="37%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OutcomeTypeName" 
                                HeaderText="Follow-Up Outcome"  ItemStyle-Width="20%"  >
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LoanDelinqStatusCdDesc" HeaderText="Delinquency Status" 
                                ItemStyle-Width="15%"  >
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>                                
                                <asp:TemplateField HeaderText = "View/Edit" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btn_Edit" Text="View/Edit" CommandName="Select" runat="server" CssClass="MyButton" />
                                    </ItemTemplate>
                                </asp:TemplateField>                              
                            </Columns>
                            <EmptyDataTemplate>
                                There is no Follow-Up!
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>                
                    <!---->
                    </td>
                    <td align="left" valign="bottom" width="5%">
                        <asp:Button ID="btn_New" runat="server" Text="  New  " CssClass="MyButton" 
                            onclick="btn_New_Click" TabIndex="1" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <hr  style="color:#8FC4F6; border-style:solid; border-width:1px"/>
        </td>
    </tr>
    <tr>
        <td colspan="4" class="sidelinks">
            Follow-Up Details:
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks" width="20%">Follow-Up Date*: </td>
        <td align="left">
            <asp:TextBox ID="txt_FollowUpDt" width="150px" runat="server" MaxLength="10" 
                CssClass="Text" TabIndex="2"></asp:TextBox>
        </td>
        <td align="right" class="sidelinks"  width="20%">Credit Score: </td>
        <td align="left">
            <asp:TextBox ID="txt_CreditScore" width="200px" runat="server" MaxLength="4" 
                CssClass="Text" TabIndex="7"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks" width="20%">Follow-Up Source*: </td>
        <td align="left"  width="30%">
            <asp:DropDownList ID="ddl_FollowUpSource" runat="server" CssClass="Text" 
                Width="300px" TabIndex="3">
            </asp:DropDownList>
        </td>
        <td align="right" class="sidelinks" width="20%">Credit Report Bureau: </td>
        <td align="left" width="30%">
            <asp:DropDownList ID="ddl_CreditReportBureau" runat="server" CssClass="Text" 
                Width="200px" TabIndex="8">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks" width="20%">Follow-Up Outcome: </td>
        <td align="left" width="30%">
            <asp:DropDownList ID="ddl_FollowUpOutcome" runat="server" CssClass="Text" 
                Width="350px" TabIndex="4">
            </asp:DropDownList>
        </td>
        <td align="right" class="sidelinks" width="20%">Credit Report Date: </td>
        <td align="left">
            <asp:TextBox ID="txt_CreditReportDt" runat="server" width="200px" 
                MaxLength="10" CssClass="Text" TabIndex="9"></asp:TextBox>
        </td>
    </tr>    
    <tr>
        <td align="right" class="sidelinks" width="20%">Delinquency Status: </td>
        <td align="left" width="30%">
            <asp:DropDownList ID="ddl_DelinqencyStatus" runat="server" CssClass="Text" 
                Width="150px" TabIndex="5">
            </asp:DropDownList>
        </td>
        <td align="right" class="sidelinks" width="20%">Follow-Up Comment: </td>        
        <td rowspan="2" width="30%">
            <asp:TextBox ID="txt_FollowUpComment" runat="server"  CssClass="Text"
                TextMode="MultiLine" MaxLength="8000" Width="200px" TabIndex="10"></asp:TextBox>
        </td>
    </tr>    
    <tr>
        <td align="right" class="sidelinks" width="20%">Still in Home?: </td>
        <td align="left" width="30%">
            <asp:DropDownList ID="ddl_StillInHome" runat="server" CssClass="Text" 
                Width="150px" TabIndex="6">
            </asp:DropDownList>
        </td>        
        <td>&nbsp;</td>        
    </tr>  
    <tr>
        <td colspan="2" align="right">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="MyButton" 
                onclick="btn_Save_Click" TabIndex="11" Width="50px" />
        </td>
        <td colspan="2" align="left">
            <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CssClass="MyButton" 
                onclick="btn_Cancel_Click" TabIndex="12" Width="50px" />
        </td>
    </tr>  
</table>
<asp:HiddenField ID="selTabCtrl"  runat="server"  Value=""/>

<script type="text/javascript" language="javascript">
    var msfWARN0450 = '<%= msgWARN0450 %>';    
    var selRow = document.getElementById('<%=selRow.ClientID %>');
    var confirmFlag = true;
    var followUpDate = document.getElementById('<%=txt_FollowUpDt.ClientID %>');
    var creditScore = document.getElementById('<%=txt_CreditScore.ClientID %>');
    var followUpSource = document.getElementById('<%=ddl_FollowUpSource.ClientID %>');
    var creditReportBureau = document.getElementById('<%=ddl_CreditReportBureau.ClientID %>');        
    var followUpOutcome = document.getElementById('<%=ddl_FollowUpOutcome.ClientID %>');        
    var creditReportDt = document.getElementById('<%=txt_CreditReportDt.ClientID %>');        
    var delinqencyStatus = document.getElementById('<%=ddl_DelinqencyStatus.ClientID %>');
    var followUpComment = document.getElementById('<%=txt_FollowUpComment.ClientID %>');
    var stillInHome = document.getElementById('<%=ddl_StillInHome.ClientID %>');
    var CaseFollowUp = function(followUpDate, creditScore, followUpSource, creditReportBureau, followUpOutcome, 
    creditReportDt, delinqencyStatus, followUpComment, stillInHome)
    {        
        this.FollowUpDate = followUpDate;
        this.CreditScore = creditScore;
        this.FollowUpSource = followUpSource;        
        this.CreditReportBureau = creditReportBureau;
        this.FollowUpOutcome = followUpOutcome;
        this.CreditReportDt = creditReportDt;
        this.DelinqencyStatus = delinqencyStatus;
        this.FollowUpComment = followUpComment;
        this.StillInHome = stillInHome;
    }
    
    var tempTabId = document.getElementById('<%=selTabCtrl.ClientID%>');
    if(tempTabId.value!='')
    {
        tabid = tempTabId.value;
        tempTabId.value='';
        TabControl.SelectTab(tabid);
    }
    
    var caseFollowUpBefore = new CaseFollowUp(followUpDate.value, creditScore.value, followUpSource.value
            , creditReportBureau.value, followUpOutcome.value, creditReportDt.value, delinqencyStatus.value, followUpComment.value, stillInHome.value);
    var caseFollowUpAfter = new CaseFollowUp();
        
    function CompareCaseFollowUpObject(caseFollowUpAfter)
    {
        if(caseFollowUpAfter.FollowUpDate != caseFollowUpBefore.FollowUpDate
            ||  caseFollowUpAfter.FollowUpDate != caseFollowUpBefore.FollowUpDate
            ||  caseFollowUpAfter.CreditScore != caseFollowUpBefore.CreditScore
            ||  caseFollowUpAfter.FollowUpSource != caseFollowUpBefore.FollowUpSource  
            ||  caseFollowUpAfter.CreditReportBureau != caseFollowUpBefore.CreditReportBureau
            ||  caseFollowUpAfter.FollowUpOutcome != caseFollowUpBefore.FollowUpOutcome
            ||  caseFollowUpAfter.CreditReportDt != caseFollowUpBefore.CreditReportDt
            ||  caseFollowUpAfter.DelinqencyStatus != caseFollowUpBefore.DelinqencyStatus
            ||  caseFollowUpAfter.FollowUpComment != caseFollowUpBefore.FollowUpComment
            ||  caseFollowUpAfter.StillInHome != caseFollowUpBefore.StillInHome
        )
            return true;
        else
            return false;
    }
    
    function ClearAllControl()
    {
        followUpDate.value = "";
        creditScore.value = "";
        followUpSource.value = "";
        creditReportBureau.value = "";
        followUpOutcome.value = "";
        creditReportDt.value = "";
        delinqencyStatus.value = "";
        followUpComment.value = "";
        stillInHome.value = "";
        CaseFollowUp.value = "";     
        confirmFlag = false;   
    }
    
    function ConfirmEdit(message, selDataRow)
    {        
        if(selDataRow != "")
            selRow.value = selDataRow;
        
        caseFollowUpAfter = new CaseFollowUp(followUpDate.value, creditScore.value, followUpSource.value
            , creditReportBureau.value, followUpOutcome.value, creditReportDt.value, delinqencyStatus.value, followUpComment.value, stillInHome.value);        
        
        
        if(CompareCaseFollowUpObject(caseFollowUpAfter))
        {                      
            Popup.showModal('mdgCaseFollowup');             
            return false;                            
        } 
                
        return true;      
    }
    
    TabControl.onChanged= function ChangeTabData(toTabId)
    {
        tempTabId.value = toTabId;
        return ConfirmEdit(msfWARN0450, "");
    };
        
    function ChangeData()
    {
      //return ConfirmEdit(msfWARN0450, "");
    }
        
</script>

<div id="mdgCaseFollowup" style="border: 1px solid black;	background-color: #60A5DE;	padding: 1px;    text-align: center;     font-family: Verdana, Arial, Helvetica, sans-serif; display: none;">
        <div class="PopUpHeader">HPF Billing&amp;Admin</div>
        <table width="250" cellpadding="5">
        
            <tr>
                <td class="PopUpMessage">
                    <%=msgWARN0450%>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnYes" runat="server" OnClientClick="Popup.hide('mdgCaseFollowup');" 
                        CssClass="MyButton" Text="Yes" onclick="btnYes_Click" Width="70px" />
                    &nbsp;
                    <asp:Button ID="btnNo" runat="server" onclick="btnNo_Click" OnClientClick="Popup.hide('mdgCaseFollowup');TabControl.SelectTab(tempTabId.value)" CssClass="MyButton" Width="70px" Text="No" />
                </td>
            </tr>
        </table>        
    </div>