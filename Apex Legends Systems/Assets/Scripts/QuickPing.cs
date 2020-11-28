using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickPing : MonoBehaviour
{
    public static GameObject currPing = null; // 0, 1
    public static GameObject enemyBeenHerePing = null; // 7
    public static GameObject goingHerePing = null; // 4
    public static GameObject attackingHerePing = null; // 5
    public static GameObject defendingHerePing = null; // 3
    public static GameObject lookingHerePing = null; // 6
    public static GameObject lootingHerePing = null; // 2

    public AudioClip[] pingSounds; //0-Enemy  1-Normal 2-Object 3-PingCancel

    public GameObject enemyPingGraphic;
    public GameObject pingGraphic;
    public PingMenu pingSelectorMenu;
    public AudioSource audioSource;
    public AudioSource voice;

    public LayerMask whatIsPingable;

    public GameObject[] pingTypes;

    public float doubleClickTime;
    public float holdDuration;

    private Camera cam;
    private RaycastHit hit;
    [SerializeField]
    private float currDoubleClickTime;
    private float currHoldDuration;
    private bool btnPressedOnce;
    private bool inPingMenu;
    private GameObject objectPinged;

    
    private PlayerLook playerLook;

    // Start is called before the first frame update
    void Start()
    {
        inPingMenu = false;
        btnPressedOnce = false;
        objectPinged = null;
        currDoubleClickTime = -10;
        currHoldDuration = -10;
        cam = transform.GetChild(0).gameObject.GetComponent<Camera>();
        currPing = null;

        playerLook = GetComponent<PlayerLook>();
    }

    // Update is called once per frame
    void Update()
    {
        //Singe Press + Double Press
        if(Input.GetKeyDown(KeyCode.Z) && !inPingMenu)
        {
            Ray r = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            Physics.Raycast(r, out hit);
            currHoldDuration = holdDuration;
            playerLook.SetSens(0, 0);

            btnPressedOnce = !btnPressedOnce;

            if(hit.collider)
            {
                bool isInLayerMask = whatIsPingable == (whatIsPingable | 1 << hit.collider.gameObject.layer);
                
                if (isInLayerMask)
                {
                    if (hit.collider.gameObject.layer == 9)
                    {
                        btnPressedOnce = !btnPressedOnce;
                        ObjectSelect o = hit.collider.gameObject.GetComponent<ObjectSelect>();
                        if (!o.isSelected)
                        {
                            o.SelectToggle();
                            GameObject ping = Instantiate(pingGraphic, hit.point, Quaternion.identity) as GameObject;
                            ping.transform.position = hit.point;
                            ManagePingPresence(ref currPing, ping);
                            PlayAudioClip(2);
                            if(hit.collider.gameObject.tag == "Mozam")
                            {
                                voice.Play();
                            }

                            if(objectPinged)
                            {
                                objectPinged.GetComponent<ObjectSelect>().SelectToggle();
                                objectPinged = null;
                            }
                            objectPinged = hit.collider.gameObject;
                        }
                        else
                        {
                            o.SelectToggle();
                            PlayAudioClip(3);
                            if(objectPinged == hit.collider.gameObject)
                            {
                                try
                                {
                                    currPing.transform.GetChild(0).GetChild(0).GetComponent<DestroyPing>().DestroyPingObject();
                                }
                               catch
                                {

                                }
                                currPing = null;
                                objectPinged = null;
                            }
                        }
                        
                    }
                    else if (btnPressedOnce)
                    {
                        //btnPressedOnce = true;
                        currDoubleClickTime = doubleClickTime;
                        //objectPinged = null;
                    }
                    else
                    {
                        //btnPressedOnce = false;
                       
                        if (currPing)
                        {
                            currPing.transform.GetChild(0).GetChild(0).GetComponent<DestroyPing>().DestroyPingObject();
                            currPing = null;
                        }

                        PlacePing(enemyPingGraphic, hit.point, doubleClickTime);
                        PlayAudioClip(0);
                    }
                }
            }
           
           
            
        }

        //Key Hold
        if(Input.GetKey(KeyCode.Z))
        {
            currHoldDuration -= Time.deltaTime;

            if(btnPressedOnce && currHoldDuration <= 0 && currHoldDuration != -10)
            {
                inPingMenu = true;
                btnPressedOnce = false;
                currDoubleClickTime = -10;
                pingSelectorMenu.SetMenuState(true);
            }

        }

        //Key Up
        if(Input.GetKeyUp(KeyCode.Z))
        {
            if(inPingMenu)
            {
                pingSelectorMenu.SetMenuState(false);
                inPingMenu = false;
                currHoldDuration = -10;
            }
           
            playerLook.SetSens();
        }


        //Double press timer
        if(currDoubleClickTime <= 0 && currDoubleClickTime != -10)
        {
            if (btnPressedOnce && hit.collider)
            {
                if (currPing)
                {
                    currPing.transform.GetChild(0).GetChild(0).GetComponent<DestroyPing>().DestroyPingObject();
                    currPing = null;
                }

                PlacePing(pingGraphic, hit.point, -10f);
                PlayAudioClip(1);

                btnPressedOnce = false;

            }
           
            currDoubleClickTime = -10;

        }
        else if(currDoubleClickTime > 0 && currDoubleClickTime != -10)
        {
            currDoubleClickTime -= Time.deltaTime;
        }
    }


    public void PlacePing(GameObject pGra, Vector3 spawnPoint, float currTimeVal)
    {
        //spawnPoint.y += 0;

        GameObject ping = Instantiate(pGra, spawnPoint, Quaternion.identity) as GameObject;
        ping.transform.position = spawnPoint;

        currPing = ping;

        currDoubleClickTime = currTimeVal;
    }

    public void PlacePing(int index)
    {

        if (hit.collider)
        {
            GameObject ping = Instantiate(pingTypes[index], hit.point, Quaternion.identity) as GameObject;
            ping.transform.position = hit.point;
       
            switch (index)
            {
                case 0:
                    {
                        ManagePingPresence(ref currPing, ping);
                        PlayAudioClip(1);
                        break;
                    }
                case 1:
                    {
                        ManagePingPresence(ref currPing, ping);
                        PlayAudioClip(0);
                        break;
                    }
                case 2:
                    {
                        ManagePingPresence(ref lootingHerePing, ping);
                        PlayAudioClip(1);
                        break;
                    }
                case 3:
                    {
                        ManagePingPresence(ref defendingHerePing, ping);
                        PlayAudioClip(1);

                        break;
                    }
                case 4:
                    {
                        ManagePingPresence(ref goingHerePing, ping);
                        PlayAudioClip(1);
                        break;
                    }
                case 5:
                    {
                        ManagePingPresence(ref attackingHerePing, ping);
                        PlayAudioClip(1);
                        break;
                    }
                case 6:
                    {
                        ManagePingPresence(ref lookingHerePing, ping);
                        PlayAudioClip(1);
                        break;
                    }
                case 7:
                    {
                        ManagePingPresence(ref enemyBeenHerePing, ping);
                        PlayAudioClip(1);
                        break;
                    }
            }
        }
       
        currDoubleClickTime = -10f;
    }

    private void ManagePingPresence(ref GameObject pingType, GameObject replacement)
    {
        if (pingType)
        {
            //Destroy(pingType);
            pingType.transform.GetChild(0).GetChild(0).GetComponent<DestroyPing>().DestroyPingObject();
            pingType = null;
        }
        pingType = replacement;
    }

    public void IgnoreZPres()
    {
        btnPressedOnce = !btnPressedOnce;
        currDoubleClickTime = -10;
    }

    public void PlayAudioClip(int index)
    {
        audioSource.clip = pingSounds[index];
        audioSource.Play();
    }
}




