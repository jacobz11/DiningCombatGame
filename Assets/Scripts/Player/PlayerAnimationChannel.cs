using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimationChannel : MonoBehaviour
{
    Animator m_Anim;
    private float i_NumOfSecondsBefurDestroy  = 5f;

    public event Action ThrowPoint;
    public event Action OnRunFast; 

    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
        ThrowPoint += () => StartCoroutine(StopAnimationToThrow());
    }

    private IEnumerator StopAnimationToThrow()
    {
        yield return null;
        yield return null;
        m_Anim.SetBool("isThrow", false);
        m_Anim.SetBool("isThrow2", false);
    }

    public void EnterThrowPoint()
    {
        Debug.Log("Enter throw point");
        ThrowPoint?.Invoke();
    }

    public void SetPlayerAnimationToRunFast(bool i_IsActive)
    {
        m_Anim.SetBool("isRunFast", i_IsActive);
    }

    public void RunFast()
    {
        OnRunFast?.Invoke();
    }

    public void SetPlayerAnimationToRun(bool i_IsActive)
    {
        m_Anim.SetBool("isRun", i_IsActive);
    }

    public void SetPlayerAnimationToThrow(bool i_IsActive)
    {
        m_Anim.SetBool("isThrow", i_IsActive);
    }

    public void SetPlayerAnimationToRunBack(bool i_IsActive)
    {
        m_Anim.SetBool("isRunBack", i_IsActive);
    }

    public void SetPlayerAnimationToIdleFall(bool i_IsActive)
    {
        m_Anim.SetBool("isIdleFall", i_IsActive);
    }

    public void SetPlayerAnimationToJump(bool i_IsActive)
    {
        m_Anim.SetBool("isJump", i_IsActive);
    }

    public void SetPlayerAnimationToThrow2(bool i_IsActive)
    {
        m_Anim.SetBool("isThrow2", i_IsActive);
    }

    public void SetPlayerAnimationToWin(bool i_IsActive)
    {
        m_Anim.SetBool("isWin", i_IsActive);
    }

    internal void OnPlayerDead()
    {
        SetPlayerAnimationToIdleFall(true);
    }

    private void OnDestroy()
    {
        SetPlayerAnimationToIdleFall(false);
    }
}
