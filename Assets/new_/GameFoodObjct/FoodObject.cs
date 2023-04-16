//using Assets.Scripts.Util.Channels.Abstracts;
//using DiningCombat.FoodObj.Managers;
//using DiningCombat;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class HitPint
//{
//    public float MaximumAmount { get; set; }
//    public float MinimumAmount { get; set; }
//    public float MullPoint { get; set; }
//}
//internal class FoodObject : MonoBehaviour, IViewingElementsPosition, ICollectable, IThrowable,
//    IAnimationDisturbing, IEffectable
//{
//    public event Action<float, int> HitPlayer;
//    public event Action Destruction;

//    private Rigidbody m_Rigidbody;
//    private ICollecter m_Collecter;
//    [SerializeField]
//    private int m_FramesToExitPlayer;
//    [SerializeField]
//    private HitPint m_MaxHitPlayerMull;
//    [SerializeField]
//    private ParticleSystem m_EffectPrePab;

//    public bool IsCollect 
//    {
//        get => tag == GameGlobal.TagNames.k_Picked;
//    }
//    public bool IsThrow 
//    {
//        get => tag == GameGlobal.TagNames.k_ThrowFoodObj;
//    }

//    public bool IsUesed { get; private set; }

//    public ParticleSystem Effect => m_EffectPrePab;

//    private void Awake()
//    {
//        m_Rigidbody = GetComponent<Rigidbody>();
//        Debug.Assert(m_Rigidbody is not null, "cant find Rigidbody for the Game FoodObj Awake");
//        m_MaxHitPlayerMull = new HitPint();
//        m_MaxHitPlayerMull.MaximumAmount = 20;
//        m_MaxHitPlayerMull.MullPoint = 0.2f;
//        m_MaxHitPlayerMull.MinimumAmount = 5;
//    }

//    public void Collect(ICollecter i_Collecter)
//    {
//        if (!IsCollect)
//        {
//            if (i_Collecter is not null)
//            {
//                m_Collecter = i_Collecter;
//                DisableRagdoll();
//                this.transform.position = m_Collecter.PikUpPonit.position;
//                this.transform.SetParent(m_Collecter.PikUpPonit, true);
//                tag = GameGlobal.TagNames.k_Picked;
//                ManagerGameFoodObj.Singlton.UickedFruit -= ViewElement;
//            }
//        }
//        else
//        {
//            Debug.LogWarning("cant Collect this obj if it Colleted");
//            return;
//        }

//    }

//    public void DisableRagdoll()
//    {
//        m_Rigidbody.isKinematic = true;
//        m_Rigidbody.detectCollisions = false;
//    }

//    public void EnableRagdoll()
//    {
//        m_Rigidbody.isKinematic = false;
//        m_Rigidbody.detectCollisions = true;
//    }

//    public float HitPintCalculator()
//    {
//        float x = Math.Abs(this.m_Rigidbody.velocity.x);
//        float y = Math.Abs(this.m_Rigidbody.velocity.y);
//        float z = Math.Abs(this.m_Rigidbody.velocity.z);

//        Vector3 v = new Vector3(x, y, z);
//        float res = m_MaxHitPlayerMull.MullPoint * (x + y + z);
//        m_MaxHitPlayerMull.MullPoint = 0;

//        return Math.Min(res, m_MaxHitPlayerMull.MinimumAmount);
//    }

//    protected virtual void OnCollisionEnter(Collision i_Collision)
//    {
//        if (IsThrow && !IsUesed)
//        {
//            IsUesed = true;
//            collisionAfterThrowingHandler(i_Collision);
//        }
//    }

//    private void collisionAfterThrowingHandler(Collision i_Collision)
//    {
//        if (i_Collision.gameObject.CompareTag(GameGlobal.TagNames.k_Player))
//        {
//            IntiraelPlayerManger playerManger = i_Collision.gameObject.GetComponentInChildren<IntiraelPlayerManger>();
//            if (playerManger != null)
//            {
//                Debug.Log("playerManger != null ");
//                float hitPoint = HitPintCalculator();
//                playerManger.OnHitPlayer(hitPoint, out bool o_IsKiil);
//                int kill = o_IsKiil ? 1 : 0;

//                if (m_Collecter.DidIHurtMyself(i_Collision))
//                {
//                    Debug.Log("you stupid son of a bitch? You hurt yourself");
//                }
//                else
//                {
//                    HitPlayer?.Invoke(hitPoint, kill);
//                }
//            }
//        }

//        PerformTheEffect();
//        destruction();
//    }

//    private void destruction()
//    {
//        Destruction?.Invoke();
//        Destroy(this.gameObject, 1);
//    }

//    public void Throw(Vector3 i_Direction, float i_Force)
//    {
//        Debug.Log("Throw Force:" + i_Force);
//        EnableRagdoll();
//        transform.parent = null;
//        m_Rigidbody.useGravity = true;
//        m_Rigidbody.AddForce(i_Force * i_Direction);
//        StartCoroutine(WaitForFrames());
//        Debug.DrawRay(transform.position, i_Direction, Color.black, 3);
//    }

//    private IEnumerator WaitForFrames()
//    {
//        for (int i = 0; i < m_FramesToExitPlayer; ++i)
//        {
//            yield return null;
//        }
//        SetToThrowing();
//    }
//    public void ViewElement(List<Vector3> elements)
//    {
//        elements.Add(transform.position);
//    }

//    public void SetToThrowing()
//    {
//        tag = GameGlobal.TagNames.k_ThrowFoodObj;
//    }

//    public bool Unsed()
//    {
//        throw new NotImplementedException();
//    }

//    public void OnEndUsing()
//    {
//        throw new NotImplementedException();
//    }

//    public void PerformTheEffect()
//    {
//        if (IsThrow && Effect != null)
//        {
//            ParticleSystem effect = Instantiate(Effect, transform.position, transform.rotation);

//            effect.Play();
//            Destroy(effect, 1.5f);
//        }
//    }
//}
