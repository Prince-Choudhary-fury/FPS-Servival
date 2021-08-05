using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentWeapon : MonoBehaviour
{

    public static int currentActiveWeapon;

    // Update is called once per frame
    void Update()
    {
        currentActiveWeapon = GetComponent<slot>().id;
    }
}
