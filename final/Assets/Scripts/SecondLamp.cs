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

    IEnumerator WhiteLightEffect(GameObject whiteLightPrefab)
    {
        float duration = 3f; // 整个效果的持续时间
        float maxSize = Screen.width * 2f; // 白光最大尺寸
        float growTime = 2f; // 白光变大的时间
        float shrinkTime = 1f; // 白光变小的时间

        // 生成白光对象
        GameObject whiteLight = Instantiate(whiteLightPrefab, player.transform.position, Quaternion.identity);

        // 开始白光变大的动画
        float startTime = Time.time;
        float endTime = startTime + growTime;
        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / growTime;
            float size = Mathf.Lerp(0, maxSize, t);
            whiteLight.transform.localScale = new Vector3(size, size, size);
            yield return null;
        }

        // 开始白光变小的动画
        startTime = Time.time;
        endTime = startTime + shrinkTime;
        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / shrinkTime;
            float size = Mathf.Lerp(maxSize, 0, t);
            whiteLight.transform.localScale = new Vector3(size, size, size);
            yield return null;
        }

        // 销毁白光对象
        Destroy(whiteLight);
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