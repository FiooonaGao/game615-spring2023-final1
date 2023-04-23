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
            // ��ȡ��Ȧ�� transform ���
            StartCoroutine(FollowCircle(other.transform));
           


            animator.SetTrigger("particle disappear");
            // ������������ֵ
            energyBar.value += 0.1f;

            // �����������ֵ�Ѿ��ﵽ���ֵ������ù�Ȧ����ײ��
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

        // �������ӵĸ���Ϊ��Ȧ�� transform ���
        transform.SetParent(circleTransform);
    }
}
