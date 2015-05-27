using UnityEngine;
using System.Collections;

public class Mining : MonoBehaviour {

    public int resourceID;
    public GUISkin skin;
    

    private Inventory inv;

    private bool connected;
    private bool count = false; 

    public float timer = 5;
    public float time = 5; 

    private PopUp pop;

    public int life;
    private GameObject player;

    public GameObject[] res; 

    void Start()
    {
        connected = true;

        switch(resourceID)
        {
            case 2: //Stone
                life = Random.Range(1000, 5000);
                break;
            case 3: //wood
                life = Random.Range(30, 100);
                break;
        }
    }

    void OnServerInitialized()
    {
        print("yo!");

    }

    void OnGUI()
    {
        GUI.skin = skin;

        if(res.Length == 0)
            res = GameObject.FindGameObjectsWithTag("Player");

        Rect toolTip = new Rect(Screen.width / 2, Screen.height / 2, 130, 20);
        Rect timerNotification = new Rect(Screen.width / 4, Screen.height / 4, 130, 40);

        if (res != null)
        {
            foreach (GameObject i in res)
            {
                if (Vector3.Distance(transform.position, i.transform.position) < 5)
                {
                    GUI.Box(toolTip, "Press F to mine", skin.GetStyle("Slot"));
                    GUI.Box(timerNotification, "TIME: " + timer.ToString("F0") + "\nCapacity: " + life, skin.GetStyle("Slot"));

                    if (Input.GetButtonDown("Mine") && timer == time)
                    {
                        count = true;
                        player = i;
                    }
                }
                else
                {
                    count = false;
                    player = null;
                }
            }
        }

    }

    void Update()
    {
            if (life <= 0)
                Destroy(gameObject);

            if (count == true)
                timer -= Time.deltaTime;

            if (timer < 0 && player != null)
            {
                count = false;
                addResource();
                life -= 1;
                timer = time;
                player = null;
            }
    }

    void addResource()
    {
        inv = player.GetComponent<Inventory>();
        pop = GameObject.Find("MenuManager").GetComponent<PopUp>();

        inv.AddItem(resourceID);
        player.GetComponent<ExpManager>().exp += 50;
        pop.pop = true;
        pop.text = "+" + 50 + "XP!";
    }

}
