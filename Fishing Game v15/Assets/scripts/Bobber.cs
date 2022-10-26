using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rigidbody2D bobberRB;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] FishingLine fishingLine;
    [SerializeField] PlayerFishing playerFishing;
    [SerializeField] Animator bobberAnim;

    [Header("Bobber Settings")]
    [SerializeField] float bobberSpeed;

    [Header("Fishing Line Points")]
    [SerializeField] Transform bobberPoint;
    [SerializeField] Transform rodPointRight;
    [SerializeField] Transform rodPointLeft;
    [SerializeField] Transform rodPointUp;
    [SerializeField] Transform rodPointDown;

    bool bobberInMotion = false;
    bool onWaterSpot = false;

    private void Update()
    {
        if (!bobberInMotion)
        {
            string direction = playerMovement.GetPlayerDirection();

            if (direction == "right")
            {
                SetBobberVelocity(new Vector2(1f, 0f).normalized * bobberSpeed);
                fishingLine.SetUpLine(bobberPoint, rodPointRight);
            }
            else if (direction == "left")
            {
                SetBobberVelocity(new Vector2(-1f, 0f).normalized * bobberSpeed);
                fishingLine.SetUpLine(bobberPoint, rodPointLeft);
            }
            else if (direction == "up")
            {
                SetBobberVelocity(new Vector2(0f, 1f).normalized * bobberSpeed);
                fishingLine.SetUpLine(bobberPoint, rodPointUp);
            }
            else
            {
                SetBobberVelocity(new Vector2(0f, -1f).normalized * bobberSpeed);
                fishingLine.SetUpLine(bobberPoint, rodPointDown);
            }

            // Fishing line can be activated after the points are set
            fishingLine.gameObject.SetActive(true);

            // Indicate that the bobber is in motion now
            bobberInMotion = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WaterSpot"))
        {
            onWaterSpot = true;
        }
    }

    // Function that sets whether or not the bobber is in motion
    public void SetBobberInMotion(bool value)
    {
        bobberInMotion = value;
    }

    // Function that sets the bobber's velocity
    public void SetBobberVelocity(Vector2 velocity)
    {
        bobberRB.velocity = velocity;
    }

    // Function that gets whether or not the bobber is currently on a water spot
    public bool GetOnWaterSpot()
    {
        return onWaterSpot;
    }

    // Function that sets whether or not the bobber is currently on a water spot
    public void SetOnWaterSpot(bool value)
    {
        onWaterSpot = value;
    }

    // Function that toggles whether or not the bobber animator is paused
    public void ToggleBobberAnim(bool value)
    {
        if (value == true)
        {
            bobberAnim.speed = 1f;
        }
        else
        {
            bobberAnim.speed = 0f;
        }
    }
}
