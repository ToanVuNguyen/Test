<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaseDetail.ascx.cs" Inherits="HPF.FutureState.Web.ForeclosureCaseDetail.ForeclosureCaseDetail" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />
<table style="width:100%;">
    <tr>
        <td>
            <table style="width:100%; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #8FC4F6;">
            <tr>
                <td colspan=3 align="right">
                    <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="MyButton" />
                </td>
            </tr>
                <tr>
                    <td style="vertical-align: top">
                        <table style="width:100%;">
                            <tr>
                                <td colspan="2">
                                    <h1>Property:</h1></td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Address 1*:</td>
                                <td align="left">
                                    <asp:Label ID="lbl_Address1" runat="server" CssClass="Text" 
                                        Text="1234 Any Street NW"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Address 2:</td>
                                <td>
                                    <asp:Label ID="lbl_Address2" runat="server" CssClass="Text">#2</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    City*:</td>
                                <td>
                                    <asp:Label ID="lbl_City" runat="server" CssClass="Text">Your Town</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    State, Zip*:</td>
                                <td>
                                    <asp:Label ID="lbl_StateZip" runat="server" CssClass="Text">MN, 55416-1234</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Primary Residence:</td>
                                <td>
                                    <asp:Label ID="lbl_PrimaryResidence" runat="server" CssClass="Text">Yes</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Owner Occupied*:</td>
                                <td>
                                    <asp:Label ID="lbl_OwnerOccupied" runat="server" CssClass="Text">Yes</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Property Code:</td>
                                <td>
                                    <asp:Label ID="lbl_PropertyCode" runat="server" CssClass="Text">Single Family 
                                    Detached</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Number of Occupants*:</td>
                                <td>
                                    <asp:Label ID="lbl_NumberOfOccupants" runat="server" CssClass="Text">5</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Purchase Year:</td>
                                <td>
                                    <asp:Label ID="lbl_PurchaseYear" runat="server" CssClass="Text">1994</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Purchase Price:</td>
                                <td>
                                    <asp:Label ID="lbl_PurchasePrice" runat="server" CssClass="Text">$300,000.000</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Current Market Value:</td>
                                <td>
                                    <asp:Label ID="lbl_CurrentMarketValue" runat="server" CssClass="Text">$200,000.000</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    For Sale Indicator:</td>
                                <td>
                                    <asp:Label ID="lbl_ForSaleIndicator" runat="server" CssClass="Text">Yes</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Realty Company:</td>
                                <td>
                                    <asp:Label ID="lbl_RealtyCompany" runat="server" CssClass="Text">Rebecca Brown 
                                    Realty</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Home Asking Price:</td>
                                <td>
                                    <asp:Label ID="lbl_HomeAskingPrice" runat="server" CssClass="Text">$189,000.00</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Primary Residence Est. Mkt Value:</td>
                                <td>
                                    <asp:Label ID="lbl_PrimaryRes" runat="server" CssClass="Text">$200,000.00</asp:Label>
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
                                    <asp:Label ID="lbl_FirstName" runat="server" CssClass="Text">Ivan</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Middle Name*:</td>
                                <td>
                                    <asp:Label ID="lbl_MidName" runat="server" CssClass="Text">A</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Last Name*:</td>
                                <td>
                                    <asp:Label ID="lbl_LastName" runat="server" CssClass="Text">Mustang</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    DOB*:</td>
                                <td>
                                    <asp:Label ID="lbl_DOB" runat="server" CssClass="Text">07/01/1960</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Last 4 of SSN:</td>
                                <td>
                                    <asp:Label ID="lbl_Last4SSN" runat="server" CssClass="Text">XXX-XX-1234</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Primary Contact #*:</td>
                                <td>
                                    <asp:Label ID="lbl_PrimaryContact" runat="server" CssClass="Text">612-803-1111</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Secondary Contact #:</td>
                                <td>
                                    <asp:Label ID="lbl_SecondaryContact" runat="server" CssClass="Text">612-866-2635</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Primary Email*:</td>
                                <td>
                                    <asp:Label ID="lbl_PrimaryEmail" runat="server" CssClass="Text">IvanAMustang@hotmail.com</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Secondary Email:</td>
                                <td>
                                    <asp:Label ID="lbl_SecondaryEmail" runat="server" CssClass="Text">Ivan_ivana@usfamily.net</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Gender*:</td>
                                <td>
                                    <asp:Label ID="lbl_Gender" runat="server" CssClass="Text">Male</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Morther&#39;s Maiden Name:</td>
                                <td>
                                    <asp:Label ID="lbl_Mother" runat="server" CssClass="Text">Male</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Disabled:</td>
                                <td>
                                    <asp:Label ID="lbl_Disabled" runat="server" CssClass="Text">No</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Race*:</td>
                                <td>
                                    <asp:Label ID="lbl_Address28" runat="server" CssClass="Text">Asia &amp; White</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Ethnicity*:</td>
                                <td>
                                    <asp:Label ID="lbl_Ethnicity" runat="server" CssClass="Text">Not Hispanic</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Preferred Language:</td>
                                <td>
                                    <asp:Label ID="lbl_PreferedLanguage" runat="server" CssClass="Text">English</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Education Level Completed:</td>
                                <td>
                                    <asp:Label ID="lbl_EducationLevel" runat="server" CssClass="Text">???????</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Marital Status:</td>
                                <td>
                                    <asp:Label ID="lbl_MaritalStatus" runat="server" CssClass="Text">???????</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Occupation:</td>
                                <td>
                                    <asp:Label ID="lbl_Occupation" runat="server" CssClass="Text">Roofer</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Military Service:</td>
                                <td>
                                    <asp:Label ID="lbl_MilitaryService" runat="server" CssClass="Text">Active</asp:Label>
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
                                    <asp:Label ID="lbl_CoFirstName" runat="server" CssClass="Text">Ivana</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Middle Name:</td>
                                <td>
                                    <asp:Label ID="lbl_CoMidName" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Last Name:</td>
                                <td>
                                    <asp:Label ID="lbl_CoLastName" runat="server" CssClass="Text">Jaguar</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    DOB:</td>
                                <td>
                                    <asp:Label ID="lbl_CoDOB" runat="server" CssClass="Text">01/01/1969</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Last 4 SSN:</td>
                                <td>
                                    <asp:Label ID="lbl_CoLast4SSN" runat="server" CssClass="Text">XXX-XX-4321</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Disabled:</td>
                                <td>
                                    <asp:Label ID="lbl_CoDisabled" runat="server" CssClass="Text">No</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Occupation:</td>
                                <td>
                                    <asp:Label ID="lbl_CoOccupation" runat="server" CssClass="Text">Roofer</asp:Label>
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
                                    <asp:Label ID="lbl_ContactAdd1" runat="server" CssClass="Text">1234 Any Street 
                                    NW</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Address 2:</td>
                                <td>
                                    <asp:Label ID="lbl_ContactAdd2" runat="server" CssClass="Text">#2</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    City*:</td>
                                <td>
                                    <asp:Label ID="lbl_ContactCity" runat="server" CssClass="Text">Your Town</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    State,Zip*:</td>
                                <td>
                                    <asp:Label ID="lbl_ContactStateZip" runat="server" CssClass="Text">MN, 
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
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="right">
                    <asp:Button ID="btn_Save0" runat="server" Text="Save" 
                CssClass="MyButton" />
                </td>
    </tr>
</table>
