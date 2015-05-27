using UnityEngine;
using System.Collections;

public class Chat : MonoBehaviour {
	
	bool usingChat = false;
	bool showChat = false;
	
	string inputFeild = "";
	
	Vector2 scrollposition;
	int width = 500;
	int height = 100;
	string playerName;
	float lastUnfocusTime = 0;
	Rect window;
	
	ArrayList playerList = new ArrayList();

	class PlayerNode
	{
		public string playerName; 
		public NetworkPlayer player;
	}
	
	ArrayList chatEntries = new ArrayList();
	class ChatEntry 
	{
		public string name = "";
		public string text = "";
	}
	
	// Use this for initialization
	void Start () 
	{
        playerName = gameObject.GetComponent<PlayerNAme>().getPlayerName;
	 	window = new Rect(Screen.width / 2 - width / 2, Screen.height - height + 5, width, height);
		playerName = PlayerPrefs.GetString("playerName","");		
		if(playerName == "") playerName = "RandomName" + Random.Range(1, 990);
		
	}
	
	void OnConnectedToServer()
	{
		ShowChatWindow();
		GetComponent<NetworkView>().RPC("TellServerOurName", RPCMode.Server, playerName);
		addGameChatMessage(playerName + " Has joined the Chat");
	}
	
	void OnServerInitialized()
	{
		ShowChatWindow();
		PlayerNode newEntry = new PlayerNode();
		newEntry.playerName = playerName;
		newEntry.player = Network.player;
		playerList.Add(newEntry);

		addGameChatMessage(playerName  + " Has joined the Chat");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	PlayerNode GetPlayerNode(NetworkPlayer netPlay)
	{
		foreach(PlayerNode entry in playerList)
		{
			if(entry.player == netPlay)
				return entry; 
		}
		Debug.LogError("GetPlayerNode request a player node of non-ex string player");
		return null;
		
	}
	
	void OnPlayerDisconnected(NetworkPlayer netPlayer)
	{
		addGameChatMessage("A player has Dissconected");
		playerList.Remove(GetPlayerNode(netPlayer));
	}
	
	void OnDisconnectedFromServer()
	{
		CloseChatWindow();
	}
	
	[RPC]
	void TellServerOurName(string name, NetworkMessageInfo info)
	{
		PlayerNode newEntry = new PlayerNode();
		newEntry.playerName = playerName;
		newEntry.player = Network.player;
		playerList.Add(newEntry);
		addGameChatMessage(playerName + "has just joined the chat");
	}
	
	void CloseChatWindow()
	{
		showChat = false;
		inputFeild = "";
		chatEntries = new ArrayList();
	}
	
	void ShowChatWindow()
	{
		showChat = true;
		inputFeild = "";
		chatEntries = new ArrayList();
	}
	
	void OnGUI()
	{
		if (!showChat) return;
		
		if(Event.current.type == EventType.keyDown && Event.current.character == '\n' & inputFeild.Length <= 0)
		{
			if(lastUnfocusTime + .25 < Time.time)
			{
				usingChat = true;
				GUI.FocusWindow(5);
				GUI.FocusControl("Chat input field");
			}
		}
		
		window = GUI.Window(5, window, GlobalChatWindow, "");
	}
	
	void GlobalChatWindow(int id)
	{
		GUILayout.BeginVertical();
		GUILayout.Space(10);
		GUILayout.EndVertical();
		
		scrollposition = GUILayout.BeginScrollView(scrollposition);
		
		foreach(ChatEntry entry in chatEntries)
		{
			GUILayout.BeginHorizontal();
			if(entry.name == " - ")
				GUILayout.Label(entry.name + entry.text);
			else
				GUILayout.Label(entry.name + ": " + entry.text);
			
			GUILayout.EndHorizontal();
			GUILayout.Space(2);
		}
		
		GUILayout.EndScrollView();
		
		if(Event.current.type == EventType.keyDown && Event.current.character == '\n' & inputFeild.Length > 0)
			HitEnter(inputFeild);
		
		GUI.SetNextControlName("chat input field");
		inputFeild = GUILayout.TextField(inputFeild);
		
		if(Input.GetKeyDown("mouse 0"))
		{
			if(usingChat)
			{
				usingChat = false;
				GUI.UnfocusWindow();
				lastUnfocusTime = Time.time;
			}
			
		}
	}
		
		void HitEnter(string msg)
		{
			msg = msg.Replace('\n', ' ');
			GetComponent<NetworkView>().RPC("ApplyGlobalChatText", RPCMode.All, playerName, msg);
		}
		
		[RPC]
		void ApplyGlobalChatText(string name, string msg)
		{
			ChatEntry entry = new ChatEntry();
			entry.name = name;
			entry.text = msg; 
			
			chatEntries.Add(entry);
			
			if(chatEntries.Count > 4)
				chatEntries.RemoveAt(0);
			
			scrollposition.y = 100000000;
			inputFeild = "";
		}
		
	
	void addGameChatMessage(string str)
	{
		ApplyGlobalChatText(" - ", str);
		if(Network.connections.Length > 0)
			GetComponent<NetworkView>().RPC("ApplyGlobalChatText", RPCMode.Others, " - ", str);
	}
}
