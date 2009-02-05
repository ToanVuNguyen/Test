
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
            <asp:TextBox CssClass="Text" ID="txtUsername" runat="server" Text="other" Width="128px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
            <asp:Label CssClass="sidelinks" ID="Label29" runat="server" Text="Password"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox CssClass="Text" ID="txtPassword" runat="Server" Text="other" Width="128px"></asp:TextBox>
            
                    </td>
                </tr>
                
            </table>
            admin/admin -> both rights
            <br />
            callcenter/callcenter -> call center right
            <br />
            other/other ->call center but OTHER type
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

    
    <asp:Table ID="Table1" runat="server">
        
        
        
        <asp:TableRow ID="TableRow1" runat="server">
            <%--<asp:TableCell ID="TableCell1" runat="server">
                <asp:Label CssClass="sidelinks" ID="Label1" runat="server" Text="Call Center ID"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell2" runat="server">
                <asp:TextBox CssClass="Text" ID="txtCallCenterID" runat="server" Width="128px"></asp:TextBox>
            </asp:TableCell>--%>
            
            <asp:TableCell ID="TableCell35" runat="server">
            <asp:Label CssClass="sidelinks" ID="Label18" runat="server" Text="Screen Rout"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell36" runat="server">
                <asp:TextBox CssClass="Text" ID="txtScreenRout" runat="server" Width="128px"></asp:TextBox>            
            </asp:TableCell>  
            
            <asp:TableCell ID="TableCell3" runat="server">
                <asp:Label CssClass="sidelinks" ID="Label2" runat="server" Text="CC Agency Id"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell4" runat="server">
                <asp:TextBox CssClass="Text" ID="txtCcAgentIdKey" runat="Server" Width="128px"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow3" runat="server">
            <asp:TableCell ID="TableCell5" runat="server">
                <asp:Label CssClass="sidelinks" ID="Label3" runat="server" Text="Start Date"></asp:Label>
             </asp:TableCell>
            <asp:TableCell ID="TableCell6" runat="server">
            <asp:TextBox CssClass="Text" ID="txtStartDate" runat="server" Width="128px" ></asp:TextBox>
            </asp:TableCell>
            
            <asp:TableCell ID="TableCell7" runat="server">
                <asp:Label CssClass="sidelinks" ID="Label4" runat="server" Text="End Date"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell8" runat="server">
                <asp:TextBox CssClass="Text" ID="txtEndDate" runat="server" Width="128px" ></asp:TextBox>
            </asp:TableCell>
            
        </asp:TableRow>
        
        
        <asp:TableRow ID="TableRow6" runat="server">
             <asp:TableCell ID="TableCell9" runat="server">
         <asp:Label CssClass="sidelinks" ID="Label5" runat="server" Text="DNIS"></asp:Label>
         </asp:TableCell>
            <asp:TableCell ID="TableCell10" runat="server">
                <asp:TextBox CssClass="Text" ID="txtDNIS" runat="server" Width="128px"></asp:TextBox>
            </asp:TableCell>
             <asp:TableCell ID="TableCell13" runat="server">
            <asp:Label CssClass="sidelinks" ID="Label7" runat="server" Text="Call Center"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell14" runat="server">
                <asp:TextBox CssClass="Text"  ID="txtCallCenter" runat="server" Width="128px" ></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow7" runat="server">
           
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow8" runat="server">
            <asp:TableCell ID="TableCell15" runat="server">
            <asp:Label CssClass="sidelinks" ID="Label8" runat="server" Text="Call source code"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell16" runat="server">
                <asp:TextBox CssClass="Text" ID="txtCallSourceCd" runat="server" Width="128px"></asp:TextBox>            
            </asp:TableCell>
            
            <asp:TableCell ID="TableCell17" runat="server">
            <asp:Label CssClass="sidelinks" ID="Label9" runat="server" Text="Calling reason"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell18" runat="server">
                <asp:TextBox CssClass="Text" ID="txtReasonToCall" runat="server" Width="128px"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow9" runat="server">
            
        </asp:TableRow>
        <asp:TableRow ID="TableRow10" runat="server">
            <asp:TableCell ID="TableCell19" runat="server">
            <asp:Label CssClass="sidelinks" ID="Label10" runat="server" Text="Acct. Number"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell20" runat="server">
                <asp:TextBox CssClass="Text" ID="txtLoanAccountNumber" runat="server" Width="128px"></asp:TextBox>
            </asp:TableCell>
            
            <asp:TableCell ID="TableCell21" runat="server">
            <asp:Label CssClass="sidelinks" ID="Label11" runat="server" Text="Service ID"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell22" runat="server">
                <asp:TextBox CssClass="Text" ID="txtServiceID" runat="server" Width="128px"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow11" runat="server">
            
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow12" runat="server">
            <asp:TableCell ID="TableCell23" runat="server">
            <asp:Label CssClass="sidelinks" ID="Label12" runat="server" Text="FirstName"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell24" runat="server">
                <asp:TextBox CssClass="Text" ID="txtFirstName" runat="server" Width="128px"></asp:TextBox>            
            </asp:TableCell>
            
             <asp:TableCell ID="TableCell25" runat="server">
            <asp:Label CssClass="sidelinks" ID="Label13" runat="server" Text="LastName"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell26" runat="server">
                <asp:TextBox CssClass="Text" ID="txtLastName" runat="server" Width="128px"></asp:TextBox>
            
            </asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow14" runat="server">
            <asp:TableCell ID="TableCell27" runat="server">
            <asp:Label CssClass="sidelinks" ID="Label14" runat="server" Text="Other Servicer Name"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell28" runat="server">
                <asp:TextBox CssClass="Text" ID="txtOtherServicerName" runat="server" Width="128px"></asp:TextBox>
            
            </asp:TableCell>
            <asp:TableCell ID="TableCell29" runat="server">
            <asp:Label CssClass="sidelinks" ID="Label15" runat="server" Text="Prop Zip Full"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell30" runat="server">
                <asp:TextBox CssClass="Text" ID="txtPropZipFull9" runat="server" Width="128px"></asp:TextBox>            
            </asp:TableCell> 
        </asp:TableRow>

        
        <asp:TableRow ID="TableRow16" runat="server">
            <asp:TableCell ID="TableCell31" runat="server">
            <asp:Label CssClass="sidelinks" ID="Label16" runat="server" Text="Prev Agency Id"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell32" runat="server">
                <asp:TextBox CssClass="Text" ID="txtPrevAgencyId" runat="server" Width="128px"></asp:TextBox>
            
            </asp:TableCell>
            <asp:TableCell ID="TableCell33" runat="server">
            <asp:Label CssClass="sidelinks" ID="Label17" runat="server" Text="Selected Agency Id"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell34" runat="server">
                <asp:TextBox CssClass="Text" ID="txtSelectedAgencyId" runat="server" Width="128px"></asp:TextBox>
            
            </asp:TableCell>
        </asp:TableRow>
        
        
        <asp:TableRow ID="TableRow18" runat="server">
            
            
            <asp:TableCell ID="TableCell37" runat="server">
            
            </asp:TableCell>
            <asp:TableCell ID="TableCell38" runat="server">
                
            
            </asp:TableCell>         
            
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow20" runat="server">
            <asp:TableCell ID="TableCell39" runat="server">
            <asp:Label CssClass="sidelinks" ID="Label20" runat="server" Text="Trans Number"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell40" runat="server">
                <asp:TextBox CssClass="Text" ID="txtTransNumber" runat="server" Width="128px"></asp:TextBox>
            
            </asp:TableCell>
            
            <asp:TableCell ID="TableCell41" runat="server">
            <asp:Label CssClass="sidelinks" ID="Label21" runat="server" Text="Cc Call Key"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell42" runat="server">
                <asp:TextBox CssClass="Text" ID="txtCcCallKey" runat="server" Width="128px"></asp:TextBox>
            
            </asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow22" runat="server">
            <asp:TableCell ID="TableCell43" runat="server">
            <asp:Label CssClass="sidelinks" ID="Label22" runat="server" Text="Loan Delinq Status Cd"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell44" runat="server">
                <asp:TextBox CssClass="Text" ID="txtLoanDelinqStatusCd" runat="server" Width="128px"></asp:TextBox>
            
            </asp:TableCell>
            
            <asp:TableCell ID="TableCell45" runat="server">
            <asp:Label CssClass="sidelinks" ID="Label23" runat="server" Text="Selected Counselor"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell46" runat="server">
                <asp:TextBox CssClass="Text" ID="txtSelectedCounselor" runat="server" Width="128px"></asp:TextBox>
            
            </asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow24" runat="server">
            <asp:TableCell ID="TableCell47" runat="server">
            <asp:Label CssClass="sidelinks" ID="Label24" runat="server" Text="Homeowner Ind"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell48" runat="server">
                <asp:TextBox CssClass="Text" ID="txtHomeownerInd" runat="server" Width="128px"></asp:TextBox>
            
            </asp:TableCell>
            <asp:TableCell ID="TableCell49" runat="server">
            <asp:Label CssClass="sidelinks" ID="Label25" runat="server" Text="Power Of Attorney Ind"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell50" runat="server">
                <asp:TextBox CssClass="Text" ID="txtPowerOfAttorneyInd" runat="server" Width="128px"></asp:TextBox>           
            </asp:TableCell>
        </asp:TableRow>
                
        <asp:TableRow ID="TableRow26" runat="server">
            <asp:TableCell ID="TableCell51" runat="server">
                <asp:Label CssClass="sidelinks" ID="Label26" runat="server" Text="AuthorizedI nd"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell52" runat="server">
                <asp:TextBox CssClass="Text" ID="txtAuthorizedInd" runat="server" Width="128px"></asp:TextBox>           
            </asp:TableCell>   
            <asp:TableCell ID="TableCell11" runat="server">
                <asp:Label CssClass="sidelinks" ID="Label6" runat="server" Text="Final Dispo Cd"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell12" runat="server">
                <asp:TextBox CssClass="Text" ID="txtFinalDispoCd" runat="server" Width="128px"></asp:TextBox>           
            </asp:TableCell>          
        </asp:TableRow>
        <asp:TableRow ID="TableRow2" runat="server">
            <asp:TableCell ID="TableCell53" runat="server">
                <asp:Label CssClass="sidelinks" ID="Label19" runat="server" Text="City"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell54" runat="server">
                <asp:TextBox CssClass="Text" ID="txtCity" runat="server" Width="128px"></asp:TextBox>           
            </asp:TableCell> 
            
            <asp:TableCell ID="TableCell1" runat="server">
                <asp:Label CssClass="sidelinks" ID="Label1" runat="server" Text="State"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell2" runat="server">
                <asp:TextBox CssClass="Text" ID="txtState" runat="server" Width="128px"></asp:TextBox>           
            </asp:TableCell>                        
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow5" runat="server">
            <asp:TableCell ID="TableCell55" runat="server">
                <asp:Label CssClass="sidelinks" ID="Label27" runat="server" Text="Nonprofit Referral 1"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell56" runat="server">
                <asp:TextBox CssClass="Text" ID="txtNonprofitReferral1" runat="server" Width="128px"></asp:TextBox>           
            </asp:TableCell>   
            <asp:TableCell ID="TableCell57" runat="server">
                <asp:Label CssClass="sidelinks" ID="Label30" runat="server" Text="Nonprofit Referral 2"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell58" runat="server">
                <asp:TextBox CssClass="Text" ID="txtNonprofitReferral2" runat="server" Width="128px"></asp:TextBox>           
            </asp:TableCell>                      
        </asp:TableRow>        
        <asp:TableRow ID="TableRow15" runat="server">
            <asp:TableCell ID="TableCell59" runat="server">
                <asp:Label CssClass="sidelinks" ID="Label31" runat="server" Text="Nonprofit Referral 3"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell60" runat="server">
                <asp:TextBox CssClass="Text" ID="txtNonprofitReferral3" runat="server" Width="128px"></asp:TextBox>           
            </asp:TableCell>                        
        </asp:TableRow>        
    </asp:Table>
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
