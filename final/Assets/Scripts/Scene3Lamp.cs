using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene3Lamp : MonoBehaviour
{
    public Animator lampAnimator;
    public SliderController energySliderController;
    public GameObject player;
    public GameObject doorPrefab;

    public Animator endAnimator;

    public Animator textAnimator;
    private void Start()
    {
        doorPrefab.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && energySliderController.energySlider.value >= 0.3f)
        {
            Debug.Log("��ҽ�����ײ�巶Χ��");

        }
        else if (other.CompareTag("light"))
        {
            lampAnimator.SetTrigger("lampLight");
        }
    }

    
    // �� OnTriggerExit �����е���
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
        // ��ȡ��������Ϊ "light" �� GameObjects
        GameObject[] lights = GameObject.FindGameObjectsWithTag("lampLight");

        bool allLightsAreOn = true;
        // �������еƣ���������Ƿ񱻵���
        foreach (GameObject light in lights)
        {
            // �����һ����û�����������Ϊδȫ������
            if (!light.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("lampLight"))
            {
                allLightsAreOn = false;
                break;
            }
        }

        // ������еƶ������ˣ����ó�����Skybox�ͻ���
        if (allLightsAreOn)
        {
            endAnimator.SetTrigger("real end");
            textAnimator.SetTrigger("real2");


        }
        else
        {
            Debug.LogError("�޷�������Ϊ EPIC �Ĳ���");
        }
    }
}