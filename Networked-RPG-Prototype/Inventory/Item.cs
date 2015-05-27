using UnityEngine;
using System.Collections;

[System.Serializable]

public class Item{

    public string itemName;
    public int itemID;
    public string itemDesc;
    public Texture2D itemIcon;
    public int itemPower;
    public int melee;
    public int sellValue; 
    public ItemType itemType;
    public GameObject obj; 

    public enum ItemType
    {
        Weapon,
        Armor,
        Consumable,
        Resource
    }

    public Item(string name, int id, string desc, int power, int melee, int value,ItemType type, GameObject obj)
    {
        itemName = name;
        itemID = id;
        itemDesc = desc;
        itemIcon = UnityEngine.Resources.Load<Texture2D>("Item Icons/" + name);
        itemPower = power;
        this.melee = melee;
        sellValue = value;
        itemType = type;
        this.obj = obj; 
    }

    public Item()
    {

    }
}
