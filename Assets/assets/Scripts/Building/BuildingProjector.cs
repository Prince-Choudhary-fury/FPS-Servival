using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingProjector : MonoBehaviour
{

    public List<Objects> objects = new List<Objects>();
    public Objects currentObject;

    private Vector3 currentPos;
    public Vector3 desiredRotation = new Vector3(0, 90, 0);

    public Vector3 currentRotation;

    public Transform currentPreview;

    [HideInInspector]
    public GameObject currentPrevObject;

    private GameObject buildings;

    public RaycastHit hit;

    public Transform cam;

    public LayerMask layerforFloar;
    public LayerMask layerforWall;
    public LayerMask layerforStears;
    public LayerMask layerforRoof;

    public float offset = 1f;
    public float gridSize = 1f;

    private int maxRaycastRange = 15;
    public int index;

    void Awake()
    {
        buildings = new GameObject("BuildingsHolder"); 
        currentObject = objects[0];
        ChangeCurrentObject(0);
        index = 0;
    }

    void Update()
    {
        //cast ray for floar
        if (index == 0)
        {
            castRayToProjectFloar();
        }
        //castRay ray for wall
        else if (index == 1)
        {
            castRayToProjectWall();
        }
        //cast ray for stears
        else if (index == 2)
        {
            castRayToProjectStears();
        }
        else if (index == 3)
        {
            castRayToProjectRoof();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            BuildObject();
        }
    }

    public void ChangeCurrentObject(int objIndex)
    {
        //creating the preview object
        currentObject = objects[objIndex];
        currentRotation = currentObject.previewObject.transform.eulerAngles;
        if (currentPreview != null)
        {
            Destroy(currentPreview.gameObject);
        }
        currentPrevObject = Instantiate(currentObject.previewObject, currentPos, Quaternion.Euler(currentRotation)) as GameObject;
        currentPreview = currentPrevObject.transform;
    }

    public void castRayToProjectFloar()
    {
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxRaycastRange, layerforFloar))
        {
            castRay();
        }
    }
    
    public void castRayToProjectStears()
    {
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxRaycastRange, layerforStears))
        {
            castRay();
        }
    }

    public void castRayToProjectWall()
    {
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxRaycastRange, layerforWall))
        {
            castRay();
        }
    }
    
    public void castRayToProjectRoof()
    {
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxRaycastRange, layerforRoof))
        {
            castRay();
        }
    }

    private void castRay()
    {
        //cast Ray for different object
        if (hit.transform != this.transform)
        {
            if (hit.transform.tag == currentObject.previewObject.transform.GetChild(0).transform.GetComponent<previewObjectController>().SnapToTag)
            {
                SnapOnTrigger();
            }
            else
            {
                NormalPreveiwLocation();
            }
        }
    }

    private void NormalPreveiwLocation()
    {
        //normal ie, on ground initializing of preview obj position
        currentPos = hit.point;
        currentPos -= Vector3.one * offset;
        currentPos /= gridSize;
        currentPos = new Vector3(Mathf.Round(currentPos.x), Mathf.Round(currentPos.y), Mathf.Round(currentPos.z));
        currentPos *= gridSize;
        currentPos += Vector3.one * offset;
        currentPreview.position = currentPos;
        DefaultRotator();
    }

    private void SnapOnTrigger()
    {
        //snapping preview obbjects to hited snap point
        currentPos = hit.transform.position;
        currentPreview.position = currentPos;
        if (index != 2)
        {
            currentRotation = hit.transform.eulerAngles;
            currentPreview.localEulerAngles = currentRotation;
        }
        else
        {
            DefaultRotator();
        }
    }

    public void DefaultRotator()
    {
        //rotates preview object by 90 
        if (Input.GetButtonDown(ButtonTags.mouse1))
        {
            currentRotation += desiredRotation;
        }
        currentPreview.localEulerAngles = currentRotation;
    }

    private void BuildObject()
    {
        //instantiate objects on place of preview objects
        Instantiate(currentObject.buildObject, currentPos, Quaternion.Euler(currentRotation), buildings.transform);
    }

}
