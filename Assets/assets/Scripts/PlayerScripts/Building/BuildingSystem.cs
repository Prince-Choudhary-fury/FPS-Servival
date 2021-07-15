using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{

    public List<BuildObjects> objects = new List<BuildObjects>();

    public BuildObjects currentObject;

    private Vector3 currentPos;

    public Transform currentPreview;
    public Transform cam;

    public RaycastHit hit;

    public LayerMask layer;

    public float offset = 1f;
    public float gridSize = 1f;

    public bool isBuilding;

    private void Start()
    {
        currentObject = objects[0];
        ChangeCurrentObject();
    }

    private void Update()
    {
        if (isBuilding)
        {
            StartPreview();
        }
    }

    public void ChangeCurrentObject()
    {
        Debug.Log("Enter");
        GameObject currentPrevObject = Instantiate(currentObject.preview, currentPos, Quaternion.identity) as GameObject;
        currentPreview = currentPrevObject.transform;
    }

    public void StartPreview()
    {
        if (Physics.Raycast(cam.position, cam.forward, out hit, 10, layer))
        {
            if (hit.transform != this.transform)
            {
                ShowPreview(hit);
            }
        }
    }

    public void ShowPreview(RaycastHit hit)
    {
        currentPos = hit.point;
        currentPos -= Vector3.one * offset;
        currentPos /= gridSize;
        currentPos = new Vector3(Mathf.Round(currentPos.x), Mathf.Round(currentPos.y), Mathf.Round(currentPos.z));
        currentPos *= gridSize;
        currentPos += Vector3.one * offset;
        currentPreview.position = currentPos;
    }

}


