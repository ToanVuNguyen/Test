<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RetrieveSummary.aspx.cs" Inherits="HPF.FutureState.WebService.Test.Web.WebForm3" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div style="text-align:left">
        <h1>
            &nbsp;</h1>
    </div>
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
                            <asp:TextBox CssClass="Text" ID="txtPassword" runat="Server" TextMode="Password" Width="128px"></asp:TextBox>
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
                            <td align="center" class="sidelinks" style="width: 131px">
                                <asp:Label CssClass = "sidelinks" ID="Label1" runat="server" Text="FCID"></asp:Label>
                                &nbsp;</td>
                            <td style="width: 141px">
                                <asp:TextBox CssClass = "Text" ID="txtFcID" runat="server"></asp:TextBox>
                                &nbsp;*</td>
                        </tr>
                        <tr>
                            <td align="center" class="sidelinks" style="width: 131px">
                                Report Ouput As</td>
                            <td style="width: 141px">
                                <asp:TextBox CssClass = "Text" ID="txtReportFormat" runat="server"></asp:TextBox>
                                                                    &nbsp;?</td>
                        </tr>
                        <tr>
                            <td align="center" class="sidelinks" style="width: 131px">
                                &nbsp;</td>
                            <td style="width: 141px">
                                [Empty], PDF,EXCEL,CVS,XML</td>
                        </tr>
                    </table>
        <asp:Button ID="btnRetrieveSummary" runat="server" Text="Retrieve Summary" 
    onclick="btnRetrieveSummary_Click" style="width: 202px" />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <asp:Panel ID="Panel1" runat="server" Height="1119px" Visible="False">
            <table >
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label5" runat="server" Text="Agency ID"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtAgencyID" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label7" runat="server" 
                        Text="Agency case number"></asp:Label>
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
                        <asp:Label CssClass = "sidelinks" ID="Label4" runat="server" 
                        Text="agency client number"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtAgencyClientNumber" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label8" runat="server" 
                        Text="intake date time"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtIntakeDt" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label9" runat="server" 
                        Text="income earners code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtIncomeEarnersCd" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label10" runat="server" 
                        Text="case source code"></asp:Label>
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
                        <asp:Label CssClass = "sidelinks" ID="Label12" runat="server" 
                        Text="House hold code"></asp:Label>
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
                        <asp:Label CssClass = "sidelinks" ID="Label15" runat="server" 
                        Text="dflt reason 1st code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtDfltReason1stCd" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label16" runat="server" 
                        Text="dflt reason 2nd code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtDfltReason2ndCd" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label17" runat="server" 
                        Text="hud termination reason code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtHudTerminationReasonCd" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label18" runat="server" 
                        Text="hud termination datetime"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtHudTerminationDt" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label21" runat="server" 
                        Text="Counseling duration code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtCounselingDurationCd" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label22" runat="server" 
                        Text="gender code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtGenderCd" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label19" runat="server" 
                        Text="hud outcome code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtHudOutcomeCd" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label27" runat="server" 
                        Text="borrower ssn"></asp:Label>
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
                        <asp:Label CssClass = "sidelinks" ID="Label23" runat="server" 
                        Text="Borrower first name"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtBorrowerFName" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label24" runat="server" 
                        Text="Borrower last name"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtBorrowerLName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label25" runat="server" 
                        Text="Borrower midle name"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtBorrowerMName" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label26" runat="server" 
                        Text="mother maiden last name"></asp:Label>
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
                        <asp:Label CssClass = "sidelinks" ID="Label29" runat="server" 
                        Text="borrower dob"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtBorrowerDOB" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label30" runat="server" 
                        Text="coborrower first name"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtCoBorrowerFName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label31" runat="server" 
                        Text="coborrower last name"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtCoBorrowerLName" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label32" runat="server" 
                        Text="coborrower middle name"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtCoBorrowerMName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label33" runat="server" 
                        Text="coborrower ssn"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtCoBorrowerSSN" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label20" runat="server" 
                        Text="Hispanic Ind"></asp:Label>
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
                        <asp:Label CssClass = "sidelinks" ID="Label35" runat="server" 
                        Text="coborrower dob"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtCoBorrowerDOB" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label36" runat="server" 
                        Text="Assigned counselor id ref"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtAssignedCounselorIDRef" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label37" runat="server" 
                        Text="primary contact no"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtPrimaryContactNo" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label38" runat="server" 
                        Text="second contact no"></asp:Label>
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
                        <asp:Label CssClass = "sidelinks" ID="Label41" runat="server" 
                        Text="Contact addr. 1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtContactAddress1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label42" runat="server" 
                        Text="Contact addr. 2"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtContactAddress2" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label43" runat="server" 
                        Text="contact city"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtContactCity" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label44" runat="server" 
                        Text="contact state code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtContactStateCd" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label45" runat="server" 
                        Text="contact zip"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtContactZip" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label46" runat="server" 
                        Text="contact zip plus 4"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtContactZipPlus4" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label47" runat="server" 
                        Text="Property addr. 1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtPropertyAddress1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label48" runat="server" 
                        Text="Property addr. 2"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtPropertyAddress2" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label49" runat="server" 
                        Text="Property city"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtPropertyCity" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label50" runat="server" 
                        Text="Property state code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtPropertyStateCd" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label51" runat="server" 
                        Text="Property zip"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtPropertyZip" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label52" runat="server" 
                        Text="Property zip plus 4"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtPropertyZipPlus4" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label53" runat="server" 
                        Text="Bankruptcy ind."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtBankruptcyInd" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label54" runat="server" 
                        Text="Bankruptcy attorney"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtBankruptcyAttorney" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label55" runat="server" 
                        Text="Bankruptcy pmt. current ind."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtBankruptcyPmtCurrentInd" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label56" runat="server" 
                        Text="borrower edu. level completed code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtBorrowerEducLevelCompletedInd" 
                        runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label57" runat="server" 
                        Text="borrower marital status code  "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtBorrowerMaritalStatusCd" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label58" runat="server" 
                        Text="Borrower preferred lang. code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtBorrowerPreferedLangCd" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label59" runat="server" 
                        Text="Borrower occupation code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtBorrowerOccupationCd" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label60" runat="server" 
                        Text="Coborrrower occupation code"></asp:Label>
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
                        <asp:Label CssClass = "sidelinks" ID="Label63" runat="server" 
                        Text="Fc notice received ind."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtFcNoticeReceivedInd" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label67" runat="server" 
                        Text="agency media interest ind."></asp:Label>
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
                        <asp:Label CssClass = "sidelinks" ID="Label65" runat="server" 
                        Text="Funding consent ind."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtFundingConsentInd" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label66" runat="server" 
                        Text="Servicer consent ind."></asp:Label>
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
                        <asp:Label CssClass = "sidelinks" ID="Label71" runat="server" 
                        Text="Borrower disabled ind."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtBorrowerDisabledInd" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label72" runat="server" 
                        Text="Coborrower disabled ind."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtCoBorrowerDisabledInd" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label73" runat="server" 
                        Text="Summary sent other code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtSummarySentOtherCd" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label74" runat="server" 
                        Text="Summary sent other datetime"></asp:Label>
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
                        <asp:Label CssClass = "sidelinks" ID="Label103" runat="server" 
                        Text="Household gross annual income amt."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtHousholdGrossAnnualIncomeAmt" 
                        runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label76" runat="server" 
                        Text="Occupant num"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtOccupantNum" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label77" runat="server" 
                        Text="Loan default reason notes"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtLoanDfltReasonNotes" runat="server"></asp:TextBox>
                    </td>
                    <!--
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label78" runat="server" 
                        Text="Prim res. est. mkt. value"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtPrimResEstMktValue" runat="server"></asp:TextBox>
                    </td>-->
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label79" runat="server" 
                        Text="counselor last name"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtCounselorLastName" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label80" runat="server" 
                        Text="counselor first name"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtCounselorFirstName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label81" runat="server" 
                        Text="Counselor email"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtCounselorEmail" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label82" runat="server" 
                        Text="Counselor phone"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtCounselorPhone" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label83" runat="server" 
                        Text="Counselor ext."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtCounselorExt" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label84" runat="server" 
                        Text="Discussed sol. with srvcr. ind."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtDiscussedSolutionWithSrvcrInd" 
                        runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label85" runat="server" 
                        Text="Worked with another agency ind."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtWorkedWithAnotherAgencyInd" 
                        runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label86" runat="server" 
                        Text="Contacted srvcr. recently ind."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtContactedSrvcrRecentlyInd" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label87" runat="server" 
                        Text="Has workout plan ind."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtHasWorkoutPlanInd" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label88" runat="server" 
                        Text="Srvcr workout plan ind."></asp:Label>
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
                        <asp:Label CssClass = "sidelinks" ID="Label93" runat="server" 
                        Text="Owner occupied ind."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtOwnerOccupiedInd" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label94" runat="server" 
                        Text="Primary residence ind."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtPrimaryResidenceInd" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label95" runat="server" 
                        Text="Realty company"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtRealtyCompany" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label96" runat="server" 
                        Text="Property code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtPropertyCd" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label97" runat="server" 
                        Text="For Sale ind."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtForSaleInd" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label98" runat="server" 
                        Text="Home sale price"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtHomeSalePrice" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label99" runat="server" 
                        Text="Home purchase year"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtHomePurchaseYear" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label100" runat="server" 
                        Text="Home purchase price"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtHomePurchasePrice" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label101" runat="server" 
                        Text="Home current market value"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtHomeCurMktValue" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label102" runat="server" 
                        Text="Military service code"></asp:Label>
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
                        <asp:Label CssClass = "sidelinks" ID="Label105" runat="server" 
                        Text="intake credit score"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtIntakeCreditScore" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label106" runat="server" 
                        Text="Intake credit bureau code"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtIntakeCreditBureauCd" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label119" runat="server" 
                        Text="FC Sale Date"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtFcSaleDate" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label108" runat="server" 
                        Text="Action item notes"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtActionItemsNotes" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label109" runat="server" 
                        Text="Follow up notes"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtFollowupNotes" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label110" runat="server" 
                        Text="Agency Success Story"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtAgencySuccessStory" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass = "sidelinks" ID="Label111" runat="server" 
                        Text="Change last User ID"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass = "Text" ID="txtWorkingUserID" runat="server"></asp:TextBox>
                    </td>
                    <td>                        
                    </td>
                    <td>
                        
                    </td>
                </tr>
                <tr>
                <td class="sidelinks">
                VIP Ind</td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtVipInd" runat="server"></asp:TextBox>
            </td>
            <td class="sidelinks">
                VIP reason</td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtVipReason" runat="server"></asp:TextBox>
            </td>                      
        </tr>   
        <tr>
            <td class="sidelinks">
                Counselelanguage Cd</td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCounseledLanguageCd" runat="server"></asp:TextBox>
            </td>
            <td class="sidelinks">
                Ercp Outcome Cd</td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtErcpOutcomeCd" runat="server"></asp:TextBox>
            </td>                      
        </tr>   
        <tr>
            <td class="sidelinks" style="height: 20px">
                Counselor contated srvcr ind</td>
            <td style="height: 20px">
                <asp:TextBox CssClass = "Text" ID="txtCounselorContactedSrvcrInd" 
                    runat="server"></asp:TextBox>
            </td>
            <td class="sidelinks" style="height: 20px">
                Number Of Units</td>
            <td style="height: 20px">
                <asp:TextBox CssClass = "Text" ID="txtNumberOfUnits" runat="server"></asp:TextBox>
            </td>                      
        </tr>   
        <tr>
            <td class="sidelinks">
                Vacant or condemed Ind</td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtVacantOrCondemedInd" runat="server"></asp:TextBox>
            </td>
            <td class="sidelinks">
                Mortgage Pmt Ratio</td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtMortgagePmtRatio" runat="server"></asp:TextBox>
            </td>
                </tr>
            </table>
            <br />
            <asp:Label ID="Label115" runat="server" CssClass="sidelinks" Text="Case Loan"></asp:Label>
            <asp:GridView ID="grdvCaseLoan" runat="server" CellPadding="4" 
                CssClass="GridViewStyle" ForeColor="#333333">
                <RowStyle BackColor="#EFF3FB" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#336699" CssClass="GridHeader" Font-Size="XX-Small" 
                    ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <br />
            <br />
            <asp:Label ID="Label116" runat="server" CssClass="sidelinks" Text="Budget Item"></asp:Label>
            <br />
            <asp:GridView ID="grdvBudgetItem" runat="server" CellPadding="4" 
                CssClass="GridViewStyle" ForeColor="#333333">
                <RowStyle BackColor="#EFF3FB" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#336699" CssClass="GridHeader" Font-Size="XX-Small" 
                    ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <br />
            <asp:Label ID="Label113" runat="server" CssClass="sidelinks" 
                Text="Budget Asset"></asp:Label>
            <asp:GridView ID="grdvBudgetAsset" runat="server" CellPadding="4" 
                CssClass="GridViewStyle" ForeColor="#333333">
                <RowStyle BackColor="#EFF3FB" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#336699" CssClass="GridHeader" Font-Size="XX-Small" 
                    ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <br />
            <asp:Label ID="Label118" runat="server" CssClass="sidelinks" Text="Outcome"></asp:Label>
            <asp:GridView ID="grdvOutcomeItem" runat="server" CellPadding="4" 
                CssClass="GridViewStyle" ForeColor="#333333">
                <RowStyle BackColor="#EFF3FB" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#336699" CssClass="GridHeader" Font-Size="XX-Small" 
                    ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </asp:Panel>
        <br />
        <br />
        <asp:Label ID="lblMessage" runat="server" Text="Message:" CssClass="sidelinks"></asp:Label>
        <br />
                <asp:GridView ID="grdvMessages" runat="server" CssClass="GridViewStyle" 
        CellPadding="4" ForeColor="#333333" Visible="False">
                    <RowStyle BackColor="#EFF3FB" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle CssClass="GridHeader" BackColor="#336699" Font-Size="XX-Small" 
                        ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
    </div>
</asp:Content>
