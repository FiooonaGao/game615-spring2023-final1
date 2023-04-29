using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void AddSliderValue(float value)
    {
        slider.value += value;

        if (slider.value >= slider.maxValue)
        {
            GetComponent<Collider>().enabled = false;
        }
        Debug.Log("Slider value increased by " + value);
    }
}