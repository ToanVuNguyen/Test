
<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InsertCallLog.aspx.cs" Inherits="HPF.FutureState.WebService.Test.Web.InsertCallLog" %>
<asp:Content ID="ContentHeader" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <asp:Table ID="Table1" runat="server">
        <asp:TableRow ID="TableRow1" runat="server">
            <asp:TableCell ID="TableCell1" runat="server">
                <asp:Label ID="Label1" runat="server" Text="Call Center ID"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell2" runat="server">
                <asp:TextBox ID="txtCallCenterID" runat="server" Width="128px"></asp:TextBox>
            </asp:TableCell>
            
            <asp:TableCell ID="TableCell3" runat="server">
                <asp:Label ID="Label2" runat="server" Text="CC Agency Id"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell4" runat="server">
                <asp:TextBox ID="txtCcAgentIdKey" runat="Server" Width="128px"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow3" runat="server">
            <asp:TableCell ID="TableCell5" runat="server">
         <asp:Label ID="Label3" runat="server" Text="Start Date"></asp:Label>
         </asp:TableCell>
            <asp:TableCell ID="TableCell6" runat="server">
            <asp:Calendar ID="txtStartDate" runat="server" Font-Size="Small"></asp:Calendar>
            </asp:TableCell>
            
            <asp:TableCell ID="TableCell7" runat="server">
                <asp:Label ID="Label4" runat="server" Text="End Date"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell8" runat="server">
                <asp:Calendar ID="txtEndDate" runat="server" Font-Size="Small"></asp:Calendar>
            </asp:TableCell>
            
        </asp:TableRow>
        
        
        <asp:TableRow ID="TableRow6" runat="server">
             <asp:TableCell ID="TableCell9" runat="server">
         <asp:Label ID="Label5" runat="server" Text="DNIS"></asp:Label>
         </asp:TableCell>
            <asp:TableCell ID="TableCell10" runat="server">
                <asp:TextBox ID="txtDNIS" runat="server" Width="128px"></asp:TextBox>
            </asp:TableCell>
             <asp:TableCell ID="TableCell13" runat="server">
            <asp:Label ID="Label7" runat="server" Text="Call Center"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell14" runat="server">
                <asp:TextBox ID="txtCallCenter" runat="server" Width="128px"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow7" runat="server">
           
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow8" runat="server">
            <asp:TableCell ID="TableCell15" runat="server">
            <asp:Label ID="Label8" runat="server" Text="Call source code"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell16" runat="server">
                <asp:TextBox ID="txtCallSourceCd" runat="server" Width="128px"></asp:TextBox>            
            </asp:TableCell>
            
            <asp:TableCell ID="TableCell17" runat="server">
            <asp:Label ID="Label9" runat="server" Text="Calling reason"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell18" runat="server">
                <asp:TextBox ID="txtReasonToCall" runat="server" Width="128px"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow9" runat="server">
            
        </asp:TableRow>
        <asp:TableRow ID="TableRow10" runat="server">
            <asp:TableCell ID="TableCell19" runat="server">
            <asp:Label ID="Label10" runat="server" Text="Acct. Number"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell20" runat="server">
                <asp:TextBox ID="txtLoanAccountNumber" runat="server" Width="128px"></asp:TextBox>
            </asp:TableCell>
            
            <asp:TableCell ID="TableCell21" runat="server">
            <asp:Label ID="Label11" runat="server" Text="Service ID"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell22" runat="server">
                <asp:TextBox ID="txtServiceID" runat="server" Width="128px"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow11" runat="server">
            
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow12" runat="server">
            <asp:TableCell ID="TableCell23" runat="server">
            <asp:Label ID="Label12" runat="server" Text="FirstName"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell24" runat="server">
                <asp:TextBox ID="txtFirstName" runat="server" Width="128px"></asp:TextBox>            
            </asp:TableCell>
            
             <asp:TableCell ID="TableCell25" runat="server">
            <asp:Label ID="Label13" runat="server" Text="LastName"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell26" runat="server">
                <asp:TextBox ID="txtLastName" runat="server" Width="128px"></asp:TextBox>
            
            </asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow13" runat="server">
           
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow14" runat="server">
            <asp:TableCell ID="TableCell27" runat="server">
            <asp:Label ID="Label14" runat="server" Text="Other Servicer Name"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell28" runat="server">
                <asp:TextBox ID="txtOtherServicerName" runat="server" Width="128px"></asp:TextBox>
            
            </asp:TableCell>
            <asp:TableCell ID="TableCell29" runat="server">
            <asp:Label ID="Label15" runat="server" Text="Prop Zip Full"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell30" runat="server">
                <asp:TextBox ID="txtPropZipFull9" runat="server" Width="128px"></asp:TextBox>            
            </asp:TableCell> 
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow15" runat="server">
                                  
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow16" runat="server">
            <asp:TableCell ID="TableCell31" runat="server">
            <asp:Label ID="Label16" runat="server" Text="Prev Agency Id"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell32" runat="server">
                <asp:TextBox ID="txtPrevAgencyId" runat="server" Width="128px"></asp:TextBox>
            
            </asp:TableCell>
            <asp:TableCell ID="TableCell33" runat="server">
            <asp:Label ID="Label17" runat="server" Text="Selected Agency Id"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell34" runat="server">
                <asp:TextBox ID="txtSelectedAgencyId" runat="server" Width="128px"></asp:TextBox>
            
            </asp:TableCell>
        </asp:TableRow>
        
        
        <asp:TableRow ID="TableRow18" runat="server">
            <asp:TableCell ID="TableCell35" runat="server">
            <asp:Label ID="Label18" runat="server" Text="Screen Rout"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell36" runat="server">
                <asp:TextBox ID="txtScreenRout" runat="server" Width="128px"></asp:TextBox>            
            </asp:TableCell>  
            
            <asp:TableCell ID="TableCell37" runat="server">
            <asp:Label ID="Label19" runat="server" Text="Final Dispo Cd"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell38" runat="server">
                <asp:TextBox ID="FinalDispoCd" runat="server" Width="128px"></asp:TextBox>
            
            </asp:TableCell>         
            
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow19" runat="server">
            
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow20" runat="server">
            <asp:TableCell ID="TableCell39" runat="server">
            <asp:Label ID="Label20" runat="server" Text="Trans Number"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell40" runat="server">
                <asp:TextBox ID="txtTransNumber" runat="server" Width="128px"></asp:TextBox>
            
            </asp:TableCell>
            
            <asp:TableCell ID="TableCell41" runat="server">
            <asp:Label ID="Label21" runat="server" Text="Cc Call Key"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell42" runat="server">
                <asp:TextBox ID="txtCcCallKey" runat="server" Width="128px"></asp:TextBox>
            
            </asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow21" runat="server">
            
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow22" runat="server">
            <asp:TableCell ID="TableCell43" runat="server">
            <asp:Label ID="Label22" runat="server" Text="Loan Delinq Status Cd"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell44" runat="server">
                <asp:TextBox ID="txtLoanDelinqStatusCd" runat="server" Width="128px"></asp:TextBox>
            
            </asp:TableCell>
            
            <asp:TableCell ID="TableCell45" runat="server">
            <asp:Label ID="Label23" runat="server" Text="Selected Counselor"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell46" runat="server">
                <asp:TextBox ID="txtSelectedCounselor" runat="server" Width="128px"></asp:TextBox>
            
            </asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow23" runat="server">
            
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow24" runat="server">
            <asp:TableCell ID="TableCell47" runat="server">
            <asp:Label ID="Label24" runat="server" Text="Homeowner Ind"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell48" runat="server">
                <asp:TextBox ID="txtHomeownerInd" runat="server" Width="128px"></asp:TextBox>
            
            </asp:TableCell>
            <asp:TableCell ID="TableCell49" runat="server">
            <asp:Label ID="Label25" runat="server" Text="Power Of Attorney Ind"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell50" runat="server">
                <asp:TextBox ID="txtPowerOfAttorneyInd" runat="server" Width="128px"></asp:TextBox>           
            </asp:TableCell>
        </asp:TableRow>
                
        <asp:TableRow ID="TableRow26" runat="server">
            <asp:TableCell ID="TableCell51" runat="server">
            <asp:Label ID="Label26" runat="server" Text="AuthorizedI nd"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell52" runat="server">
                <asp:TextBox ID="txtAuthorizedInd" runat="server" Width="128px"></asp:TextBox>           
            </asp:TableCell>   
            <asp:TableCell ID="TableCell11" runat="server">
            <asp:Label ID="Label6" runat="server" Text="Final Dispo Cd"></asp:Label>
            </asp:TableCell>
            <asp:TableCell ID="TableCell12" runat="server">
                <asp:TextBox ID="txtFinalDispoCd" runat="server" Width="128px"></asp:TextBox>           
            </asp:TableCell>          
        </asp:TableRow>
                
    </asp:Table>
    <br />
    <br />
        <asp:Button ID="btnSearch" runat="server" Text="Save call log" onclick="btnSearch_Click" />  <asp:Label ID="lblResult" runat="server" Text="New Call Log ID: "></asp:Label>
    <br />
    
    
    <asp:Button ID="btnGenerateTestData" runat="server" Text="Generate test data" 
        onclick="btnGenerateTestData_Click" />
    <asp:Button ID="btnDeleteTestData" runat="server" Text="Delete test data" 
            onclick="btnDeleteTestData_Click" /> 
        <asp:Button ID="btnCheck" runat="server" onclick="btnCheck_Click" 
            Text="Finalize Testing" />
        <br />
    <asp:ListBox ID="lstMessage" runat="server" Height="245px" Width="500px"></asp:ListBox>
    </ContentTemplate>
    </asp:UpdatePanel>
    
<asp:GridView ID="grdvResult" runat="server" >
</asp:GridView>
<br />
<br />
</asp:Content>
