using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door3 : MonoBehaviour
{
    public Animator endAnimator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            endAnimator.SetTrigger("real end");
        }
    }
}
