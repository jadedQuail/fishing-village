using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exclamation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerFishing playerFishing;
    [SerializeField] Bobber bobberScript;

    // Function that simulates the fish getting away (called by animator)
    public void FishGetsAway()
    {
        // Destroy the hooked fish
        Destroy(playerFishing.GetHookedFish());

        // Restart bobber animation
        bobberScript.ToggleBobberAnim(true);

        // Deactivate self
        gameObject.SetActive(false);
    }
}
