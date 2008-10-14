<script language="VBScript" runat="server" Explicit="True"  Buffer = "true" ExpiresAbsolute="DateTime.Now.AddDays(-365d)">
 ' The following functions are used to reformat dates and times

Function ReformatDatetime(parmDate, dateFormat, timeFormat)
	ReformatDatetime = ReformatDate(parmDate, dateFormat) & " " & ReformatTime(parmDate, timeFormat)
End Function
   
Function ReformatDate(parmDate, parmFormat)
  Dim myDate,pMonth,pDay,pYear,resDate

  if parmDate iS Nothing or len(parmDate) < 1 then
    ReformatDate = ""
  else
    myDate = cDate(parmDate)
    pMonth = Month(myDate)
    pDay = Day(myDate)
    pYear = Year(myDate)
	resDate = ""
    parmFormat = uCase(parmFormat)
	if parmFormat = "MM-DD-YY" then
	  if pMonth < 10 then resDate = "0"
      resDate = resDate & cStr(pMonth) & "-"
	  if pDay < 10 then resDate = resDate & "0"
      resDate = resDate & cStr(pDay) & "-"
      resDate = resDate & right(cStr(pYear), 2)
	end if
	if parmFormat = "MM/DD/YY" then
	  if pMonth < 10 then resDate = "0"
      resDate = resDate & cStr(pMonth) & "/"
	  if pDay < 10 then resDate = resDate & "0"
      resDate = resDate & cStr(pDay) & "/"
      resDate = resDate & right(cStr(pYear), 2)
	end if
	if parmFormat = "MM/DD/YYYY" then
	  if pMonth < 10 then resDate = "0"
      resDate = resDate & cStr(pMonth) & "/"
	  if pDay < 10 then resDate = resDate & "0"
      resDate = resDate & cStr(pDay) & "/"
      resDate = resDate & right(cStr(pYear), 4)
	end if
	if parmFormat = "M/D/YY" then
      resDate = resDate & cStr(pMonth) & "/"
      resDate = resDate & cStr(pDay) & "/"
      resDate = resDate & right(cStr(pYear), 2)
	end if
	if parmFormat = "MMM YY" then
      if pMonth = 1  then resDate = "Jan "
      if pMonth = 2  then resDate = "Feb "
      if pMonth = 3  then resDate = "Mar "
      if pMonth = 4  then resDate = "Apr "
      if pMonth = 5  then resDate = "May "
      if pMonth = 6  then resDate = "Jun "
      if pMonth = 7  then resDate = "Jul "
      if pMonth = 8  then resDate = "Aug "
      if pMonth = 9  then resDate = "Sep "
      if pMonth = 10 then resDate = "Oct "
      if pMonth = 11 then resDate = "Nov "
      if pMonth = 12 then resDate = "Dec "
      resDate = resDate & right(cStr(pYear), 2)
    end if
	if left(parmFormat, 5) = "MMMM " then
      if pMonth = 1  then resDate = "January "
      if pMonth = 2  then resDate = "February "
      if pMonth = 3  then resDate = "March "
      if pMonth = 4  then resDate = "April "
      if pMonth = 5  then resDate = "May "
      if pMonth = 6  then resDate = "June "
      if pMonth = 7  then resDate = "July "
      if pMonth = 8  then resDate = "August "
      if pMonth = 9  then resDate = "September "
      if pMonth = 10 then resDate = "October "
      if pMonth = 11 then resDate = "November "
      if pMonth = 12 then resDate = "December "
      if parmFormat = "MMMM YY" then
        resDate = resDate & right(cStr(pYear), 2)
      end if
	  if parmFormat = "MMMM D, YY" then
        resDate = resDate & cStr(pDay) & ", "
        resDate = resDate & right(cStr(pYear), 2)
      end if
	  if parmFormat = "MMMM D, YYYY" then
        resDate = resDate & cStr(pDay) & ", "
        resDate = resDate & cStr(pYear)
      end if
    end if
	if resDate = "" then resDate = ParmDate
    ReformatDate = resDate
  end if
end function

Function ReformatTime(parmTime, parmFormat)
  Dim resTime,AMPM,myTime,pHour,pMinute,pSeconds
  if parmTime Is nothing or len(parmTime) < 1 then
    ReformatTime = ""
  else
    myTime = cDate(parmTime)
    pHour = Hour(myTime)
    pMinute = Minute(myTime)
    pSeconds = Second(myTime)
    parmFormat = uCase(parmFormat)

	If parmFormat = "24:MM" then
		If pHour = 0 Then
			resTime = "00" & ":"
		Else
			resTime = cStr(pHour) & ":"
		End If
		If pMinute = 0 Then
			resTime = resTime & "00"
		Else
			If pMinute < 10 Then
				resTime = resTime & "0" & cstr(pMinute)
			Else
				resTime = resTime & cstr(pMinute)
			End If
		End If
	End If

	if parmFormat = "HH:MM AM" then
	  if pHour >= 12 then 
		AMPM = " PM" 
		If pHour = 12 Then
			resTime = "12" & ":"
		Else
			resTime = resTime & cStr(cInt(pHour) - 12) & ":"
		End If
	  else 
		AMPM = " AM"
		If pHour = 0 Then
		  	resTime = "12" & ":"
		else
		  	resTime = resTime & cStr(pHour) & ":"
		End If
	  end if
	  if pMinute < 10 then resTime = resTime & "0"
	  resTime = resTime & cStr(pMinute) & AMPM
	end if

	if parmFormat = "HH:MM" then
	  if pHour < 10 then resTime = "0"
      resTime = resTime & cStr(pHour) & ":"
	  if pMinute < 10 then resTime = resTime & "0"
      resTime = resTime & cStr(pMinute)
	end if
	if parmFormat = "HH:MM:SS" then
	  if pHour < 10 then resTime = "0"
      resTime = resTime & cStr(pHour) & ":"
	  if pMinute < 10 then resTime = resTime & "0"
      resTime = resTime & cStr(pMinute) & ":"
	  if pSeconds < 10 then resTime = resTime & "0"
      resTime = resTime & cStr(pSeconds)
	end if
	if resTime  = "" then resTime = ParmTime
    ReformatTime = resTime
  end if
end function
</Script>
