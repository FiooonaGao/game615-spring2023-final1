using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Znake : MonoBehaviour
{
    public float speed = 15f; // �����ƶ��ٶ�
    public float stoppingDistance = 1f; // ����ֹͣ����
    public float retreatDistance = 0.5f; // ���˳��˾���

    private Transform player; // ��Ҷ����Transform���

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // �ҵ���ǩΪ��Player���Ķ��󣬲���ȡ��Transform���
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
        { // �����������ҵľ������ֹͣ����
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime); // �������
        }
        else if (Vector3.Distance(transform.position, player.position) < retreatDistance)
        { // �����������ҵľ���С�ڳ��˾���
            transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime); // �����
        }
    }
}
