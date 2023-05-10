using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door1 : MonoBehaviour
{
    //public GameObject sceneLoading;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //sceneLoading.SetActive(true);
            SceneManager.LoadScene("3");
        }
    }

}