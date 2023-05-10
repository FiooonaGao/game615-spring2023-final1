using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlyingTree : MonoBehaviour
{
    public GameObject keyInfo;
    public GameObject talkPlane;
    public TMP_Text talkText;
    //public GameObject arrowIcon;
    public GameObject door;
    public GameObject player;
    public string[] dialogues = new string[]
    {
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"
    };

    private int currentDialogueIndex = 0;
    private bool isTalking = false;
    private bool isPlayerTalking = false; // 记录玩家是否正在对话

    void Start()
    {
        keyInfo.SetActive(false);
        talkPlane.SetActive(false);
       // arrowIcon.SetActive(false);
        talkText.text = "......";
        door.SetActive(false); // 禁用门
    }

    void Update()
    {
        if (isPlayerTalking && Input.GetKeyDown(KeyCode.C)) // 只有在玩家正在对话时检查 C 键
        {
            currentDialogueIndex++;
            if (currentDialogueIndex >= dialogues.Length)
            {
                currentDialogueIndex = 0;
                talkPlane.SetActive(false);
                isTalking = false;
                //arrowIcon.SetActive(false);
                keyInfo.SetActive(false);
                isPlayerTalking = false; // 玩家对话结束
                door.SetActive(true); // 启用门
                Vector3 doorPos = player.transform.position + player.transform.forward * 10.0f;
                Instantiate(door, doorPos, Quaternion.identity);
                //door.transform.position = transform.position + transform.forward * 2.0f; // 将门位置设置为 FlyingTree 的前方
            }
            else
            {
                talkText.text = dialogues[currentDialogueIndex];
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
        //arrowIcon.SetActive(false);
        keyInfo.SetActive(false);
        talkPlane.SetActive(false);
        isPlayerTalking = false; // 玩家离开触发器时，取消对话
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.C))
        {
            isPlayerTalking = true; // 玩家按下 C 键时，开始对话
            isTalking = true;
            //arrowIcon.SetActive(true);
            talkText.text = dialogues[currentDialogueIndex];
            talkPlane.SetActive(true);
            keyInfo.SetActive(false); // 隐藏keyInfo
        }
    }
}