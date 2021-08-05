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
        //GameObject tempItem = this.gameObject.GetComponent<slot>().Item;
        Transform tempSlotIcon = this.gameObject.GetComponent<slot>().slotIconGO;
        int tempID = this.gameObject.GetComponent<slot>().id;
        string tempType = this.gameObject.GetComponent<slot>().type;
        string tempDescription = this.gameObject.GetComponent<slot>().description;
        Sprite tempIcon = this.gameObject.GetComponent<slot>().icon;

        //***************************************************************************************************************************//

        //print(currentDrag.objParent.name);

        //for slot on which the item is dragged
        //this.transform.GetChild(1).SetParent(currentDrag.objParent.transform);
        this.transform.GetChild(0).GetComponent<Image>().sprite = currentDrag.currentDraggingObj.GetComponent<Image>().sprite;

        //this.gameObject.GetComponent<slot>().Item = currentDrag.objParent.GetComponent<slot>().Item;
        this.gameObject.GetComponent<slot>().slotIconGO = currentDrag.objParent.GetComponent<slot>().slotIconGO;
        this.gameObject.GetComponent<slot>().id = currentDrag.objParent.GetComponent<slot>().id;
        this.gameObject.GetComponent<slot>().type = currentDrag.objParent.GetComponent<slot>().type;
        this.gameObject.GetComponent<slot>().description = currentDrag.objParent.GetComponent<slot>().description;
        this.gameObject.GetComponent<slot>().icon = currentDrag.objParent.GetComponent<slot>().icon;
        
        //***************************************************************************************************************************//
        
        //for slot from which item was dragged

        currentDrag.objParent.transform.GetChild(0).SetParent(this.transform);
        currentDrag.currentDraggingObj.GetComponent<Image>().sprite = tempSprite;

        //currentDrag.objParent.GetComponent<slot>().Item = tempItem;
        currentDrag.objParent.GetComponent<slot>().slotIconGO = tempSlotIcon;
        currentDrag.objParent.GetComponent<slot>().id = tempID;
        currentDrag.objParent.GetComponent<slot>().type = tempType;
        currentDrag.objParent.GetComponent<slot>().description = tempDescription;
        currentDrag.objParent.GetComponent<slot>().icon = tempIcon;
        //print("Slot sets to its default (NULL) value");

    }

    public void ResetData(drag currentDrag)
    {
        //reseting previous slot data

        currentDrag.objParent.GetComponent<Drop>().enabled = true;

        //currentDrag.objParent.transform.GetChild(0).SetParent(this.transform);
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

        //currentDrag.objParent.GetComponent<slot>().Item = null;
        currentDrag.objParent.GetComponent<slot>().slotIconGO = currentDrag.objParent.transform.GetChild(0).transform;
        currentDrag.objParent.GetComponent<slot>().id = 0;
        currentDrag.objParent.GetComponent<slot>().type = null;
        currentDrag.objParent.GetComponent<slot>().description = null;
        currentDrag.objParent.GetComponent<slot>().empty = true;
        currentDrag.objParent.GetComponent<slot>().icon = null;
        //print("Slot sets to its default (NULL) value");

        currentDrag.currentDraggingObj.GetComponent<drag>().enabled = false;
        currentDrag.currentDraggingObj.GetComponent<drag>().currentDraggingObj = null;
    }

    public void SwipeData(drag currentDrag)
    {
        this.transform.GetChild(0).GetComponent<Image>().sprite = currentDrag.currentDraggingObj.GetComponent<Image>().sprite;
        this.transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        this.transform.GetChild(0).GetComponent<drag>().enabled = true;
        //print("Sprite transfered to droped slots and draging enableded there");

        //this.gameObject.GetComponent<slot>().Item = currentDrag.objParent.GetComponent<slot>().Item;
        this.gameObject.GetComponent<slot>().slotIconGO = currentDrag.objParent.GetComponent<slot>().slotIconGO;
        this.gameObject.GetComponent<slot>().id = currentDrag.objParent.GetComponent<slot>().id;
        this.gameObject.GetComponent<slot>().type = currentDrag.objParent.GetComponent<slot>().type;
        this.gameObject.GetComponent<slot>().description = currentDrag.objParent.GetComponent<slot>().description;
        this.gameObject.GetComponent<slot>().empty = false;
        this.gameObject.GetComponent<slot>().icon = currentDrag.objParent.GetComponent<slot>().icon;
    }
}
