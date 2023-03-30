using System;
using UnityEngine;
using DiningCombat.FoodObj;
using static DiningCombat.GameGlobal;
using DiningCombat.Player.Manger;

namespace DiningCombat.Player
{
    public class PlayerHand : MonoBehaviour
    {
        public event Action<bool, Collider> TriggerAction;
        private GameFoodObj m_FoodItem;
        [SerializeField]
        [Range(500f, 3000f)]
        public int m_MaxCargingPower = 1800;
        private Transform m_PikUpPonit;
        private PlayerAnimationChannel m_AnimationChannel;
        private float m_ChargingPower;

        public Transform PikUpPonit
        {
            get => m_PikUpPonit;
            private set => m_PikUpPonit= value;
        }
        public bool ThrowingAnimator 
        {
            get => false;
            internal set
            {
                if (value) 
                { 
                    m_AnimationChannel.SetPlayerAnimationToThrow(); 
                }
            }
        }
        public float ForceMulti
        {
            get => this.m_ChargingPower;
            set
            {
                this.m_ChargingPower = Math.Max(Math.Min(value, this.m_MaxCargingPower), 0);
            }
        }
        private void Awake()
        {
            m_AnimationChannel = gameObject.GetComponentInChildren<PlayerAnimationChannel>();
        }

        internal void SetGameFoodObj(GameObject i_GameObject)
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
                    obj.PickedUp(this);
                }
            }

            //this.m_Collider.enabled = !isSucceed;
        }

        internal void ThrowObj()
        {
            Debug.Log("ThrowObj");
            if (this.m_FoodItem == null)
            {
                Debug.LogError("foodItem is null");
            }
            else
            {
                GameFoodObj gameFoodObj = m_FoodItem.GetComponent<GameFoodObj>();

                //gameFoodObj.HitPlayer += m_Score.OnHitPlayer;

                //m_FoodItem.ThrowFood(ForceMulti, this.m_PikUpPonit.transform.forward);
                m_FoodItem = null;

                //Rigidbody foodRb = this.m_FoodItem.GetComponent<Rigidbody>();
                //Debug.DrawRay(this.m_PikUpPonit.transform.position, this.m_PikUpPonit.transform.forward, Color.blue, 10f);

                //this.m_FoodItem.transform.parent = null;
                //foodRb.AddForce(this.m_PikUpPonit.transform.forward * this.ForceMulti);
                //this.m_FoodItem = null;
            }
        }


        public void SetPickUpPoint(Transform i_PickUpPoint)
        {
            if (PikUpPonit is null)
            {
                PikUpPonit = i_PickUpPoint;
            }
            else
            {
                Debug.LogError("Try to set PickUpPoint more then once");
            }
        }

        internal bool DidIHurtMyself(Collision i_Collision)
        {
            return this.gameObject.Equals(i_Collision.gameObject);
        }

        //public static void Builder(GameObject i_PlayerCharacter, ePlayerModeType i_Type,
        //    out PlayerHand o_Player, out OfflinePlayerStateMachine o_Implementor)
        //{
        //    o_Player = i_PlayerCharacter.AddComponent<PlayerHand>();

        //    switch (i_Type)
        //    {
        //        case ePlayerModeType.OfflinePlayer:
        //            Debug.Log("Builder  PlayerMovement : OfflinePlayer");
        //            o_Implementor = i_PlayerCharacter.AddComponent<OfflinePlayerStateMachine>();
        //            o_Implementor.SetPlayerHand(o_Player);
        //            o_Implementor.BuildOfflinePlayerState();
        //            //List<State> states = new List<State> 
        //            //{
        //            //    // StateFree StateHoldsObj StatePowering StateThrowing
        //            //    new PlayerP.StateFree(),
        //            //    new PlayerP.StateHoldsObj(),
        //            //    new PlayerP.StatePowering(),
        //            //    new PlayerP.StateThrowing()
        //            //};

        //            //implementor.SetStates(states);
        //            break;
        //        case ePlayerModeType.OnlinePlayer:
        //            Debug.Log("Builder  PlayerMovement : OnlinePlayer");
        //            o_Implementor = null;
        //            return;
        //        case ePlayerModeType.OfflineAiPlayer:
        //            Debug.Log("Builder  PlayerMovement : OfflineAiPlayer");
        //            o_Implementor = null;
        //            return;
        //        case ePlayerModeType.OnlineAiPlayer:
        //            Debug.Log("Builder  PlayerMovement : OnlineAiPlayer");
        //            o_Implementor = null;
        //            return;
        //        default:
        //            o_Implementor = null;
        //            return;
        //    }
        //    // TODO : this 
        //}
    }
}

