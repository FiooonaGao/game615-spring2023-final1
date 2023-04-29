using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private float windForce = 0.1f; // �������
    private float windDuration = 5f; // �����ʱ��
    private float windCooldown = 10f; // ����ȴʱ��
    private float windTimer = 0f; // ���ʱ��

    void Update()
    {
        windTimer += Time.deltaTime;

        // �����ʱ����������ȴʱ�䣬�����µķ�
        if (windTimer >= windCooldown)
        {
            windTimer = 0f;
            StartCoroutine(WindCoroutine());
        }
    }

    // ������Э��
    IEnumerator WindCoroutine()
    {
        // ���ŷ���Ч��
        //GetComponent<AudioSource>().Play();
        // ���÷��ǿ��
        GetComponent<WindZone>().windMain = windForce;
        // �ȴ������ʱ��
        yield return new WaitForSeconds(windDuration);
        // ���÷��ǿ��
        GetComponent<WindZone>().windMain = 0f;
    }

    void OnTriggerStay(Collider other)
    {
        // �����ҽ�������򣬽������
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            playerRigidbody.AddForce(-transform.forward * windForce, ForceMode.Impulse);
        }
    }
}