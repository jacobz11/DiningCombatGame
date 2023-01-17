using DiningCombat;
using System;
using UnityEngine;
using Assets.Scripts.Player;

public class GameFoodObj : MonoBehaviour
{
    // ================================================
    // constant Variable 
    private const bool k_Enter = true, k_Exit = false;
    private const string k_ClassName = nameof(GameFoodObj);

    // ================================================
    // Delegate
    /// <summary>to notify the thrower that he hit</summary>
    public event EventHandler HitPlayer;

    /// <summary>to notify Game-Manager that this GameFoodObj destroyed</summary>
    public event EventHandler Destruction;

    // ================================================
    // Fields 
    private bool m_IsThrow;
    private Rigidbody m_Rigidbody;
    // ================================================
    // ----------------Serialize Field-----------------

    // ================================================
    // properties
    public bool IsThrow 
    {
        get => m_IsThrow;
        private set
        {
            m_IsThrow = value;
        }
    }

    // ================================================
    // auxiliary methods programmings
    private void dedugger(string func, string i_var)
    {
        GameGlobal.Dedugger(k_ClassName, func, i_var);
    }

    // ================================================
    // Unity Game Engine
    protected virtual void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // ================================================
    //  methods
    /// <summary>
    /// this method can call one time for obj 
    /// </summary>
    /// <param name="i_ForceMulti"> the Force to add</param>
    /// <param name="i_ThrowDirection"></param>
    public void ThrowFood(float i_ForceMulti, Vector3 i_ThrowDirection)
    {
        IsThrow = true;
        tag = GameGlobal.TagNames.k_ThrowFoodObj;
        transform.parent = null;
        m_Rigidbody.useGravity = true;
        m_Rigidbody.AddForce(i_ThrowDirection * i_ForceMulti);
    }

    internal void SetPickUpItem(PickUpItem pickUpItem)
    {
        this.transform.position = pickUpItem.transform.position;
        this.transform.SetParent(pickUpItem.transform, true);
        this.transform.localPosition = pickUpItem.transform.localPosition;
        m_Rigidbody.useGravity = false;
    }
    /// <summary>
    /// collision After Throwing Handler will:
    /// 1. will tell the throw player if it hit the player
    /// 2. will perform the effect
    /// 3. will destroy the object
    /// </summary>
    /// <param name="i_Collision"></param>
    private void collisionAfterThrowingHandler(Collision i_Collision)
    {
        if (isPlayer(i_Collision))
        {
            OnHitPlayer(EventArgs.Empty);
            PlayerHp playerHit = i_Collision.gameObject
                .GetComponent<PlayerHp>();
            if (playerHit != null)
            {
                playerHit.HitYou(1f);
            }
            else
            {

            }
        }

        performTheEffect();
        destruction();
    }

    private bool performTheEffect()
    {
        // TODO: 
        return true;
    }


    // ================================================
    // auxiliary methods
    /// <summary>
    /// this func parsing form Collision PickUpItem 
    /// </summary>
    /// <param name="i_Collision"></param>
    /// <param name="o_Pic"></param>
    /// <returns>if parsing success </returns>
    private bool parseCollision(Collision i_Collision, out PickUpItem o_Pic)
    {
        o_Pic = i_Collision.gameObject
            .GetComponentInChildren(typeof(PickUpItem)) as PickUpItem;
        
        return (o_Pic != null);
    }

    /// <summary>
    /// a query to test if Collision tag as Player
    /// </summary>
    /// <param name="i_Collision"></param>
    /// <returns></returns>
    private bool isPlayer(Collision i_Collision)
    {
        return i_Collision.gameObject.CompareTag(GameGlobal.TagNames.k_Player);
    }

    /// <summary>
    /// this func notify the PickUpItem (Player)
    /// interface IStatePlayerHand - cant or can Pick-Up this item
    /// </summary>
    /// <param name="i_Collision"></param>
    /// <param name="i_IsEnter">
    /// <true>true->Enter-Collision-Food-Obj</true>
    /// <false>false->ExitCollisionFoodObj</false></param>
    private void notifyPlayerPickUp(Collision i_Collision, bool i_IsEnter)
    {
        if (parseCollision(i_Collision, out PickUpItem o_Pick))
        {
            if (i_IsEnter)
            {
                o_Pick.StatePlayer.EnterCollisionFoodObj(this.gameObject);
            }
            else
            {
                o_Pick.StatePlayer.ExitCollisionFoodObj();
            }
        }
        else
        {
            dedugger("notify-Player-Cant-PickUp", "eror :parse-Collision Fails");
        }
    }

    // ================================================
    // Delegates Invoke 

    // ================================================
    // ----------------Unity--------------------------- 

    /// <summary>
    /// this Delegates 
    /// </summary>
    /// <param name="i_Collision"></param>
    protected virtual void OnCollisionEnter(Collision i_Collision)
    {
        if (isPlayer(i_Collision) && !IsThrow)
        {
            notifyPlayerPickUp(i_Collision, k_Enter);
        }
        else if (IsThrow)
        {
            collisionAfterThrowingHandler(i_Collision);
        }
    }

    /// <summary>
    /// this wiil notify the Player thet he cant PickUp this
    /// </summary>
    /// <param name="i_Collision"></param>
    protected virtual void OnCollisionExit(Collision i_Collision)
    {
        if (isPlayer(i_Collision) && !IsThrow)
        {
            notifyPlayerPickUp(i_Collision, k_Exit);
        }
    }

    // ----------------GameFoodObj---------------------
    protected virtual void OnHitPlayer(EventArgs e)
    {
        HitPlayer?.Invoke(this, e);
    }

    private void destruction()
    {
        OnDestruction(EventArgs.Empty);
        Destroy(this.gameObject, 1);
    }

    protected virtual void OnDestruction(EventArgs e)
    {
        Destruction?.Invoke(this, e);
    }

    internal void CleanUpDelegatesPlayer()
    {
        //HitPlayer
    }
}