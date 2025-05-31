using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject AirSpawnPrefab1;
    public GameObject HangerSpawnPrefabefab1;

    // Holding pattern
    public Transform[] airWaypoints;
    public bool[] airStopAtWaypoint;

    // Landing path
    public Transform[] landingWaypoints;
    public bool[] landingStopAtWaypoint;

    // Waypoints for Ground prefab instances
    public Transform[] groundWaypoints;
    public bool[] groundStopAtWaypoint;

    public float initialSpawnRate = 2f;
    public float spawnRateDecreasePerLevel = 0.2f;
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
            float spawnRate = 10; // simplified
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void SpawnSprite()
    {
        System.Random rand = new System.Random();
        bool AirOrGround = rand.Next(2) == 0;
        GameObject sprite;

        if (!AirOrGround)
        {
            sprite = Instantiate(AirSpawnPrefab1, GetRandomSpawnPoint(AirOrGround), Quaternion.identity);

            MoverPatternToBlue mover = sprite.GetComponent<MoverPatternToBlue>();
            if (mover != null)
            {
                mover.waypoints = airWaypoints;
                mover.stopAtWaypoint = airStopAtWaypoint;

                // 🟢 Give it a landing path for later switching
                mover.landingWaypoints = landingWaypoints;
                mover.landingStopAtWaypoint = landingStopAtWaypoint;
            }
            else
            {
                Debug.LogWarning("Air prefab missing MoverPatternToBlue script!");
            }
        }
        else
        {
            sprite = Instantiate(HangerSpawnPrefabefab1, GetRandomSpawnPoint(AirOrGround), Quaternion.identity);

            MoverHangerTo35 mover = sprite.GetComponent<MoverHangerTo35>();
            if (mover != null)
            {
                mover.waypoints = groundWaypoints;
                mover.stopAtWaypoint = groundStopAtWaypoint;
            }
            else
            {
                Debug.LogWarning("Ground prefab missing MoverHangerTo35 script!");
            }
        }

        StartCoroutine(DespawnAfterDelay(sprite, 500f));
    }

    IEnumerator DespawnAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (obj != null)
            Destroy(obj);
    }

    Vector3 GetRandomSpawnPoint(bool AirOrGround)
    {
        return AirOrGround
            ? new Vector3(4.182f, -4.644f, 0f)
            : new Vector3(7.6f, 7.0f, 0f);
    }

    public void IncreaseLevel()
    {
        currentLevel++;
    }
}
