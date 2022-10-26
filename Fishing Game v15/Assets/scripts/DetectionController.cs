using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerMovement playerMovement;

    [Header("Detectors")]
    [SerializeField] WaterDetector leftDetector;
    [SerializeField] WaterDetector rightDetector;
    [SerializeField] WaterDetector upDetector;
    [SerializeField] WaterDetector downDetector;

    WaterDetector activeDetector;

    bool validCastSpot;

    // Update is called once per frame
    void Update()
    {
        // Check for the active detector
        if (playerMovement.GetPlayerDirection() == "left") { activeDetector = leftDetector; }
        else if (playerMovement.GetPlayerDirection() == "right") { activeDetector = rightDetector; }
        else if (playerMovement.GetPlayerDirection() == "up") { activeDetector = upDetector; }
        else { activeDetector = downDetector; }

        if (activeDetector.GetWaterDetected())
        {
            validCastSpot = true;
        }
        else
        {
            validCastSpot = false;
        }
    }

    // Function that gets whether or not the player is in a valid cast spot
    public bool GetValidCastSpot()
    {
        return validCastSpot;
    }
}
