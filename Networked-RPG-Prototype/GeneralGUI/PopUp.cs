using UnityEngine;
using System.Collections;

public class PopUp : MonoBehaviour {

	public float popLength = .5f;
    public bool pop = false;
    public string text = "";

    private bool display;

    public GUISkin skin;

    /*void PopUp(float popL, bool popT)
    {
        popL = popLenght;
        popT = pop;
    }*/

    void Update()
    {
        if(pop)
        {
            display = true;
            popLength -= Time.deltaTime;
        }
        if (popLength < 0f)
        { 
            display = false;
            pop = false;
            popLength = .5f; 
        }
    }

    void OnGUI()
    {
        GUI.skin = skin;

        Rect toolTip = new Rect(Screen.width / 2, Screen.height / 4, 300, 300);

        if(display)
            GUI.Box(toolTip, text, skin.GetStyle("PopUp"));
    }
}
