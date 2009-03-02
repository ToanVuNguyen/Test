<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaseLoan.ascx.cs" Inherits="HPF.FutureState.Web.ForeclosureCaseDetail.CaseLoan" %>
<br />
<div class="sidelinks"><h1>Loan Details:</h1></div>
<Div style="Height:210px;Overflow:Auto">
&nbsp;&nbsp;&nbsp;<asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
<asp:DataList ID="dtlCaseLoan" runat="server" Width=100%>        
    <ItemTemplate>
        <tr>
            <td align="right" class="sidelinks" width="30%">Loan Number*: </td>
            <td align="left" width="20%" class ="Text"><%# DataBinder.Eval(Container.DataItem, "AcctNum")%></td>
            <td align="right" class="sidelinks"  width="30%">Current Loan Balance: </td>
            <td align="left"  width="20%" class ="Text"><%# DataBinder.Eval(Container.DataItem, "CurrentLoanBalanceAmt", "{0:C}")%></td>
        </tr>
        <tr>
            <td align="right" class="sidelinks">Servicer*: </td>
            <td align="left" class ="Text"><%# DataBinder.Eval(Container.DataItem, "ServicerName")%></td>
            <td align="right" class="sidelinks">Original Loan Amount: </td>
            <td align="left" class ="Text"><%# DataBinder.Eval(Container.DataItem, "OrigLoanAmt", "{0:C}")%></td>
            
        </tr>       
        <tr>
            <td align="right" class="sidelinks">Other Servicer Name: </td>
            <td align="left" class ="Text"><%# DataBinder.Eval(Container.DataItem, "OtherServicerName")%></td>
            <td align="right" class="sidelinks">Orig. Mortgate Co. Name: </td>
            <td align="left" class ="Text"><%# DataBinder.Eval(Container.DataItem, "OrigMortgageCoName")%></td>            
        </tr>       
        <tr>
            <td align="right" class="sidelinks">Mortgage Type*: </td>
            <td align="left" class ="Text"><%# DataBinder.Eval(Container.DataItem, "MortgageTypeCd")%></td>
            <td align="right" class="sidelinks">Orig. Mortgage Co FDIC/NCUA: </td>
            <td align="left" class ="Text"><%# DataBinder.Eval(Container.DataItem, "OrigMortgageCoFdicNcusNum")%></td>            
        </tr>       
        <tr>
            <td align="right" class="sidelinks">Mortgage Program*: </td>
            <td align="left" class ="Text"><%# DataBinder.Eval(Container.DataItem, "MortgageProgramCd")%></td>
            <td align="right" class="sidelinks">Orig. Loan Number: </td>
            <td align="left" class ="Text"><%# DataBinder.Eval(Container.DataItem, "OrginalLoanNum")%></td>            
        </tr>       
         <tr>
            <td align="right" class="sidelinks">Loan 1<sup>st</sup> 2<sup>nd</sup> Code: </td>
            <td align="left" class ="Text"><%# DataBinder.Eval(Container.DataItem, "Loan1st2nd")%></td>
            <td align="right" class="sidelinks">Current Servicer FDIC/NCUA: </td>
            <td align="left" class ="Text"><%# DataBinder.Eval(Container.DataItem, "CurrentServicerFdicNcuaNum")%></td>            
        </tr>     
        <tr>
            <td align="right" class="sidelinks">ARM Reset Indicator: </td>
            <td align="left" class ="Text"><%# DataBinder.Eval(Container.DataItem, "ArmResetInd")%></td>            
            <td align="right" class="sidelinks">Investor Number: </td>
            <td align="left" class ="Text"><%# DataBinder.Eval(Container.DataItem, "InvestorNum")%></td>            
        </tr>    
        <tr>
            <td align="right" class="sidelinks">Interest Rate*: </td>
            <td align="left" class ="Text"><%# DataBinder.Eval(Container.DataItem, "InterestRate", "{0:F3}")%>%</td>            
            <td align="right" class="sidelinks">Investor Name: </td>
            <td align="left" class ="Text"><%# DataBinder.Eval(Container.DataItem, "InvestorName")%></td>            
        </tr>  
        <tr>
            <td align="right" class="sidelinks">Term*: </td>
            <td align="left" class ="Text"><%# DataBinder.Eval(Container.DataItem, "TermLengthDesc")%></td>            
            <td align="right" class="sidelinks">Investor Loan ID: </td>
            <td align="left" class ="Text"><%# DataBinder.Eval(Container.DataItem, "InvestorLoanNum")%></td>
        </tr>     
        <tr>
            <td align="right" class="sidelinks">Loan Delinquency Status*: </td>
            <td align="left" class ="Text"><%# DataBinder.Eval(Container.DataItem, "LoanDelinquencyDesc")%></td>
            <td align="right" class="sidelinks"></td>
            <td align="left" class ="Text"></td>
        </tr>
    </ItemTemplate>
    <SeparatorTemplate>
        <tr>
        <td colspan="4"><hr /></td>
        </tr>
    </SeparatorTemplate>
</asp:DataList>
</Div>
