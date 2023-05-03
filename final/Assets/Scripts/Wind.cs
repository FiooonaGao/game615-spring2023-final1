using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private float windForce = 0.1f; // 风的力量
    private float windDuration = 5f; // 风持续时间
    private float windCooldown = 10f; // 风冷却时间
    private float windTimer = 0f; // 风计时器
    private bool isWindActive = false; // 风是否处于激活状态

    void Update()
    {
        if (!isWindActive) // 如果风没有激活，进入冷却状态
        {
            windTimer += Time.deltaTime;
            if (windTimer >= windCooldown) // 如果冷却时间结束，开始激活风
            {
                StartCoroutine(WindCoroutine());
            }
        }
        else // 如果风已经激活，进入激活状态
        {
            windTimer += Time.deltaTime;
            if (windTimer >= windDuration) // 如果激活时间结束，停止风的激活
            {
                GetComponent<WindZone>().windMain = 0f;
                isWindActive = false;
                windTimer = 0f;
            }
        }
    }

    // 发射风的协程
    IEnumerator WindCoroutine()
    {
        // 播放风声效果
        GetComponent<AudioSource>().Play();
        // 设置风的强度
        GetComponent<WindZone>().windMain = windForce;
        isWindActive = true;
        // 等待风持续时间
        yield return new WaitForSeconds(windDuration);
        // 重置风的强度
        GetComponent<WindZone>().windMain = 0f;
        isWindActive = false;
        windTimer = 0f;
        yield return new WaitForSeconds(windCooldown - windDuration);
    }

    void OnTriggerStay(Collider other)
    {
        // 如果玩家进入风区域，将其向后吹
        if (other.CompareTag("Player") && !other.GetComponent<playerController>().isShielded)
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            playerRigidbody.AddForce(-transform.forward * windForce, ForceMode.Impulse);
        }
    }
}
