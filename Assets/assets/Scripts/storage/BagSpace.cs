using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagSpace : MonoBehaviour
{

    public GameObject holder;

    //private int startIndex = 10;

    private int allowedSpace;

    public static int currnetBagId;

    private GameObject[] slot;

    private int NoBag = 10;
    private int levelOneBag = 15;
    private int levelTwoBag = 25;
    private int levelThreeBag = 40;

    // Start is called before the first frame update
    void Start()
    {
        allowedSpace = NoBag; 
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < allowedSpace; i++)
        {
            holder.transform.GetChild(i).gameObject.SetActive(true);
        }
        for (int i = allowedSpace; i < holder.transform.childCount; i++)
        {
            holder.transform.GetChild(i).gameObject.SetActive(false);
        }
        if (this.GetComponent<slot>().type == ItemType.bag)
        {
            if (this.GetComponent<slot>().id == ItemType.bagLevelOne)
            {
                allowedSpace = levelOneBag;
            }
            else if(this.GetComponent<slot>().id == ItemType.bagLevelTwo)
            {
                allowedSpace = levelTwoBag;
            }
            else if(this.GetComponent<slot>().id == ItemType.bagLevelThree)
            {
                allowedSpace = levelThreeBag;
            }
        }
        else if(this.GetComponent<slot>().type == null)
        {
            allowedSpace = NoBag;
        }
    }
}
