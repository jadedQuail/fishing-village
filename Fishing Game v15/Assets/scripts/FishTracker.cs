using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTracker : MonoBehaviour
{
    List<int> caughtFishCodes;

    private void Start()
    {
        caughtFishCodes = new List<int>();
    }

    // Add a code to the caught fish codes
    public void AddCode(int code)
    {
        if (!caughtFishCodes.Contains(code))
        {
            caughtFishCodes.Add(code);
        }
    }

    // Get the caught fish codes
    public List<int> GetCaughtFishCodes()
    {
        return caughtFishCodes;
    }
}
