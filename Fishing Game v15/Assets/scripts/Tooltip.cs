using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI headerField;

    public void SetText(string content)
    {
        headerField.text = content;
    }

    public void MoveTooltipToMouse()
    {
        // Set tooltip position to mouse's position
        Vector2 position = Input.mousePosition;
        transform.position = position;
    }
}
