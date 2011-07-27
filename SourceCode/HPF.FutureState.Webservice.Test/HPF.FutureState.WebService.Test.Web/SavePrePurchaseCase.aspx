<%@ Page Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="SavePrePurchaseCase.aspx.cs" Inherits="HPF.FutureState.WebService.Test.Web.SavePrePurchaseCase" Title="HPF Webservice Test Application - Save Pre-Purchase Case" %>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    
    <div style="text-align:left"><h1>Save Pre-Purchase Case</h1></div>
    <table style="border-bottom-style: solid; border-bottom-width: medium; border-bottom-color: #49A3FF" width="100%">
        <tr>
        <td align="left">
            <table>
                <tr>
                    <td align="center" class="sidelinks" colspan="2">
                        Authentication Info</td>
                </tr>
                <tr>
                    <td align="right">
            
            <asp:Label CssClass="sidelinks"  ID="Label120" runat="server" Text="Username" ></asp:Label>
                    </td>
                    <td>
            <asp:TextBox CssClass="Text" ID="txtUsername" runat="server" Text="admin" Width="128px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
            <asp:Label CssClass="sidelinks" ID="Label121" runat="server" Text="Password"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox CssClass="Text" ID="txtPassword" runat="Server" TextMode="Password" Width="128px"></asp:TextBox>
            
                    </td>
                </tr>
                
            </table>
            <br />
            <br />
        </td>
        </tr>
    </table>
    
    <div>
     <table style="border-bottom-style: solid; border-bottom-width: medium; border-bottom-color: #49A3FF" width="100%">
        <tr>
        <td align="left">
            <table>
                <tr>
                    <td align="center" class="sidelinks">
                        Select file: <asp:FileUpload ID="fileUpload" runat="server"  />
                    </td>
                    <td>
                        <asp:Button id="UploadBtn" Text="Read File" runat="server" Width="105px" 
                            onclick="UploadBtn_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </td>
        </tr>
        <tr>
        <td align="left">
            <table>
                <tr>
                    <td align="center" class="sidelinks">
                        PpcId: <asp:TextBox runat="server" ID="txtPpcIdInput"/>
                    </td>
                    <td>
                        <asp:Button id="btnRetrieve" Text="Retrieve" runat="server" Width="105px" Enabled="false"/>
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </td>
        </tr>
    </table>
    
    <br />
    
    <asp:Button ID="btnSave" runat="server" Text="Save Pre-Purchase Case" onclick="btnSave_Click"/>
    
    <br />
    <table>
       <tr>    
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label6" runat="server" Text="ApplicantId"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtApplicantId" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label8" runat="server" Text="RightPartyContactInd"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtRightPartyContactInd" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>    
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label24" runat="server" Text="RpcMostrecentDt"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtRpcMostrecentDt" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label25" runat="server" Text="NoRpcReason"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtNoRpcReason" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>    
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label26" runat="server" Text="CounselingAcceptedDt"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCounselingAcceptedDt" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label27" runat="server" Text="CounselingScheduledDt"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCounselingScheduledDt" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>    
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label28" runat="server" Text="CounselingCompletedDt"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCounselingCompletedDt" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label47" runat="server" Text="CounselingRefusedDt"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCounselingRefusedDt" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>    
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label48" runat="server" Text="FirstCounseledDt"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtFirstCounseledDt" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label49" runat="server" Text="SecondCounseledDt"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtSecondCounseledDt" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>    
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label50" runat="server" Text="EdModuleCompletedDt"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtEdModuleCompletedDt" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label51" runat="server" Text="InboundCallToNumDt"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtInboundCallToNumDt" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>    
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label52" runat="server" Text="InboundCallToNumReason"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtInboundCallToNumReason" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label53" runat="server" Text="ActualCloseDt"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtActualCloseDt" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label111" runat="server" Text="Change last User ID"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtWorkingUserID" runat="server"></asp:TextBox>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
        <td colspan="2">&nbsp;</td>
        </tr>
        <tr>            
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label1" runat="server" Text="PpcId"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtPpcId" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label7" runat="server" Text="Agency case number"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtAgencyCaseNumber" runat="server"></asp:TextBox>
            </td>
            
        </tr>
        
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label5" runat="server" Text="Agency ID"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtAgencyId" runat="server"></asp:TextBox>
            </td>            
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label3" runat="server" Text="MortgageProgramCd"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtMortgageProgramCd" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label9" runat="server" Text="AcctNum"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtAcctNum" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label10" runat="server" Text="BorrowerFName"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtBorrowerFName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label11" runat="server" Text="BorrowerLName"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtBorrowerLName" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label12" runat="server" Text="CoBorrowerFName"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCoBorrowerFName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label13" runat="server" Text="CoBorrowerLName"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCoBorrowerLName" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label14" runat="server" Text="PropAddr1"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtPropAddr1" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label15" runat="server" Text="PropAddr2"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtPropAddr2" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label16" runat="server" Text="PropCity"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtPropCity" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label17" runat="server" Text="PropStateCd"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtPropStateCd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label18" runat="server" Text="PropZip"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtPropZip" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label19" runat="server" Text="MailAddr1"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtMailAddr1" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label20" runat="server" Text="MailAddr2"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtMailAddr2" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label21" runat="server" Text="MailCity"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtMailCity" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label22" runat="server" Text="MailStateCd"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtMailStateCd" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label23" runat="server" Text="MailZip"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtMailZip" runat="server"></asp:TextBox>
            </td>
            <td>
                
            </td>
            <td>
                
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label29" runat="server" Text="BorrowerAuthorizationInd"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtBorrowerAuthorizationInd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label30" runat="server" Text="MotherMaidenLName"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtMotherMaidenLName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label31" runat="server" Text="PrimaryContactNo"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtPrimaryContactNo" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label32" runat="server" Text="SecondaryContactNo"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtSecondaryContactNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label33" runat="server" Text="BorrowerEmployerName"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtBorrowerEmployerName" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label34" runat="server" Text="BorrowerJobTitle"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtBorrowerJobTitle" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label35" runat="server" Text="BorrowerSelfEmployedInd"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtBorrowerSelfEmployedInd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label36" runat="server" Text="BorrowerYearsEmployed"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtBorrowerYearsEmployed" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label37" runat="server" Text="CoBorrowerEmployerName"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCoBorrowerEmployerName" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label38" runat="server" Text="CoBorrowerJobTitle"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCoBorrowerJobTitle" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label39" runat="server" Text="CoBorrowerSelfEmployedInd"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCoBorrowerSelfEmployedInd" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label40" runat="server" Text="CoBorrowerYearsEmployed"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCoBorrowerYearsEmployed" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label41" runat="server" Text="CounselorIdRef"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCounselorIdRef" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label42" runat="server" Text="CounselorFName"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCounselorFName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label43" runat="server" Text="CounselorLName"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCounselorLName" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label44" runat="server" Text="CounselorEmail"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCounselorEmail" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label45" runat="server" Text="CounselorPhone"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCounselorPhone" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label46" runat="server" Text="CounselorExt"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCounselorExt" runat="server"></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                <asp:Label CssClass = "sidelinks" ID="Label4" runat="server" Text="CounselingDurationMins"></asp:Label>             
            </td>
            <td>
                <asp:TextBox CssClass = "Text" ID="txtCounselingDurationMins" runat="server"></asp:TextBox>
            </td>
            <td></td>
            <td></td>
        </tr>
        </table>

    <br />
    
    <asp:Button ID="btnSave2" runat="server" Text="Save Pre-Purchase Case"  onclick="btnSave_Click"/>
        
    <br />
    <br />
    <asp:Label ID="Label116" runat="server" Text="Pre-Purchase Budget Items"></asp:Label>
                                                <br />
    <asp:GridView ID="grdvPPBudgetItem" runat="server" 
        ShowFooter = "True" 
        AutoGenerateColumns = "False" 
        onrowediting="grdvPPBudgetItem_RowEditing"
        OnRowCancelingEdit = "grdvPPBudgetItem_RowCancelEditing"
        onrowupdating="grdvPPBudgetItem_RowUpdating"
        onrowcommand="grdvPPBudgetItemRowCommand" 
        onrowdeleting="grdvPPBudgetItem_RowDeleting">
        <Columns>
            <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="AddNew" Text="New"></asp:LinkButton>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>            
            
            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ShowHeader="false" />
            
            
            <asp:TemplateField HeaderText="Amount" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtPPBudgetItemAmt" runat="server" Text='<%# Eval("PPBudgetItemAmt") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtPPBudgetItemAmt" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("PPBudgetItemAmt") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Note" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtPPBudgetNote" runat="server" Text='<%# Eval("PPBudgetNote") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtPPBudgetNote" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("PPBudgetNote") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            
            <asp:TemplateField HeaderText="Subcategory ID" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtBudgetSubcategoryId" runat="server" Text='<%# Eval("BudgetSubcategoryId") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtBudgetSubcategoryId" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("BudgetSubcategoryId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                                    
            <asp:TemplateField HeaderText="Index" >  
                <FooterTemplate>
                    <asp:Label ID="lblPPBudgetItemId" runat="server"></asp:Label> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblPPBudgetItemId" runat="server" Text='<%# Bind("PPBudgetItemId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>            
    </asp:GridView>
        <br />
       Proposed Pre-Purchase Budget Items<asp:GridView ID="grdvProposedPPBudgetItem" runat="server" 
        ShowFooter = "True" 
        AutoGenerateColumns = "False" onrowcommand="grdvProposedPPBudgetItem_RowCommand" 
            onrowdeleting="grdvProposedPPBudgetItem_RowDeleting" 
            onrowediting="grdvProposedPPBudgetItem_RowEditing" 
            onrowupdating="grdvProposedPPBudgetItem_RowUpdating" 
            onrowcancelingedit="grdvProposedPPBudgetItem_RowCancelingEdit">
        <Columns>
            <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="True" 
                        CommandName="Update" Text="Update"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" 
                        CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="False" 
                        CommandName="AddNew" Text="New"></asp:LinkButton>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton6" runat="server" CausesValidation="False" 
                        CommandName="Edit" Text="Edit"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>            
            
            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ShowHeader="false" />
            
            
            <asp:TemplateField HeaderText="Amount" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtProposedBudgetItemAmt" runat="server" 
                        Text='<%# Eval("ProposedBudgetItemAmt") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtProposedBudgetItemAmt" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label304" runat="server" Text='<%# Bind("ProposedBudgetItemAmt") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Note" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtProposedBudgetNote" runat="server" 
                        Text='<%# Eval("ProposedBudgetNote") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtProposedBudgetNote" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label305" runat="server" Text='<%# Bind("ProposedBudgetNote") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            
            <asp:TemplateField HeaderText="Subcategory ID" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtBudgetSubcategoryId1" runat="server" 
                        Text='<%# Eval("BudgetSubcategoryId") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtBudgetSubcategoryId1" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label306" runat="server" 
                        Text='<%# Bind("BudgetSubcategoryId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
 
            
            <asp:TemplateField HeaderText="Index" >               
                <FooterTemplate>
                    <asp:Label ID="lblProposedBudgetItemId" runat="server"></asp:Label> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblProposedBudgetItemId" runat="server" 
                        Text='<%# Bind("PPPBudgetItemId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>            
    </asp:GridView>
    <br />
    <asp:Label ID="Label113" runat="server" Text="Pre-Purchase Budget Assets"></asp:Label>
        <asp:GridView ID="grdvPPBudgetAsset" runat="server" 
        ShowFooter = "True" 
        AutoGenerateColumns = "False" onrowcommand="grdvPPBudgetAssetRowCommand" 
            onrowdeleting="grdvPPBudgetAsset_RowDeleting" 
            onrowediting="grdvPPBudgetAsset_RowEditing" 
            onrowupdating="grdvPPBudgetAsset_RowUpdating">
        <Columns>
            <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="AddNew" Text="New"></asp:LinkButton>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>            
            
            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ShowHeader="false" />
            
            <asp:TemplateField HeaderText="Asset name" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtPPBudgetAssetName" runat="server" Text='<%# Eval("PPBudgetAssetName") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtPPBudgetAssetName" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("PPBudgetAssetName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Asset value" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtPPBudgetAssetValue" runat="server" Text='<%# Eval("PPBudgetAssetValue") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtPPBudgetAssetValue" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblPPBudgetAssetValue" runat="server" Text='<%# Bind("PPBudgetAssetValue") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Asset Note" >  
                <EditItemTemplate>
                    <asp:TextBox ID="txtPPBudgetAssetNote" runat="server" Text='<%# Eval("PPBudgetAssetNote") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtPPBudgetAssetNote" runat="server"></asp:TextBox> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblPPBudgetAssetNote" runat="server" Text='<%# Bind("PPBudgetAssetNote") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Index" >  
                <FooterTemplate>
                    <asp:Label ID="lblPPBudgetAssetId" runat="server"></asp:Label> </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblPPBudgetAssetId" runat="server" Text='<%# Bind("PPBudgetAssetId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>            
    </asp:GridView>
    <br />
    <br />
    <asp:Label ID="lblMessage" runat="server" Text="Message:"></asp:Label>
    <br />
    <asp:GridView ID="grdvMessages" runat="server">
    </asp:GridView>
    </div>
</asp:Content>
