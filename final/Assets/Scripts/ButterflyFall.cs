using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyFall : MonoBehaviour
{
    public GameObject petalPrefab; // The prefab for the flower petal
    public int numPetals; // The number of petals to spawn
    public float spawnRadius; // The radius around the parent object to spawn the petals
    public float minSpeed; // The minimum speed at which the petals fall
    public float maxSpeed; // The maximum speed at which the petals fall
    public float spawnInterval; // The time interval between petal spawns

    void Start()
    {
        SpawnPetal(); // Spawn the first petal immediately
        StartCoroutine(SpawnPetals());
    }

    IEnumerator SpawnPetals()
    {
        while (true)
        {
            SpawnPetal();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnPetal()
    {
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        GameObject petal = Instantiate(petalPrefab, spawnPosition, Quaternion.identity) as GameObject;
        petal.transform.parent = transform;

        Rigidbody petalRigidbody = petal.GetComponent<Rigidbody>();
        petalRigidbody.velocity = Vector3.down * Random.Range(minSpeed, maxSpeed);
    }
}