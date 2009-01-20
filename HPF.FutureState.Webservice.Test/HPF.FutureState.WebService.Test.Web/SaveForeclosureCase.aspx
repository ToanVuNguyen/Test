<%@ Page Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="SaveForeclosureCase.aspx.cs" Inherits="HPF.FutureState.WebService.Test.Web.SaveForeclosureCase" Title="HPF Webservice Test Application - Save Foreclosure Case" %>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    
    <div style="text-align:left"><h1>Save Foreclosure Case</h1></div>
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
            
            <asp:Label CssClass="sidelinks"  ID="Label120" runat="server" Text="Username" ></asp:Label>
                    </td>
                    <td>
            <asp:TextBox CssClass="Text" ID="txtUsername" runat="server" Text="admin" Width="128px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
            <asp:Label CssClass="sidelinks" ID="Label121" runat="server" Text="Password"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox CssClass="Text" ID="txtPassword" runat="Server" Text="admin" Width="128px"></asp:TextBox>
            
                    </td>
                </tr>
                
            </table>
            <br />
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
                        <asp:Button id="UploadBtn" Text="Read File" OnClick="UploadBtn_Click" runat="server" Width="105px" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </td>
        </tr>
    </table>
    
    <br />
    
    <asp:Button ID="btnSave" runat="server" Text="Save Foreclosure Case" 
    onclick="btnSave_Click" />
    
    <br />
    <table >
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label1" runat="server" Text="FCID"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtFcID" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>            
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label5" runat="server" Text="Agency ID"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtAgencyID" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label7" runat="server" Text="Agency case number"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtAgencyCaseNumber" runat="server"></asp:TextBox>
            </td>
            
        </tr>
        
        <tr>
            <%--<td>
                <asp:Label CssClass = "sidelinks" ID="Label2" runat="server" Text="Completed datetime"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCompletedDt" runat="server"></asp:TextBox>
            </td>--%>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label6" runat="server" Text="Call id"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCallID" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label3" runat="server" Text="Program id"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtProgramID" runat="server"></asp:TextBox>
            </td>
        </tr>
        
        
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label4" runat="server" Text="agency client number"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtAgencyClientNumber" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label8" runat="server" Text="intake date time"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtIntakeDt" runat="server"></asp:TextBox>
            </td>
        </tr> 
        
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label9" runat="server" Text="income earners code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtIncomeEarnersCd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label10" runat="server" Text="case source code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCaseSourceCd" runat="server"></asp:TextBox>
            </td>
        </tr> 
        
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label11" runat="server" Text="race code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtRaceCd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label12" runat="server" Text="House hold code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtHouseholdCd" runat="server"></asp:TextBox>
            </td>
        </tr> 
        
        <%--<tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label13" runat="server" Text="Never bill reason code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtNeverBillReasonCd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label14" runat="server" Text="Never pay reason code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtNeverPayReasonCd" runat="server"></asp:TextBox>
            </td>
        </tr> --%>
        
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label15" runat="server" Text="dflt reason 1st code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtDfltReason1stCd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label16" runat="server" Text="dflt reason 2nd code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtDfltReason2ndCd" runat="server"></asp:TextBox>
            </td>
        </tr> 
        
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label17" runat="server" Text="hud termination reason code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtHudTerminationReasonCd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label18" runat="server" Text="hud termination datetime"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtHudTerminationDt" runat="server"></asp:TextBox>
            </td>
        </tr> 
        
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label21" runat="server" Text="Counseling duration code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCounselingDurationCd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label22" runat="server" Text="gender code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtGenderCd" runat="server"></asp:TextBox>
            </td>            
        </tr>
        
         
        
         
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label19" runat="server" Text="hud outcome code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtHudOutcomeCd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label27" runat="server" Text="borrower ssn"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtBorrowerSSN" runat="server"></asp:TextBox>
            </td>
            <%--<td>
                <asp:Label CssClass = "sidelinks" ID="Label20" runat="server" Text="AMI percentage"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtAMIPercentage" runat="server"></asp:TextBox>
            </td>--%>
        </tr> 
        
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label23" runat="server" Text="Borrower first name"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtBorrowerFName" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label24" runat="server" Text="Borrower last name"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtBorrowerLName" runat="server"></asp:TextBox>
            </td>
        </tr>
            
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label25" runat="server" Text="Borrower midle name"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtBorrowerMName" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label26" runat="server" Text="mother maiden last name"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtMotherMaidenLName" runat="server"></asp:TextBox>
            </td>
        </tr>
        
        
        <tr>
            
            <%--<td>
                <asp:Label CssClass = "sidelinks" ID="Label28" runat="server" Text="borrower last 4 ssn"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtBorrowerLast4SSN" runat="server"></asp:TextBox>
            </td>--%>
        </tr> 
        
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label29" runat="server" Text="borrower dob"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtBorrowerDOB" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label30" runat="server" Text="coborrower first name"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCoBorrowerFName" runat="server"></asp:TextBox>
            </td>
        </tr> 
        
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label31" runat="server" Text="coborrower last name"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCoBorrowerLName" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label32" runat="server" Text="coborrower middle name"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCoBorrowerMName" runat="server"></asp:TextBox>
            </td>
        </tr> 
        
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label33" runat="server" Text="coborrower ssn"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCoBorrowerSSN" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label20" runat="server" Text="Hispanic Ind"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtHispanicInd" runat="server"></asp:TextBox>
            </td> 
            <%--<td>
                <asp:Label CssClass = "sidelinks" ID="Label34" runat="server" Text="coborrower last 4 ssn"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCoBorrowerLast4SSN" runat="server"></asp:TextBox>
            </td>--%>
        </tr> 
        
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label35" runat="server" Text="coborrower dob"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCoBorrowerDOB" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label36" runat="server" Text="Assigned counselor id ref"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtAssignedCounselorIDRef" runat="server"></asp:TextBox>
            </td>
        </tr> 
        
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label37" runat="server" Text="primary contact no"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtPrimaryContactNo" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label38" runat="server" Text="second contact no"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtSecondContactNo" runat="server"></asp:TextBox>
            </td>                       
        </tr>      

        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label39" runat="server" Text="email 1"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtEmail1" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label40" runat="server" Text="email 2"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtEmail2" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label41" runat="server" Text="Contact addr. 1"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtContactAddress1" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label42" runat="server" Text="Contact addr. 2"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtContactAddress2" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label43" runat="server" Text="contact city"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtContactCity" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label44" runat="server" Text="contact state code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtContactStateCd" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label45" runat="server" Text="contact zip"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtContactZip" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label46" runat="server" Text="contact zip plus 4"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtContactZipPlus4" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label47" runat="server" Text="Property addr. 1"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtPropertyAddress1" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label48" runat="server" Text="Property addr. 2"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtPropertyAddress2" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label49" runat="server" Text="Property city"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtPropertyCity" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label50" runat="server" Text="Property state code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtPropertyStateCd" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label51" runat="server" Text="Property zip"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtPropertyZip" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label52" runat="server" Text="Property zip plus 4"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtPropertyZipPlus4" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label53" runat="server" Text="Bankruptcy ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtBankruptcyInd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label54" runat="server" Text="Bankruptcy attorney"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtBankruptcyAttorney" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label55" runat="server" Text="Bankruptcy pmt. current ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtBankruptcyPmtCurrentInd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label56" runat="server" Text="borrower edu. level completed code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtBorrowerEducLevelCompletedInd" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label57" runat="server" Text="borrower marital status code  "></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtBorrowerMaritalStatusCd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label58" runat="server" Text="Borrower preferred lang. code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtBorrowerPreferedLangCd" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label59" runat="server" Text="Borrower occupation code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtBorrowerOccupationCd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label60" runat="server" Text="Coborrrower occupation code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCoBorrowerOccupationCd" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            
            <%--<td>
                <asp:Label CssClass = "sidelinks" ID="Label62" runat="server" Text="Duplicate ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtDuplicateInd" runat="server"></asp:TextBox>
            </td>  --%>                     
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label63" runat="server" Text="Fc notice received ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtFcNoticeReceivedInd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label67" runat="server" Text="agency media consent ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtAgencyMediaConsentInd" runat="server"></asp:TextBox>
            </td>
            <%--<td>
                <asp:Label CssClass = "sidelinks" ID="Label64" runat="server" Text="Case complete ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCaseCompleteInd" runat="server"></asp:TextBox>
            </td>  --%>                     
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label65" runat="server" Text="Funding consent ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtFundingConsentInd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label66" runat="server" Text="Servicer consent ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtServicerConsentInd" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            
            <%--<td>
                <asp:Label CssClass = "sidelinks" ID="Label68" runat="server" Text="hpf media candidate ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtHpfMediaCandidateInd" runat="server"></asp:TextBox>
            </td>  --%>                     
        </tr>   
        <%--<tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label69" runat="server" Text="hpf network candidate ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtHpfNetworkCandidateInd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label70" runat="server" Text="hpf success story ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtHpfSuccessStoryInd" runat="server"></asp:TextBox>
            </td>                       
        </tr>   --%>
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label71" runat="server" Text="Borrower disabled ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtBorrowerDisabledInd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label72" runat="server" Text="Coborrower disabled ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCoBorrowerDisabledInd" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label73" runat="server" Text="Summary sent other code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtSummarySentOtherCd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label74" runat="server" Text="Summary sent other datetime"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtSummarySentOtherDt" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <%--<td>
                <asp:Label CssClass = "sidelinks" ID="Label75" runat="server" Text="Summary sent datetime"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtSummarySentDt" runat="server"></asp:TextBox>
            </td>--%>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label103" runat="server" Text="Household gross annual income amt."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtHousholdGrossAnnualIncomeAmt" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label76" runat="server" Text="Occupant num"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtOccupantNum" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label77" runat="server" Text="Loan default reason notes"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtLoanDfltReasonNotes" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label78" runat="server" Text="Prim res. est. mkt. value"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtPrimResEstMktValue" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label79" runat="server" Text="counselor last name"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCounselorLastName" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label80" runat="server" Text="counselor first name"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCounselorFirstName" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label81" runat="server" Text="Counselor email"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCounselorEmail" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label82" runat="server" Text="Counselor phone"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCounselorPhone" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label83" runat="server" Text="Counselor ext."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCounselorExt" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label84" runat="server" Text="Discussed sol. with srvcr. ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtDiscussedSolutionWithSrvcrInd" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label85" runat="server" Text="Worked with another agency ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtWorkedWithAnotherAgencyInd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label86" runat="server" Text="Contacted srvcr. recently ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtContactedSrvcrRecentlyInd" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label87" runat="server" Text="Has workout plan ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtHasWorkoutPlanInd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label88" runat="server" Text="Srvcr workout plan ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtSrvcrWorkoutPlanInd" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <%--<td>
                <asp:Label CssClass = "sidelinks" ID="Label89" runat="server" Text="FC sale date set ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtFcSaleDateSetInd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label90" runat="server" Text="Opt out newsletter ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtOptOutNewsletterInd" runat="server"></asp:TextBox>
            </td>--%>
        </tr>   
        <tr>
            <%--<td>
                <asp:Label CssClass = "sidelinks" ID="Label91" runat="server" Text="Opt out survey ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtOptOutSurveyInd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label92" runat="server" Text="Do not call ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtDoNotCallInd" runat="server"></asp:TextBox>
            </td>                       --%>
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label93" runat="server" Text="Owner occupied ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtOwnerOccupiedInd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label94" runat="server" Text="Primary residence ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtPrimaryResidenceInd" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label95" runat="server" Text="Realty company"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtRealtyCompany" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label96" runat="server" Text="Property code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtPropertyCd" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label97" runat="server" Text="For Sale ind."></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtForSaleInd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label98" runat="server" Text="Home sale price"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtHomeSalePrice" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label99" runat="server" Text="Home purchase year"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtHomePurchaseYear" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label100" runat="server" Text="Home purchase price"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtHomePurchasePrice" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label101" runat="server" Text="Home current market value"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtHomeCurMktValue" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label102" runat="server" Text="Military service code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtMilitaryServiceCd" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            
            <%--<td>
                <asp:Label CssClass = "sidelinks" ID="Label104" runat="server" Text="loan list"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtLoanList" runat="server"></asp:TextBox>
            </td>   --%>                    
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label105" runat="server" Text="intake credit score"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtIntakeCreditScore" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label106" runat="server" Text="Intake credit bureau code"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtIntakeCreditBureauCd" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label119" runat="server" Text="FC Sale Date"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtFcSaleDate" runat="server"></asp:TextBox>
            </td>  
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label108" runat="server" Text="Action item notes"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtActionItemsNotes" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label109" runat="server" Text="Follow up notes"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtFollowupNotes" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label110" runat="server" Text="Agency Success Story"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtAgencySuccessStory" runat="server"></asp:TextBox>
            </td>                       
        </tr>   
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label111" runat="server" Text="Working User ID"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtWorkingUserID" runat="server"></asp:TextBox>
            </td>
            <%--<td>
                <asp:Label CssClass = "sidelinks" ID="Label112" runat="server" Text="Change Last User ID"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtChangeLastUserID" runat="server"></asp:TextBox>
            </td>--%>                       
        </tr>   
        <tr>
             
                                             
        </tr>                  
    </table>

    
        
    <br />
    <asp:Label ID="Label115" runat="server" Text="Case Loan"></asp:Label>
    <br />
    <asp:GridView ID="grdvCaseLoan" runat="server" 
        ShowFooter = "true" 
        AutoGenerateColumns = "false" 
        onrowediting="grdvCaseLoan_RowEditing"
        OnRowCancelingEdit = "grdvCaseLoan_RowCancelEditing"
        onrowupdating="grdvCaseLoan_RowUpdating"
        onrowcommand="grdvCaseLoan_RowCommand" 
        onrowdeleting="grdvCaseLoan_RowDeleting">
        <Columns>
            
            <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                <EditItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                  <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <FooterTemplate>
                  <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="AddNew" Text="New"></asp:LinkButton>
                </FooterTemplate>
                <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                </ItemTemplate>
                </asp:TemplateField>            
            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ShowHeader="false" />
            <asp:TemplateField HeaderText="ServicerId" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtServicerId" runat="server" Text='<%# Eval("ServicerId") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtServicerId" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ServicerId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Other Service name">  
                <EditItemTemplate>
                    <asp:TextBox ID="txtOtherServiceName" runat="server" Text='<%# Eval("OtherServicerName") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtOtherServiceName" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("OtherServicerName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Acc num" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtAccNum" runat="server" Text='<%# Eval("AcctNum") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtAccNum" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("AcctNum") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Loan 1st 2nd" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtLoan1st2nd" runat="server" Text='<%# Eval("Loan1st2nd") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtLoan1st2nd" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Loan1st2nd") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Morgage type code" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtMorgageTypeCode" runat="server" Text='<%# Eval("MortgageTypeCd") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtMorgageTypeCode" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("MortgageTypeCd") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Arm reset indicator" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtArmResetInd" runat="server" Text='<%# Eval("ArmResetInd") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtArmResetInd" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("ArmResetInd") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Term length code" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtTermLengthCd" runat="server" Text='<%# Eval("TermLengthCd") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtTermLengthCd" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("TermLengthCd") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Loan delinq status code" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtLoanDelinqCd" runat="server" Text='<%# Eval("LoanDelinqStatusCd") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtLoanDelinqCd" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("LoanDelinqStatusCd") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Current loan balance amt" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtCurrentLoanBalanceAmt" runat="server" Text='<%# Eval("CurrentLoanBalanceAmt") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtCurrentLoanBalanceAmt" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("CurrentLoanBalanceAmt") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Orig loan amt" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtOrigLoanAmt" runat="server" Text='<%# Eval("OrigLoanAmt") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtOrigLoanAmt" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("OrigLoanAmt") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Interest rate" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtInterestRate" runat="server" Text='<%# Eval("InterestRate") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtInterestRate" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("InterestRate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Originating lender name" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtOrigLenderName" runat="server" Text='<%# Eval("OriginatingLenderName") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtOrigLenderName" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("OriginatingLenderName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FDIC NCUS num" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtOrigMorgageNum" runat="server" Text='<%# Eval("OrigMortgageCoFdicNcusNum") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtOrigMorgageNum" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label114" runat="server" Text='<%# Bind("OrigMortgageCoFdicNcusNum") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="mortgage con name" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtOrigMotgateConName" runat="server" Text='<%# Eval("OrigMortgageCoName") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtOrigMotgateConName" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label15" runat="server" Text='<%# Bind("OrigMortgageCoName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Original loan num" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtOrigLoanNum" runat="server" Text='<%# Eval("OrginalLoanNum") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtOrigLoanNum" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label16" runat="server" Text='<%# Bind("OrginalLoanNum") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FDIC NCUA" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtFDICNCUANum" runat="server" Text='<%# Eval("FdicNcusNumCurrentServicerTbd") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtFDICNCUANum" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label17" runat="server" Text='<%# Bind("FdicNcusNumCurrentServicerTbd") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Current servicer" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtCurrentServiceNameTBD" runat="server" Text='<%# Eval("CurrentServicerNameTbd") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtCurrentServiceNameTBD" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label18" runat="server" Text='<%# Bind("CurrentServicerNameTbd") %>'></asp:Label>
                </ItemTemplate>                
                
            </asp:TemplateField>
            
            <%--<asp:TemplateField HeaderText="Freddie loan num" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtFreddieLoanNum" runat="server" Text='<%# Eval("InvestorLoanNum") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtFreddieLoanNum" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label19" runat="server" Text='<%# Bind("InvestorLoanNum") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="FCID" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtFcId" runat="server" Text='<%# Eval("FcId") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtFcId" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label22" runat="server" Text='<%# Bind("FcId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%> 
            <asp:TemplateField HeaderText="Case Loan ID" >  
                <%--<EditItemTemplate>
                    <asp:TextBox ID="txtCaseLoanId" runat="server" Text='<%# Eval("CaseLoanId") %>'></asp:TextBox>
                </EditItemTemplate>--%>
                <FooterTemplate>
                    <asp:Label ID="lblCaseLoanId" runat="server"></asp:Label> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblCaseLoanId" runat="server" Text='<%# Bind("CaseLoanId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>          
        </Columns> 
    </asp:GridView>
    <br />
    <asp:Label ID="Label116" runat="server" Text="Budget Item"></asp:Label>
    <br />
    <asp:GridView ID="grdvBudgetItem" runat="server" 
        ShowFooter = "true" 
        AutoGenerateColumns = "false" 
        onrowediting="grdvBudgetItem_RowEditing"
        OnRowCancelingEdit = "grdvBudgetItem_RowCancelEditing"
        onrowupdating="grdvBudgetItem_RowUpdating"
        onrowcommand="grdvBudgetItemRowCommand" 
        onrowdeleting="grdvBudgetItem_RowDeleting">
        <Columns>
            <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="AddNew" Text="New"></asp:LinkButton>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>            
            
            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ShowHeader="false" />
            
            
            <asp:TemplateField HeaderText="Amount" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtBudgetItemAmt" runat="server" Text='<%# Eval("BudgetItemAmt") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtBudgetItemAmt" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("BudgetItemAmt") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Note" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtBudgetNote" runat="server" Text='<%# Eval("BudgetNote") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtBudgetNote" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("BudgetNote") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            
            <asp:TemplateField HeaderText="Subcategory ID" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtBudgetSubcategoryId" runat="server" Text='<%# Eval("BudgetSubcategoryId") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtBudgetSubcategoryId" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("BudgetSubcategoryId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <%--<asp:TemplateField HeaderText="Set ID" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtBudgetItemSetId" runat="server" Text='<%# Eval("BudgetSetId") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtBudgetItemSetId" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("BudgetSetId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
            
            <asp:TemplateField HeaderText="ID" >  
                <%--<EditItemTemplate>
                    <asp:TextBox ID="txtBudgetItemId" runat="server" Text='<%# Eval("BudgetItemId") %>'></asp:TextBox>
                </EditItemTemplate>--%>
                <FooterTemplate>
                    <asp:Label ID="lblBudgetItemId" runat="server"></asp:Label> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblBudgetItemId" runat="server" Text='<%# Bind("BudgetItemId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>            
    </asp:GridView>
    <br />
    <asp:Label ID="Label113" runat="server" Text="Budget Asset"></asp:Label>
    <asp:GridView ID="grdvBudgetAsset" runat="server" 
        ShowFooter = "true" 
        AutoGenerateColumns = "false" 
        onrowediting="grdvBudgetAsset_RowEditing"
        OnRowCancelingEdit = "grdvBudgetAsset_RowCancelEditing"
        onrowupdating="grdvBudgetAsset_RowUpdating"
        onrowcommand="grdvBudgetAssetRowCommand" 
        onrowdeleting="grdvBudgetAsset_RowDeleting">
        <Columns>
            <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="AddNew" Text="New"></asp:LinkButton>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>            
            
            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ShowHeader="false" />
            
            <asp:TemplateField HeaderText="Asset name" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtAssetName" runat="server" Text='<%# Eval("AssetName") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtAssetName" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("AssetName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Asset value" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtAssetValue" runat="server" Text='<%# Eval("AssetValue") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtAssetValue" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("AssetValue") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <%--<asp:TemplateField HeaderText="Budget ID" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtBudgetID" runat="server" Text='<%# Eval("BudgetSetId") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtBudgetID" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("BudgetSetId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
            
            <asp:TemplateField HeaderText="Budget Asset ID" >  
                <%--<EditItemTemplate>
                    <asp:TextBox ID="txtServicerId" runat="server" Text='<%# Eval("BudgetAssetId") %>'></asp:TextBox>
                </EditItemTemplate>--%>
                <FooterTemplate>
                    <asp:Label ID="lblBudgetAssetId" runat="server"></asp:Label> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblBudgetAssetId" runat="server" Text='<%# Bind("BudgetAssetId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>            
    </asp:GridView>
    <br />
    
    <asp:Label ID="Label117" runat="server" Text="Activity Log" Visible ="false" ></asp:Label>
    <br />
    <asp:GridView ID="grdvActivityLog" runat="server" 
        ShowFooter = "true" 
        AutoGenerateColumns = "false" 
        onrowediting="grdvActivityLog_RowEditing"
        OnRowCancelingEdit = "grdvActivityLog_RowCancelEditing"
        onrowupdating="grdvActivityLog_RowUpdating"
        onrowcommand="grdvActivityLogRowCommand" 
        onrowdeleting="grdvActivityLog_RowDeleting"
        visible = "false" >
        <Columns>
            <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="AddNew" Text="New"></asp:LinkButton>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>            
            
            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ShowHeader="false" />
            
            
            <asp:TemplateField HeaderText="Code" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtActivityCode" runat="server" Text='<%# Eval("ActivityCd") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtActivityCode" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ActivityCd") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Note" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtActivityNote" runat="server" Text='<%# Eval("ActivityNote") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtActivityNote" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ActivityNote") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            
            <asp:TemplateField HeaderText="Date" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtActivityDt" runat="server" Text='<%# Eval("ActivityDt") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtActivityDt" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("ActivityDt") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Fc ID" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtFcId" runat="server" Text='<%# Eval("FcId") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtFcId" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("FcId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="ID" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtActivityLogId" runat="server" Text='<%# Eval("ActivityLogId") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtActivityLogId" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("ActivityLogId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>            
    </asp:GridView>
    <br />
    <asp:Label ID="Label118" runat="server" Text="Outcome"></asp:Label>
    <br />
    <asp:GridView ID="grdvOutcomeItem" runat="server" 
        ShowFooter = "true" 
        AutoGenerateColumns = "false" 
        onrowediting="grdvOutcomeItem_RowEditing"
        OnRowCancelingEdit = "grdvOutcomeItem_RowCancelEditing"
        onrowupdating="grdvOutcomeItem_RowUpdating"
        onrowcommand="grdvOutcomeItemRowCommand" 
        onrowdeleting="grdvOutcomeItem_RowDeleting">
        <Columns>
            <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="AddNew" Text="New"></asp:LinkButton>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>            
            
            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ShowHeader="false" />
            
            
            <asp:TemplateField HeaderText="ExtRefOtherName" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtExtRefOtherName" runat="server" Text='<%# Eval("ExtRefOtherName") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtExtRefOtherName" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ExtRefOtherName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="NonprofitReferralKeyNum" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtNonprofitreferralKeyNum" runat="server" Text='<%# Eval("NonprofitreferralKeyNum") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtNonprofitreferralKeyNum" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("NonprofitreferralKeyNum") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Outcome Type Id" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtOutcomeTypeId" runat="server" Text='<%# Eval("OutcomeTypeId") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtOutcomeTypeId" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("OutcomeTypeId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            
            
            <%--<asp:TemplateField HeaderText="Fc ID" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtFcId" runat="server" Text='<%# Eval("FcId") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtFcId" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("FcId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Outcome Set Id" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtOutcomeSetId" runat="server" Text='<%# Eval("OutcomeSetId") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtOutcomeSetId" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("OutcomeSetId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Deleted Date" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtOutcomeDeletedDt" runat="server" Text='<%# Eval("OutcomeDeletedDt") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtOutcomeDeletedDt" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("OutcomeDeletedDt") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Date" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtOutcomeDt" runat="server" Text='<%# Eval("OutcomeDt") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtOutcomeDt" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("OutcomeDt") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField--%>
            <asp:TemplateField HeaderText="ID" >  
                <%--<EditItemTemplate>
                    <asp:TextBox ID="txtOutcomeItemId" runat="server" Text='<%# Eval("OutcomeItemId") %>'></asp:TextBox>
                </EditItemTemplate>--%>
                <FooterTemplate>
                    <asp:Label ID="lblOutcomeItemId" runat="server"></asp:Label> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblOutcomeItemId" runat="server" Text='<%# Bind("OutcomeItemId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>            
    </asp:GridView>
    
    <br />
    <asp:Label ID="lblMessage" runat="server" Text="Message:"></asp:Label>
    
    <br />
    <asp:GridView ID="grdvMessages" runat="server">
    </asp:GridView>
</asp:Content>
