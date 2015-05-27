using UnityEngine;
using System.Collections;

public class converseMenu : MonoBehaviour {

    private Color dColor;

    public string noodleName = "";
    public string defaultContent = "";
    public string afterContent = ""; 

    private Rect windRect = new Rect(200, 100, 291, 230);
    private bool showWind = false;

    public bool activeQuest = true;
    public GameObject questSignal;

    public GUISkin skin; 
    
    void Start()
    {
        dColor = gameObject.GetComponent<Renderer>().material.color;
    }

    void OnGUI()
    {
        GUI.skin = skin;

        if(showWind)
            windRect = GUI.Window(4, windRect, talkWindow, noodleName, skin.GetStyle("BackGround"));
    }

    void Update()
    {
        if (activeQuest == false)
            questSignal.SetActive(false);
        else
            questSignal.SetActive(true);
    }

    void OnMouseOver()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(1,1,1,1);
    }
    
    void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().material.color = dColor;
    }

    void OnMouseDown()
    {
        showWind = true;
    }

    void talkWindow(int windID)
    {
        GUI.skin = skin;

        if (GUI.Button(new Rect(259, 1, 30, 30), "X",skin.GetStyle("Slot")))
        {
            showWind = false;
        }

        if(activeQuest)
            GUI.Label(new Rect(10, 40, 275, 180), defaultContent,skin.GetStyle("Text"));

        GUI.DragWindow();
    }
}
