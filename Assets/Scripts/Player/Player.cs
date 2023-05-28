using DiningCombat.Manger;
using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;
namespace DiningCombat.Player
{
    public class Player : NetworkBehaviour
    {
        public event Action<bool> OnPlayerSweepFall;
        [SerializeField]
        private GameInput m_GameInput;
        [SerializeField]
        private Transform m_PickUpPoint;
        private PlayerAnimationChannel m_PlayerAnimation;
        public Transform PicUpPoint { get; private set; }

        private void Awake()
        {
            GameStrting.Instance.AddNumOfPlyers(1);
            m_PlayerAnimation = gameObject.GetComponentInChildren<PlayerAnimationChannel>();
        }

        private void GameInput_OnBostRunnigAction(object sender, System.EventArgs e)
        {
            Debug.Log("GameInput_OnChargingAction");
        }

        public IEnumerator ToggleSweepFallEnds()
        {
            OnPlayerSweepFall?.Invoke(true);
            m_PlayerAnimation.AnimationBool("SweepFall", true);
            yield return new WaitForSeconds(1);
            OnPlayerSweepFall?.Invoke(false);
            m_PlayerAnimation.AnimationBool("SweepFall", true);
        }
    }
}