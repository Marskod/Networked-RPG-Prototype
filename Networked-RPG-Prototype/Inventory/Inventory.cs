using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{

    public int slotsX, slotsY;
    public GUISkin skin;
    public bool shopWindOpen = false; 
    public List<Item> inventory = new List<Item>();
    public List<Item> slots = new List<Item>();
    public bool showInventory;
    
    private ItemDatabase database;
    private CharacterMenu charMen;
    private PlayerCombat combat;
    private ConsumableManager consumable;
    private bool showTooltip;
    private string tooltip;

    private bool draggingItem;
    private Item draggedItem;
    private Item weap;
    private int prevIndex;

    public float monies = 500;
    public float barter = 2;

    private Rect windRect = new Rect(220, 300, 291, 280);

    void Start()
    {
        for (int i = 0; i < (slotsX * slotsY); i++)
        {
            slots.Add(new Item());
            inventory.Add(new Item());
        }

        database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
        charMen = GetComponent<CharacterMenu>();
        combat = GetComponent<PlayerCombat>();
        AddItem(5);
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            showInventory = !showInventory;
        }
    }

    void OnGUI()
    {
        tooltip = "";
        GUI.skin = skin;

        if (showInventory)
        {
            windRect = GUI.Window(0, windRect, invWind, " Inventory    \t       Monies: " + monies, skin.GetStyle("BackGround"));
        }

        if (showTooltip)
        {
            GUI.Window(1, new Rect(Event.current.mousePosition.x + 10f, Event.current.mousePosition.y, 200, 200), toolWind, "", skin.GetStyle("Slot"));
        }

        if (tooltip == "")
        {
            showTooltip = false;
        }
    }

    void toolWind(int windID)
    {
        GUI.Label(new Rect(10, 10, 180, 180), tooltip);
    }

    void invWind(int windID)
    {
        Event e = Event.current;
        int i = 0;
        for (float y = 0; y < slotsY; y++)
        {
            for (float x = 0; x < slotsX; x++)
            {
                Rect slotRect = new Rect(x * 60, (y * 60)+40, 50, 50);
                if(GUI.Button(slotRect, "", skin.GetStyle("Slot")))
                {
                    string type = inventory[i].itemType.ToString();

                    if(shopWindOpen && InventoryContains(inventory[i].itemID))
                    {
                        monies += (inventory[i].sellValue)/barter;
                        RemoveItem(inventory[i].itemID,i);
                    }
                    else if(type == "Weapon")
                    {
                        combat.weapon = inventory[i];
                        combat.SpawnWeapon();
                        charMen.weapon = inventory[i];
                        charMen.AddItem(inventory[i].itemID);
                        RemoveItem(inventory[i].itemID,i);
                    }
                    else if(type == "Consumable")
                    {
                        
                    }
                }
                slots[i] = inventory[i];

                if (slots[i].itemName != null)
                {
                    GUI.DrawTexture(slotRect, slots[i].itemIcon);
                    if (slotRect.Contains(e.mousePosition))
                    {
                        tooltip = CreateTooltip(slots[i]);
                        showTooltip = true;
                        if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)
                        {
                            draggingItem = true;
                            prevIndex = i;
                            draggedItem = slots[i];
                            inventory[i] = new Item();
                        }
                        if (e.type == EventType.mouseUp && draggingItem)
                        {
                            inventory[prevIndex] = inventory[i];
                            inventory[i] = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                        }
                    }
                }
                else
                {
                    if (slotRect.Contains(e.mousePosition))
                    {
                        if (e.type == EventType.mouseUp && draggingItem)
                        {
                            inventory[i] = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                        }
                    }
                }

                i++;
            }
        }

        if (draggingItem)
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x - 15f, Event.current.mousePosition.y, 50, 50), draggedItem.itemIcon);
        else
            GUI.DragWindow();

    }

    string CreateTooltip(Item item)
    {
        string type = item.itemType.ToString();

        if (type == "Weapon")
        {
            tooltip = "<color=#CC2D65>  " + item.itemName + "</color>\n\n" + "<color=#749091>  "
            + item.itemDesc + "</color>" + "\n\n  Item Power: " + item.itemPower + "\n\n  Sell Value: " + item.sellValue + "$";
        }
        else if (type == "Consumable")
        {
            tooltip = "<color=#CC2D65>  " + item.itemName + "</color>\n\n" + "<color=#749091>  "
            + item.itemDesc + "</color>" + "\n\n  Item Power: " + item.itemPower + "\n\n  Sell Value: " + item.sellValue + "$";
        }
        else if (type == "Armor")
        {
            tooltip = "<color=#CC2D65>  " + item.itemName + "</color>\n\n" + "<color=#749091>  "
            + item.itemDesc + "</color>" + "\n\n  Item Power: " + item.itemPower + "\n\n  Sell Value: " + item.sellValue + "$";
        }
        else if (type == "Resource")
        {
            tooltip = "<color=#CC2D65>  " + item.itemName + "</color>\n\n" + "<color=#749091>  "
            + item.itemDesc + "</color>" + "\n\n  Sell Value: " + item.sellValue + "$";
        }

        return tooltip;
    }

    void RemoveItem(int id, int start)
    {
        //When removing items, make sure the search starts at that specific item or it will loop through the entire inventory. 
        for (int i = start; i < inventory.Count; i++)
        {
            if (inventory[i].itemID == id)
            {
                inventory[i] = new Item();
                break;
            }
        }
    }

    public void AddItem(int id)
    {
        //Add item -> make sure there is an open spot to add the item, search through the database, if the given id matches the 
        //database ID, add that item into the user's inventory. 
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemName == null)
            {
                for (int j = 0; j < database.items.Count; j++)
                {
                    if (database.items[j].itemID == id)
                    {
                        inventory[i] = database.items[j];
                    }
                }
                break;
            }
        }
    }

    bool InventoryContains(int id)
    {
        foreach (Item item in inventory)
        {
            if (item.itemID == id) return true;
        }
        return false;
    }
}
