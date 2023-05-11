using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private float windForce = 0.1f; // 风的力量
    private float windDuration = 5f; // 风持续时间
    private float windCooldown = 5f; // 风冷却时间
    private float windTimer = 0f; // 风计时器
    private bool isWindActive = false; // 风是否处于激活状态

    void Start()
    {
        StartCoroutine(WindCycleCoroutine());
    }

    IEnumerator WindCycleCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(windCooldown); // 等待冷却时间

            isWindActive = true;
            GetComponent<WindZone>().windMain = windForce; // 激活风

            yield return new WaitForSeconds(windDuration); // 等待风持续时间

            isWindActive = false;
            GetComponent<WindZone>().windMain = 0f; // 停止风

            windTimer = 0f;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (isWindActive && other.CompareTag("Player") && !other.GetComponent<PlayerController4>().isShielded)
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            if (GetComponent<WindZone>().windMain > 0f)
            {
                playerRigidbody.AddForce(-transform.forward * windForce, ForceMode.Impulse);
            }
            else
            {
                playerRigidbody.velocity = Vector3.zero; // 当风停止时，将玩家速度归零
            }
        }
    }
}