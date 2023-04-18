using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;
 
    public bool GameOn = false;



    // Start is called before the first frame update
    void Start()
    {
        GameOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        if (GameOn == true)
        {

            gameObject.transform.Translate(gameObject.transform.forward * Time.deltaTime * moveSpeed * vAxis, Space.World);

            gameObject.transform.Rotate(0, rotateSpeed * Time.deltaTime * hAxis, 0);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            // 按住空格键，向上飞行
            Vector3 newPosition = transform.position + Vector3.up * Time.deltaTime * 10f;
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(newPosition.y, 1f, Mathf.Infinity), transform.position.z);
        }
        else
        {
            // 松开空格键，向下飞行
            Vector3 newPosition = transform.position + Vector3.down * Time.deltaTime * 3f;
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(newPosition.y, 10f, Mathf.Infinity), transform.position.z);
        }

    }
}
