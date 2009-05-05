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
    var loadingDiv= document.getElementById("loadingDiv");    
    
    if(!loadingDiv)
    {
        loadingDiv = document.createElement("div");
        loadingDiv.id = "loadingDiv";
        document.body.appendChild(loadingDiv);
        loadingDiv.style.backgroundColor = "red";        
        loadingDiv.style.zIndex = 1000;
        moveIt(loadingDiv, 0, 0);        
        
        var progressStatus = document.createElement("div");
        progressStatus.id = "progressStatus";
        loadingDiv.appendChild(progressStatus);
        progressStatus.innerHTML = "Please wait...";
        progressStatus.style.color = "white";
        progressStatus.style.fontWeight = 'bold';
        
//        var slider = document.createElement("div");
//        slider.id = "slider";
//        loadingDiv.appendChild(slider);
//        slider.style.backgroundColor = "blue";
    }
    loadingDiv.style.display = "";
}

function updateProgressBar(progressData)
{
    var loadingDiv= document.getElementById("loadingDiv");
    
    //clear interval if no data returned from server
	if(progressData && !progressData.InProgress) {
	    loadingDiv.style.display = "none";
	    if(progressData.HasError) {
	        alert(progressData.ErrorMessage);
	    }
    }
    else {        
        if(progressData && progressData.InProgress) {
            var progressStatus = document.getElementById("progressStatus");
            progressStatus.innerHTML = progressData.ProcessPercentage + "%" + ":" + progressData.ProgressAction;
            setTimeout("CallProgressDataInterval()", 0);
        }
    }
}