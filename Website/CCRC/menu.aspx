<%@ Page Language="VB" AutoEventWireup="false" CodeFile="menu.aspx.vb" Inherits="menu" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>CCRC MENU</title>
    <link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
    
</head>
<body>
    <form id="form1" runat="server" >

     <table border = "0" width="100%"   cellspacing="0"  cellpadding="0" id="Menuitem">
            <tr>
                 <td><a href=home.aspx target="Homepage">Home</a></td>
            </tr>
          <%Dim userRoles As String
              
            userRoles = Request.Cookies("CCRCCookie")("profileRoles")
            
              If InStr(1, userRoles, "83") > 0 Or InStr(1, userRoles, "29") > 0 Then%>            
               <tr>
                     <td width="15" colspan="2" style="color:#000088" ><b>Admin</b></td>
               </tr>
            <tr>

                <td>&nbsp;&nbsp;&nbsp;&nbsp;<a href="Admin/entity_list.aspx?entityType=3" target="Homepage" >Agencies</a></td>
            </tr>
            <tr>
                 
                <td>&nbsp;&nbsp;&nbsp;&nbsp;<a href="Admin/entity_list.aspx?entityType=2" target="Homepage">Servicers</a></td>
            </tr>
            <tr>
                
                <td>&nbsp;&nbsp;&nbsp;&nbsp;<a href="Admin/entity_list.aspx?entityType=1" target="Homepage">Sponsors</a></td>
            </tr>
            <tr>
                
                <td>&nbsp;&nbsp;&nbsp;&nbsp;<a href="Admin/program_list.aspx" target="Homepage">Programs</a></td>
            </tr>

            <tr>
                
                <td>&nbsp;&nbsp;&nbsp;&nbsp;<a href="referrals/InvoiceSessions.aspx" target="Homepage">Invoice Sessions </a></td>
            </tr>
            <tr>
                
                <td>&nbsp;&nbsp;&nbsp;&nbsp;<a href="referrals/ServicerPayments.aspx" target="Homepage">Process Servicer Payments </a></td>
            </tr>

            <tr>
                
                <td>&nbsp;&nbsp;&nbsp;&nbsp;<a href="referrals/counselingSummaryAutoEmailGo.aspx" target="Homepage">Send Counseling<br />&nbsp;&nbsp;&nbsp;&nbsp;Summaries Not Yet Sent</a></td>
            </tr>
            <tr>
                <td >&nbsp;&nbsp;&nbsp;&nbsp;<a href="reports/referral_EmailLog.aspx"  target="Homepage">E-mail Summary Report</a></td>
            </tr>
              

            <% End If%>

            <tr>
                <td><a href="ContactUs.htm"  target="Homepage">Contact Us</a></td>
            </tr>

   
            <%  If InStr(1, userRoles, "125") > 0 Or InStr(1, userRoles, "123") > 0 Or InStr(1, userRoles, "83") > 0 Then %>
               <tr>
                    <td><a href="referrals/search.aspx?formMode=ADD" target="Homepage">Add Referral</a></td>
                </tr>

                <tr>
                    <td><a href="referrals/search.aspx?formMode=EDIT" target="Homepage">Edit Referral</a></td>
                </tr>
            <% Elseif InStr(1, userRoles, "84") > 0 Or InStr(1, userRoles, "35") > 0 Or InStr(1, userRoles, "34") > 0%>
                <tr>
                    <td><a href="referrals/search.aspx?formMode=VIEW" target="Homepage">View Referral</a></td>
                </tr>
            <% End If%>
            

            
            
    <%If InStr(1, userRoles, "83") > 0 Or InStr(1, userRoles, "35") > 0  Or InStr(1, userRoles, "33") > 0 Then%>            
            <tr>
                <td ><a href="reports/ccrc_Report_Config.aspx"  target="Homepage">Reports</a></td>
            </tr>
            
    <%End if %>       
            <tr>
                <td ><a href="reports/ccrc_Users_Report.aspx?agencyType=1"  target="Homepage">CCRC Counselor Directory</a></td>
            </tr>
     <%If InStr(1, userRoles, "125") > 0 Or InStr(1, userRoles, "123") > 0  Or InStr(1, userRoles, "83") > 0 Then%>            
            <tr>
                <td ><a href="reports/ccrc_Servicers_Report.aspx"  target="Homepage">Servicer Directory</a></td>
            </tr>
    <%End if %>    
   
    <%If InStr(1, userRoles, "125") > 0 Or InStr(1, userRoles, "83") > 0   Then%>            
            <tr>
                <td ><a href="http://nonprofitreferral.org/"  target="Homepage">Local Resources</a></td>
            </tr>

            <tr>
                <td ><a href="LMServices_HOPE.PDF"  target="Homepage">LifeMatters Counseling</a></td>
            </tr>
            <tr>
                <td ><a href="AtEaseGrantProcedure.doc"   target="Homepage">Military Assistance Program</a></td>
            </tr>
            <tr>
                <td ><a href="NationalUrbanLeague.pdf"  target="Homepage">National Urban League</a></td>
            </tr>
            
            <tr>
                <td style="color:#000088" ><b>NWO/NHS Resources</b></td>
            </tr>
            <%If InStr(1, userRoles, "29") > 0   Then%>
            <tr>
                <td >&nbsp;&nbsp;&nbsp;&nbsp;<a href="http://www.nw5.org/locator"  target="Homepage">NWO Resource Locator</a></td>
            </tr>

            <tr>
                <td >&nbsp;&nbsp;&nbsp;&nbsp;<a href="reports/ccrc_Users_Report.aspx?agencyType=2"  target="Homepage">Counselor Directory</a></td>
            </tr>
            <%End if %>  
<%End if %>  
  
   <%If InStr(1, userRoles, "125") > 0 Or InStr(1, userRoles, "123") > 0 Or InStr(1, userRoles, "83") > 0   Then%>            

          <!--  Commented for Issue Id 34
            <tr>
                <td ><a href="CCRC User Guide.doc"  target="Homepage" >Counselor User Guide</a></td>
            </tr>--> 

 <%End if %>   
  
               
            <tr>
                <td>
                    <a href="ChangePassword.aspx"  target="Homepage">Change Password</a><br />        
                    <a href="Logout.aspx" target="_parent">Logout</a><br />        
                    <a href="CCRC_FAQ.doc" target="Homepage">FAQ</a><br />        
                </td>
            </tr>
        </table>
   
       
    </form>
</body>
</html>
