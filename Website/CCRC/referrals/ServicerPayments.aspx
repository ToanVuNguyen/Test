<%@ Page Language="VB" aspcompat=true %>
<!--#INCLUDE virtual='/ccrc/utilities/ccrc.inc'-->
<!--#include virtual="/ccrc/utilities/ice/GetUserID.inc"-->
<!--#INCLUDE virtual='/ccrc/utilities/global.inc'-->
<%

	On Error Resume Next
	
	Dim errLog
	errLog = Request.QueryString("errLog")
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
	<head>
		<title>Referral</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
		<script language="javascript">
			function submitForm() {
				var frm = document.myform;
				document.myform.submit();
				return true;
			}
			
			function cancelForm() {
				location.replace("/home.aspx");
			}
			
			function ParseServicePayments()
			{
                var strData;
                strData = document.getElementById("stringData").value;
                var len = strData.length-1
                 if (strData.charAt(len) == '^')
                    strData = strData.substr(0,len)
                //alert(strData);
                var Record = [];
                Record = strData.split("^");
                
                for(var l=0;l<Record.length;l++)
                {
                    var Columns = [];
                    Columns = Record[l].split("~");
                    
                    if (! isDate(Columns[1], l+1))
                        return false;
                 }
                
                submitForm()
 			}
 			
 			// For Date Validatoin - Begin
 			var dtCh= "/";
            var minYear=1900;
            var maxYear=2100;

            function isInteger(s){
	            var i;
                for (i = 0; i < s.length; i++){   
                    // Check that current character is number.
                    var c = s.charAt(i);
                    if (((c < "0") || (c > "9"))) return false;
                }
                // All characters are numbers.
                return true;
            }

            function stripCharsInBag(s, bag){
	            var i;
                var returnString = "";
                // Search through string's characters one by one.
                // If character is not in bag, append to returnString.
                for (i = 0; i < s.length; i++){   
                    var c = s.charAt(i);
                    if (bag.indexOf(c) == -1) returnString += c;
                }
                return returnString;
            }

            function daysInFebruary (year){
	            // February has 29 days in any year evenly divisible by four,
                // EXCEPT for centurial years which are not also divisible by 400.
                return (((year % 4 == 0) && ( (!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28 );
            }
            function DaysArray(n) {
	            for (var i = 1; i <= n; i++) {
		            this[i] = 31
		            if (i==4 || i==6 || i==9 || i==11) {this[i] = 30}
		            if (i==2) {this[i] = 29}
               } 
               return this
            }

            function isDate(dtStr, row){
	            var daysInMonth = DaysArray(12)
	            var pos1=dtStr.indexOf(dtCh)
	            var pos2=dtStr.indexOf(dtCh,pos1+1)
	            var strMonth=dtStr.substring(0,pos1)
	            var strDay=dtStr.substring(pos1+1,pos2)
	            var strYear=dtStr.substring(pos2+1)
	            strYr=strYear
	            if (strDay.charAt(0)=="0" && strDay.length>1) strDay=strDay.substring(1)
	            if (strMonth.charAt(0)=="0" && strMonth.length>1) strMonth=strMonth.substring(1)
	            for (var i = 1; i <= 3; i++) {
		            if (strYr.charAt(0)=="0" && strYr.length>1) strYr=strYr.substring(1)
	            }
	            month=parseInt(strMonth)
	            day=parseInt(strDay)
	            year=parseInt(strYr)
	            if (pos1==-1 || pos2==-1){
		            alert("The date format should be : mm/dd/yyyy for row " + row)
		            return false
	            }
	            if (strMonth.length<1 || month<1 || month>12){
		            alert("Please enter a valid month for row " + row)
		            return false
	            }
	            if (strDay.length<1 || day<1 || day>31 || (month==2 && day>daysInFebruary(year)) || day > daysInMonth[month]){
		            alert("Please enter a valid day for row " + row)
		            return false
	            }
	            if (strYear.length != 4 || year==0 || year<minYear || year>maxYear){
		            alert("Please enter a valid 4 digit year between "+minYear+" and "+maxYear + "for row " + row)
		            return false
	            }
	            if (dtStr.indexOf(dtCh,pos2+1)!=-1 || isInteger(stripCharsInBag(dtStr, dtCh))==false){
		            alert("Please enter a valid date for row " + row)
		            return false
	            }
            return true
            }

            function ValidateForm(){
	            var dt=document.frmSample.txtDate
	            if (isDate(dt.value)==false){
		            dt.focus()
		            return false
	            }
                return true
             }
 			// Date Validation - End
										
		</script>
	</head>
	<body>
		<FORM name="myform" id="myform" method="post" action="ServicerPayments_process.aspx">
			<table border="0" class="pageBody" ID="Table2" width="600">
				<tr>
					<td><font class="label" title="">Data string:</font></td>
					<td>
						<textarea id="stringData" cols="66" rows="50" NAME="stringData"><%=errLog%></textarea>
					</td>
				</tr>
			</table>
			<table ID="Table1">
				<tr>
					<td>
						<input name="save" id="save" value="Save" class="btn" type="button" accesskey="s" onclick="ParseServicePayments();"
							title="Save the changed information and continue">
					</td>
					<td>
						<input name="cancel" id="cancel" value="Cancel" class="btn" type="button" accesskey="s"
							onclick="cancelForm();" title="Cancel all changes">
					</td>
				</tr>
			</table>
		</FORM>
	</body>
</html>
<%

%>
