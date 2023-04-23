using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Particle : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 5f;
    public Slider energyBar;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Circle"))
        {
            // 获取光圈的 transform 组件
            StartCoroutine(FollowCircle(other.transform));
           


            animator.SetTrigger("particle disappear");
            // 增加能量条的值
            energyBar.value += 0.1f;

            // 如果能量条的值已经达到最大值，则禁用光圈的碰撞器
            if (energyBar.value >= energyBar.maxValue)
            {
                other.GetComponent<Collider>().enabled = false;
            }
        }
    }
    private IEnumerator FollowCircle(Transform circleTransform)
    {
        float t = 0f;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = circleTransform.position;
        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        // 设置粒子的父级为光圈的 transform 组件
        transform.SetParent(circleTransform);
    }
}
