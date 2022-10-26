using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawn : MonoBehaviour
{
    SpawnManager spawnManager;              // Reference back to this spawn's spawn manager

    private void OnDrawGizmos()
    {
        // Draw a semitransparent green cube at the transforms position (to visualize spawns)
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(0.5f, 0.5f, 1));
    }

    private void Start()
    {
        // Get a reference back to this spawn's spawn manager
        spawnManager = GetComponentInParent<SpawnManager>();
    }

    // PUBLIC FUNCTIONS

    // Function for getting the spawn manager of this spawn point
    public SpawnManager GetSpawnManager()
    {
        return spawnManager;
    }
}
