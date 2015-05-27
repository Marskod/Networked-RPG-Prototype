using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

    public List<Item> items = new List<Item>();
    public GameObject[] obj;

    void Awake()
    {
        //Power,Speed,Cost
        items.Add(new Item("Rusty Knife", 0, "A Rusty Knife used for \n  minimal protection", 1, 1, 100,Item.ItemType.Weapon, obj[0]));
        items.Add(new Item("Minor Speed Enhancer", 1, "An Enhancer that \n  increases your speed \n  by 15%", 0, 0, 25,Item.ItemType.Consumable, obj[1]));
        items.Add(new Item("Stone", 2, "Used for creating structers", 0, 0, 2, Item.ItemType.Resource,obj[2]));
        items.Add(new Item("Wood", 3, "Used for creating structers", 0, 0, 2, Item.ItemType.Resource,obj[3]));
        items.Add(new Item("Monies", 4, "Monies!", 0, 0, 0, Item.ItemType.Resource,obj[4]));
        items.Add(new Item("Used Pistol", 5, "A weak ranged weapon", 1, 2, 150, Item.ItemType.Weapon, obj[5]));
    }
}
