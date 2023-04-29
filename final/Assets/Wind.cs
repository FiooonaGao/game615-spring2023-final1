using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private float windForce = 0.1f; // 风的力量
    private float windDuration = 5f; // 风持续时间
    private float windCooldown = 10f; // 风冷却时间
    private float windTimer = 0f; // 风计时器

    void Update()
    {
        windTimer += Time.deltaTime;

        // 如果计时器超过风冷却时间，则发射新的风
        if (windTimer >= windCooldown)
        {
            windTimer = 0f;
            StartCoroutine(WindCoroutine());
        }
    }

    // 发射风的协程
    IEnumerator WindCoroutine()
    {
        // 播放风声效果
        //GetComponent<AudioSource>().Play();
        // 设置风的强度
        GetComponent<WindZone>().windMain = windForce;
        // 等待风持续时间
        yield return new WaitForSeconds(windDuration);
        // 重置风的强度
        GetComponent<WindZone>().windMain = 0f;
    }

    void OnTriggerStay(Collider other)
    {
        // 如果玩家进入风区域，将其向后吹
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            playerRigidbody.AddForce(-transform.forward * windForce, ForceMode.Impulse);
        }
    }
}