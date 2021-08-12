using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{

    private void Start()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (GetComponent<slot>().empty)
        {
            //print("Enter drop");
            drag currentDrag = eventData.pointerDrag.GetComponent<drag>();
            if (currentDrag != null)
            {
                currentDrag.currentDraggingObj.GetComponent<CanvasGroup>().blocksRaycasts = true;
                currentDrag.currentDraggingObj.transform.SetParent(currentDrag.objParent.transform);
                currentDrag.currentDraggingObj.transform.position = currentDrag.objParent.transform.position;
                //print("RePositioned the holded element");

                if (this.gameObject.tag == Tags.Equipable)
                {
                    //print("Equipable Zone");
                    if (currentDrag.objParent.GetComponent<slot>().type == this.GetComponent<PlayerEquip>().AllowedType)
                    {

                        //swaping slot data
                        SwipeData(currentDrag);
                        //print("Equipped");

                        if (currentDrag.objParent.tag == Tags.Equipable)
                        {
                            currentDrag.currentDraggingObj.GetComponent<Image>().sprite = currentDrag.defaultSprite;
                            currentDrag.currentDraggingObj.GetComponent<Image>().color = new Color32(0, 0, 0, 100);
                        }
                        ResetData(currentDrag);
                    }
                }
                else
                {
                    //swaping slot data
                    SwipeData(currentDrag);

                    ResetData(currentDrag);
                }
            }
        }
        else
        {
            //print("Enter drop");
            drag currentDrag = eventData.pointerDrag.GetComponent<drag>();
            if (currentDrag != null)
            {
                currentDrag.currentDraggingObj.GetComponent<CanvasGroup>().blocksRaycasts = true;
                currentDrag.currentDraggingObj.transform.SetParent(currentDrag.objParent.transform);
                currentDrag.currentDraggingObj.transform.position = currentDrag.objParent.transform.position;
                //print("RePositioned the holded element");

                if (this.gameObject.tag == Tags.Equipable)
                {
                    //print("Equipable Zone");
                    if (currentDrag.objParent.GetComponent<slot>().type == this.GetComponent<PlayerEquip>().AllowedType)
                    {

                        //swaping slot data
                        SwipeAll(currentDrag);
                    }
                }
                else
                {
                    //swaping slot data
                    SwipeAll(currentDrag);
                }
            }
        }
    }

    private void SwipeAll(drag currentDrag)
    {

        //storing the current store in temp
        Sprite tempSprite = this.transform.GetChild(0).GetComponent<Image>().sprite;
        Transform tempSlotIcon = this.gameObject.GetComponent<slot>().slotIconGO;
        int tempID = this.gameObject.GetComponent<slot>().id;
        string tempType = this.gameObject.GetComponent<slot>().type;
        int tempQty = this.gameObject.GetComponent<slot>().qty;
        string tempDescription = this.gameObject.GetComponent<slot>().description;
        Sprite tempIcon = this.gameObject.GetComponent<slot>().icon;

        //***************************************************************************************************************************//

        //print(currentDrag.objParent.name);

        //for slot on which the item is dragged
        this.transform.GetChild(0).GetComponent<Image>().sprite = currentDrag.currentDraggingObj.GetComponent<Image>().sprite;
        setThisObject(currentDrag.objParent.GetComponent<slot>().slotIconGO, currentDrag.objParent.GetComponent<slot>().id, currentDrag.objParent.GetComponent<slot>().type, currentDrag.objParent.GetComponent<slot>().description, currentDrag.objParent.GetComponent<slot>().icon, currentDrag.objParent.GetComponent<slot>().qty);
        
        //***************************************************************************************************************************//
        
        //for slot from which item was dragged

        currentDrag.objParent.transform.GetChild(0).SetParent(this.transform);
        currentDrag.currentDraggingObj.GetComponent<Image>().sprite = tempSprite;
        currentDragValueSet(currentDrag, tempSlotIcon, tempID, tempType, tempDescription, tempIcon, tempQty);

    }

    public void ResetData(drag currentDrag)
    {
        //reseting previous slot data

        currentDrag.objParent.GetComponent<Drop>().enabled = true;

        currentDrag.currentDraggingObj.GetComponent<drag>().enabled = false;
        currentDrag.currentDraggingObj.GetComponent<Image>().sprite = currentDrag.defaultSprite;
        if (currentDrag.objParent.tag == Tags.Equipable)
        {
            currentDrag.currentDraggingObj.GetComponent<Image>().color = new Color32(0, 0, 0, 100);
        }
        else
        {
            currentDrag.currentDraggingObj.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }
        //print("Sprite Reseted to default of previous slot");

        Transform transform = currentDrag.objParent.transform.GetChild(0).transform;
        currentDragValueSet(currentDrag, transform, 0, null, null, null, 0);
        currentDrag.objParent.GetComponent<slot>().empty = true;

        currentDrag.currentDraggingObj.GetComponent<drag>().enabled = false;
        currentDrag.currentDraggingObj.GetComponent<drag>().currentDraggingObj = null;
    }

    public void SwipeData(drag currentDrag)
    {
        this.transform.GetChild(0).GetComponent<Image>().sprite = currentDrag.currentDraggingObj.GetComponent<Image>().sprite;
        this.transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        this.transform.GetChild(0).GetComponent<drag>().enabled = true;

        //print("Sprite transfered to droped slots and draging enableded there");
        setThisObject(currentDrag.objParent.GetComponent<slot>().slotIconGO, currentDrag.objParent.GetComponent<slot>().id, currentDrag.objParent.GetComponent<slot>().type, currentDrag.objParent.GetComponent<slot>().description, currentDrag.objParent.GetComponent<slot>().icon, currentDrag.objParent.GetComponent<slot>().qty);
        this.gameObject.GetComponent<slot>().empty = false;
    }

    private void currentDragValueSet(drag currentDrag, Transform transform, int ID, string Type, string Description, Sprite Icon, int qty)
    {
        currentDrag.objParent.GetComponent<slot>().slotIconGO = transform;
        currentDrag.objParent.GetComponent<slot>().id = ID;
        currentDrag.objParent.GetComponent<slot>().type = Type;
        //print(qty);
        currentDrag.objParent.GetComponent<slot>().qty = qty;
        currentDrag.currentDraggingObj.transform.GetChild(0).GetComponent<Text>().text = null;
        currentDrag.objParent.GetComponent<slot>().description = Description;
        currentDrag.objParent.GetComponent<slot>().icon = Icon;
    }

    private void setThisObject(Transform transform, int ID, string Type, string Description, Sprite Icon, int qty)
    {
        this.gameObject.GetComponent<slot>().slotIconGO = transform;
        this.gameObject.GetComponent<slot>().id = ID;
        this.gameObject.GetComponent<slot>().type = Type;
        if (Type == ItemType.resources)
        {
            print(qty);
            this.gameObject.GetComponent<slot>().qty = qty;
            this.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "" + qty;
        }
        this.gameObject.GetComponent<slot>().description = Description;
        this.gameObject.GetComponent<slot>().icon = Icon;
    }

}
