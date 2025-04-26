using System.Collections;
using UnityEngine;
using System;
public class Spawner : MonoBehaviour
{
    public GameObject AirSpawnPrefab1;
    public GameObject HangerSpawnPrefabefab1;
    public float initialSpawnRate = 2f; // seconds between spawns at level 1
    public float spawnRateDecreasePerLevel = 0.2f; // faster spawns per level
    public int currentLevel = 1;

    private bool isSpawning = true;

    void Start()
    {
        StartCoroutine(SpawnCycle());
    }

    IEnumerator SpawnCycle()
    {
        while (isSpawning)
        {
            SpawnSprite();
            float spawnRate = Mathf.Max(0.5f, initialSpawnRate - spawnRateDecreasePerLevel * (currentLevel - 1));
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void SpawnSprite()
    {
        System.Random rand = new System.Random();
        bool AirOrGround = rand.Next(2) == 0;
        GameObject sprite;

        if(AirOrGround == false)
        {
            sprite = Instantiate(AirSpawnPrefab1, GetRandomSpawnPoint(AirOrGround), Quaternion.identity);
        }
        else
        {
            sprite = Instantiate(HangerSpawnPrefabefab1, GetRandomSpawnPoint(AirOrGround), Quaternion.identity);
        }

        
        StartCoroutine(DespawnAfterDelay(sprite, 50f)); // despawn after 5 seconds
    }

    IEnumerator DespawnAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (obj != null)
            Destroy(obj);
    }

    Vector3 GetRandomSpawnPoint(bool AirOrGround)
    {
        //return new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);
        
        if(AirOrGround == true)
        {
            return new Vector3(4.182f, -4.644f, 0f);
        }
        else
        {
            return new Vector3(9.0f, 4.8f, 0f);
        }
    }

    public void IncreaseLevel()
    {
        currentLevel++;
    }
}
