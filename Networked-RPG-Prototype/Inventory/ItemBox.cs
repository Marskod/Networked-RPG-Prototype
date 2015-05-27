using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemBox : MonoBehaviour {

    private ItemDatabase database;
    public bool showWind = true;
    private Inventory inv; 

    private int items = 3; 
    public List<Item> container = new List<Item>();
    public List<Item> slots = new List<Item>();

    public GUISkin skin;
    private Rect windRect = new Rect(200, 100, 291, 230);

    public GameObject[] players;

    void Start()
    {
        for (int i = 0; i < items; i++)
        {
            slots.Add(new Item());
            container.Add(new Item());
        }

        database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();    
        //inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

        AddItem(0);
        AddItem(2);
        AddItem(4);
    }

    void OnMouseDown()
    {
        showWind = !showWind;

        players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject p in players)
        {
            if (Vector3.Distance(transform.position, p.transform.position) < 5)
            {
                inv = p.GetComponent<Inventory>();
                GetComponent<Animation>().Play();
                GetComponent<Animation>().wrapMode.Equals("PingPong");
            }
            else
            {
                inv = null;
                GetComponent<Animation>().Rewind();
            }
        }
    }

    void OnGUI()
    {
        GUI.skin = skin;

        if(showWind)
            windRect = GUI.Window(2,windRect,contWind,"",skin.GetStyle("BackGround"));
    }

    void contWind(int WindID)
    {
        int i = 0;

        for (float y = 0; y < items; y++)
        {
            Rect slotRect = new Rect(y * 60, y, 50, 50);
            if(GUI.Button(slotRect, "", skin.GetStyle("Slot")))
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
                GUI.DrawTexture(slotRect, slots[i].itemIcon);
            }
            i++;
        }

        if(GUI.Button(new Rect(210, 05, 30, 30), "X"))
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
