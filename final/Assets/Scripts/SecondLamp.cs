using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class SecondLamp : MonoBehaviour
{
    public Animator lampAnimator;
    public TextMeshProUGUI PressQText;
    public SliderController energySliderController;
    public GameObject environment;
    public GameObject player;
    public GameObject whiteLightPrefab;

    public AudioSource audioSource;

    public TextMeshProUGUI FollowText;
    private bool isSkyboxLoaded = false;


    private IEnumerator LoadSkyboxCoroutine()
    {
        // 加载 Skybox 材质
        Material skyboxMaterial = Resources.Load<Material>("Epic_GloriousPink/EPIC");

        if (skyboxMaterial != null)
        {
            // 设置场景的 Skybox 材质
            RenderSettings.skybox = skyboxMaterial;
            // 设置摄像机的清除标志为 Skybox
            Camera.main.clearFlags = CameraClearFlags.Skybox;
            // 显示 Environment
            environment.SetActive(true);

            // 等待 5 秒
            yield return new WaitForSeconds(5f);

            // 显示 FollowText
            FollowText.enabled = true;
            GetComponent<Animator>().Play("Follow");

            PlayMusic();
        }
        else
        {
            Debug.LogError("无法加载名为 EPIC 的材质");
        }
    }
    void Start()
    {
        PressQText.enabled = false;
        environment.SetActive(false);
        FollowText.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && energySliderController.energySlider.value >= 0.3f)
        {
            Debug.Log("玩家进入碰撞体范围内");
            PressQText.enabled = true;
        }
        else if (other.CompareTag("light"))
        {
            lampAnimator.SetTrigger("lampLight");
        }
    }


    void PlayMusic()
    {
        audioSource.Play(); // 播放音乐
    }

    // 在 OnTriggerExit 方法中调用
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PressQText.enabled = false;
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
        if (allLightsAreOn && !isSkyboxLoaded)
        {
            Debug.Log("所有的灯都点亮了");
            // 开始协程加载 Skybox
            StartCoroutine(LoadSkyboxCoroutine());
            isSkyboxLoaded = true;
     

        }
        else
        {
            Debug.LogError("无法加载名为 EPIC 的材质");
        }
    }
}