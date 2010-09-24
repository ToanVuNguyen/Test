<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AppManageUserUC.ascx.cs" Inherits="HPF.FutureState.Web.AppManageUser.AppManageUserUC" %>
<div class="Text">
    <asp:Label ID="lblErrorMessage" runat="server" CssClass="ErrorMessage"></asp:Label>
</div>
<div align="center">
<h3>Manage User</h3></div>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
 <ContentTemplate>
    <asp:Panel ID="panHPFUser" runat="server" CssClass="ScrollTable" Width="100%"
                    Height="80%" Visible="true">
    <asp:GridView ID="grdvHPFUser" runat="server" CellPadding="4" 
                    CssClass="GridViewStyle" ForeColor="#333333" 
            AutoGenerateColumns="false" ShowFooter="true" 
            onrowcreated="grdvHPFUser_RowCreated" 
            onrowediting="grdvHPFUser_RowEditing" 
            onrowcancelingedit="grdvHPFUser_RowCancelingEdit" 
            onrowcommand="grdvHPFUser_RowCommand" onrowupdating="grdvHPFUser_RowUpdating">
                    <RowStyle BackColor="#EFF3FB" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#336699" CssClass="FixedHeader" Font-Size="XX-Small" 
                        ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:HyperLinkField DataTextField="HpfUserId" DataNavigateUrlFields="HpfUserId" DataNavigateUrlFormatString="../ManageUserPermission.aspx?userId={0}"
                                    HeaderText="User ID"/>
                        <asp:TemplateField HeaderText="Index" Visible="false" >  
                            <ItemTemplate>
                                <asp:Label ID="lblHpfUserId" runat="server" Text='<%# Bind("HpfUserId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User Login ID" ItemStyle-Width="80" >  
                            <EditItemTemplate>
                                <asp:TextBox ID="txtUserLoginId" runat="server" Text='<%# Eval("UserLoginId") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtUserLoginId" runat="server"></asp:TextBox> 
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblUserLoginId" runat="server" Text='<%# Bind("UserLoginId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Password" >  
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPassword" runat="server" Text='<%# Eval("Password") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox> </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPassword" runat="server" Text='<%# Bind("Password") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="First Name" >  
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFirstName" runat="server" Text='<%# Eval("FirstName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox> </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Name" >  
                            <EditItemTemplate>
                                <asp:TextBox ID="txtLastName" runat="server" Text='<%# Eval("LastName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox> </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Email" >  
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEmail" runat="server" Text='<%# Eval("Email") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox> </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Edit" ShowHeader="True" ItemStyle-Width="100px">
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbtnUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="lbtnAddNew" runat="server" CausesValidation="False" CommandName="AddNew" Text="New"></asp:LinkButton>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
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
 

