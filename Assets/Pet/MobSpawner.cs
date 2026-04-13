using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MobSpawner : MonoBehaviour
{
    [System.Serializable]
    public class MobSpawnData
    {
        public GameObject mobPrefab;
        [Range(0, 100)]
        public float spawnChance;
    }

    public MobSpawnData[] mobsToSpawn;
    public bool spawnOnStart = true;

    void Start()
    {
        if (spawnOnStart)
            SpawnMob();
    }

    void SpawnMob()
    {
        float totalChance = mobsToSpawn.Sum(mob => mob.spawnChance);
        float randomValue = Random.Range(0, totalChance);
        float cumulative = 0;

        foreach (var mobData in mobsToSpawn)
        {
            cumulative += mobData.spawnChance;
            if (randomValue < cumulative)
            {
                Instantiate(mobData.mobPrefab, transform.position, Quaternion.identity);
                break;
            }
        }
    }
}
