using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.UI;

public class UIButtonManager : MonoBehaviour
{

    public GameObject player;

    public GameObject buildingPlan;

    public GameObject buildMenu;
    public GameObject craftPanal;

    [SerializeField]
    private GameObject UICanves;

    public BuildingProjector buildingProjector;

    [SerializeField]
    private TextMeshProUGUI objTitleName;

    public static bool isBuildOpened;
    private int currentObjIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isBuildOpened = true;
            UICanves.SetActive(false);
            buildingPlan.SetActive(true);
            Destroy(buildingProjector.currentPrevObject);
            player.GetComponent<BuildingProjector>().enabled = false;
            buildMenu.SetActive(true);
            pauseGame(false);
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            UICanves.SetActive(true);
            buildingPlan.SetActive(false);
            Destroy(buildingProjector.currentPrevObject);
            player.GetComponent<BuildingProjector>().enabled = false;
            buildMenu.SetActive(false);
            pauseGame(true);
        }
    }

    public void buttonPressed(int Index, string objName)
    {
        objTitleName.text = objName;
        currentObjIndex = Index - 1;
        craftPanal.SetActive(true);
    }

    public void craftButton()
    {
        isBuildOpened = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseGame(true);
        //isBPressed = false;
        buildMenu.GetComponent<Animator>().Play(AnimationsName.inverseAnim);
        Invoke("StartGame", 2);
    }


    public void pauseGame(bool value)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.EnemyTag);
        player.GetComponent<PlayerMovement>().enabled = value;
        player.GetComponent<PlayerAttack>().enabled = value;
        player.GetComponent<CharacterController>().enabled = value;
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyController>().enabled = value;
        }
    }

    public void StartGame()
    {
        buildMenu.SetActive(false);
        craftPanal.SetActive(false);
        player.GetComponent<BuildingProjector>().enabled = true;
        buildingProjector.index = currentObjIndex;
        buildingProjector.ChangeCurrentObject(currentObjIndex);
    }
}
