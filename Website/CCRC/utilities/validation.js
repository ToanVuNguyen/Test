// Validation.js - originally written by Steve Berzins July 1999
//
// SUMMARY :
//
// This is a set of JavaScript functions for validating input on an HTML form.
//
// .js files are only downloaded to the client the first time they are
// included in a page, and thereafter only when a newer version is available.
// the browser is supposed to be smart enough to download again only if 
// a newer version exists.
//
// this file uses extended HTML attributes to do input validation.
// required attributes:
// 1. datatype
// 2. errorlbl or errormsg - if errorlbl and errormsg are both used errormsg overrides errorlbl.
// other attributes:
// 1. required - if missing, assumed false
// 2. min - only used with numeric types
// 3. max - only used with numeric types
//
// SAMPLE CODE :
// in your page put a function like this and call it from
// a procedure that submits the form only if it passes validation.
//
// function submitForm() {
//	 if (validate()) {
//	   document.frm.submit();
//	 }
// }
//
// function validate() {
//   var pv = new Validator();
//	 with (document.master) {
//		 pv.addField(firstName);
//		 pv.addField(middleName);
//		 pv.addField(lastName);
//		 pv.addField(socialSecurityID);
//		 pv.addField(phn);
//	 }
//	 return pv.validate(document.all.results);
// }
//
// or in the form tag like this. where document.all.results is a form element
// that has an innerHTML property to display failure results.
// <form id="frm" name="frm" onSubmit="return validate(document.all.results);">
//
// validate can be called three different ways:
// 1. with no parameters - returns an array of failures, will have a length of 0 if passed validation
//	 resultsArray = validate();
// 2. with one parameter - returns true or false and the parameter must have an innerHTML property to display failures.
//	 passed = validate(document.all.results);
// 3. with two parameters - returns true or false, first parameter must have an innerHTML property to display failures
//													second parameter is true/false to override required attributes of input fields.
//	 passed = validate(document.all.results, true/false); true to ignore required attribute, false will validate as usual.
//
// sample text inputs
// <input name="firstName" type="text" onFocus="this.select();" required="true" datatype="anytext" errorlbl="First Name" errormsg="A first name must be entered.">
// <input name="middleName" type="text" onFocus="this.select();" required="false" datatype="anytext" errorlbl="Middle Initial">
// <input name="lastName" type="text" onFocus="this.select();" required="true" datatype="anytext" errormsg="Last Name is required.">
// <input name="socialSecurityID" type="text" maxlength="11" size="12" onFocus="this.select();" onBlur="formatSSN(this);" required="true" datatype="ssn" errorlbl="Social Security" errormsg="Social Security Number is required.">
// <input name="ageYr" type="text" onFocus="this.select();" required="true" datatype="positiveinteger" min="18" max="110" errormsg="The borrower's age must be between 18 and 110." >
// <input name="phn" type="text" maxlength="14" size="14" onFocus="this.select();" onBlur="formatPhone(this);" required="false" datatype="phone" errorlbl="Phone Number">
//
// will also validate an item has been selected from a select box if option values are used.
// and validate that an item is selected in an option group.
//
// at end of this set of functions there are input formatting functions for
// ssn, money, interest, phone number, and more to come...
//
// these functions are intended to be used on the client as an
// extension to validation and should be used in an inputs onBlur
// event to format input appropriately.
// these functions use many of the validation routines in this file
// so they were added to this same file to be sure these functions
// are available.
//
// spb-08/08/99 added sumFloat function to end of file to avoid 
// javascript rounding errors when adding floating point numbers.
// added here because it uses the formatFloat function in this file.
//
// spb-08/10/99 added sumMoney function to end of file to avoid
// javascript rounding errors when adding Money values. 
// added here because it uses the formatMoney and stripMoney function in this file.
//
// spb-08/10/99 added stripMoney, stripSSN, stripPhone etc. to aid client
// side validation/calculation functions.
//
// spb-08/11/99 added validateForm method to do simple form validation
// will not do forms with arrays of controls or option groups, is designed 
// for using on simple forms with no conditionally required fields. On 
// forms that meet this requirement use this method and it will loop through
// all inputs on a form and validate them only if they have the extended
// attribute of 'datatype', if this attribute does not exist the field is
// ignored. validateForm(form, resultsSpan) form = form to validate, 
// resultSpan = span to output validation results to.
//
// spb-08/24/99 added optional parameter on validate to turn off required flag
// added to be used with pages where we may want to turn off the required attribute, 
// to just verify data is not in an invalid format which will mess up business or data
// services.
// to be used when we know we will be coming back to a page so we do not want
// to force the user enter data in all fields, but do need to be sure data that may 
// have been entered is in a valid format.
//
// jwv-9/27/99 added the alphabetic type

function Validator() {
/*
"alphabetic":					a-zA-Z
"text":								a-zA-Z ',.
"numbertext":         numbers and text characters
"anytext":						used for validating something was entered other than whitespace
"integer":						integer
"signedinteger":			(+/-)integer
"positiveinteger":		integer > 0
"negativeinteger":		-integer
"float":							number
"signedfloat":				(+/-)number
"positivefloat":			number > 0
"negativefloat":			-number
"money":							0,000.00
"signedmoney":				(-)0,000.00
"interest":						00.000 > 0
"percent":						00.000
"positivepercent":		00.000 > 0
"negativepercent":		-00.000
"ssn"									9 digit positive number with or without spaces or dashes as delimiters
"email":							someone@somewhere.com
"zip":								5 digit zip code
"plus4":              4 digit zip plus code
"zipplus":						9 digit zip with a space or a - separator
"phone":							10 digit US phone number with or without formatting characters '(nnn) nnn-nnnn'
"optiongroup":				verifies that an option is selected
"selectlist":					verifies that an item with a value attribute is selected
// PH 10/06/1999
"date":						    mm/dd/yyyy, m/d/yyyy date
"creditconnectiontext": 0-9a-zA-Z
"anysearchnumber": 0-9 or * (broker-specified loan ID and SSN pipeline searches)
"mopcode": 1-9 or - or whitespace
*/
	
	this.addField = addField ;
	this.validate = validate ;
	this.validateForm = validateForm ;
	var focusSet = false;
	var ignoreRequired = false; //added to be used with pages where we may want to turn
															//off the required attribute, to just verify data is not
															//in an invalid format which will mess up business or data 
															//services
															//I am using it in pages where I show a list of items with
															//editable fields, but want to allow the user to use another form
															//to add another item to the list which will bring the user
															//back to the list page after adding the item.
															//this way I can save the partial work completed if valid 
															//but not force the user to enter all data, submit the form,
															//and then add the item, but allow them to update a
															//few rows, add an item, then come back to finish.
															//and enforce full validation when the page is submitted.
															 
	var fields = new Array() ;
	
	if (Validator.arguments.length) {
		var argv = Validator.arguments;
		var argc = argv.length;
		
		for (var i = 0; i < argc; i++) {
			addField(argv[i]);
		}
		
	}

	return this;
	
	function addField(field) {
		fields[fields.length] = field ;
	}
	
	function validateForm(form, resultContainer) {
		for (var i = 0; i < form.length; i++) {
			if (isDefined(form[i].datatype) && ("" != form[i].datatype)) {
				addField(form[i]);
			}
		}
		return validate(resultContainer);
	}
	
	function validate() {
		var argv = validate.arguments;
		var argc = argv.length;

		if (argc > 1)
			ignoreRequired = argv[1];
		else
			ignoreRequired = false;
		
		var results = new Array();

		for (i = 0; i < fields.length; i++) {
			var rc = validateField(fields[i]);
	
			if (false == rc) {
				setFocus(fields[i]);
				results[results.length] = getErrorMessage(fields[i]);
			} 
			
		}
		
		if (0 == argc) {
			return results;
		} else {
			if (results.length) {
				argv[0].innerHTML = makeError(results);
				setTimeout("self.scrollBy(0, 500)", 5);
				return false;
			} else {
				argv[0].innerHTML = "";
				return true;
			}
		}
	}

	function setFocus(field) {
		if ("hidden" == field.type) return;

		if (!focusSet) {
			if (isDefined(field.focus)) {
				if (!field.disabled) {
					field.focus();
					focusSet = true;
				}
			}
		}
	}

	function isDefined(s) {
		return "undefined" != typeof(s);
	}

	function validateField(field) {
		if (isDefined(field))
		{
			if (isaN(field.length) && ("optiongroup" == field[0].datatype))
				return isOptionSelected(field);
			else if (isaN(field.length) && isNotDefined(field.datatype))
				return validateFieldArray(field);
			else {
				return defaultValidation(field);
			}
		}
	}
	
	function validateFieldArray(fields) {
		for (var i = 0; i < fields.length; i++) {
			if (!validateField(fields[i])) {
				setFocus(fields[i]);
				return false;
			}	
		}
		return true;
	}
	
	function isInRange(field) {
		if (isNaN(field.min) && isNaN(field.max))
			return true;
		else if (isNaN(field.min) && isaN(field.max))
			return (parseFloat(stripMoney(field.value)) <= parseFloat(field.max));
		else if (isaN(field.min) && isNaN(field.max))
			return (parseFloat(stripMoney(field.value)) >= parseFloat(field.min));
		else
			return ((parseFloat(field.min) <= parseFloat(stripMoney(field.value))) && (parseFloat(stripMoney(field.value)) <= parseFloat(field.max)));
	}
	
	function getRangeMessage(field) {
		if (isNaN(field.min) && isNaN(field.max))
			return "";
		else if (isNaN(field.min) && isaN(field.max))
			return " and must be less than " + field.max;
		else if (isaN(field.min) && isNaN(field.max))
			return " and must be greater than " + field.min;
		else
			return " and must be between " + field.min + " and " + field.max;
	}
	
	function tooLong(field) {
		// JDW - validate fix for importing fields longer than maxlength attribute
		// if the maxlength attribute is numeric and not null
		// SPB - moved here to avoid problems with numeric validation
		if (isaN(field.getAttribute("maxlength")) && null != field.getAttribute("maxlength")) {
			// if the field value length is greater than the maxlength attribute
			if (field.getAttribute("value").length > field.getAttribute("maxlength")) {
				return true; 
			}
		}
		return false;
	}

	function getFieldTooLongMessage(field) {
		if (tooLong(field))
			return " ( max field size = " + field.getAttribute("maxlength") + " )";
		else
			return "";
	}

	function getErrorMessage(field) {
		if (isaN(field.length) && ("optiongroup" == field[0].datatype)) {
			if (validMessage(field[0]))
				return field[0].errormsg;
			else
				return getFieldLabel(field[0]) + " must be selected.";
		} else if (isaN(field.length) && isDefined(field[0].datatype) && ("" != field[0].datatype)) {
			if (validMessage(field[0]))
				return field[0].errormsg + getFieldTooLongMessage(field[0]);
			else
				return defaultFieldMessage(field[0]);
		} else {
			if (validMessage(field))
				return field.errormsg + getFieldTooLongMessage(field);
			else 
				return defaultFieldMessage(field);
		}	
	}
	
	function validMessage(field) {
		if (field.errormsg)
			if (field.errormsg.length > 0)
				return true;
			else
				return false;
		else
			return false;
	}
	
	function getFieldLabel(field) {
		if (field.errorlbl)
			return field.errorlbl;
		else
			return field.name;
	}
	
	function defaultFieldMessage(field) {
		switch (field.datatype) {
			case "alphabetic":
				return getFieldLabel(field) + " must be alphabetic" + getFieldTooLongMessage(field);
			case "text":
				return getFieldLabel(field) + " must be text" + getFieldTooLongMessage(field);
			case "numbertext":
				return getFieldLabel(field) + " must be alphanumeric" + getFieldTooLongMessage(field);
			case "anytext":
				return getFieldLabel(field) + " is required" + getFieldTooLongMessage(field);
			case "searchtext":
				return getFieldLabel(field) + " can contain only text and wildcard characters.";
			case "searchnumber":
				return getFieldLabel(field) + " can contain only numbers 0-9 and wildcard characters.";
			case "integer":
				return getFieldLabel(field) + " must be an unsigned integer value" + getRangeMessage(field) + ".";
			case "signedinteger":
				return getFieldLabel(field) + " must be an integer value" + getRangeMessage(field) + ".";
			case "positiveinteger":
				return getFieldLabel(field) + " must be a positive integer value" + getRangeMessage(field) + ".";
			case "negativeinteger":
				return getFieldLabel(field) + " must be a negative integer value" + getRangeMessage(field) + ".";
			case "float":
				return getFieldLabel(field) + " must be an unsigned number" + getRangeMessage(field) + ".";
			case "signedfloat":
				return getFieldLabel(field) + " must be a number" + getRangeMessage(field) + ".";
			case "positivefloat":
				return getFieldLabel(field) + " must be a positive number" + getRangeMessage(field) + ".";
			case "negativefloat":
				return getFieldLabel(field) + " must be a negative number" + getRangeMessage(field) + ".";
			case "money":
				return getFieldLabel(field) + " must be a valid currency amount" + getRangeMessage(field) + ".";
			case "signedmoney":
				return getFieldLabel(field) + " must be a valid currency amount" + getRangeMessage(field) + ".";
			case "interest":
				return getFieldLabel(field) + " must be a valid interest rate" + getRangeMessage(field) + ".";
			case "percent":
				return getFieldLabel(field) + " must be a number" + getRangeMessage(field) + ".";
			case "positivepercent":
				return getFieldLabel(field) + " must be a positive number" + getRangeMessage(field) + ".";
			case "negativepercent":
				return getFieldLabel(field) + " must be a negative number" + getRangeMessage(field) + ".";
			case "ssn":
				return getFieldLabel(field) + " must be a valid 9 digit Social Security Number.";
			case "email":
				return getFieldLabel(field) + " must be a valid E-Mail address i.e 'yourname@yourdomain.com'.";
			case "zip":
				return getFieldLabel(field) + " must be a valid 5 digit zip code.";
			case "plus4":
				return getFieldLabel(field) + " must be a valid 4 digit zip plus4 code.";
			case "zipplus":
				return getFieldLabel(field) + " must be a valid 9 digit zip code.";
			case "phone":
				return getFieldLabel(field) + " must be a valid US Phone number with area code.";
			case "selectlist":
				return getFieldLabel(field) + " is required.";
			case "date":
				return getFieldLabel(field) + " must be a valid date in the format: mm/dd/yyyy.";
			case "monthyeardate":
				return getFieldLabel(field) + " must be a valid date in the format: mm/yyyy.";
			case "creditconnectiontext":
				return getFieldLabel(field) + " must contain only numbers and letters 0-9, a-z, A-Z.";
			case "image":
				return getFieldLabel(field) + " must be a valid image file type.";
			case "uicolor":
				return getFieldLabel(field) + " must be a valid RGB color.";
			case "anysearchnumber":
				return getFieldLabel(field) + " can contain only numbers 0-9 and wildcard characters.";
			case "mopcode":
				return getFieldLabel(field) + " can contain only numbers 1-9 or - or empty space.";
			default:
				return "unknown data type";
		}
	}
	
	function defaultValidation(field) {
		if (ignoreRequired)
			var emptyOK = "true";
		else
			var emptyOK = ("true" == field.required) ? false : true ;
		
		if (isWhitespace(field.value))
			return emptyOK;
		else {
			switch (field.datatype) {
				case "alphabetic":
					if (tooLong(field)) return false;
					return isAlphabetic(field.value);
				case "text":
					if (tooLong(field)) return false;
					return isText(field.value);
				case "numbertext":
					if (tooLong(field)) return false;
					return isNumberText(field.value);
				case "anytext":
					if (tooLong(field)) return false;
					return isAnyText(field.value);
				case "searchtext":
					return isSearchText(field.value);
				case "searchnumber":
					return isSearchNumber(field.value);
				case "integer":
					return (isInteger(field.value) && isInRange(field));
				case "signedinteger":
					return (isSignedInteger(field.value) && isInRange(field));
				case "positiveinteger":
					return (isPositiveInteger(field.value) && isInRange(field));
				case "negativeinteger":
					return (isNegativeInteger(field.value) && isInRange(field));
				case "float":
					return (isFloat(field.value) && isInRange(field));
				case "signedfloat":
					return (isSignedFloat(field.value) && isInRange(field));
				case "positivefloat":
					return (isPositiveFloat(field.value) && isInRange(field));
				case "negativefloat":
					return (isNegativeFloat(field.value) && isInRange(field));
				case "money":
					return (isMoney(field.value) && isInRange(field));
				case "signedmoney":
					return (isSignedMoney(field.value) && isInRange(field));
				case "interest":
					return (isInterest(field.value) && isInRange(field));
				case "percent":
					return (isPercent(field.value) && isInRange(field));
				case "positivepercent":
					return (isPositivePercent(field.value) && isInRange(field));
				case "negativepercent":
					return (isNegativePercent(field.value) && isInRange(field));
				case "ssn":
					return isSsn(field.value);
				case "email":
					if (tooLong(field)) return false;
					return isEmail(field.value);
				case "zip":
					return isZip5(field.value);
				case "plus4":
					return isPlus4(field.value);
				case "zipplus":
					return isZip9(field.value);
				case "phone":
					return isUSPhone(field.value);
				case "selectlist":
					return isItemSelected(field);
				case "date":
					return isDate(field.value);
				case "monthyeardate":
					return isMonthYearDate(field.value);
				case "creditconnectiontext":
				  return isCreditConnectionText(field.value);
				case "image":
					return isValidImageName(field.value.toLowerCase());
				case "uicolor":
					return isValidColor(field.value);
				case "anysearchnumber":
					return isAnySearchNumber(field.value);
				case "mopcode":
					return isMopCode(field.value);
				default:
					return emptyOK;
			}
		}
	}
}

function makeError(errors) {
	var argv = makeError.arguments;
	var argc = argv.length;
	
	var sTemp = "";
	if (argc > 1)
		sTemp = "<b>" + argv[1] + "</b><br>";
	else
		sTemp = "<b>Data Entry Errors:</b><br>";
	
	for (var i = 0; i < errors.length; i++) {
		sTemp += errors[i] + "<br>" ;
	}

	return sTemp;
}

//outputs an existing array of errors to a element with innerHTML property
//and scrolls window to display error
function showError(resultContainer, errors) {
	var argv = showError.arguments;
	var argc = argv.length;
	
	var sTemp = "";
	if (argc > 2)
		sTemp = "<b>" + argv[2] + "</b><br>";
	else
		sTemp = "<b>Data Entry Errors:</b><br>";
	
	for (var i = 0; i < errors.length; i++) {
		sTemp += errors[i] + "<br>" ;
	}

	resultContainer.innerHTML = sTemp;
	
	setTimeout("self.scrollBy(0, 500)", 5);
}

function isaN(s) {
	return !isNaN(s);
}

function isDefined(s) {
	return "undefined" != typeof(s);
}

function isNotDefined(s) {
	return "undefined" == typeof(s);
}

function isMoney(s) {
	s = stripCharsInBag(s, ",")
	return isFloat(s) && expressionTest(/^\d*\.?\d{0,2}$/, s);
}

function isSignedMoney(s) {
	var m = stripMoney(s);
	return (isSignedFloat(m) || isMoney(m));
}

function isInterest(s) {
	return (isPositivePercent(s) && parseFloat(s) < 100);
}

function isPercent(s) {
	return expressionTest(/^\d{0,2}\.?\d{0,3}$/, s);
}

function isPositivePercent(s) {
	return (isPercent(s) && parseFloat(s) > 0);
}

function isNegativePercent(s) {
	return (isPercent(s) && parseFloat(s) < 0);
}

function isFloat(s) {
	return expressionTest(/^((\d+(\.\d*)?)|((\d*\.)?\d+))$/, s);
}

function isSignedFloat(s) {
	return expressionTest(/^(((\+|-)?\d+(\.\d*)?)|((\+|-)?(\d*\.)?\d+))$/, s);
}

function isPositiveFloat(s) {
	return (isFloat(s) && parseFloat(s) > 0); 
}

function isNegativeFloat(s) {
	return (isSignedFloat(s) && parseFloat(s) < 0); 
}

function isAlphabetic(s) {
	return expressionTest(/^[a-zA-Z]+$/, s);
}
function isText(s) {
	return expressionTest(/^[ \-,'\.a-zA-Z]+$/, s);
}

function isSearchText(s) {
	if (1 == s.length)
		return expressionTest(/^[a-zA-Z0-9]+/, s);
	else
		return true; //expressionTest(/^[a-zA-Z0-9.\!\@\#\$]+/, s);
}

function isSearchNumber(s) {
	return expressionTest(/^\d[0-9\*\?]*$/, s);
}

function isAnySearchNumber(s) {
	return expressionTest(/^\d[0-9\*\?]*$/, s);
}

function isAnyText(s) {
	if (expressionTest(/\S/, s))
		return !expressionTest(/"/, s);
	else
		return false;
}

function isMopCode(s) {
	return expressionTest(/^[0-9\-]+$/, s);
}

function isNumberText(s) {
	return expressionTest(/^[# ,'\.a-zA-Z0-9\s]+$/, s);
}

function isCreditConnectionText(s) {
	return expressionTest(/^[a-zA-Z0-9\-]+$/, s);
}

function isInteger(s) {
	return expressionTest(/^\d+$/, s);
}

function isSignedInteger(s) {
	return expressionTest(/^(\+|-)?\d+$/, s);
}

function isPositiveInteger(s) {
	return (expressionTest(/^\d+$/, s) && parseInt(s) > 0);
}

function isNegativeInteger(s) {
	return (expressionTest(/^-?\d+$/, s) && parseInt(s) < 0);
}

function isDate(s) {

	if (expressionTest(/^\d{1,2}\/\d{1,2}\/\d{4}$/ , s)) {
		d = s.split("/");
		return validateDate(d[0], d[1], d[2]);
	}
	return false; 
	
	function validateDate(month, day, year) {
		
		if ((month < 1) || (month > 12))
			return false;

		if (0 == day)
			return false;
		else if (day < 29)
			return true;
		else {
				
			var bestMonths = new Array(1, 3, 5, 7, 8, 10, 12);
			for(var i = 0; i < bestMonths.length; i++) {
				if (month == bestMonths[i])
					return true;
			}
			
			var nextbestMonths = new Array(4, 6, 9, 11);
			for(i = 0; i < nextbestMonths.length; i++) {
				if (month == nextbestMonths[i]) {
					if (day < 31)
						return true;
					else
						return false;
				}
			}
			
			//february
			if (day > 29)
				return false;
			else {
				return isLeapYear(year);
			}
		}
		
		function isLeapYear(year) {
			if (0 != (year % 4))
				return false;
			else if (0 == (year % 100))
				return (0 == (year % 400));
			else
				return true;
		}
	}
}

function isMonthYearDate(s) {

	if (expressionTest(/^\d{1,2}\/\d{4}$/ , s)) {
		d = s.split("/");
		return validateDate(d[0], d[1]);
	}
	return false; 
	
	function validateDate(month, year) {
		
		return ((month > 0) && (month < 13));
		
	}

}

function isSsn(s) {
	s = stripCharsInBag(s, " -")
	return expressionTest(/^\d{9}$/, s);
}

function isEmail(s) {
	return expressionTest(/^.+\@.+\..+$/, s);
}

function isZip5(s) {
	return expressionTest(/^\d{5}$/, s);
}

function isZip9(s) {
	return expressionTest(/^\d{5}( |-)?\d{4}$/, s);
}

function isPlus4(s) {
	return expressionTest(/^\d{4}$/, s);
}

function isUSPhone(s) {
	sTemp = stripCharsInBag(s, "() -");
	return ((isInteger(sTemp)) && (10 == sTemp.length));
}

function isWhitespace(s) {
  return (isEmpty(s) || expressionTest(/^\s+$/, s));
}

function isEmpty(s) {
	return ((null == s) || (0 == s.length));
}

function isValidImageName(s) {
	return expressionTest(/.gif$/, s);
}

function isValidColor(s) {
	return expressionTest(/^[A-Fa-f0-9]{6,6}$/, s);
}

function expressionTest(re, s) {
	return re.test(s);
}

function isOptionSelected(field) {
	for (var i = 0; i < field.length; i++) {
		if (field[i].checked) return true;
	}	
	return false;
}

function isItemSelected(field) {
	if (field.length)
		return ((null != field.value) && ("" != field.value));
	else
		return false;
}

function isValidSubjectPropertyState(s) {
	return ("PR" != s.value);
}

// Removes all characters which do NOT appear in string bag 
// from string s.
function stripCharsNotInBag(s, bag){ 
  var returnString = "";
	
  // Search through string's characters one by one.
  // If character is in bag, append to returnString.
  for (var i = 0; i < s.length; i++) {   
    // Check that current character isn't whitespace.
    var c = s.charAt(i);
    if (bag.indexOf(c) != -1)
			returnString += c;
  }
	
  return returnString;
}

// Removes all characters which appear in string bag from string s.
function stripCharsInBag(s, bag) {
  var returnString = "";
	
  // Search through string's characters one by one.
  // If character is not in bag, append to returnString.
  for (var i = 0; i < s.length; i++) {   
		// Check that current character isn't whitespace.
    var c = s.charAt(i);
    if (bag.indexOf(c) == -1)
			returnString += c;
  }
	
  return returnString;
}

// Removes all whitespace characters from s.
// Global variable whitespace (see above)
// defines which characters are considered whitespace.
function stripWhitespace(s) {
	return stripCharsInBag (s, " \t\n\r");
}

// formatting functions. most use validation in this file already
// so they are here to be sure validation is available.

function stripSSN(s) {
	return stripCharsInBag(s, "- ");
}
function formatSSN(inp) {
	var s = ("text" == inp.type ? inp.value : inp);
	if (isSsn(s)) {
		s = stripSSN(s);
		if ("text" == inp.type)
		  inp.value = s.substr(0,3) + "-" + s.substr(3,2) + "-" + s.substr(5,4);
		else
		  return s.substr(0,3) + "-" + s.substr(3,2) + "-" + s.substr(5,4);
	} else {
		if ("text" != inp.type)
		  return "";
	}
}

function stripMoney(s) {
	return stripCharsInBag(s, ",");
}
function formatMoney(inp) {
// PH 09/17/99 - added optional parameter (allowSigned)

	var argv = formatMoney.arguments;
	argc = argv.length;
	
	var tmp = ("text" == inp.type ? inp.value : inp);
	tmp = tmp.toString();
	
	var s = stripMoney(tmp); //strip out commas in case they are wrong
	
	var allowSigned = false;
	if (1 < argc)
		allowSigned = argv[1];
	else
		allowSigned = ("signedmoney" == inp.datatype);
		
	if (!((allowSigned && isSignedFloat(s)) || isFloat(s))) {
		if (argc > 2) {
			if ("text" == inp.type)
				inp.value = argv[2];
			else
				return argv[2];
		}
		return "";
	}
	
	if (allowSigned) {
		// get the sign, if added
		if (s.length > 0) {
			var mySign = s.charAt(0);
			if (!("+" == mySign || "-" == mySign))
				mySign = "";
			else
				s = s.slice(1); // remove the sign
		}
	}

	s = formatFloat(s, 2, true);
	
	re = /\d{4}\.|\d{4}\,/; //look for four digits followed by a decimal or comma
	
	while (null != re.exec(s)) {
		
		iPos = re.exec(s).index + 1;
		
		lft = s.substr(0, iPos);
		rgt = s.substr(iPos);
		
		s = lft + "," + rgt;
			
	}

	if (allowSigned) {
		// add the sign back
		s = mySign + s;
	}	

	if ("text" == inp.type)
		inp.value = s;
	else
		return s;
		
}

function formatInterest(inp) {
	var argv = formatInterest.arguments;
	var argc = argv.length;
	
	var tmp = ("text" == inp.type ? inp.value : inp);

	var s = tmp.toString();	
	
	if (s.length) {
		while ('0' == s.charAt(0)) {
			s = s.substr(1)
			if (0 == s.length) break;
		}
	}

	if (!isInterest(s)) return;

	var prec;
	if (argc > 1) {
		prec = argv[1];
	}
	else {
		prec = 3; // default to 3
	}

	if ("text" == inp.type)
		inp.value = formatFloat(s, prec, true);
	else
		return formatFloat(s, prec, true);

}

function formatFloat(inp, precision) {
	var argv = formatFloat.arguments;
	var argc = argv.length;
	
	var tmp = ("text" == inp.type ? inp.value : inp);

	var s = tmp.toString();

	if (!isFloat(s)) return "";

	if (0 == precision) {
		var i = s.indexOf(".", 0);
		if (-1 == i)
			return s;
		else
			return s.substr(i);
	} 

	s = roundNumber(s, precision);
		
	if (-1 == s.indexOf(".", 0)) //if no decimal point add it.
		s += '.';

	if (0 == s.indexOf(".", 0)) { //if no zero in front add it if third parameter exists and is true.
		if (argc > 2) {
			if (argv[2]) 
				s = "0" + s;
		}
	}

	precision = precision + 1

	while ((s.length - precision) < s.indexOf(".", 0)) { //while not at least two places after decimal point 
		s += "0";																					 //add zeros after decimal point
	}

	if ("text" == inp.type)
		inp.value = s.substring(0, s.indexOf(".", 0) + precision);
	else
		return s.substring(0, s.indexOf(".", 0) + precision);
}

function stripPhone(s) {
	return stripCharsInBag(s, "()- ");
}
function formatPhone(inp) {
	
  var	s = ("text" == inp.type ? inp.value : inp);
	
	if (isUSPhone(s)) {
		
		s = stripPhone(s);
		
		area = s.substr(0, 3);
		pre = s.substr(3, 3);
		post = s.substr(6);
		
		if ("text" == inp.type)
  		inp.value = "(" + area + ") "	+ pre + "-" + post;
  	else
  	  return "(" + area + ") "	+ pre + "-" + post;
	}
}

function sumMoney(precision) {

	var argv = sumMoney.arguments;
	var argc = argv.length;
	
	var factor = Math.pow(10, precision);
	
	var sumTemp = 0;
	
	var vTemp;

	for (var i = 1; i < argc; i++) {
		vTemp = stripMoney(argv[i]);
		if (isaN(vTemp))
			sumTemp += vTemp * factor;
	}	
	
	return formatMoney(sumTemp / factor);
}

function sumFloat(precision) {

	var argv = sumFloat.arguments;
	var argc = argv.length;
	
	var factor = Math.pow(10, precision);
		
	var sumTemp = 0;
	
	for (var i = 1; i < argc; i++) {
		if (isaN(argv[i]))
			sumTemp += argv[i] * factor;
	}	
	
	return formatFloat(sumTemp / factor, precision);
}

function toMonths(years, months) {
	return parseInt(years * 12) + parseInt(months);
}

function roundNumber(val, precision) {
	var factor = Math.pow(10, precision);
	var tmp = val * factor;
	tmp = Math.round(tmp);
	return new String(tmp/factor);
}