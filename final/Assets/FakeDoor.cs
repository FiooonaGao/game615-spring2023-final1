using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeDoor : MonoBehaviour
{
    public GameObject Fakedoor;
    public GameObject player;

    void Start()
    {
     
        Fakedoor .SetActive(false); // 禁用门
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Fakedoor.SetActive(true);
            // 让门出现在玩家前方
            Vector3 doorPos = player.transform.position + player.transform.forward * 10.0f;
            Instantiate(Fakedoor, doorPos, Quaternion.identity);
            
        }
    }
}