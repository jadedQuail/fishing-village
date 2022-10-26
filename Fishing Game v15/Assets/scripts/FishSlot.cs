using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSlot : MonoBehaviour
{
    [Header("Properties")]
    public int code;

    private bool isFilled = false;

    // Function that activates the filled image for this slot
    public void ActivateFilledImage()
    {
        GameObject filledImage = transform.GetChild(0).gameObject;
        filledImage.SetActive(true);
        isFilled = true;
    }

    public bool GetIsFilled()
    {
        return isFilled;
    }
}
