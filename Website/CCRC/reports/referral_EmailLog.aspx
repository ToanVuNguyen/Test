<%@ Page Language="VB" AutoEventWireup="false" CodeFile="referral_EmailLog.aspx.vb" Inherits="referrals_referral_EmailLog" %>
 <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>CCRC - Email Log</title>
    <link rel="stylesheet" type="text/css" href="../CSS/Main_W.CSS">
    <SCRIPT LANGUAGE="JavaScript" SRC="/ccrc/utilities/calendar.js"></SCRIPT>
    <script language="javascript">
        function SetDates(StartDate, EndDate)
        {
          document.all.master.START_DATE.value = StartDate;
          document.all.master.END_DATE.value = EndDate;
        }
        
        function SetValues()
        {
            document.all.master.hdnStartDate.value = document.all.master.START_DATE.value;
            document.all.master.hdnEndDate.value =  document.all.master.END_DATE.value;
        }
    </script>
</head>
<body>
    <form id= "master"  runat="server">
        <table border="0" class="pageBody" ID="Table2" width="600" v>		
  
<%
    Dim ThisMonthStart
    Dim ThisMonthEnd
    Dim LastMonthStart
    Dim LastMonthEnd
    Dim Yesterday
    Dim Today
    Dim startDate
    Dim endDate
    
    Dim tMonth = Month(Now())
    Dim tDate = Year(Now())
    
    EndDate = tMonth & "/1/" & tDate
    EndDate = DateAdd("m", +1, EndDate)
    EndDate = DateAdd("d", -1, EndDate)
    StartDate = Month(EndDate) & "/1/" & Year(EndDate)
    Response.Write("E-mail Summary Report ")
    Response.Write("<BR>")
    Response.Write("<BR>")
    Response.Write("      Start : ")
    Response.Write("<input NAME='START_DATE' Value= " & startDate & " SIZE='10' MAXLENGTH='10' >")
    Response.Write("      <A HREF=""javascript:doNothing()"" onClick=""setDateField(document.all.master.START_DATE); top.newWin = window.open('/ccrc/utilities/calendar.html', 'cal', 'dependent=yes, width=210, height=230, screenX=200, screenY=300, titlebar=yes')"" TITLE=""Click here to open a calendar selection window""><IMG SRC=""/ccrc/images/Calendar.gif"" BORDER=""0""></A>")
    Response.Write("      End : ")
    Response.Write("<input NAME='END_DATE' Value= " & endDate & " SIZE='10' MAXLENGTH='10' >")
    Response.Write("      <A HREF=""javascript:doNothing()"" onClick=""setDateField(document.all.master.END_DATE); top.newWin = window.open('/ccrc/utilities/calendar.html', 'cal', 'dependent=yes, width=210, height=230, screenX=200, screenY=300, titlebar=yes')"" TITLE=""Click here to open a calendar selection window""><IMG SRC=""/ccrc/images/Calendar.gif"" BORDER=""0""></A>")
    ' calculate this month's start & end date
    ThisMonthStart = Month(Now()) & "/01/" & Year(Now())
    If IsDate(Month(ThisMonthStart) & "/31/" & Year(ThisMonthStart)) Then
        ThisMonthEnd = Month(ThisMonthStart) & "/31/" & Year(ThisMonthStart)
    Else
        If IsDate(Month(ThisMonthStart) & "/30/" & Year(ThisMonthStart)) Then
            ThisMonthEnd = Month(ThisMonthStart) & "/30/" & Year(ThisMonthStart)
        Else
            If IsDate(Month(ThisMonthStart) & "/29/" & Year(ThisMonthStart)) Then
                ThisMonthEnd = Month(ThisMonthStart) & "/29/" & Year(ThisMonthStart)
            Else
                ThisMonthEnd = Month(ThisMonthStart) & "/28/" & Year(ThisMonthStart)
            End If
        End If
    End If
    ' calculate last month's start & end date
    If Month(ThisMonthStart) = 1 Then
        LastMonthStart = "12/01/" & (Year(ThisMonthStart) - 1)
    Else
        LastMonthStart = (Month(ThisMonthStart) - 1) & "/01/" & Year(ThisMonthStart)
    End If
    If IsDate(Month(LastMonthStart) & "/31/" & Year(LastMonthStart)) Then
        LastMonthEnd = Month(LastMonthStart) & "/31/" & Year(LastMonthStart)
    Else
        If IsDate(Month(LastMonthStart) & "/30/" & Year(LastMonthStart)) Then
            LastMonthEnd = Month(LastMonthStart) & "/30/" & Year(LastMonthStart)
        Else
            If IsDate(Month(LastMonthStart) & "/29/" & Year(LastMonthStart)) Then
                LastMonthEnd = Month(LastMonthStart) & "/29/" & Year(LastMonthStart)
            Else
                LastMonthEnd = Month(LastMonthStart) & "/28/" & Year(LastMonthStart)
            End If
        End If
    End If
    Yesterday = DateAdd("d", -1, Now()).ToString("d")
    Today = Now().ToString("d")
	
    Response.Write("&nbsp;<a class=""Mini"" href=""javascript:SetDates('" & LastMonthStart & "', '" & LastMonthEnd & "');"">Last Month</a>")
    Response.Write("&nbsp;<a class=""Mini"" href=""javascript:SetDates('" & ThisMonthStart & "', '" & ThisMonthEnd & "');"">This Month</a>")
    Response.Write("&nbsp;<a class=""Mini"" href=""javascript:SetDates('" & Today & "', '" & Today & "');"">Today</a>")
    Response.Write("<BR>")
 %>

            <tr><td colspan="2">&nbsp</td></tr>
            <tr valign ="top"><td>Servicer: </td><td><asp:ListBox  ID="lstbServicers" Height="200px" Width="250px" SelectionMode="Multiple" runat="server"></asp:ListBox></td></tr>
            <tr><td colspan="2">&nbsp</td></tr>
            <tr><td colspan="2"><asp:Button ID="BtnReport" runat="server" Text="Run Report" OnClientClick ="javascript:SetValues()" /></td></tr>
        </table>
        <input type="hidden" id="hdnStartDate" runat="server" name="hdnStartDate" value="" />
        <input type="hidden" id="hdnEndDate" runat="server" name="hdnEndDate" value="" />
        
    </form>
</body>
</html>
