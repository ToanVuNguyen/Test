<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AppForeClosureCaseSearch.ascx.cs"
    Inherits="HPF.FutureState.Web.BillingAdmin.AppForeClosureCaseSearch" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    TagPrefix="cc1" %>
<%@ Register Src="~/BillingAdmin/FixedHeaderGrid.ascx" TagName="FixedHeaderGrid"
    TagPrefix="uc1" %>
<style type="text/css">
    .Title
    {
        font-weight: bold;
        text-align: right;
        font-size:medium;
    }
    .Control
    {
        text-align: left;
        padding-left: 10px;
    }
    .FixedHeader
    {
        position: relative;
        top: expression(offsetParent.scrollTop);
    }
    .ScrollTable
    {
        position: relative;
        width: 950px;
        height: 300px;
        overflow: auto;
        margin: 0;
    }
    .GridViewStyle
    {
        border-top-style: none;
        
    }
</style>
<table width="100%">
    <colgroup>
        <col width="20%" />
        <col width="15%" />
        <col width="15%" />
        <col width="15%" />
        <col width="10%" />
        <col width="25%" />
    </colgroup>
    <tr style="">
        <td colspan="6" style="font-size:larger; text-align:center; color:Blue;">
            Foreclosure Case Search
        </td>
    </tr>
    <tr>
        <td colspan="6">
            Search Criteria:
        </td>
    </tr>
    <tr>
        <td class="Title">
            Last 4 or SSN:
        </td>
        <td class="Control">
            <asp:TextBox ID="txtSSN" runat="server"></asp:TextBox>
            
        </td>
        <td class="Title">
            Agency Case ID:
        </td>
        <td class="Control">
            <asp:TextBox ID="txtAgencyCaseID" runat="server"></asp:TextBox>
          
        </td>
        <td class="Title">
            Duplicate:
        </td>
        <td class="Control">
            <asp:DropDownList ID="ddlDup" runat="server">
                <asp:ListItem Value='Y'>Y</asp:ListItem>
                <asp:ListItem Value='N'>N</asp:ListItem>
                <asp:ListItem Selected="True" Value=''></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="Title">
            Last Name:
        </td>
        <td class="Control">
            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
        </td>
        <td class="Title">
            Loan Number:
        </td>
        <td class="Control">
            <asp:TextBox ID="txtLoanNum" runat="server"></asp:TextBox>
          
        </td>
        <td class="Title">
            Agency:
        </td>
        <td class="Control">
            <asp:DropDownList ID="ddlAgency" runat="server" Width="100px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="Title">
            First Name:
        </td>
        <td class="Control">
            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
        </td>
        <td class="Title">
            Property Zip:
        </td>
        <td class="Control">
            <asp:TextBox ID="txtPropertyZip" runat="server"></asp:TextBox>
           
        </td>
        <td class="Title">
            Program:
        </td>
        <td class="Control">
            <asp:DropDownList ID="ddlProgram" runat="server" Width="100px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="Title">
            Foreclosure Case ID:
        </td>
        <td class="Control">
            <asp:TextBox ID="txtForeclosureCaseID" runat="server" MaxLength="10"></asp:TextBox>
           <%--<cc1:PropertyProxyValidator ID="txtForeclosureCaseIDCheckValidator" runat="server" 
           PropertyName="ForeclosureCaseID" RulesetName="Default" ControlToValidate="txtForeclosureCaseID" 
           SourceTypeName="HPF.FutureState.Common.DataTransferObjects.AppForeclosureCaseSearchCriteriaDTO"></cc1:PropertyProxyValidator>--%>
           
        </td>
        <td class="Title">
            Property State:
        </td>
        <td class="Control">
            <asp:DropDownList ID="ddlPropertyState" runat="server" Width="100px">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" OnClick="btnSearch_Click" />
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
            <%--<uc1:FixedHeaderGrid ID="grvForeClosureCaseSearch" runat="server" />--%>
            <asp:Panel ID="panForeClosureCaseSearch" runat="server" CssClass="ScrollTable" BorderStyle="Inset"
                BorderColor="Gray" BorderWidth="1px">
                <asp:GridView ID="grvForeClosureCaseSearch" runat="server" CellPadding="2" ForeColor="#333333"
                    GridLines="Vertical" AutoGenerateColumns="false" CssClass="GridViewStyle" 
                    Width="2500px" 
                    onrowdatabound="grvForeClosureCaseSearch_RowDataBound">
                    <RowStyle BackColor="#EFF3FB" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle CssClass="FixedHeader" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="CaseID" HeaderText="Case ID"  />
                        <asp:BoundField DataField="AgencyCaseID" HeaderText="Agency Case ID" ItemStyle-HorizontalAlign="Center" />
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
                                <asp:Label ID="lblCoBorrowerName" runat="server" Text='<%#Eval("CoborrowerFirstName")+","+Eval("CoborrowerLastName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CoborrowerLast4SSN" HeaderText="SSN" />
                        <asp:BoundField DataField="PropertyAddress" HeaderText="Property Address" />
                        <asp:BoundField DataField="PropertyCity" HeaderText="Property City" />
                        <asp:BoundField DataField="PropertyState" HeaderText="Property State" />
                        <asp:BoundField DataField="PropertyZip" HeaderText="Property Zip" />
                        <asp:BoundField DataField="AgencyName" HeaderText="Agency Name" />
                        <asp:BoundField DataField="AgentName" HeaderText="Agent Name" />
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
