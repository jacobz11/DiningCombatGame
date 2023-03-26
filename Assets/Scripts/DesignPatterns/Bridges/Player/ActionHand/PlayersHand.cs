using DiningCombat.Managers.Player;
using SojaExiles;
using System;
using UnityEngine;
using static DiningCombat.GameGlobal;

namespace Abstraction
{
    namespace DiningCombat
    {
        namespace Player
        {
            public class PlayerHand : MonoBehaviour
            {
                public event Action<bool, Collider> TriggerAction;
                private GameFoodObj m_FoodItem;
                [SerializeField]
                [Range(500f, 3000f)]
                public int m_MaxCargingPower = 1800;
                [SerializeField]
                public GameObject m_PikUpPonit;
                private Collider m_Collider;

                private void Awake()
                {
                    m_Collider = GetComponent<Collider>();
                }

                private void OnTriggerEnter(Collider other)
                {
                    TriggerAction?.Invoke(true, other);
                }
                private void OnTriggerExit(Collider other)
                {
                    TriggerAction?.Invoke(false, other);
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

                public Transform GetPoint()
                {
                    return this.m_PikUpPonit.transform;
                }

                public static void Builder(GameObject i_PlayerCharacter, ePlayerModeType i_Type,
                    out PlayerHand o_Player, out StateMachineImplemntor o_Implementor)
                {
                    o_Player = i_PlayerCharacter.AddComponent<PlayerHand>();

                    switch (i_Type)
                    {
                        case ePlayerModeType.OfflinePlayer:
                            Debug.Log("Builder  PlayerMovement : OfflinePlayer");
                            o_Implementor = i_PlayerCharacter.AddComponent<StateMachineImplemntor>();
                            o_Implementor.SetPlayerHand(o_Player);
                            o_Implementor.BuildOfflinePlayerState();
                            //List<State> states = new List<State> 
                            //{
                            //    // StateFree StateHoldsObj StatePowering StateThrowing
                            //    new PlayerP.StateFree(),
                            //    new PlayerP.StateHoldsObj(),
                            //    new PlayerP.StatePowering(),
                            //    new PlayerP.StateThrowing()
                            //};

                            //implementor.SetStates(states);
                            break;
                        case ePlayerModeType.OnlinePlayer:
                            Debug.Log("Builder  PlayerMovement : OnlinePlayer");
                            o_Implementor = null;
                            return;
                        case ePlayerModeType.OfflineAiPlayer:
                            Debug.Log("Builder  PlayerMovement : OfflineAiPlayer");
                            o_Implementor = null;
                            return;
                        case ePlayerModeType.OnlineAiPlayer:
                            Debug.Log("Builder  PlayerMovement : OnlineAiPlayer");
                            o_Implementor = null;
                            return;
                        default:
                            o_Implementor = null;
                            return;
                    }
                    // TODO : this 
                }
            }
        }
    }
}
