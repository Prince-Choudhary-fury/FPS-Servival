using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBox : MonoBehaviour
{

    public GameObject loadingVisuals;
    public Image loadingBar;

    public GameObject playerPreviews;
    public GameObject inventryStroagePreview;

    public static bool enableInventry;
    private bool load = false;
    private bool isOpened = false;
    private bool allowedQ;
    private bool isQPressed;

    public GameObject buttonQ;

    private float fillSpeed = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            //print("pressed");
            playerPreviews.SetActive(true);
            inventryStroagePreview.SetActive(false);
        }
        if (allowedQ)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                isQPressed = true;
                buttonQ.SetActive(false);
                if (!isOpened)
                {
                    load = true;
                    loadingVisuals.SetActive(true);
                }
                else
                {
                    enableInventry = true;
                }
                allowedQ = false;
                playerPreviews.SetActive(false);
                inventryStroagePreview.SetActive(true);
            }
        }
        if (load)
        {
            loadingBar.fillAmount += fillSpeed * Time.deltaTime * Time.deltaTime;
            if (loadingBar.fillAmount == 1)
            {
                enableInventry = true;
                load = false;
                isOpened = true;
                loadingVisuals.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.PlayerTag)
        {
            if (!isQPressed)
            {
                buttonQ.SetActive(true);
                allowedQ = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.PlayerTag)
        {
            isQPressed = false;
            buttonQ.SetActive(false);
        }
    }

}
