using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject fishSpot;

    [Header("Spawn Lists")]
    [SerializeField] GameObject[] possibleSpawns;
    [SerializeField] int[] weights;
    [SerializeField] List<GameObject> fishSpawns;

    [Header("Attributes")]
    [SerializeField] int maxSpawns;
    int currentSpawns;

    List<GameObject> activeFishSpawns;      // Those fish spawns that are currently active

    // Function that gets a random fish, accounting for weights
    private GameObject GetRandomFish()
    {
        // Create a list of strings that reflects the weights of each possible spawn
        List<string> choices = new List<string>();
        for (int i = 0; i < possibleSpawns.Length; i++)
        {
            for (int j = 0; j < weights[i]; j++)
            {
                choices.Add(possibleSpawns[i].gameObject.name);
            }
        }

        // Select a random string
        string selection = choices[Random.Range(0, choices.Count)];

        // Find the fish that matches this selected name
        foreach (GameObject fishSprite in possibleSpawns)
        {
            if (fishSprite.name == selection) { return fishSprite; }
        }

        return null;
    }

    // PUBLIC FUNCTIONS

    // Function that spawns a fish
    public void SpawnFish()
    {
        // Sometimes this method beats the actual creation of the empty "activeFishSpawns" list
        if (activeFishSpawns == null) { activeFishSpawns = new List<GameObject>(); }

        // Select a random spawn in this area for the fish
        int index = Random.Range(0, fishSpawns.Count);
        GameObject thisFishSpawn = fishSpawns[index];
        activeFishSpawns.Add(fishSpawns[index]);
        fishSpawns.RemoveAt(index);

        // Select a fish to be spawned in, then spawn it
        GameObject fishSelection = GetRandomFish();
        GameObject thisFishSpot = Instantiate(fishSpot, thisFishSpawn.transform.position, transform.rotation);
        
        // Set an attached fish and the spawn point for this fish spot
        thisFishSpot.GetComponent<FishSpot>().SetAttachedFish(fishSelection);
        thisFishSpot.GetComponent<FishSpot>().SetSpawnPoint(thisFishSpawn);

        // Increment current spawns active
        currentSpawns += 1;
    }

    // Function that despawns a fish - frees up a spawn
    public void FreeUpSpawn(GameObject spawn)
    {
        // Remove this spawn from the active spawns
        activeFishSpawns.Remove(spawn);

        // Add it back into the available fish spawns
        fishSpawns.Add(spawn);

        // Decrement the count of currentSpawns
        currentSpawns -= 1;
    }

    // Function that gets the current spawns in use from this manager
    public int GetCurrentSpawns()
    {
        return currentSpawns;
    }

    // Function that gets whether or not this area is at capacity for fish

    public bool AtCapacity()
    {
        if (currentSpawns >= maxSpawns)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
