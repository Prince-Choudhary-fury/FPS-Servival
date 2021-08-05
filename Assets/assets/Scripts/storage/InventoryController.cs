using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    private bool inventoryEnabled;

    public GameObject inventory;
    public GameObject crossHairCanvas;
    public GameObject UICanvas;

    private int allSlots;
    private int enabledSlots = 10;
    private GameObject[] slots;
    private GameObject ItemPickedUp;

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
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryEnabled = !inventoryEnabled;
        }
        if (inventoryEnabled)
        {
            crossHairCanvas.SetActive(false);
            UICanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            inventoryActiveator(true, false);
        }
        else
        {
            crossHairCanvas.SetActive(true);
            UICanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            inventoryActiveator(false, true);
        }
    }

    private void inventoryActiveator(bool value, bool value1)
    {
        Cursor.visible = value;
        this.GetComponent<CharacterController>().enabled = value1;
        this.GetComponent<PlayerMovement>().enabled = value1;
        this.GetComponent<WeaponManager>().enabled = value1;
        this.GetComponent<PlayerAttack>().enabled = value1;
        inventory.SetActive(value);
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("Trigger");
        if (other.tag == "Item")
        {
            ItemPickedUp = other.gameObject;
            Item item = ItemPickedUp.GetComponent<Item>();
            AddItem(ItemPickedUp, item.id, item.type, item.description, item.icon);
            Destroy(other.gameObject);
        }
    }

    void AddItem(GameObject itemObject, int itemID, string itemType, string itemDiscription, Sprite itemIcon)
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
                //slots[i].GetComponent<slot>().Item = itemObject;
                slots[i].GetComponent<slot>().icon = itemIcon;
                slots[i].GetComponent<slot>().type = itemType;
                slots[i].GetComponent<slot>().id = itemID;
                slots[i].GetComponent<slot>().description = itemDiscription;

                itemObject.transform.SetParent(slots[i].transform);
                itemObject.SetActive(false);

                slots[i].GetComponent<slot>().UpdateSlot();
                slots[i].GetComponent<slot>().empty = false;
                //slots[i].GetComponent<Drop>().enabled = false;
            }
        }
    }

}
