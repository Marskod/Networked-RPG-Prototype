using UnityEngine;
using System.Collections;

public class ExpManager : MonoBehaviour {

    private int baseLevel = 594; 
    private bool connected = false;
    public float health = 500;
    private float maxHealth = 500;

    public int exp;
    public int expMax;
    public int barWidth = 594;

    public GUISkin skin;

    void OnServerInitialized()
    {
        maxHealth = health;
        fill();
    }

    void OnConnectedToServer()
    {
        maxHealth = health;
        fill();
    }

    void Update()
    {
        if (health <= 0)
            Destroy(gameObject);
    }

    void OnGUI()
    {
        Rect outline = new Rect((Screen.width / 3.7f), Screen.height * .87f, 495, 12);

        GUI.skin = skin;
        GUI.Box(new Rect(outline), "", skin.GetStyle("ExpSkin"));


        GUI.Box(new Rect((Screen.width / 3.7f), Screen.height * .85f, health, 0), "             \t\t\t"+health+"/"+maxHealth, skin.GetStyle("ExpBar"));

        if (exp < 594)
            GUI.Box(new Rect((Screen.width / 3.7f), Screen.height * .88f, exp, 0), "", skin.GetStyle("ExpBar"));
    }

    void fill()
    {
        connected = true;
    }
}
