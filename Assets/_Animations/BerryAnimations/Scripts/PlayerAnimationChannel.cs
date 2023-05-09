using Assets.scrips;
using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerAnimationChannel : MonoBehaviour
{
    public event Action ThrowPoint;
    public event Action JumpingEnd;
    public event Action StartTrowing;
    //public static Action<string, bool> SetAnimationBool;
    //public static Action<string, float> SetAnimationFloat;
    private Animator m_Anim;

    private void Awake()
    {
        m_Anim = GetComponentInChildren<Animator>();
        ThrowPoint += () => StartCoroutine(StopAnimationToThrow());
    }

    public void AnimationBool(string arg1, bool arg2)
    {
        m_Anim.SetBool(arg1, arg2);
    }
    public void AnimationFloat(string arg1, float arg2)
    {
        m_Anim.SetFloat(arg1, arg2);
    }



    public void OnStartTrowing()
    {
        StartTrowing?.Invoke();
    }
    private void OnStart()
    {
        PlayerMovment player = GetComponentInParent<PlayerMovment>();
        if (player != null)
        {
            player.OnIsRunnigBackChang += player_OnIsRunnigBackChang;
            player.OnIsRunnigChang += player_OnIsRunnigChang;
        }
        else
        {
            Debug.Log("cant find PlayerMovment");
        }
    }

    private void player_OnIsRunnigChang(bool i_IsActive)
    {
        m_Anim.SetBool("isRun", i_IsActive);
    }

    private void player_OnIsRunnigBackChang(bool i_IsActive)
    {
        m_Anim.SetBool("isRunBack", i_IsActive);
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
        ThrowPoint?.Invoke();
    }

    public void SetPlayerAnimationToRunFast(bool i_IsActive)
    {
        m_Anim.SetBool("isRunFast", i_IsActive);
    }

    public void SetPlayerAnimationToRun(bool i_IsActive)
    {
        m_Anim.SetBool("isRun", i_IsActive);
    }

    public void SetPlayerAnimationToThrow(float i)
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
        m_Anim.SetBool("isRunBack", i_IsActive);
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

    internal void SetPlayerAnimationDroping()
    {
        Debug.Log("SetPlayerAnimationDroping");
    }
}
