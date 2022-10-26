using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDetector : MonoBehaviour
{
    bool waterDetected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WaterSpot"))
        {
            waterDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("WaterSpot"))
        {
            waterDetected = false;
        }
    }

    // Function that gets whether or not water is detected
    public bool GetWaterDetected()
    {
        return waterDetected;
    }
}
