
var initialValues = "";

function buildValues(frm) {

	var arTemp = new Array();

	for (var i = 0; i < frm.length; i++) {

		if ('undefined' != typeof(frm[i].detectChanges)) {
			if ('hidden' == frm[i].type) {
				arTemp[arTemp.length] = frm[i].value;
			} else if ('none' == frm[i].style.display || 'hidden' == frm[i].style.visibility) {
				//do nothing - ignore visible inputs that are hidden
			} else if ('checkbox' == frm[i].type) {
				arTemp[arTemp.length] = frm[i].checked ? 'Y' : 'N';
			} else if ('radio' == frm[i].type) {
				if (frm[i].checked) {
					arTemp[arTemp.length] = frm[i].value;
				}
			} else if ('select-multiple' == frm[i].type) {
				arTemp[arTemp.length] = getSelectValues(frm[i]);
			} else {
				arTemp[arTemp.length] = frm[i].value;
			}
		}
	}
	return arTemp.join("|");

	function getSelectValues(field) {

		var sRet = "";
			
		for(var i = 0; i < field.length; i++) {
			if (field[i].selected) {

				if ("" != sRet) { 
					sRet += ","; 
				}
				  
				sRet += field[i].value;
				    
			}
		}

		return sRet;
	}
	
}

function dataChanged(frm) {
	return buildValues(frm) != initialValues;
}

//made for list pages to aid in determining if changes were made 
//not for use with pages with checkboxes, radio buttons or multiple select lists
function setInitialValues(frm) {
	for (var i = 0; i < frm.length; i++) {
		if ('undefined' != typeof(frm[i].detectChanges)) {
			frm[i].initialValue = frm[i].value;
		}
	}
	return;
}

function listDataChanged(frm) {
	for (var i = 0; i < frm.length; i++) {
		if ('undefined' != typeof(frm[i].detectChanges)) {
			if (frm[i].initialValue != frm[i].value) 
				return true;
		}
	}
	return false;
}
