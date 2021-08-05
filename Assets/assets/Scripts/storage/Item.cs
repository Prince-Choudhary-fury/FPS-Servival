using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public int id;
    public string type;
    public string description;
    public Sprite icon;
    public bool pickedUp;
    public bool equiped;

    public void Update()
    {
        if (equiped)
        {
            //perform weapon acts here
        }
    }

    public void ItemUsage()
    {
        //weapon

        if (type == ItemType.Weapon)
        {
            equiped = true;
        }
    }

}
