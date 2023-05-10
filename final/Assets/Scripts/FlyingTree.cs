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
    private bool isPlayerTalking = false; // ��¼����Ƿ����ڶԻ�

    void Start()
    {
        keyInfo.SetActive(false);
        talkPlane.SetActive(false);
       // arrowIcon.SetActive(false);
        talkText.text = "......";
        door.SetActive(false); // ������
    }

    void Update()
    {
        if (isPlayerTalking && Input.GetKeyDown(KeyCode.C)) // ֻ����������ڶԻ�ʱ��� C ��
        {
            currentDialogueIndex++;
            if (currentDialogueIndex >= dialogues.Length)
            {
                currentDialogueIndex = 0;
                talkPlane.SetActive(false);
                isTalking = false;
                //arrowIcon.SetActive(false);
                keyInfo.SetActive(false);
                isPlayerTalking = false; // ��ҶԻ�����
                door.SetActive(true); // ������
                Vector3 doorPos = player.transform.position + player.transform.forward * 10.0f;
                Instantiate(door, doorPos, Quaternion.identity);
                //door.transform.position = transform.position + transform.forward * 2.0f; // ����λ������Ϊ FlyingTree ��ǰ��
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
        isPlayerTalking = false; // ����뿪������ʱ��ȡ���Ի�
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.C))
        {
            isPlayerTalking = true; // ��Ұ��� C ��ʱ����ʼ�Ի�
            isTalking = true;
            //arrowIcon.SetActive(true);
            talkText.text = dialogues[currentDialogueIndex];
            talkPlane.SetActive(true);
            keyInfo.SetActive(false); // ����keyInfo
        }
    }
}