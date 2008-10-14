	' calculate this month's start & end date
	ThisMonthStart = Month(Date()) & "/01/" & Year(Date())
	if isdate(Month(ThisMonthStart) & "/31/" & Year(ThisMonthStart)) then
	ThisMonthEnd = Month(ThisMonthStart) & "/31/" & Year(ThisMonthStart)
	else
	if isdate(Month(ThisMonthStart) & "/30/" & Year(ThisMonthStart)) then
		ThisMonthEnd = Month(ThisMonthStart) & "/30/" & Year(ThisMonthStart)
	else
		if isdate(Month(ThisMonthStart) & "/29/" & Year(ThisMonthStart)) then
		ThisMonthEnd = Month(ThisMonthStart) & "/29/" & Year(ThisMonthStart)
		else
		ThisMonthEnd = Month(ThisMonthStart) & "/28/" & Year(ThisMonthStart)
		end if
	end if
	end if
	' calculate last month's start & end date
	if Month(ThisMonthStart) = 1 then
	LastMonthStart = "12/01/" & (Year(ThisMonthStart) - 1)
	else
	LastMonthStart = (Month(ThisMonthStart) - 1) & "/01/" & Year(ThisMonthStart)
	end if
	if isdate(Month(LastMonthStart) & "/31/" & Year(LastMonthStart)) then
	LastMonthEnd = Month(LastMonthStart) & "/31/" & Year(LastMonthStart)
	else  
	if isdate(Month(LastMonthStart) & "/30/" & Year(LastMonthStart)) then
		LastMonthEnd = Month(LastMonthStart) & "/30/" & Year(LastMonthStart)
	else
		if isdate(Month(LastMonthStart) & "/29/" & Year(LastMonthStart)) then
		LastMonthEnd = Month(LastMonthStart) & "/29/" & Year(LastMonthStart)
		else
		LastMonthEnd = Month(LastMonthStart) & "/28/" & Year(LastMonthStart)
		end if
	end if
	end if
	Yesterday = DateAdd("d", -1, Date())
