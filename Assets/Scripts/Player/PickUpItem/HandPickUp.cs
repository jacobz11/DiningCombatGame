namespace Assets.Scripts.Player
{
    using System;
    using Assets.Scripts.PickUpItem;
    using Assets.Scripts.Player.PickUpItem;
    using DiningCombat;
    using UnityEngine;
    using UnityEngine.Events;

    public class HandPickUp : ThrowingGameObj
    {
        // location : Player-> Goalie Throw -> mixamorig:Hips
        // -> mixamorig:Spine -> mixamorig:Spine1 -> mixamorig:Spine2
        // ->mixamorig:RightShoulder -> mixamorig:RightArm -> mixamorig:RightForeArm
        // ->mixamorig:RightHand -> PickUpPoint
        private int currentHandState;
        private float chargingPower;
        private Animator playerAnimator;
        private IStatePlayerHand[] arrayOfPlayerState;
        private GameFoodObj foodItem;
        [SerializeField]
        private Vector3 buffer = new Vector3(-0.3f, 0, -0.5f);
        [SerializeField]
        [Range(500f, 3000f)]
        public int maxCargingPower = 1800;
        [SerializeField]
        public GameObject pikUpPint;
        [SerializeField]
        private FilliStatus ForceMultiUi;
        private Collider collider;

        public override float ForceMulti
        {
            get => this.chargingPower;
            set
            {
                this.chargingPower = Math.Max(Math.Min(value, this.maxCargingPower), 0);
                ForceMultiUi.UpdateFilliStatus = this.chargingPower;
            }
        }

        public int StatePlayerHand
        {
            get => this.currentHandState;
            set
            {
                this.currentHandState = value % this.arrayOfPlayerState.Length;
                this.arrayOfPlayerState[this.currentHandState].InitState();
            }
        }

        public IStatePlayerHand StatePlayer
        {
            get => this.arrayOfPlayerState[this.StatePlayerHand];
        }

        internal bool ThrowingAnimator
        {
            get => this.playerAnimator.GetBool(GameGlobal.AnimationName.k_Throwing);
            set
            {
                if (value)
                {
                    this.playerAnimator.SetBool(GameGlobal.AnimationName.k_RunningSide, false);
                    this.playerAnimator.SetBool(GameGlobal.AnimationName.k_Running, false);
                }

                this.playerAnimator.SetBool(GameGlobal.AnimationName.k_Throwing, value);
            }
        }

        private void Awake()
        {
            this.arrayOfPlayerState = new IStatePlayerHand[]
            {
                new StateFree(this),
                new StateHoldsObj(this),
                new StatePowering(this),
                new StateThrowing(this),
            };
        }

        private void Start()
        {
            this.playerAnimator = this.GetComponentInParent<Animator>();
            this.collider = this.GetComponent<Collider>();
            this.StatePlayerHand = 0;
        }

        private void Update()
        {
            this.StatePlayer.UpdateByState();
            //UpdateBuffer();
        }

        private void UpdateBuffer()
        {
            //if (this.foodItem != null)
            //{
            //    Vector2 v = this.pikUpPint.transform.position + this.buffer;
            //    this.foodItem.transform.position = v;
            //}
            //Debug.DrawRay(this.pikUpPint.transform.position, this.pikUpPint.transform.forward, Color.green, 2f);
        }

        public void OnThrowingAnimator()
        {
            this.ThrowingAnimator = false;
            this.ThrowObj();
        }

        public void OnThrowingAnimaEnd()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            this.StatePlayer.EnterCollisionFoodObj(other);
        }

        private void OnTriggerExit(Collider other)
        {
            this.StatePlayer.ExitCollisionFoodObj(other);
        }

        internal override void SetGameFoodObj(GameObject i_GameObject)
        {
            bool isSucceed = false;

            if (i_GameObject == null)
            {
                this.foodItem = null;
            }
            else
            {
                GameFoodObj obj = i_GameObject.GetComponent<GameFoodObj>();
                if (obj != null)
                {
                    isSucceed = true;
                    this.foodItem = obj;
                    obj.SetHolderFoodObj(this);
                }
            }

            this.collider.enabled = !isSucceed;

            //return isSucceed;
        }

        internal override void ThrowObj()
        {
            if (this.foodItem == null)
            {
                Debug.LogError("foodItem is null");
            }
            else
            {
                Rigidbody foodRb = this.foodItem.GetComponent<Rigidbody>();
                Debug.DrawRay(this.pikUpPint.transform.position, this.pikUpPint.transform.forward, Color.blue, 10f);

                foodRb.constraints = RigidbodyConstraints.None;
                this.foodItem.transform.parent = null;
                foodRb.AddForce(this.pikUpPint.transform.forward * this.ForceMulti);
                this.foodItem = null;
            }
            this.StatePlayerHand = 0;
        }

        public override Transform GetPoint()
        {
            return this.pikUpPint.transform;
        }
    }
}




//using Assets.Scripts.PickUpItem;
//using Assets.Scripts.Player.PickUpItem;
//using DiningCombat;
//using System;
//using UnityEngine;

///// <summary>
///// this is a  
///// </summary>
//public class HandPickUp : ThrowingGameObj
//{
//    // location : Player-> Goalie Throw -> mixamorig:Hips
//    // -> mixamorig:Spine -> mixamorig:Spine1 -> mixamorig:Spine2
//    // ->mixamorig:RightShoulder -> mixamorig:RightArm -> mixamorig:RightForeArm
//    // ->mixamorig:RightHand -> PickUpPoint

//    // ================================================
//    // constant Variable 
//    public const byte k_Free = 0;
//    public const byte k_HoldsObj = 1;
//    public const byte k_Powering = 2;
//    public const byte k_Throwing = 3;

//    // ================================================
//    // Delegate

//    // ================================================
//    // Fields 
//    private IStatePlayerHand[] m_PlayerState;
//    private KeysHamdler m_Power;
//    private GameObject m_GameFoodObj;
//    private Animator m_Anim;
//    private float m_MaxSlderVal;
//    private float m_MinSlderVal;
//    // ================================================
//    // ----------------Serialize Field-----------------
//    public float m_ForceMulti;
//    private int m_StateVal;
//    [SerializeField]
//    private bool m_EventTrow;
//    [SerializeField]
//    private bool m_EventEnd;
//    [SerializeField]
//    private FilliStatus m_PowerSlider;

//    // ================================================
//    // properties
//    public override float ForceMulti
//    {
//        get => m_ForceMulti;
//        set
//        {
//            m_ForceMulti = Math.Max(Math.Max(value, m_MaxSlderVal), m_MinSlderVal);
//            //m_PowerSlider.UpdateFilliStatus = m_ForceMulti;
//            PowerCounter.PowerValue = m_ForceMulti;
//        }
//    }

//    // input
//    internal KeysHamdler Power
//    {
//        get => m_Power;
//    }

//    // State machine
//    public int StatePlayerHand
//    {
//        get => m_StateVal;
//        set
//        {
//            m_StateVal = value % m_PlayerState.Length;
//            m_PlayerState[m_StateVal].InitState();
//        }
//    }

//    // State machine
//    public IStatePlayerHand StatePlayer
//    {
//        get => m_PlayerState[StatePlayerHand];
//    }

//    // Animator
//    internal bool ThrowingAnimator
//    {
//        get
//        {
//            return m_Anim.GetBool(GameGlobal.AnimationName.k_Throwing);
//        }
//        set
//        {
//            if (value)
//            {
//                m_Anim.SetBool(GameGlobal.AnimationName.k_RunningSide, false);
//                m_Anim.SetBool(GameGlobal.AnimationName.k_Running, false);
//            }

//            m_Anim.SetBool(GameGlobal.AnimationName.k_Throwing, value);
//        }
//    }

//    // Animator
//    public bool EventTrow
//    {
//        get => m_EventTrow;
//        set => m_EventTrow = value;
//    }

//    // Animator
//    public bool EventEnd
//    {
//        get => m_EventEnd;
//        set =>m_EventEnd = value;
//    }

//    // ================================================
//    // auxiliary methods programmings


//    // ================================================
//    // Unity Game Engine

//    private void Awake()
//    {
//        m_PlayerState = new IStatePlayerHand[]
//        {
//            new StateFree(this),
//            new StateHoldsObj(this),
//            new StatePowering(this),
//            new StateThrowing(this)
//        };

//        EventTrow = false;
//        EventEnd = false;
//        m_Power = KeysHamdler.Builder("Power");
//    }
//    protected void Start()
//    {
//        m_ForceMulti = m_PowerSlider.GetSliderCurAndMaxAndMinValue(out m_MaxSlderVal,
//                out m_MinSlderVal);
//        m_Anim = GetComponentInParent<Animator>();
//        StatePlayerHand = k_Free;
//    }

//    public void OnThrowingAnimator()
//    {
//        //StatePlayer.SetEventTrowing();
//    }
//    public void OnThrowingAnimaEnd()
//    {
//        //Debug.Log("in SetEventTrowingEnd");

//        //StatePlayer.SetEventTrowingEnd();
//        //EventEnd = true;
//    }
//    protected void Update()
//    {
//        UpdateByState();
//        //StatePlayer.UpdateByState();
//    }

//    // ================================================
//    //  methods

//    // ================================================
//    // auxiliary methods

//    // ================================================
//    // Delegates Invoke 

//    // ================================================
//    // ----------------Unity--------------------------- 
//    void OnTriggerEnter(Collider other)
//    {
//        StatePlayer.EnterCollisionFoodObj(other);
//    }

//    void OnTriggerExit(Collider other)
//    {
//        StatePlayer.ExitCollisionFoodObj(other);
//    }
//    // ----------------GameFoodObj---------------------
//    protected virtual void On_HitPlayer_GameFoodObj(object i_Sender, EventArgs e)
//    {
//        ScoreCounter.ScoreValue++;
//    }

//    /// <summary>
//    /// null - will set the fild m_GameFoodObj to null and set the animation k_Throwing to false 
//    /// </summary>
//    /// <param name="i_GameObject">null or GameObject</param>
//    internal override void SetGameFoodObj(GameObject i_GameObject)
//    {
//        if (i_GameObject == null)
//        {
//            m_GameFoodObj = null;
//            ThrowingAnimator = false;
//        }
//        else
//        {
//            GameFoodObj obj = i_GameObject.GetComponent<GameFoodObj>();

//            if (obj != null)
//            {
//                m_GameFoodObj = i_GameObject;
//                obj.SetHolderFoodObj(this);
//                StatePlayerHand++;
//            }
//        }
//    }

//    /// <summary>
//    /// 
//    /// </summary>
//    internal override void ThrowObj()
//    {
//        GameFoodObj foodObj = m_GameFoodObj.GetComponent<GameFoodObj>();

//        if (foodObj != null)
//        {
//            foodObj.CleanUpDelegatesPlayer();
//            foodObj.HitPlayer += On_HitPlayer_GameFoodObj;
//            foodObj.ThrowFood(ForceMulti, this.transform.forward);
//        }
//    }

//    public void UpdateByState()
//    {
//        StatePlayer.UpdateByState();
//    }

//    public void InitState()
//    {
//    }

//    public bool IsPassStage()
//    {
//        return StatePlayer.IsPassStage();
//    }

//    public void EnterCollisionFoodObj(Collider other)
//    {
//        StatePlayer.EnterCollisionFoodObj(other);
//    }

//    public void ExitCollisionFoodObj(Collider other)
//    {
//        StatePlayer.ExitCollisionFoodObj(other);
//    }

//    public void SetEventTrowingEnd()
//    {
//    }

//    public void SetEventTrowing()
//    {
//    }
//}






/// <summary>
/// null - will set the fild m_GameFoodObj to null and set the animation k_Throwing to false 
/// </summary>
/// <param name="i_GameObject">null or GameObject</param>
//internal void SetGameFoodObj(GameObject i_GameObject)
//{
//    if (i_GameObject == null)
//    {
//        m_GameFoodObj = null;
//        ThrowingAnimator = false;
//    }
//    else
//    {
//        GameFoodObj obj = i_GameObject.GetComponent<GameFoodObj>();

//        if (obj != null)
//        {
//            m_GameFoodObj = i_GameObject;
//            obj.SetHolderFoodObj(this);
//            StatePlayerHand++;
//        }
//    }
//}

//internal void ThrowObj()
//{
//    GameFoodObj foodObj = m_GameFoodObj.GetComponent<GameFoodObj>();

//    if (foodObj != null)
//    {
//        foodObj.CleanUpDelegatesPlayer();
//        foodObj.HitPlayer += On_HitPlayer_GameFoodObj;
//        foodObj.ThrowFood(ForceMulti, this.transform.forward);
//    }
//}

