using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderDisplay : MonoBehaviour
{
    public Text sliderVal;

    private static Slider slider;

    // Update is called once per frame
    void LateUpdate()
    {
        sliderVal.text = slider.value + "";
    }

    public void Activate()
    {
        if(!slider)
        {
            slider = GetComponent<Slider>();
        }
        slider.value = PlayerPrefs.GetFloat("Sens");
    }

}
