using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerFishing playerFishing;

    private void Update()
    {
        if (IsPointerOverUIObject())
        {
            playerFishing.SetCanClick(false);
        }
        else
        {
            playerFishing.SetCanClick(true);
        }
    }

    // Function that detects whether or not the mouse is hovering over this object
    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
