using DiningCombat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChannel : MonoBehaviour
{
    public event Action<Collision> CollisionEnter;
    public event Action<Collision> CollisionExit;
    public event Action<Collider> PickUpZonEnter;
    public event Action<Collider> PickUpZonExit;

    [SerializeField]
    protected GameObject m_PickUpPoint;
    public GameObject PickUpPoint
    {
        get
        {
            return m_PickUpPoint;
        }
        protected set 
        {
        m_PickUpPoint = value;
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
        CollisionEnter?.Invoke(collision);
    }

    protected virtual void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit");

        CollisionExit?.Invoke(collision);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameGlobal.TagNames.k_FoodObj))
        {
            PickUpZonEnter?.Invoke(other);
        }
    }

    protected virtual void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag(GameGlobal.TagNames.k_FoodObj))
        {
            PickUpZonExit?.Invoke(other);
        }
    }
}
