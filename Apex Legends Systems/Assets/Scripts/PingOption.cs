using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PingOption : MonoBehaviour
{
    public Color defaultColor;


    private Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
    }

    public void Activate(bool val)
    {
        if(val)
        {
            GetComponent<Image>().color = Color.white;
        }
        else
        {
            GetComponent<Image>().color = defaultColor;
        }
    }
}
