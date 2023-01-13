using Assets.Scripts.PickUpItem;
using DiningCombat;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PickUpItem : MonoBehaviour
{
    private const string k_ClassName = nameof(PickUpItem);
    [SerializeField]
    public float m_ForceMulti;
    private IStatePlayerHand m_State;
    private KeysHamdler m_Power;
    private GameObject m_GameObject;

    // ==================================================
    // property
    // ==================================================

    public float ForceMulti
    {
        get => m_ForceMulti;
        set
        {
            m_ForceMulti = value;
            PowerCounter.PowerValue = m_ForceMulti;
        }
    }
    public IStatePlayerHand StatePlayerHand
    {
        get
        {
            if(m_State == null)
            {
                dedugger("StatePlayerHand", "m_State == null ");
                m_State = new StateFree(this);

            }

            return m_State;
        }
        set
        {
            m_State = value;
            m_State.InitState();
        }
    }

    internal KeysHamdler Power
    {
        get => m_Power;
    }

    protected void Start()
    {
        Debug.Log("");
        ForceMulti = 0;
        m_Power = new KeysHamdler(GameKeyboardControls.k_PowerKey);
        m_State = new StateFree(this);
    }

    protected void Update()
    {
        StatePlayerHand.UpdateByState();
    }

    internal void SetGameFoodObj(GameObject i_GameObject)
    {
        dedugger("SetGameFoodObj", "enter ");
 
        m_GameObject = i_GameObject;
        m_GameObject.transform.SetParent(this.transform, false);
    }

    internal void ThrowObj()
    {
        dedugger("ThrowObj", "enter ");
    }

    private void dedugger(string func ,string i_var)
    {
        GameGlobal.Dedugger(k_ClassName, func, i_var);
    }
    protected void OnCollisionEnter(Collision col)
    {
        
        if (col.gameObject.CompareTag(GameGlobal.TagNames.k_FoodObj))
        {
            dedugger("OnCollisionEnter", "CompareTag " +
                GameGlobal.TagNames.k_FoodObj + " true");
            m_State.EnterCollisionFoodObj(col.gameObject);
        }
        else
        {
        }
    }

    protected void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag(GameGlobal.TagNames.k_FoodObj))
        {
            dedugger("OnCollisionExit", "CompareTag " +
                GameGlobal.TagNames.k_FoodObj + " true");
            m_State.ExitCollisionFoodObj();
        }
    }
}









//using UnityEngine;
//using DiningCombat;
//using Assets.Scripts.PickUpItem;
//using Assets.Scripts.FoodObj;
//using System;

//public class PickUpItem : MonoBehaviour
//{
//    public const bool k_ThrowObj = true;
//    private const string k_AnimaThrowObj = GameGlobal.AnimationName.k_Throwing;

//    // ==================================================
//    // var
//    // ==================================================
//    private Animator m_Anim;
//    private Rigidbody m_Rigidbody;

//    private Transform m_Player;
//    private IStatePlayerHand m_State;
//    protected KeyHamdler m_PowerKey;
//    private FoodTypeData m_FoodTypeData;

//    [SerializeField]
//    public float m_ForceMulti;
//    [SerializeField] 
//    protected float m_PickUpDistance;
//    [SerializeField] 
//    private bool m_ItemIsPicked;
//    private GameFoodObj m_FoodObject;

//    // ==================================================
//    // property
//    // ==================================================

//    public float ForceMulti
//    {
//        get => m_ForceMulti;
//        set
//        {
//            m_ForceMulti = value;
//            PowerCounter.PowerValue = m_ForceMulti;
//        }
//    }

//    public IStatePlayerHand StatePlayerHand
//    {
//        get => m_State; 
//        set 
//        { 
//            m_State = value;
//            m_State.InitState();
//        }
//    }

//    public bool IsItemPicked
//    {
//        get => m_ItemIsPicked; 
//        set => m_ItemIsPicked = value;
//    }

//    private Transform FindPickUpPointTransform 
//        => GameObject.Find(GameGlobal.GameObjectName.k_PickUpPoint).transform;

//    // ==================================================
//    // Unity engin 
//    // ==================================================
//    void Start()
//    {
//        m_Rigidbody = GetComponent<Rigidbody>();

//        m_Player = GetComponent<PlayerMovement>().transform;
//        //GameObject.Find(GameGlobal.GameObjectName.k_Player).transform;
//        m_Anim = GetComponentInParent<Animator>();

//        if (m_Anim == null)
//        {
//            Debug.Log("null GetComponent<Animator>()");
//            m_Anim.GetComponent<PlayerMovement>().GetComponent<Animator>();
//            if (m_Anim == null)
//            {
//                Debug.Log("null GetComponent<PlayerMovement>().GetComponent<Animator>();");
//                m_Anim.GetComponent<Animator>();
//            }
//        }

//        m_PowerKey = new KeyHamdler(GameKeyboardControls.k_PowerKey);
//        m_State = new StateFree(this, new KeysHamdler(GameKeyboardControls.k_PowerKey));
//    }

//    public void SetGameFoodObj(GameObject i_GameObject)
//    {
//        m_FoodObject = i_GameObject.GetComponent<GameFoodObj>();
//        m_FoodObject.transform.parent = this.transform;
//        m_FoodObject.transform.position = this.transform.position;
//        m_State = new StateHoldsObj(this, new KeysHamdler(GameKeyboardControls.k_PowerKey));
//    }
//    protected void Update()
//    {
//        if (m_State == null)
//        {
//            Debug.Log("is null");
//            m_State = new StateHoldsObj(this, new KeysHamdler(GameKeyboardControls.k_PowerKey));
//        }
//        else
//        {
//            m_State.UpdateByState();
//        }
//    }

//    public void SetPhysics(bool i_SetTo)
//    {
//        GetComponent<Rigidbody>().useGravity = i_SetTo;
//        GetComponent<BoxCollider>().enabled = i_SetTo;
//    }

//    protected void OnCollisionEnter(Collision col)
//    {
//        Debug.Log(col.gameObject.tag);
//        if (col.gameObject.CompareTag(GameGlobal.TagNames.k_FoodObj))
//        {
//            Debug.Log("CompareTag ");
//            m_State.EnterCollisionFoodObj(col.gameObject);
//        }
//        else
//        {
//        }
//    }

//    protected void OnCollisionExit(Collision col)
//    {
//        if (col.gameObject.CompareTag(GameGlobal.TagNames.k_FoodObj))
//        {
//            m_State.ExitCollisionFoodObj();
//        }
//    }

//    private void hitCapsule(Collision col)
//    {
//        ScoreCounter.ScoreValue++;
//    }

//    internal void ThrowObj()
//    {
//        SetThrowingAnim(k_ThrowObj);
//        m_FoodObject.HitPlayer += OnHitPlayer;
//        m_FoodObject.ThrowFood(ForceMulti, m_Player.transform.forward);
//    }

//    internal void SetThrowingAnim(bool i_Var)
//    {
//         m_Anim.SetBool(k_AnimaThrowObj, i_Var);
//    }

//    protected virtual void OnHitPlayer(object i_S, EventArgs e)
//    {

//    }
//}
