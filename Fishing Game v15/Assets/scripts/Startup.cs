using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject player;
    PlayerMovement playerMovement;
    PlayerFishing playerFishing;

    [Header("Pause Tip")]
    [SerializeField] Animator pauseTipAnimator;

    bool instructionsEnded = false;

    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        playerFishing = player.GetComponent<PlayerFishing>();
    }

    // Start is called before the first frame update
    private void Update()
    {
        if (!instructionsEnded)
        {
            playerMovement.SetCanMove(false);
            playerFishing.SetCanClick(false);
        }
    }

    // Function that ends the instructions, and destroys this gameObject
    public void EndInstructions()
    {
        // Allow the player to move again
        instructionsEnded = true;
        playerMovement.SetCanMove(true);
        playerFishing.SetCanClick(true);

        // Start up the pause tip
        pauseTipAnimator.SetTrigger("moveIn");

        Destroy(gameObject);
    }

    // Function that gets whether or not the instructions are ended
    public bool GetInstructionsEnded()
    {
        return instructionsEnded;
    }
}
