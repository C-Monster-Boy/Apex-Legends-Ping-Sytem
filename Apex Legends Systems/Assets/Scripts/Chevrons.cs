using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Chevrons : MonoBehaviour
{
    public GameObject leftChev;
    public GameObject rightChev;
    public GameObject[] items;
    public Slider slider;

    private int currIndex;

    // Start is called before the first frame update
    void Start()
    {
        currIndex = 0;
        EnableOne(currIndex);
    }

    void EnableOne(int index)
    {
        foreach(GameObject g in items)
        {
            g.SetActive(false);
        }

        items[index].SetActive(true);
    }

    public void LeftChevronClicked()
    {
        if(!rightChev.activeSelf)
        {
            rightChev.SetActive(true);
        }

        if(currIndex == 1)
        {
            leftChev.SetActive(false);
        }

        currIndex--;

        EnableOne(currIndex);
    }

    public void RightChevronClicked()
    {
        if(!leftChev.activeSelf)
        {
            leftChev.SetActive(true);
        }

        if(currIndex == items.Length -2)
        {
            rightChev.SetActive(false);
        }

        currIndex++;

        EnableOne(currIndex);
    }

    public void Back()
    {
        gameObject.SetActive(false);
    }

    public void HandleSlider()
    {
        PlayerPrefs.SetFloat("Sens", (int)(slider.value));
    }
}
