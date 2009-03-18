<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaseFollowUp.ascx.cs" Inherits="HPF.FutureState.Web.ForeclosureCaseDetail.CaseFollowUp" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />
<asp:HiddenField ID="hdf_Confirm" runat="server" />
<asp:HiddenField ID="hfAction" runat="server" />
<br />
<asp:BulletedList ID="errorList" runat="server" CssClass="ErrorMessage"></asp:BulletedList>
<table width="100%" id="tbl_main">
    <tr>
        <td colspan="4" class="sidelinks">
            Follow-Up List:
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <table id="tbl_FollowUpList" width="100%">
                <tr>
                    <td align="left" valign="bottom" width="95%">
                    <!--Display Follow-Up list-->
                    <asp:Panel ID="pnlActivity" runat="server" CssClass="ScrollTable"  
                    BorderStyle="Inset" BorderColor="Gray" BorderWidth="0px" Height="100%" Width="100%">
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
                                DataFormatString="{0:MM/dd/yyyy}" ItemStyle-Width="13%" ItemStyle-CssClass="Text">
                                    <ItemStyle Width="13%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FollowUpSourceCdDesc" HeaderText="Source" 
                                ItemStyle-Width="37%"  ItemStyle-CssClass="Text">
                                    <ItemStyle Width="37%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OutcomeTypeName" 
                                HeaderText="Follow-Up Outcome"  ItemStyle-Width="20%"  ItemStyle-CssClass="Text">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LoanDelinqStatusCdDesc" HeaderText="Delinquency Status" 
                                ItemStyle-Width="15%"  ItemStyle-CssClass="Text">
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
            <asp:TextBox ID="txt_FollowUpDt" width="100px" runat="server" MaxLength="10" 
                CssClass="Text" TabIndex="2"></asp:TextBox>
        </td>
        <td align="right" class="sidelinks"  width="20%">Credit Score: </td>
        <td align="left">
            <asp:TextBox ID="txt_CreditScore" width="100px" runat="server" MaxLength="4" 
                CssClass="Text" TabIndex="7"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks" width="20%">Follow-Up Source*: </td>
        <td align="left"  width="30%">
            <asp:DropDownList ID="ddl_FollowUpSource" runat="server" CssClass="Text" 
                Width="100px" TabIndex="3">
            </asp:DropDownList>
        </td>
        <td align="right" class="sidelinks" width="20%">Credit Report Bureau: </td>
        <td align="left" width="30%">
            <asp:DropDownList ID="ddl_CreditReportBureau" runat="server" CssClass="Text" 
                Width="100px" TabIndex="8">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks" width="20%">Follow-Up Outcome: </td>
        <td align="left" width="30%">
            <asp:DropDownList ID="ddl_FollowUpOutcome" runat="server" CssClass="Text" 
                Width="100px" TabIndex="4">
            </asp:DropDownList>
        </td>
        <td align="right" class="sidelinks" width="20%">Credit Report Date: </td>
        <td align="left">
            <asp:TextBox ID="txt_CreditReportDt" runat="server" width="100px" 
                MaxLength="10" CssClass="Text" TabIndex="9"></asp:TextBox>
        </td>
    </tr>    
    <tr>
        <td align="right" class="sidelinks" width="20%">Delinquency Status: </td>
        <td align="left" width="30%">
            <asp:DropDownList ID="ddl_DelinqencyStatus" runat="server" CssClass="Text" 
                Width="100px" TabIndex="5">
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
                Width="100px" TabIndex="6">
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

<script type="text/javascript" language="javascript">
    var msfWARN0450 = '<%= msgWARN0450 %>';    
    var status = document.getElementById('<%=hdf_Confirm.ClientID %>');
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
    
    var caseFollowUpBefore = new CaseFollowUp(followUpDate.value, creditScore.value, followUpSource.value
            , creditReportBureau.value, followUpOutcome.value, creditReportDt.value, delinqencyStatus.value, followUpComment.value, stillInHome.value);
    var caseFollowUpAfter = new CaseFollowUp();
    function ConfirmToCancel()
    {   
        caseFollowUpAfter = new CaseFollowUp(followUpDate.value, creditScore.value, followUpSource.value
            , creditReportBureau.value, followUpOutcome.value, creditReportDt.value, delinqencyStatus.value, followUpComment.value, stillInHome.value);        
        if(CompareCaseFollowUpObject(caseFollowUpAfter))
        {            
             if (confirm(msfWARN0450)==true)
                return true;
             else
             {
                ClearAllControl();
                return false;         
             }
        }
        else
        {            
            ClearAllControl();
            return false;
        }
    }
    
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
    
    function ConfirmEdit(message)
    {        
        caseFollowUpAfter = new CaseFollowUp(followUpDate.value, creditScore.value, followUpSource.value
            , creditReportBureau.value, followUpOutcome.value, creditReportDt.value, delinqencyStatus.value, followUpComment.value, stillInHome.value);        
        if(CompareCaseFollowUpObject(caseFollowUpAfter))
        {            
            if (confirm(message)==true)            
                status.value = "TRUE";                            
            else            
                status.value = "FALSE";            
        } 
        else        
            status.value = "FALSE";    
                            
        return true;
    }
    
    TabControl.onChanged=function()
    {
        ConfirmEdit(msfWARN0450);
    };
    function ChangeData()
    { 
        ConfirmEdit(msfWARN0450);
    }
</script>