using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPosToUI : MonoBehaviour
{
    public GameObject pingToFollow;
    public Text distanceText;

    private float fov;
    private GameObject player;

    private Camera cam;
    private RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        cam = Camera.main;
        fov = cam.fieldOfView;
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player && pingToFollow)
        {
            Vector3 v = player.transform.position - pingToFollow.transform.position;
            v.y = 0;
            distanceText.text = (int)(v.magnitude) + "m";

            Vector3 toFollow = pingToFollow.transform.position - player.transform.position;
            Vector3 currForward = player.transform.forward;
            toFollow.y = 0;
            currForward.y = 0;
            float angle = Vector3.SignedAngle(currForward, toFollow, player.transform.up);
            //angle *= Mathf.Rad2Deg;
            Vector3 pos = new Vector3(0f, 0f, 0f);

            if (IsBetween(angle, -fov / 2, fov / 2)) // In front
            {
                pos.y = 0;
                pos.x = Remap(angle, -fov / 2, fov / 2, 0, Screen.width - 163f);
            }
            else if (IsBetween(angle, 20f, 135f)) // Right
            {
                pos.x = Screen.width - 163f;
                pos.y = Remap(angle, 20f, 135f, 0, Screen.height - 105f);
            }
            else if (IsBetween(angle, -135f, -20f)) // Left
            {
                pos.x = 0;
                pos.y = Remap(-angle, 20f, 135f, 0, Screen.height - 105f);
            }
            else if (IsBetween(angle, 135f, 180f)) // Back-1
            {
                pos.y = Screen.height - 105f;
                pos.x = Remap(-angle, -180f, -135f, Screen.width / 2, Screen.width - 163f);
            }
            else if (IsBetween(angle, -180f, -135f)) // Back-2
            {
                pos.y = Screen.height - 105f;
                pos.x = Remap(-angle, 135f, 180f, 0, Screen.width / 2);
            }

            rect.position = pos;
        }
        
    }

    float Remap (float currVal, float currMin, float currMax, float desiredMin, float desiredMax)
    {
        float a = (currVal - currMin) / (currMax - currMin);
        float b = desiredMax - desiredMin;

        return (a * b) + desiredMin;
    }

    bool IsBetween(float val, float l, float r)
    {
        return (val >= l && val <= r);
    }

}
