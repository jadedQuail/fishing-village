using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFishing : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator playerAnim;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] FishingLine fishingLine;
    [SerializeField] DetectionController detectionController;
    [SerializeField] Startup startup;

    [Header("Bobber Object")]
    [SerializeField] GameObject bobberObj;
    [SerializeField] Bobber bobberScript;

    [Header("Bobber Positions")]
    [SerializeField] Vector3 bobberPosUp;
    [SerializeField] Vector3 bobberPosDown;
    [SerializeField] Vector3 bobberPosLeft;
    [SerializeField] Vector3 bobberPosRight;

    [Header("Surprise Emote")]
    [SerializeField] GameObject surpriseEmote;

    [Header("Fish Showcase Spot")]
    [SerializeField] GameObject fishShowcase;

    // HUD reference
    HUD hud;

    bool rodOut = false;        // Becomes true as soon as the rod cast animation begins

    bool triggerReset = false;
    bool canClick = true;

    bool reelingFish = false;   // Becomes true as soon as the rod is pulled and there's a fish on the line
                                // Becomes false as soon as the fish lands on the player

    GameObject hookedFish;      // The fish that is on the line
    FishSpot hookedFishScript;  // Hooked fish's primary script

    private void Awake()
    {
        // Player should not be able to cast rod while instructions are showing, at the very beginning
        canClick = false;
    }

    private void Start()
    {
        // Get HUD reference
        hud = GameObject.FindWithTag("HUD").GetComponent<HUD>();
    }

    // Update is called once per frame
    void Update()
    {
        // Don't do any fishing mechanics if the game is paused
        if (!hud.GetGamePaused())
        {
            // Reset the throw, pull, and cancel triggers when returning to the idle state
            if (playerMovement.GetPlayerVelocity() <= 0f && !rodOut && !triggerReset)
            {
                ResetFishingTriggers();
                canClick = true;
                triggerReset = true;
            }

            // Player can only cast or pull back rod when not moving
            if (Input.GetMouseButtonDown(0) && playerMovement.GetPlayerVelocity() <= 0f && canClick && startup.GetInstructionsEnded())
            {
                if (rodOut)
                {
                    playerAnim.SetTrigger("pullRod");
                    DeactivateBobber();
                    ToggleSurpriseEmote(false);
                    rodOut = false;
                    triggerReset = false;
                    canClick = false;

                    // If there's a fish on the line, catch it
                    if (hookedFish != null)
                    {
                        CatchFish();
                    }
                }
                else
                {
                    playerAnim.SetTrigger("throwRod");
                    rodOut = true;
                    triggerReset = false;
                    canClick = false;
                }
            }

            // If the player moves during a fishing animation or while the rod is out, go back to idle.
            if (rodOut && playerMovement.GetPlayerVelocity() > 0f)
            {
                playerAnim.SetTrigger("cancelFishing");
                DeactivateBobber();
                ToggleSurpriseEmote(false);
                rodOut = false;
                triggerReset = false;

                // Also, if a hooked fish is on the line, destroy the hooked fish
                if (hookedFish != null) { Destroy(hookedFish); }
            }
        }
    }

    // Function that sets the bobber after a cast
    private void SetBobber()
    {
        // Only going to set the bobber if we're in a valid fishing spot
        if (detectionController.GetValidCastSpot())
        {
            string direction = playerMovement.GetPlayerDirection();

            // Activate the bobber
            bobberObj.SetActive(true);

            // Set the bobber's position
            if (direction == "down") { bobberObj.transform.localPosition = bobberPosDown; }
            else if (direction == "up") { bobberObj.transform.localPosition = bobberPosUp; }
            else if (direction == "left") { bobberObj.transform.localPosition = bobberPosLeft; }
            else if (direction == "right") { bobberObj.transform.localPosition = bobberPosRight; }
        }

        // If the bobber is set (or rod is out and finished animating), the player can click again
        canClick = true;
    }

    // Function that deactivates the bobber
    private void DeactivateBobber()
    {
        // Bobber is no longer in motion
        bobberScript.SetBobberInMotion(false);
        bobberScript.SetBobberVelocity(new Vector2(0f, 0f));
        bobberScript.SetOnWaterSpot(false);

        // Shut off the bobber and the fishing line
        bobberObj.SetActive(false);
        bobberScript.ToggleBobberAnim(true);  // Restore the animator to it's full speed
        fishingLine.ResetLineRenderer();
        fishingLine.gameObject.SetActive(false);

        // Once the bobber is deactivated, the player can click again
        canClick = true;
    }

    // Function that forcibly activates the bobber (for little glitches when casting is rapidly clicked)
    private void ForceActivateBobber()
    {
        // Only going to set the bobber if we're in a valid fishing spot
        if (detectionController.GetValidCastSpot())
        {
            // Activate the bobber
            bobberObj.SetActive(true);
        }

        // Also some scenarios where the rod is out but the boolean doesn't follow
        // So, forcing it here as well
        rodOut = true;

        // If the bobber is set (or rod is out and finished animating), the player can click again
        canClick = true;
    }

    // Function that resets the player from all fishing triggers
    private void ResetFishingTriggers()
    {
        playerAnim.ResetTrigger("throwRod");
        playerAnim.ResetTrigger("pullRod");
        playerAnim.ResetTrigger("cancelFishing");
        DeactivateBobber();
        ToggleSurpriseEmote(false);
    }

    private void SetCanClickTrue()
    {
        canClick = true;
    }

    // Function that gets the value of canClick flag
    public bool GetCanClick()
    {
        return canClick;
    }

    // Function that sets the value of canClick flag
    public void SetCanClick(bool value)
    {
        canClick = value;
    }

    // Function that simulates catching a fish
    private void CatchFish()
    {
        // Instantiate the attached fish; indicate we're reeling in, player can't move
        hookedFishScript.InstantiateAttachedFish();
        reelingFish = true;
        playerMovement.SetCanMove(false);

        // Destroy the fish spot
        hookedFishScript.DestroyFishSpot();

        // Reset the hookedFish
        hookedFish = null;
        hookedFishScript = null;
    }

    // PUBLIC FUNCTIONS

    // Function that toggles the surprise emote
    public void ToggleSurpriseEmote(bool value)
    {
        surpriseEmote.SetActive(value);
    }

    // Function that sets the hooked fish
    public void SetHookedFish(GameObject fish)
    {
        hookedFish = fish;
        hookedFishScript = hookedFish.GetComponent<FishSpot>();
    }

    // Function that gets the hooked fish
    public GameObject GetHookedFish()
    {
        return hookedFish;
    }

    // Function that gets the reelingFish flag
    public bool GetReelingFish()
    {
        return reelingFish;
    }

    // Function that sets the reelingFish flag
    public void SetReelingFish(bool value)
    {
        reelingFish = value;
    }

    // Function that gets the fish showcase spot
    public GameObject GetFishShowcase()
    {
        return fishShowcase;
    }

    // Function that gets whether or not the player's rod is out
    public bool GetRodOut()
    {
        return rodOut;
    }
}
