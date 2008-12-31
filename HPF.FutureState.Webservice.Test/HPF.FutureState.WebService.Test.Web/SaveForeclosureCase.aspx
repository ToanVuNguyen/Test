<%@ Page Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="SaveForeclosureCase.aspx.cs" Inherits="HPF.FutureState.WebService.Test.Web.SaveForeclosureCase" %>
<asp:Content ID="ContentHeader" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">


    <asp:Table ID="Table1" runat="server">
        <asp:TableRow ID="TableRow1" runat="server">
            <asp:TableCell ID="TableCell1" runat="server">
                <asp:Label ID="Label1" runat="server" Text="FCID"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell2" runat="server">
                <asp:TextBox ID="txtFcID" runat="server"></asp:TextBox>
            </asp:TableCell>
            
            <asp:TableCell ID="TableCell9" runat="server">
                <asp:Label ID="Label5" runat="server" Text="Agency ID"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell10" runat="server">
                <asp:TextBox ID="txtAgencyID" runat="server"></asp:TextBox>
            </asp:TableCell>
            
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow2" runat="server">
            <asp:TableCell ID="TableCell3" runat="server">
                <asp:Label ID="Label2" runat="server" Text="completed datetime"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell4" runat="server">
                <asp:TextBox ID="txtCompletedDt" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell11" runat="server">
                <asp:Label ID="Label6" runat="server" Text="Call id"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell12" runat="server">
                <asp:TextBox ID="txtCallID" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow3" runat="server">
            <asp:TableCell ID="TableCell5" runat="server">
                <asp:Label ID="Label3" runat="server" Text="Program id"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell6" runat="server">
                <asp:TextBox ID="txtProgramID" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell13" runat="server">
                <asp:Label ID="Label7" runat="server" Text="agency case number"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell14" runat="server">
                <asp:TextBox ID="txtAgencyCaseNumber" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow4" runat="server">
            <asp:TableCell ID="TableCell7" runat="server">
                <asp:Label ID="Label4" runat="server" Text="agency client number"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell8" runat="server">
                <asp:TextBox ID="txtAgencyClientNumber" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell15" runat="server">
                <asp:Label ID="Label8" runat="server" Text="intake date time"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell16" runat="server">
                <asp:TextBox ID="txtIntakeDt" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow> 
        
        <asp:TableRow ID="TableRow5" runat="server">
            <asp:TableCell ID="TableCell17" runat="server">
                <asp:Label ID="Label9" runat="server" Text="income earners code"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell18" runat="server">
                <asp:TextBox ID="txtIncomeEarnersCd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell19" runat="server">
                <asp:Label ID="Label10" runat="server" Text="case source code"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell20" runat="server">
                <asp:TextBox ID="txtCaseSourceCd" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow> 
        
        <asp:TableRow ID="TableRow6" runat="server">
            <asp:TableCell ID="TableCell21" runat="server">
                <asp:Label ID="Label11" runat="server" Text="race code"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell22" runat="server">
                <asp:TextBox ID="txtRaceCd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell23" runat="server">
                <asp:Label ID="Label12" runat="server" Text="House hold code"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell24" runat="server">
                <asp:TextBox ID="txtHouseholdCd" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow> 
        
        <asp:TableRow ID="TableRow7" runat="server">
            <asp:TableCell ID="TableCell25" runat="server">
                <asp:Label ID="Label13" runat="server" Text="Never bill reason code"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell26" runat="server">
                <asp:TextBox ID="txtNeverBillReasonCd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell27" runat="server">
                <asp:Label ID="Label14" runat="server" Text="Never pay reason code"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell28" runat="server">
                <asp:TextBox ID="txtNeverPayReasonCd" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow> 
        
        <asp:TableRow ID="TableRow8" runat="server">
            <asp:TableCell ID="TableCell29" runat="server">
                <asp:Label ID="Label15" runat="server" Text="dflt reason 1st code"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell30" runat="server">
                <asp:TextBox ID="txtDfltReason1stCd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell31" runat="server">
                <asp:Label ID="Label16" runat="server" Text="dflt reason 2nd code"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell32" runat="server">
                <asp:TextBox ID="txtDfltReason2ndCd" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow> 
        
        <asp:TableRow ID="TableRow9" runat="server">
            <asp:TableCell ID="TableCell33" runat="server">
                <asp:Label ID="Label17" runat="server" Text="hud termination reason code"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell34" runat="server">
                <asp:TextBox ID="txtHudTerminationReasonCd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell35" runat="server">
                <asp:Label ID="Label18" runat="server" Text="hud termination datetime"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell36" runat="server">
                <asp:TextBox ID="txtHudTerminationDt" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow> 
        
        <asp:TableRow ID="TableRow10" runat="server">
            <asp:TableCell ID="TableCell37" runat="server">
                <asp:Label ID="Label19" runat="server" Text="hud outcome code"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell38" runat="server">
                <asp:TextBox ID="txtHudOutcomeCd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell39" runat="server">
                <asp:Label ID="Label20" runat="server" Text="AMI percentage"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell40" runat="server">
                <asp:TextBox ID="txtAMIPercentage" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow> 
        
        <asp:TableRow ID="TableRow11" runat="server">
            <asp:TableCell ID="TableCell41" runat="server">
                <asp:Label ID="Label21" runat="server" Text="Counseling duration code"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell42" runat="server">
                <asp:TextBox ID="txtCounselingDurationCd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell43" runat="server">
                <asp:Label ID="Label22" runat="server" Text="gender code"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell44" runat="server">
                <asp:TextBox ID="txtGenderCd" runat="server"></asp:TextBox>
            </asp:TableCell>            
        </asp:TableRow>     
        
        <asp:TableRow ID="TableRow12" runat="server">
            <asp:TableCell ID="TableCell45" runat="server">
                <asp:Label ID="Label23" runat="server" Text="Borrower first name"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell46" runat="server">
                <asp:TextBox ID="txtBorrowerFName" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell47" runat="server">
                <asp:Label ID="Label24" runat="server" Text="Borrower last name"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell48" runat="server">
                <asp:TextBox ID="txtBorrowerLName" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow> 
        
        <asp:TableRow ID="TableRow13" runat="server">
            <asp:TableCell ID="TableCell49" runat="server">
                <asp:Label ID="Label25" runat="server" Text="Borrower midle name"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell50" runat="server">
                <asp:TextBox ID="txtBorrowerMName" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell51" runat="server">
                <asp:Label ID="Label26" runat="server" Text="mother maiden last name"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell52" runat="server">
                <asp:TextBox ID="txtMotherMaidenLName" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow> 
        
        <asp:TableRow ID="TableRow14" runat="server">
            <asp:TableCell ID="TableCell53" runat="server">
                <asp:Label ID="Label27" runat="server" Text="borrower ssn"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell54" runat="server">
                <asp:TextBox ID="txtBorrowerSSN" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell55" runat="server">
                <asp:Label ID="Label28" runat="server" Text="borrower last 4 ssn"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell56" runat="server">
                <asp:TextBox ID="txtBorrowerLast4SSN" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow> 
        
        <asp:TableRow ID="TableRow15" runat="server">
            <asp:TableCell ID="TableCell57" runat="server">
                <asp:Label ID="Label29" runat="server" Text="borrower dob"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell58" runat="server">
                <asp:TextBox ID="txtBorrowerDOB" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell59" runat="server">
                <asp:Label ID="Label30" runat="server" Text="coborrower first name"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell60" runat="server">
                <asp:TextBox ID="txtCoBorrowerFName" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow> 
        
        <asp:TableRow ID="TableRow16" runat="server">
            <asp:TableCell ID="TableCell61" runat="server">
                <asp:Label ID="Label31" runat="server" Text="coborrower last name"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell62" runat="server">
                <asp:TextBox ID="txtCoBorrowerLName" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell63" runat="server">
                <asp:Label ID="Label32" runat="server" Text="coborrower middle name"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell64" runat="server">
                <asp:TextBox ID="txtCoBorrowerMName" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow> 
        
        <asp:TableRow ID="TableRow17" runat="server">
            <asp:TableCell ID="TableCell65" runat="server">
                <asp:Label ID="Label33" runat="server" Text="coborrower ssn"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell66" runat="server">
                <asp:TextBox ID="txtCoBorrowerSSN" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell67" runat="server">
                <asp:Label ID="Label34" runat="server" Text="coborrower last 4 ssn"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell68" runat="server">
                <asp:TextBox ID="txtCoBorrowerLast4SSN" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow> 
        
        <asp:TableRow ID="TableRow18" runat="server">
            <asp:TableCell ID="TableCell69" runat="server">
                <asp:Label ID="Label35" runat="server" Text="coborrower dob"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell70" runat="server">
                <asp:TextBox ID="txtCoBorrowerDOB" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell71" runat="server">
                <asp:Label ID="Label36" runat="server" Text="Assigned counselor id ref"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell72" runat="server">
                <asp:TextBox ID="txtAssignedCounselorIDRef" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow> 
        
        <asp:TableRow ID="TableRow19" runat="server">
            <asp:TableCell ID="TableCell73" runat="server">
                <asp:Label ID="Label37" runat="server" Text="primary contact no"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell74" runat="server">
                <asp:TextBox ID="txtPrimaryContactNo" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell75" runat="server">
                <asp:Label ID="Label38" runat="server" Text="second contact no"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell76" runat="server">
                <asp:TextBox ID="txtSecondContactNo" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>      

<asp:TableRow ID="TableRow20" runat="server">
            <asp:TableCell ID="TableCell77" runat="server">
                <asp:Label ID="Label39" runat="server" Text="email 1"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell78" runat="server">
                <asp:TextBox ID="txtEmail1" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell79" runat="server">
                <asp:Label ID="Label40" runat="server" Text="email 2"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell80" runat="server">
                <asp:TextBox ID="txtEmail2" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow21" runat="server">
            <asp:TableCell ID="TableCell81" runat="server">
                <asp:Label ID="Label41" runat="server" Text="Contact addr. 1"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell82" runat="server">
                <asp:TextBox ID="txtContactAddress1" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell83" runat="server">
                <asp:Label ID="Label42" runat="server" Text="Contact addr. 2"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell84" runat="server">
                <asp:TextBox ID="txtContactAddress2" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow22" runat="server">
            <asp:TableCell ID="TableCell85" runat="server">
                <asp:Label ID="Label43" runat="server" Text="contact city"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell86" runat="server">
                <asp:TextBox ID="txtContactCity" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell87" runat="server">
                <asp:Label ID="Label44" runat="server" Text="contact state code"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell88" runat="server">
                <asp:TextBox ID="txtContactStateCd" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow23" runat="server">
            <asp:TableCell ID="TableCell89" runat="server">
                <asp:Label ID="Label45" runat="server" Text="contact zip"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell90" runat="server">
                <asp:TextBox ID="txtContactZip" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell91" runat="server">
                <asp:Label ID="Label46" runat="server" Text="contact zip plus 4"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell92" runat="server">
                <asp:TextBox ID="txtContactZipPlus4" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow24" runat="server">
            <asp:TableCell ID="TableCell93" runat="server">
                <asp:Label ID="Label47" runat="server" Text="Property addr. 1"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell94" runat="server">
                <asp:TextBox ID="txtPropertyAddress1" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell95" runat="server">
                <asp:Label ID="Label48" runat="server" Text="Property addr. 2"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell96" runat="server">
                <asp:TextBox ID="txtPropertyAddress2" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow25" runat="server">
            <asp:TableCell ID="TableCell97" runat="server">
                <asp:Label ID="Label49" runat="server" Text="Property city"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell98" runat="server">
                <asp:TextBox ID="txtPropertyCity" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell99" runat="server">
                <asp:Label ID="Label50" runat="server" Text="Property state code"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell100" runat="server">
                <asp:TextBox ID="txtPropertyStateCd" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow26" runat="server">
            <asp:TableCell ID="TableCell101" runat="server">
                <asp:Label ID="Label51" runat="server" Text="Property zip"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell102" runat="server">
                <asp:TextBox ID="txtPropertyZip" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell103" runat="server">
                <asp:Label ID="Label52" runat="server" Text="Property zip plus 4"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell104" runat="server">
                <asp:TextBox ID="txtPropertyZipPlus4" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow27" runat="server">
            <asp:TableCell ID="TableCell105" runat="server">
                <asp:Label ID="Label53" runat="server" Text="Bankruptcy ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell106" runat="server">
                <asp:TextBox ID="txtBankruptcyInd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell107" runat="server">
                <asp:Label ID="Label54" runat="server" Text="Bankruptcy attorney"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell108" runat="server">
                <asp:TextBox ID="txtBankruptcyAttorney" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow28" runat="server">
            <asp:TableCell ID="TableCell109" runat="server">
                <asp:Label ID="Label55" runat="server" Text="Bankruptcy pmt. current ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell110" runat="server">
                <asp:TextBox ID="txtBankruptcyPmtCurrentInd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell111" runat="server">
                <asp:Label ID="Label56" runat="server" Text="borrower edu. level completed code"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell112" runat="server">
                <asp:TextBox ID="txtBorrowerEducLevelCompletedInd" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow29" runat="server">
            <asp:TableCell ID="TableCell113" runat="server">
                <asp:Label ID="Label57" runat="server" Text="borrower marital status code  "></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell114" runat="server">
                <asp:TextBox ID="txtBorrowerMaritalStatusCd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell115" runat="server">
                <asp:Label ID="Label58" runat="server" Text="Borrower preferred lang. code"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell116" runat="server">
                <asp:TextBox ID="txtBorrowerPreferedLangCd" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow30" runat="server">
            <asp:TableCell ID="TableCell117" runat="server">
                <asp:Label ID="Label59" runat="server" Text="Borrower occupation code"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell118" runat="server">
                <asp:TextBox ID="txtBorrowerOccupationCd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell119" runat="server">
                <asp:Label ID="Label60" runat="server" Text="Coborrrower occupation code"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell120" runat="server">
                <asp:TextBox ID="txtCoBorrowerOccupationCd" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow31" runat="server">
            <asp:TableCell ID="TableCell121" runat="server">
                <asp:Label ID="Label61" runat="server" Text="Hispanic ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell122" runat="server">
                <asp:TextBox ID="txtHispanicInd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell123" runat="server">
                <asp:Label ID="Label62" runat="server" Text="Duplicate ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell124" runat="server">
                <asp:TextBox ID="txtDuplicateInd" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow32" runat="server">
            <asp:TableCell ID="TableCell125" runat="server">
                <asp:Label ID="Label63" runat="server" Text="Fc notice received ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell126" runat="server">
                <asp:TextBox ID="txtFcNoticeReceivedInd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell127" runat="server">
                <asp:Label ID="Label64" runat="server" Text="Case complete ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell128" runat="server">
                <asp:TextBox ID="txtCaseCompleteInd" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow33" runat="server">
            <asp:TableCell ID="TableCell129" runat="server">
                <asp:Label ID="Label65" runat="server" Text="Funding consent ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell130" runat="server">
                <asp:TextBox ID="txtFundingConsentInd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell131" runat="server">
                <asp:Label ID="Label66" runat="server" Text="Servicer consent ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell132" runat="server">
                <asp:TextBox ID="txtServicerConsentInd" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow34" runat="server">
            <asp:TableCell ID="TableCell133" runat="server">
                <asp:Label ID="Label67" runat="server" Text="agency media consent ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell134" runat="server">
                <asp:TextBox ID="txtAgencyMediaConsentInd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell135" runat="server">
                <asp:Label ID="Label68" runat="server" Text="hpf media candidate ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell136" runat="server">
                <asp:TextBox ID="txtHpfMediaCandidateInd" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow35" runat="server">
            <asp:TableCell ID="TableCell137" runat="server">
                <asp:Label ID="Label69" runat="server" Text="hpf network candidate ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell138" runat="server">
                <asp:TextBox ID="txtHpfNetworkCandidateInd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell139" runat="server">
                <asp:Label ID="Label70" runat="server" Text="hpf success story ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell140" runat="server">
                <asp:TextBox ID="txtHpfSuccessStoryInd" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow36" runat="server">
            <asp:TableCell ID="TableCell141" runat="server">
                <asp:Label ID="Label71" runat="server" Text="Borrower disabled ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell142" runat="server">
                <asp:TextBox ID="txtBorrowerDisabledInd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell143" runat="server">
                <asp:Label ID="Label72" runat="server" Text="Coborrower disabled ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell144" runat="server">
                <asp:TextBox ID="txtCoBorrowerDisabledInd" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow37" runat="server">
            <asp:TableCell ID="TableCell145" runat="server">
                <asp:Label ID="Label73" runat="server" Text="Summary sent other code"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell146" runat="server">
                <asp:TextBox ID="txtSummarySentOtherCd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell147" runat="server">
                <asp:Label ID="Label74" runat="server" Text="Summary sent other datetime"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell148" runat="server">
                <asp:TextBox ID="txtSummarySentOtherDt" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow38" runat="server">
            <asp:TableCell ID="TableCell149" runat="server">
                <asp:Label ID="Label75" runat="server" Text="Summary sent datetime"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell150" runat="server">
                <asp:TextBox ID="txtSummarySentDt" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell151" runat="server">
                <asp:Label ID="Label76" runat="server" Text="Occupant num"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell152" runat="server">
                <asp:TextBox ID="txtOccupantNum" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow39" runat="server">
            <asp:TableCell ID="TableCell153" runat="server">
                <asp:Label ID="Label77" runat="server" Text="Loan default reason notes"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell154" runat="server">
                <asp:TextBox ID="txtLoanDfltReasonNotes" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell155" runat="server">
                <asp:Label ID="Label78" runat="server" Text="Prim res. est. mkt. value"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell156" runat="server">
                <asp:TextBox ID="txtPrimResEstMktValue" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow40" runat="server">
            <asp:TableCell ID="TableCell157" runat="server">
                <asp:Label ID="Label79" runat="server" Text="counselor last name"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell158" runat="server">
                <asp:TextBox ID="txtCounselorLastName" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell159" runat="server">
                <asp:Label ID="Label80" runat="server" Text="counselor first name"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell160" runat="server">
                <asp:TextBox ID="txtCounselorFirstName" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow41" runat="server">
            <asp:TableCell ID="TableCell161" runat="server">
                <asp:Label ID="Label81" runat="server" Text="Counselor email"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell162" runat="server">
                <asp:TextBox ID="txtCounselorEmail" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell163" runat="server">
                <asp:Label ID="Label82" runat="server" Text="Counselor phone"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell164" runat="server">
                <asp:TextBox ID="txtCounselorPhone" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow42" runat="server">
            <asp:TableCell ID="TableCell165" runat="server">
                <asp:Label ID="Label83" runat="server" Text="Counselor ext."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell166" runat="server">
                <asp:TextBox ID="txtCounselorExt" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell167" runat="server">
                <asp:Label ID="Label84" runat="server" Text="Discussed sol. with srvcr. ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell168" runat="server">
                <asp:TextBox ID="txtDiscussedSolutionWithSrvcrInd" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow43" runat="server">
            <asp:TableCell ID="TableCell169" runat="server">
                <asp:Label ID="Label85" runat="server" Text="Worked with another agency ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell170" runat="server">
                <asp:TextBox ID="txtWorkedWithAnotherAgencyInd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell171" runat="server">
                <asp:Label ID="Label86" runat="server" Text="Contacted srvcr. recently ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell172" runat="server">
                <asp:TextBox ID="txtContactedSrvcrRecentlyInd" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow44" runat="server">
            <asp:TableCell ID="TableCell173" runat="server">
                <asp:Label ID="Label87" runat="server" Text="Has workout plan ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell174" runat="server">
                <asp:TextBox ID="txtHasWorkoutPlanInd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell175" runat="server">
                <asp:Label ID="Label88" runat="server" Text="Srvcr workout plan ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell176" runat="server">
                <asp:TextBox ID="txtSrvcrWorkoutPlanInd" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow45" runat="server">
            <asp:TableCell ID="TableCell177" runat="server">
                <asp:Label ID="Label89" runat="server" Text="FC sale date set ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell178" runat="server">
                <asp:TextBox ID="txtFcSaleDateSetInd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell179" runat="server">
                <asp:Label ID="Label90" runat="server" Text="Opt out newsletter ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell180" runat="server">
                <asp:TextBox ID="txtOptOutNewsletterInd" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow46" runat="server">
            <asp:TableCell ID="TableCell181" runat="server">
                <asp:Label ID="Label91" runat="server" Text="Opt out survey ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell182" runat="server">
                <asp:TextBox ID="txtOptOutSurveyInd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell183" runat="server">
                <asp:Label ID="Label92" runat="server" Text="Do not call ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell184" runat="server">
                <asp:TextBox ID="txtDoNotCallInd" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow47" runat="server">
            <asp:TableCell ID="TableCell185" runat="server">
                <asp:Label ID="Label93" runat="server" Text="Owner occupied ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell186" runat="server">
                <asp:TextBox ID="txtOwnerOccupiedInd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell187" runat="server">
                <asp:Label ID="Label94" runat="server" Text="Primary residence ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell188" runat="server">
                <asp:TextBox ID="txtPrimaryResidenceInd" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow48" runat="server">
            <asp:TableCell ID="TableCell189" runat="server">
                <asp:Label ID="Label95" runat="server" Text="Realty company"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell190" runat="server">
                <asp:TextBox ID="txtRealtyCompany" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell191" runat="server">
                <asp:Label ID="Label96" runat="server" Text="Property code"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell192" runat="server">
                <asp:TextBox ID="txtPropertyCd" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow49" runat="server">
            <asp:TableCell ID="TableCell193" runat="server">
                <asp:Label ID="Label97" runat="server" Text="For Sale ind."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell194" runat="server">
                <asp:TextBox ID="txtForSaleInd" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell195" runat="server">
                <asp:Label ID="Label98" runat="server" Text="Home sale price"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell196" runat="server">
                <asp:TextBox ID="txtHomeSalePrice" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow50" runat="server">
            <asp:TableCell ID="TableCell197" runat="server">
                <asp:Label ID="Label99" runat="server" Text="Home purchase year"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell198" runat="server">
                <asp:TextBox ID="txtHomePurchaseYear" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell199" runat="server">
                <asp:Label ID="Label100" runat="server" Text="Home purchase price"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell200" runat="server">
                <asp:TextBox ID="txtHomePurchasePrice" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow51" runat="server">
            <asp:TableCell ID="TableCell201" runat="server">
                <asp:Label ID="Label101" runat="server" Text="Home current market value"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell202" runat="server">
                <asp:TextBox ID="txtHomeCurMktValue" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell203" runat="server">
                <asp:Label ID="Label102" runat="server" Text="Military service code"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell204" runat="server">
                <asp:TextBox ID="txtMilitaryServiceCd" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow52" runat="server">
            <asp:TableCell ID="TableCell205" runat="server">
                <asp:Label ID="Label103" runat="server" Text="Household gross annual income amt."></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell206" runat="server">
                <asp:TextBox ID="txtHousholdGrossAnnualIncomeAmt" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell207" runat="server">
                <asp:Label ID="Label104" runat="server" Text="loan list"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell208" runat="server">
                <asp:TextBox ID="txtLoanList" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow53" runat="server">
            <asp:TableCell ID="TableCell209" runat="server">
                <asp:Label ID="Label105" runat="server" Text="intake credit score"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell210" runat="server">
                <asp:TextBox ID="txtIntakeCreditScore" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell211" runat="server">
                <asp:Label ID="Label106" runat="server" Text="Intake credit bureau code"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell212" runat="server">
                <asp:TextBox ID="txtIntakeCreditBureauCd" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow54" runat="server">
            <asp:TableCell ID="TableCell213" runat="server">
                <asp:Label ID="Label107" runat="server" Text="Change last user id"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell214" runat="server">
                <asp:TextBox ID="txtChgLstUserID" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell215" runat="server">
                <asp:Label ID="Label108" runat="server" Text="Action item notes"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell216" runat="server">
                <asp:TextBox ID="TextBox108" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow55" runat="server">
            <asp:TableCell ID="TableCell217" runat="server">
                <asp:Label ID="Label109" runat="server" Text="Follow up notes"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell218" runat="server">
                <asp:TextBox ID="TextBox109" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell219" runat="server">
                <asp:Label ID="Label110" runat="server" Text="Agency case number"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell220" runat="server">
                <asp:TextBox ID="TextBox110" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
        <asp:TableRow ID="TableRow56" runat="server">
            <asp:TableCell ID="TableCell221" runat="server">
                <asp:Label ID="Label111" runat="server" Text="Agency case number"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell222" runat="server">
                <asp:TextBox ID="TextBox111" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="TableCell223" runat="server">
                <asp:Label ID="Label112" runat="server" Text="Agency case number"></asp:Label>             
            </asp:TableCell>
            <asp:TableCell ID="TableCell224" runat="server">
                <asp:TextBox ID="TextBox112" runat="server"></asp:TextBox>
            </asp:TableCell>                       
        </asp:TableRow>   
                  
    </asp:Table>
    <br />  

<br />
<br />
</asp:Content>
