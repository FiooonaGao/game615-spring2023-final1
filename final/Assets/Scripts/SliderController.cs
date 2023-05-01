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

            // ����������
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
            // ��2�����𽥷Ŵ��Ȧ
            lightInstance = light;
            scale = Mathf.SmoothStep(0f, 1f, time / 2f);
            light.transform.localScale = new Vector3(scale, scale, scale);

            time += Time.deltaTime;

            // ���¹�Ȧ��λ��
            light.transform.position = player.transform.position;

            yield return null;
        }

        // �ȴ�2��
        yield return new WaitForSeconds(0.5f);

        time = 0f;
        while (scale > 0f)
        {
            // ��2��������С��Ȧ
            scale = Mathf.SmoothStep(1f, 0f, time / 2f);
            light.transform.localScale = new Vector3(scale, scale, scale);

            time += Time.deltaTime;

            // ���¹�Ȧ��λ��
            light.transform.position = player.transform.position;

            yield return null;
        }

        Destroy(light);
    }
}

