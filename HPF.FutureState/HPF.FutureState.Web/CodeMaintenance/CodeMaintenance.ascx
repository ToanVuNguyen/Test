<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CodeMaintenance.ascx.cs" Inherits="HPF.FutureState.Web.CodeMaintenance.CodeMaintenance" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc2" %>
<%@ Register assembly="HPF.FutureState.Web.HPFWebControls" namespace="HPF.FutureState.Web.HPFWebControls" tagprefix="cc1" %>

            <asp:ScriptManager ID="myscript" runat="server">
            </asp:ScriptManager>


<table>
    <tr>
        <td align="center" colspan="2">
            <h1>Code Maintenance</h1></td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <td align="right" class="sidelinks" nowrap="nowrap">
            Code Set*:
                    </td>
                    <td>
            <asp:DropDownList ID="dropCodeSet" runat="server" CssClass="Text" 
                Width="280px">
            </asp:DropDownList>
                    </td>
                    <td align="right" class="sidelinks" width="130" nowrap="nowrap">
            Include Inactive*:</td>
                    <td>
            <asp:DropDownList ID="dropIncludeInactive" runat="server" CssClass="Text" 
                Width="80px" Height="16px">
                <asp:ListItem Value="false">No</asp:ListItem>
                <asp:ListItem Selected="True" Value="true">Yes</asp:ListItem>
            </asp:DropDownList>
                    </td>                    
                </tr>
            </table>
        </td>
        <td align="right">
            <asp:Button ID="btnSearch" runat="server" CssClass="MyButton" 
                Text="Search" Width="120px" onclick="btnSearch_Click" />
                    </td>
    </tr>
    <tr>
        <td>
            <asp:BulletedList ID="lblErrorMessage" BulletStyle="Square" runat="server" CssClass="ErrorMessage">
            </asp:BulletedList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="sidelinks">
            Code Item List:</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
        <asp:Panel CssClass="ScrollTable" ID="myPannel" runat="server"
                Visible="True" BorderWidth="1" BorderColor="LightGray">
                <asp:UpdatePanel ID="myupdatepan" runat="server">
                    <ContentTemplate>
                <asp:GridView ID="grvCodeMaintenance" runat="server" CellPadding="2" ForeColor="#333333"
                    GridLines="Vertical" AutoGenerateColumns="False" CssClass="GridViewStyle" 
                    DataKeyNames="RefCodeItemId" width="100%">
                    <RowStyle CssClass="RowStyle"  />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1"  ForeColor="#333333" />
                    <HeaderStyle CssClass="FixedHeader" HorizontalAlign="Center" 
                        BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                    <Columns>
                        <asp:BoundField DataField="RefCodeSetName" HeaderText="Code Set" 
                            ItemStyle-HorizontalAlign="Left"  HeaderStyle-Wrap="false" >
                            <HeaderStyle Wrap="False" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="CodeValue" HeaderText="Code" 
                            ItemStyle-HorizontalAlign="Left"  HeaderStyle-Wrap="false">
                            <HeaderStyle Wrap="False" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="CodeDescription" HeaderText="Description" 
                            ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="100%" 
                            HeaderStyle-Wrap="false" >
                            <HeaderStyle Wrap="False" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="CodeComment" ItemStyle-HorizontalAlign="Right"  
                            HeaderText="Comment" HeaderStyle-Wrap="false" ItemStyle-Width="100%">
                            <HeaderStyle Wrap="False" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="SortOrder" ItemStyle-HorizontalAlign="Right"  
                            HeaderText="Sort Order" HeaderStyle-Wrap="false" >
                            <HeaderStyle Wrap="False" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ActiveInd" HeaderText="Active" 
                            ItemStyle-HorizontalAlign="Left"  HeaderStyle-Wrap="false" >
                            <HeaderStyle Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>                        
                        <asp:BoundField DataField="CreateDate" HeaderText="Created" 
                            DataFormatString="{0:d}"></asp:BoundField>
                        <asp:BoundField DataField="ChangeLastDate" HeaderText="Updated" 
                            DataFormatString="{0:d}"></asp:BoundField>
                        <asp:CommandField ShowSelectButton="true" ButtonType="Button" 
                            ControlStyle-CssClass="MyButton" ItemStyle-HorizontalAlign="Center" 
                            HeaderText="Select" HeaderStyle-Wrap="false" >
                            <controlstyle cssclass="MyButton" />
                            <HeaderStyle Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>
                </ContentTemplate>
                </asp:UpdatePanel>
                </asp:Panel>
                </td>
        <td valign="top">
            <table class="style1">
                <tr>
                    <td>
            <asp:Button ID="btnNewCode" runat="server" CssClass="MyButton" 
                Text="New Code" Width="120px" onclick="btnNewCode_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
            <asp:Button ID="btnCode" runat="server" CssClass="MyButton" 
                Text="Edit Code" Width="120px" onclick="btnCode_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    </table>
<script type="text/javascript">
    var mypanel = document.getElementById('<%=myPannel.ClientID %>');
    if(mypanel != null)
    {
        mypanel.style.width= screen.width - 166;             
        mypanel.style.height = screen.height - 488;
    }
              
</script>
<asp:HiddenField ID="SelectedRowIndex" runat="server"  />
                