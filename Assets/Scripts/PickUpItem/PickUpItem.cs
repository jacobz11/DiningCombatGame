using UnityEngine;
using DiningCombat;
using Assets.Scripts.PickUpItem;


public class PickUpItem : MonoBehaviour
{
    public const bool k_ThrowObj = true;
    private const string k_AnimaThrowObj = GameGlobal.AnimationName.k_Throwing;
    // ==================================================
    // var
    // ==================================================
    private Animator m_Anim;
    private Rigidbody m_Rb;
    private Transform m_PickUpPoint;
    private Transform m_Player;
    private IStatePlayerHand m_State;
    protected KeyHamdler m_PowerKey;

    [SerializeField]
    public float m_ForceMulti;
    [SerializeField] 
    protected float m_PickUpDistance;
    //[SerializeField] 
    //private bool m_ReadyToThrow;
    [SerializeField] 
    private bool m_ItemIsPicked;


    // ==================================================
    // property
    // ==================================================

    public float ForceMulti
    {
        get
        {
            return m_ForceMulti;
        }
        set
        {
            m_ForceMulti = value;
            PowerCounter.PowerValue = m_ForceMulti;
        }
    }

    public IStatePlayerHand StatePlayerHand
    {
        get { return m_State; }
        set 
        { 
            m_State = value;
            m_State.InitState();
        }
    }

    public bool IsItemPicked
    {
        get { return m_ItemIsPicked; }
        set { m_ItemIsPicked = value; }
    }

    public bool HasItem 
    { 
        get { return !m_ItemIsPicked && m_PickUpPoint.childCount < 1; }
    }

    protected Transform FindPickUpPointTransform()
    {
        return GameObject.Find(GameGlobal.GameObjectName.k_PickUpPoint).transform;
    }
    // ==================================================
    // Unity engin 
    // ==================================================
    protected void Start()
    {
        m_Rb = GetComponent<Rigidbody>();
        m_Player = GameObject.Find(GameGlobal.GameObjectName.k_Player).transform;
        m_PickUpPoint = FindPickUpPointTransform();
        m_Anim = m_Player.GetComponentInChildren<Animator>();
        m_PowerKey = new KeyHamdler(GameKeyboardControls.k_PowerKey);
        m_State = new StateFree(this, new KeysHamdler(GameKeyboardControls.k_PowerKey));
    }

    protected void Update()
    {
        m_State.UpdateByState();
    }

    public bool IsDistance()
    {
        return Vector3.Distance(m_Player.position, transform.position) <= 2;
    }
    public void SetPhysics(bool i_SetTo)
    {
        GetComponent<Rigidbody>().useGravity = i_SetTo;
        GetComponent<BoxCollider>().enabled = i_SetTo;
    }

    public void SetTransform()
    {
        this.transform.position = m_PickUpPoint.position;
        this.transform.parent = FindPickUpPointTransform();
    }


    protected void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag(GameGlobal.TagNames.k_Capsule))
        {
            hitCapsule(col);
        }
    }

    private void hitCapsule(Collision col)
    {
        // ui Update
        ScoreCounter.ScoreValue++;

        // Destroy
        Destroy(col.gameObject, 1);
        Destroy(gameObject, 1);
    }

    internal void ThrowObj()
    {
        SetThrowingAnim(k_ThrowObj);
        m_Rb.AddForce(m_Player.transform.forward * ForceMulti);
        this.transform.parent = null;
        SetPhysics(true);
        m_ItemIsPicked = false;
    }

    internal void SetThrowingAnim(bool i_Var)
    {
         m_Anim.SetBool(k_AnimaThrowObj, i_Var);
    }
    //transform.localScale = sr_ScaleToRight;
}

//private bool isPickUpDistance()
//{
//    m_PickUpDistance = Vector3.Distance(m_Player.position, transform.position);
//    return m_PickUpDistance <= 2;
//}

//protected bool HaveItem()
//{
//    return m_PickUpPoint.childCount < 1;
//}

//protected bool ReadyToThrow
//{
//    get { return m_ReadyToThrow; }
//    set { m_ReadyToThrow = value; }
//}


//protected Animator Anim
//{
//    get
//    {
//        return m_Anim;
//    }
//}

//protected Rigidbody Rb
//{
//    get { return m_Rb; }
//    set { m_Rb = value; }
//}
//protected Transform PickUpPoint
//{
//    get { return m_PickUpPoint; }
//    //set { m_PickUpItem = value; }
//}
//protected Transform Player
//{
//    get { return m_Player; }
//    set { m_Player = value; }
//}

//protected Vector3 PlayerPosition
//{
//    get { return m_Player.position; }
//}


//private const float k_MinForceToThrow = 10f;
//// Hand-State enum
//private const byte k_HandStateFree = 0;
//private const byte k_HandStateHoldsObj = 1;
//private const byte k_HandStatePowering = 2;
//private const byte k_HandStateThrowing = 3;