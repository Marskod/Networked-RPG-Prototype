var netplayer : Transform;

function Update()
{
	var dual = gameObject.GetComponent("DualScript");
	
	if(Vector3.Distance(netplayer.position, transform.position)<5)
	{
	dual.enabled = true;
	}
	
	if(Vector3.Distance(netplayer.position, transform.position)>5)
	{
	dual.enabled = false; 
	}
}