using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishProjectile : MonoBehaviour
{
    // References - Obtained at Start()
    GameObject player;
    GameObject master;
    PlayerMovement playerMovement;
    PlayerFishing playerFishing;

    GameObject showcaseSpot;

    [Header("References")]
    [SerializeField] Animator fishSpriteAnim;

    [Header("Attributes")]
    [SerializeField] float speedMultiplier;
    [SerializeField] float heightFactor;
    [SerializeField] float widthFactor;

    [Header("ID Code")]
    [SerializeField] int code;

    float speed;

    bool initialDistObtained = false;
    float initialDist;

    Vector3 fishStartPos;

    bool flightComplete = false;

    float fishX;
    float showcaseX;
    float distanceHor;
    float nextX;
    float baseY;
    float height;

    float fishY;
    float showcaseY;
    float distanceVert;
    float nextY;
    float baseX;
    float width;

    void Start()
    {
        fishStartPos = transform.position;
        player = GameObject.FindWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        playerFishing = player.GetComponent<PlayerFishing>();
        showcaseSpot = playerFishing.GetFishShowcase();
        master = GameObject.FindWithTag("Master");
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal arc
        if (playerMovement.GetPlayerDirection() == "right" || playerMovement.GetPlayerDirection() == "left")
        {
            fishX = fishStartPos.x;
            showcaseX = showcaseSpot.transform.position.x;

            // Obtain the initial distance, for the speed calculation
            if (!initialDistObtained)
            {
                initialDist = Mathf.Abs(showcaseX - fishX);
                initialDistObtained = true;
                speed = speedMultiplier * initialDist;
            }

            distanceHor = showcaseX - fishX;
            nextX = Mathf.MoveTowards(transform.position.x, showcaseX, speed * Time.deltaTime);
            baseY = Mathf.Lerp(fishStartPos.y, showcaseSpot.transform.position.y, (nextX - fishX) / distanceHor);
            height = heightFactor * (nextX - fishX) * (nextX - showcaseX) / (-0.25f * distanceHor * distanceHor);

            Vector3 movePosHor = new Vector3(nextX, baseY + height, transform.position.z);
            transform.position = movePosHor;
        }
        else  // Vertical arc
        {
            // Change the sprite's order in layer if we're facing down
            if (playerMovement.GetPlayerDirection() == "down")
            {
                GetComponent<SpriteRenderer>().sortingOrder = 15;
            }

            fishY = fishStartPos.y;
            showcaseY = showcaseSpot.transform.position.y;

            // Obtain the initial distance, for the speed calculation
            if (!initialDistObtained)
            {
                initialDist = Mathf.Abs(showcaseY - fishY);
                initialDistObtained = true;
                speed = speedMultiplier * initialDist;
            }

            distanceVert = showcaseY - fishY;
            nextY = Mathf.MoveTowards(transform.position.y, showcaseY, speed * Time.deltaTime);
            baseX = Mathf.Lerp(fishStartPos.x, showcaseSpot.transform.position.x, (nextY - fishY) / distanceVert);
            width = widthFactor * (nextY - fishY) * (nextY - showcaseY) / (-0.25f * distanceVert * distanceVert);

            Vector3 movePosVert = new Vector3(baseX + width, nextY, transform.position.z);
            transform.position = movePosVert;
        }

        // The fish has reached the player
        if (transform.position == showcaseSpot.transform.position && !flightComplete)
        {
            // Indicate we're no longer reeling the fish; player can move again
            playerFishing.SetReelingFish(false);
            playerMovement.SetCanMove(true);

            // Name text appears
            fishSpriteAnim.SetTrigger("textAppears");

            // Add this fish code to the FishTracker
            master.GetComponent<FishTracker>().AddCode(code);

            // Set the flightComplete flag to true, so this doesn't get called unnecessarily again
            flightComplete = true;
        }

        // If at any point the player is not "reeling in", is moving, and this fish proj. still exists, destroy it.
        // Or, if the player throws his rod again, destroy the fish projectile.
        if ((!playerFishing.GetReelingFish() && playerMovement.GetPlayerVelocity() > 0f) || playerFishing.GetRodOut())
        {
            Destroy(gameObject);

            // As a failsafe - if this object is destroyed and player is stuck, needs to be able to move again
            playerMovement.SetCanMove(true);
        }
    }
}
