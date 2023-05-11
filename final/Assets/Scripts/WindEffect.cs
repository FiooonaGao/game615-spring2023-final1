using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffect : MonoBehaviour
{
    //public Rigidbody playerRigidbody;
    public float minWindForce = 10f; // 最小的风力
    public float maxWindForce = 20f; // 最大的风力
    public float minDuration = 3f; // 最小的持续时间
    public float maxDuration = 6f; // 最大的持续时间
    public float minInterval = 3f; // 最小的间隔时间
    public float maxInterval = 6f; // 最大的间隔时间

    private bool isWindy = false;

    void Start()
    {
        //playerRigidbody = GetComponent<Rigidbody>();

        // 启动风的循环效果
        StartCoroutine(StartWindEffect());
    }

    private IEnumerator StartWindEffect()
    {
        while (true)
        {
            if (!isWindy)
            {
                isWindy = true;

                // 吹风
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