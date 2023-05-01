using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider energySlider;
    public GameObject lightPrefab;
    public GameObject player;
    private GameObject lightInstance;

    void Start()
    {
        //energySlider = GetComponent<Slider>();
        //energySlider.maxValue = maxEnergy;
        //energySlider.value = energy;
    }

    public void AddSliderValue(float value)
    {
        energySlider.value += value;

        if (energySlider.value >= energySlider.maxValue)
        {
            GetComponent<Collider>().enabled = false;
        }
    }

    void Update()
    {
        if (energySlider.value >= 0.3f && Input.GetKeyDown(KeyCode.Q))
        {
            GameObject light = Instantiate(lightPrefab, player.transform.position, Quaternion.identity);
            light.transform.localScale = Vector3.zero;
            lightInstance = light;
            StartCoroutine(ScaleLight(light));

            // 减少能量条
            AddSliderValue(-0.3f);
        }

        if (lightInstance != null)
        {
            lightInstance.transform.position = player.transform.position;
        }
    }

    IEnumerator ScaleLight(GameObject light)
    {
        float scale = 0f;
        float time = 0f;
        while (scale < 1f)
        {
            // 在2秒内逐渐放大光圈
            lightInstance = light;
            scale = Mathf.SmoothStep(0f, 1f, time / 2f);
            light.transform.localScale = new Vector3(scale, scale, scale);

            time += Time.deltaTime;

            // 更新光圈的位置
            light.transform.position = player.transform.position;

            yield return null;
        }

        // 等待2秒
        yield return new WaitForSeconds(0.5f);

        time = 0f;
        while (scale > 0f)
        {
            // 在2秒内逐渐缩小光圈
            scale = Mathf.SmoothStep(1f, 0f, time / 2f);
            light.transform.localScale = new Vector3(scale, scale, scale);

            time += Time.deltaTime;

            // 更新光圈的位置
            light.transform.position = player.transform.position;

            yield return null;
        }

        Destroy(light);
    }
}

