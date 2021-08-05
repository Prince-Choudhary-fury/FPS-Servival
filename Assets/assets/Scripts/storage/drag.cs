using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public GameObject Inventory;
    public Sprite defaultSprite;
    public static bool isDraging = false;

    public GameObject objParent = null;
    public GameObject currentDraggingObj = null;

    private void Start()
    {
        defaultSprite = GetComponent<Image>().sprite;
        this.gameObject.GetComponent<drag>().objParent = this.transform.parent.gameObject;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //print("dragging");
        isDraging = true;
        //this.transform.parent.GetComponent<Drop>().enabled = false;
        currentDraggingObj = this.gameObject;
        this.GetComponent<CanvasGroup>().blocksRaycasts = false;
        this.transform.SetParent(Inventory.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //print("H");
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        this.transform.position = objParent.transform.position;
        this.transform.SetParent(objParent.transform);
        isDraging = false;
        //this.transform.parent.GetComponent<Drop>().enabled = true;
    }
}
