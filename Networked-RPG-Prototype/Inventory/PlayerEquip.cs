﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerEquip : MonoBehaviour {

    private ItemDatabase database;
    public bool showWind = true;
    private Inventory inv;

    private int items = 4;
    public int monies = 200;
    public List<Item> container = new List<Item>();
    public List<Item> slots = new List<Item>();

    public GUISkin skin;
    private Rect windRect = new Rect(200, 100, 291, 230);

    void Start()
    {
        for (int i = 0; i < items; i++)
        {
            slots.Add(new Item());
            container.Add(new Item());
        }

        database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    void OnMouseDown()
    {
        showWind = !showWind;

        GetComponent<Animation>().Play();

        GetComponent<Animation>().wrapMode.Equals("PingPong");
    }

    void OnGUI()
    {
        GUI.skin = skin;

        if (showWind)
        {
            inv.shopWindOpen = true;
            windRect = GUI.Window(2, windRect, contWind, " Shop Keeper \t\t\t\tMonies: " + monies, skin.GetStyle("BackGround"));
        }
        else
        {
            inv.shopWindOpen = false;
        }
    }

    void contWind(int WindID)
    {
        int i = 0;

        for (float y = 0; y < items; y++)
        {
            Rect slotRect = new Rect(y * 60, y + 20, 50, 50);
            if (GUI.Button(slotRect, "", skin.GetStyle("Slot")))
            {
                print(container[i].itemID);

                if (InventoryContains(container[i].itemID) == true)
                {
                    inv.monies -= container[i].sellValue;
                    inv.AddItem(container[i].itemID);
                    RemoveItem(container[i].itemID);
                }
            }
            slots[i] = container[i];

            if (slots[i].itemName != null)
            {
                GUI.DrawTexture(slotRect, slots[i].itemIcon);
            }
            i++;
        }

        if (GUI.Button(new Rect(210, 05, 30, 30), "X"))
        {
            showWind = false;
        }
        GUI.DragWindow();
    }

    void RemoveItem(int id)
    {
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].itemID == id)
            {
                container[i] = new Item();
                break;
            }
        }
    }

    void AddItem(int id)
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
