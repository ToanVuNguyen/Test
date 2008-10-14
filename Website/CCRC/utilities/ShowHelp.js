// This function returns the current base 
// application path using location.pathname
// the actual file name and subdirectory is left out.
// (i.e. "/CCRC/Controls/acontrol.asp" becomes "/CCRC/")

var helpWindow = null;
function ShowHelp(provider, overridefor) {
	
	if (null == helpWindow || helpWindow.closed) {
		helpWindow = window.open(GetHelpURL(),'help','toolbar=no,status=no,location=no,menubar=no,dependent=yes,resizable=yes,scrollbars=yes,width=400,height=500,left=0,top=0');
	} else {
		helpWindow.location.href = GetHelpURL();
		helpWindow.focus();
	}
	
	function GetHelpURL() {
		if ('undefined' == typeof(overridefor)) {
			//if page contains a method to get help URL, use it instead
			if ('undefined' != typeof(HelpContext))
			{
				if ('function' == typeof(HelpContext))
					s = HelpContext();
				else
					s = HelpContext;
			}
			else//otherwise use this pages href to find out what to show.
			{
				s = location.href;
				s = s.substr(s.lastIndexOf("/") + 1, (s.indexOf(".") - 1) - s.lastIndexOf("/"));
			}
		} else {
			s = overridefor;
		}
		
		return getBasePath() + "help.asp?provider=" + provider + "&helpfor=" + escape(s);
	} 
}

function getBasePath() {

	var myPath = location.pathname; // document.URL;
		
	myPath = myPath.toUpperCase();
	
	//Make sure there is a leading "/"
	if (myPath.substr(0,1) != '/')
	{
		myPath = "/" + myPath;
	}

	if (myPath.indexOf('CCRC/') > -1) {
		return myPath.substr(0, myPath.indexOf('CCRC/') + 5);
	} else {
		return "/"
	}
}
