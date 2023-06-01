using Unity.Netcode;
using UnityEngine;
namespace DiningCombat.Player
{
    public class SoundEffects : NetworkBehaviour
    {
        [SerializeField]
        private AudioSource m_FootStepsVertical;
        [SerializeField]
        private AudioSource m_FootStepsHorizontal;
        public PlayerMovment m_Player;
        void Start()
        {
            m_FootStepsVertical.enabled = false;
            m_FootStepsHorizontal.enabled = false;
        }

        void Update()
        {
            bool vertical = m_Player.IsGrounded && Input.GetAxis("Horizontal") != 0;
            bool horizontal = vertical ? false : m_Player.IsGrounded && Input.GetAxis("Vertical") != 0;

            ActivationOfFootStepsSund(vertical, horizontal);
        }

        private void ActivationOfFootStepsSund(bool i_Vertical, bool i_Horizontal)
        {
            m_FootStepsVertical.enabled = i_Vertical;
            m_FootStepsHorizontal.enabled = i_Horizontal;
        }
    }
}