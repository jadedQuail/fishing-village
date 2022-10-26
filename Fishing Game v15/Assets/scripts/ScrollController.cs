using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Scrollbar scrollBar;

    public void ResetScroll()
    {
        scrollRect.verticalNormalizedPosition = 1f;
    }
}
