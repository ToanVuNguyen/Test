<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaseDetail.ascx.cs" Inherits="HPF.FutureState.Web.ForeclosureCaseDetail.ForeclosureCaseDetail" %>
<link href="../Styles/HPF.css" rel="stylesheet" type="text/css" />
<table style="width:100%;">
    <tr>
        <td>
            <table style="width:100%;">
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
                                    <asp:Label ID="lbl_Address2" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    City*:</td>
                                <td>
                                    <asp:Label ID="lbl_Address3" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    State, Zip*:</td>
                                <td>
                                    <asp:Label ID="lbl_Address4" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Primary Residence:</td>
                                <td>
                                    <asp:Label ID="lbl_Address5" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Owner Occupied*:</td>
                                <td>
                                    <asp:Label ID="lbl_Address6" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Property Code:</td>
                                <td>
                                    <asp:Label ID="lbl_Address7" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Number of Occupants*:</td>
                                <td>
                                    <asp:Label ID="lbl_Address8" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Purchase Year:</td>
                                <td>
                                    <asp:Label ID="lbl_Address9" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Purchase Price:</td>
                                <td>
                                    <asp:Label ID="lbl_Address10" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Current Market Value:</td>
                                <td>
                                    <asp:Label ID="lbl_Address11" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    For Sale Indicator:</td>
                                <td>
                                    <asp:Label ID="lbl_Address12" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Realty Company:</td>
                                <td>
                                    <asp:Label ID="lbl_Address13" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Home Asking Price:</td>
                                <td>
                                    <asp:Label ID="lbl_Address14" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Primary Residence Est. Mkt Value:</td>
                                <td>
                                    <asp:Label ID="lbl_Address15" runat="server" CssClass="Text"></asp:Label>
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
                                    <asp:Label ID="lbl_Address16" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Middle Name*:</td>
                                <td>
                                    <asp:Label ID="lbl_Address17" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Last Name*:</td>
                                <td>
                                    <asp:Label ID="lbl_Address18" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    DOB*:</td>
                                <td>
                                    <asp:Label ID="lbl_Address19" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Last 4 of SSN:</td>
                                <td>
                                    <asp:Label ID="lbl_Address20" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Primary Contact #*:</td>
                                <td>
                                    <asp:Label ID="lbl_Address21" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Secondary Contact #:</td>
                                <td>
                                    <asp:Label ID="lbl_Address22" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Primary Email*:</td>
                                <td>
                                    <asp:Label ID="lbl_Address23" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Secondary Email:</td>
                                <td>
                                    <asp:Label ID="lbl_Address24" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Gender*:</td>
                                <td>
                                    <asp:Label ID="lbl_Address25" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Morther&#39;s Maiden Name:</td>
                                <td>
                                    <asp:Label ID="lbl_Address26" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Disabled:</td>
                                <td>
                                    <asp:Label ID="lbl_Address27" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Race*:</td>
                                <td>
                                    <asp:Label ID="lbl_Address28" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Ethnicity*:</td>
                                <td>
                                    <asp:Label ID="lbl_Address29" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Preferred Language:</td>
                                <td>
                                    <asp:Label ID="lbl_Address30" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Education Level Completed:</td>
                                <td>
                                    <asp:Label ID="lbl_Address31" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Marital Status:</td>
                                <td>
                                    <asp:Label ID="lbl_Address32" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Occupation:</td>
                                <td>
                                    <asp:Label ID="lbl_Address33" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Military Service:</td>
                                <td>
                                    <asp:Label ID="lbl_Address34" runat="server" CssClass="Text"></asp:Label>
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
                                    <asp:Label ID="lbl_Address35" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Middle Name:</td>
                                <td>
                                    <asp:Label ID="lbl_Address36" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Last Name:</td>
                                <td>
                                    <asp:Label ID="lbl_Address37" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    DOB:</td>
                                <td>
                                    <asp:Label ID="lbl_Address38" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Last 4 SSN:</td>
                                <td>
                                    <asp:Label ID="lbl_Address39" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Disabled:</td>
                                <td>
                                    <asp:Label ID="lbl_Address40" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Occupation:</td>
                                <td>
                                    <asp:Label ID="lbl_Address41" runat="server" CssClass="Text"></asp:Label>
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
                                    <asp:Label ID="lbl_Address42" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    Address 2:</td>
                                <td>
                                    <asp:Label ID="lbl_Address43" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    City*:</td>
                                <td>
                                    <asp:Label ID="lbl_Address44" runat="server" CssClass="Text"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="sidelinks">
                                    State,Zip*:</td>
                                <td>
                                    <asp:Label ID="lbl_Address45" runat="server" CssClass="Text"></asp:Label>
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
