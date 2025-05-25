using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject AirSpawnPrefab1;
    public GameObject HangerSpawnPrefabefab1;

    // Waypoints for Air prefab instances
    public Transform[] airWaypoints;
    public bool[] airStopAtWaypoint;

    // Waypoints for Ground prefab instances
    public Transform[] groundWaypoints;
    public bool[] groundStopAtWaypoint;

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
            //float spawnRate = Mathf.Max(0.5f, initialSpawnRate - spawnRateDecreasePerLevel * (currentLevel - 1));
            float spawnRate = 1;
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void SpawnSprite()
    {
        System.Random rand = new System.Random();
        bool AirOrGround = rand.Next(2) == 0;
        GameObject sprite;

        if (AirOrGround == false)
        {
            sprite = Instantiate(AirSpawnPrefab1, GetRandomSpawnPoint(AirOrGround), Quaternion.identity);

            // Assign waypoints to the spawned prefab's script
            MoverPatternToBlue mover = sprite.GetComponent<MoverPatternToBlue>();
            if (mover != null)
            {
                mover.waypoints = airWaypoints;
                mover.stopAtWaypoint = airStopAtWaypoint;
            }
            else
            {
                Debug.LogWarning("Air prefab missing MoverPatternToBlue script!");
            }
        }
        else
        {
            sprite = Instantiate(HangerSpawnPrefabefab1, GetRandomSpawnPoint(AirOrGround), Quaternion.identity);

            // Assign waypoints to the spawned prefab's script
            MoverHangerTo35 mover = sprite.GetComponent<MoverHangerTo35>();
            if (mover != null)
            {
                mover.waypoints = groundWaypoints;
                mover.stopAtWaypoint = groundStopAtWaypoint;
            }
            else
            {
                Debug.LogWarning("Ground prefab missing MoverPatternToBlue script!");
            }
        }

        StartCoroutine(DespawnAfterDelay(sprite, 500f)); // despawn after 500 seconds
    }

    IEnumerator DespawnAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (obj != null)
            Destroy(obj);
    }

    Vector3 GetRandomSpawnPoint(bool AirOrGround)
    {
        if (AirOrGround == true)
        {
            return new Vector3(4.182f, -4.644f, 0f);
        }
        else
        {
            return new Vector3(7.6f, 7.0f, 0f);
        }
    }

    public void IncreaseLevel()
    {
        currentLevel++;
    }
}
