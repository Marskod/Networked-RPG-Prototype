using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuDatabase : MonoBehaviour {

    public List<Menu> menus = new List<Menu>();

    void Awake()
    {
        menus.Add(new Menu("Inventory", "A bag that holds your items",0));
        menus.Add(new Menu("Character Menu", "A bag that holds your items",1));
    }
}
