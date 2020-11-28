using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PingMenu : MonoBehaviour
{
    public static bool isMenuActive;

    public Text pingInfo;
    public string[] pingInfoTexts;
    public PingOption[] options;
    public float[] selectionRotations;
    public GameObject graphic;
    public GameObject player;
    public RectTransform selectionMarker;
    public float inputThresholdVal;
    public int pingIndex;
    public AudioSource clickSound;

    private bool hasMoved;

    private PingOption activatedOption;
    private QuickPing qp;
    private PlayerLook playerLook;

    // Start is called before the first frame update
    void Start()
    {
        hasMoved = false;
        playerLook =  player.GetComponent<PlayerLook>();
        qp = player.GetComponent<QuickPing>();
        isMenuActive = false;
        activatedOption = null;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isMenuActive)
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            x = Mathf.Clamp(x, -1f, 1f);
            y = Mathf.Clamp(y, -1f, 1f);

            if(x != 0 || y != 0)
            {
                hasMoved = true;
                selectionMarker.gameObject.SetActive(true);
                pingInfo.gameObject.SetActive(true);
            }

            

            if(hasMoved)
            {
                int index = GetSelection(x, y);
               
                if(index != -1)
                {
                    //print(hasMoved + ": (" + x + ", " + y + ")");
                    if (activatedOption != null)
                    {
                        activatedOption.Activate(false);
                    }
                    pingIndex = index;
                    activatedOption = options[index];
                    activatedOption.Activate(true);
                    
                }
                
            } 

            if(Input.GetKeyDown(KeyCode.X))
            {
                pingIndex = -1;
                SetMenuState(false);
            }
        }
    }

    private int GetSelection(float x, float y)
    {
        if(x == 0 && y >= inputThresholdVal)
        {
            PlaySelectSound(0);
            SetPingInfoText(0);
            SetSelectionRotZ(selectionRotations[0]);
            return 0;
        }
        else if (x >= inputThresholdVal && y >= inputThresholdVal)
        {
            PlaySelectSound(1);
            SetPingInfoText(1);
            SetSelectionRotZ(selectionRotations[1]);
            return 1;
        }
        else if( x >= inputThresholdVal && y == 0)
        {
            PlaySelectSound(2);
            SetPingInfoText(2);
            SetSelectionRotZ(selectionRotations[2]);
            return 2;
        }
        else if (x >= inputThresholdVal && y <=-inputThresholdVal)
        {
            PlaySelectSound(3);
            SetPingInfoText(3);
            SetSelectionRotZ(selectionRotations[3]);
            return 3;
        }
        else if (x == 0 && y <= -inputThresholdVal)
        {
            PlaySelectSound(4);
            SetPingInfoText(4);
            SetSelectionRotZ(selectionRotations[4]);
            return 4;
        }
        else if (x <= -inputThresholdVal && y <= -inputThresholdVal)
        {
            PlaySelectSound(5);
            SetPingInfoText(5);
            SetSelectionRotZ(selectionRotations[5]);
            return 5;
        }
        else if (x <= -inputThresholdVal && y == 0)
        {
            PlaySelectSound(6);
            SetPingInfoText(6);
            SetSelectionRotZ(selectionRotations[6]);
            return 6;
        }
        else if (x <= -inputThresholdVal && y >= inputThresholdVal)
        {
            PlaySelectSound(7);
            SetPingInfoText(7);
            SetSelectionRotZ(selectionRotations[7]);
            return 7;
        }

        return -1;

    }

    public void SetMenuState(bool val)
    {
        isMenuActive = val;

        if (val)
        {
            hasMoved = false;
            activatedOption = null;
            pingIndex = -1;

            //playerLook.SetSens(0, 0);
            

            graphic.SetActive(true);
        }
        else
        {
            foreach(PingOption p in options)
            {
                p.Activate(false);
            }
            if(pingIndex != -1)
            {
                qp.PlacePing(pingIndex);
            }
            selectionMarker.gameObject.SetActive(false);
            pingInfo.gameObject.SetActive(false);
            //playerLook.SetSens();

            graphic.SetActive(false);

        }

        
    }

    void SetSelectionRotZ(float val)
    {
        Vector3 rot = selectionMarker.rotation.eulerAngles;
        rot.z = val;
        selectionMarker.rotation = Quaternion.Euler(rot);
    }

    void SetPingInfoText(int index)
    {
        pingInfo.text = pingInfoTexts[index];
    }

    void PlaySelectSound(int toSelect)
    {
        if(toSelect != pingIndex && !clickSound.isPlaying)
        {
            clickSound.Play();
        }
    }

}
