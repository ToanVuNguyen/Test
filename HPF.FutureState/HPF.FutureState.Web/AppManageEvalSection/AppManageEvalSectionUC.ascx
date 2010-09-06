<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AppManageEvalSectionUC.ascx.cs" Inherits="HPF.FutureState.Web.AppManageEvalSection.AppManageEvalSectionUC" %>
<asp:ScriptManager ID="myScript" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="myupdatepan" runat="server">
<ContentTemplate>  
<table width="100%">
    <tr>
        <td colspan="2" align="center" >
            <h1>
            Manage Evaluation Section
            </h1>
        </td>
    </tr>
    <tr>
        <td colspan="2" align = "left">
        <asp:BulletedList ID="lblInstruction" CssClass="sidelinks" runat="server">
        <asp:ListItem>To update existing template section information, please use section drop down list for retrieval, make the change, then press Update button.</asp:ListItem>
        <asp:ListItem>To add new template section, simply type the information and press Add New button.</asp:ListItem>
        </asp:BulletedList>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="ErrorMessage">
            <asp:BulletedList ID="lblErrorMessage" runat="server" CssClass="ErrorMessage">
            </asp:BulletedList>
        </td>
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Section:</td>        
        <td  align="left">
            <asp:DropDownList ID="ddlSection" runat="server" Height="16px" 
                CssClass="Text" Width="400px" AutoPostBack="true" 
                onselectedindexchanged="ddlSection_SelectedIndexChanged">
            </asp:DropDownList>
        </td>        
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Section Name:</td>        
        <td  align="left"><asp:TextBox ID="txtSectionName" Columns="100" Rows="2" runat="server"/>
        </td>        
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Section Description:</td>        
        <td  align="left"><asp:TextBox ID="txtSectionDescription" Columns="100" Rows="2" runat="server"/>
        </td>        
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Active:</td>        
        <td  align="left"><asp:CheckBox ID="chkActive" runat="server" /></td>        
    </tr>
    <tr>
    <td colspan="2" align="center" nowrap="nowrap">
        <asp:Button ID="btnAddNew" runat="server" Text="Add New" Width="120px" 
            CssClass="MyButton" onclick="btnAddNew_Click"/>
        <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="120px" 
            CssClass="MyButton" onclick="btnUpdate_Click"/>
        <asp:Button ID="btnClose" runat="server" Text="Close" Width="120px" 
            CssClass="MyButton" onclick="btnClose_Click"/>
    </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>