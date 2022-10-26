using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [Header("Menus to Affect")]
    [SerializeField] GameObject menuToOpen;
    [SerializeField] GameObject menuToClose;

    // Function that transitions menus
    public void TransitionMenus()
    {
        menuToOpen.SetActive(true);
        menuToClose.SetActive(false);

        // If we're opening the "fish caught" menu, check for newly caught fish
        if (menuToOpen.name == "fish_caught_menu")
        {
            GameObject.FindWithTag("FishGrid").GetComponent<FishSlotController>().FillOutFishSlots();
        }
    }

    // Function that only closes a menu, doesn't open another
    public void CloseMenuOnly()
    {
        menuToClose.SetActive(false);
    }
}
