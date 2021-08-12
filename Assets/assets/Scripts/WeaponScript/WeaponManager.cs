using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [SerializeField]
    private WeaponHandler[] weapon;

    public int currentWeaponIndex;

    public static bool weaponLoaded = false;

    // Start is called before the first frame update
    void Start()
    {
        currentWeaponIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            weapon[currentWeaponIndex].gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            weapon[currentWeaponIndex].gameObject.SetActive(true);
        }
        else
        {
            if (CurrentWeapon.currentActiveWeapon > 0)
            {
                weaponLoader(CurrentWeapon.currentActiveWeapon);
            }
            else if (CurrentWeapon.currentActiveWeapon == 0)
            {
                weapon[currentWeaponIndex].gameObject.SetActive(false);
            }
        }
    }

    public void weaponLoader(int index)
    {
        //print(currentWeaponIndex);
        //print(index);
        if (index > 0)
        {
            if (weaponLoaded && currentWeaponIndex == index - 1)
            {
                return;
            }
            weapon[currentWeaponIndex].gameObject.SetActive(false);
            currentWeaponIndex = index - 1;
            weapon[currentWeaponIndex].gameObject.SetActive(true);
            weaponLoaded = true;
        }
        else
        {
            weapon[currentWeaponIndex].gameObject.SetActive(false);
        }

    }

    public WeaponHandler GetCurrentSelectedWeapon()
    {
        return weapon[currentWeaponIndex];
    }

}
