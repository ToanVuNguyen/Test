<%@ Page Language="VB" Inherits="AddSecureUser" CodeFile="AddSecureUser.aspx.vb"
    AutoEventWireup="false" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>NewUser</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">

    <script language="JavaScript">
	    function UpdateLoginPassword(FnameId,LastNameid)
	    {
            var LName;
            LName = document.getElementById("<%= txtLastName.ClientID %>").value;
            var FName;
            FName = document.getElementById("<%= txtFirstName.ClientID %>").value;
            var UserId;
            UserId = FName.charAt(0).toLowerCase() + LName.toLowerCase();
            document.getElementById("<%= txtUserID.ClientID %>").value = UserId;
            document.getElementById("<%= txtPassword.ClientID %>").value = UserId + "$123";
 	    }
    </script>

</head>
<body onload="return SetPassword()">
    <form id="Form1" method="post" runat="server">
        <div id="divEnterInfo" runat="server" visible="true">
            <table id="Table1" style="width: 520px; height: 243px" cellspacing="1" cellpadding="1"
                width="520" border="0">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="idPageTitle" runat="server" Text="" class='listTextBold'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 118px">
                        Last Name:</td>
                    <td>
                        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqLastName" runat="server" ErrorMessage="* Required"
                            Display="Dynamic" ControlToValidate="txtLastName"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="width: 118px">
                        First Name:</td>
                    <td>
                        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqFirstName" runat="server" ErrorMessage="* Required"
                            Display="Dynamic" ControlToValidate="txtFirstName"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="width: 118px">
                        Title:</td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td style="width: 118px">
                        Email:</td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqEmail" runat="server" ErrorMessage="* Required"
                            Display="Dynamic" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ControlToValidate="txtEmail" ID="regEmail" runat="server"
                            ErrorMessage="Invalid Email ID" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 118px">
                        Primary Phone:</td>
                    <td>
                        <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqPhone" runat="server" ErrorMessage="* Required"
                            Display="Dynamic" ControlToValidate="txtPhone"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 118px">
                        User ID:</td>
                    <td>
                        <asp:TextBox ID="txtUserID" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqUserID" runat="server" ErrorMessage="* Required"
                            Display="Dynamic" ControlToValidate="txtUserID"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="width: 118px">
                        Password:</td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqPassword" runat="server" ErrorMessage="* Required"
                            Display="Dynamic" ControlToValidate="txtPassword"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="width: 118px">
                      </td>
                    <td>
                        <asp:TextBox ID="txtPasswordConfirm" runat="server" TextMode="Password" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 118px">
                        Agency User ID:</td>
                    <td>
                        <asp:TextBox ID="txtAgencyUserID" runat="server"></asp:TextBox>                       
                        </td>
                </tr>
                <tr>
                    <td style="width: 118px">
                        Access Level:</td>
                    <td>
                        <asp:CheckBoxList ID="AccessLevel" runat="server" RepeatDirection="Vertical" RepeatColumns="2">
                            <asp:ListItem Value="125">Non-NHS Counselor</asp:ListItem>
                            <asp:ListItem Value="123">NHS Counselor</asp:ListItem>
                            <asp:ListItem Value="85">Counselor</asp:ListItem>
                            <asp:ListItem Value="84">Program Sponsor</asp:ListItem>
                            <asp:ListItem Value="83">Super user</asp:ListItem>
                            <asp:ListItem Value="35">Owner</asp:ListItem>
                            <asp:ListItem Value="34">Servicer</asp:ListItem>
                            <asp:ListItem Value="33">Report User</asp:ListItem>
                            <asp:ListItem Value="29">Admin</asp:ListItem>
                        </asp:CheckBoxList>
                        <!--<xasp:ListItem value="30">User</xasp:ListItem>-->
                    </td>
                </tr>
                <tr>
                    <td style="width: 118px">
                        Active:</td>
                    <td>
                        <asp:RadioButtonList ID="rdoActive" runat="server" RepeatDirection="Vertical" RepeatColumns="2">
                            <asp:ListItem Value="Y" Selected="True">Active</asp:ListItem>
                            <asp:ListItem Value="N">Inactive</asp:ListItem>
                        </asp:RadioButtonList>
                </tr>
                <!--<TR>
					<TD style="WIDTH: 118px">
                        Servicer</TD>
					<TD>
						<asp:DropDownList id="ENTITY" runat="server"></asp:DropDownList>

				<TR>
					<TD style="WIDTH: 118px">Entity Location</TD>
					<TD>
						<asp:DropDownList id="ddENTITYLocation" runat="server"></asp:DropDownList>
				</TR>
				</TR>-->
                <tr>
                    <td style="width: 118px">
                    </td>
                    <td>
                        <asp:Button ID="cmdOk" runat="server" Text="Save"></asp:Button>&nbsp;
                        <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CausesValidation="False"></asp:Button></td>
                </tr>
                <tr>
                    <td style="width: 118px">
                    </td>
                    <td>
                        <input type="hidden" id="IsEditMode" runat="server" name="IsEditMode" value="0" />
                        <input type="hidden" id="hidBSN_ENTITY_LOCTN_SEQ_ID" runat="server" name="hidBSN_ENTITY_LOCTN_SEQ_ID"
                            value="0" />
                        <input type="hidden" id="hidPROFILE_SEQ_ID" runat="server" name="hidPROFILE_SEQ_ID"
                            value="0" />
                        <input type="hidden" id="hidLocationId" runat="server" name="hidLocationId" value="0" />
                    </td>
                </tr>
            </table>
        </div>
        <table>
            <tr>
                <td style="width: 118px">
                </td>
                <td>
                    <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label>
                </td>
            </tr>
        </table>
        <div id="DivShowSavedData" runat="server" visible="False">
            <table id="Table2" style="width: 520px; height: 243px" cellspacing="1" cellpadding="1"
                width="520" border="0">
                <tr>
                    <td style="width: 118px">
                        Last Name:</td>
                    <td>
                        <asp:Label ID="lblLastName" runat="server"></asp:Label>
                </tr>
                <tr>
                    <td style="width: 118px">
                        First Name:</td>
                    <td>
                        <asp:Label ID="lblFirstName" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 118px">
                        Title:</td>
                    <td>
                        <asp:Label ID="lblTitle" runat="server"></asp:Label>
                </tr>
                <tr>
                    <td style="width: 118px">
                        Email:</td>
                    <td>
                        <asp:Label ID="lblEmail" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 118px">
                        Primary Phone:</td>
                    <td>
                        <asp:Label ID="lblPhone" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 118px">
                        User ID:</td>
                    <td>
                        <asp:Label ID="lblUserID" runat="server"></asp:Label>
                </tr>
                <tr>
                    <td style="width: 118px">
                        Password:</td>
                    <td>
                        <asp:Label ID="lblPassword" runat="server"></asp:Label>
                </tr>
                <tr>
                    <td style="width: 118px">
                        Agency User ID:</td>
                    <td>
                        <asp:Label ID="lblAgencyUserId" runat="server"></asp:Label>
                </tr>
                <tr>
                    <td style="width: 118px">
                        Access Level:</td>
                    <td>
                        <asp:Label ID="lblAccessLabel" runat="server" Text="*******"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 118px">
                        Active:</td>
                    <td>
                        <asp:Label ID="lblActive" runat="server"></asp:Label>
                </tr>
                <tr>
                    <td style="width: 118px">
                    </td>
                    <td>
                        <asp:Button ID="Finish" runat="server" Text="Email User"></asp:Button>
                    &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </form>

    <script language="javascript">
		function SetPassword()
		{
			var ctl;
                	ctl = document.getElementById("<%= IsEditMode.ClientID %>");
                	if (ctl != null)
                	{
                    	if (ctl.value=="1")
                    	{
                        	pwd = document.getElementById("<%= txtPassword.ClientID %>");
                        	//cpwd = document.getElementById("<%= txtPasswordConfirm.ClientID %>");
                        	if (pwd != null)
                            	pwd.value="*******";
                        	//cpwd.value="*******";
                        	ctl.value=="0"
                    	}
                	}
		
		}
    </script>

</body>
</html>
