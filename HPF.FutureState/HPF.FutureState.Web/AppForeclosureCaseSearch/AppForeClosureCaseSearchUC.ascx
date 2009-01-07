<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AppForeClosureCaseSearchUC.ascx.cs"
    Inherits="HPF.FutureState.Web.AppForeClosureCaseSearch.AppForeClosureCaseSearchUC" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    TagPrefix="cc1" %>

<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />

<table width="100%">
    <colgroup>
        <col width="20%" />
        <col width="15%" />
        <col width="15%" />
        <col width="15%" />
        <col width="10%" />
        <col width="25%" />
    </colgroup>
    <tr style="  ">
        <td colspan="6"  class="Header">
            Foreclosure Case Search
        </td>
    </tr>
    <tr>
        <td colspan="6" >
            <h1>Search Criteria:</h1>
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks" >
            Last 4 or SSN:
        </td>
        <td class="Control">
            <asp:TextBox ID="txtSSN" runat="server" CssClass="Text" Width="140px"></asp:TextBox>
            
        </td>
        <td align="right" class="sidelinks">
            Agency Case ID:
        </td>
        <td class="Control">
            <asp:TextBox ID="txtAgencyCaseID" runat="server" CssClass="Text"></asp:TextBox>
          
        </td>
        <td align="right" class="sidelinks">
            Duplicate:
        </td>
        <td class="Control">
            <asp:DropDownList ID="ddlDup" runat="server" CssClass="Text">
                <asp:ListItem Selected="True" Value=''></asp:ListItem>
                <asp:ListItem Value='Y'>Yes</asp:ListItem>
                <asp:ListItem Value='N'>No</asp:ListItem>
                
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            Last Name:
        </td>
        <td class="Control">
            <asp:TextBox ID="txtLastName" runat="server" MaxLength="30" CssClass="Text" 
                Width="140px"></asp:TextBox>
        </td>
        <td align="right" class="sidelinks">
            Loan Number:
        </td>
        <td class="Control">
            <asp:TextBox ID="txtLoanNum" runat="server" CssClass="Text"></asp:TextBox>
          
        </td>
        <td align="right" class="sidelinks">
            Agency:
        </td>
        <td class="Control">
            <asp:DropDownList ID="ddlAgency" runat="server" Width="100px" CssClass="Text">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            First Name:
        </td>
        <td class="Control">
            <asp:TextBox ID="txtFirstName" runat="server" MaxLength="30" CssClass="Text" 
                Width="140px"></asp:TextBox>
        </td>
        <td align="right" class="sidelinks">
            Property Zip:
        </td>
        <td class="Control">
            <asp:TextBox ID="txtPropertyZip" runat="server" CssClass="Text"></asp:TextBox>
           
        </td>
        <td align="right" class="sidelinks">
            Program:
        </td>
        <td class="Control">
            <asp:DropDownList ID="ddlProgram" runat="server" Width="100px" CssClass="Text">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right" class="sidelinks">
            Foreclosure Case ID:
        </td>
        <td class="Control">
            <asp:TextBox ID="txtForeclosureCaseID" runat="server" MaxLength="10" 
                Width="140px" CssClass="Text"></asp:TextBox>
           <%--<cc1:PropertyProxyValidator ID="txtForeclosureCaseIDCheckValidator" runat="server" 
           PropertyName="ForeclosureCaseID" RulesetName="Default" ControlToValidate="txtForeclosureCaseID" 
           SourceTypeName="HPF.FutureState.Common.DataTransferObjects.AppForeclosureCaseSearchCriteriaDTO"></cc1:PropertyProxyValidator>--%>
           
        </td>
        <td align="right" class="sidelinks">
            Property State:
        </td>
        <td class="Control">
            <asp:DropDownList ID="ddlPropertyState" runat="server" Width="100px" 
                CssClass="Text">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;
        </td>
        <td  class="Control">
            <asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" OnClick="btnSearch_Click"  CssClass="MyButton"/>
        </td>
    </tr>
    <tr>
        <td colspan="6">
           
        </td>
        
    </tr>
     <tr>
        <td colspan="6">
        <cc1:PropertyProxyValidator ID="txtSSNCheckValidator" runat="server" PropertyName="Last4SSN" 
            RulesetName="Default" ControlToValidate="txtSSN"  Display="Dynamic"
            SourceTypeName="HPF.FutureState.Common.DataTransferObjects.AppForeclosureCaseSearchCriteriaDTO"></cc1:PropertyProxyValidator> 
            <cc1:PropertyProxyValidator ID="txtAgencyCaseIDCheckValidator" runat="server" PropertyName="AgencyCaseID" 
            RulesetName="Default" ControlToValidate="txtAgencyCaseID"  Display="Dynamic"
            SourceTypeName="HPF.FutureState.Common.DataTransferObjects.AppForeclosureCaseSearchCriteriaDTO"></cc1:PropertyProxyValidator>
             <cc1:PropertyProxyValidator ID="txtLoanNumCheckValidator" runat="server" PropertyName="LoanNumber" 
            RulesetName="Default" ControlToValidate="txtLoanNum"  Display="Dynamic"
            SourceTypeName="HPF.FutureState.Common.DataTransferObjects.AppForeclosureCaseSearchCriteriaDTO"></cc1:PropertyProxyValidator>
            <cc1:PropertyProxyValidator ID="txtPropertyZipCheckValidator" runat="server" Display="Dynamic"
            PropertyName="PropertyZip" RulesetName="Default" ControlToValidate="txtPropertyZip"
             SourceTypeName="HPF.FutureState.Common.DataTransferObjects.AppForeclosureCaseSearchCriteriaDTO"></cc1:PropertyProxyValidator>
            <asp:RegularExpressionValidator  ControlToValidate="txtForeclosureCaseID" ID="RegularExpressionValidator1" Display="Dynamic" runat="server" ErrorMessage="Foreclosure Case ID: Only numeric characters allowed" ValidationExpression="[\d]*"></asp:RegularExpressionValidator>
        </td>
        
    </tr>
    <tr>
    <td colspan="6">
    <asp:Label ID="lblErrorMessage"  CssClass="ErrorMessage" runat="server"></asp:Label>
    </td>
    </tr>
    <tr>
        <td colspan="6">
            <%--<uc1:FixedHeaderGrid ID="grvForeClosureCaseSearch" runat="server" />--%>
            <asp:Panel ID="panForeClosureCaseSearch" runat="server" CssClass="ScrollTable" BorderStyle="Inset"
                BorderColor="Gray" BorderWidth="1px" Visible="false">
                <asp:GridView ID="grvForeClosureCaseSearch" runat="server" CellPadding="2" ForeColor="#333333"
                    GridLines="Vertical" AutoGenerateColumns="false" CssClass="GridViewStyle" 
                    Width="2500px"  SelectedRowStyle-BackColor="Yellow" 
                    onrowdatabound="grvForeClosureCaseSearch_RowDataBound" 
                   
                    onrowcreated="grvForeClosureCaseSearch_RowCreated">
                    <RowStyle CssClass="RowStyle"  />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle CssClass="FixedHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle"  />
                    <Columns>
                        <%--<asp:BoundField DataField="CaseID" HeaderText="Case ID"  />--%>
                        <asp:HyperLinkField DataTextField="CaseID" DataNavigateUrlFields="CaseID" DataNavigateUrlFormatString="../AppForeclosureCaseDetailPage.aspx?CaseID={0}" HeaderText="Case ID" />
                        <asp:BoundField DataField="AgencyCaseNum" HeaderText="Agency Case ID" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="Counseled">
                        <ItemTemplate>
                        <asp:Label ID="lblCounseled" runat="server" Text='<%#Eval("CaseCompleteDate") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CaseDate" HeaderText="Case Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:TemplateField HeaderText="Borrower Name">
                            <ItemTemplate>
                                <asp:Label ID="lblBorrowerName" runat="server" Text='<%#Eval("BorrowerFirstName")+","+Eval("BorrowerLastName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Last4SSN" HeaderText="SSN" />
                        <asp:TemplateField HeaderText="Co-Borrower">
                            <ItemTemplate>
                                <asp:Label ID="lblCoBorrowerName" runat="server" Text='<%#Eval("CoborrowerFirstName")+" "+Eval("CoborrowerLastName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CoborrowerLast4SSN" HeaderText="SSN" />
                        <asp:BoundField DataField="PropertyAddress" HeaderText="Property Address" />
                        <asp:BoundField DataField="PropertyCity" HeaderText="Property City" />
                        <asp:BoundField DataField="PropertyState" HeaderText="Property State" />
                        <asp:BoundField DataField="PropertyZip" HeaderText="Property Zip" />
                        <asp:BoundField DataField="AgencyName" HeaderText="Agency Name" />
                        <asp:TemplateField HeaderText="Agent Name">
                        <ItemTemplate>
                        <asp:Label ID="lblAgentName" runat="server" Text='<%#Eval("AgentFirstName")+","+Eval("AgentLastName") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="AgentPhone" HeaderText="Agent Phone" />
                        <asp:BoundField DataField="AgentExtension" HeaderText="Agent Extension" />
                        <asp:BoundField DataField="AgentEmail" HeaderText="Agent Email" />
                        <asp:BoundField DataField="CaseCompleteDate" HeaderText="Complete Date"  DataFormatString="{0:dd/MM/yyyy}"/>
                        <asp:BoundField DataField="DaysDelinquent" HeaderText="Days Delinquent" />
                        <asp:BoundField DataField="BankruptcyIndicator" HeaderText="Bankrupcy Indicator" />
                        <asp:BoundField DataField="ForeclosureNoticeReceivedIndicator" HeaderText="Foreclosure Notice Received Indicator" />
                        <asp:BoundField DataField="LoanList" HeaderText="Loan List" />
                    </Columns>
                    <EmptyDataTemplate>
                    There is no data match !
                    </EmptyDataTemplate>
                </asp:GridView>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
        <asp:Label ID="lblMinRow" runat="server" Visible="false"></asp:Label>    
        <asp:Label ID="lbl1" runat="server" Text =" - " Visible="false"></asp:Label>
        <asp:Label ID="lblMaxRow" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lbl2" runat="server" Text=" of " Visible="false"></asp:Label>
        <asp:Label ID="lblTotalRowNum" runat="server" Visible="false"></asp:Label>
        <asp:LinkButton ID="lbtnFirst" CommandName="First" OnCommand="lbtnNavigate_Click" runat="server" Text="<<  " Visible="false" CssClass="NoUnderLine"></asp:LinkButton>
        <asp:LinkButton ID="lbtnPrev" CommandName="Prev" OnCommand="lbtnNavigate_Click" runat="server" Text="<" Visible="false" CssClass="NoUnderLine"></asp:LinkButton>
        <asp:PlaceHolder ID="phPages"  runat="server" Visible="true"></asp:PlaceHolder>
        <asp:LinkButton ID="lbtnNext"  CommandName="Next" OnCommand="lbtnNavigate_Click" runat="server" Text=">  " Visible="false" CssClass="NoUnderLine"></asp:LinkButton>
        <asp:LinkButton ID="lbtnLast" CommandName="Last" OnCommand="lbtnNavigate_Click" runat="server" Text=">>" Visible="false" CssClass="NoUnderLine"></asp:LinkButton>
        <asp:Label ID="lblTemp" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblTemp1" runat="server" Text="" Visible="false"></asp:Label>
        </td>
        
    </tr>
     <tr>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
