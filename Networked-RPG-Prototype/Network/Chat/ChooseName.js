
var pname : Transform;
var spawnpoint: Transform;
var Netname : boolean = true;
var nametext : String = "Player";
function OnGUI()
{
	if(GetComponent.<NetworkView>().isMine)
{
	if(Netname)
	{
	
		GUI.Window(0,Rect(300,300,300,100),n,"Choose Character Name");
	}
	}
}

function n(windowID : int)
{
nametext = GUI.TextField (Rect (10, 50, 200, 20), nametext, 25);
	if(nametext != "Player")
		if(GUI.Button(Rect(25,75,128,24),"Done"))
		{
			Netname = false;
			var fire = Network.Instantiate(pname,spawnpoint.position,transform.rotation,0);
			pname.parent = transform; 
			/*var textfind = gameObject.Find("PlayerName");
			var namedone = gameObject.GetComponent("Text Mesh");
			var invgui = gameObject.GetComponent("Inventory");
			invgui.enabled = true;
			textfind.GetComponent(TextMesh).text = nametext;
			*/
		}
}
