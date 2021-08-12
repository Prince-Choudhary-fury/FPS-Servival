using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{

    public static bool inventoryEnabled = false;

    public GameObject inventory;
    public GameObject UICanvas;

    private int allSlots;
    private int enabledSlots = 10;
    private GameObject[] slots;
    private GameObject ItemPickedUp;

    private int maxResourcesAllowed = 20;

    public GameObject slotHolder;

    private void Start()
    {
        allSlots = enabledSlots;

        slots = new GameObject[allSlots];

        for (int i = 0; i < allSlots; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
            if(slots[i].GetComponent<slot>().type == null)
            {
                slots[i].GetComponent<Drop>().enabled = true;
            }
        }
    }

    void Update()
    {
        if (InventoryBox.enableInventry)
        {
            inventoryEnabled = true;
            InventoryBox.enableInventry = false;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryEnabled = !inventoryEnabled;
        }
        if (inventoryEnabled)
        {
            UICanvas.SetActive(false);
            inventoryActiveator(true, false);
        }
        else
        {
            UICanvas.SetActive(true);
            inventoryActiveator(false, true);
        }
    }

    private void inventoryActiveator(bool value, bool value1)
    {
        this.GetComponent<CharacterController>().enabled = value1;
        this.GetComponent<PlayerMovement>().enabled = value1;
        this.GetComponent<WeaponManager>().enabled = value1;
        this.GetComponent<PlayerAttack>().enabled = value1;
        inventory.SetActive(value);
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("Trigger");
        if (other.tag == "Item" )
        {
            ItemPickedUp = other.gameObject;
            Item item = ItemPickedUp.GetComponent<Item>();
            AddItem(ItemPickedUp, item.id, item.type, item.description, item.icon, item.qty);
            Destroy(other.gameObject);
        }
    }

    void AddItem(GameObject itemObject, int itemID, string itemType, string itemDiscription, Sprite itemIcon, int qty)
    {
        for (int i = 0; i < allSlots; i++)
        {
            if(itemObject.GetComponent<Item>().pickedUp)
            {
                return;
            }
            else if(slots[i].GetComponent<slot>().empty)
            {
                slots[i].transform.GetChild(0).transform.GetComponent<drag>().enabled = true;
                //add item to slot
                itemObject.GetComponent<Item>().pickedUp = true;
                slots[i].GetComponent<slot>().icon = itemIcon;
                slots[i].GetComponent<slot>().type = itemType;
                slots[i].GetComponent<slot>().id = itemID;
                slots[i].GetComponent<slot>().description = itemDiscription;
                if (itemType == ItemType.resources)
                {
                    print(slots[i].GetComponent<slot>().qty);
                    int tempQty = slots[i].GetComponent<slot>().qty;
                    slots[i].GetComponent<slot>().qty = tempQty + qty;
                    slots[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "" + slots[i].GetComponent<slot>().qty;
                    //slots[i].GetComponent<slot>().qty = qty;
                }
                itemObject.transform.SetParent(slots[i].transform);
                itemObject.SetActive(false);

                slots[i].GetComponent<slot>().UpdateSlot();
                if (itemType == ItemType.resources)
                {
                    if (slots[i].GetComponent<slot>().qty == maxResourcesAllowed)
                    {
                        slots[i].GetComponent<slot>().empty = false;
                    }
                    else
                    {
                        slots[i].GetComponent<slot>().empty = true;
                    }
                }
                else
                {
                    slots[i].GetComponent<slot>().empty = false;
                }
            }
        }
    }

}
