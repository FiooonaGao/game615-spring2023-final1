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

            PlayMusic();
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


    void PlayMusic()
    {
        audioSource.Play(); // ��������
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