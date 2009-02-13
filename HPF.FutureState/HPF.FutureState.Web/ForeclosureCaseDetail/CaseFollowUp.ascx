<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaseFollowUp.ascx.cs" Inherits="HPF.FutureState.Web.ForeclosureCaseDetail.CaseFollowUp" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />
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
                    BorderStyle="Inset" BorderColor="Gray" BorderWidth="0px" Height="100%" Width="820px">
                        <asp:GridView ID="grd_FollowUpList" runat="server" 
                        CellPadding="2" ForeColor="#333333"
                        GridLines="Vertical" AutoGenerateColumns="False" 
                        SelectedRowStyle-BackColor="Yellow" 
                        DataKeyNames="CasePostCounselingStatusId"
                        onrowcommand="grd_FollowUpList_RowCommand" 
                        onrowcreated="grd_FollowUpList_RowCreated" Width="100%">
                            <RowStyle CssClass="RowStyle"  />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle CssClass="FixedHeader" BackColor="#507CD1" Font-Bold="True" 
                            ForeColor="White" HorizontalAlign="Left"/>
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <Columns>
                                <asp:BoundField DataField="CasePostCounselingStatusId" HeaderText="ID" />                                     
                                <asp:BoundField DataField="FollowUpDt" HeaderText="Follow-Up Date"  
                                DataFormatString="{0:MM/dd/yyyy}" ItemStyle-Width="13%">
                                    <ItemStyle Width="13%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FollowUpSourceCdDesc" HeaderText="Source" 
                                ItemStyle-Width="37%">
                                    <ItemStyle Width="37%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OutcomeTypeName" 
                                HeaderText="Follow-Up Outcome"  ItemStyle-Width="20%">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LoanDelinqStatusCdDesc" HeaderText="Delinquency Status" 
                                ItemStyle-Width="15%">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:CommandField ShowSelectButton="true" SelectText = "View/Edit" 
                                    ButtonType="Button" ControlStyle-CssClass="MyButton" 
                                    ItemStyle-HorizontalAlign="Center" HeaderText="Select" ItemStyle-Width="15%" />
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
                            onclick="btn_New_Click" />
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
            <asp:TextBox ID="txt_FollowUpDt" width="30%" runat="server" MaxLength="10"></asp:TextBox>
        </td>
        <td align="right" class="sidelinks"  width="20%">Credit Score: </td>
        <td align="left">
            <asp:TextBox ID="txt_CreditScore" width="30%" runat="server" MaxLength="4"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks" width="20%">Follow-Up Source*: </td>
        <td align="left"  width="30%">
            <asp:DropDownList ID="ddl_FollowUpSource" runat="server">
            </asp:DropDownList>
        </td>
        <td align="right" class="sidelinks" width="20%">Credit Report Bureau: </td>
        <td align="left" width="30%">
            <asp:DropDownList ID="ddl_CreditReportBureau" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks" width="20%">Follow-Up Outcome: </td>
        <td align="left" width="30%">
            <asp:DropDownList ID="ddl_FollowUpOutcome" runat="server">
            </asp:DropDownList>
        </td>
        <td align="right" class="sidelinks" width="20%">Credit Report Date: </td>
        <td align="left">
            <asp:TextBox ID="txt_CreditReportDt" runat="server" width="30%" MaxLength="10"></asp:TextBox>
        </td>
    </tr>    
    <tr>
        <td align="right" class="sidelinks" width="20%">Delinquency Status: </td>
        <td align="left" width="30%">
            <asp:DropDownList ID="ddl_DelinqencyStatus" runat="server">
            </asp:DropDownList>
        </td>
        <td align="right" class="sidelinks" width="20%">Follow-Up Comment: </td>        
        <td rowspan="2" width="30%">
            <asp:TextBox ID="txt_FollowUpComment" runat="server" 
                TextMode="MultiLine" MaxLength="8000"></asp:TextBox>
        </td>
    </tr>    
    <tr>
        <td align="right" class="sidelinks" width="20%">Still in Home?: </td>
        <td align="left" width="30%">
            <asp:DropDownList ID="ddl_StillInHome" runat="server">
            </asp:DropDownList>
        </td>        
        <td>&nbsp;</td>        
    </tr>  
    <tr>
        <td colspan="2" align="right">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="MyButton" 
                onclick="btn_Save_Click" />
        </td>
        <td colspan="2" align="left">
            <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CssClass="MyButton" 
                onclick="btn_Cancel_Click" />
        </td>
    </tr>  
</table>
