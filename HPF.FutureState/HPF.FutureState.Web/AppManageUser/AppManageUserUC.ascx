<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AppManageUserUC.ascx.cs" Inherits="HPF.FutureState.Web.AppManageUser.AppManageUserUC" %>
<div class="Text">
    <asp:Label ID="lblErrorMessage" runat="server" CssClass="ErrorMessage"></asp:Label>
</div>
<div align="center">
<h3>Manage User</h3></div>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div align="center">
<table width="80%">
<tr>
<td align="right">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
 <ContentTemplate>
    <asp:Panel ID="panHPFUser" runat="server" CssClass="ScrollTable" Width="100%"
                    Height="80%" Visible="true">
    <asp:GridView ID="grdvHPFUser" runat="server" CellPadding="4" CssClass="GridViewStyle" ForeColor="#333333" 
                    AutoGenerateColumns="false" AllowPaging="true"
                    onrowcreated="grdvHPFUser_RowCreated" onrowcommand="grdvHPFUser_RowCommand">
                    <PagerSettings Visible="False" />
                    <RowStyle BackColor="#EFF3FB" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#336699" CssClass="FixedHeader" Font-Size="XX-Small" 
                        ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Index" Visible="false" >  
                            <ItemTemplate>
                                <asp:Label ID="lblHpfUserId" runat="server" Text='<%# Bind("HpfUserId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User Login ID" ItemStyle-Width="80" >  
                            <ItemTemplate>
                                <asp:HyperLink runat="server" Text='<%# Bind("UserLoginId") %>' ID="lnkUserLoginId"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="First Name" >  
                            <ItemTemplate>
                                <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Name" >  
                            <ItemTemplate>
                                <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active" ShowHeader="true">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnActivate" runat="server" CausesValidation="False" CommandName="Activate"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblActiveInd" runat="server" Text='<%# Bind("ActiveInd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>            
                    </Columns>
                    
    </asp:GridView>
    </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
</td>
<td valign="top" align="left">
    <asp:Button ID="btnAddNew" runat="server" Text="Add User" Width="120px" 
            CssClass="MyButton" onclick="btnAddNew_Click"/></td>
</tr>
<tr>
<td align="right">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
         <ContentTemplate>
            <table>
            <tr>
            <td>
            <asp:Label ID="lblMinRow" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lbl1" runat="server" Text=" - " Visible="false"></asp:Label>
            <asp:Label ID="lblMaxRow" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lbl2" runat="server" Text=" of " Visible="false"></asp:Label>
            <asp:Label ID="lblTotalRowNum" runat="server" Visible="false"></asp:Label>
            &nbsp;
            <asp:LinkButton ID="lbtnFirst" CommandName="First" OnCommand="lbtnNavigate_Click"
                runat="server" Text="&lt;&lt;" Visible="false" CssClass="NoUnderLine" OnClientClick="ShowWaitPanel()"></asp:LinkButton>
            &nbsp;
            <asp:LinkButton ID="lbtnPrev" CommandName="Prev" OnCommand="lbtnNavigate_Click" runat="server"
                Text="&lt;" Visible="false" CssClass="NoUnderLine" OnClientClick="ShowWaitPanel()"></asp:LinkButton>
            &nbsp;<asp:PlaceHolder ID="phPages" runat="server" Visible="true"></asp:PlaceHolder>
            <asp:LinkButton ID="lbtnNext" CommandName="Next" OnCommand="lbtnNavigate_Click" runat="server"
                Text="&gt;" Visible="false" CssClass="NoUnderLine" OnClientClick="ShowWaitPanel()"></asp:LinkButton>
            &nbsp;
            <asp:LinkButton ID="lbtnLast" CommandName="Last" OnCommand="lbtnNavigate_Click" runat="server"
                Text="&gt;&gt;" Visible="false" CssClass="NoUnderLine" OnClientClick="ShowWaitPanel()"></asp:LinkButton>
            <asp:Label ID="lblTemp" runat="server" Text="" Visible="false"></asp:Label>
            </td>
            <td width="40">&nbsp;</td>
             <td><div id="waitPanel" style="display:none">Please wait...</div></td>
             </tr>
            </table>
            </ContentTemplate>
            </asp:UpdatePanel>
</td>
<td></td>
</tr> 
</table>
</div>
<script type="text/javascript">
   function ShowWaitPanel()
   {
        var mypanel = document.getElementById('waitPanel');
        mypanel.style.display ="block";
   }
</script>
