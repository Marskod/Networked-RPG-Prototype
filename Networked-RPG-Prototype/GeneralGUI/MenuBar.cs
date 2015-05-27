using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuBar : MonoBehaviour {

    public bool connected = false;

    private Inventory inv;
    private CharacterMenu charScript;

    public GUISkin skin;

    private MenuDatabase database; 

    void Start()
    {
        inv = GetComponent<Inventory>();
        charScript = GetComponent<CharacterMenu>();
    }

	void OnGUI()
    {
        GUI.depth = 1;
        if (GUI.Button(new Rect((Screen.width / 2.45f), Screen.height * .91f, 50, 50), "", skin.GetStyle("CharacterMenu")))
        {
            charScript.showWind = !(charScript.showWind);
        }
        else if (GUI.Button(new Rect((Screen.width / 2.45f) + 60, Screen.height * .91f, 50, 50), "", skin.GetStyle("InventoryIcon")))
        {
            inv.showInventory = !(inv.showInventory);
        }
        else if (GUI.Button(new Rect((Screen.width / 2.450f) + 120, Screen.height * .91f, 50, 50), "", skin.GetStyle("MenuIcon")))
        {

        }
    }
}
