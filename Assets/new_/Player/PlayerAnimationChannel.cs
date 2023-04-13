using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimationChannel : MonoBehaviour
{
    public event Action ThrowPoint;
    public event Action JumpingEnd;
    private Animator m_Anim;

    private bool m_Running = false;
    private bool m_IsRunFast = false;
    private bool m_IsRunnigBake = false;
    private void stopRunBack() => SetPlayerAnimationToRunBack(false);
    private void stopRunFord() { stopRunFast(); stopRuning(); }
    private void stopRunFast() => SetPlayerAnimationToRun(false);
    private void stopRuning() => SetPlayerAnimationToRun(false);

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
        if (i_IsActive != m_IsRunFast) 
        {
            if (i_IsActive)
            {
                stopRunBack();
                stopRuning();
            }
            m_Anim.SetBool("isRunFast", i_IsActive);
            m_IsRunFast = i_IsActive;
        }
    }
    public void SetPlayerAnimationToRun(bool i_IsActive)
    {
        if (m_Running != i_IsActive)
        {
            if (i_IsActive)
            {
                stopRunBack();
                stopRunFast();
            }
            m_Anim.SetBool("isRun", i_IsActive);
            m_IsRunFast = i_IsActive;
        }
    }

    public void SetPlayerAnimationToThrow()
    {
        m_Anim.SetTrigger("isThrow");
    }

    public void OnJumpingUpEnd()
    {
        JumpingEnd?.Invoke();
        m_Anim.SetTrigger("isJumpingDonw");
    }

    public void SetPlayerAnimationToRunBack(bool i_IsActive)
    {
        if (i_IsActive != m_IsRunnigBake)
        {
            if (i_IsActive)
            {
                stopRunFord();
            }
            m_Anim.SetBool("isRunBack", i_IsActive);
            m_IsRunnigBake = i_IsActive;
        }
    }


    public void SetPlayerAnimationToIdleFall(bool i_IsActive)
    {
        m_Anim.SetBool("isIdleFall", i_IsActive);
    }

    public void SetPlayerAnimationToJump()
    {
        m_Anim.SetTrigger("isJump");
    }
 
    public void SetPlayerAnimationToThrow2(bool i_IsActive)
    {
        m_Anim.SetBool("isThrow", i_IsActive);
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
