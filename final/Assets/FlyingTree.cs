using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlyingTree : MonoBehaviour
{
    public GameObject KeyInfo;
    public GameObject TalkPlane;
    public TMP_Text talkText;
    public GameObject arrowIcon;
    public string[] dialogues = new string[]
    {
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"
    };

    private int currentDialogueIndex = 0;
    private bool isTalking = false;

    void Start()
    {
        KeyInfo.SetActive(false);
        TalkPlane.SetActive(false);
        arrowIcon.SetActive(false);
        talkText.text = "......";
    }

    void Update()
    {
        if (KeyInfo.activeSelf && Input.GetKeyDown(KeyCode.C))
        {
            if (isTalking)
            {
                currentDialogueIndex++;
                if (currentDialogueIndex >= dialogues.Length)
                {
                    currentDialogueIndex = 0;
                    TalkPlane.SetActive(false);
                    isTalking = false;
                    arrowIcon.SetActive(false);
                }
                else
                {
                    talkText.text = dialogues[currentDialogueIndex];
                }
            }
            else
            {
                isTalking = true;
                arrowIcon.SetActive(true);
                talkText.text = dialogues[currentDialogueIndex];
                TalkPlane.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            KeyInfo.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentDialogueIndex = 0;
        isTalking = false;
        arrowIcon.SetActive(false);
        KeyInfo.SetActive(false);
        TalkPlane.SetActive(false);
    }
}