<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AppForeClosureCaseSearchUC.ascx.cs"
    Inherits="HPF.FutureState.Web.AppForeClosureCaseSearch.AppForeClosureCaseSearchUC" %>
<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="cc2" %>
<%--<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    TagPrefix="cc1" %>--%>
<%--<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />--%>
<table width="100%">
    <colgroup>
        <col width="10%" />
        <col width="90%" />
    </colgroup>
    <tr style="">
        <td align="center" class="sidelinks">
            <b><font size="3">Foreclosure Case Search</font></b>
        </td>
    </tr>
    <tr>
        <td class="sidelinks">
           <b><font size="2">Search Criteria:</font> </b>
        </td>
    </tr>
    <tr>
        <td>
            <table width="100%">
            <tr>
               <td align="right" class="sidelinks" nowrap="nowrap">
                        Loan Number:
                    </td>
                    <td class="Control" width="110px">
                        <asp:TextBox ID="txtLoanNum" runat="server" CssClass="Text" Width="110px" 
                            TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="sidelinks" nowrap="nowrap">
                        Foreclosure Case ID:
                    </td>
                    <td class="Control">
                        <asp:TextBox ID="txtForeclosureCaseID" runat="server" MaxLength="10" 
                            CssClass="Text" Width="140px" TabIndex="5"></asp:TextBox>
                    </td>
                    <td align="right" class="sidelinks">
                        Duplicate:
                    </td>
                    <td class="Control">
                        <table width="100%">
                            <tr>
                                <td>
                        <asp:DropDownList ID="ddlDup" runat="server" CssClass="Text" Width="60px" 
                            TabIndex="9">
                            <asp:ListItem Selected="True" Value=''></asp:ListItem>
                            <asp:ListItem Value='Y'>Yes</asp:ListItem>
                            <asp:ListItem Value='N'>No</asp:ListItem>
                        </asp:DropDownList>
                                </td>
                                <td align="right">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" OnClick="btnSearch_Click"
                CssClass="MyButton" TabIndex="12" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="sidelinks">
                        Last 4 of SSN:
                    </td>
                    <td class="Control" width="110 px">
                        <asp:TextBox ID="txtSSN" runat="server" CssClass="Text" Width="110px" 
                            TabIndex="2"></asp:TextBox>
                    </td>
                    <td align="right" class="sidelinks">
                        Agency Case ID:
                    </td>
                    <td class="Control">
                        <asp:TextBox ID="txtAgencyCaseID" runat="server" CssClass="Text" Width="140px" 
                            TabIndex="6"></asp:TextBox>
                    </td>
                    <td align="right" class="sidelinks">
                        Agency:
                    </td>
                    <td class="Control">
                        <asp:DropDownList ID="ddlAgency" runat="server" Width="200px" CssClass="Text" 
                            Height="16px" TabIndex="10">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="sidelinks">
                        Last Name:
                    </td>
                    <td class="Control" width="110 px">
                        <asp:TextBox ID="txtLastName" runat="server" MaxLength="30" CssClass="Text" 
                            Width="110px" TabIndex="3"></asp:TextBox>
                    </td>
                    <td align="right" class="sidelinks">
                        Property Zip:
                    </td>
                    <td class="Control">
                        <asp:TextBox ID="txtPropertyZip" runat="server" CssClass="Text" Width="140px" 
                            TabIndex="7"></asp:TextBox>
                    </td>
                    <td align="right" class="sidelinks">
                        Program:
                    </td>
                    <td class="Control">
                        <asp:DropDownList ID="ddlProgram" runat="server" Width="200px" CssClass="Text" 
                            TabIndex="11">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="sidelinks">
                        First Name:
                    </td>
                    <td class="Control" width="110 px">
                        <asp:TextBox ID="txtFirstName" runat="server" MaxLength="30" CssClass="Text" 
                            Width="110px" TabIndex="4"></asp:TextBox>
                    </td>
                    <td align="right" class="sidelinks">
                        Property State:
                    </td>
                    <td class="Control">
                        <asp:DropDownList ID="ddlPropertyState" runat="server"  
                            CssClass="Text" TabIndex="8">
                        </asp:DropDownList>
                    </td>
                    <td class="sidelinks" align="right">
                        Servicer:
                    </td>
                    <td class="Control">
                        <asp:DropDownList ID="ddlServicer" runat="server"  CssClass="Text" 
                            TabIndex="12">
                        </asp:DropDownList>
                    </td>
                    </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:BulletedList ID="bulErrorMessage" CssClass="ErrorMessage" runat="server">
            </asp:BulletedList>
        </td>        
    </tr>
    <tr>
        <td class="sidelinks">
            &nbsp;<asp:Label ID="lblResult" runat="server" Text="Search Results:" Visible="False"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>            
            <asp:ScriptManager ID="myscript" runat="server">
            </asp:ScriptManager>
            <asp:Panel CssClass="ScrollTable" ID="myPannel" runat="server"
                Visible="False">
                <asp:UpdatePanel ID="myupdatepan" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grvForeClosureCaseSearch" runat="server" CellPadding="2" GridLines="Vertical"
                            AutoGenerateColumns="false" CssClass="GridViewStyle" Width="2900px" 
                            OnRowDataBound="grvForeClosureCaseSearch_RowDataBound" AllowPaging="True" 
                            onpageindexchanging="grvForeClosureCaseSearch_PageIndexChanging" PageSize="50">
                            <PagerSettings Visible="False" />
                            <RowStyle CssClass="RowStyle" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="FixedHeader" HorizontalAlign="Center" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                            <Columns>
                                <asp:HyperLinkField DataTextField="CaseID" DataNavigateUrlFields="CaseID" DataNavigateUrlFormatString="../ForeclosureCaseInfo.aspx?CaseID={0}"
                                    HeaderText="Case ID" ItemStyle-Width="60" />
                                <asp:BoundField DataField="AgencyCaseNum" HeaderText="Agency Case ID" ItemStyle-Width="100"/>
                                <asp:TemplateField HeaderText="Counseled">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCounseled" runat="server" Text='<%#Eval("CaseCompleteDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CaseDate" HeaderText="Case Date" DataFormatString="{0:MM/dd/yyyy}" />
                                <asp:BoundField DataField="LoanList" HeaderText="Loan List" />
                                <asp:BoundField DataField="BorrowerFirstName" HeaderText="Borrower First Name" />
                                <asp:BoundField DataField="BorrowerLastName" HeaderText="Borrower Last Name" />
                                <asp:BoundField DataField="Last4SSN" HeaderText="Borrower Last 4 of SSN" />
                                <asp:BoundField DataField="CoborrowerFirstName" HeaderText="Co-borrower First Name" />
                                <asp:BoundField DataField="CoborrowerLastName" HeaderText="Co-borrower Last Name" />
                                <asp:BoundField DataField="CoborrowerLast4SSN" HeaderText="Co-borrower Last 4 of SSN" />
                                <asp:TemplateField HeaderText="Property Address">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPropertyAddress" runat="server" Text='<%#Eval("PropertyAddress")+", "+Eval("PropertyCity")+", "+Eval("PropertyState")+", "+Eval("PropertyZip") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="AgencyName" HeaderText="Agency Name" />
                                <asp:TemplateField HeaderText="Agent Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAgentName" runat="server" Text='<%#Eval("AgentFirstName")+" "+Eval("AgentLastName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="AgentPhone" HeaderText="Agent Phone" />
                                <asp:BoundField DataField="AgentExtension" HeaderText="Agent Extension" />
                                <asp:BoundField DataField="AgentEmail" HeaderText="Agent Email" />
                                <asp:TemplateField HeaderText="Complete Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCaseCompleteDate" runat="server" Text='<%#Eval("CaseCompleteDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DaysDelinquent" HeaderText="Days Delinquent" />
                                <asp:BoundField DataField="BankruptcyIndicator" HeaderText="Bankrupcy Indicator" />
                                <asp:BoundField DataField="ForeclosureNoticeReceivedIndicator" HeaderText="Foreclosure Notice Received Indicator" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
         <ContentTemplate>
            <table>
            <tr>
            <td>
            <asp:Label ID="lblMinRow" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lbl1" runat="server" Text=" - " Visible="false"></asp:Label>
            <asp:Label ID="lblMaxRow" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lbl2" runat="server" Text=" of " Visible="false"></asp:Label>
            <asp:Label ID="lblTotalRowNum" runat="server" Visible="false"></asp:Label>
            &nbsp;
            <asp:LinkButton ID="lbtnFirst" CommandName="First" OnCommand="lbtnNavigate_Click"
                runat="server" Text="&lt;&lt;" Visible="false" CssClass="NoUnderLine"></asp:LinkButton>
            &nbsp;
            <asp:LinkButton ID="lbtnPrev" CommandName="Prev" OnCommand="lbtnNavigate_Click" runat="server"
                Text="&lt;" Visible="false" CssClass="NoUnderLine"></asp:LinkButton>
            &nbsp;<asp:PlaceHolder ID="phPages" runat="server" Visible="true"></asp:PlaceHolder>
            <asp:LinkButton ID="lbtnNext" CommandName="Next" OnCommand="lbtnNavigate_Click" runat="server"
                Text="&gt;" Visible="false" CssClass="NoUnderLine"></asp:LinkButton>
            &nbsp;
            <asp:LinkButton ID="lbtnLast" CommandName="Last" OnCommand="lbtnNavigate_Click" runat="server"
                Text="&gt;&gt;" Visible="false" CssClass="NoUnderLine"></asp:LinkButton>
            <asp:Label ID="lblTemp" runat="server" Text="" Visible="false"></asp:Label>
            </td>
            <td width="40">&nbsp;</td>
             <td><div id="waitPanel" style="display:none">Please wait...</div></td>
             </tr>
            </table>
            </ContentTemplate>
            </asp:UpdatePanel>            
        </td>
    </tr>
    </table>
<script type="text/javascript">
    var mypanel = document.getElementById('<%=myPannel.ClientID %>');
    if(mypanel != null)
    {
        mypanel.style.width= screen.width - 50;             
        mypanel.style.height = screen.height - 580;
    }
            
   function ShowWaitPanel()
   {
        var mypanel = document.getElementById('waitPanel');
        mypanel.style.display ="block";
   }
</script>