using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LampLight : MonoBehaviour
{
    
    public Animator lampAnimator;
    public TextMeshProUGUI PressText;
    public SliderController energySliderController;


    void Start()
    {
        PressText.enabled = false;
    }
        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && energySliderController.energySlider.value >= 0.3f)
        {
            // 玩家进入碰撞体范围内，显示提示
            Debug.Log("玩家进入碰撞体范围内");
            PressText.enabled = true;

        }
        else if (other.CompareTag("light"))
        {
            // 光进入碰撞体范围内，播放动画
            lampAnimator.SetTrigger("lampLight");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PressText.enabled = false;
        }
    }

}