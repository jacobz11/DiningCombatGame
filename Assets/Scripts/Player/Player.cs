using DiningCombat.Manger;
using Unity.Netcode;
using UnityEngine;
namespace DiningCombat.Player
{
    public class Player : NetworkBehaviour
    {
        [SerializeField]
        private GameInput m_GameInput;
        [SerializeField]
        private Transform m_PickUpPoint;
        public Transform PicUpPoint { get; private set; }

        private void Awake()
        {
            GameStrting.Instance.AddNumOfPlyers(1);
        }

        private void GameInput_OnBostRunnigAction(object sender, System.EventArgs e)
        {
            Debug.Log("GameInput_OnChargingAction");
        }
    }
}