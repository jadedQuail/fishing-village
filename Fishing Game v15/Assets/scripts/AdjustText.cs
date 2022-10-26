using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustText : MonoBehaviour
{
    // Simple script for moving the TextMesh to the top layer
    [Header("Layer Adjustment")]
    [SerializeField] string layerToPushTo;

    void Start()
    {
        GetComponent<Renderer>().sortingLayerName = layerToPushTo;
    }
}
