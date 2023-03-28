using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimationChannel : MonoBehaviour
{
    Animator anim;
    public event Action onThrowPoint;
    public event Action OnRunFast; 
    [SerializeField] private GameObject pickUpPoint;
    private void Awake()
    {
        anim = GetComponent<Animator>();

    }

    //private void Update()
    //{
        
    //    if (pickUpPoint != null)
    //        Debug.DrawRay(pickUpPoint.transform.position, pickUpPoint.transform.forward, Color.red);
    //    // Just to see how animation work. Can be applied in other parts of the code
    //    if (Input.GetKeyDown(KeyCode.F))
    //        SetPlayerAnimationToRunFast();
    //    if (Input.GetKeyUp(KeyCode.F))
    //        anim.SetBool("isRunFast", false);

    //    if (Input.GetKeyDown(KeyCode.T))
    //        SetPlayerAnimationToRun();

    //    if (Input.GetKeyUp(KeyCode.T))
    //        anim.SetBool("isRun", false);

    //    if (Input.GetKeyDown(KeyCode.R))
    //        SetPlayerAnimationToRunBack();
    //    if (Input.GetKeyUp(KeyCode.R))
    //        anim.SetBool("isRunBack", false);

    //    if (Input.GetKeyDown(KeyCode.C))
    //        SetPlayerAnimationToIdleFall();
    //    if (Input.GetKeyUp(KeyCode.C))
    //        anim.SetBool("isIdleFall", false);

    //    if (Input.GetKeyDown(KeyCode.Space))
    //        SetPlayerAnimationToJump();
    //    if (Input.GetKeyUp(KeyCode.Space))
    //        anim.SetBool("isJump", false);

    //    if (Input.GetKeyDown(KeyCode.X))
    //        SetPlayerAnimationToThrow();
    //    if (Input.GetKeyUp(KeyCode.X))
    //        anim.SetBool("isThrow", false);

    //    if (Input.GetKeyDown(KeyCode.Q))
    //        SetPlayerAnimationToThrow2();
    //    if (Input.GetKeyUp(KeyCode.Q))
    //        anim.SetBool("isThrow2", false);

    //    if (Input.GetKeyDown(KeyCode.Z))
    //        SetPlayerAnimationToWin();

    //    if (Input.GetKeyUp(KeyCode.Z))
    //        anim.SetBool("isWin", false);
    //}

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
