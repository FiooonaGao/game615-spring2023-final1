using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider energySlider;
    //public float energy = 0f;
    //public float maxEnergy = 1f;
    public GameObject lightPrefab;
    public GameObject player;
    private GameObject lightInstance;
    // Start is called before the first frame update
    void Start()
    {
        //energySlider = GetComponent<Slider>();
        //energySlider.maxValue = maxEnergy; // 设置能量条最大值
       // energySlider.value = energy; // 设置能量条当前值
    }

  public void AddSliderValue(float value)
    {
        energySlider.value += value;

        if (energySlider.value >= energySlider.maxValue)
        {
            GetComponent<Collider>().enabled = false;
        }
        //energySlider.value = energy; // 更新能量条显示
    }  
    void Update()
    {
        if (energySlider. value >= 0.3f && Input.GetKeyDown(KeyCode.Q)) // 当能量达到0.3并且按下 Q 键
        {
            
            GameObject light = Instantiate(lightPrefab, player.transform.position, Quaternion.identity);

            light.transform.localScale = Vector3.zero;

            // 开始协程来控制光圈的缩放效果
            StartCoroutine(ScaleLight(light));

        }

        if (lightInstance != null)
        {
            lightInstance.transform.position = player.transform.position;
        }
        IEnumerator ScaleLight(GameObject light)
        {
            float scale = 0f;
            float time = 0f;
            while (scale < 6f)
            {
                // 在2秒内逐渐放大光圈
                lightInstance = light;
                scale = Mathf.SmoothStep(0f, 6f, time / 2f);
                light.transform.localScale = new Vector3(scale, scale, scale);

                time += Time.deltaTime;
                light.transform.position = player.transform.position;
                yield return null;
            }

            // 等待2秒
            yield return new WaitForSeconds(0.5f);

            time = 0f;
            while (scale > 0f)
            {
                // 在2秒内逐渐缩小光圈
                scale = Mathf.SmoothStep(6f, 0f, time / 2f);
                light.transform.localScale = new Vector3(scale, scale, scale);
                light.transform.position = player.transform.position;
                time += Time.deltaTime;
                yield return null;
            }

            Destroy(light);

        }
    }
}


    
