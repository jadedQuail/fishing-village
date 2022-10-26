using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSlotController : MonoBehaviour
{
    [Header("Fish Slots")]
    [SerializeField] FishSlot[] fishSlots;

    // Function that fills out the fish slots for fish that are caught
    public void FillOutFishSlots()
    {
        // Get reference to those caught fish
        List<int> caughtFishCodes = GameObject.FindWithTag("Master").GetComponent<FishTracker>().GetCaughtFishCodes();

        // Activate slots of caught fish
        foreach (FishSlot slot in fishSlots)
        {
            if (caughtFishCodes.Contains(slot.code))
            {
                slot.ActivateFilledImage();
            }
        }
    }
}
