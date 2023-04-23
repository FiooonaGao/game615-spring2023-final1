using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject keyInfo;
    public GameObject talkPlane;
    public TMP_Text talkText;
    public GameObject arrowIcon;
    public string[] dialogues = new string[]
    {
        "...", "1", "2", "3", "4",
        "5", "6", "7", "8", "9"
    };

    private int currentDialogueIndex = 0;
    private bool isTalking = false;

    void Start()
    {
        keyInfo.SetActive(false);
        talkPlane.SetActive(false);
        arrowIcon.SetActive(false);
        talkText.text = "......";
    }

    void Update()
    {
        if (keyInfo.activeSelf && Input.GetKeyDown(KeyCode.C))
        {
            if (isTalking)
            {
                currentDialogueIndex++;
                if (currentDialogueIndex >= dialogues.Length)
                {
                    currentDialogueIndex = 0;
                    talkPlane.SetActive(false);
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
                talkPlane.SetActive(true);

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            keyInfo.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentDialogueIndex = 0;
        isTalking = false;
        arrowIcon.SetActive(false);
        keyInfo.SetActive(false);
        talkPlane.SetActive(false);
    }
}