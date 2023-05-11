using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Znake : MonoBehaviour
{
    public float speed = 15f; // 敌人移动速度
    public float stoppingDistance = 1f; // 敌人停止距离
    public float retreatDistance = 0.5f; // 敌人撤退距离

    private Transform player; // 玩家对象的Transform组件

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // 找到标签为“Player”的对象，并获取其Transform组件
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
        { // 如果敌人与玩家的距离大于停止距离
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime); // 移向玩家
        }
        else if (Vector3.Distance(transform.position, player.position) < retreatDistance)
        { // 如果敌人与玩家的距离小于撤退距离
            transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime); // 向后撤退
        }
    }
}
