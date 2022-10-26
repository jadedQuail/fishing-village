using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject fishCaughtMenu;
    [SerializeField] GameObject instructionMenu1;
    [SerializeField] GameObject instructionMenu2;
    [SerializeField] GameObject instructionMenu3;
    [SerializeField] GameObject creditsMenu;
    [SerializeField] GameObject pauseTip;

    Startup startup;

    // Pause Controller reference
    PauseController pauseController;

    bool gamePaused = false;        // Bool that indicates whether or not the game is in a pause state

    private void Start()
    {
        pauseController = GetComponent<PauseController>();
        startup = GetComponentInChildren<Startup>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && startup.GetInstructionsEnded())
        {
            // If the pause tip is still in existence, destroy it
            if (pauseTip != null)
            {
                Destroy(pauseTip);
            }

            ToggleMenuInterface();
        }
    }

    // Function that closes all menus
    private void CloseAllMenus()
    {
        CloseMenu(pauseMenu);
        CloseMenu(fishCaughtMenu);
        CloseMenu(instructionMenu1);
        CloseMenu(instructionMenu2);
        CloseMenu(instructionMenu3);
        CloseMenu(creditsMenu);

        // Reset the creditsMenu's scroll
        creditsMenu.GetComponent<ScrollController>().ResetScroll();
    }

    // Function that toggles the overall menu interface
    public void ToggleMenuInterface()
    {
        if (pauseMenu.GetComponent<Menu>().GetIsOpen())
        {
            CloseAllMenus();
            gamePaused = false;
            pauseController.ResumeGame();
        }
        else
        {
            OpenMenu(pauseMenu);
            gamePaused = true;
            pauseController.PauseGame();
        }
    }

    // Function that opens a given menu
    public void OpenMenu(GameObject menu)
    {
        menu.SetActive(true);
        menu.GetComponent<Menu>().SetIsOpen(true);
    }

    // Function that closes a given menu
    public void CloseMenu(GameObject menu)
    {
        menu.SetActive(false);
        menu.GetComponent<Menu>().SetIsOpen(false);
    }

    // Function that gets the gamePaused flag
    public bool GetGamePaused()
    {
        return gamePaused;
    }

    // Function that sets the gamePaused flag
    public void SetGamePaused(bool value)
    {
        gamePaused = value;
    }
}
