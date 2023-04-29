using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextAppear : MonoBehaviour
{
    public TMP_Text textObject;
    public float appearTime = 1f;
    public float disappearTime = 1f;
    public GameObject Fakedoor;
    public GameObject player;
    void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(appearTime);
        textObject.text = "confused";
        yield return new WaitForSeconds(disappearTime);
        textObject.text = "tired";
        yield return new WaitForSeconds(disappearTime);
        textObject.text = "lost";
        yield return new WaitForSeconds(disappearTime);
        textObject.text = "these are the end of your choice";
        yield return new WaitForSeconds(disappearTime);
        textObject.text = "If you feel tired, come here";

        Fakedoor.SetActive(true);
        // 让门出现在玩家前方
        Vector3 doorPos = player.transform.position + player.transform.forward * 20.0f + Vector3.up * -5.0f;
        Instantiate(Fakedoor, doorPos, Quaternion.identity);

        yield return new WaitForSeconds(1f);
        textObject.text = "";

    }
}