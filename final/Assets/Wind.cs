using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private float windForce = 0.1f; // �������
    private float windDuration = 5f; // �����ʱ��
    private float windCooldown = 10f; // ����ȴʱ��
    private float windTimer = 0f; // ���ʱ��
    private bool isWindActive = false; // ���Ƿ��ڼ���״̬

    void Update()
    {
        if (!isWindActive) // �����û�м��������ȴ״̬
        {
            windTimer += Time.deltaTime;
            if (windTimer >= windCooldown) // �����ȴʱ���������ʼ�����
            {
                StartCoroutine(WindCoroutine());
            }
        }
        else // ������Ѿ�������뼤��״̬
        {
            windTimer += Time.deltaTime;
            if (windTimer >= windDuration) // �������ʱ�������ֹͣ��ļ���
            {
                GetComponent<WindZone>().windMain = 0f;
                isWindActive = false;
                windTimer = 0f;
            }
        }
    }

    // ������Э��
    IEnumerator WindCoroutine()
    {
        // ���ŷ���Ч��
        GetComponent<AudioSource>().Play();
        // ���÷��ǿ��
        GetComponent<WindZone>().windMain = windForce;
        isWindActive = true;
        // �ȴ������ʱ��
        yield return new WaitForSeconds(windDuration);
        // ���÷��ǿ��
        GetComponent<WindZone>().windMain = 0f;
        isWindActive = false;
        windTimer = 0f;
        yield return new WaitForSeconds(windCooldown - windDuration);
    }

    void OnTriggerStay(Collider other)
    {
        // �����ҽ�������򣬽������
        if (other.CompareTag("Player") && !other.GetComponent<playerController>().isShielded)
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            playerRigidbody.AddForce(-transform.forward * windForce, ForceMode.Impulse);
        }
    }
}
