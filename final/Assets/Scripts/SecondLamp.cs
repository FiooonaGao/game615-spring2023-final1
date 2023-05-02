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
        // ���� Skybox ����
        Material skyboxMaterial = Resources.Load<Material>("Epic_GloriousPink/EPIC");

        if (skyboxMaterial != null)
        {
            // ���ó����� Skybox ����
            RenderSettings.skybox = skyboxMaterial;
            // ����������������־Ϊ Skybox
            Camera.main.clearFlags = CameraClearFlags.Skybox;
            // ��ʾ Environment
            environment.SetActive(true);

            // �ȴ� 5 ��
            yield return new WaitForSeconds(5f);

            // ��ʾ FollowText
            FollowText.enabled = true;
            GetComponent<Animator>().Play("Follow");
        }
        else
        {
            Debug.LogError("�޷�������Ϊ EPIC �Ĳ���");
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
            Debug.Log("��ҽ�����ײ�巶Χ��");
            PressQText.enabled = true;
        }
        else if (other.CompareTag("light"))
        {
            lampAnimator.SetTrigger("lampLight");
        }
    }

    IEnumerator WhiteLightEffect(GameObject whiteLightPrefab)
    {
        float duration = 3f; // ����Ч���ĳ���ʱ��
        float maxSize = Screen.width * 2f; // �׹����ߴ�
        float growTime = 2f; // �׹����ʱ��
        float shrinkTime = 1f; // �׹��С��ʱ��

        // ���ɰ׹����
        GameObject whiteLight = Instantiate(whiteLightPrefab, player.transform.position, Quaternion.identity);

        // ��ʼ�׹���Ķ���
        float startTime = Time.time;
        float endTime = startTime + growTime;
        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / growTime;
            float size = Mathf.Lerp(0, maxSize, t);
            whiteLight.transform.localScale = new Vector3(size, size, size);
            yield return null;
        }

        // ��ʼ�׹��С�Ķ���
        startTime = Time.time;
        endTime = startTime + shrinkTime;
        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / shrinkTime;
            float size = Mathf.Lerp(maxSize, 0, t);
            whiteLight.transform.localScale = new Vector3(size, size, size);
            yield return null;
        }

        // ���ٰ׹����
        Destroy(whiteLight);
    }

    // �� OnTriggerExit �����е���
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PressQText.enabled = false;
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
        if (allLightsAreOn && !isSkyboxLoaded)
        {
            Debug.Log("���еĵƶ�������");
            // ��ʼЭ�̼��� Skybox
            StartCoroutine(LoadSkyboxCoroutine());
            isSkyboxLoaded = true;


        }
        else
        {
            Debug.LogError("�޷�������Ϊ EPIC �Ĳ���");
        }
    }
}