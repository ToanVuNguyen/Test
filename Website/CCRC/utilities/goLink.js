
document.onmouseover = doOver;
document.onmouseout = doOut;

function doOver() {
	eSrc = window.event.srcElement;
	if ("goLink" == eSrc.className) {
		eSrc.style.color = '#0000FF';

		if ("undefined" != typeof(eSrc.id)) {
    
			var targetID = eSrc.id;

			targetID = "ListItem" + targetID.substr(6);

			var targetElement = document.all(targetID);
			
			if (null != targetElement) {
				if ("undefined" == typeof(targetElement.length)) {
					if ("undefined" != typeof(targetElement.style)) {
						targetElement.style.color = '#0000FF';
					}
				} else {
					for(var i = 0; i < targetElement.length; i++) {
						if ("undefined" != typeof(targetElement[i].style)) {
							targetElement[i].style.color = '#0000FF';
						}
					}
				}
			}
    }
		self.status = "";
	}
}

function doOut() {
	eSrc = window.event.srcElement;
	if ("goLink" == eSrc.className) {
		eSrc.style.color = '#000088';

		if ("undefined" != typeof(eSrc.id)) {
    
			var targetID = eSrc.id;

			targetID = "ListItem" + targetID.substr(6);

			var targetElement = document.all(targetID);
			
			if (null != targetElement) {
				if ("undefined" == typeof(targetElement.length)) {
					if ("undefined" != typeof(targetElement.style)) {
						targetElement.style.color = '#000000';
					}
				} else {
					for(var i = 0; i < targetElement.length; i++) {
						if ("undefined" != typeof(targetElement[i].style)) {
							if ("undefined" != typeof(targetElement[i].style)) {
								targetElement[i].style.color = '#000000';
							}
						}
					}
				}
			}
    }
	
	}
}

/*
document.onmouseover = doOver;
document.onmouseout = doOut;

function doOver() {
	eSrc = window.event.srcElement;
	if ("goLink" == eSrc.className) {
		eSrc.style.color = '#0000FF';

		if ("undefined" != typeof(eSrc.id)) {
    
			var targetID = eSrc.id;

			targetID = "ListItem" + targetID.substr(6);

			var targetElement = document.all(targetID);
			
			if (null != targetElement) {
				if ("undefined" == typeof(targetElement.length)) {
					if ("undefined" != typeof(targetElement.style)) {
						targetElement.style.color = '#0000FF';
					}
				} else {
					for (var i = 0; i < targetElement.length; i++)) {
						if ("undefined" != typeof(targetElement.style)) {
							targetElement.style.color = '#0000FF';
						}
					}				
				}
			}
    }
		self.status = "";
	}
}

function doOut() {
	eSrc = window.event.srcElement;
	if ("goLink" == eSrc.className) {
		eSrc.style.color = '#000088';

		if ("undefined" != typeof(eSrc.id)) {
    
			var targetID = eSrc.id;

			targetID = "ListItem" + targetID.substr(6);

			var targetElement = document.all(targetID);
			
			if (null != targetElement) {
				if ("undefined" == typeof(targetElement.length)) {
					if ("undefined" != typeof(targetElement.style)) {
						targetElement.style.color = '#000000';
					}
				} else {
					for (var i = 0; i < targetElement.length; i++)) {
						if ("undefined" != typeof(targetElement.style)) {
							targetElement.style.color = '#000000';
						}
					}
				}
			}

    }
	
	}
}

*/