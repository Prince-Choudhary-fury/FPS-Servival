using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slot : MonoBehaviour
{
    //public GameObject Item;
    public Transform slotIconGO;
    public int id;
    public string type;
    public string itemName;
    public int qty;
    public string description;
    public bool empty;
    public Sprite icon;

    private void Start()
    {
        slotIconGO = transform.GetChild(0).transform;
        GetComponent<slot>().empty = true;
    }

    public void UpdateSlot()
    {
        slotIconGO.GetComponent<Image>().sprite = icon;
        slotIconGO.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

}
