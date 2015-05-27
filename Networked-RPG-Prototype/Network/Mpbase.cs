using UnityEngine;
using System.Collections;
using System;

public class Mpbase : MonoBehaviour {
	
	public string connectToIp = "127.0.0.1";
	public int connectPort = 25000;
	public bool useNat = false;
	public string ipaddress = "";
	public string port = "";
	
	public string playerName = "LoginName";
    public GameObject fa;

	void OnGUI()
	{
		if(Network.peerType == NetworkPeerType.Disconnected)
		{
			if(GUILayout.Button("Connect"))
			{
				if(playerName != "LoginName")
				{
					Network.useNat = useNat;
					Network.Connect(connectToIp, connectPort);
					PlayerPrefs.SetString("playerName", playerName);
                    Debug.Log(playerName);
				}
			}
			
			if(GUILayout.Button("Start Server"))
			{
				
				if(playerName != "<NAME ME>")
				{
					Network.useNat = useNat;
					Network.InitializeServer(32, connectPort);

                    //fa.GetComponent<Chat>().enabled = false;

					
					foreach(GameObject go in FindObjectsOfType(typeof(GameObject)))
					{
						go.SendMessage("OnNetWorkLoadLevel", SendMessageOptions.DontRequireReceiver);
						
					}
				   PlayerPrefs.SetString("playerName", playerName);
                   //Debug.Log(playerName);
				}
			}

            playerName = GUILayout.TextField(playerName);
			connectToIp = GUILayout.TextField(connectToIp);
			connectPort = Convert.ToInt32(GUILayout.TextField(connectPort.ToString()));
		}
		else 
		{
			//Application.LoadLevel("ServerTest");
			if(Network.peerType == NetworkPeerType.Connecting) GUILayout.Label("Connect Status: Connecting");
			else if(Network.peerType == NetworkPeerType.Client)
			{
				GUILayout.Label("Connection Status: Client!");
				GUILayout.Label("Ping to Server: " + Network.GetAveragePing(Network.connections[0]));
			}
			else if(Network.peerType == NetworkPeerType.Server)
			{
				GUILayout.Label("Connection Status: Server!");
				GUILayout.Label("Connections: " + Network.connections.Length);
				if(Network.connections.Length >= 1)
					GUILayout.Label("Ping to Server: " + Network.GetAveragePing(Network.connections[0]));
				
			}
			
			if(GUILayout.Button("Disconnect"))
				Network.Disconnect(200);
			
			ipaddress = Network.player.ipAddress;
			port = Network.player.port.ToString();
			GUILayout.Label("IP Address: " + ipaddress + ":" + port);
		}
	}
	
	void OnConnectedToServer()
	{

		foreach(GameObject go in FindObjectsOfType(typeof(GameObject)))
		{
			go.SendMessage("OnNetWorkLoadLevel", SendMessageOptions.DontRequireReceiver);
						
		}
		
	}
}

