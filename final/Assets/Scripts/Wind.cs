using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private float windForce = 0.1f; // �������
    private float windDuration = 5f; // �����ʱ��
    private float windCooldown = 5f; // ����ȴʱ��
    private float windTimer = 0f; // ���ʱ��
    private bool isWindActive = false; // ���Ƿ��ڼ���״̬

    void Start()
    {
        StartCoroutine(WindCycleCoroutine());
    }

    IEnumerator WindCycleCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(windCooldown); // �ȴ���ȴʱ��

            isWindActive = true;
            GetComponent<WindZone>().windMain = windForce; // �����

            yield return new WaitForSeconds(windDuration); // �ȴ������ʱ��

            isWindActive = false;
            GetComponent<WindZone>().windMain = 0f; // ֹͣ��

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
                playerRigidbody.velocity = Vector3.zero; // ����ֹͣʱ��������ٶȹ���
            }
        }
    }
}