using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SnakeTrigger : MonoBehaviour
{
    public GameObject Znake;
    public GameObject Znake2;
    public GameObject Camera;
    public Animator snakeAnimator;
    public SliderController energySliderController;

    public TextMeshProUGUI PressRText;

    private bool hasTriggered = false;

    void Start()
    {
        Znake.SetActive(false);
        Znake2.SetActive(false);
        Camera.SetActive(false);
        PressRText.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            snakeAnimator.SetTrigger("move");
            Znake.SetActive(true);
            Camera.SetActive(true);

            StartCoroutine(DisableAndShowZnake(Znake, Znake2, Camera, 4f));
            hasTriggered = true;
        }
    }

    private IEnumerator DisableAndShowZnake(GameObject objToDisable, GameObject objToShow, GameObject camera, float delay)
    {
        yield return new WaitForSeconds(delay);

        objToDisable.SetActive(false);
        camera.SetActive(false);

        objToShow.SetActive(true);

        PressRText.enabled = true;
    }
    void Update()
    {
        if (energySliderController.energySlider.value >= 0.5f && Input.GetKeyDown(KeyCode.R))
        {
            PressRText.enabled = false; // 按下R键后禁用"press R"文本
        }
    }
}