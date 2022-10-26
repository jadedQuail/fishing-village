using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float rotateSpeed;

    // References
    HUD hud;

    private void Start()
    {
        hud = GameObject.FindWithTag("HUD").GetComponent<HUD>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * rotateSpeed, Space.Self);
    }
}
