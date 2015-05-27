using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	private const string typeName = "UniqueGameName";
	private const string gameName = "RoomName";
	private HostData[] hostList;

	public GameObject playerPrefab;

	private void StartServer() {
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

	private void RefreshHostList () {
		MasterServer.RequestHostList(typeName);
	}

	private void JoinServer (HostData hostData) {
		Network.Connect (hostData);
	}

	private void SpawnPlayer () {
		Network.Instantiate (playerPrefab, new Vector3(10f, 5f, 10f), Quaternion.identity, 0);
	}

	void OnConnectedToServer () {
		SpawnPlayer ();
		Debug.Log ("Server Joined");
	}

	void OnServerInitialized() {
		SpawnPlayer ();
		Debug.Log("Server Initialized");
	}

	void OnMasterServerEvent (MasterServerEvent msEvent) {
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}

	void OnGUI () {
		if (!(Network.isClient || Network.isServer)) {
			if (GUI.Button (new Rect(100, 100, 250, 100), "Start Server"))
				StartServer ();
			if (GUI.Button (new Rect(100, 250, 250, 100), "Refresh Hosts"))
				RefreshHostList();
			if (hostList != null) {
				for (int i = 0; i < hostList.Length; i++)
					if(GUI.Button (new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
						JoinServer(hostList[i]);
			}
		}
	}

	// Use this for initialization
	void Start () {
		//MasterServer.ipAddress = "127.0.0.1";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
