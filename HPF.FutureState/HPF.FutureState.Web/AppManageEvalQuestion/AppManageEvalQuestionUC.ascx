<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AppManageEvalQuestionUC.ascx.cs" Inherits="HPF.FutureState.Web.AppManageEvalQuestion.AppManageEvalQuestionUC" %>
<asp:ScriptManager ID="myScript" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="myupdatepan" runat="server">
<ContentTemplate>  
<table width="100%">
    <tr>
        <td colspan="2" align="center" >
            <h1>
            Manage Evaluation Question
            </h1>
        </td>
    </tr>
    <tr>
        <td colspan="2" align = "left">
        <asp:BulletedList ID="lblInstruction" CssClass="sidelinks" runat="server">
        <asp:ListItem>To update existing question information, please use section drop down list for retrieval, make the change, then press Update button.</asp:ListItem>
        <asp:ListItem>To add new question, simply type the information and press Add New button.</asp:ListItem>
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
            Questions:</td>        
        <td  align="left">
            <asp:DropDownList ID="ddlQuestion" runat="server" Height="16px" 
                CssClass="Text" Width="500px" AutoPostBack="true" 
                onselectedindexchanged="ddlQuestion_SelectedIndexChanged">
            </asp:DropDownList>
        </td>        
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Question:</td>        
        <td  align="left"><asp:TextBox ID="txtQuestion" Columns="100" Rows="2" runat="server"/>
        </td>        
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Question Description:</td>        
        <td  align="left"><asp:TextBox ID="txtQuestionDescription" Columns="100" Rows="2" runat="server"/>
        </td>        
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Question Example:</td>        
        <td  align="left"><asp:TextBox ID="txtQuestionExample" Columns="100" Rows="2" runat="server"/>
        </td>        
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Question Type:</td>        
        <td  align="left"><asp:TextBox ID="txtQuestionType" Columns="100" Rows="2" runat="server"/>
        </td>        
    </tr>
    <tr>
        <td class="sidelinks" align="right" nowrap="nowrap">
            Question Score:</td>        
        <td  align="left">
            <asp:DropDownList ID="ddlQuestionScore" runat="server" Height="16px" 
                CssClass="Text" Width="50px" AutoPostBack="true">
            </asp:DropDownList>
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