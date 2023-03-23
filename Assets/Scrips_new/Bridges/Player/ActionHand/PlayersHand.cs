using Assets.Scrips_new.DesignPatterns;
using DiningCombat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static DiningCombat.GameGlobal;
using PlayerP = Assets.Scrips_new.Player;

namespace Assets.Scrips_new.Bridges.Player.ActionHand
{
    public class PlayersHand : MonoBehaviour
    {
        private int m_CurrentHandState;
        private float m_ChargingPower;
        private Animator m_PlayerAnimator;
        private IStatePlayerHand[] m_ArrayOfPlayerState;
        private GameFoodObj m_FoodItem;
        [SerializeField]
        [Range(500f, 3000f)]
        public int m_MaxCargingPower = 1800;
        [SerializeField]
        public GameObject m_PikUpPonit;
        [SerializeField]
        private FilliStatus m_ForceMultiUi;
        [SerializeField]
        private PlayerScore m_Score;
        private Collider m_Collider;

        public float ForceMulti
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
                int changTo = m_FoodItem != null ?
                    value % this.m_ArrayOfPlayerState.Length : 0;
                this.m_CurrentHandState = changTo;
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

        private void Start()
        {
            this.m_PlayerAnimator = this.GetComponentInParent<Animator>();
            this.m_Collider = this.GetComponent<Collider>();
            this.StatePlayerHand = 0;
        }

        private void Update()
        {
            this.StatePlayer.UpdateByState();
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
                    //obj.SetHolderFoodObj(this);
                }
            }

            this.m_Collider.enabled = !isSucceed;

            //return isSucceed;
        }

        internal void ThrowObj()
        {
            if (this.m_FoodItem == null)
            {
                Debug.LogError("foodItem is null");
            }
            else
            {
                GameFoodObj gameFoodObj = m_FoodItem.GetComponent<GameFoodObj>();

                gameFoodObj.HitPlayer += m_Score.OnHitPlayer;

                m_FoodItem.ThrowFood(ForceMulti, this.m_PikUpPonit.transform.forward);
                m_FoodItem = null;

                //Rigidbody foodRb = this.m_FoodItem.GetComponent<Rigidbody>();
                //Debug.DrawRay(this.m_PikUpPonit.transform.position, this.m_PikUpPonit.transform.forward, Color.blue, 10f);

                //this.m_FoodItem.transform.parent = null;
                //foodRb.AddForce(this.m_PikUpPonit.transform.forward * this.ForceMulti);
                //this.m_FoodItem = null;
            }
            this.StatePlayerHand = 0;
        }

        public Transform GetPoint()
        {
            return this.m_PikUpPonit.transform;
        }

        public static void Builder(GameObject i_PlayerCharacter, ePlayerModeType i_Type)
        {
            PlayersHand player = i_PlayerCharacter.AddComponent<PlayersHand>();
            StateMachineImplemntor implementor;

            switch (i_Type)
            {
                case ePlayerModeType.OfflinePlayer:
                    Debug.Log("Builder  PlayerMovement : OfflinePlayer");
                    implementor = i_PlayerCharacter.AddComponent<StateMachineImplemntor>();
                    implementor.SetPlayersHand(player);
                    List<State> states = new List<State> 
                    {
                                // StateFree StateHoldsObj StatePowering StateThrowing
                        new PlayerP.StateFree(),
                        new PlayerP.StateHoldsObj(),
                        new PlayerP.StatePowering(),
                        new PlayerP.StateThrowing()
                    };
                    implementor.SetStates(states);
                    break;
                case ePlayerModeType.OnlinePlayer:
                    Debug.Log("Builder  PlayerMovement : OnlinePlayer");
                    return;
                case ePlayerModeType.OfflineAiPlayer:
                    Debug.Log("Builder  PlayerMovement : OfflineAiPlayer");
                    return;
                case ePlayerModeType.OnlineAiPlayer:
                    Debug.Log("Builder  PlayerMovement : OnlineAiPlayer");
                    return;
            }
            // TODO : this 
        }
    }
}
