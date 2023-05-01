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
            // ��ҽ�����ײ�巶Χ�ڣ���ʾ��ʾ
            Debug.Log("��ҽ�����ײ�巶Χ��");
            PressText.enabled = true;

        }
        else if (other.CompareTag("light"))
        {
            // �������ײ�巶Χ�ڣ����Ŷ���
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