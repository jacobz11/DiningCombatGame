using Assets.Scripts.PickUpItem;
using DiningCombat;
using System;
using UnityEngine;

/// <summary>
/// this is a  
/// </summary>
public class HandPickUp : MonoBehaviour
{
    // location : Player-> Goalie Throw -> mixamorig:Hips
    // -> mixamorig:Spine -> mixamorig:Spine1 -> mixamorig:Spine2
    // ->mixamorig:RightShoulder -> mixamorig:RightArm -> mixamorig:RightForeArm
    // ->mixamorig:RightHand -> PickUpPoint

    // ================================================
    // constant Variable 
    public const int k_Free = 0;
    public const int k_HoldsObj = 1;
    public const int k_Powering = 2;

    // ================================================
    // Delegate

    // ================================================
    // Fields 
    private IStatePlayerHand[] m_PlayerState;
    private KeysHamdler m_Power;
    private GameObject m_GameObject;
    private Animator m_Anim;

    // ================================================
    // ----------------Serialize Field-----------------
    [SerializeField]
    public float m_ForceMulti;
    private int m_StateVal;

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
            m_StateVal = value % 3;
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

    // ================================================
    // auxiliary methods programmings


    // ================================================
    // Unity Game Engine

    private void Awake()
    {
        m_PlayerState = new IStatePlayerHand[3];
        m_PlayerState[k_Free] = new StateFree(this);
        m_PlayerState[k_HoldsObj] = new StateHoldsObj(this);
        m_PlayerState[k_Powering] = new StatePowering(this);

    }
    protected void Start()
    {
        StatePlayerHand = k_Free;
        m_Power = new KeysHamdler(GameKeyboardControls.k_PowerKey);
        m_Anim = GetComponentInParent<Animator>();
    }

    protected void Update()
    {
        StatePlayer.UpdateByState();
    }

    // ================================================
    //  methods
    internal void SetGameFoodObj(GameObject i_GameObject)
    {
        GameFoodObj obj = i_GameObject.GetComponent<GameFoodObj>();
        
        if (obj != null)
        {
            m_GameObject = i_GameObject;
            obj.SetPickUpItem(this);
            StatePlayerHand++;
        }
    }

    internal void ThrowObj()
    {
        GameFoodObj foodObj = m_GameObject.GetComponent<GameFoodObj>();
        
        if (foodObj != null)
        {
            foodObj.CleanUpDelegatesPlayer();
            m_Anim.SetBool(GameGlobal.AnimationName.k_Throwing, true);
            foodObj.HitPlayer += On_HitPlayer_GameFoodObj;
            foodObj.ThrowFood(ForceMulti, this.transform.forward);
        }

        StatePlayerHand = k_Free;
    }


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
        // TODO : 
        ScoreCounter.ScoreValue++;
    }
}