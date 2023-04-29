using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeDoor : MonoBehaviour
{
    public GameObject Fakedoor;
    public GameObject player;

    void Start()
    {
     
        Fakedoor .SetActive(false); // ������
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Fakedoor.SetActive(true);
            // ���ų��������ǰ��
            Vector3 doorPos = player.transform.position + player.transform.forward * 10.0f;
            Instantiate(Fakedoor, doorPos, Quaternion.identity);
            
        }
    }
}