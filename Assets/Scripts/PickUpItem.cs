using UnityEngine;
using DiningCombat;

public class PickUpItem : MonoBehaviour
{
    private const float k_MaxDistanceToPickUp = 2f;
    private Transform m_PickUpPoint;
    private Transform m_Player;
    private Rigidbody m_Rb;
    private bool m_IsPowerPressed;
    private bool IsPowerPressed
    {
        get 
        { 
            return m_IsPowerPressed; 
        }
        set
        {
            m_IsPowerPressed = value;
        }
    }

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

    private bool isAddForce()
    {
        return IsPowerPressed && HasItemPicked && IsReadyToThrow;
    }

    protected void Start()
    {
        m_Rb = GetComponent<Rigidbody>();
        m_Player = GameObject.Find(GameGlobal.k_GameObjectPlayer).transform;
        m_PickUpPoint = GameObject.Find(GameGlobal.k_GameObjectPickUpPoint).transform;
    }

    protected void Update()
    {
        // var Update 
        IsPowerPressed = Input.GetKeyUp(GameGlobal.k_PowerKey);
        m_PickUpDistance = Vector3.Distance(m_Player.position, transform.position);
        
        if (isAddForce())
        {
            ForceMulti += 1400 * Time.deltaTime;
        }

        bool isDistanceValid = m_PickUpDistance <= k_MaxDistanceToPickUp;
        bool doesntHaveItme = m_PickUpPoint.childCount < 1;

        if (IsPowerPressed && ! HasItemPicked && isDistanceValid && doesntHaveItme)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<BoxCollider>().enabled = false;
            this.transform.position = m_PickUpPoint.position;
            this.transform.parent = GameObject.Find(GameGlobal.k_GameObjectPickUpPoint).transform;

            HasItemPicked = true;
            ForceMulti = 0;
        }

        if (IsPowerPressed && HasItemPicked)
        {
            IsReadyToThrow = true;

            if (ForceMulti > 10)
            {
                //  throw away
                m_Rb.AddForce(m_Player.transform.forward * ForceMulti);

                // reset all var 
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<BoxCollider>().enabled = true;
                this.transform.parent = null;
                HasItemPicked = false;
                IsReadyToThrow = false;
                ForceMulti = 0;
            }

            ForceMulti = 0;
        }
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