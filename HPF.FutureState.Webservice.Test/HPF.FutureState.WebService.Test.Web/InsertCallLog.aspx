
<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InsertCallLog.aspx.cs" Inherits="HPF.FutureState.WebService.Test.Web.InsertCallLog" Title="HPF Webservice Test Application - Search Foreclosure Case" %>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div style="text-align:left"><h1>Insert CallLog</h1></div>
    <table style="border-bottom-style: solid; border-bottom-width: medium; border-bottom-color: #49A3FF" width="100%">
        <tr>
        <td align="left">
            <table>
                <tr>
                    <td align="center" class="sidelinks" colspan="2">
                        Authentication Info</td>
                </tr>
                <tr>
                    <td align="right">
            
            <asp:Label CssClass="sidelinks"  ID="Label28" runat="server" Text="Username" ></asp:Label>
                    </td>
                    <td>
            <asp:TextBox CssClass="Text" ID="txtUsername" runat="server"  Width="128px" 
                            Text="callcenter"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
            <asp:Label CssClass="sidelinks" ID="Label29" runat="server" Text="Password"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox CssClass="Text" ID="txtPassword" runat="Server"  Width="128px" 
                            TextMode="Password"></asp:TextBox>
            
                    </td>
                </tr>
                
            </table>
            admin/admin -&gt; both rights
            <br />
            callcenter/callcenter -&gt; call center right
            <br />
            other/other -&gt;call center but OTHER type
            <br />
        </td>
        </tr>
    </table>
    <div>
    
     <table style="border-bottom-style: solid; border-bottom-width: medium; border-bottom-color: #49A3FF" width="100%">
        <tr>
        <td align="left">
            <table>
                <tr>
                    <td align="center" class="sidelinks">
                        Select file: <asp:FileUpload ID="fileUpload" runat="server"  />
                    </td>
                    <td>
                        <asp:Button id="UploadBtn" Text="Read File" OnClick="UploadBtn_Click" runat="server" Width="105px" TabIndex="1000" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </td>
        </tr>
    </table>
    
    <br />

    
        <table border="0">
            <tr >
                <td>
                    <span class="sidelinks">Screen Rout</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtScreenRout" runat="Server"  Width="128px"></asp:TextBox>
                    </td>
                <td>
                    <span class="sidelinks">CC Agency Id</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtCcAgentIdKey" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
            </tr>
            <tr>
                <td>
                    <span class="sidelinks">Start Date</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtStartDate" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
                <td>
                    <span class="sidelinks">End Date</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtEndDate" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
            </tr>
            <tr>
                <td>
                    <span class="sidelinks">DNIS</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtDNIS" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
                <td>
                    <span class="sidelinks">Call Center</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtCallCenter" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
            </tr>
            <tr>
                <td>
                    <span class="sidelinks">Call source code</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtCallSourceCd" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
                <td>                    
                    <span class="sidelinks">Calling reason</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtReasonToCall" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
            </tr>
            <tr>
                <td>
                    <span class="sidelinks">Acct. Number</span></td>
                <td >
                    <asp:TextBox CssClass="Text" ID="txtLoanAccountNumber" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
                <td>
                    <span class="sidelinks">Service ID</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtServicerID" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
            </tr>
            <tr>
                <td>
                    <span class="sidelinks">FirstName</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtFirstName" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
                <td>
                    <span class="sidelinks">LastName</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtLastName" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
            </tr>
            <tr>
                <td >
                    <span class="sidelinks">Other Servicer Name</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtOtherServicerName" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
                <td>
                    <span class="sidelinks">Prop Zip Full</span></td>
                <td >
                    <asp:TextBox CssClass="Text" ID="txtPropZipFull9" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
            </tr>
            <tr>
                <td>
                    <span class="sidelinks">Prev Agency Id</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtPrevAgencyId" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
                <td>
                    <span class="sidelinks">Selected Agency Id</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtSelectedAgencyId" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
            </tr>
            <tr>
                <td >
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td >
                    <span class="sidelinks">Trans Number</span></td>
                <td >
                    <asp:TextBox CssClass="Text" ID="txtTransNumber" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
                <td>
                    <span class="sidelinks">Cc Call Key</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtCcCallKey" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
            </tr>
            <tr>
                <td>
                    <span class="sidelinks">Loan Delinq Status Cd</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtLoanDelinqStatusCd" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
                <td>                
                    <span class="sidelinks">Selected Counselor</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtSelectedCounselor" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
            </tr>
            <tr >
                <td>
                    <span class="sidelinks">Homeowner Ind</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtHomeownerInd" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
                <td>
                    <span class="sidelinks">Power Of Attorney Ind</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtPowerOfAttorneyInd" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
            </tr>
            <tr>
                <td>
                    <span class="sidelinks">Authorized Ind</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtAuthorizedInd" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
                <td>
                    <span class="sidelinks">Final Dispo Cd</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtFinalDispoCd" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
            </tr>
            <tr>
                <td>
                    <span class="sidelinks">City</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtCity" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
                <td>
                    <span class="sidelinks">State</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtState" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
            </tr>
            <tr>
                <td>
                    <span class="sidelinks">Nonprofit Referral 1</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtNonprofitReferral1" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
                <td>
                    <span class="sidelinks">Nonprofit Referral 2</span></td>
                <td>
                    <asp:TextBox CssClass="Text" ID="txtNonprofitReferral2" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
            </tr>
            <tr>
                <td>
                    <span class="sidelinks">Nonprofit Referral 3</span></td>
                <td >
                    <asp:TextBox CssClass="Text" ID="txtNonprofitReferral3" runat="Server"  Width="128px"></asp:TextBox>
                    </td>                    
                 <td>
                     &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="sidelinks">
                    DelinqInd</td>
                <td >
                    <asp:TextBox CssClass="Text" ID="txtDelinqInd" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
                <td class="sidelinks">
                    Prop Street Address </td>
                <td >
                    <asp:TextBox CssClass="Text" ID="txtPropStreetAddr" runat="Server"  
                        Width="128px"></asp:TextBox>
                                                        </td>
            </tr>
            <tr>
                <td class="sidelinks">
                    Primary Residence Ind </td>
                <td >
                    <asp:TextBox CssClass="Text" ID="txtPrimResInd" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
                <td class="sidelinks">
                    
                    MaxLoan Amount Ind</td>
                <td >
                    <asp:TextBox CssClass="Text" ID="txtLoanAmtInd" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
            </tr>
            <tr>
                <td class="sidelinks">
                    

                    Customer Phone</td>
                <td >
                    <asp:TextBox CssClass="Text" ID="txtCustPhone" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
                <td class="sidelinks">
                    

                    Loan Lookup Cd</td>
                <td >
                    <asp:TextBox CssClass="Text" ID="txtLoanLookupCd" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
            </tr>
            <tr>
                <td class="sidelinks">
                    

                    Originated Prior 2009 Ind </td>
                <td >
                    <asp:TextBox CssClass="Text" ID="txtOrigdateInd" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
                <td class="sidelinks">
                    

                    Payment Amount</td>
                <td >
                    <asp:TextBox CssClass="Text" ID="txtPayment" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
            </tr>
            <tr>
                <td class="sidelinks">
                    

                    Gross Income Amount</td>
                <td >
                    <asp:TextBox CssClass="Text" ID="txtGrossIncome" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
                <td class="sidelinks">
                    

                    DTI Ind</td>
                <td >
                    <asp:TextBox CssClass="Text" ID="txtDTIIndicator" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
            </tr>
            <tr>
                <td class="sidelinks">
                    

                    Servicer CA Number</td>
                <td >
                    <asp:TextBox CssClass="Text" ID="txtServicerCA" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
                <td class="sidelinks">
                    

                    Servicer CA Last Contact Date </td>
                <td >
                    <asp:TextBox CssClass="Text" ID="txtLastSCA" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
            </tr>
            <tr>
                <td class="sidelinks">
                    

                    Servicer CA Id</td>
                <td >
                    <asp:TextBox CssClass="Text" ID="txtServicerIdCA" runat="Server"  Width="128px"></asp:TextBox>
                                                        </td>
                <td class="sidelinks">
                    

                    Servicer CA OtherName</td>
                <td >
                    <asp:TextBox CssClass="Text" ID="txtServicerOtherNameCA" runat="Server"  
                        Width="128px"></asp:TextBox>
                                                        </td>
            </tr>
            <tr class="sidelinks">
                <td class="sidelinks">
                    

                    MHA Info Share Ind</td>
                <td >
                    <asp:TextBox CssClass="Text" ID="txtMHAInfoShare" runat="Server"  Width="128px"></asp:TextBox>
                    </td>
                <td class="sidelinks">
                    

                    ICT Call Id</td>
                <td >
                    <asp:TextBox CssClass="Text" ID="txtICTCallId" runat="Server"  Width="128px"></asp:TextBox>
                    </td>
            </tr>
            <tr class="sidelinks">
                <td class="sidelinks">
                    

                    Servicer Complaint Cd</td>
                <td >
                    <asp:TextBox CssClass="Text" ID="txtServicerComplaintCd" runat="Server"  
                        Width="128px"></asp:TextBox>
                    </td>
                <td class="sidelinks">
                    

                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            </table>
        <br />
    <br />
    <br />
        <asp:Button ID="btnSave" runat="server" Text="Save call log" onclick="btnSave_Click" TabIndex="1" />  <asp:Label CssClass="sidelinks" ID="lblResult" runat="server" Text="Call Log ID: "></asp:Label>
    <br />
    
    
    
    <asp:ListBox ID="lstMessage" runat="server" Height="245px" Width="500px" Visible ="false"></asp:ListBox>
    
    <asp:GridView ID="grdvResult" runat="server" CellPadding="4" 
        ForeColor="#333333" GridLines="None" >
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    
   
    

<br />
<br />
</div>
</div>
    </div>
</asp:Content>
