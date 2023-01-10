using UnityEngine;
using DiningCombat;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using System;

public class PickUpItem : MonoBehaviour
{
    // Hand-State enum
    private const byte k_HandStateFree = 0;
    private const byte k_HandStateHoldsObj = 1;
    private const byte k_HandStatePowering = 2;
    private const byte k_HandStateThrowing = 3;
    private int m_HandState;
    private int HandState
    {
        get 
        { 
            return m_HandState;
        }
        set
        {
            m_HandState = value % 4;
        }
    }
    private const float k_MaxDistanceToPickUp = 2f;

    private Transform m_PickUpPoint;
    private Transform m_Player;
    private Rigidbody m_Rb;
    

    [SerializeField]
    public float m_PickUpDistance;

    [SerializeField]
    private bool m_IsReadyToThrow;
    public bool IsReadyToThrow
    {
        get
        {
            return m_IsReadyToThrow;
        }
        set
        {
            m_IsReadyToThrow = value;
        }
    }

    [SerializeField]
    private bool m_HasItemPicked;
    public bool HasItemPicked
    {
        get
        {
            return m_HasItemPicked;
        }
        set
        {
            m_HasItemPicked = value;
        }
    }

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

    public bool IsPowerPress
    {
        get
        {
            return Input.GetKeyUp(GameGlobal.k_PowerKey);
        }
    }
    protected void Start()
    {
        m_Rb = GetComponent<Rigidbody>();
        m_Player = GameObject.Find(GameGlobal.k_GameObjectPlayer).transform;
        m_PickUpPoint = GameObject.Find(GameGlobal.k_GameObjectPickUpPoint).transform;
    }

    protected void Update()
    {

        if (Input.GetKey(GameGlobal.k_PowerKey) && HasItemPicked == true && IsReadyToThrow)
        {
            ForceMulti += 1400 * Time.deltaTime;
        }

        m_PickUpDistance = Vector3.Distance(m_Player.position, transform.position);

        if (m_PickUpDistance <= 2)
        {
            if (Input.GetKeyDown(GameGlobal.k_PowerKey) && HasItemPicked == false && m_PickUpPoint.childCount < 1)
            {
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<BoxCollider>().enabled = false;
                this.transform.position = m_PickUpPoint.position;
                this.transform.parent = GameObject.Find(GameGlobal.k_GameObjectPickUpPoint).transform;

                HasItemPicked = true;
                ForceMulti = 0;
            }
        }

        if (Input.GetKeyUp(GameGlobal.k_PowerKey) && HasItemPicked == true)
        {
            IsReadyToThrow = true;

            if (ForceMulti > 10)
            {
                m_Rb.AddForce(m_Player.transform.forward * ForceMulti);
                this.transform.parent = null;
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<BoxCollider>().enabled = true;
                HasItemPicked = false;

                ForceMulti = 0;
                IsReadyToThrow = false;
            }
            ForceMulti = 0;
        }



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
    private void handFree()
    {
        // TODO:
        // This will be a function that checks if an object can be picked up
        if (haveAvailableGameObject())
        {
            // pickUpItem();
            HandState++;
        }
    }

    private bool haveAvailableGameObject()
    {
       // TODO: 
       return false;
    }

    private void handHoldsObj()
    {
        // TODO:
        // This function does when there is an object
        if (IsPowerPress)
        {
            HandState++;
        }
    }
    private void handPowering()
    {
        // TODO:
        // This function does when you press the power button
        if (IsPowerPress)
        {
            ForceMulti += 1400 * Time.deltaTime;
        }
        else
        {
            throwAway();
            HandState++;
        }

    }

    private void throwAway()
    {
        m_Rb.AddForce(m_Player.transform.forward * ForceMulti);
    }

    private void handThrowing()
    {
        // TODO:
        // This function calls after a throw

        // remove all the ...  
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<BoxCollider>().enabled = true;
        HasItemPicked = false;

        ForceMulti = 0;
        IsReadyToThrow = false;

        // and do handFree
        handFree();
    }




    protected void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag(GameGlobal.k_TagCapsule))
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