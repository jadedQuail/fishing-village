using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] Animator playerAnim;

    [Header("Movement Settings")]
    [SerializeField] float moveSpeed;

    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    private bool canMove;   // Flag that enables the player to move

    // HUD reference
    HUD hud;

    private void Start()
    {
        // Set HUD reference
        hud = GameObject.FindWithTag("HUD").GetComponent<HUD>();

        // Player can move off the start
        canMove = true;
    }

    void Update()
    {
        if (canMove && !hud.GetGamePaused())
        {
            // Move the player
            playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * moveSpeed;

            // Set the player's walk animations
            playerAnim.SetFloat("moveX", playerRB.velocity.x);
            playerAnim.SetFloat("moveY", playerRB.velocity.y);

            // Set the player's idle animations
            if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1
                || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
            {
                playerAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                playerAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }

            // Keep the player inside the map
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
                                             Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
                                             transform.position.z);
        }
    }


    // Sets the map bounds that the player cannot exit
    public void SetBounds(Vector3 botLeft, Vector3 topRight)
    {
        bottomLeftLimit = botLeft + new Vector3(0.5f, 0.5f, 0f);
        topRightLimit = topRight + new Vector3(-0.5f, -0.5f, 0f);
    }

    // Get the magnitude of the velocity of the player
    public float GetPlayerVelocity()
    {
        return playerRB.velocity.magnitude;
    }

    // Get the direction that the player is facing
    public string GetPlayerDirection()
    {
        if (playerAnim.GetFloat("lastMoveX") >= 1f)
        {
            return "right";
        }
        else if (playerAnim.GetFloat("lastMoveX") <= -1f)
        {
            return "left";
        }
        else if (playerAnim.GetFloat("lastMoveY") >= 1f)
        {
            return "up";
        }
        else
        {
            return "down";
        }
    }

    // Get whether or not the player can move
    public bool GetCanMove()
    {
        return canMove;
    }

    // Set whether or not the player can move
    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}
