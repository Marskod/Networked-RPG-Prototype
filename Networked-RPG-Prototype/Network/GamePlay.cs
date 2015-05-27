using UnityEngine;
using System.Collections;

public class GamePlay : MonoBehaviour {
	public Transform player;
	private Transform text; 
	
	void OnServerInitialized()
	{
		SpawnPlayer();
	}
	
	void OnConnectedToServer()
	{
		SpawnPlayer();
	}

	
	void SpawnPlayer()
	{
		Network.Instantiate(player, transform.position, transform.rotation, 0);
		//Network.Instantiate(text, transform.position, transform.rotation, 0);
	}
	
	void OnPlayerDisconnected(NetworkPlayer player)
	{
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
		
	}
	void OnDisconnectedFromServer(NetworkDisconnection info)
	{
		Network.RemoveRPCs(Network.player);
		Network.DestroyPlayerObjects(Network.player);
		Application.LoadLevel(Application.loadedLevel);
	}

}
