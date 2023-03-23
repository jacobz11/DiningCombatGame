using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimationChannel : MonoBehaviour
{
    Animator anim;
    public event Action onThrowPoint;
    [SerializeField] private GameObject pickUpPoint;
    private void Awake()
    {
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        if (pickUpPoint != null)
            Debug.DrawRay(pickUpPoint.transform.position, pickUpPoint.transform.forward, Color.red);
    }
    public void SetPlayerAnimationToIdle()
    {
        anim.SetBool("isIdle", true);
    }

    public void ThrowPoint()
    {
        Debug.Log("Enter throw point");
        onThrowPoint?.Invoke();
    }

    public void SetPlayerAnimationToRunFast()
    {
        anim.SetBool("isRunFast", true);
    }

}
