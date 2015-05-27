var camerascript = "SmoothFollow";
//var minimap : GameObject;
function OnNetworkInstantiate(info : NetworkMessageInfo)
    {

	if(GetComponent.<NetworkView>().isMine)
	{
        /** MAIN CAMERA**/
		var other = gameObject.Find("Main Camera");
		var other1 = other.GetComponent(camerascript);
		other1.target = transform;

	    /** MINI CAMERA**/
		var mini = gameObject.Find("MiniMapCam");
		//mini.enabled = true;
	}
}