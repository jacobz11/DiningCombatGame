namespace Assets.Scripts.Player
{
    using Assets.Scripts.Player.StatePlayerRightHand;
    using DiningCombat;
    using System;
    using UnityEngine;

    public class PlayerRightHand : MonoBehaviour
    {
        // location : Player-> Goalie Throw -> mixamorig:Hips
        // -> mixamorig:Spine -> mixamorig:Spine1 -> mixamorig:Spine2
        // ->mixamorig:RightShoulder -> mixamorig:RightArm -> mixamorig:RightForeArm
        // ->mixamorig:RightHand -> PickUpPoint
        private int currentHandState;
        private Animator playerAnimator;
        private IStatePlayerHand[] arrayOfPlayerState;
        private GameObject foodItem;
        [SerializeField]
        private Vector3 buffer = new Vector3(-0.3f, 0, -0.5f);
        [SerializeField]
        [Range(500f, 3000f)]
        public int maxCargingPower = 1800;
        [SerializeField]
        public GameObject pikUpPint;

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
            this.StatePlayerHand = 0;
        }

        private void Update()
        {
            this.StatePlayer.UpdateByState();
            //UpdateBuffer();
        }

        private void UpdateBuffer()
        {
            if (this.foodItem != null)
            {
                this.foodItem.transform.position = this.pikUpPint.transform.position + this.buffer;
            }
        }

        public void OnThrowingAnimator()
        {
            this.StatePlayer.OnThrowingAnimator();
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

        internal bool CollectFoodItem(GameObject collectFood)
        {
            bool isSucceed = false;
            if (collectFood == null)
            {
                this.foodItem = null;
            }
            else
            {
                collectFood.transform.position = this.pikUpPint.transform.position + buffer;
                collectFood.transform.SetParent(this.pikUpPint.transform, true);
                collectFood.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                this.foodItem = collectFood;
                isSucceed = true;
            }

            return isSucceed;
        }

        internal void ThrowObj(float throwingForce)
        {
            if (this.foodItem == null)
            {
                Debug.LogError("foodItem is null");
            }
            else
            {
                Rigidbody foodRb = this.foodItem.GetComponent<Rigidbody>();
                foodRb.constraints = RigidbodyConstraints.None;
                foodRb.AddForce(this.foodItem.transform.forward * throwingForce);
            }

            this.StatePlayerHand = 0;
        }
    }
}
