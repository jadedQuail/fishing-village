using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMaster : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] int minFishActive;
    [SerializeField] int maxFishActive;
    [SerializeField] float timeBetweenSpawns;
    float timeBetweenCounter;

    [Header("Spawn Managers")]
    [SerializeField] List<SpawnManager> spawnManagers;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the timeBetweenCounter
        timeBetweenCounter = timeBetweenSpawns;

        // Pick a random number of fish to spawn between our min and our max
        int initialFish = Random.Range(minFishActive, maxFishActive + 1);

        int counter = 0;
        while (counter < initialFish)
        {
            bool result = SpawnRandomFish();
            if (result == true) { counter++; }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Every 30 seconds, see if we're ready to spawn a fish
        //      If we're already at the max, don't; check again in 30 seconds.
        //      If we're below the min at ANY point, spawn a fish immediately.

        // If we're ever below the minimum, spawn until we reach it
        if (GetAllActiveSpawns() < minFishActive)
        {
            bool spawnSucceeded = false;
            while (!spawnSucceeded)
            {
                spawnSucceeded = SpawnRandomFish();
            }
        }

        timeBetweenCounter -= Time.deltaTime;
        if (timeBetweenCounter <= 0f)
        {
            // only spawn a fish if we're not at active
            if (GetAllActiveSpawns() < maxFishActive)
            {
                bool spawnSucceeded = false;
                while (!spawnSucceeded)
                {
                    spawnSucceeded = SpawnRandomFish();
                }
            }

            // Reset the timer
            timeBetweenCounter = timeBetweenSpawns;
        }
    }

    // Function that spawns a fish at the next available, random manager
    private bool SpawnRandomFish()
    {
        // Pick a random spawn manager
        int randIndex = Random.Range(0, spawnManagers.Count);

        // If that spawn manager is not at capacity, spawn another fish; return true
        if (!spawnManagers[randIndex].AtCapacity())
        {
            spawnManagers[randIndex].SpawnFish();
            return true;
        }

        // If a fish didn't spawn because the manager wasn't available, return false
        return false;
    }

    // Function that gets active spawns on the whole map
    private int GetAllActiveSpawns()
    {
        int spawns = 0;
        foreach (SpawnManager manager in spawnManagers)
        {
            spawns += manager.GetCurrentSpawns();
        }
        return spawns;
    }
}
