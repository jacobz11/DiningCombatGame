using Assets.Scripts.PickUpItem;
using Assets.Scripts.Player.PickUpItem;
using DiningCombat;
using System;
using UnityEngine;

/// <summary>
/// this is a  
/// </summary>
public class HandPickUp :IThrowingGameObj
{
    // location : Player-> Goalie Throw -> mixamorig:Hips
    // -> mixamorig:Spine -> mixamorig:Spine1 -> mixamorig:Spine2
    // ->mixamorig:RightShoulder -> mixamorig:RightArm -> mixamorig:RightForeArm
    // ->mixamorig:RightHand -> PickUpPoint

    // ================================================
    // constant Variable 
    public const byte k_Free = 0;
    public const byte k_HoldsObj = 1;
    public const byte k_Powering = 2;
    public const byte k_Throwing = 3;

    // ================================================
    // Delegate

    // ================================================
    // Fields 
    private IStatePlayerHand[] m_PlayerState;
    private KeysHamdler m_Power;
    private GameObject m_GameFoodObj;
    private Animator m_Anim;

    // ================================================
    // ----------------Serialize Field-----------------
    public float m_ForceMulti;
    private int m_StateVal;
    [SerializeField]
    private bool m_EventTrow;
    [SerializeField]
    private bool m_EventEnd;

    // ================================================
    // properties
    public float ForceMulti
    {
        get => m_ForceMulti;
        set
        {
            m_ForceMulti = value;
            PowerCounter.PowerValue = m_ForceMulti;
        }
    }

    public int StatePlayerHand
    {
        get => m_StateVal;
        set
        {
            m_StateVal = value % 4;
            m_PlayerState[m_StateVal].InitState();
        }
    }

    public IStatePlayerHand StatePlayer
    {
        get => m_PlayerState[StatePlayerHand];
    }

    internal KeysHamdler Power
    {
        get => m_Power;
    }

    internal bool ThrowingAnimator
    {
        get
        {
            return m_Anim.GetBool(GameGlobal.AnimationName.k_Throwing);
        }
        set
        {
            if (value)
            {
                m_Anim.SetBool(GameGlobal.AnimationName.k_RunningSide, false);
                m_Anim.SetBool(GameGlobal.AnimationName.k_Running, false);
            }

            m_Anim.SetBool(GameGlobal.AnimationName.k_Throwing, value);
        }
    }

    public bool EventTrow
    {
        get => m_EventTrow;
        set => m_EventTrow = value;
    }

    public bool EventEnd
    {
        get => m_EventEnd;
        set =>m_EventEnd = value;
    }

    // ================================================
    // auxiliary methods programmings


    // ================================================
    // Unity Game Engine

    private void Awake()
    {
        m_PlayerState = new IStatePlayerHand[4];
        m_PlayerState[k_Free] = new StateFree(this);
        m_PlayerState[k_HoldsObj] = new StateHoldsObj(this);
        m_PlayerState[k_Powering] = new StatePowering(this);
        m_PlayerState[k_Throwing] = new StateThrowing(this);
        EventTrow = false;
        EventEnd = false;
        m_Power = new KeysHamdler(GameKeyboardControls.k_PowerKey);
    }
    protected void Start()
    {
        m_Anim = GetComponentInParent<Animator>();
        StatePlayerHand = k_Free;
    }

    public void OnThrowingAnimator()
    {
        StatePlayer.SetEventTrowing();
    }
    public void OnThrowingAnimaEnd()
    {
        Debug.Log("in SetEventTrowingEnd");

        StatePlayer.SetEventTrowingEnd();
        //EventEnd = true;
    }
    protected void Update()
    {
        StatePlayer.UpdateByState();
    }

    // ================================================
    //  methods
    /// <summary>
    /// null - will set the fild m_GameFoodObj to null and set the animation k_Throwing to false 
    /// </summary>
    /// <param name="i_GameObject">null or GameObject</param>
    //internal void SetGameFoodObj(GameObject i_GameObject)
    //{
    //    if (i_GameObject == null)
    //    {
    //        m_GameFoodObj = null;
    //        ThrowingAnimator = false;
    //    }
    //    else
    //    {
    //        GameFoodObj obj = i_GameObject.GetComponent<GameFoodObj>();
        
    //        if (obj != null)
    //        {
    //            m_GameFoodObj = i_GameObject;
    //            obj.SetPickUpItem(this);
    //            StatePlayerHand++;
    //        }
    //    }
    //}

    //internal void ThrowObj()
    //{
    //    GameFoodObj foodObj = m_GameFoodObj.GetComponent<GameFoodObj>();
        
    //    if (foodObj != null)
    //    {
    //        foodObj.CleanUpDelegatesPlayer();
    //        foodObj.HitPlayer += On_HitPlayer_GameFoodObj;
    //        foodObj.ThrowFood(ForceMulti, this.transform.forward);
    //    }
    //}


    // ================================================
    // auxiliary methods

    // ================================================
    // Delegates Invoke 

    // ================================================
    // ----------------Unity--------------------------- 
    void OnTriggerEnter(Collider other)
    {
        StatePlayer.EnterCollisionFoodObj(other);
    }

    void OnTriggerExit(Collider other)
    {
        StatePlayer.ExitCollisionFoodObj(other);
    }
    // ----------------GameFoodObj---------------------
    protected virtual void On_HitPlayer_GameFoodObj(object i_Sender, EventArgs e)
    {
        ScoreCounter.ScoreValue++;
    }

    internal override void SetGameFoodObj(GameObject i_GameObject)
    {
        if (i_GameObject == null)
        {
            m_GameFoodObj = null;
            ThrowingAnimator = false;
        }
        else
        {
            GameFoodObj obj = i_GameObject.GetComponent<GameFoodObj>();

            if (obj != null)
            {
                m_GameFoodObj = i_GameObject;
                obj.SetPickUpItem(this);
                StatePlayerHand++;
            }
        }
    }

    internal override void ThrowObj()
    {
        GameFoodObj foodObj = m_GameFoodObj.GetComponent<GameFoodObj>();

        if (foodObj != null)
        {
            foodObj.CleanUpDelegatesPlayer();
            foodObj.HitPlayer += On_HitPlayer_GameFoodObj;
            foodObj.ThrowFood(ForceMulti, this.transform.forward);
        }
    }
}