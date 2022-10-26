using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    [Header("Tilemap (Boundary)")]
    [SerializeField] Tilemap theMap;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    private float halfHeight;
    private float halfWidth;
    
    private Transform target;

    // Player reference
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // Set the target to be the player
        player = GameObject.FindWithTag("Player");
        target = player.transform;

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        // Assign corners of the map
        bottomLeftLimit = theMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        topRightLimit = theMap.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);

        // Set these map bounds for the player
        player.GetComponent<PlayerMovement>().SetBounds(theMap.localBounds.min, theMap.localBounds.max);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Follow the player
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        // Keep the camera inside the map
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
                                         Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
                                         transform.position.z);
    }
}
