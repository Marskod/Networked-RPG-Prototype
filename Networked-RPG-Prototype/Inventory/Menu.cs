using UnityEngine;
using System.Collections;

[System.Serializable]


public class Menu {

    public string name;
    public string toolTip;
    public int id; 
    public Texture2D menuIcon; 
	
    public Menu(string n, string tt,int i)
    {
        n = name;
        tt = toolTip;
        menuIcon = UnityEngine.Resources.Load<Texture2D>("Menus Icons/" + name);
        i = id;
    }

    public Menu()
    {

    }
}
