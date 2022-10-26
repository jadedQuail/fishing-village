using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    bool isOpen;

    // Function that gets the isOpen flag
    public bool GetIsOpen()
    {
        return isOpen;
    }

    // Function that sets the isOpen flag
    public void SetIsOpen(bool value)
    {
        isOpen = value;
    }

    // Function that provides alt way to close this menu
    public void CloseMenu()
    {
        isOpen = false;
        gameObject.SetActive(false);

        // If this is the pause menu, unpause the game
        if (gameObject.name == "pause_menu")
        {
            // Resume the game
            GameObject hud = GameObject.FindWithTag("HUD");
            hud.GetComponent<PauseController>().ResumeGame();
            hud.GetComponent<HUD>().SetGamePaused(false);
        }
    }
}
