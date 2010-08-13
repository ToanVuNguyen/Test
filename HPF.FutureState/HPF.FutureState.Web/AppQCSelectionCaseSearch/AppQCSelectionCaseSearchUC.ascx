<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AppQCSelectionCaseSearchUC.ascx.cs" Inherits="HPF.FutureState.Web.ApphQCSelectionCaseSearch.AppQCSelectionCaseSearchUC" %>
<%@ Register Assembly="HPF.FutureState.Web.HPFWebControls" Namespace="HPF.FutureState.Web.HPFWebControls"
    TagPrefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc2" %>
<asp:ScriptManager ID="myScript" runat="server">
    </asp:ScriptManager>
<table width="100%">
    <tr>
        <td colspan="6" align="center" >
            <h1>
            QC Selection Case Search
            </h1>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Agency*:</td>        
        <td  align="left" nowrap>
            <asp:DropDownList ID="ddlAgency" runat="server" Height="16px" 
                CssClass="Text" Width="280px">
            </asp:DropDownList>
        </td>        
        <td  align="right" class="sidelinks" nowrap="nowrap">
            From Year/Month*:
        </td>
        <td>
            <asp:DropDownList ID="ddlFromYearMonth" runat="server" Height="16px" 
                CssClass="Text" Width="80px">
            </asp:DropDownList>
        </td>
        <td align="right"><asp:Button ID="btnSearch" runat="server" Text="Search" Width="120px"
                CssClass="MyButton"/></td>
    </tr>
    <tr>
        <td>
        </td>
        <td width="280" >
        </td>
        <td  align="right" class="sidelinks" nowrap="nowrap">
            To Year/Month*:
        </td>
        <td>
            <asp:DropDownList ID="ddlToYearMonth" runat="server" Height="16px" 
                CssClass="Text" Width="80px">
            </asp:DropDownList>
        </td>        
        <td align="right"><asp:Button ID="btnClose" runat="server" Text="Close" Width="120px"
                CssClass="MyButton"/></td>
    </tr>
    <tr>
        <td colspan="6" class="ErrorMessage">
            <asp:BulletedList ID="lblErrorMessage" runat="server" CssClass="ErrorMessage">
            </asp:BulletedList>
        </td>
    </tr>
    <tr>
        <td colspan="6" class="sidelinks">
            Audit Case List:
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <cc1:StatefullScrollPanel ID="auditCaseList" runat="server" CssClass="ScrollTable"
                Width="100%" BorderColor="LightGray" BorderWidth="1">
                <asp:UpdatePanel ID="myUPanel" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grvAuditCaseList" runat="server" GridLines="Vertical"  Width="100%"
                            AutoGenerateColumns="false" DataKeyNames="FcId">
                            <HeaderStyle Wrap="false" CssClass="FixedHeader"  HorizontalAlign="Center" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle CssClass="RowStyle" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                            <EmptyDataTemplate>
                                <asp:Label ID="lblEmptySearch" runat="server">No Results Found</asp:Label>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField HeaderText="HPF Case ID" DataField="FcId"  ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"/>
                                <asp:BoundField HeaderText="HO FirstName" DataField="HomeowenerFirstName"  ItemStyle-HorizontalAlign="Right"  HeaderStyle-Wrap="false" />
                                <asp:BoundField HeaderText="HO LastName" DataField="HomeowenerLastName"  ItemStyle-HorizontalAlign="Right"  HeaderStyle-Wrap="false" />
                                <asp:BoundField HeaderText="Zip Code" DataField="ZipCode" ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                                <asp:BoundField HeaderText="Loan Number" DataField="LoanNumber" ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                                <asp:BoundField HeaderText="Counserlor" DataField="CounselorName" ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                                <asp:BoundField HeaderText="Servicer" DataField="ServicerName" ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                                <asp:BoundField HeaderText="Agency" DataField="Agency Name" ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                                <asp:CommandField ShowSelectButton="true" ButtonType="Button" ControlStyle-CssClass="MyButton"
                                    ItemStyle-HorizontalAlign="Center" HeaderText="Select" HeaderStyle-Wrap="false" />
                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="SelectedRowIndex" runat="server"  />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </cc1:StatefullScrollPanel>
        </td>
        <td valign="top" width="120">
                    <asp:Button ID="btnEditCase" runat="server" Text="Edit Audit Case" CssClass="MyButton"
                        Width="120px"/>
        </td>
    </tr>
</table>
