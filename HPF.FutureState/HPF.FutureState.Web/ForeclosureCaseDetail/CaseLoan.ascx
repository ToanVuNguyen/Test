﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaseLoan.ascx.cs" Inherits="HPF.FutureState.Web.ForeclosureCaseDetail.CaseLoan" %>
<br />
<div class="sidelinks"><h1>Loan Details:</h1></div>
<Div style="Height:210px;Overflow:Auto">
&nbsp;&nbsp;&nbsp;<asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
<asp:DataList ID="dtlCaseLoan" runat="server" Width=100% >        
    <ItemTemplate>
        <tr>
            <td align="right" class="sidelinks">Loan Number*: </td>
            <td align="left"><%# DataBinder.Eval(Container.DataItem, "AcctNum")%></td>
            <td align="right" class="sidelinks">Current Loan Balance: </td>
            <td align="left"><%# DataBinder.Eval(Container.DataItem, "CurrentLoanBalanceAmt", "{0:C}")%></td>
        </tr>
        <tr>
            <td align="right" class="sidelinks">Servicer*: </td>
            <td align="left"><%# DataBinder.Eval(Container.DataItem, "ServicerName")%></td>
            <td align="right" class="sidelinks">Original Loan Amount: </td>
            <td align="left"><%# DataBinder.Eval(Container.DataItem, "OrigLoanAmt", "{0:C}")%></td>
            
        </tr>       
        <tr>
            <td align="right" class="sidelinks">Other Servicer Name: </td>
            <td align="left"><%# DataBinder.Eval(Container.DataItem, "OtherServicerName")%></td>
            <td align="right" class="sidelinks">Originating Lender: </td>
            <td align="left"><%# DataBinder.Eval(Container.DataItem, "OriginatingLenderName")%></td>            
        </tr>       
        <tr>
            <td align="right" class="sidelinks">Mortgage Type*: </td>
            <td align="left"><%# DataBinder.Eval(Container.DataItem, "MortgageTypeCd")%></td>
            <td align="right" class="sidelinks">Orig. Mortgage Co FDIC/NCUS: </td>
            <td align="left"><%# DataBinder.Eval(Container.DataItem, "OrigMortgageCoFdicNcusNum")%></td>            
        </tr>       
        <tr>
            <td align="right" class="sidelinks">Loan 1<sup>st</sup> 2<sup>nd</sup> Code: </td>
            <td align="left"><%# DataBinder.Eval(Container.DataItem, "Loan1st2nd")%></td>
            <td align="right" class="sidelinks">Orig. Mortgage Co. Name: </td>
            <td align="left"><%# DataBinder.Eval(Container.DataItem, "OrigMortgageCoName")%></td>            
        </tr>       
         <tr>
            <td align="right" class="sidelinks">ARM Reset Indicator: </td>
            <td align="left"><%# DataBinder.Eval(Container.DataItem, "ArmResetInd")%></td>
            <td align="right" class="sidelinks">Orginal Loan Number: </td>
            <td align="left"><%# DataBinder.Eval(Container.DataItem, "OrginalLoanNum")%></td>            
        </tr>     
        <tr>
            <td align="right" class="sidelinks">Interest Rate*: </td>
            <td align="left"><%# DataBinder.Eval(Container.DataItem, "InterestRate", "{0:P3}")%></td>
            <td align="right" class="sidelinks">Current Servicer FDIC/NCUS: </td>
            <td align="left"><%# DataBinder.Eval(Container.DataItem, "FdicNcusNumCurrentServicerTbd")%></td>            
        </tr>    
        <tr>
            <td align="right" class="sidelinks">Term*: </td>
            <td align="left"><%# DataBinder.Eval(Container.DataItem, "TermLengthCd")%></td>
            <td align="right" class="sidelinks">Investor Loan ID: </td>
            <td align="left"><%# DataBinder.Eval(Container.DataItem, "InvestorLoanNum")%></td>            
        </tr>  
        <tr>
            <td align="right" class="sidelinks">Loan Delinquency Status*: </td>
            <td align="left"><%# DataBinder.Eval(Container.DataItem, "LoanDelinqStatusCd")%></td>
            <td align="right" class="sidelinks"></td>
            <td align="left"></td>
        </tr>     
    </ItemTemplate>
    <SeparatorTemplate>
        <tr>
        <td colspan="4"><hr /></td>
        </tr>
    </SeparatorTemplate>
</asp:DataList>
</Div>
