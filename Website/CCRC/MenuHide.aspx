<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MenuHide.aspx.vb" Inherits="MenuHide" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link rel="stylesheet" href="/ccrc/css/main.css" type="text/css">
    <SCRIPT LANGUAGE="JavaScript">
    function toggleFrame () 
    {
//        top.frames.item{0).location.href="www.rediff.com"
//        //middle.leftFrameVisible = !middle.leftFrameVisible;
//        //parent.document.body.cols = middle.leftFrameVisible ? '50%, *' : '0, *';

        var frm = window.parent.frames;
        for (i=0; i < frm.length; i++) 
        {
            var obj;
            obj = frm(i).name;
            //alert(obj);
            if(obj == "menu")
            {
                //parent.document.body.cols = top.leftFrameVisible ? '50%, *' : '0, *';
                //alert("Top Layer Frameset rows: " + parent.document.all("TopFrame").rows);
                //alert("Next Layer Frameset rows: " + parent.document.all("TopFrame").all("MiddleFrame").cols);
                //alert("Next Layer Frame Source: " + parent.document.all("TopFrame").all("menu").src);
                //alert(parent.document.all("TopFrame").all("MiddleFrame").cols)
                if (parent.document.all("TopFrame").all("MiddleFrame").cols != "0,45,*")
                {
                    parent.document.all("TopFrame").all("MiddleFrame").cols="0,45,*";
                    document.getElementById("MenuId").src="/ccrc/images/arrow_open_blue.gif"
                }
                else
                {
                    parent.document.all("TopFrame").all("MiddleFrame").cols="175,45,*";
                    document.getElementById("MenuId").src="/ccrc/images/arrow_close_blue.gif"
                }
            }
        }
    }
</SCRIPT>
</head>
<body style="width:28;">
    <form id="form1" runat="server" style="width:28">
    <table border="0" width="28" cellpadding="0" cellspacing="0">
    <tr>
   <td><a href="javascript:toggleFrame()" style="width:28;"><img BORDER=0 id="MenuId" src="/ccrc/images/arrow_close_blue.gif" </a></td>

    </tr>
    </table>
    </form>
</body>
</html>
