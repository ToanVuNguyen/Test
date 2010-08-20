﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QCSelectionCaseInfo.aspx.cs"
    Inherits="HPF.FutureState.Web.QCSelectionCaseInfo" Title="QC Selection Case Detail" 
    EnableEventValidation="false" enableViewStateMac="false" viewstateencryptionmode="Never" %>
<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="HPF" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <table style="width: 100%;" align="left">        
        <tr>
            <td align="center">
                <h1>Audit Case Detail</h1>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                 <tr>
                        <td class="sidelinks" align="right">
                            HPF Case ID:
                        </td>
                        <td  class="Text">
                            <asp:Label ID="lblHpfID" runat="server" Text="100987654" ></asp:Label>
                        </td>
                        <td align="right" class="sidelinks">
                            Agency Name:
                        </td>
                        <td class="Text">
                            <asp:Label ID="lblAgencyName" runat="server" Text="Money Management Inc." ></asp:Label>
                        </td>
                        <td align="right" class="sidelinks" >
                            Counselor:
                        </td>
                        <td class="Text">
                            <asp:Label ID="lblCounselor" runat="server" >Amada - Huggenkiss</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="sidelinks" align="right" >
                            Home Owner:                         </td>
                        <td  class="Text" colspan="3">
                            <asp:Label ID="lblHomeOwner" runat="server">Ivan A Mustang</asp:Label>
                            &nbsp;
                        </td>
                        <td align="right" class="sidelinks">
                            Zip Code:
                        </td>
                        <td class="Text">
                            <asp:Label ID="lblZipCode" runat="server" >55416</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="sidelinks" align="right" >
                            Loan Number:
                        </td>
                        <td class="Text" colspan="3" >
                            <asp:Label ID="lblLoanNumber" runat="server" >1298494593 - 
                            Citibank; 554587876 - Bank of America</asp:Label>
                            &nbsp;
                        </td>
                        <td align="right" class="sidelinks">
                            Servicer Name:
                        </td>
                        <td class="Text">
                            <asp:Label ID="lblServicerName" runat="server">Servicer</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="sidelinks" align="right">
                            Evaluation Status:
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblEvaluationStatus" runat="server" CssClass="Text">Agency Input Required</asp:Label>
                        </td>
                        <td align="right" class="sidelinks">
                            Call Date:
                        </td>
                        <td class="Text">
                            <asp:Label ID="lblCallDate" runat="server">09/12/2010</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:BulletedList ID="lblErrorMessage" runat="server" CssClass="ErrorMessage">
                            </asp:BulletedList>
                        </td>
                    </tr>
                </table>
             </td>
        </tr>
       
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="100%" align="left">
                    <tr>
                        <td>
                            <HPF:TabControl ID="tabControl2" runat="server">
                            </HPF:TabControl>
                        </td>
                    </tr>
                    <tr >
                        <td style="border: solid 1px #8FC4F6">
                            <HPF:UserControlLoader ID="UserControlLoader2" runat="server">
                            </HPF:UserControlLoader>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
</asp:Content>
