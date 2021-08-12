using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{

    public float damage = 2;
    public float radius = 1f;

    public LayerMask layerMask;
    public LayerMask layerMaskObj;

    public AudioSource treeChoping;

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);
        
        if (hits.Length > 0)
        {

            hits[0].gameObject.GetComponent<HealthScript>().ApplyDamage(damage);

            gameObject.SetActive(false);
        }

        Collider[] hitObj = Physics.OverlapSphere(transform.position, radius, layerMaskObj);

        if(hitObj.Length > 0)
        {
            if(hitObj[0].tag == Tags.Tree)
            {
                treeChoping.Play();
                hitObj[0].gameObject.GetComponent<choping>().ApplyDamage(damage);
            }
            gameObject.SetActive(false);
        }

    }
}
