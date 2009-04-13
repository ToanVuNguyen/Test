var TabControl = new Object;
TabControl.onChanged = function(toTabId)
{    
    return true;
};
TabControl.SelectTab = function(toTabId)
{    
    WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions(toTabId,'',false,'','#', false, true));
};