function addEventListenerEx(el, sEvent, func, bCapture) {
	var newFunc = function(e) {
		e = (e) ? e : window.event;
		if (!e.target) e.target = e.srcElement;
		if (!e.stopPropagation) e.stopPropagation = function() {this.cancelBubble = true;};
		b = func(e);
		if (b === false && e.preventDefault) {
			e.preventDefault();
		}
		return b;
	};
	if (el) {
		if (el.addEventListener) {
			el.addEventListener(sEvent, newFunc, bCapture);
		} else if (el.attachEvent) {
			el.attachEvent("on" + sEvent, newFunc);
		} else {
			alert("cannot addEventListener");
		}
	}
}
// The following functions provide some basic functionality missing from most browsers
Array.prototype.indexOf = function(o) {
	for (var i = 0; i < this.length; i++)
		if (this[i] == o)
			return i;
	return -1;
};
Array.prototype.lastIndexOf = function(o) {
	for (var i = this.length - 1; i >= 0; i--)
		if (this[i] == o)
			return i;
	return -1;
};
Array.prototype.contains = function(o) {
	return this.indexOf(o) != -1;
};
Array.prototype.copy = function(o) {
	return this.concat();
};
Array.prototype.insertAt = function(o, i) {
	this.splice(i, 0, o);
};
Array.prototype.insertBefore = function(o, o2) {
	var i = this.indexOf(o2);
	if (i == -1)
		this.push(o);
	else
		this.splice(i, 0, o);
};
Array.prototype.removeAt = function(i) {
	this.splice(i, 1);
};
Array.prototype.remove = function(o) {
	var i = this.indexOf(o);
	if (i != -1)
		this.splice(i, 1);
};
Array.prototype.removeEmptyEntries = function() {
    for (var i = this.length - 1; i >= 0; i--) {
        if (typeof(this[i]) == "string" && this[i].trim().length == 0) {
            delete this[i];
            this.length -= 1;
        }
    }
};
String.prototype.trim = function() {
	return this.replace(/(^\s+)|\s+$/g, "");
};
String.prototype.trimEnd = function() {
	return this.replace(/\s+$/g, "");
};
String.prototype.remove = function(chars) {
    var s = this;
	for (var i = 0; i < chars.length; i++) {
	    while (s.indexOf(chars[i]) >= 0) {
	        s = s.replace(chars[i], "");
	    }
	}
	return s;
};
String.format = function (sFormat) {
    var token;
    for (var i = 1; i < arguments.length; i++) {
        token = "{" + (i - 1) + "}";
        while (sFormat.indexOf(token) >= 0) {
            sFormat = sFormat.replace(token, arguments[i]);
        }
    }
    return sFormat;
};

String.prototype.equal = function(s) {
    if (this.length != s.length) { return false; }
    for (var i = 0; i < this.length; i++) {
        if (this.charAt(i) != s.charAt(i)) {
            //fix for culture problem with number format like "-999 999 999,88880"
            if (this.charCodeAt(i) != 32 || s.charCodeAt(i) != 160){            
                return false;
            }
        }
    }
    return true;
}
Date.prototype.addYears = function(nNumberOfYears) {
	var d = new Date(this);
	d.setYear(this.getYear() + nNumberOfYears);
	return d;
};
Date.prototype.getTotalMinutes = function() {
	return (this.getHours() * 60) + this.getMinutes();
};
Date.prototype.getTotalSeconds = function() {
	return (this.getTotalMinutes() * 60) + this.getSeconds();
};
Date.prototype.getTotalMilliseconds = function() {
	return (this.getTotalSeconds() * 1000) + this.getMilliseconds();
};
Date.prototype.getMonthName = function() {
	var a = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
	return a[this.getMonth()];
};
Date.prototype.getShortMonthName = function() {
	return this.getMonthName().substr(0, 3);
};
Date.prototype.getDayName = function() {
	var a = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
	return a[this.getDay()];
};
Date.prototype.getShortDayName = function() {
	return this.getDayName().substr(0, 3);
};
Date.prototype._getYear = Date.prototype.getYear;
Date.prototype.getYear = function() {
	var n = this._getYear();
	if (String(n).length == 2) {
		n += 1900;
	}
	return n;
}

// The -is- object is used to identify the browser.  Every browser edition
// identifies itself, but there is no standard way of doing it, and some of
// the identification is deceptive. This is because the authors of web
// browsers are liars. For example, Microsoft's IE browsers claim to be 
// Mozilla 4. Netscape 6 claims to be version 5.
var is = {
	ie:      navigator.appName == 'Microsoft Internet Explorer',
	java:    navigator.javaEnabled(),
	ns:      navigator.appName == 'Netscape',
	ua:      navigator.userAgent.toLowerCase(),
	version: parseFloat(navigator.appVersion.substr(21)) || parseFloat(navigator.appVersion),
	win:     navigator.platform == 'Win32'};
is.mac = is.ua.indexOf('mac') >= 0;
if (is.ua.indexOf('opera') >= 0) {
	is.ie = is.ns = false;
	is.opera = true;
} else {
	is.opera = false;
}
if (is.ua.indexOf('gecko') >= 0) {
	is.ie = is.ns = false;
	is.gecko = true;
} else {
	is.gecko = false;
}
if (is.ua.indexOf("safari") >= 0) {
	is.ie = is.ns = false;
	is.safari = true;
} else {
	is.safari = false;
}
if (is.ua.indexOf("firefox") >= 0) {
	is.ie = is.ns = false;
	is.firefox = true;
} else {
	is.firefox = false;
}

function openDownloadWindow(url) {    
    addEventListenerEx(window, "load", function(){
        if(is.ie && is.version == 6) {        
            var frame = document.createElement("iframe");
            document.appendChild(frame);
            if(frame) {
                try {                
                    frame.src = url;
                }
                catch(e){}
            }
        } else {
            window.open(url);
        }   
    })
}

function DocumentSelectionOnClick(ckb, itemId)
{    
    //debugger;
    var hdnItemIdsElem = document.getElementById("hiddenFieldSelectItemId");
    if(hdnItemIdsElem) {
        if(ckb.checked) {
            hdnItemIdsElem.value = ConcatValue(hdnItemIdsElem.value, itemId);
        } else {
            hdnItemIdsElem.value = RemoveValue(hdnItemIdsElem.value, itemId);
        }        
    }
}

function ConcatValue(beginValue, itemId)
{
    if(beginValue.length == 0) {
        beginValue += itemId;    
    } else {
        beginValue += ";" + itemId;
    }
    return beginValue;
}

function RemoveValue(beginValue, itemId)
{
    var values = beginValue.split(";");
    values.remove(itemId);
    beginValue = "";
    for(var i = 0; i < values.length; i++)
    {    
        if(i == values.length - 1) {
            beginValue += values[i]
        } else {
            beginValue += values[i] + ";"
        }
    }
    return beginValue;
}