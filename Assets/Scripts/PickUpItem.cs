using UnityEngine;
using DiningCombat;

public class PickUpItem : MonoBehaviour
{
    private Transform pickUpPoint;
    private Transform player;
    private Rigidbody rb;

    [SerializeField] public float pickUpDistance;
    [SerializeField] public float forceMulti;
    [SerializeField] public bool readyToThrow;
    [SerializeField] public bool itemIsPicked;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;
        pickUpPoint = GameObject.Find("PickUpPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.E) && itemIsPicked == true && readyToThrow)
        {
            forceMulti += 1400 * Time.deltaTime;
            PowerCounter.PowerValue = forceMulti;
        }

        pickUpDistance = Vector3.Distance(player.position, transform.position);

        if (pickUpDistance <= 2)
        {
            if (Input.GetKeyDown(KeyCode.E) && itemIsPicked == false && pickUpPoint.childCount < 1)
            {
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<BoxCollider>().enabled = false;
                this.transform.position = pickUpPoint.position;
                this.transform.parent = GameObject.Find("PickUpPoint").transform;

                itemIsPicked = true;
                forceMulti = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.E) && itemIsPicked == true)
        {
            readyToThrow = true;

            if (forceMulti > 10)
            {
                rb.AddForce(player.transform.forward * forceMulti);
                this.transform.parent = null;
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<BoxCollider>().enabled = true;
                itemIsPicked = false;

                forceMulti = 0;
                readyToThrow = false;
            }
            forceMulti = 0;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Capsule"))
        {
            ScoreCounter.ScoreValue++;
            Destroy(col.gameObject, 1);
            Destroy(gameObject, 1);
        }

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