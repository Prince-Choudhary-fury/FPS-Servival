using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choping : MonoBehaviour
{

    public float treeHealth = 100f;
    public int availableWood = 30;

    public WeaponManager playerWeaponManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(float damage)
    {
        if (treeHealth <= 0)
        {
            StartCoroutine(FallAnim());
        }
        else if (playerWeaponManager.currentWeaponIndex == 0)
        {
            treeHealth -= damage;
        }
    }

    IEnumerator FallAnim()
    {
        this.GetComponent<Animator>().Play(AnimationsName.treeFall);
        yield return new WaitForSeconds(4);
        this.GetComponent<Animator>().Play(AnimationsName.Empty);
        Destroy(this.gameObject);
    }

}
