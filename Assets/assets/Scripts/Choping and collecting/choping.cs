using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choping : MonoBehaviour
{

    public float treeHealth = 100f;
    public int availableWood = 30;

    public GameObject woodPrefeb;

    [SerializeField]
    private GameObject woodParent;

    private GameObject wood;

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
        yield return new WaitForSeconds(2);
        this.GetComponent<Animator>().Play(AnimationsName.FallenTree);
        Vector3 pos = this.transform.position + new Vector3(0, 0.5f, 0);
        wood = Instantiate(woodPrefeb, pos, Quaternion.identity, woodParent.transform);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

}
