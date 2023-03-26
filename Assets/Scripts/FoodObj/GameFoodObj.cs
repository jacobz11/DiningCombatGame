﻿using DiningCombat;
using System;
using UnityEngine;

public class GameFoodObj : MonoBehaviour
{
    public event EventHandler HitPlayer;

    public event EventHandler Destruction;

    private bool m_IsThrow;
    private Rigidbody m_Rigidbody;
    //private ThrowingGameObj m_HoldingGameObj;
    [SerializeField]
    private Vector3 m_Offset = new Vector3(-0.042f, -0.112f, 0.622f);
    [SerializeField]
    private Vector3 m_OffsetRo = new Vector3(330.269f, -82.976f, 265.504f);
    [SerializeField]
    private ParticleSystem m_Effect;
    [SerializeField]
    [Range(0f, 2f)]
    private float m_HitPlayerMull;
    [SerializeField]
    [Range(0f,100f)]
    private float m_MaxHitPlayerMull;

    public bool IsThrow 
    {
        get => m_IsThrow;
        private set
        {
            tag = GameGlobal.TagNames.k_ThrowFoodObj;
            m_IsThrow = value;
        }
    }

    public Action<GameFoodObj> Collect { get; internal set; }

    private void Awake()
    {
        m_Offset = new Vector3(
            UnityEngine.Random.Range(-1f, 1f),
            UnityEngine.Random.Range(-1f, 1f),
            UnityEngine.Random.Range(-1f, 1f));
        m_Rigidbody = GetComponent<Rigidbody>();
        if(m_Rigidbody == null ) 
        {
            Debug.LogError("cant find Rigidbody");
        }
        //if (m_HooldingPoint == null)
        //{
        //    Debug.LogError("Holding Game Obj in null in Awake" + this.name);
        //}
    }

    //protected virtual void Start()
    //{
    //    //m_Rigidbody = GetComponent<Rigidbody>();
    //    //m_Effect.Stop();
    //    if (m_HooldingPoint == null)
    //    {
    //        Debug.LogError("Holding Game Obj in null in Start :" + this.name);
    //    }
    //}


    public void ThrowFood(float i_ForceMulti, Vector3 i_ThrowDirection)
    {
        // remove parent
        transform.parent = null;
        m_Rigidbody.constraints = RigidbodyConstraints.None;
        // add Gravity
        m_Rigidbody.useGravity = true;

        actualThrow(i_ForceMulti*i_ThrowDirection);
    }


    private void actualThrow(Vector3 i_ThrowDirection)
    {
        IsThrow = true;

        // DrawRay for  Debuging
        Debug.DrawRay(transform.position, i_ThrowDirection, Color.black, 3);
        m_Rigidbody.AddForce(i_ThrowDirection);
    }

    //internal void SetHolderFoodObj(ThrowingGameObj i_HoldingGameObj)
    //{
    //    m_HoldingGameObj = i_HoldingGameObj;

    //    if(m_HoldingGameObj)
    //    {
    //        //this.transform.rotation = m_OffsetRo;
    //        Transform point = this.m_HoldingGameObj.GetPoint();
    //        this.transform.SetParent(point, true);
    //        this.transform.position = point.position + m_Offset;
    //        float uDistance = Vector3.Distance(this.transform.position, point.position);
    //        Debug.Log(uDistance);

    //        //if (m_HoldingGameObj is HandPickUp)
    //        //{
    //        //    this.m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    //        //}
    //    }
    //}

    //private void updatePosition()
    //{
    //    GameObject pickUpItem = m_HoldingGameObj.gameObject;
    //    this.transform.position = pickUpItem.transform.position;
    //    this.transform.SetParent(pickUpItem.transform, false);
    //    this.transform.localPosition = pickUpItem.transform.localPosition;

    //    if (m_HoldingGameObj is HandPickUp)
    //    {
    //        m_Rigidbody.useGravity = false;
    //    }
    //}

    //private void collisionAfterThrowingHandler(Collision i_Collision)
    //{
    //    if (isPlayer(i_Collision))
    //    {
    //        PlayerHp playerHit = i_Collision.gameObject.GetComponent<PlayerHp>();
    //        int kill = 0;
    //        float hitPoint = GetHitPonit();

    //        if (playerHit != null)
    //        {
    //            if (playerHit.HitYou(hitPoint))
    //            {
    //                kill = 1;
    //            }
    //        }

    //        if (m_HoldingGameObj.DidIHurtMyself(i_Collision))
    //        {
    //            Debug.Log("you stupid son of a bitch? You hurt yourself");
    //        }
    //        else
    //        {
    //            Debug.Log("OnHitPlayer");
    //            OnHitPlayer(new EventHitPlayer(kill, (int)hitPoint));
    //        }
    //    }

    //    performTheEffect();
    //    destruction();
    //}

    private float GetHitPonit()
    {
        float x = Math.Abs(this.m_Rigidbody.velocity.x);
        float y = Math.Abs(this.m_Rigidbody.velocity.y);
        float z = Math.Abs(this.m_Rigidbody.velocity.z);
        Vector3 v = new Vector3(x, y, z);
        float res = m_HitPlayerMull * (x + y + z);
        m_HitPlayerMull = 0;
        
        return Math.Min(res, m_MaxHitPlayerMull);
    }

    private bool performTheEffect()
    {
        if(IsThrow && m_Effect != null)
        {
            ParticleSystem effect = Instantiate(m_Effect, transform.position, transform.rotation);

            effect.Play();
            Destroy(effect, 1.5f);
        }

        return true;
    }


    private bool isPlayer(Collision i_Collision)
    {
        return i_Collision.gameObject.CompareTag(GameGlobal.TagNames.k_Player);
    }

    protected virtual void OnCollisionEnter(Collision i_Collision)
    {
        if (IsThrow && m_HitPlayerMull != 0)
        {
            //collisionAfterThrowingHandler(i_Collision);
        }
        //else if (isPlayer(i_Collision)) 
        //{
        //    this.m_Rigidbody.velocity = getInverseVelocity(i_Collision.gameObject);
        //}
        
    }

    private Vector3 getInverseVelocity(GameObject gameObject)
    {
        Debug.Log("in getInverseVelocity");
        float invX = 0;
        float invY = 0;
        float invZ = 0;
        if (gameObject != null)
        {
            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                invX = rigidbody.velocity.x * (-0.5f);
                //invY = rigidbody.velocity.y * (-0.5f);
                invZ = rigidbody.velocity.z * (-0.5f);
            }
        }

        return new Vector3(invX, invY, invZ);
    }

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
    }

    //internal bool GetCollectPosition(out Vector3 o_pod)
    //{
    //    //bool isCollect = m_HoldingGameObj != null;
    //    //o_pod = isCollect ? transform.position : Vector3.zero;

    //    //return isCollect;
    //}
}
