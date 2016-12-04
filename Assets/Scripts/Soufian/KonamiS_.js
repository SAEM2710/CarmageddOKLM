private var konamiCode = ["UpArrow", "UpArrow", "DownArrow", "DownArrow", "LeftArrow", "RightArrow", "LeftArrow", "RightArrow", "B", "A", "Return"];
private var currentPos : int = 0;
private var inKonami : boolean = false;
 
function OnGUI () {
    var e : Event = Event.current;
    if (e.isKey && Input.anyKeyDown && !inKonami && e.keyCode.ToString()!="None") {
        konamiFunction (e.keyCode);
    }
}
 
function Update () 
{
    if (inKonami) 
    {
        if(!GetComponent("CameraFilterPack_FX_Glitch1").enabled)
        {
            GetComponent("CameraFilterPack_FX_Glitch1").enabled = true;
        }
    }
    else
    {
        if(GetComponent("CameraFilterPack_FX_Glitch1").enabled)
        {
            GetComponent("CameraFilterPack_FX_Glitch1").enabled = false;
        }
    }
}
 
function konamiFunction (incomingKey) {
     
    var incomingKeyString = incomingKey.ToString();
    if(incomingKeyString==konamiCode[currentPos]) {
        print("Unlocked part "+(currentPos+1)+"/"+konamiCode.length+" with "+incomingKeyString);
        currentPos++;
         
        if((currentPos+1)>konamiCode.length){
            print("You master Konami.");
            inKonami=true;
            currentPos=0;
        }
    } else {
        print("You fail Konami at position "+(currentPos+1)+", find the ninja in you.");
        currentPos=0;
    }
     
}