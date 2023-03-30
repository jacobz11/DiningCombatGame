using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimationChannel : MonoBehaviour
{
    Animator anim;
    public event Action onThrowPoint;
    public event Action OnRunFast; 

    private void Awake()
    {
        anim = GetComponent<Animator>();
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

    public void RunFast()
    {
        OnRunFast?.Invoke();
    }

    public void SetPlayerAnimationToRun()
    {
        anim.SetBool("isRun", true);
    }

    public void SetPlayerAnimationToThrow()
    {
        anim.SetBool("isThrow", true);
    }

    public void SetPlayerAnimationToRunBack()
    {
        anim.SetBool("isRunBack", true);
    }

    public void SetPlayerAnimationToIdleFall()
    {
        anim.SetBool("isIdleFall", true);
    }

    public void SetPlayerAnimationToJump()
    {
        anim.SetBool("isJump", true);
    }

    public void SetPlayerAnimationToThrow2()
    {
        anim.SetBool("isThrow2", true);
    }

    public void SetPlayerAnimationToWin()
    {
        anim.SetBool("isWin", true);
    }
}
