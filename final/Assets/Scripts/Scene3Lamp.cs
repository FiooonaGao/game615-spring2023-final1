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
            Debug.Log("玩家进入碰撞体范围内");

        }
        else if (other.CompareTag("light"))
        {
            lampAnimator.SetTrigger("lampLight");
        }
    }

    
    // 在 OnTriggerExit 方法中调用
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
        // 获取所有名字为 "light" 的 GameObjects
        GameObject[] lights = GameObject.FindGameObjectsWithTag("lampLight");

        bool allLightsAreOn = true;
        // 遍历所有灯，检查它们是否被点亮
        foreach (GameObject light in lights)
        {
            // 如果有一个灯没被点亮，标记为未全部点亮
            if (!light.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("lampLight"))
            {
                allLightsAreOn = false;
                break;
            }
        }

        // 如果所有灯都点亮了，设置场景的Skybox和环境
        if (allLightsAreOn)
        {
            endAnimator.SetTrigger("real end");
            textAnimator.SetTrigger("real2");


        }
        else
        {
            Debug.LogError("无法加载名为 EPIC 的材质");
        }
    }
}