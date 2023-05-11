using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffect : MonoBehaviour
{
    //public Rigidbody playerRigidbody;
    public float minWindForce = 10f; // ��С�ķ���
    public float maxWindForce = 20f; // ���ķ���
    public float minDuration = 3f; // ��С�ĳ���ʱ��
    public float maxDuration = 6f; // ���ĳ���ʱ��
    public float minInterval = 3f; // ��С�ļ��ʱ��
    public float maxInterval = 6f; // ���ļ��ʱ��

    private bool isWindy = false;

    void Start()
    {
        //playerRigidbody = GetComponent<Rigidbody>();

        // �������ѭ��Ч��
        StartCoroutine(StartWindEffect());
    }

    private IEnumerator StartWindEffect()
    {
        while (true)
        {
            if (!isWindy)
            {
                isWindy = true;

                // ����
                Vector3 windForce = transform.forward * Random.Range(minWindForce, maxWindForce);
                //playerRigidbody.AddForce(windForce, ForceMode.Impulse);

                float duration = Random.Range(minDuration, maxDuration);
                yield return new WaitForSeconds(duration);

                isWindy = false;

                float interval = Random.Range(minInterval, maxInterval);
                yield return new WaitForSeconds(interval);
            }
            else
            {
                yield return null;
            }
        }
    }
}