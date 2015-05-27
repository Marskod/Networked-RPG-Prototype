using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class PlayerMenu : MonoBehaviour {

    public List<charPlayer> characterList = new List<charPlayer>();

    private string playerName = "Player Name";
    private string selectedRace = ""; 

    public GameObject spawn;
    public Transform player;
    public Transform clear;
    public Transform dark;
    public Transform noodle;

    private int counter = 0;
    private int dCounter = 4;

    void Start()
    {
        //PlayerPrefs.DeleteAll();
    }

	void OnGUI()
    {
        int width = Screen.width;
        int height = Screen.height;

        GameObject n = GameObject.Find("Noodle(Clone)");
        GameObject cl = GameObject.Find("ClearHead(Clone)");
        GameObject d = GameObject.Find("DarkHead(Clone)");

        //------------------------------------------------
        //**************New Race Selection**************** 
        GUILayout.BeginArea(new Rect(10, 10, 100, 400));

        GUILayout.Label("New Race");
        GUILayout.Label("Select One:");

        if (GUILayout.Button("Noodle"))
        {
            activate(n,cl, d, noodle, "Noodle");
        }
        if(GUILayout.Button("Clear Head"))
        {
            activate(cl,d, n, clear, "Clear Head");
        }
        if(GUILayout.Button("Dark Head"))
        {
            activate(d,cl, n, dark, "Dark Head");
        }

        GUILayout.Label("Player Name:");
        playerName = GUILayout.TextField(playerName, 15);

        if (GUILayout.Button("Create"))
        {
            if(playerName != "Player Name" && selectedRace!="")
            {
                characterList.Add(new charPlayer(playerName, selectedRace));
                PlayerPrefs.SetString("Race"+counter, selectedRace);
                PlayerPrefs.SetString("Name"+counter, playerName);
                counter--;
            }
        }

        GUILayout.EndArea();
        //--------------------------------------
        //*********Existing Character*********** 
        GUILayout.BeginArea(new Rect(10, 400, 500, 420));

        GUILayout.Label("Characters:");

        characterList.Sort();

        int length = characterList.Count;
        float i = 1;
        int item = 0;

        for (int c = length-1; c >= 0; c--)
        {
            //if ((PlayerPrefs.GetString("Race" + c) != "" && PlayerPrefs.GetString("Name" + c) != ""))
                //characterList.Add(new charPlayer(PlayerPrefs.GetString("Race" + c), PlayerPrefs.GetString("Name" + c)));

            GUI.Label(new Rect(2, 20 * i, 300, 20), "Name: " + characterList[c].name + " Race: " + characterList[c].race);

            if (GUI.Button(new Rect(305, 20 * i, 60, 20), "Load"))
            {
                Debug.Log("Level Loading...");
                Application.LoadLevel("main");
            }
            if (GUI.Button(new Rect(370, 20 * i, 60, 20), "Delete"))
            {
                characterList.RemoveAt(c);
            }
            i = i + 1.2f;
            item++;
        }
        /*
        for (int e = 4; e >= 0; e--)
        {
            GUI.Label(new Rect(2, 20 * i, 300, 20), "Name: " + PlayerPrefs.GetString("Race" + e) + " Race: " + PlayerPrefs.GetString("Name" + e));



            if (GUI.Button(new Rect(305, 20 * i, 60, 20), "Load"))
            {
                Debug.Log("Level Loading...");
                Application.LoadLevel("main");
            }
            if (GUI.Button(new Rect(370, 20 * i, 60, 20), "Delete"))
            {
                //characterList.RemoveAt(c);
            }
            i = i + 1.2f;
            item++;      
        }*/

        GUILayout.EndArea();
    }
    void activate(GameObject curr, GameObject other1, GameObject other2, Transform model, string race)
    {
        if (curr == null)//Check if the model is already present so the user cannot spawn multiple models 
        {
            Destroy(other1);
            Destroy(other2);
            selectedRace = race;
            player = Instantiate(model, spawn.transform.position, Quaternion.identity) as Transform;
        }
    }
}
