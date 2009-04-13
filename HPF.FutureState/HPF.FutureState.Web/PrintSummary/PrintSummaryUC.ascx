<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrintSummaryUC.ascx.cs"
    Inherits="HPF.FutureState.Web.PrintSummary.PrintSummaryUC" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<rsweb:ReportViewer ID="ReportViewerPrintSummary" runat="server" Width="100%" Font-Names="Verdana"
    Font-Size="8pt" Height="610px" ProcessingMode="Remote" ShowParameterPrompts="false"
    ShowExportControls="true">
</rsweb:ReportViewer>
<div style="display: none;">
    <object id="RSClientPrintHack" classid="CLSID:FA91DF8D-53AB-455D-AB20-F2F023E498D3">
    </object>
</div>

<script type="text/javascript">
    var http;
    function addEventController(el, sEvent, func, bCapture) {
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
    
    addEventController(window, "load", function(e){
        RSClientController.prototype.LoadPrintControl = RSCC_LoadPrintControl_Hack;
        
        http = getHTTPObject();
    });
    
    function RSCC_LoadPrintControl_Hack()
    {        
        var printFrame = document.getElementById(this.m_printFrameId);
        if (printFrame != null)
        {
            if (printFrame.src != this.m_printHtmlLink) {
                http.open("GET", this.m_printHtmlLink, true); 
                http.onreadystatechange = handleHttpResponse; 
                http.send(null);
                
                RSClientController.PrintFrameId = this.m_printFrameId;
            }
            else {
                eval(this.m_printFrameId + ".Print();");
            }
        }
        
        return false;
    }

    function getHTTPObject() {
      var xmlhttp;
      /*@cc_on
      @if (@_jscript_version >= 5)
        try {
          xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
        } catch (e) {
          try {
            xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
          } catch (E) {
            xmlhttp = false;
          }
        }
      @else
      xmlhttp = false;
      @end @*/
      if (!xmlhttp && typeof XMLHttpRequest != 'undefined') {
        try {
          xmlhttp = new XMLHttpRequest();
        } catch (e) {
          xmlhttp = false;
        }
      }
      return xmlhttp;
    }
    
    function handleHttpResponse() {
      if (http.readyState == 4) {
        if(window.frames[RSClientController.PrintFrameId].Print == undefined) {
            var oldCId = "CLSID:FA91DF8D-53AB-455D-AB20-F2F023E498D3";
            var newCId = "CLSID:41861299-EAB2-4DCC-986C-802AE12AC499";
            
            var text = http.responseText;
            var RSClientPrintHack = document.getElementById("RSClientPrintHack");
            if (typeof(RSClientPrintHack.Print) == "undefined")            
            {
                text = http.responseText.replace(oldCId, newCId);
            }
            window.frames[RSClientController.PrintFrameId].document.write(text);
        }
        window.frames[RSClientController.PrintFrameId].Print();
      }
    }


</script>

