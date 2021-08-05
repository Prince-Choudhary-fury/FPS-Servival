using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class previewObjectController : MonoBehaviour
{

    public bool isBuildable;

    public List<GameObject> col = new List<GameObject>();
    public ObjectList type;
    public Material green;
    public Material red;

    public string SnapToTag;

    [SerializeField]
    private GameObject[] meshHolders;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == Tags.Buildable)
        {
            col.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.tag == Tags.Buildable)
        {
            col.Remove(other.gameObject);
        }
    }

    private void Update()
    {
        changeColor();
    }

    public void changeColor()
    {
        if (type == ObjectList.preview)
        {
            if (col.Count == 0)
            {
                isBuildable = true;
            }
            else
            {
                isBuildable = false;
            }
        }
        if (isBuildable)
        {
            for (int i = 0; i < meshHolders.Length; i++)
            {
                meshHolders[i].GetComponent<MeshRenderer>().material = green;
            }
        }
        else
        {
            for (int i = 0; i < meshHolders.Length; i++)
            {
                meshHolders[i].GetComponent<MeshRenderer>().material = green;
            }
        }
    }

    public enum ObjectList
    {
        normal,
        preview,
        Build
    }

}
