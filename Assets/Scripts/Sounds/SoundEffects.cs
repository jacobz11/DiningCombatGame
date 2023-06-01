using Unity.Netcode;
using UnityEngine;
namespace DiningCombat.Player
{
    public class SoundEffects : NetworkBehaviour
    {
        [SerializeField] private AudioSource m_FootStepsVertical;
        [SerializeField] private AudioSource m_FootStepsHorizontal;
        public PlayerMovment m_Player;
        // Start is called before the first frame update
        void Start()
        {
            m_FootStepsVertical.enabled = false;
            m_FootStepsHorizontal.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (m_Player.IsGrounded)
            {
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticallInput = Input.GetAxis("Vertical");
                if (verticallInput != 0)
                {
                    FootStepsVertical();
                }
                else if (horizontalInput != 0)
                {
                    FootStepsHorizontal();
                }
                else
                {
                    StopFootSteps();
                }
            }
            else
            {
                StopFootSteps();
            }
        }

        void FootStepsVertical()
        {
            m_FootStepsVertical.enabled = true;
            m_FootStepsHorizontal.enabled = false;
        }
        void FootStepsHorizontal()
        {
            m_FootStepsHorizontal.enabled = true;
            m_FootStepsVertical.enabled = false;
        }
        void StopFootSteps()
        {
            m_FootStepsVertical.enabled = false;
            m_FootStepsHorizontal.enabled = false;
        }
    }
}