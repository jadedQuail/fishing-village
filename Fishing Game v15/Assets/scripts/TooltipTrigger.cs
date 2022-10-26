using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Attributes")]
    [SerializeField] string content;
    [SerializeField] float delayTime;
    [SerializeField] Tooltip tooltip;

    // This is broken
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Only activate the tooltip if this fish slot is actually filled
        if (GetComponent<FishSlot>().GetIsFilled())
        {
            StartCoroutine(ShowTooltipAfterDelay(delayTime));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        TooltipSystem.Hide();
    }

    private IEnumerator ShowTooltipAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        tooltip.MoveTooltipToMouse();
        TooltipSystem.Show(content);
    }
}
