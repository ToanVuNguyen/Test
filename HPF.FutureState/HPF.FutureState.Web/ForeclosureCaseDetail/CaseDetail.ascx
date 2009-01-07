<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaseDetail.ascx.cs" Inherits="HPF.FutureState.Web.ForeclosureCaseDetail.ForeclosureCaseDetail" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />

<table style="width:100%;">
    <tr>
        <td>
            <table style="width:100%; border-bottom-style: none;">
            <tr>
                <td colspan=3 align="right">
                    <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="MyButton" 
                        onclick="btn_Save_Click" />
                </td>
            </tr>
                <tr>
                    <td style="vertical-align: top">
                        <table style="width:100%; border-bottom-style:none;">
                            <tr>
                                <td colspan="2">
                                    <h1>Property:</h1></td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Address 1*:</td>
                                <td align="left">
                                    <asp:Label ID="lblAddress1" runat="server" CssClass="Text" 
                                        Text="1234 Any Street NW"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Address 2:</td>
                                <td>
                                    <asp:Label ID="lblAddress2" runat="server" CssClass="Text">#2</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    City*:</td>
                                <td>
                                    <asp:Label ID="lblCity" runat="server" CssClass="Text">Your Town</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    State, Zip*:</td>
                                <td>
                                    <asp:Label ID="lblStateZip" runat="server" CssClass="Text">MN, 55416-1234</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Primary Residence:</td>
                                <td>
                                    <asp:Label ID="lblPrimaryResidence" runat="server" CssClass="Text">Yes</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Owner Occupied*:</td>
                                <td>
                                    <asp:Label ID="lblOwnerOccupied" runat="server" CssClass="Text">Yes</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Property Code:</td>
                                <td>
                                    <asp:Label ID="lblPropertyCode" runat="server" CssClass="Text">Single Family 
                                    Detached</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Number of Occupants*:</td>
                                <td>
                                    <asp:Label ID="lblNumberOfOccupants" runat="server" CssClass="Text">5</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Purchase Year:</td>
                                <td>
                                    <asp:Label ID="lblPurchaseYear" runat="server" CssClass="Text">1994</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Purchase Price:</td>
                                <td>
                                    <asp:Label ID="lblPurchasePrice" runat="server" CssClass="Text">$300,000.000</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Current Market Value:</td>
                                <td>
                                    <asp:Label ID="lblCurrentMarketValue" runat="server" CssClass="Text">$200,000.000</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    For Sale Indicator:</td>
                                <td>
                                    <asp:Label ID="lblForSaleIndicator" runat="server" CssClass="Text">Yes</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Realty Company:</td>
                                <td>
                                    <asp:Label ID="lblRealtyCompany" runat="server" CssClass="Text">Rebecca Brown 
                                    Realty</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Home Asking Price:</td>
                                <td>
                                    <asp:Label ID="lblHomeAskingPrice" runat="server" CssClass="Text">$189,000.00</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Primary Residence Est. Mkt Value:</td>
                                <td>
                                    <asp:Label ID="lblPrimaryRes" runat="server" CssClass="Text">$200,000.00</asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="vertical-align: top">
                        <table style="width:100%;">
                            <tr>
                                <td colspan="2">
                                    <h1>Borrower:</h1></td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    First Name*:</td>
                                <td>
                                    <asp:Label ID="lblFirstName" runat="server" CssClass="Text">Ivan</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Middle Name*:</td>
                                <td>
                                    <asp:Label ID="lblMidName" runat="server" CssClass="Text">A</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Last Name*:</td>
                                <td>
                                    <asp:Label ID="lblLastName" runat="server" CssClass="Text">Mustang</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    DOB*:</td>
                                <td>
                                    <asp:Label ID="lblDOB" runat="server" CssClass="Text">07/01/1960</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Last 4 of SSN:</td>
                                <td>
                                    <asp:Label ID="lblLast4SSN" runat="server" CssClass="Text">XXX-XX-1234</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Primary Contact #*:</td>
                                <td>
                                    <asp:Label ID="lblPrimaryContact" runat="server" CssClass="Text">612-803-1111</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Secondary Contact #:</td>
                                <td>
                                    <asp:Label ID="lblSecondaryContact" runat="server" CssClass="Text">612-866-2635</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Primary Email*:</td>
                                <td>
                                    <asp:Label ID="lblPrimaryEmail" runat="server" CssClass="Text">IvanAMustang@hotmail.com</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Secondary Email:</td>
                                <td>
                                    <asp:Label ID="lblSecondaryEmail" runat="server" CssClass="Text">Ivan_ivana@usfamily.net</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Gender*:</td>
                                <td>
                                    <asp:Label ID="lblGender" runat="server" CssClass="Text">Male</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Morther&#39;s Maiden Name:</td>
                                <td>
                                    <asp:Label ID="lblMother" runat="server" CssClass="Text">Male</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Disabled:</td>
                                <td>
                                    <asp:Label ID="lblDisabled" runat="server" CssClass="Text">No</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Race*:</td>
                                <td>
                                    <asp:Label ID="lblRace" runat="server" CssClass="Text">Asia &amp; White</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Ethnicity*:</td>
                                <td>
                                    <asp:Label ID="lblEthnicity" runat="server" CssClass="Text">Not Hispanic</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Preferred Language:</td>
                                <td>
                                    <asp:Label ID="lblPreferedLanguage" runat="server" CssClass="Text">English</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Education Level Completed:</td>
                                <td>
                                    <asp:Label ID="lblEducationLevel" runat="server" CssClass="Text">???????</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Marital Status:</td>
                                <td>
                                    <asp:Label ID="lblMaritalStatus" runat="server" CssClass="Text">???????</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Occupation:</td>
                                <td>
                                    <asp:Label ID="lblOccupation" runat="server" CssClass="Text">Roofer</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Military Service:</td>
                                <td>
                                    <asp:Label ID="lblMilitaryService" runat="server" CssClass="Text">Active</asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="vertical-align: top">
                        <table style="width:100%;">
                            <tr>
                                <td colspan="2">
                                   <h1>Co-Borrower:</h1></td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    First Name:</td>
                                <td>
                                    <asp:Label ID="lblCoFirstName" runat="server" CssClass="Text">Ivana</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Middle Name:</td>
                                <td>
                                    <asp:Label ID="lblCoMidName" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Last Name:</td>
                                <td>
                                    <asp:Label ID="lblCoLastName" runat="server" CssClass="Text">Jaguar</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    DOB:</td>
                                <td>
                                    <asp:Label ID="lblCoDOB" runat="server" CssClass="Text">01/01/1969</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Last 4 SSN:</td>
                                <td>
                                    <asp:Label ID="lblCoLast4SSN" runat="server" CssClass="Text">XXX-XX-4321</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Disabled:</td>
                                <td>
                                    <asp:Label ID="lblCoDisabled" runat="server" CssClass="Text">No</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Occupation:</td>
                                <td>
                                    <asp:Label ID="lblCoOccupation" runat="server" CssClass="Text">Roofer</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                        <table style="width:100%;">
                            <tr>
                                <td colspan="2">
                                   <h1>Contact Address:</h1></td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Address 1*:</td>
                                <td>
                                    <asp:Label ID="lblContactAdd1" runat="server" CssClass="Text">1234 Any Street 
                                    NW</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Address 2:</td>
                                <td>
                                    <asp:Label ID="lblContactAdd2" runat="server" CssClass="Text">#2</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    City*:</td>
                                <td>
                                    <asp:Label ID="lblContactCity" runat="server" CssClass="Text">Your Town</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    State,Zip*:</td>
                                <td>
                                    <asp:Label ID="lblContactStateZip" runat="server" CssClass="Text">MN, 
                                    55416-1234</asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <hr  style="color:#8FC4F6; border-style:solid; border-width:1px"/></td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            <table class="style1">
                <colgroup>
                <col width="20%" />
                <col width=""20%" />
                <col width="30%" />
                <col width="30%" />
                </colgroup>
                <tr>
                     <td align="right" >
                                    <h1>Case Status:</h1></td>
                  <td>
                                    &nbsp;</td>
                    <td align="right" >
                                    <h1>Case Financials:</h1></td>
                    <td>
                                    &nbsp;</td>
                </tr>
                <tr>
                     <td align="right" class="sidelinks">
                                    Duplicate:</td>
                  <td>
                                                <asp:DropDownList ID="ddlDuplicate" runat="server" CssClass="Text">
                                                 <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                                </asp:DropDownList>
                                </td>
                    <td align="right" class="sidelinks">
                                    Household Type*:</td>
                    <td>
                                    <asp:Label ID="lblHouseholdType" runat="server" CssClass="Text">Female-headed single parent household</asp:Label>
                                </td>
                </tr>
                 <tr>
                     <td align="right" class="sidelinks">
                                    Agency:</td>
                  <td>
                                                <asp:DropDownList ID="ddlAgency" runat="server" CssClass="Text">
                                                </asp:DropDownList>
                                </td>
                    <td align="right" class="sidelinks">
                                    Household Gross Annual Income*:</td>
                    <td>
                                    <asp:Label ID="lblAnnualIncome" runat="server" CssClass="Text">$76,500.00</asp:Label>
                                </td>
                </tr>
                 <tr>
                     <td align="right" class="sidelinks">
                                    Agency Case*:</td>
                  <td>
                                    <asp:Label ID="lblAgencyCase" runat="server" CssClass="Text">AH123490</asp:Label>
                                </td>
                    <td align="right" class="sidelinks">
                                    Income Earner Code*:</td>
                    <td>
                                    <asp:Label ID="lblEarnerCode" runat="server" CssClass="Text">6+</asp:Label>
                                </td>
                </tr>
                 <tr>
                     <td align="right" class="sidelinks">
                                    Agency Client#:</td>
                  <td>
                                    <asp:Label ID="lblAgencyClient" runat="server" CssClass="Text">45466</asp:Label>
                                </td>
                    <td align="right" class="sidelinks">
                                    AMI Percentage*:</td>
                    <td>
                                    <asp:Label ID="lblAMIPercentage" runat="server" CssClass="Text">80%</asp:Label>
                                </td>
                </tr>
                 <tr>
                     <td align="right" class="sidelinks">
                                    Counselor*:</td>
                  <td>
                                    <asp:Label ID="lblCounselor" runat="server" CssClass="Text">Amada Huggenkiss</asp:Label>
                                </td>
                    <td align="right" class="sidelinks">
                                    Discussed Solution w/Servicer*:</td>
                    <td>
                                    <asp:Label ID="lblWServicer" runat="server" CssClass="Text">Yes</asp:Label>
                                </td>
                </tr>
                 <tr>
                     <td align="right" class="sidelinks">
                                    Phone &amp; Ext.*:</td>
                  <td>
                                    <asp:Label ID="lblPhoneExt" runat="server" CssClass="Text">877-123-1234x55432</asp:Label>
                                </td>
                    <td align="right" class="sidelinks">
                                    Worked w/Another Agency*:</td>
                    <td>
                                    <asp:Label ID="lblAnotherAgency" runat="server" CssClass="Text">No</asp:Label>
                                </td>
                </tr>
                 <tr>
                     <td align="right" class="sidelinks">
                                    Counselor Email*:</td>
                  <td>
                                    <asp:Label ID="lblCounselorEmail" runat="server" CssClass="Text">ahuggenkiss@moes.com</asp:Label>
                                </td>
                    <td align="right" class="sidelinks">
                                    Contacted Servicer Recently*:</td>
                    <td>
                                    <asp:Label ID="lblSevicerRecently" runat="server" CssClass="Text">No</asp:Label>
                                </td>
                </tr>
                 <tr>
                     <td align="right" class="sidelinks">
                                    Program*:</td>
                  <td>
                                    <asp:Label ID="lblProgram" runat="server" CssClass="Text">995-HOPE</asp:Label>
                                </td>
                    <td align="right" class="sidelinks">
                                    Has Workout Plan*:</td>
                    <td>
                                    <asp:Label ID="lblWorkoutPlan" runat="server" CssClass="Text">Yes</asp:Label>
                                </td>
                </tr>
                 <tr>
                     <td align="right" class="sidelinks">
                                    Intake Date*:</td>
                  <td>
                                    <asp:Label ID="lblIntakeDate" runat="server" CssClass="Text">07/14/2008</asp:Label>
                                </td>
                    <td align="right" class="sidelinks">
                                    Workout Plan Current*:</td>
                    <td>
                                    <asp:Label ID="lblPlanCurrent" runat="server" CssClass="Text">Yes</asp:Label>
                                </td>
                </tr>
                 <tr>
                     <td align="right" class="sidelinks">
                                    Complete Date*:</td>
                  <td>
                                    <asp:Label ID="lblCompleteDate" runat="server" CssClass="Text">08/01/2008</asp:Label>
                                </td>
                    <td align="right" class="sidelinks">
                                    Intake Credit Scores*:</td>
                    <td>
                                    <asp:Label ID="lblCreditScores" runat="server" CssClass="Text">Yes</asp:Label>
                                </td>
                </tr>
                 <tr>
                     <td align="right" class="sidelinks">
                                    Counselling Duration*:</td>
                  <td>
                                    <asp:Label ID="lblCounsellingDuration" runat="server" CssClass="Text">31-6- minutes</asp:Label>
                                </td>
                    <td align="right" class="sidelinks">
                                    Intake Credit Bureau*:</td>
                    <td>
                                    <asp:Label ID="lblCreditBureau" runat="server" CssClass="Text">Yes</asp:Label>
                                </td>
                </tr>
                <tr>
                     <td align="right" class="sidelinks">
                                    Case Source Code*:</td>
                  <td>
                                    <asp:Label ID="lblSourceCode" runat="server" CssClass="Text">Newspaper/ads</asp:Label>
                                </td>
                    <td >
                                    </td>
                    <td>
                                   
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
                </tr>
                
                <tr>
                    <td  align="right" >
                        <h1>Case Summary :</h1></td>
                    <td>
                        &nbsp;</td>
                    <td align="right">
                         <h1>Foreclosure Notice :</h1></td>
                    <td>
                        &nbsp;</td>
                </tr>
                
                 <tr>
                     <td align="right" class="sidelinks">
                                    Summary Sent Date*:</td>
                  <td>
                                    <asp:Label ID="lblSentDate" runat="server" CssClass="Text">08/01/2008</asp:Label>
                                </td>
                    <td align="right" class="sidelinks">
                                    Foreclosure Notice Received*:</td>
                    <td>
                                    <asp:Label ID="lblNoticeReceived" runat="server" CssClass="Text">09/25/2008</asp:Label>
                                </td>
                </tr>
                 <tr>
                     <td align="right" class="sidelinks">
                                    Summary Sent Other*:</td>
                  <td>
                                    <asp:Label ID="lblSentOrther" runat="server" CssClass="Text">Fax</asp:Label>
                                </td>
                    <td align="right" class="sidelinks">
                                    Foreclosure Date Set*:</td>
                    <td>
                                    <asp:Label ID="lblDateSet" runat="server" CssClass="Text">Yes</asp:Label>
                                </td>
                </tr>
                 <tr>
                     <td align="right" class="sidelinks">
                                    Summary Sent Other Date*:</td>
                  <td>
                                    <asp:Label ID="lblOtherDate" runat="server" CssClass="Text">08/01/2008</asp:Label>
                                </td>
                    <td align="right" class="sidelinks">
                                    &nbsp;</td>
                    <td>
                                    &nbsp;</td>
                </tr>
                 <tr>
                     <td align="right" class="sidelinks">
                                    &nbsp;</td>
                  <td>
                                    &nbsp;</td>
                    <td align="right" class="sidelinks">
                                    &nbsp;</td>
                    <td>
                                    &nbsp;</td>
                </tr>
                 <tr>
                     <td align="right" >
                                    <h1>Consent:</h1></td>
                  <td>
                                    &nbsp;</td>
                    <td align="right" >
                                    <h1>Bankruptcy:</h1></td>
                    <td>
                                    &nbsp;</td>
                </tr>
                  <tr>
                     <td align="right" class="sidelinks">
                                    Servicer Consent*:</td>
                  <td>
                                    <asp:Label ID="lblServicerConsent" runat="server" CssClass="Text">No</asp:Label>
                                </td>
                    <td align="right" class="sidelinks">
                                    Bankruptcy Indicator*:</td>
                    <td>
                                    <asp:Label ID="lblBankruptcy" runat="server" CssClass="Text">No</asp:Label>
                                </td>
                </tr>
                 <tr>
                     <td align="right" class="sidelinks">
                                    Funding Consent*:</td>
                  <td>
                                    <asp:Label ID="lblFundingConsent" runat="server" CssClass="Text">No</asp:Label>
                                </td>
                    <td align="right" class="sidelinks">
                                    Bankruptcy Attomey*:</td>
                    <td>
                                    <asp:Label ID="lblBankruptcyAttomey" runat="server" CssClass="Text">Jack Prescott</asp:Label>
                                </td>
                </tr>
                 <tr>
                     <td align="right" class="sidelinks">
                                    &nbsp;</td>
                  <td>
                                </td>
                    <td align="right" class="sidelinks">
                                    Bankcruptcy Payments Current Indicator:</td>
                    <td>
                                    <asp:Label ID="lblCurrentIndicator" runat="server" CssClass="Text">Yes</asp:Label>
                                </td>
                </tr>
                 <tr>
                     <td align="right" class="sidelinks">
                                    &nbsp;</td>
                  <td>
                                    &nbsp;</td>
                    <td align="right" class="sidelinks">
                                    &nbsp;</td>
                    <td>
                                    &nbsp;</td>
                </tr>
                 <tr>
                     <td align="right" >
                                    <h1>Default Reason:</h1></td>
                  <td>
                                    &nbsp;</td>
                    <td align="right" >
                                    <h1>HUD:</h1></td>
                    <td>
                                    &nbsp;</td>
                </tr>
                 <tr>
                     <td align="right" class="sidelinks">
                                    Primary Default Reason*:</td>
                  <td>
                                    <asp:Label ID="lblDefaultReason" runat="server" CssClass="Text">Excessive Obligations</asp:Label>
                                </td>
                    <td align="right" class="sidelinks">
                                    HUD Termination Reason*:</td>
                    <td>
                                    <asp:Label ID="lblTerminationReason" runat="server" CssClass="Text">Client terminated counseling</asp:Label>
                                </td>
                </tr>
                 <tr>
                     <td align="right" class="sidelinks">
                                    Secondary Default Reason*:</td>
                  <td>
                                    <asp:Label ID="lblSDefaultReason" runat="server" CssClass="Text">Inability to sell</asp:Label>
                                </td>
                    <td align="right" class="sidelinks">
                                    HUD Termination Date*:</td>
                    <td>
                                    <asp:Label ID="lblTerminationDate" runat="server" CssClass="Text">11/25/2008</asp:Label>
                                </td>
                </tr>
                 <tr>
                     <td align="right" class="sidelinks">
                                    &nbsp;</td>
                  <td>
                                    &nbsp;</td>
                    <td align="right" class="sidelinks">
                                    HUD Outcome*:</td>
                    <td>
                                    <asp:Label ID="lblHUDOutcome" runat="server" CssClass="Text">Currently receiving foreclosure prevention/buget conseling</asp:Label>
                                </td>
                </tr>
                
                 <tr>
                     <td align="right" class="sidelinks" colspan="4">
                                    <hr  style="color:#8FC4F6; border-style:solid; border-width:1px"/></td>
                </tr>
                
                 <tr>
                     <td colspan="4">
                     
                         <table  width="100%">
                             <colgroup>
                             <col width="25%" />
                             <col width="80%" />
                             
                             </colgroup>
                             <tr>
                                 <td  align="right">
                                     <h1>Counselor Notes:</h1></td>
                                 <td>
                                     &nbsp;</td>
                             </tr>
                             <tr>
                                 <td align="right" class="sidelinks" style="vertical-align:top">
                                     Loan Default Reason Notes:</td>
                                 <td class="Control">
                                     <asp:TextBox ID="txtReasonNote" runat="server" Rows="3" Width="100%" Height="56px" 
                                         TextMode="MultiLine" CssClass="Text"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td align="right" class="sidelinks" style="vertical-align:top">
                                     Action Item Notes:</td>
                                 <td class="Control">
                                     <asp:TextBox ID="txtItemNotes" runat="server" Rows="3" Width="100%" 
                                         Height="56px" TextMode="MultiLine" CssClass="Text"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td align="right" class="sidelinks" style="vertical-align:top">
                                     FollowUp Notes:</td>
                                 <td class="Control">
                                     <asp:TextBox ID="txtFollowUpNotes" runat="server" Rows="3" Width="100%" Height="56px" 
                                         TextMode="MultiLine" CssClass="Text"></asp:TextBox>
                                 </td>
                             </tr>
                         </table>
                     
                     </td>
                </tr>
                 <tr>
                     <td colspan="4">
                                    <hr  style="color:#8FC4F6; border-style:solid; border-width:1px"/></td>
                </tr>
                 <tr>
                     <td align="right" class="sidelinks" >
                                    &nbsp;</td>
                  <td>
                                    &nbsp;</td>
                    <td align="right" class="sidelinks">
                                    &nbsp;</td>
                    <td>
                                    &nbsp;</td>
                </tr>
                 <tr>
                     <td colspan="4" class="style7">
                                    <table  width="100%">
                                        <colgroup>
                                        <col width="15%" />
                                        <col width="15%" />
                                        <col width="20%"/>
                                        <col width="15%" />
                                        <col width="20%" />
                                        <col width="15%"/>
                                        </colgroup>
                                        <tr>
                                            <td align="right" >
                                                <h1>Opt In/Out:</h1></td>
                                            <td>
                                                &nbsp;</td>
                                            <td align="right"> 
                                                <h1>Media Candidate:</h1></td>
                                            <td>
                                                &nbsp;</td>
                                            <td align="right">
                                                <h1>Success Story:</h1></td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right"  class="sidelinks">
                                                Do Not Call*:</td>
                                            <td  class="Control">
                                                <asp:DropDownList ID="ddlNotCall" runat="server" CssClass="Text" >
                                                 <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right"  class="sidelinks">
                                                Agency Media Interest:</td>
                                            <td>
                                    <asp:Label ID="lblMediaInterest" runat="server" CssClass="Text" >Yes</asp:Label>
                                            </td>
                                            <td align="right"  class="sidelinks">
                                                Agency Success Story:</td>
                                            <td>
                                    <asp:Label ID="lblSuccessStory" runat="server"  CssClass="Text">Yes</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right"  class="sidelinks">
                                                News Letter*:</td>
                                            <td class="Control">
                                                <asp:DropDownList ID="ddlNewsLetter" runat="server" CssClass="Text"  >
                                                 <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" class="sidelinks" >
                                                HPF Media Condirmation:</td>
                                            <td class="Control">
                                                <asp:DropDownList ID="ddlMediaCondirmation" runat="server" CssClass="Text" >
                                                 <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right"  class="sidelinks">
                                                HPF Success Story:</td>
                                            <td class="Control">
                                                <asp:DropDownList ID="ddlSuccessStory" runat="server" CssClass="Text" >
                                                <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right"  class="sidelinks">
                                                Survey*:</td>
                                            <td class="Control">
                                                <asp:DropDownList ID="ddlServey" runat="server" CssClass="Text" >
                                                 <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" >
                                                &nbsp;</td>
                                            <td class="Control">
                                                &nbsp;</td>
                                            <td align="right" >
                                                &nbsp;</td>
                                            <td class="Control">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                    </td>
                </tr>
                 <tr>
                     <td align="right" class="sidelinks">
                                    &nbsp;</td>
                  <td>
                                    &nbsp;</td>
                    <td align="right" class="sidelinks">
                                    &nbsp;</td>
                    <td>
                                    &nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="right">
                    <asp:Button ID="btn_Save0" runat="server" Text="Save" 
                CssClass="MyButton" onclick="btn_Save_Click" />
                </td>
    </tr>
</table>
