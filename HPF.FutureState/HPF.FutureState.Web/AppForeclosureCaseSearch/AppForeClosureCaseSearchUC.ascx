<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AppForeClosureCaseSearchUC.ascx.cs"
    Inherits="HPF.FutureState.Web.AppForeClosureCaseSearch.AppForeClosureCaseSearchUC" %>
<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="cc2" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    TagPrefix="cc1" %>
<style type="text/css">
    .style1
    {
        height: 210px;
    }
</style>
<%--<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />--%>
<table width="100%">
    <colgroup>
        <col width="10%" />
        <col width="90%" />
    </colgroup>
    <tr style="">
        <td align="center">
            <h1>Foreclosure Case Search</h1>
        </td>
    </tr>
    <tr>
        <td>
            <h1>
                Search Criteria:</h1>
        </td>
    </tr>
    <tr>
        <td>
            <table style="width: 940px">
            <tr>
               <td align="right" class="sidelinks">
                        Loan Number:
                    </td>
                    <td class="Control" width="110 px">
                        <asp:TextBox ID="txtLoanNum" runat="server" CssClass="Text" Width="110px" 
                            TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="sidelinks">
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
                        <asp:DropDownList ID="ddlDup" runat="server" CssClass="Text" Width="60px" 
                            TabIndex="9">
                            <asp:ListItem Selected="True" Value=''></asp:ListItem>
                            <asp:ListItem Value='Y'>Yes</asp:ListItem>
                            <asp:ListItem Value='N'>No</asp:ListItem>
                        </asp:DropDownList>
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
                <tr>
                    <td align="right" class="sidelinks">
                        &nbsp;</td>
                    <td class="Control" width="110 px">
                        &nbsp;</td>
                    <td align="right" class="sidelinks">
                        &nbsp;</td>
                    <td class="Control">
                        &nbsp;</td>
                    <td class="sidelinks" align="right">
                        &nbsp;</td>
                    <td class="Control">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" OnClick="btnSearch_Click"
                CssClass="MyButton" TabIndex="12" />
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
            <asp:Panel ID="myPannel" runat="server" CssClass="ScrollTable" Width="960px" 
                Visible="False">
                <asp:UpdatePanel ID="myupdatepan" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grvForeClosureCaseSearch" runat="server" CellPadding="2" GridLines="Vertical"
                            AutoGenerateColumns="false" CssClass="GridViewStyle" Width="2900px" OnRowDataBound="grvForeClosureCaseSearch_RowDataBound">
                            <RowStyle CssClass="RowStyle" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="FixedHeader" HorizontalAlign="Center" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                            <Columns>
                                <asp:HyperLinkField DataTextField="CaseID" DataNavigateUrlFields="CaseID" DataNavigateUrlFormatString="../ForeclosureCaseInfo.aspx?CaseID={0}"
                                    HeaderText="Case ID" />
                                <asp:BoundField DataField="AgencyCaseNum" HeaderText="Agency Case ID" />
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
            <asp:Label ID="lblMinRow" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lbl1" runat="server" Text=" - " Visible="false"></asp:Label>
            <asp:Label ID="lblMaxRow" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lbl2" runat="server" Text=" of " Visible="false"></asp:Label>
            <asp:Label ID="lblTotalRowNum" runat="server" Visible="false"></asp:Label>
            <asp:LinkButton ID="lbtnFirst" CommandName="First" OnCommand="lbtnNavigate_Click"
                runat="server" Text="<<  " Visible="false" CssClass="NoUnderLine"></asp:LinkButton>
            <asp:LinkButton ID="lbtnPrev" CommandName="Prev" OnCommand="lbtnNavigate_Click" runat="server"
                Text="<" Visible="false" CssClass="NoUnderLine"></asp:LinkButton>
            <asp:PlaceHolder ID="phPages" runat="server" Visible="true"></asp:PlaceHolder>
            <asp:LinkButton ID="lbtnNext" CommandName="Next" OnCommand="lbtnNavigate_Click" runat="server"
                Text=">  " Visible="false" CssClass="NoUnderLine"></asp:LinkButton>
            <asp:LinkButton ID="lbtnLast" CommandName="Last" OnCommand="lbtnNavigate_Click" runat="server"
                Text=">>" Visible="false" CssClass="NoUnderLine"></asp:LinkButton>
            <asp:Label ID="lblTemp" runat="server" Text="" Visible="false"></asp:Label>
        </td>
    </tr>
    </table>
