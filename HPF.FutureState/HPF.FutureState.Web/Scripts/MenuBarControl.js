﻿

var timeout	= 200;
var closetimer	= 0;
var ddmenuitem	= 0;
var currentItem = 0;
var parentItem = 0;
var backcolor =  '#55AAFF';// '#49A3FF';

function MenuMouseOver(sender)
{
    sender.style.background = backcolor;
    mclose();
}
function MenuMouseLeave(sender)
{
    sender.style.background = 'transparent';
}

// open hidden layer
function mopen(parent, id, selid)
{	
    if(currentItem) currentItem.style.background='transparent';
	currentItem = document.getElementById(selid);
	// cancel close timer
	mcancelclosetime();

	// close old layer
	if(ddmenuitem) ddmenuitem.style.visibility = 'hidden';

	// get new layer and show it
	ddmenuitem = document.getElementById(id);
	ddmenuitem.style.visibility = 'visible';
	parentItem = parent;
	parentItem.style.background = backcolor;
    
}
// close showed layer
function mclose()
{
	if(ddmenuitem) ddmenuitem.style.visibility = 'hidden';
	if(currentItem) currentItem.style.background = 'transparent';
    if(parentItem) parentItem.style.background = 'transparent';
}

// go close timer
function mclosetime()
{
    
	closetimer = window.setTimeout(mclose, timeout);
	
}

// cancel close timer
function mcancelclosetime()
{
	if(closetimer)
	{
		window.clearTimeout(closetimer);
		closetimer = null;
	}
	if(currentItem) 
	{
	    if(currentItem.style.background!=backcolor)
	        currentItem.style.background= backcolor;
	}
    
}

// close layer when click-out
document.onclick = mclose; 
