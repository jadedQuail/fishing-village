using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPauseTip : MonoBehaviour
{
    [SerializeField] Animator pauseTipAnim;

    public void MoveOut()
    {
        pauseTipAnim.SetTrigger("moveOut");
    }

    public void DestroyGameObj()
    {
        Destroy(gameObject);
    }
}
