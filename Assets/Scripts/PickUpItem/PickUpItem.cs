using UnityEngine;
using DiningCombat;
using Assets.Scripts.PickUpItem;


public class PickUpItem : MonoBehaviour
{

    private const float k_MinForceToThrow = 10f;
    // Hand-State enum
    private const byte k_HandStateFree = 0;
    private const byte k_HandStateHoldsObj = 1;
    private const byte k_HandStatePowering = 2;
    private const byte k_HandStateThrowing = 3;

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
    [SerializeField] 
    private bool m_ReadyToThrow;
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
    public IStatePlayerHand StatePlayerHand
    {
        get { return m_State; }
        set 
        { 
            m_State = value;
            m_State.InitState();
        }
    }

    //protected bool ReadyToThrow
    //{
    //    get { return m_ReadyToThrow; }
    //    set { m_ReadyToThrow = value; }
    //}

    public bool IsItemPicked
    {
        get { return m_ItemIsPicked; }
        set { m_ItemIsPicked = value; }
    }

    public bool HasItem 
    { 
        get { return !m_ItemIsPicked && m_PickUpPoint.childCount < 1; }
    }

    //public bool IsItemPicked { get; internal set; }

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
        m_Anim = GetComponentInChildren<Animator>();
        m_PowerKey = new KeyHamdler(GameKeyboardControls.k_PowerKey);
        m_State = new StateFree(this, new KeysHamdler(GameKeyboardControls.k_PowerKey));
    }

    // Update is called once per frame
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
    //protected bool HaveItem()
    //{
    //    return m_PickUpPoint.childCount < 1;
    //}

    protected void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag(GameGlobal.TagNames.k_Capsule))
        {
            HitCapsule(col);
        }
    }

    protected void HitCapsule(Collision col)
    {
        // ui Update
        ScoreCounter.ScoreValue++;

        // Destroy
        Destroy(col.gameObject, 1);
        Destroy(gameObject, 1);
    }
    //private bool isPickUpDistance()
    //{
    //    m_PickUpDistance = Vector3.Distance(m_Player.position, transform.position);
    //    return m_PickUpDistance <= 2;
    //}

    internal void ThrowObj()
    {
        m_Rb.AddForce(m_Player.transform.forward * ForceMulti);
        this.transform.parent = null;
        SetPhysics(true);
        m_ItemIsPicked = false;
        m_ReadyToThrow = false;
    }
}









































































/*using UnityEngine;
using DiningCombat;
using Unity.VisualScripting;

public class PickUpItem : MonoBehaviour
{
    private const float k_MinForceToThrow = 10f;
    // Hand-State enum
    private const byte k_HandStateFree = 0;
    private const byte k_HandStateHoldsObj = 1;
    private const byte k_HandStatePowering = 2;
    private const byte k_HandStateThrowing = 3;

    // ==================================================
    // var
    // ==================================================
    private Transform m_PickUpPoint;
    private Transform m_Player;
    private Rigidbody m_Rb;
    private float m_LastPower;

    // Hand-State
    private int m_HandState;
    private int HandState
    {
        get 
        { 
            return m_HandState;
        }
        set
        {
            //if (m_LastPower )
            //if ()
            //{
            //    if (HandState == k_HandStatePowering)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        bool isTime = m_LastPower + 0.5f > Time.time;
            //        if (isTime)
            //        {
            //            m_LastPower = Time.time;
            //        }
            //        return isTime;
            //    }

            //}
            //else
            //{
            //    return false;
            //}
            m_HandState = value % 4;
            Debug.Log("HandState chang" + m_HandState);
        }
    }

    // Force-Multi
    [SerializeField]
    public float m_ForceMulti;

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

    private Animator m_Anim;
    // ==================================================
    // property
    // ==================================================
    public bool IsPowerPress()
    {
        return Input.GetKey(GameGlobal.k_PowerKey);
    }

    // ==================================================
    // Unity engin 
    // ==================================================
    protected void Start()
    {
        m_LastPower = Time.time;
        m_Rb = GetComponent<Rigidbody>();
        m_Player = GameObject.Find(GameGlobal.GameObjectName.k_Player).transform;
        m_PickUpPoint = GameObject.Find(GameGlobal.GameObjectName.k_PickUpPoint).transform;
        m_Anim = GetComponentInChildren<Animator>();
    }

    protected void Update()
    {
        switch (HandState)
        {
            case k_HandStateFree:
                handFree();
                break;
            case k_HandStateHoldsObj:
                handHoldsObj();
                break;
            case k_HandStatePowering:
                handPowering();
                break;
            case k_HandStateThrowing:
                handThrowing();
                break;
            default:
                HandState = k_HandStateFree;
                break;
        }
    }

    // ==================================================
    // Hand-State handler
    // ==================================================
    // hand-Free-case
    private void handFree()
    {
        if (haveAvailableGameObject())
        {
            setPhysics(false, out Transform o_Parent);
            this.transform.parent = o_Parent;
            this.transform.position = m_PickUpPoint.position;

            HandState++;
        }
    }

    private bool haveAvailableGameObject()
    {
        bool res = false;
        
        if (IsPowerPress())
        {
            bool isInDistance = Vector3.Distance(m_Player.position, transform.position) 
                <= GameGlobal.k_MinDistanceToPickUp;
            bool hasItemPicked = m_PickUpPoint.childCount < GameGlobal.k_MaxItemToPick;
            res = isInDistance && hasItemPicked;
        }
        
        return res;
    }

    // hand-Holds-Obj
    private void handHoldsObj()
    {
        if (IsPowerPress())
        {
            HandState++;
        }
    }
    private void handPowering()
    {
        if (IsPowerPress())
        {
            ForceMulti += 1400 * Time.deltaTime;
        }
        else
        {
            if (ForceMulti > k_MinForceToThrow)
            {
                throwAway();
                HandState++;
            }
            else
            {
                ForceMulti = 0;
                HandState--;
            }
        }

    }

    private void throwAway()
    {
        m_Anim.SetBool(GameGlobal.AnimationName.k_Throwing, true);
        m_Rb.AddForce(m_Player.transform.forward * ForceMulti);
        setPhysics(true, out Transform o_Parent);
        this.transform.parent = o_Parent;
    }

    private void handThrowing()
    {
        ForceMulti = 0;
        HandState = k_HandStateFree;
        handFree();
    }

    private void setPhysics(bool i_Var, out Transform o_ParentToSet)
    {
        GetComponent<Rigidbody>().useGravity = i_Var;
        GetComponent<BoxCollider>().enabled = i_Var;

        o_ParentToSet = !i_Var ? GameObject.Find(GameGlobal.GameObjectName.k_Player).transform: null;
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
}
//if (Input.GetKey(GameGlobal.k_PowerKey) && HasItemPicked == true && IsReadyToThrow)
//{
//    ForceMulti += 1400 * Time.deltaTime;
//}

//m_PickUpDistance = Vector3.Distance(m_Player.position, transform.position);

//if (m_PickUpDistance <= 2)
//{
//    if (Input.GetKeyDown(GameGlobal.k_PowerKey) && HasItemPicked == false && m_PickUpPoint.childCount < 1)
//    {
//        GetComponent<Rigidbody>().useGravity = false;
//        GetComponent<BoxCollider>().enabled = false;
//        this.transform.position = m_PickUpPoint.position;
//        

//        HasItemPicked = true;
//        ForceMulti = 0;
//    }
//}

//if (Input.GetKeyUp(GameGlobal.k_PowerKey) && HasItemPicked == true)
//{
//    IsReadyToThrow = true;

//    if (ForceMulti > 10)
//    {
//        m_Rb.AddForce(m_Player.transform.forward * ForceMulti);
//        this.transform.parent = null;
//        GetComponent<Rigidbody>().useGravity = true;
//        GetComponent<BoxCollider>().enabled = true;
//        HasItemPicked = false;

//        ForceMulti = 0;
//        IsReadyToThrow = false;
//    }
//    ForceMulti = 0;
//}



*//*using UnityEngine;
using DiningCombat;

public class PickUpItem : MonoBehaviour
{
    private Transform m_PickUpPoint;
    private Transform m_Player;
    private Rigidbody m_Rb;

    [SerializeField]
    private float m_PickUpDistance;

    [SerializeField]
    private float m_ForceMulti;

    public float ForceMulti
    {
        get { return m_ForceMulti; }
        set { m_ForceMulti = value; }
    }

    [SerializeField]
    private bool m_ReadyToThrow;

    [SerializeField]
    private bool m_ItemIsPicked;
    private const float k_MaxForcex = 1900;
    private const bool k_Enabled = true;
    private const bool k_UseGravity = true;

    private bool IsCanThrowObj
    {
        get
        {
            return Input.GetKey(GameGlobal.k_PowerKey) && m_ItemIsPicked;
        }
    }

    private Transform findPickUpPoint()
    {
        return GameObject.Find(GameGlobal.k_GameObjectPickUpPoint).transform;
    }

    protected void Start()
    {
        m_Rb = GetComponent<Rigidbody>();
        m_Player = GameObject.Find(GameGlobal.k_GameObjectPlayer).transform;
        m_PickUpPoint = findPickUpPoint();
    }

    // Update is called once per frame
    protected void Update()
    {
        bool isAddForce = IsCanThrowObj && m_ReadyToThrow && m_ForceMulti < k_MaxForcex;

        if (isAddForce)
        {
            m_ForceMulti += 500 * Time.deltaTime;
            PowerCounter.PowerValue = m_ForceMulti;
        }

        m_PickUpDistance = Vector3.Distance(m_Player.position, transform.position);

        if (m_PickUpDistance <= 2 && IsThrowGameObj)
        {
            EnabledGravity = !k_UseGravity;
            EnabledBoxCollider = !k_Enabled;
            this.transform.position = m_PickUpPoint.position;
            this.transform.parent = findPickUpPoint();

            m_ItemIsPicked = true;
            m_ForceMulti = 0;
        }

        if (IsCanThrowObj)
        {
            m_ReadyToThrow = true;

            if (m_ForceMulti > 10)
            {
                m_Rb.AddForce(m_Player.transform.forward * m_ForceMulti);
                this.transform.parent = null;

                EnabledGravity = k_UseGravity;
                EnabledBoxCollider = k_Enabled;

                m_ItemIsPicked = false;
                m_ReadyToThrow = false;
                m_ForceMulti = 0;
            }

            m_ForceMulti = 0;
        }
    }
    private bool EnabledGravity
    {
        get
        {
            return m_Rb.useGravity;
        }
        set
        {
            m_Rb.useGravity =value;
        }
    }

    private bool EnabledBoxCollider
    {
        get
        {
            return GetComponent<BoxCollider>().enabled;
        }
        set
        {
            EnabledBoxCollider = value;
        }
    }

    private bool IsThrowGameObj
    {
        get
        {
            bool isPrssTo = Input.GetKeyDown(GameGlobal.k_PowerKey);
            bool haveItemTo = m_PickUpPoint.childCount < 1;

            return isPrssTo && !m_ItemIsPicked && haveItemTo;
        }
    }

    protected void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag(GameGlobal.k_TagCapsule))
        {
            ScoreCounter.ScoreValue++;
            Destroy(col.gameObject, 1);
            Destroy(gameObject, 1);
        }
    }
}
*/


/*using UnityEngine;
using DiningCombat;

public class PickUpItem : MonoBehaviour
{
    private Transform m_PickUpPoint;
    private Transform m_Player;
    private Rigidbody m_Rb;

    [SerializeField]
    private float m_PickUpDistance;

    [SerializeField]
    private float m_ForceMulti;

    public float ForceMulti
    {
        get { return m_ForceMulti; }
        set { m_ForceMulti = value; }
    }

    [SerializeField]
    private bool m_IsReadyToThrow;

    [SerializeField]
    private bool m_HasItemPicked;
    private const float k_MaxForcex = 1900;
    private const bool k_Enabled = true;
    private const bool k_UseGravity = true;

    private bool IsCanThrowObj
    {
        get
        {
            return Input.GetKey(GameGlobal.k_PowerKey) && m_HasItemPicked;
        }
    }

    private Transform findPickUpPoint()
    {
        return GameObject.Find(GameGlobal.k_GameObjectPickUpPoint).transform;
    }

    protected void Start()
    {
        m_Rb = GetComponent<Rigidbody>();
        m_Player = GameObject.Find(GameGlobal.k_GameObjectPlayer).transform;
        m_PickUpPoint = findPickUpPoint();
    }

    // Update is called once per frame
    protected void Update()
    {
        bool isAddForce = IsCanThrowObj && m_IsReadyToThrow && m_ForceMulti < k_MaxForcex;

        if (isAddForce)
        {
            m_ForceMulti += 500 * Time.deltaTime;
            PowerCounter.PowerValue = m_ForceMulti;
        }

        m_PickUpDistance = Vector3.Distance(m_Player.position, transform.position);

        if (m_PickUpDistance <= 2 && IsThrowGameObj)
        {
            EnabledGravity = !k_UseGravity;
            EnabledBoxCollider = !k_Enabled;
            this.transform.position = m_PickUpPoint.position;
            this.transform.parent = findPickUpPoint();

            m_HasItemPicked = true;
            m_ForceMulti = 0;
        }

        if (IsCanThrowObj)
        {
            m_IsReadyToThrow = true;

            if (m_ForceMulti > 10)
            {
                m_Rb.AddForce(m_Player.transform.forward * m_ForceMulti);
                this.transform.parent = null;

                EnabledGravity = k_UseGravity;
                EnabledBoxCollider = k_Enabled;

                m_HasItemPicked = false;
                m_IsReadyToThrow = false;
                m_ForceMulti = 0;
            }

            m_ForceMulti = 0;
        }
    }
    private bool EnabledGravity
    {
        get
        {
            return m_Rb.useGravity;
        }
        set
        {
            m_Rb.useGravity =value;
        }
    }

    private bool EnabledBoxCollider
    {
        get
        {
            return GetComponent<BoxCollider>().enabled;
        }
        set
        {
            EnabledBoxCollider = value;
        }
    }

    private bool IsThrowGameObj
    {
        get
        {
            bool isPrssTo = Input.GetKeyDown(GameGlobal.k_PowerKey);
            bool haveItemTo = m_PickUpPoint.childCount < 1;

            return isPrssTo && !m_HasItemPicked && haveItemTo;
        }
    }

    protected void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag(GameGlobal.k_TagCapsule))
        {
            ScoreCounter.ScoreValue++;
            Destroy(col.gameObject, 1);
            Destroy(gameObject, 1);
        }
    }
}
*/