using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;

    [SerializeField] Tooltip tooltip;

    public void Awake()
    {
        current = this;
    }

    private void Start()
    {
        // Hide this system off the start
        Hide();
    }

    public static void Show(string content)
    {
        current.tooltip.SetText(content);
        current.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        current.tooltip.gameObject.SetActive(false);
    }

    // Function that gets the tooltip itself
    public Tooltip GetTooltip()
    {
        return tooltip;
    }
}
