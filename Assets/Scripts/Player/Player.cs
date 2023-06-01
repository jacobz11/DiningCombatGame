using DiningCombat.Manger;
using DiningCombat.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
namespace DiningCombat.Player
{
    public class Player : NetworkBehaviour
    {
        public event Action<bool> OnPlayerSweepFall;
        [SerializeField]
        private Transform m_PickUpPoint;
        [SerializeField]
        private bool m_IsAI;
        [SerializeField]
        private List<LifePointsVisual> m_LifePointsVisual;
        public bool IsAi { get { return m_IsAI; } }
        public PlayerAnimationChannel PlayerAnimation { get; private set; }
        public PlayerLifePoint PlayerLifePoint { get; private set; }
        public PlayerScore PlayerScore { get; private set; }
        public Transform PicUpPoint { get; private set; }
        public GameInput GameInput { get; private set; }
        public ActionStateMachine ActionState { get; private set; }

        private void Awake()
        {
            GameStrting.Instance.AddNumOfPlyers(1);
            PlayerAnimation = gameObject.GetComponentInChildren<PlayerAnimationChannel>();
            PlayerLifePoint = gameObject.GetComponent<PlayerLifePoint>();
            PlayerScore = gameObject.GetComponent<PlayerScore>();
            GameInput = gameObject.GetComponent<GameInput>();
            ActionState = gameObject.GetComponent<ActionStateMachine>();
            GameOverLogic.Instance.CharacterEntersTheGame(PlayerLifePoint);

            if (!m_IsAI)
            {
                PlayerScore.OnPlayerKillsChanged += PlayerScoreVisel.Instance.UpdeteValueKills;
                PlayerScore.OnPlayerScorePointChanged += PlayerScoreVisel.Instance.UpdeteValueScore;


                GameInput.OnStartChargingAction += ActionState.GameInput_OnStartChargingAction;
                GameInput.OnStopChargingAction += ActionState.GameInput_OnStopChargingAction;
                GameInput.OnPickUpAction += ActionState.GameInput_OnPickUpAction;

            }

            PlayerLifePoint.OnPlayerLifePointChanged += PlayerLifePoint_OnPlayerLifePointChanged;

        }

        private void PlayerLifePoint_OnPlayerLifePointChanged(float i_NewLifePoint)
        {
            float normalizHp = i_NewLifePoint / PlayerLifePoint.k_StrtingLifePoint;
            m_LifePointsVisual.ForEach(visual => { visual.UpdateBarNormalized(normalizHp); });
        }

        public IEnumerator ToggleSweepFallEnds()
        {
            Debug.Log("ToggleSweepFallEnds");
            OnPlayerSweepFall?.Invoke(true);
            PlayerAnimation.AnimationBool("SweepFall", true);
            yield return new WaitForSeconds(1);
            OnPlayerSweepFall?.Invoke(false);
            PlayerAnimation.AnimationBool("SweepFall", true);
        }
    }
}