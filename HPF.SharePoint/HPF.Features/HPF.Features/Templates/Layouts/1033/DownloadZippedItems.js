var invoked = 0;
var progressDataFromServer;

function InvokeProgressViaServerSide(me) 
{
    if(invoked % 2 == 0)
    {           
        createProgressContainer();
        
        setTimeout("CallProgressDataInterval()", 0);	    
	    invoked++;
	}
}
function ReceiveProgressDataFromServer(progressData) 
{   
    progressDataFromServer = eval(progressData);
    updateProgressBar(progressDataFromServer);
}

function CallProgressDataInterval() {
    CallProgressData('', '');
}

function moveIt(obj, mvTop, mvLeft) {
	obj.style.position = "absolute";
	obj.style.top = mvTop;
	obj.style.left = mvLeft;
}

function createProgressContainer()
{    
    var progressContainer = document.getElementById("progressContainer");    
    
    if(!progressContainer)
    {   
        var progressContainer = document.createElement("div");
        progressContainer.id = "progressContainer";
        progressContainer.innerHTML = "<table><tr><td><img src='/_layouts/1033/Images/progress.gif' /></td> <td id='loadingDiv'></td> </tr></table>";
        document.body.appendChild(progressContainer);
        progressContainer.style.backgroundColor = "red"; 
        
        var loadingDiv= document.getElementById("loadingDiv");
        loadingDiv.style.zIndex = 1000;
        moveIt(progressContainer, 0, 0);
        
        var progressStatus = document.createElement("div");
        progressStatus.id = "progressStatus";
        loadingDiv.appendChild(progressStatus);
        progressStatus.innerHTML = "Please wait...";
        progressStatus.style.color = "white";
    }
    progressContainer.style.display = "";
}

function updateProgressBar(progressData)
{
    var loadingDiv= document.getElementById("loadingDiv");
    
    //clear interval if no data returned from server
	if(progressData != undefined || progressData != null) {	    
	    if(progressData.InProgress) {
	        var progressStatus = document.getElementById("progressStatus");
            progressStatus.innerHTML = progressData.ProcessPercentage + "%" + ":" + progressData.ProgressAction;                
            setTimeout("CallProgressDataInterval()", 0);
	    } else {	        
	        progressContainer.style.display = "none";
        }
    }
    else {        
        setTimeout("CallProgressDataInterval()", 0);
    }
}

window.onbeforeunload = function() {
    var trackingMessage = "The download is in progress. Are you sure to cancel it ?";
    if(progressDataFromServer != undefined || progressDataFromServer != null) {
        if(progressDataFromServer.InProgress == true) {
            return trackingMessage;
        }
    }
}