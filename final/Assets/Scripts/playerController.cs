using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;
    public GameObject circlePrefab;
    public GameObject player;
    public GameObject doorPrefab;
    private GameObject circleInstance;
    public bool GameOn = false;

    public int health = 100; // ��ҳ�ʼ����ֵ
    public int maxHealth = 100; // ����������ֵ
    public Slider healthSlider; // ��ʾ����ֵ��Slider


    public bool isShielded = false;



    // Start is called before the first frame update
    void Start()
    {
        GameOn = true;

        // �ҵ������е�Slider����
        healthSlider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();
        // ����Slider�����ֵ
        healthSlider.maxValue = maxHealth;
        // ����Slider�ĳ�ʼֵ
        healthSlider.value = health;
        doorPrefab.SetActive(false);
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

            Vector3 newPosition = transform.position + Vector3.up * Time.deltaTime * 10f;
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(newPosition.y, 1f, Mathf.Infinity), transform.position.z);
        }
        else
        {

            Vector3 newPosition = transform.position + Vector3.down * Time.deltaTime * 3f;
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(newPosition.y, 10f, Mathf.Infinity), transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.E))

        {
            GameObject circle = Instantiate(circlePrefab, player.transform.position, Quaternion.identity);

            circle.transform.localScale = Vector3.zero;

            // ��ʼЭ�������ƹ�Ȧ������Ч��
            StartCoroutine(ScaleCircle(circle));

        }

        if (circleInstance != null)
        {
            circleInstance.transform.position = player.transform.position;
        }

        IEnumerator ScaleCircle(GameObject circle)
        {
            float scale = 0f;
            float time = 0f;
            while (scale < 6f)
            {
                // ��2�����𽥷Ŵ��Ȧ
                circleInstance = circle;
                scale = Mathf.SmoothStep(0f, 6f, time / 2f);
                circle.transform.localScale = new Vector3(scale, scale, scale);

                time += Time.deltaTime;
                circle.transform.position = player.transform.position;
                yield return null;
            }

            // �ȴ�2��
            yield return new WaitForSeconds(0.5f);

            time = 0f;
            while (scale > 0f)
            {
                // ��2��������С��Ȧ
                scale = Mathf.SmoothStep(6f, 0f, time / 2f);
                circle.transform.localScale = new Vector3(scale, scale, scale);
                circle.transform.position = player.transform.position;
                time += Time.deltaTime;
                yield return null;
            }

            Destroy(circle);

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        { // �������������ײ
            health -= 10; // ��������ֵ
            // ����Slider��ֵ
            healthSlider.value = health;
        }
        else if (other.CompareTag("stone"))
        {
            isShielded = true;
        }
        else if (other.CompareTag("scene2Door"))
        {
            doorPrefab.SetActive(true);
            // ���ų��������ǰ��
            Vector3 doorPos = player.transform.position + player.transform.forward * 20.0f;
            Instantiate(doorPrefab, doorPos, Quaternion.identity);
        }
    }


 
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("stone"))
        {
            isShielded = false;
        }
    }
}