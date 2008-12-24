<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AppForeClosureCaseSearch.ascx.cs" Inherits="HPF.FutureState.Web.BillingAdmin.AppForeClosureCaseSearch" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    TagPrefix="cc1" %>
<%@ Register Src="~/BillingAdmin/FixedHeaderGrid.ascx" TagName="FixedHeaderGrid"  TagPrefix="uc1" %>
<style type="text/css">
    .style1
    {
        width: 100%;
        
    }
    .title 
    {
            font-weight:bold;
            text-align:right;
    }
    .style2
    {
        color: #0066FF;
        font-weight: bold;
        font-size: medium;
        text-align:center;
    }
</style>
<table class="style1">
    <colgroup>
    <col=30% />
    <col=30%" />
    <col=60% />
    </colgroup>
    <tr style=" ">
        <td colspan="6" class="style2">
            Foreclosure Case Search</td>
    </tr>
    <tr>
        <td colspan="6">
            Search Criteria:</td>
    </tr>
    <tr>
        <td class="title">
            Last 4 or SSN:
        </td>
        <td>
            <asp:TextBox ID="txtSSN" runat="server"></asp:TextBox>
        </td>
        <td class="title">
            Agency Case ID:</td>
        <td>
            <asp:TextBox ID="txtAgencyCaseID" runat="server"></asp:TextBox>
        </td>
        <td class="title">
            Duplicate:
        </td>
        <td>
            <asp:DropDownList ID="ddlDup" runat="server">
                <asp:ListItem Value='Y'>Y</asp:ListItem>
                <asp:ListItem Value='N'>N</asp:ListItem>
                <asp:ListItem Selected="True" Value=''></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="title">
            Last Name:         </td>
        <td>
            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
        </td>
        <td class="title">
            Loan Number:
        </td>
        <td>
            <asp:TextBox ID="txtLoanNum" runat="server"></asp:TextBox>
        </td>
        <td class="title">
            Agency:</td>
        <td>
            <asp:DropDownList ID="ddlAgency" runat="server" Width="100px">
            </asp:DropDownList>
            
        </td>
    </tr>
    <tr>
        <td class="title">
            First Name:         </td>
        <td>
            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
            <cc1:PropertyProxyValidator ID="txtFirstNameCheckValidator" runat="server" PropertyName="FirstName" RulesetName="Default" ControlToValidate="txtFirstName" SourceTypeName="HPF.FutureState.Common.DataTransferObjects.BillingAdmin.AppForeclosureCaseSearchCriteriaDTO"></cc1:PropertyProxyValidator>
        </td>
        <td class="title">
            Property Zip:</td>
        <td>
            <asp:TextBox ID="txtPropertyZip" runat="server"></asp:TextBox>
            <%--<cc1:PropertyProxyValidator ID="txtPropertyZipCheckValidator" runat="server" PropertyName="PropertyZip" RulesetName="Default" ControlToValidate="txtPropertyZip" SourceTypeName="HPF.FutureState.Common.DataTransferObjects.BillingAdmin.AppForeclosureCaseSearchCriteriaDTO"></cc1:PropertyProxyValidator>--%>
        </td>
        <td class="title">
            Program:</td>
        <td>
            <asp:DropDownList ID="ddlProgram" runat="server" Width="100px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="title">
            Foreclosure Case ID:
            </td>
        <td>
            <asp:TextBox ID="txtForeclosureCaseID" runat="server"></asp:TextBox>
            <cc1:PropertyProxyValidator ID="txtForecloseureCaseIDCheckValidate" runat="server" PropertyName="ForeclosureCaseID" RulesetName="Default" ControlToValidate="txtForeclosureCaseID" SourceTypeName="HPF.FutureState.Common.DataTransferObjects.BillingAdmin.AppForeclosureCaseSearchCriteriaDTO"></cc1:PropertyProxyValidator>
        </td>
        <td class="title">
            Property State:</td>
        <td>
            <asp:DropDownList ID="ddlPropertyState" runat="server" Width="100px">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
        <td>
            <asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6">
            <uc1:FixedHeaderGrid ID="grvForeClosureCaseSearch" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
