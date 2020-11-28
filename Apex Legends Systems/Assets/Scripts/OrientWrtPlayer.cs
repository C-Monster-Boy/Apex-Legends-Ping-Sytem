using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class OrientWrtPlayer : MonoBehaviour
{
    public GameObject uiBuddy;
    public float constant;
    public TextMeshPro distText;

    private GameObject uiInstance;
    private bool onScreen;
    private GameObject player;
    private Vector3 baseLocalScale;
    private bool displayUI;
    private Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        onScreen = false;
        cam = Camera.main;
        baseLocalScale = Vector3.one;
        displayUI = false;

        uiInstance = Instantiate(uiBuddy) as GameObject;
        uiInstance.SetActive(false);
        uiInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);
        try
        {
            uiInstance.GetComponent<ObjectPosToUI>().pingToFollow = gameObject;
        }
        catch
        {

        }
       
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toFollow = transform.position - player.transform.position;
        Vector3 currForward = player.transform.forward;
        toFollow.y = 0;
        currForward.y = 0;
        float angle = Vector3.SignedAngle(currForward, toFollow, player.transform.up);

        if(angle >= -cam.fieldOfView/2 && angle <= cam.fieldOfView / 2)
        {
            onScreen = true;
        }
        else
        {
            onScreen = false;
        }

        if (onScreen)
        {
            if(displayUI)
            {
                if (uiInstance)
                {
                    uiInstance.SetActive(false);
                }
                displayUI = false; 
            }
        }
        else
        {
            if (!displayUI)
            {
                if (uiInstance)
                {
                    uiInstance.SetActive(true);
                }

                displayUI = true;
            }
        }

        Vector3 v3 = player.transform.position - transform.position;
        v3.y = 0.0f;
        transform.rotation = Quaternion.LookRotation(-v3);

        float dist = v3.magnitude;
        distText.text = (int)(dist) + "m";

        Vector3 adder = (Vector3.one * dist) / constant;
        transform.localScale = Vector3.one + adder;
       
    }

    public void DestroyUI()
    {
        if(uiInstance)
        {
            Destroy(uiInstance);
        }
    }
}