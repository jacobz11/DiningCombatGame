using DiningCombat;
using System;
using Unity.VisualScripting;
using UnityEngine;


public class GameFoodObj : MonoBehaviour
{
    private const string k_ClassName = nameof(GameFoodObj);
    public event EventHandler HitPlayer;
    public event EventHandler Destruction;

    private bool m_IsThrow;
    private Rigidbody m_Rigidbody;

    public bool IsThrow 
    {
        get => m_IsThrow;
        private set
        {
            m_IsThrow = value;
        }
    }
    private void dedugger(string func, string i_var)
    {
        GameGlobal.Dedugger(k_ClassName, func, i_var);
    }
    protected void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    protected void OnCollisionEnter(Collision collision)
    {
        string func = System.Reflection.MethodBase.GetCurrentMethod().Name;

        if (isPlayer(collision))
        {
            dedugger(func, "in CompareTag tag" + collision.gameObject.tag);

            if (!IsThrow && parrsFormCollision(collision, out PickUpItem o_Pick))
            {
                o_Pick.StatePlayerHand.EnterCollisionFoodObj(this.gameObject);
            }
            else
            {
                OnHitPlayer(EventArgs.Empty);
            }
        }
        else
        {
            dedugger(func, "else CompareTag tag" + collision.gameObject.tag);
        }
    }

    protected void OnCollisionExit(Collision collision)
    {
        string func = System.Reflection.MethodBase.GetCurrentMethod().Name;

        if (isPlayer(collision))
        {
            if (!IsThrow && parrsFormCollision(collision, out PickUpItem o_Pick))
            {
                o_Pick.StatePlayerHand.ExitCollisionFoodObj();
            }
        }
        else
        {
            dedugger(func, "else CompareTag tag" + collision.gameObject.tag);
        }
    }

    private bool parrsFormCollision(Collision collision, out PickUpItem o_pic)
    {
        o_pic = collision.gameObject.GetComponentInChildren(typeof(PickUpItem)) as PickUpItem;
        
        if (o_pic != null)
        {
            dedugger("parrsFormCollision", "success");
            return true;
        }

        dedugger("parrsFormCollision", "failure");
        return false;
    }

    private bool isPlayer(Collision collision)
    {
        return collision.gameObject.CompareTag(GameGlobal.TagNames.k_Player);
    }

    //destruction();
    //if (collision.gameObject.CompareTag(GameGlobal.TagNames.k_Player))
    //{
    //    
    //}
    //



    private void destruction()
    {
        OnDestruction(EventArgs.Empty);
        Destroy(this);
    }

    public void ThrowFood(float i_ForceMulti, Vector3 i_ThrowForward)
    {
        IsThrow = true;
        m_Rigidbody.AddForce(i_ThrowForward * i_ForceMulti);
    }

    protected virtual void OnHitPlayer(EventArgs e)
    {
        HitPlayer?.Invoke(this, e);
    }

    protected virtual void OnDestruction(EventArgs e)
    {
        Destruction?.Invoke(this, e);
    }
}

