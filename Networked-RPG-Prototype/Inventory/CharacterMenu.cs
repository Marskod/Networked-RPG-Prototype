using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterMenu : MonoBehaviour {


    private ItemDatabase database;
    public bool showWind;
    private Inventory inv; 

    private int items = 3; 
    public List<Item> container = new List<Item>();
    public List<Item> slots = new List<Item>();

    public GUISkin skin;
    private Rect windRect = new Rect(300, 200, 391, 330);

    public Item weapon;

    void Start()
    {
        for (int i = 0; i < items; i++)
        {
            slots.Add(new Item());
            container.Add(new Item());
        }

        database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();    
        inv = GetComponent<Inventory>();
    }

    void Update()
    {
        if(Input.GetKeyDown("c"))
        {
            showWind = !showWind;
        }
    }

    void OnGUI()
    {
        GUI.skin = skin;

        if(showWind)
            windRect = GUI.Window(2,windRect,contWind,"Character Menu",skin.GetStyle("CharacterMenuBackground"));
    }

    void contWind(int WindID)
    {
        Rect slotRect = new Rect(20, (110) + 30, 50, 50);

        //Head
        if (GUI.Button(new Rect(80, 30, 50, 50), "", skin.GetStyle("Slot")))
        {
        }
        //Torso
        else if (GUI.Button(new Rect(80, (70) + 30, 50, 50), "", skin.GetStyle("Slot")))
        {
        }
        //Legs 
        else if (GUI.Button(new Rect(80, (170) + 30, 50, 50), "", skin.GetStyle("Slot")))
        {
        }
        //Weapon
        else if (GUI.Button(slotRect, "", skin.GetStyle("Slot")))
        {
            inv.AddItem(weapon.itemID);
            weapon = null;
        }

        if (weapon.itemIcon != null && weapon != null)
            GUI.DrawTexture(slotRect, weapon.itemIcon);

        GUI.Box(new Rect(200,20,180,300),"\t\t\tPlayer Stats",skin.GetStyle("Background"));
        GUILayout.BeginArea(new Rect(210, 30, 180, 300));
        GUILayout.Label("Health: ",skin.label);
        GUILayout.Label("Level: ");
        GUILayout.Label("Mining Level: ");
        GUILayout.Label("Buildings Built: ");
        GUILayout.EndArea();

        /*
        for (float y = 0; y < items; y++)
        {
            Rect slotRect = new Rect(80, (y * 70) + 30, 50, 50);
            if (GUI.Button(slotRect, "", skin.GetStyle("Slot")))
            {
                print(container[i].itemID);

                //If money, do not add to inventroy, simply add money to the player's inventory 
                if (container[i].itemID == 4 && InventoryContains(container[i].itemID) == true)
                {
                    inv.monies += 10;
                    RemoveItem(container[i].itemID,i);
                }

                else if(InventoryContains(container[i].itemID) == true)
                {
                    inv.AddItem(container[i].itemID);
                    //Remove the item of this ID and this Index of the array 
                    RemoveItem(container[i].itemID,i);
                    print("spot:"+i);
                }
            }
            slots[i] = container[i];

            if(slots[i].itemName != null)
            {
               // GUI.DrawTexture(slotRect, slots[i].itemIcon);
            }
            i++;
        }*/

        if(GUI.Button(new Rect(367, 2, 19, 17), "X"))
        {
            showWind = false;
        }
        GUI.DragWindow();
    }

    void RemoveItem(int id, int start)
    {
        //Start at the given index of the item needing to be removed. 
        for (int i = start; i < container.Count; i++)
        {
            if (container[i].itemID == id)
            {
                container[i] = new Item();
                break;
            }
        }
    }

    public void AddItem(int id)
    {
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].itemName == null)
            {
                for (int j = 0; j < database.items.Count; j++)
                {
                    if (database.items[j].itemID == id)
                    {
                        container[i] = database.items[j];
                    }
                }
                break;
            }
        }
    }

    bool InventoryContains(int id)
    {
        foreach (Item item in container)
        {
            if (item.itemID == id) return true;
        }
        return false;
    }
}

