using UnityEngine;
using System.Collections;

public class SiliconExtractor : MonoBehaviour {
	public string name;
    public GameObject currPlayer;

	private int structureID;
	private int rank = 0;
    private int health = 500;
    private int quantity = 0; 
    
    public int resID = 2;
    public GameObject[] silicon;

    public GUISkin skin;
    private Rect windRect = new Rect(200, 100, 291, 230);
    public bool showWind = false;

    public float timer = 5;
    public float time = 5;

    public GameObject[] players;
    private bool count = false; 

	public SiliconExtractor()
	{
		
	}
	
    void Start()
    {
        silicon = GameObject.FindGameObjectsWithTag("Resource");
        timer = extract();
        time = extract();
    }

	public int promote()
	{
		return ++rank;
	}
	
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        //Check if there is a silicon desposit nearby. Loop through all silicon desposits that have the tag 'silicon'
        foreach(GameObject i in silicon)
        {
            float dist = Vector3.Distance(i.transform.position, transform.position);

            if (dist < 50 && i.GetComponent<Mining>().resourceID == resID)
            {
                count = true;
                addSilicon();
            }
        }
    }

    public void addSilicon()
    {
        foreach (GameObject p in players)
        {
            if (Vector3.Distance(transform.position, p.transform.position) < 20)
            {
                currPlayer = p;
                //inv = p.GetComponent<Inventory>();
            }
            else
            {
                currPlayer = null;
            }
        }

            if (count == true)
                timer -= Time.deltaTime;

            if (timer < 0)
            {
                count = false;
                quantity += 1;
                timer = time;
            }
    }

    void OnMouseDown()
    {
        Debug.Log("Yo");
        showWind = !showWind;
    }

    void OnGUI()
    {
        GUI.skin = skin;

        if (showWind)
            windRect = GUI.Window(2, windRect, contWind, "\t\tSILICON MINER", skin.GetStyle("BackGround"));
    }

    void contWind(int WindID)
    {
        GUI.skin = skin;

        GUILayout.Label("",skin.GetStyle("Label"));
        GUILayout.Label("Mining Time: " + timer, skin.GetStyle("Label"));
        GUILayout.Label("Amount of Silicon: " + quantity, skin.GetStyle("Label"));

        if (GUILayout.Button("COLLECT RESOURCES", skin.GetStyle("Button")))
        {
            for (int i = 0; i < quantity; i++)
            {
                currPlayer.GetComponent<Inventory>().AddItem(resID);
            }
            print("yoooo");
            quantity = 0;        
        }
        if (GUILayout.Button("AUTOSELL RESOURCES", skin.GetStyle("Button")))
        {
            currPlayer.GetComponent<Inventory>().monies += quantity * 2;
            quantity = 0;
        }

        if (GUI.Button(new Rect(250, 05, 30, 20), "X", skin.GetStyle("Button")))
        {
            showWind = false;
        }
        GUI.DragWindow();
    }

	public int extract()
	{
		switch(rank)
		{
		case 0:
			return 40;
		case 1:
			return 30;
		case 2:
			return 20;
		case 3:
			return 15;
		case 4:
			return 8;
		}

		return 0;
	}
}
