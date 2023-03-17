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
        private int m_CurrentHandState;
        private float m_ChargingPower;
        private Animator m_PlayerAnimator;
        private IStatePlayerHand[] m_ArrayOfPlayerState;
        private GameFoodObj m_FoodItem;
        [SerializeField]
        private Vector3 m_Buffer = new Vector3(-0.3f, 0, -0.5f);
        [SerializeField]
        [Range(500f, 3000f)]
        public int m_MaxCargingPower = 1800;
        [SerializeField]
        public GameObject m_PikUpPonit;
        [SerializeField]
        private FilliStatus m_ForceMultiUi;
        private Collider m_Collider;

        public override float ForceMulti
        {
            get => this.m_ChargingPower;
            set
            {
                this.m_ChargingPower = Math.Max(Math.Min(value, this.m_MaxCargingPower), 0);
                m_ForceMultiUi.UpdateFilliStatus = this.m_ChargingPower;
            }
        }

        public int StatePlayerHand
        {
            get => this.m_CurrentHandState;
            set
            {
                this.m_CurrentHandState = value % this.m_ArrayOfPlayerState.Length;
                this.m_ArrayOfPlayerState[this.m_CurrentHandState].InitState();
            }
        }

        public IStatePlayerHand StatePlayer
        {
            get => this.m_ArrayOfPlayerState[this.StatePlayerHand];
        }

        internal bool ThrowingAnimator
        {
            get => this.m_PlayerAnimator.GetBool(GameGlobal.AnimationName.k_Throwing);
            set
            {
                if (value)
                {
                    this.m_PlayerAnimator.SetBool(GameGlobal.AnimationName.k_RunningSide, false);
                    this.m_PlayerAnimator.SetBool(GameGlobal.AnimationName.k_Running, false);
                }

                this.m_PlayerAnimator.SetBool(GameGlobal.AnimationName.k_Throwing, value);
            }
        }

        private void Awake()
        {
            this.m_ArrayOfPlayerState = new IStatePlayerHand[]
            {
                new StateFree(this),
                new StateHoldsObj(this),
                new StatePowering(this),
                new StateThrowing(this),
            };
        }

        private void Start()
        {
            this.m_PlayerAnimator = this.GetComponentInParent<Animator>();
            this.m_Collider = this.GetComponent<Collider>();
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

        public void OnTriggerEnter(Collider other)
        {
            this.StatePlayer.EnterCollisionFoodObj(other);
        }

        public void OnTriggerExit(Collider other)
        {
            this.StatePlayer.ExitCollisionFoodObj(other);
        }

        internal override void SetGameFoodObj(GameObject i_GameObject)
        {
            bool isSucceed = false;

            if (i_GameObject == null)
            {
                this.m_FoodItem = null;
            }
            else
            {
                GameFoodObj obj = i_GameObject.GetComponent<GameFoodObj>();
                if (obj != null)
                {
                    isSucceed = true;
                    this.m_FoodItem = obj;
                    obj.SetHolderFoodObj(this);
                }
            }

            this.m_Collider.enabled = !isSucceed;

            //return isSucceed;
        }

        internal override void ThrowObj()
        {
            if (this.m_FoodItem == null)
            {
                Debug.LogError("foodItem is null");
            }
            else
            {
                Rigidbody foodRb = this.m_FoodItem.GetComponent<Rigidbody>();
                Debug.DrawRay(this.m_PikUpPonit.transform.position, this.m_PikUpPonit.transform.forward, Color.blue, 10f);

                foodRb.constraints = RigidbodyConstraints.None;
                this.m_FoodItem.transform.parent = null;
                foodRb.AddForce(this.m_PikUpPonit.transform.forward * this.ForceMulti);
                this.m_FoodItem = null;
            }
            this.StatePlayerHand = 0;
        }

        public override Transform GetPoint()
        {
            return this.m_PikUpPonit.transform;
        }
    }
}